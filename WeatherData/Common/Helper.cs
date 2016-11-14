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
        public static Position Melbourne = new Position { Latitude = -37.83, Longitude = 144.98 };
        public static Position Adelaide = new Position { Latitude = -34.92, Longitude = 138.62 };
        
        internal static string FormatIso8601(this DateTimeOffset dto)
        {
            string format = dto.Offset == TimeSpan.Zero
                ? "yyyy-MM-ddTHH:mm:ss.fffZ"
                : "yyyy-MM-ddTHH:mm:ss.fffzzz";

            return dto.ToString(format, CultureInfo.InvariantCulture);
        }
    }
}
