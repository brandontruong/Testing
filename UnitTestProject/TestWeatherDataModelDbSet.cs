using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherData;

namespace UnitTestProject
{
    public class TestWeatherDataModelDbSet : TestDbSet<LocationData>
    {
        public override LocationData Find(params object[] keyValues)
        {
            return this.SingleOrDefault(location => location.Id == (int)keyValues.Single());
        }
    }
}
