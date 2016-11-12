using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData
{
    class Program
    {

        // Having these 3 objects just for testing purpose 
        private static Position sydney = new Position { Latitude = -33.86, Longitude = 51.21};
        private static Position melbourne = new Position { Latitude = -37.83, Longitude = 144.98};
        private static Position adelaide = new Position { Latitude = -34.92, Longitude =  138.62 };

        static void Main(string[] args)
        {
            simulation();
        }

        private static void simulation()
        {
            var positions = new List<Position>
            {
                new Position() { Latitude = sydney.Latitude, Longitude = sydney.Longitude},
                new Position() { Latitude = melbourne.Latitude, Longitude = melbourne.Longitude},
                new Position() { Latitude = adelaide.Latitude, Longitude = adelaide.Longitude},
            };

            Weather weatherData;
            foreach (var position in positions)
            {
                weatherData = getWeatherData(position, DateTime.UtcNow);
                Console.WriteLine(weatherData);
                Console.ReadLine();
            }
            
        }

        private static Weather getWeatherData(Position position, DateTime now)
        {
            DateTime localTime = GetLocalDateTime(position.Latitude, position.Longitude, now);

            return null;
        }

        public static DateTime GetLocalDateTime(double latitude, double longitude, DateTime utcDate)
        {
            // Chekc the latitude and longitude to return the local date time
            if (latitude == -33.86 && longitude == 51.21)
            {
                return utcDate;
            }
            if (latitude == -37.83 && longitude == 144.98)
            {
                return utcDate;
            }
            return utcDate;
        }
    }
}
