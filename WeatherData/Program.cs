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
            simulation(new WeatherService());
        }

        private static void simulation(IWeatherService service)
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
                    weatherData = service.getWeatherDataByPosition(position);
                    Console.WriteLine(weatherData);
                }
                exitCommand = Console.ReadLine();
            }
        }
    }
}
