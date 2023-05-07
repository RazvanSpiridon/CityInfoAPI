using CityInfoAPI.Context;
using CityInfoAPI.Entities;
using CityInfoAPI.Models;
using CityInfoAPI.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CityInfoAPI.Services
{
    public class PointOfInterestService : IPointOfInterestService
    {
        private readonly CityInfoDbContext _dbContext;

        public PointOfInterestService(CityInfoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PointOfInterestDto> AddPointOfInterest(PointOfInterestDto pointOfInterestDto)
        {
            var cityExists = await _dbContext.Cities.Where(c => c.CityId == pointOfInterestDto.CityId).AnyAsync();
            if (cityExists == false)
                return new PointOfInterestDto();

            var pointOfInterest = new PointOfInterest();
            pointOfInterest.PointOfInterestId = new Guid();
            PointOfInterestDto.MapToEntity(pointOfInterest, pointOfInterestDto);

            await _dbContext.PointOfInterests.AddAsync(pointOfInterest);
            await _dbContext.SaveChangesAsync();

            return PointOfInterestDto.MapToModel(pointOfInterest);
        }

        public async Task<PointOfInterestDto> DeletePointOfInterest(Guid pointOfInterestId)
        {
            var pointOfInterest = await _dbContext.PointOfInterests.FirstOrDefaultAsync(p => p.PointOfInterestId  == pointOfInterestId) ?? new PointOfInterest();

            if (pointOfInterest.PointOfInterestId == Guid.Empty)
                return new PointOfInterestDto();

            _dbContext.PointOfInterests.Remove(pointOfInterest);
            await _dbContext.SaveChangesAsync();

            return PointOfInterestDto.MapToModel(pointOfInterest);
        }

        public async Task<PointOfInterestDto> GetPointOfInterest(Guid pointOfInterestId)
        {
            var pointOfInterest = await _dbContext.PointOfInterests.FirstOrDefaultAsync(p => p.PointOfInterestId == pointOfInterestId) ?? new PointOfInterest();

            if (pointOfInterest.PointOfInterestId == Guid.Empty)
                return new PointOfInterestDto();

            return PointOfInterestDto.MapToModel(pointOfInterest);
        }

        public async Task<List<PointOfInterestDto>> GetPointsOfInterest()
        {
            var pointsOfInterest = await _dbContext.PointOfInterests.ToListAsync();

            return pointsOfInterest.Select(p => PointOfInterestDto.MapToModel(p)).ToList();
        }

        public async Task<PointOfInterestDto> UpdatePointOfInterest(Guid pointOfInterestId, PointOfInterestDto pointOfInterestDto)
        {
            var cityExists = await _dbContext.Cities.Where(c => c.CityId == pointOfInterestDto.CityId).AnyAsync();
            var pointOfInterest = await _dbContext.PointOfInterests.FirstOrDefaultAsync(p => p.PointOfInterestId == pointOfInterestId) ?? new PointOfInterest();

            if (pointOfInterest.PointOfInterestId == Guid.Empty || cityExists == false)
                return new PointOfInterestDto();

            PointOfInterestDto.MapToEntity(pointOfInterest, pointOfInterestDto);
            await _dbContext.SaveChangesAsync();

            return PointOfInterestDto.MapToModel(pointOfInterest);
        }
    }
}
