namespace CityInfoAPI.Models
{
    public class CityWithPointsOfInterestDto
    {
        public CityWithPointsOfInterestDto()
        {
        }

        public CityWithPointsOfInterestDto(Guid cityId, string cityName, string cityDescription, List<PointOfInterestDto> pointsOfInterest)
        {
            CityId = cityId;
            CityName = cityName;
            CityDescription = cityDescription;
            PointsOfInterest = pointsOfInterest;
        }

        public Guid CityId { get; set; }
        public string CityName { get; set; } = null!;
        public string CityDescription { get; set; } = null!;
        public List<PointOfInterestDto> PointsOfInterest { get; set; } = new List<PointOfInterestDto>();

        
    }
}
