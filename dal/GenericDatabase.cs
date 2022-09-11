using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using framework.config;
namespace framework.dal
{
    class GenericDatabase
        {

        public static IDbConnection GetConnection()
            {

            //IDbConnection connection = SqlClientFactory.Instance.CreateConnection();
            IDbConnection connection = new SqlConnection();
            if (config.config.connectionString == null)
                {
                connection.ConnectionString = config.config.GetConnectionString();

                }
            else
                {
                connection.ConnectionString = config.config.connectionString;
                }
            //connection.ConnectionString = config.GetConnectionString();


            return connection;
            }

        public static IDbCommand GetCommand()
            {
            IDbCommand command = SqlClientFactory.Instance.CreateCommand();
            return command;
            }

        public static DbParameter GetParameter()
            {
            DbParameter parameter = new SqlParameter();
            return parameter;
            }

        public static IDbDataAdapter GetAdapter(IDbCommand command)
            {
            IDbDataAdapter adapter = new SqlDataAdapter((SqlCommand)command);

            return adapter;

            }



        }

    }
