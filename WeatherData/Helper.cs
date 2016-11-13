using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData
{
    public static class Helper
    {
        // Having these 3 objects just for testing purpose 
        public static Position Sydney = new Position { Latitude = -33.86, Longitude = 151.21 };
        internal static Position Melbourne = new Position { Latitude = -37.83, Longitude = 144.98 };
        internal static Position Adelaide = new Position { Latitude = -34.92, Longitude = 138.62 };

        internal static DateTimeOffset GetLocalDateTime(double latitude, double longitude, DateTimeOffset utcDate)
        {
            // Check the latitude and longitude to return the local date time
            // lets assume for Sydney local time is the UTC time, Melbourne will run 1 hour behind and Adelaide runs 2 hours behind
            if (latitude == Sydney.Latitude && longitude == Sydney.Longitude)
            {
                return utcDate;
            }
            if (latitude == Melbourne.Latitude && longitude == Melbourne.Longitude)
            {
                return utcDate.AddHours(-1);
            }
            if (latitude == Adelaide.Latitude && longitude == Adelaide.Longitude)
            {
                return utcDate.AddHours(-2);
            }
            return utcDate;
        }
        
    }
}
