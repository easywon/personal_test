using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using System.Data.Odbc;
using Snowflake.Data.Client;
using System.Data.SqlClient;

namespace personal_cdc_test
{
    class Connection
    {
        private string snowConnectInfo = "ACCOUNT=zs31584;" +
                                         "HOST=zs31584.east-us-2.azure.snowflakecomputing.com;" +
                                         "USER=CraigWeiss;" +
                                         "PASSWORD=Sage.4242;" +
                                         "WAREHOUSE=COMPUTE_WH;" +
                                         "DB=SANDBOX_MODEL_PETER;";

        public string SnowConnectInfo
        {
            get
            {
                return snowConnectInfo;
            }
            set
            {
                snowConnectInfo = value;
            }
        }

        // Create a connection to the SQL server
        public IDbConnection sqlSocket()
        {
            OdbcConnectionStringBuilder builder = new OdbcConnectionStringBuilder();
            builder.Driver = "SQL Server";
            builder.Add("Server", "cougar5");
            builder.Add("Database", "SANDBOXMODEL_PETER");
            builder.Add("Trusted_Connection", "Yes");

            IDbConnection socket = new OdbcConnection(builder.ConnectionString);
            return socket;
        }
        // Implement dynamic connection key creation later
    }
}
