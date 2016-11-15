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

        private Condition condition;
        public Condition Condition {
            get
            {
                if (temperature.StartsWith("-"))
                {
                    condition = Condition.Snow;
                }
                return condition;
            }
            set
            {
                condition = value;
            }
        }

        
        private string temperature;
        public string Temperature {
            get
            {
                return temperature;
            }
            set
            {
                temperature = value;
                if (value.StartsWith("-"))
                {
                    Condition = Condition.Snow;
                }
            }
        }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
        public override string ToString()
        {
            return String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", Location, Position, LocalTime.FormatIso8601(), Condition, Temperature, Pressure, Humidity);
       }
    }
}
