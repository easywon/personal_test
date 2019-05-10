using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.Odbc;
using Snowflake.Data.Client;
using System.Data.SqlClient;

namespace personal_cdc_test
{
    public partial class TestCDC : Form
    {
        //acc: zs31584.east-us-2.azure
        //user: CraigWeiss
        //pass: Sage.4242
        //wh: COMPUTE_WH

        string connectInfo = "ACCOUNT=zs31584;" +
                             "HOST=zs31584.east-us-2.azure.snowflakecomputing.com;" +
                             "USER=CraigWeiss;" +
                             "PASSWORD=Sage.4242;" +
                             "WAREHOUSE=COMPUTE_WH;" +
                             "DB=SANDBOX_MODEL_PETER;";


        public TestCDC()
        {
            InitializeComponent();

            mainMessage.Text = "Welcome!";
        }

        private void mainMessage_Click(object sender, EventArgs e)
        {
            
        }

        private void connectToSQL_Click(object sender, EventArgs e)
        {
            OdbcConnectionStringBuilder builder = new OdbcConnectionStringBuilder();
            builder.Driver = "SQL Server";
            builder.Add("Server", "cougar5");
            builder.Add("Database", "SANDBOX_MODEL_PETER");
            builder.Add("Trusted_Connection", "Yes");
            
            IDbConnection sqlConn = new OdbcConnection(builder.ConnectionString);

            try
            {
                sqlConn.Open();
                IDbCommand sqlCmd = sqlConn.CreateCommand();
                sqlCmd.CommandText = "SELECT * FROM dbo.Source";
                IDataReader reader = sqlCmd.ExecuteReader();


                string result = "";
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        result += reader.GetString(i) + " ";
                    }
                    result += "\n";
                }

                MessageBox.Show(result);

                reader.Close();
                sqlConn.Close();
                mainMessage.Text = "SQL command Successful";
            }
            catch(SqlException sqle)
            {
                mainMessage.Text = sqle.ToString();
            }
        }

        private void connectToSnow_Click(object sender, EventArgs e)
        {
            IDbConnection conn = new SnowflakeDbConnection();
            conn.ConnectionString = connectInfo;

            try
            {
                string result = "";

                conn.Open();
                IDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Landing.Product;";
                IDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        result += reader.GetString(i) + " ";
                    }
                    result += "\n";
                }

                MessageBox.Show(result);

                reader.Close();
                conn.Close();
                mainMessage.Text = "Test Successful";
            }
            catch (SnowflakeDbException sfe)
            {
                mainMessage.Text = sfe.ToString();
            }
        }
    }
}

/*
 *             // Attemp to establish connection to server
            IDbConnection conn = new SnowflakeDbConnection();
            conn.ConnectionString = connectInfo;

            try
            {
                string result = "";

                conn.Open();
                IDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Landing.Product;";
                IDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        result += reader.GetString(i) + " ";
                    }
                    result += "\n";
                }

                Console.WriteLine(result);
                MessageBox.Show(result);

                reader.Close();
                conn.Close();
                mainMessage.Text = "Test Successful";
            }
            catch(SnowflakeDbException sfe)
            {
                mainMessage.Text = sfe.ToString();
            }
*/