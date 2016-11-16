using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData
{
    public class WeatherService : IWeatherService
    {
        private IWeatherDataModel _weatherDataModelContext;
        IList<LocationData> _locationData;
        public WeatherService(IWeatherDataModel weatherDataModelContext)
        {
            _weatherDataModelContext = weatherDataModelContext;

            _locationData = _weatherDataModelContext.LocationDatas.ToList();
        }

        public Weather getWeatherDataByPosition(Position position)
        {

            Weather result = new Weather()
            {
                Position = new Position()
                {
                    Latitude = position.Latitude,
                    Longitude = position.Longitude
                }
            };

            Task[] taskArray = new Task[5];

            // Run the task getting the Location
            taskArray[0] = Task.Factory.StartNew(
               () =>
               {
                   return new
                   {
                       Location = getLocation(position)
                   };
               }).ContinueWith(task =>
               {
                   result.Location = task.Result.Location;

                   // getting Localtime based on location
                   result.LocalTime = getCurrentLocalDateTimeByLocation(result.Location);
               });

            // Run the task getting the Elevation to fulfill the Position
            taskArray[1] = Task.Factory.StartNew(
               () =>
               {
                   return new
                   {
                       Elevation = getElevation(position)
                   };
               }).ContinueWith(task =>
               {
                   result.Position.Elevation = task.Result.Elevation;
               });

            // Run the task getting Condition and Temperature
            taskArray[2] = Task.Factory.StartNew(
                () =>
                {
                    Condition condition;
                    var temperature = getTemperature(position);

                    // Let's assume the condition will be snowy if the temperature is below 0 degree
                    // If there are more logics, it should be checking at the point in time.
                    // Lets assume there is only 1 relationship between the temperature and condition
                    if (temperature.StartsWith("-"))
                    {
                        condition = Condition.Snow;
                    }
                    else
                    {
                        // if the temperature is greater than zero, the condition would be either sunny or rainy
                        condition = getCondition(position);
                        while (condition == Condition.Snow)
                        {
                            condition = getCondition(position);
                        }
                    }

                    return new
                    {
                        Condition = condition,
                        Temperature = temperature
                    };
                }).ContinueWith(task =>
                {
                    result.Condition = task.Result.Condition;
                    result.Temperature = task.Result.Temperature;
                });

            // Run the task getting Pressure
            taskArray[3] = Task.Factory.StartNew(
               () =>
               {
                   return new
                   {
                       Pressure = getPressure(position)
                   };
               }).ContinueWith(task =>
               {
                   result.Pressure = task.Result.Pressure;
               });

            // Run the task getting Pressure
            taskArray[4] = Task.Factory.StartNew(
              () =>
              {
                  return new
                  {
                      Humidity = getHumidity(position)
                  };
              }).ContinueWith(task =>
              {
                  result.Humidity = task.Result.Humidity;
              });

            Task.WaitAll(taskArray);

            return result;
        }

        public string getLocation(Position position)
        {
            // based on the latitude and longtitude, return the location name
            var locationData = _locationData.FirstOrDefault(l => l.Latitude == position.Latitude && l.Longitude == position.Longitude);
            return (locationData != null) ? locationData.LocationName : string.Empty;
        }

        public double getElevation(Position position)
        {
            // based on the latitude and longtitude, return the location name
            var locationData = _locationData.FirstOrDefault(l => l.Latitude == position.Latitude && l.Longitude == position.Longitude);
            return (locationData != null) ? locationData.Elevation : 0;
        }

        public Condition getCondition(Position position)
        {
            // In real life, it would require the position to retrieve the temperature via web services like OpenWeatherMap, Yahoo weather API or Google weather API
            // Because we're stimulating the weather therefore I am just generating random condition 
            Array values = Enum.GetValues(typeof(Condition));
            Random random = new Random(DateTime.Now.Ticks.GetHashCode());
            return (Condition)values.GetValue(random.Next(values.Length));
        }

        public string getTemperature(Position position)
        {
            // In real life, it would require the position to retrieve the temperature via web services like OpenWeatherMap, Yahoo weather API or Google weather API
            // Because we're stimulating the weather therefore I am just generating random number 
            // Hottest temperature recorded was 56.7 °C and coldest was −89.2 °C

            Random random = new Random(DateTime.Now.Ticks.GetHashCode());
            var maxNumber = 56.7;
            var minNumber = -89.2;
            var randomTemperature = Math.Round(random.NextDouble() * (maxNumber - minNumber) + minNumber, 1);
            return randomTemperature >= 0 ? string.Format("+{0}", randomTemperature) : randomTemperature.ToString();
        }

        public double getPressure(Position position)
        {
            // In real life, it would require the position to retrieve the temperature via web services like OpenWeatherMap, Yahoo weather API or Google weather API
            // Because we're stimulating the weather therefore I am just generating random number 
            // Highest Pressure recorded was 108.5kPa and lowest Pressure was 87kPa

            Random random = new Random(DateTime.Now.Ticks.GetHashCode());
            var maxNumber = 108.5;
            var minNumber = 87;
            return Math.Round(random.NextDouble() * (maxNumber - minNumber) + minNumber, 1);
        }

        public double getHumidity(Position position)
        {
            // In real life, it would require the position to retrieve the temperature via web services like OpenWeatherMap, Yahoo weather API or Google weather API
            // Because we're stimulating the weather therefore I am just generating random number 

            Random random = new Random(DateTime.Now.Ticks.GetHashCode());
            return (double)random.Next(0, 100);
        }

        public DateTimeOffset getCurrentLocalDateTimeByLocation(string location)
        {
            // if we're using real web services like googleAPI or yahooApi, latitude and longtitude should be enough to determine where the location is and the local time is
            // In our case, I just gonna use the location name (city's name) to achieve the local time
            var timeZoneInfo = TimeZoneInfo.GetSystemTimeZones().First(tz => tz.DisplayName.Contains(location));
            return TimeZoneInfo.ConvertTime(DateTimeOffset.Now, timeZoneInfo);
        }

        #region Dispose
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // dispose any unnecessary objects here
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
