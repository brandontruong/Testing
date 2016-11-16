using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherData;

namespace UnitTestProject
{
    [TestClass]
    public class WeatherServiceUnitTest
    {

        private IWeatherDataModel mock;
        private IWeatherService service;

        [TestInitialize()]
        public void Initialize()
        {
            mock = new WeatherDataModelMock();
            LoadMockData(mock);
            service = new WeatherService(mock);
        }


        [TestMethod]
        public void testGetWeatherDataByPositionMethod()
        {

            // arrange  
            var sydneyPosition = new Position()
            {
                Longitude = Helper.Sydney.Longitude,
                Latitude = Helper.Sydney.Latitude,
            };

            // act  
            var weatherData = service.getWeatherDataByPosition(sydneyPosition);

            // assert  
            string expectedLocation = "Sydney";
            Assert.AreEqual(expectedLocation, weatherData.Location, "Location should be Sydney");
            Assert.AreEqual(39, weatherData.Position.Elevation, "Location should be Sydney");
        }

        [TestMethod]
        public void testGetLocationMethod()
        {
            // arrange  
            var sydneyPosition = new Position()
            {
                Longitude = Helper.Sydney.Longitude,
                Latitude = Helper.Sydney.Latitude,
            };

            // act  
            var actualLocation = service.getLocation(sydneyPosition);

            // assert  
            string expectedLocation = "Sydney";
            Assert.AreEqual(expectedLocation, actualLocation, "Location should be Sydney");
        }

        [TestMethod]
        public void testGetElevationMethod()
        {
            // arrange  
            var sydneyPosition = new Position()
            {
                Longitude = Helper.Sydney.Longitude,
                Latitude = Helper.Sydney.Latitude,
            };

            // act  
            var actualElevation = service.getElevation(sydneyPosition);

            // assert  
            var expectedElevation = 39;
            Assert.AreEqual(expectedElevation, actualElevation, "Elevation should be 39");
        }


        [TestMethod]
        public void testSnowConditionLogic()
        {
            // arrange  
            var sydneyPosition = new Position()
            {
                Longitude = Helper.Sydney.Longitude,
                Latitude = Helper.Sydney.Latitude,
            };
            var weatherData = service.getWeatherDataByPosition(sydneyPosition);

            // act  
            weatherData.Temperature = "-10";

            // assert  
            Condition expectedCondition = Condition.Snow;
            Assert.AreEqual(expectedCondition, weatherData.Condition, "Condition should be Snow when the temperature is less than zero");
        }

        private void LoadMockData(IWeatherDataModel mock)
        {
            mock.LocationDatas.Add(new LocationData()
            {
                LocationName = "Sydney",
                Latitude = -33.86,
                Longitude = 151.21,
                Elevation = 39
            });
        }
    }
}
