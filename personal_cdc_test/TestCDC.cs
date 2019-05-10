using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace personal_cdc_test
{
    public partial class TestCDC : Form
    {
        //user: CraigWeiss
        //pass: Sage.4242
        //wh: COMPUTE_WH


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
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "cougar5";
                builder.IntegratedSecurity = true;

                using (SqlConnection connect = new SqlConnection(builder.ConnectionString))
                {
                    connect.Open();
                    mainMessage.Text = "Connected";

                }
            }
            catch(SqlException sqle)
            {
                mainMessage.Text = sqle.ToString();
            }
        }

        private void TestConnection_Click(object sender, EventArgs e)
        {

        }
    }
}
