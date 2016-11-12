using System;

namespace WeatherData
{
    public class Position
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Elevation { get; set; }

        public override string ToString()
        {
            return String.Format("{0},{1},{2}", Latitude, Longitude, Elevation);
        }
    }
}