using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData
{
    public class Weather
    {
        public string Location { get; set; }
        public Position Position { get; set; }
        public DateTimeOffset LocalTime { get; set; }
        public Condition Condition { get; set; }
        public string Temperature { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
        public override string ToString()
        {
            return String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", Location, Position, LocalTime.FormatIso8601(), Condition, Temperature, Pressure, Humidity);
       }
    }
}
