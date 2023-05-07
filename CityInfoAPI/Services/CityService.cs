using CityInfoAPI.Context;
using CityInfoAPI.Entities;
using CityInfoAPI.Models;
using CityInfoAPI.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CityInfoAPI.Services
{
    public class CityService : ICityService
    {
        private readonly CityInfoDbContext _dbContext;

        public CityService(CityInfoDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<CityDto> AddCity(CityDto cityDto)
        {
            var city = new City
            {
                CityId = new Guid(),
                CityName = cityDto.CityName,
                CityDescription = cityDto.CityDescription
            };

            await _dbContext.Cities.AddAsync(city);
            await _dbContext.SaveChangesAsync();

            return new CityDto(city.CityId, city.CityName, city.CityDescription);
        }

        public async Task<CityDto> DeleteCity(Guid cityId)
        {
            var city = await _dbContext.Cities.Where(c => c.CityId == cityId).FirstOrDefaultAsync() ?? new City(); 

            if (city.CityId == Guid.Empty)
                return new CityDto();

            _dbContext.Cities.Remove(city);
            await _dbContext.SaveChangesAsync();
            return new CityDto(city.CityId, city.CityName, city.CityDescription);
        }

        public async Task<List<CityDto>> GetCities()
        {
            var cities = await _dbContext.Cities.ToListAsync();

            return cities.Select(c => new CityDto(
                c.CityId,
                c.CityName,
                c.CityDescription
                )).ToList();
        }

        public async Task<CityDto> GetCity(Guid cityId)
        {
            var city = await _dbContext.Cities.Where(c => c.CityId == cityId).FirstOrDefaultAsync() ?? new City();

            return new CityDto(city.CityId, city.CityName, city.CityDescription);
        }

        public async Task<CityDto> UpdateCity(Guid cityId, CityDto cityDto)
        {
            var city = await _dbContext.Cities.Where(c => c.CityId == cityId).FirstOrDefaultAsync() ?? new City();

            if (city.CityId == Guid.Empty)
                return new CityDto();

            city.CityName = cityDto.CityName;
            city.CityDescription = cityDto.CityDescription;

            await _dbContext.SaveChangesAsync();

            return new CityDto(city.CityId, city.CityName, city.CityDescription);
        }

        //city with points
        public  async Task<CityWithPointsOfInterestDto> GetCityWithPointsOfInterest(Guid cityId)
        {
            var city = await _dbContext.Cities.Where(c => c.CityId == cityId)
                .Include(p => p.PointsOfInterest).FirstOrDefaultAsync() ?? new City();

            if (city.CityId == Guid.Empty)
                return new CityWithPointsOfInterestDto();

            return new CityWithPointsOfInterestDto(city.CityId, city.CityName, city.CityDescription,
                city.PointsOfInterest.Select(p => PointOfInterestDto.MapToModel(p)).ToList());
        }
        public async Task<List<CityWithPointsOfInterestDto>> GetCitiesWithPointsOfInterest()
        {
            var cities = await _dbContext.Cities.Include(p => p.PointsOfInterest).ToListAsync();

            return cities.Select(c => new CityWithPointsOfInterestDto(
                c.CityId,
                c.CityName,
                c.CityDescription,
                c.PointsOfInterest.Select(p => PointOfInterestDto.MapToModel(p)).ToList()
                )).ToList();
        }
    }
}
