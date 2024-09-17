using Microsoft.Extensions.Configuration;
using OT.Assessment.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Domain.Implementation
{
    public class GlobalConfiguration : IGlobalConfiguration
    {
        private readonly IConfiguration _configuration;

        public GlobalConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool UseMessaging => bool.TryParse(_configuration["GlobalConfiguration:UseMessaging"], out bool isEnabled) ? isEnabled : false;
    }
}
