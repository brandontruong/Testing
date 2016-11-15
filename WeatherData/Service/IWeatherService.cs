using System;

namespace WeatherData
{
    public interface IWeatherService : IDisposable
    {
        Weather getWeatherDataByPosition(Position position);
        string getLocation(Position position);
        double getElevation(Position position);
        Condition getCondition(Position position);
        string getTemperature(Position position);
        double getPressure(Position position);
        double getHumidity(Position position);
        DateTimeOffset getCurrentLocalDateTimeByLocation(string location);
    }
}