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
                new Position() { Latitude = Helper.Alaska.Latitude, Longitude = Helper.Alaska.Longitude},
                new Position() { Latitude = Helper.Bogota.Latitude, Longitude = Helper.Bogota.Longitude},
                new Position() { Latitude = Helper.Hanoi.Latitude, Longitude = Helper.Hanoi.Longitude},
                new Position() { Latitude = Helper.Hawaii.Latitude, Longitude = Helper.Hawaii.Longitude},
                new Position() { Latitude = Helper.Mumbai.Latitude, Longitude = Helper.Mumbai.Longitude},
                new Position() { Latitude = Helper.NewDelhi.Latitude, Longitude = Helper.NewDelhi.Longitude},
                new Position() { Latitude = Helper.Tokyo.Latitude, Longitude = Helper.Tokyo.Longitude}
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
