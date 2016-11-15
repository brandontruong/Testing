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
        // Having these list of positions here just for testing purpose 
        public static Position Sydney = new Position { Latitude = -33.86, Longitude = 151.21 };
        public static Position Melbourne = new Position { Latitude = -37.83, Longitude = 144.98 };
        public static Position Adelaide = new Position { Latitude = -34.92, Longitude = 138.62 };
        public static Position Hawaii = new Position { Latitude = -19.89, Longitude = 155.58 };
        public static Position Alaska = new Position { Latitude = 64.20, Longitude = 149.49 };
        public static Position Hanoi = new Position { Latitude = 21.02, Longitude = 105.83 };
        public static Position Bogota = new Position { Latitude = 4.71, Longitude = 74.07 };
        public static Position Mumbai = new Position { Latitude = 19.07, Longitude = 72.87 };
        public static Position NewDelhi = new Position { Latitude = 28.61, Longitude = 77.20 };
        public static Position Tokyo = new Position { Latitude = 35.68, Longitude = 139.69 };

        internal static string FormatIso8601(this DateTimeOffset dto)
        {
            return dto.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
        }
    }
}
