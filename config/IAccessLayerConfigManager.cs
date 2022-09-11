using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework.config
    {
    public interface IAccessLayerConfigManager
        {
        string UserId { get; }
        string DataSource { get; }

        string Password { get; }

        string DatabaseName { get; }

        string GetConnectionString(string connectionName);

        IConfigurationSection GetConfigurationSection(string key);
        }
    }
