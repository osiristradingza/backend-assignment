using Microsoft.Extensions.Configuration;

namespace OT.Assessment.Consumer.Data
{
    public abstract class DataAccesBase
    {
        protected readonly IConfiguration _configuration;
        public DataAccesBase()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            _configuration = configurationBuilder.Build();
        }

    }
}
