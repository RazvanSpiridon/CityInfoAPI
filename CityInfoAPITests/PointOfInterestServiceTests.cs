using CityInfoAPI.Context;
using CityInfoAPI.Entities;
using CityInfoAPI.Models;
using CityInfoAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfoAPITests
{
    public class PointOfInterestServiceTests
    {
        private readonly DbContextOptions<CityInfoDbContext> _dbContextOptions;
        private readonly CityInfoDbContext _dbContext;
        private readonly PointOfInterestService _pointOfInterestService;
        public PointOfInterestServiceTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<CityInfoDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _dbContext = new CityInfoDbContext(_dbContextOptions);
            _pointOfInterestService = new PointOfInterestService(_dbContext);
        }

        [Fact]
        public async Task PointOfInterestService_AddPointOfInterest_MustReturnFalseIfPointDoNotExist()
        {
            //Arrange

            //Act
            await _dbContext.Cities.AddAsync(new City
            {
                CityId = Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                CityName = "Test",
                CityDescription = "TestDescription",
            });
            await _dbContext.SaveChangesAsync();
            var addPoint = await _pointOfInterestService.AddPointOfInterest(TestDataRepository.TestPointOfInterest());
           
            //Assert
            Assert.NotNull(addPoint);

            Assert.True(addPoint.PointOfInterestId != TestDataRepository.TestPointOfInterest().PointOfInterestId);
            Assert.Equal(addPoint.PointOfInterestName, TestDataRepository.TestPointOfInterest().PointOfInterestName);

            var point = await _dbContext.PointOfInterests.FirstOrDefaultAsync(p => p.PointOfInterestId == addPoint.PointOfInterestId) ?? new PointOfInterest();
            Assert.Equal(point.PointOfInterestName, TestDataRepository.TestPointOfInterest().PointOfInterestName);
        }

        [Fact]
        public async Task PointOfInterestService_DeletepointOfInterest_MustReturnNull()
        {
            //Arrange
            var addpoint = new PointOfInterest();
            PointOfInterestDto.MapToEntity(addpoint, TestDataRepository.TestPointOfInterest());
            await _dbContext.PointOfInterests.AddAsync(addpoint);
            await _dbContext.SaveChangesAsync();

            //Act
            var deletePoint = await _pointOfInterestService.DeletePointOfInterest(addpoint.PointOfInterestId);

            //Assert
            var findPoint = await _dbContext.PointOfInterests.AnyAsync(p => p.PointOfInterestId == addpoint.PointOfInterestId);
            Assert.False(findPoint);

            Assert.Equal(deletePoint.PointOfInterestName, addpoint.PointOfInterestName);
        }

        [Fact]
        public async Task PointOfInterestService_GetPointOfInterest_MustReturnPointById()
        {
            //Arrange
            var addpoint = new PointOfInterest();
            PointOfInterestDto.MapToEntity(addpoint, TestDataRepository.TestPointOfInterest());
            await _dbContext.PointOfInterests.AddAsync(addpoint);
            await _dbContext.SaveChangesAsync();

            //Act
            var pointById = await _pointOfInterestService.GetPointOfInterest(addpoint.PointOfInterestId);

            //Assert
            Assert.Equal(pointById.PointOfInterestId, addpoint.PointOfInterestId);
            Assert.Equal(pointById.PointOfInterestName, addpoint.PointOfInterestName);

            var findPoint = await _dbContext.PointOfInterests.FirstOrDefaultAsync(p => p.PointOfInterestId == addpoint.PointOfInterestId) ?? new PointOfInterest();
            Assert.Equal(findPoint.PointOfInterestId, pointById.PointOfInterestId);
            Assert.Equal(findPoint.PointOfInterestName, pointById.PointOfInterestName);

        }

        [Fact]
        public async Task PointOfInterestService_GetPointsOfInterest_MustReturnListOfPoints()
        {
            //Arrange
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
            var listOfPoints = await _pointOfInterestService.GetPointsOfInterest();

            //Arrange
            Assert.Equal(listOfPoints.Count, TestDataRepository.TestPointsOfInterest().Count);

            for (int i = 0; i < listOfPoints.Count; i++)
            {
                Assert.Equal(TestDataRepository.TestPointsOfInterest()[i].PointOfInterestId, listOfPoints[i].PointOfInterestId);
                Assert.Equal(TestDataRepository.TestPointsOfInterest()[i].PointOfInterestName, listOfPoints[i].PointOfInterestName);
                Assert.Equal(TestDataRepository.TestPointsOfInterest()[i].PointOfInterestDescription, listOfPoints[i].PointOfInterestDescription);
            }
        }

        [Fact]
        public async Task PointOfInterestService_UpdatePointOfInterest_MustModifyTheAddedPoint()
        {
            //Arrange
            await _dbContext.Cities.AddAsync(new City
            {
                CityId = Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                CityName = "Test",
                CityDescription = "TestDescription",
            });

            var addpoint = new PointOfInterest
            {
                PointOfInterestId = Guid.Parse("f484ad8f-78fd-46d1-9f87-bbb1e676e37f"),
                PointOfInterestName = "Test Point",
                PointOfInterestDescription = "first point of interest for test",
                CityId = Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01")
            };
            PointOfInterestDto.MapToEntity(addpoint, TestDataRepository.TestPointOfInterest());
            await _dbContext.PointOfInterests.AddAsync(addpoint);
            await _dbContext.SaveChangesAsync();

            //Act
            var modifyPoint = await _pointOfInterestService.UpdatePointOfInterest(
                Guid.Parse("f484ad8f-78fd-46d1-9f87-bbb1e676e37f"),
                TestDataRepository.TestPointOfInterest());

            //Assert
            Assert.NotNull(modifyPoint);
            Assert.Equal(modifyPoint.PointOfInterestId, TestDataRepository.TestPointOfInterest().PointOfInterestId);
            Assert.Equal(modifyPoint.PointOfInterestName, TestDataRepository.TestPointOfInterest().PointOfInterestName);
            Assert.Equal(modifyPoint.PointOfInterestDescription, TestDataRepository.TestPointOfInterest().PointOfInterestDescription);

            var findModifyPoint = await _dbContext.PointOfInterests.FirstOrDefaultAsync(
                p => p.PointOfInterestId == modifyPoint.PointOfInterestId) ?? new PointOfInterest();

            Assert.NotEqual(findModifyPoint.PointOfInterestId, Guid.Empty);
            Assert.Equal(findModifyPoint.PointOfInterestName, TestDataRepository.TestPointOfInterest().PointOfInterestName);
        }
    }
}
