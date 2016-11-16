namespace WeatherData
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WeatherDataModel : DbContext, IWeatherDataModel
    {
        public WeatherDataModel()
            : base("name=WeatherDataModel")
        {
        }

        public virtual DbSet<LocationData> LocationDatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
