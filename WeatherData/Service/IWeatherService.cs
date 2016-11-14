using System;

namespace WeatherData
{
    public interface IWeatherService : IDisposable
    {
        Weather getWeatherDataByPosition(Position position, DateTimeOffset time);
        string getLocation(Position position);
        string getElevation(Position position);
        Condition getCondition(Position position);
        string getTemperature(Position position);
        double getPressure(Position position);
        double getHumidity(Position position);
        DateTimeOffset getLocalDateTime(Position position, DateTimeOffset utcDate);
    }
}