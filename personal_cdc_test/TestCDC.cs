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




        public TestCDC()
        {
            InitializeComponent();

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
            // Snowflake socket connection.
            var connector = new Connection();
            IDbConnection snowConn = connector.snowSocket();

            var logger = new SnowLog();
            var op = new CdcSQL();

            snowConn.Open();



            snowConn.Close();
        }

        private void DeleteHDSButton_Click(object sender, EventArgs e)
        {
            // Snowflake socket connection.
            var connector = new Connection();
            IDbConnection snowConn = connector.snowSocket();

            // CDC function object
            var op = new CdcSQL();
            op.HdsDelete(snowConn, "Customer");

            MessageBox.Show("Items in Hds deleted");
        }

        private void UpdateHdsButton_Click(object sender, EventArgs e)
        {
            // Snowflake socket connection.
            var connector = new Connection();
            IDbConnection snowConn = connector.snowSocket();

            // CDC function object
            var op = new CdcSQL();
            op.HdsUpdate(snowConn, "Customer");

            MessageBox.Show("Update complete.");
        }

        private void AddHDSButton_Click(object sender, EventArgs e)
        {
            // Snowflake socket connection.
            var connector = new Connection();
            IDbConnection snowConn = connector.snowSocket();

            // CDC function object
            var op = new CdcSQL();
            op.HdsAdd(snowConn, "Customer");

            MessageBox.Show("Added new items to the HDS");
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