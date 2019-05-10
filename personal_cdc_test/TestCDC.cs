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

        private void ConnectToServer_Click(object sender, EventArgs e)
        {
            // Attemp to establish connection to server
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
        }

        private void TestConnection_Click(object sender, EventArgs e)
        {

        }
    }
}
