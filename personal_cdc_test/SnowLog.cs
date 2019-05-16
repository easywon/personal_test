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
                                        "BatchID Date, " +
                                        "JobDatabase string, " +
                                        "JobName string, " +
                                        "JobRunStart TIMESTAMP, " +
                                        "Step string, " +
                                        "StepLabel string, " +
                                        "StepDescription string, " +
                                        "StepTargetSchema string, " +
                                        "StepTargetTable string, " +
                                        "StepRowsAffected int, " +
                                        "StepStatus string, " +
                                        "StepStartDatetime TIMESTAMP, " +
                                        "StepEndDatetime TIMESTAMP, " +
                                        "StepDuration int); ";
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

        public void SuccessLog(IDbConnection c, LoggingInfo l)
        {
            IDbCommand logger = c.CreateCommand();
            IDbTransaction logTransaction = c.BeginTransaction();

            logger.Connection = c;
            logger.Transaction = logTransaction;

            try
            {
                logger.CommandText = "SET LogEnd = CURRENT_TIMESTAMP;";
                logger.ExecuteReader();
                logger.CommandText = "SET TimeDiff = (SELECT DATEDIFF(second, $Logstart, CURRENT_TIMESTAMP));";
                logger.ExecuteReader();
                logger.CommandText = "INSERT INTO Log.TransactionJobTracking " +
                                     "Values(" +
                                     GetBatchDay() +", " +
                                     "CURRENT_DATABASE(), " +
                                     "'" + l.JobName + "', " +
                                     "$Jobstart, " +
                                     "'" + l.Step + "', " +
                                     "'" + l.StepLabel + "', " +
                                     "'" + l.StepDescription + "', " +
                                     "'" + l.StepTargetSchema + "', " +
                                     "'" + l.StepTargetTable + "', " +
                                     "" + l.StepRowsAffected + ", " +
                                     "'Success', " +
                                     "$Logstart, " +
                                     "$LogEnd," +
                                     "$TimeDiff);";

                logger.ExecuteReader();
                logTransaction.Commit();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                logTransaction.Rollback();
            }
        }

        public void FailLog(IDbConnection c, LoggingInfo l)
        {
            IDbCommand logger = c.CreateCommand();
            IDbTransaction logTransaction = c.BeginTransaction();

            logger.Connection = c;
            logger.Transaction = logTransaction;

            try
            {
                logger.CommandText = "INSERT INTO Log.TransactionJobTracking " +
                                     "Values(" +
                                     GetBatchDay() + ", " +
                                     "CURRENT_DATABASE()," +
                                     "CURRENT_SCHEMA()," +
                                     "'Landing to HDS - CDC'," +
                                     "'" + l.Step + "'," +
                                     "'" + l.StepDescription + "'," +
                                     "true," +
                                     "'" + l.StepTargetTable + "'," +
                                     "" + l.StepRowsAffected + "," +
                                     "$Logstart," +
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

        public void SetLogStart(IDbCommand c)
        {
            c.CommandText = "SET Logstart = CURRENT_TIMESTAMP;";
            c.ExecuteReader();
        }
        
        private string GetBatchDay()
        {
            if (DateTime.Now.Hour > 16)
                return "CURRENT_DATE+1";
            else
                return "CURRENT_DATE";
        }

        // Internal class to track necessary log information
        public class LoggingInfo
        {
            public string JobName { get; set; }
            public string Step { get; set; }
            public string StepLabel { get; set; }
            public string StepDescription { get; set; }
            public string StepTargetSchema { get; set; }
            public string StepTargetTable { get; set; }
            public int StepRowsAffected { get; set; }

            public LoggingInfo(string jobName, string step, string stepLabel, string stepDescription, string stepTargetSchema, string stepTargetTable, int stepRowsAffected)
            {
                JobName = jobName;
                Step = step;
                StepLabel = stepLabel;
                StepDescription = stepDescription;
                StepTargetSchema = stepTargetSchema;
                StepTargetTable = stepTargetTable;
                StepRowsAffected = stepRowsAffected;
            }
        }
    }
}
