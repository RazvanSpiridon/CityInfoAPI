using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfoAPI.Entities
{
    public class City
    {
        public City()
        {
        }

        public City(Guid cityId, string cityName, string cityDescription)
        {
            CityId = cityId;
            CityName = cityName;
            CityDescription = cityDescription;
        }

        public Guid CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
        public string CityDescription { get; set; } = string.Empty;


        public List<PointOfInterest> PointsOfInterest { get; set; } = new List<PointOfInterest>();
    }
}
