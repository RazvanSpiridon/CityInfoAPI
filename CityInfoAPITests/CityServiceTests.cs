using CityInfoAPI.Context;
using CityInfoAPI.Entities;
using CityInfoAPI.Models;
using CityInfoAPI.Services;
using Microsoft.EntityFrameworkCore;


namespace CityInfoAPITests
{
    public class CityServiceTests
    {
        private readonly DbContextOptions<CityInfoDbContext> _dbContextOptions;
        private readonly CityInfoDbContext _dbContext;
        private readonly CityService _cityService;
        public CityServiceTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<CityInfoDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _dbContext = new CityInfoDbContext(_dbContextOptions);
            _cityService = new CityService(_dbContext);
        }

        [Fact]
        public async Task CityService_AddCity_MustReturnANotEmptyCity()
        {
            //Arrange

            //Act
            var addCity = await _cityService.AddCity(TestDataRepository.TestCityDto());

            //Assert
            Assert.NotNull(addCity);

            //more tests
            Assert.Equal(addCity.CityName, TestDataRepository.TestCityDto().CityName);
            Assert.Equal(addCity.CityDescription, TestDataRepository.TestCityDto().CityDescription);

            var findCity = await _dbContext.Cities.FirstOrDefaultAsync(c => c.CityId == addCity.CityId) ?? new City();
            Assert.NotEqual(findCity.CityId.ToString(), Guid.Empty.ToString());
            Assert.Equal(findCity.CityName, TestDataRepository.TestCityDto().CityName);
            Assert.Equal(findCity.CityDescription, TestDataRepository.TestCityDto().CityDescription);
        }

        [Fact]
        public async Task CityService_DeleteCity_MustReturnNull()
        {
            //Arrange
            await _dbContext.Cities.AddAsync(new City(
                Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                TestDataRepository.TestCityDto().CityName,
                TestDataRepository.TestCityDto().CityDescription));
            await _dbContext.SaveChangesAsync();

            //Act
            var deleteCity = await _cityService.DeleteCity(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));

            //Assert
            Assert.Equal(TestDataRepository.TestCityDto().CityId, deleteCity.CityId);
            Assert.Equal(TestDataRepository.TestCityDto().CityName, deleteCity.CityName);
            Assert.Equal(TestDataRepository.TestCityDto().CityDescription, deleteCity.CityDescription);

