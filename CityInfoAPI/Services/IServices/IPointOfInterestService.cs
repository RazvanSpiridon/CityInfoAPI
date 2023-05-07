using CityInfoAPI.Models;

namespace CityInfoAPI.Services.IServices
{
    public interface IPointOfInterestService
    {
        Task<List<PointOfInterestDto>> GetPointsOfInterest();
        Task<PointOfInterestDto> GetPointOfInterest(Guid pointOfInterestId);
        Task<PointOfInterestDto> AddPointOfInterest(PointOfInterestDto pointOfInterestDto);
        Task<PointOfInterestDto> UpdatePointOfInterest(Guid pointOfInterestId, PointOfInterestDto pointOfInterestDto);
        Task<PointOfInterestDto> DeletePointOfInterest(Guid pointOfInterestId);
    }
}
