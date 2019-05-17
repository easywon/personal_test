using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

using Snowflake.Data.Client;
using System.Data.SqlClient;

namespace personal_cdc_test
{
    class CdcFunc
    {
        SnowLog logger;

        public CdcFunc()
        {
            logger = new SnowLog(new Connection().SnowConnectInfo); // Change the constructor to have connection key from Zach's form
        }

        public void CDC() // Put in arguments from form selection as parameters
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

                SnowLog.LoggingInfo loginfo = new SnowLog.LoggingInfo("", "", "", "", "", "", 0);

                try
                {
                    var op = new CdcSQL();

                    op.SetJobstart(cmd);

                    logger.SetLogStart(cmd);
                    loginfo.Step = "Step 1";
                    loginfo.StepRowsAffected = op.HdsDelete(cmd, "Customer");
                    logger.SuccessLog(snowConn, loginfo);

                    logger.SetLogStart(cmd);
                    loginfo.Step = "Step 2";
                    loginfo.StepRowsAffected = op.HdsUpdate(cmd, "Customer");
                    logger.SuccessLog(snowConn, loginfo);

                    logger.SetLogStart(cmd);
                    loginfo.Step = "Step 3";
                    loginfo.StepRowsAffected = op.HdsAdd(cmd, "Customer");
                    logger.SuccessLog(snowConn, loginfo);

                    MessageBox.Show("Add");

                    op.TruncateLanding(cmd, "Customer");

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    loginfo.StepRowsAffected = 0;
                    logger.FailLog(snowConn, loginfo);
                    transaction.Rollback();
                }

                snowConn.Close();
            }
        }
    }
}
