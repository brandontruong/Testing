using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData
{
    class Program
    {

        static void Main(string[] args)
        {
            simulation();
        }

        private static void simulation()
        {
            var positions = new List<Position>
            {
                new Position() { Latitude = Helper.Sydney.Latitude, Longitude = Helper.Sydney.Longitude},
                new Position() { Latitude = Helper.Melbourne.Latitude, Longitude = Helper.Melbourne.Longitude},
                new Position() { Latitude = Helper.Adelaide.Latitude, Longitude = Helper.Adelaide.Longitude},
            };

            Weather weatherData;
            var exitCommand = "";
            while (exitCommand != "exit")
            {
                foreach (var position in positions)
                {
                    weatherData = getWeatherData(position, DateTimeOffset.UtcNow);
                    Console.WriteLine(weatherData);
                }
                exitCommand = Console.ReadLine();
            }
        }

        private static Weather getWeatherData(Position position, DateTimeOffset now)
        {
            var localTime = Helper.GetLocalDateTime(position.Latitude, position.Longitude, now);
            var location = position.GetLocation();
            position.GetElevation();
            
            return new Weather() {
                Location = location,
                Position = position,
                LocalTime = localTime,
                Condition = position.GetCondition(),
                Temperature = position.GetTemperature(),
                Pressure = position.GetPressure(),
                Humidity = position.GetHumidity()
            };
        }
     
    }
}
