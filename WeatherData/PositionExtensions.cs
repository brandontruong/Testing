using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData
{
    public static class PositionExtensions
    {
        public static void GetElevation(this Position position)
        {
            // lets assume the elevation doenst change over the time based on Latitude and Longitude
            if (position.Latitude == Helper.Sydney.Latitude && position.Longitude == Helper.Sydney.Longitude)
            {
                position.Elevation = "39";
            }
            if (position.Latitude == Helper.Melbourne.Latitude && position.Longitude == Helper.Melbourne.Longitude)
            {
                position.Elevation = "7";
            }
            if (position.Latitude == Helper.Adelaide.Latitude && position.Longitude == Helper.Adelaide.Longitude)
            {
                position.Elevation = "48";
            }
        }


        public static Condition GetCondition(this Position position)
        {
            //for now we just return random condition 
            Array values = Enum.GetValues(typeof(Condition));
            Random random = new Random();
            return (Condition)values.GetValue(random.Next(values.Length));
        }

        public static string GetTemperature(this Position position)
        {
            //Hottest temperature recorded was 56.7 °C and coldest was −89.2 °C
            //for now we just return random number between 56.7 and -89.2
            Random random = new Random();
            var maxNumber = 56.7;
            var minNumber = -89.2;
            var randomTemperature = Math.Round(random.NextDouble() * (maxNumber - minNumber) + minNumber, 1);
            return randomTemperature >= 0? string.Format("+{0}", randomTemperature): randomTemperature.ToString();
        }

        public static double GetPressure(this Position position)
        {
            //for now we just return random pressure value 
            Random random = new Random();
            var maxNumber = 1500;
            var minNumber = 0;
            return Math.Round(random.NextDouble() * (maxNumber - minNumber) + minNumber, 1); ;
        }

        public static double GetHumidity(this Position position)
        {
            //for now we just return random number 
            Random random = new Random();
            return (double)random.Next(0, 100);
        }

        public static string GetLocation(this Position position)
        {
            // Check the latitude and longitude to return the local date time
            // lets assume for Sydney local time is the UTC time, Melbourne will run 1 hour behind and Adelaide runs 2 hours behind
            if (position.Latitude == Helper.Sydney.Latitude && position.Longitude == Helper.Sydney.Longitude)
            {
                return "Sydney";
            }
            if (position.Latitude == Helper.Melbourne.Latitude && position.Longitude == Helper.Melbourne.Longitude)
            {
                return "Melbourne";
            }
            if (position.Latitude == Helper.Adelaide.Latitude && position.Longitude == Helper.Adelaide.Longitude)
            {
                return "Adelaide";
            }
            return "Sydney";
        }
    }
}
