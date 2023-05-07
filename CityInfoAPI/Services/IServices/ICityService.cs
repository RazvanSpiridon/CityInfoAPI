using CityInfoAPI.Models;

namespace CityInfoAPI.Services.IServices
{
    public interface ICityService
    {
        Task<List<CityDto>> GetCities();
        Task<CityDto> GetCity(Guid cityId);
        Task<CityDto> AddCity(CityDto cityDto);
        Task<CityDto> UpdateCity(Guid cityId, CityDto cityDto);
        Task<CityDto> DeleteCity(Guid cityId);
        Task<CityWithPointsOfInterestDto> GetCityWithPointsOfInterest(Guid cityId);
        Task<List<CityWithPointsOfInterestDto>> GetCitiesWithPointsOfInterest();
    }
}
