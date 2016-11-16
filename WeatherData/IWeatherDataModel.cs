using System;
using System.Data.Entity;

namespace WeatherData
{
    public interface IWeatherDataModel : IDisposable
    {
        DbSet<LocationData> LocationDatas { get; set; }
    }
}