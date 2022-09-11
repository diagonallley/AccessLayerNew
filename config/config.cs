using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
namespace framework.config
{
    public static class config
    {
        public static string connectionString;

        public static string GetConnectionString()
        {

            #region Configuration
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true);
            //var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true);
            var config = builder.Build();
            var DataServer = config["userInfo:dataSource"];  //Data Source or the Server Name fetched from the appsettings.json file 
            #endregion

            #region connectionStringBuilder
            string DatabaseName = config["userInfo:DataBaseName"]; // Database or Schema name
            string Trusted_Connection = "true"; //Trusted Connection property 
            string MultipleActiveResultSets = "true"; //MultipleActiveResults

            string connString = "data source=" + DataServer + ";" + "Database=" + DatabaseName + ";" + "Trusted_Connection=" + Trusted_Connection + ";" + "MultipleActiveResultSets=" + MultipleActiveResultSets; //ConnectionString builder


            connectionString = connString;
            #endregion

            return connectionString;

        }




    }
}
