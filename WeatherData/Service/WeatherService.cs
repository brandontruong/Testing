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
            var location = "";
            if (position.Latitude == Helper.Sydney.Latitude && position.Longitude == Helper.Sydney.Longitude)
            {
                location = "Sydney";
            }
            if (position.Latitude == Helper.Melbourne.Latitude && position.Longitude == Helper.Melbourne.Longitude)
            {
                location = "Melbourne";
            }
            if (position.Latitude == Helper.Adelaide.Latitude && position.Longitude == Helper.Adelaide.Longitude)
            {
                location = "Adelaide";
            }
            if (position.Latitude == Helper.Alaska.Latitude && position.Longitude == Helper.Alaska.Longitude)
            {
                location = "Alaska";
            }
            if (position.Latitude == Helper.Bogota.Latitude && position.Longitude == Helper.Bogota.Longitude)
            {
                location = "Bogota";
            }
            if (position.Latitude == Helper.Hanoi.Latitude && position.Longitude == Helper.Hanoi.Longitude)
            {
                location = "Hanoi";
            }
            if (position.Latitude == Helper.Hawaii.Latitude && position.Longitude == Helper.Hawaii.Longitude)
            {
                location = "Hawaii";
            }
            if (position.Latitude == Helper.Mumbai.Latitude && position.Longitude == Helper.Mumbai.Longitude)
            {
                location = "Mumbai";
            }
            if (position.Latitude == Helper.NewDelhi.Latitude && position.Longitude == Helper.NewDelhi.Longitude)
            {
                location = "New Delhi";
            }
            if (position.Latitude == Helper.Tokyo.Latitude && position.Longitude == Helper.Tokyo.Longitude)
            {
                location = "Tokyo";
            }
            return location;
        }


        public double getElevation(Position position)
        {
            double elevation = 0;
            // lets assume the elevation doenst change over the time based on Latitude and Longitude
            if (position.Latitude == Helper.Sydney.Latitude && position.Longitude == Helper.Sydney.Longitude)
            {
                elevation = 39;
            }
            else if (position.Latitude == Helper.Melbourne.Latitude && position.Longitude == Helper.Melbourne.Longitude)
            {
                elevation = 7;
            }
            else if (position.Latitude == Helper.Adelaide.Latitude && position.Longitude == Helper.Adelaide.Longitude)
            {
                elevation = 48;
            }
            else if (position.Latitude == Helper.Alaska.Latitude && position.Longitude == Helper.Alaska.Longitude)
            {
                elevation = 6190.5;
            }
            else if (position.Latitude == Helper.Bogota.Latitude && position.Longitude == Helper.Bogota.Longitude)
            {
                elevation = 2644;
            }
            else if (position.Latitude == Helper.Hanoi.Latitude && position.Longitude == Helper.Hanoi.Longitude)
            {
                elevation = 10;
            }
            else if (position.Latitude == Helper.Hawaii.Latitude && position.Longitude == Helper.Hawaii.Longitude)
            {
                elevation = 4205;
            }
            else if (position.Latitude == Helper.Mumbai.Latitude && position.Longitude == Helper.Mumbai.Longitude)
            {
                elevation = 14;
            }
            else if (position.Latitude == Helper.NewDelhi.Latitude && position.Longitude == Helper.NewDelhi.Longitude)
            {
                elevation = 216;
            }
            else if (position.Latitude == Helper.Tokyo.Latitude && position.Longitude == Helper.Tokyo.Longitude)
            {
                elevation = 40;
            }
            return elevation;
        }

        public Condition getCondition(Position position)
        {
            //for now we just return random condition 
            Array values = Enum.GetValues(typeof(Condition));
            Random random = new Random(DateTime.Now.Ticks.GetHashCode());
            return (Condition)values.GetValue(random.Next(values.Length));
        }

        public string getTemperature(Position position)
        {
            //Hottest temperature recorded was 56.7 °C and coldest was −89.2 °C
            //for now we just return random number between 56.7 and -89.2
            Random random = new Random(DateTime.Now.Ticks.GetHashCode());
            var maxNumber = 56.7;
            var minNumber = -89.2;
            var randomTemperature = Math.Round(random.NextDouble() * (maxNumber - minNumber) + minNumber, 1);
            return randomTemperature >= 0 ? string.Format("+{0}", randomTemperature) : randomTemperature.ToString();
        }

        public double getPressure(Position position)
        {
            //for now we just return random pressure value 
            Random random = new Random(DateTime.Now.Ticks.GetHashCode());
            var maxNumber = 1500;
            var minNumber = 0;
            return Math.Round(random.NextDouble() * (maxNumber - minNumber) + minNumber, 1);
        }

        public double getHumidity(Position position)
        {
            //for now we just return random number 
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
