using System.Text.Json.Serialization;

namespace CityInfoAPI.Entities
{
    public class PointOfInterest
    {
        public PointOfInterest()
        {
        }

        public Guid PointOfInterestId { get; set; }
        public string PointOfInterestName { get; set; } = string.Empty;
        public string PointOfInterestDescription { get; set; } = string.Empty;

        public Guid CityId { get; set; }
        public City? City { get; set; }

    }
}
