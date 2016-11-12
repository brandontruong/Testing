using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData
{
    class Weather
    {
        public Location Location { get; set; }
        public Position Position { get; set; }
        public TimeSpan LocalTime { get; set; }
        public Condition Condition { get; set; }
        public string Temperature { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
        public override string ToString()
        {
            return String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", Location, Position, LocalTime, Condition, Temperature, Pressure, Humidity);
       }
    }

    public enum Condition { Rain, Snow, Sunny };
    public enum Location { Sydney, Melbourne, Adelaide };

}
