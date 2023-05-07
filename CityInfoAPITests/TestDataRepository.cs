using CityInfoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfoAPITests
{
    public class TestDataRepository
    {
        public static CityDto TestCityDto()
        {
            return new CityDto
            {
                CityId = Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                CityName = "Pitesti",
                CityDescription = "Jud. Arges"
            };
        }

        public static List<CityDto> TestCitiesDto()
        {
            return new List<CityDto>
            {
                new CityDto
                {
                    CityId = Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                    CityName = "Pitesti",
                    CityDescription = "Jud. Arges"
                },
                new CityDto
                {
                    CityId = Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"),
                    CityName = "Targoviste",
                    CityDescription = "Jud. Dambovita"
                },
                new CityDto
                {
                    CityId = Guid.Parse("844e14ce-c055-49e9-9610-855669c9859b"),
                    CityName = "Rm. Valcea",
                    CityDescription = "Jud. Valcea"
                },
                new CityDto
                {
                    CityId = Guid.Parse("d6e0e4b7-9365-4332-9b29-bb7bf09664a6"),
                    CityName = "Tg-Jiu",
                    CityDescription = "Gorj"
                },
                new CityDto
                {
                    CityId = Guid.Parse("72f2f5fe-e50c-4966-8420-d50258aefdcb"),
                    CityName = "Craiova",
                    CityDescription = "Olt"
                }
            };
        }

        public static PointOfInterestDto TestPointOfInterest()
        {
            return new PointOfInterestDto
            {
                PointOfInterestId = Guid.Parse("f484ad8f-78fd-46d1-9f87-bbb1e676e37f"),
                PointOfInterestName = "Point for Pitesti",
                PointOfInterestDescription = "first point of interest in Pitesti",
                CityId = Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01")
            };
        }

        public static List<PointOfInterestDto> TestPointsOfInterest()
        {
            return new List<PointOfInterestDto>
            {
                new PointOfInterestDto
                {
                    PointOfInterestId = Guid.Parse("f484ad8f-78fd-46d1-9f87-bbb1e676e37f"),
                    PointOfInterestName = "Point for Pitesti",
                    PointOfInterestDescription = "first point of interest in Pitesti",
                    CityId = Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01")
                },
                new PointOfInterestDto
                {
                    PointOfInterestId = Guid.Parse("f484ad8f-78fd-46d1-9f87-bbb1e676e373"),
                    PointOfInterestName = "Point for Pitesti",
                    PointOfInterestDescription = "second point of interest in Pitesti",
                    CityId = Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01")
                },
                new PointOfInterestDto
                {
                    PointOfInterestId = Guid.Parse("f484ad8f-78fd-46d1-9f87-bbb1e676e371"),
                    PointOfInterestName = "Point for Tg-Jiu",
                    PointOfInterestDescription = "first point of interest in Tg-Jiu",
                    CityId = Guid.Parse("d6e0e4b7-9365-4332-9b29-bb7bf09664a6")
                },
                new PointOfInterestDto
                {
                    PointOfInterestId = Guid.Parse("f484ad8f-78fd-46d1-9f87-bbb1e676e379"),
                    PointOfInterestName = "Point for Targoviste",
                    PointOfInterestDescription = "first point of interest in Targoviste",
                    CityId = Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e")
                },
                new PointOfInterestDto
                {
                    PointOfInterestId = Guid.Parse("f484ad8f-78fd-46d1-9f87-bbb1e676e391"),
                    PointOfInterestName = "Point for Craiova",
                    PointOfInterestDescription = "first point of interest in Craiova",
                    CityId = Guid.Parse("72f2f5fe-e50c-4966-8420-d50258aefdcb")
                },
                new PointOfInterestDto
                {
                    PointOfInterestId = Guid.Parse("f484ad8f-78fd-46d1-9f87-bbb1e676e341"),
                    PointOfInterestName = "Point for Craiova",
                    PointOfInterestDescription = "second point of interest in Craiova",
                    CityId = Guid.Parse("72f2f5fe-e50c-4966-8420-d50258aefdcb")
                }
            };
        }

    }
}
