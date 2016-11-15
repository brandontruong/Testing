using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData
{
    public class WeatherService : IWeatherService
    {
        public Weather getWeatherDataByPosition(Position position)
        {
            //position.Elevation = getElevation(position);
            //return new Weather()
            //{
            //    Location = getLocation(position),
            //    Position = position,
            //    LocalTime = getLocalDateTime(position, time),
            //    Condition = getCondition(position),
            //    Temperature = getTemperature(position),
            //    Pressure = getPressure(position),
            //    Humidity = getHumidity(position)
            //};

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
                    var condition = getCondition(position);
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


        public string getElevation(Position position)
        {
            var elevation = "";
            // lets assume the elevation doenst change over the time based on Latitude and Longitude
            if (position.Latitude == Helper.Sydney.Latitude && position.Longitude == Helper.Sydney.Longitude)
            {
                elevation = "39";
            }
            if (position.Latitude == Helper.Melbourne.Latitude && position.Longitude == Helper.Melbourne.Longitude)
            {
                elevation = "7";
            }
            if (position.Latitude == Helper.Adelaide.Latitude && position.Longitude == Helper.Adelaide.Longitude)
            {
                elevation = "48";
            }
            return elevation;
        }

        public Condition getCondition(Position position)
        {
            //for now we just return random condition 
            Array values = Enum.GetValues(typeof(Condition));
            Random random = new Random();
            return (Condition)values.GetValue(random.Next(values.Length));
        }

        public string getTemperature(Position position)
        {
            //Hottest temperature recorded was 56.7 °C and coldest was −89.2 °C
            //for now we just return random number between 56.7 and -89.2
            Random random = new Random();
            var maxNumber = 56.7;
            var minNumber = -89.2;
            var randomTemperature = Math.Round(random.NextDouble() * (maxNumber - minNumber) + minNumber, 1);
            return randomTemperature >= 0 ? string.Format("+{0}", randomTemperature) : randomTemperature.ToString();
        }

        public double getPressure(Position position)
        {
            //for now we just return random pressure value 
            Random random = new Random();
            var maxNumber = 1500;
            var minNumber = 0;
            return Math.Round(random.NextDouble() * (maxNumber - minNumber) + minNumber, 1);
        }

        public double getHumidity(Position position)
        {
            //for now we just return random number 
            Random random = new Random();
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
