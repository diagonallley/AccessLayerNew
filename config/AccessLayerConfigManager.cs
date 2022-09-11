using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework.config
    {
    public class AccessLayerConfigManager : IAccessLayerConfigManager
        {
        private readonly IConfiguration _configuration;
        public AccessLayerConfigManager(IConfiguration configuration)
            {
            this._configuration = configuration;
            }

        public string UserId { get { return this._configuration["AppSettings:UserId"]; } }
        public string Password { get { return this._configuration["AppSettings:Password"]; } }

        public string DataSource { get { return this._configuration["AppSettings:DataSource"]; } }

        public string DatabaseName { get { return this._configuration["AppSettings:DatabaseName"]; } }

        public IConfigurationSection GetConfigurationSection(string key) { return this._configuration.GetSection(key); }

        public string GetConnectionString(string connectionName) { return this._configuration.GetConnectionString(connectionName); }



        }

    }