            var cityExist = await _dbContext.Cities.FindAsync(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));
            Assert.Null(cityExist);
        }

        [Fact]
        public async Task CityService_GetCities_MustReturnListOfCities()
        {
            //Arrange
            await _dbContext.Cities.AddRangeAsync(TestDataRepository.TestCitiesDto()
                .Select(c => new City(c.CityId, c.CityName, c.CityDescription)));
            await _dbContext.SaveChangesAsync();

            //Act
            var listOfCities = await _cityService.GetCities();

            //Assert
            Assert.Equal(listOfCities.Count, TestDataRepository.TestCitiesDto().Count);

            for (int i = 0; i < listOfCities.Count; i++)
            {
                Assert.Equal(TestDataRepository.TestCitiesDto()[i].CityId, listOfCities[i].CityId);
                Assert.Equal(TestDataRepository.TestCitiesDto()[i].CityName, listOfCities[i].CityName);
                Assert.Equal(TestDataRepository.TestCitiesDto()[i].CityDescription, listOfCities[i].CityDescription);
            }
        }

        [Fact]
        public async Task CityService_GetCity_MustReturnCityById()
        {
            //Arrange
            await _dbContext.Cities.AddAsync(new City(
                Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                TestDataRepository.TestCityDto().CityName,
                TestDataRepository.TestCityDto().CityDescription));
            await _dbContext.SaveChangesAsync();

            //Act
            var city = await _cityService.GetCity(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));

            //Assert
            Assert.Equal(TestDataRepository.TestCityDto().CityId, city.CityId);
            Assert.Equal(TestDataRepository.TestCityDto().CityName, city.CityName);
            Assert.Equal(TestDataRepository.TestCityDto().CityDescription, city.CityDescription);

            var findCity = await _dbContext.Cities.FirstOrDefaultAsync(c => c.CityId == city.CityId) ?? new City();

            Assert.Equal(city.CityId.ToString(), findCity.CityId.ToString());
            Assert.Equal(city.CityName, findCity.CityName);
            Assert.Equal(city.CityDescription, findCity.CityDescription);
        }

        [Fact]
        public async Task CityService_UpdateCity_MustReturnModifyCity()
        {
            //Arrange
            await _dbContext.Cities.AddAsync(
                new City
                {
                    CityId = TestDataRepository.TestCityDto().CityId,
                    CityName = "Random city",
                    CityDescription = "Random description"
                });
            await _dbContext.SaveChangesAsync();

            //Act
            var modifyCity = await _cityService.UpdateCity(TestDataRepository.TestCityDto().CityId, TestDataRepository.TestCityDto());

            //Assert
            Assert.NotNull(modifyCity);
            Assert.Equal(TestDataRepository.TestCityDto().CityId, modifyCity.CityId);
            Assert.Equal(TestDataRepository.TestCityDto().CityName, modifyCity.CityName);
            Assert.Equal(TestDataRepository.TestCityDto().CityDescription, modifyCity.CityDescription);
            Assert.Equal("Pitesti", modifyCity.CityName);
            Assert.NotEqual("Random City", modifyCity.CityName);


            var findModifyCity = await _dbContext.Cities.FirstOrDefaultAsync(
                    c => c.CityId == TestDataRepository.TestCityDto().CityId);

            Assert.NotNull(findModifyCity);
            Assert.Equal(TestDataRepository.TestCityDto().CityName, findModifyCity.CityName);
            Assert.Equal(TestDataRepository.TestCityDto().CityDescription, findModifyCity.CityDescription);
            Assert.NotEqual("Random City", findModifyCity.CityName);
        }

        [Fact]
        public async Task CityService_GetCityWithPointOfInterest_MustReturnCityByIdWithPoints()
        {
            //Arrange
            await _dbContext.Cities.AddAsync(new City(
                Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                TestDataRepository.TestCityDto().CityName,
                TestDataRepository.TestCityDto().CityDescription));
            await _dbContext.PointOfInterests.AddRangeAsync(new List<PointOfInterest> {
                new PointOfInterest
                {
                    PointOfInterestId = Guid.Parse("f484ad8f-78fd-46d1-9f87-bbb1e676e37f"),
                    PointOfInterestName = "Point for Pitesti",
                    PointOfInterestDescription = "first point of interest in Pitesti",
                    CityId = Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01")
                },
                new PointOfInterest
                {
                    PointOfInterestId = Guid.Parse("f484ad8f-78fd-46d1-9f87-bbb1e676e373"),
                    PointOfInterestName = "Point for Pitesti",
                    PointOfInterestDescription = "second point of interest in Pitesti",
                    CityId = Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01")
                },
            });
            await _dbContext.SaveChangesAsync();

            //Act
            var cityWithPoints = await _cityService.GetCityWithPointsOfInterest(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01")) ?? new CityWithPointsOfInterestDto();

            //Assert
            Assert.Equal("37e03ca7-c730-4351-834c-b66f280cdb01", cityWithPoints.CityId.ToString());
            Assert.Equal(2, cityWithPoints.PointsOfInterest.Count);
        }

        [Fact]
        public async Task CityService_GetCityWithPointOfInterest_MustReturnListOfCitiesWithPoints()
        {
            //Arrange
            await _dbContext.Cities.AddRangeAsync(TestDataRepository.TestCitiesDto()
                .Select(c => new City(c.CityId, c.CityName, c.CityDescription)));

            var pointList = new List<PointOfInterest>(TestDataRepository
                .TestPointsOfInterest().Select(p => new PointOfInterest
                {
                    PointOfInterestId = p.PointOfInterestId,
                    PointOfInterestName = p.PointOfInterestName,
                    PointOfInterestDescription = p.PointOfInterestDescription
                }).ToList());
            await _dbContext.PointOfInterests.AddRangeAsync(pointList);

            await _dbContext.SaveChangesAsync();

            //Act
            var listOfCity = await _cityService.GetCitiesWithPointsOfInterest();

            //Assert
            Assert.Equal(5, listOfCity.Count);
        }
    }
}
