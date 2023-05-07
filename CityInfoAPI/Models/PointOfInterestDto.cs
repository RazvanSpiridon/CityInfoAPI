using CityInfoAPI.Entities;

namespace CityInfoAPI.Models
{
    public class PointOfInterestDto
    {
        public PointOfInterestDto()
        {
        }

        public Guid PointOfInterestId { get; set; }
        public string PointOfInterestName { get; set; } = string.Empty;
        public string PointOfInterestDescription { get; set; } = string.Empty;
        public Guid CityId { get; set; }

        public static PointOfInterestDto MapToModel(PointOfInterest pointOfInterest)
        {
            return new PointOfInterestDto
            {
                PointOfInterestId = pointOfInterest.PointOfInterestId,
                PointOfInterestName = pointOfInterest.PointOfInterestName,
                PointOfInterestDescription = pointOfInterest.PointOfInterestDescription,
                CityId = pointOfInterest.CityId,
            };
        }

        public static void MapToEntity(PointOfInterest pointOfInterest, PointOfInterestDto pointOfInterestDto)
        {
            pointOfInterest.PointOfInterestName = pointOfInterestDto.PointOfInterestName;
            pointOfInterest.PointOfInterestDescription = pointOfInterestDto.PointOfInterestDescription;
            pointOfInterest.CityId = pointOfInterestDto.CityId;
        }
    }
}
