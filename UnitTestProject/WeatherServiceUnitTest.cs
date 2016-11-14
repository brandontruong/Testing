using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherData;

namespace UnitTestProject
{
    [TestClass]
    public class WeatherServiceUnitTest
    {
        IWeatherService service = new WeatherService();

        [TestMethod]
        public void testGetWeatherDataByPositionMethod()
        {
            // arrange  
            var sydneyPosition = new Position() {
                Longitude = Helper.Sydney.Longitude,
                Latitude = Helper.Sydney.Latitude,
            };
            
            // act  
            var weatherData = service.getWeatherDataByPosition(sydneyPosition, DateTimeOffset.Now);

            // assert  
            string expectedLocation = "Sydney";
            Assert.AreEqual(expectedLocation, weatherData.Location, "Location should be Sydney");
        }
    }
}
