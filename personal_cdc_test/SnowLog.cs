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
    class SnowLog
    {
        private string step;
        private string stepLabel;
        private string stepDescription;
        private bool stepStatus;
        private string stepTargetTable;
        private int stepRowsAffected;

        #region
        public string Step
        {
            get { return step; }
            set { step = value; }
        }
        public string StepLabel
        {
            get { return stepLabel; }
            set { stepLabel = value; }
        }
        public string StepDescription
        {
            get { return stepDescription; }
            set { stepDescription = value; }
        }
        public bool StepStatus
        {
            get { return stepStatus; }
            set { stepStatus = value; }
        }

        public string StepTargetTable
        {
            get { return stepTargetTable; }
            set { stepTargetTable = value; }
        }
        public int StepRowsAffected
        {
            get { return stepRowsAffected; }
            set { stepRowsAffected = value; }
        }
        #endregion

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
                                        "StepStatus bit, " +
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
    }
}
