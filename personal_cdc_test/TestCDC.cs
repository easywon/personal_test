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

        string snowConnectInfo = "ACCOUNT=zs31584;" +
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
            conn.ConnectionString = snowConnectInfo;

            try
            {
                conn.Open();
                IDbCommand landingCmd = conn.CreateCommand();
                IDbCommand hdsCmd = conn.CreateCommand();

                landingCmd.CommandText = "SELECT * FROM Landing.Customer;";
                hdsCmd.CommandText = "SELECT * FROM HDS.Customer WHERE Delete_Datetime IS NULL;";

                IDataReader landingRead = landingCmd.ExecuteReader();
               

                // start by looking at each record of landing.
                while (landingRead.Read())
                {
                    // Compare the primary key against each entry in hds.
                    IDataReader hdsRead = hdsCmd.ExecuteReader();

                    // tracks whether an entry already exists or if current record is brand new
                    bool matchFound = false;
                    bool newEntry = false;

                    int replaceId = 0;

                    while (hdsRead.Read())
                    {
                        // Same primary key in landing and hds.
                        if(landingRead.GetInt32(0) == hdsRead.GetInt32(0))
                        {
                            // Match indicates that the record has been inserted into HDS before.
                            matchFound = true;

                            // Checks all field values except for first and last (id and land_time)
                            for (int i = 1; i < landingRead.FieldCount - 1; i++)
                            {
                                // *future implemenation* Use reader.GetFieldType() and use switch cases
                                if(landingRead.GetString(i) != hdsRead.GetString(i))
                                {
                                    newEntry = true;
                                    replaceId = landingRead.GetInt32(0);
                                }
                            }
                        }
                    }

                    if (!matchFound)
                    {
                        newEntry = true;
                    }

                    // No match. No entry. Default Error case
                    if (!matchFound && !newEntry)
                    {
                        // Throw exception
                        MessageBox.Show("00");

                    }
                    // No match. Requires a new entry. 
                    else if(!matchFound && newEntry)
                    {
                        // Insert new record
                        IDbCommand hdsInsert = conn.CreateCommand();
                        
                        hdsInsert.CommandText = "INSERT INTO Hds.Customer(Id, Name, Load_Datetime) VALUES (" +
                                             landingRead.GetInt32(0) + ", '" +
                                             landingRead.GetString(1) + "', '" +
                                             landingRead.GetDateTime(2) + "');";

                        MessageBox.Show(hdsInsert.CommandText);
                        hdsInsert.ExecuteReader();
                    }
                    // Matched. Same record. No need for update.
                    else if(matchFound && !newEntry)
                    {
                        // Do nothing
                        mainMessage.Text = "Duplicate Entry.";
                    }
                    // Matched. Requires a new entry. Update.
                    else if(matchFound && newEntry)
                    {
                        MessageBox.Show("In Update");

                        // Update the old entry with delete reason and date.
                        IDbCommand hdsUpdate = conn.CreateCommand();
                        hdsUpdate.CommandText = "UPDATE Hds.Customer " +
                                                "SET Delete_Reason = 2, Delete_Datetime = CURRENT_DATE " +
                                                "WHERE Id = " + replaceId + " AND Delete_Datetime IS NULL;";

                        hdsUpdate.ExecuteReader();

                        // Insert new record.
                        IDbCommand hdsInsert = conn.CreateCommand();

                        hdsInsert.CommandText = "INSERT INTO Hds.Customer(Id, Name, Load_Datetime) VALUES (" +
                                             landingRead.GetInt32(0) + ", '" +
                                             landingRead.GetString(1) + "', '" +
                                             landingRead.GetDateTime(2) + "');";

                        MessageBox.Show(hdsInsert.CommandText);
                        hdsInsert.ExecuteReader();
                    }

                    hdsRead.Close();
                }

           

                landingRead.Close();
                conn.Close();
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