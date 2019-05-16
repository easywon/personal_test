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

        SnowLog logger;
        
        public TestCDC()
        {
            InitializeComponent();

            logger = new SnowLog(new Connection().SnowConnectInfo);
            mainMessage.Text = "Welcome!";
        }

        private void connectToSQL_Click(object sender, EventArgs e)
        {
            // SQL Socket connection
            var connector = new Connection();
            IDbConnection sqlConn = connector.sqlSocket();

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
            using (IDbConnection snowConn = new SnowflakeDbConnection())
            {
                // Open the connection
                snowConn.ConnectionString = new Connection().SnowConnectInfo;
                snowConn.Open();

                // Declare the command and transactions which will be used throughout the entire batch job.
                IDbCommand cmd = snowConn.CreateCommand();
                IDbTransaction transaction;

                // Start the transaction
                transaction = snowConn.BeginTransaction();

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                cmd.Connection = snowConn;
                cmd.Transaction = transaction;

                try
                {
                    var op = new CdcSQL();

                    op.SetJobstart(cmd);

                    op.HdsDelete(cmd, "Customer");
                    op.HdsUpdate(cmd, "Customer");
                    op.HdsAdd(cmd, "Customer");

                    op.TruncateLanding(cmd, "Customer");

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    transaction.Rollback();
                }

                snowConn.Close();
            }
        }


        private void DeleteHDSButton_Click(object sender, EventArgs e)
        {
            using (IDbConnection snowConn = new SnowflakeDbConnection())
            {
                // Open the connection
                snowConn.ConnectionString = new Connection().SnowConnectInfo;
                snowConn.Open();

                // Declare the command and transactions which will be used throughout the entire batch job.
                IDbCommand cmd = snowConn.CreateCommand();
                IDbTransaction transaction;

                // Start the transaction
                transaction = snowConn.BeginTransaction();

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                cmd.Connection = snowConn;
                cmd.Transaction = transaction;

                try
                {
                    MessageBox.Show(DateTime.Now.ToString());
                    cmd.CommandText = "INSERT INTO Log.Date " +
                                      "VALUES (" + DateTime.Now.ToString() + ") ";
                    cmd.ExecuteReader();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    transaction.Rollback();
                }

                snowConn.Close();
            }
        }

        private void UpdateHdsButton_Click(object sender, EventArgs e)
        {
            using (IDbConnection snowConn = new SnowflakeDbConnection())
            {
                // Open the connection
                snowConn.ConnectionString = new Connection().SnowConnectInfo;
                snowConn.Open();

                // Declare the command and transactions which will be used throughout the entire batch job.
                IDbCommand cmd = snowConn.CreateCommand();
                IDbTransaction transaction;

                // Start the transaction
                transaction = snowConn.BeginTransaction();

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                cmd.Connection = snowConn;
                cmd.Transaction = transaction;

                try
                {
                    var op = new CdcSQL();

                    op.SetJobstart(cmd);

                    cmd.CommandText = "SELECT $Jobstart";
                    IDataReader reader = cmd.ExecuteReader();

                    SnowLog.LoggingInfo logInfo = new SnowLog.LoggingInfo("Step 1",
                                                      "Testing",
                                                      "Debugging the process of logging",
                                                      false,
                                                      "Customer",
                                                      reader.RecordsAffected);

                    while (reader.Read())
                    {
                        MessageBox.Show(reader.GetString(0));
                    }


                    logger.StartLog(snowConn, logInfo);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    transaction.Rollback();
                }

                snowConn.Close();
            }
        }

        private void AddHDSButton_Click(object sender, EventArgs e)
        {
            /*
            // Snowflake socket connection.
            var connector = new Connection();
            IDbConnection snowConn = connector.snowSocket();

            // CDC function object
            var op = new CdcSQL();
            op.HdsAdd(snowConn, "Customer");

            MessageBox.Show("Added new items to the HDS");*/
        }

            private void ButtonTableLayout_Paint(object sender, PaintEventArgs e)
        {

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