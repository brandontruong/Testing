using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WeatherData;

namespace UnitTestProject
{
    public class WeatherDataModelMock : IWeatherDataModel
    {
        public WeatherDataModelMock()
        {
            this.LocationDatas = new TestWeatherDataModelDbSet();
        }

        public DbSet<LocationData> LocationDatas { get; set; }

        public void Dispose()
        {
            LocationDatas = null;
        }
    }
}
