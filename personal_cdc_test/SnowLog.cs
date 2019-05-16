using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Snowflake.Data.Client;

namespace personal_cdc_test
{
    public class SnowLog
    {
        // Constructor - Make the necessary table for log storage if it does not exist. 
        public SnowLog(string connectInfo)
        {
            using (IDbConnection c = new SnowflakeDbConnection())
            {
                c.ConnectionString = connectInfo;
                c.Open();

                // Declare the command and transactions which will be used throughout the entire batch job.
                IDbCommand cmd = c.CreateCommand();
                IDbTransaction logTransaction;

                // Start the transaction
                logTransaction = c.BeginTransaction();

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                cmd.Connection = c;
                cmd.Transaction = logTransaction;

                try
                {
                    // Ensure the Datetime columns will have the proper data type.
                    cmd.CommandText = "ALTER SESSION SET timestamp_type_mapping = timestamp_ltz;";
                    cmd.ExecuteReader();

                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Log.TransactionJobTracking ( " +
                                        "BatchID TIMESTAMP, " +
                                        "JobDatabase string, " +
                                        "JobSchema string, " +
                                        "JobName string, " +
                                        "Step string, " +
                                        "StepDescription string, " +
                                        "StepStatus boolean, " +
                                        "StepTargetTable string, " +
                                        "StepRowsAffected int, " +
                                        "StepStartDatetime TIMESTAMP, " +
                                        "StepEndDatetime TIMESTAMP); ";
                    cmd.ExecuteReader();

                    logTransaction.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    logTransaction.Rollback();
                }

                c.Close();
            }
        }

        public void StartLog(IDbConnection c, LoggingInfo l)
        {
            IDbCommand logger = c.CreateCommand();
            IDbTransaction logTransaction = c.BeginTransaction();

            logger.Connection = c;
            logger.Transaction = logTransaction;

            try
            {
                logger.CommandText = "INSERT INTO Log.TransactionJobTracking " +
                                     "Values(" +
                                     "$Jobstart, " +
                                     "CURRENT_DATABASE()," +
                                     "CURRENT_SCHEMA()," +
                                     "'Landing to HDS - CDC'," +
                                     "'" + l.Step + "'," +
                                     "'" + l.StepDescription + "'," +
                                     "false," +
                                     "'" + l.StepTargetTable + "'," +
                                     "" + l.StepRowsAffected + "," +
                                     "$Jobstart," +
                                     "CURRENT_TIMESTAMP)";

                logger.ExecuteReader();
                logTransaction.Commit();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                logTransaction.Rollback();
            }
        }

        public void EndLog(IDbConnection c, LoggingInfo l)
        {
            IDbCommand logger = c.CreateCommand();
            IDbTransaction logTransaction = c.BeginTransaction();

            logger.Connection = c;
            logger.Transaction = logTransaction;

            try
            {
                logger.CommandText = "INSERT INTO Log.TransactionJobTracking " +
                                     "Values(" +
                                     "$Jobstart, " +
                                     "CURRENT_DATABASE()," +
                                     "CURRENT_SCHEMA()," +
                                     "'Landing to HDS - CDC'," +
                                     "'" + l.Step + "'," +
                                     "'" + l.StepDescription + "'," +
                                     "true," +
                                     "'" + l.StepTargetTable + "'," +
                                     "" + l.StepRowsAffected + "," +
                                     "$Jobstart," +
                                     "CURRENT_TIMESTAMP)";

                logger.ExecuteReader();
                logTransaction.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                logTransaction.Rollback();
            }
        }

        // Internal class to track necessary log information
        public class LoggingInfo
        {
            public string Step { get; set; }
            public string StepLabel { get; set; }
            public string StepDescription { get; set; }
            public string StepTargetTable { get; set; }
            public int StepRowsAffected { get; set; }

            public LoggingInfo(string step, string stepLabel, string stepDescription, string stepTargetTable, int stepRowsAffected)
            {
                Step = step;
                StepLabel = stepLabel;
                StepDescription = stepDescription;
                StepTargetTable = stepTargetTable;
                StepRowsAffected = stepRowsAffected;
            }
        }
    }
}
