using CityInfoAPI.Entities;

namespace CityInfoAPI.Models
{
    public class CityDto
    {
        public CityDto()
        {
        }

        public CityDto(Guid cityId, string cityName, string cityDescription)
        {
            CityId = cityId;
            CityName = cityName;
            CityDescription = cityDescription;
        }

        public Guid CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
        public string CityDescription { get; set; } = string.Empty;

    }
}
