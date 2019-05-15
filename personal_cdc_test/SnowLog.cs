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
        /* Implement logging features
         * Requirements:
         *  Commit
         *  Rollback
         *  Begin
         *  Insert logs for each step
         *   - Insert on begin, update on end
         *  
         */
        public void TransactionBegin(IDbConnection c)
        {
            IDbCommand transactionBegin = c.CreateCommand();
            transactionBegin.CommandText = "BEGIN TRANSACTION";
            transactionBegin.ExecuteReader();
        }

        public void TransactionCommit(IDbConnection c)
        {
            IDbCommand transactionCommit = c.CreateCommand();
            IDbCommand unsetJobstart = c.CreateCommand();

            transactionCommit.CommandText = "COMMIT;";
            unsetJobstart.CommandText = "UNSET Jobstart;";

            unsetJobstart.ExecuteReader();
            transactionCommit.ExecuteReader(); 
        }

        public void TransactionRollback(IDbConnection c)
        {
            IDbCommand transactionRollback = c.CreateCommand();
            IDbCommand unsetJobstart = c.CreateCommand();

            transactionRollback.CommandText = "ROLLBACK;";
            unsetJobstart.CommandText = "UNSET Jobstart;";

            unsetJobstart.ExecuteReader();
            transactionRollback.ExecuteReader();
        }

        public void SetJobstart(IDbConnection c)
        {
            IDbCommand cmd = c.CreateCommand();
            cmd.CommandText = "SET Jobstart = CURRENT_TIMESTAMP;";
            cmd.ExecuteReader();
        }
    }
}
