using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

using System.Data.Odbc;
using Snowflake.Data.Client;
using System.Data.SqlClient;

namespace personal_cdc_test
{
    /// <summary>
    /// Contains methods that return info needed for 
    /// delete, update, and add functionality.
    /// </summary>
    class CdcSQL
    {
        /// <summary>
        /// Adds new records from landing into HDS. Table is specified through the parameter. 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="tableName"></param>
        public void HdsAdd(IDbConnection c, string tableName)
        {
            try
            {
                c.Open();
                
                List<string> columnName = GetColumnName(c, tableName);
                int width = columnName.Count();
                string primaryKey = columnName[0];

                // columnVariable represents the names of the column parameter that is used to fill the hds table.
                // Formatted to "(col1, col2, col3, ..., colx)"
                string columnVariable = "";

                for(int i = 0; i < width; i++)
                {
                    columnVariable += columnName[i];
                    if (i + 1 < width)
                    {
                        columnVariable += ", ";
                    }
                }

                IDbCommand addNewRecords = c.CreateCommand();

                // Add all active ids in Landing that are not present in HDS to HDS.
                addNewRecords.CommandText = "INSERT INTO HDS." + tableName + " (" + columnVariable + ") " +
                                            "SELECT " + columnVariable + " " +
                                            "FROM Landing." + tableName + " l " +
                                            "WHERE l." + primaryKey + " NOT IN " +
                                            "(" +
                                            "SELECT h." + primaryKey + " " +
                                            "FROM Hds." + tableName + " h " +
                                            "WHERE h.Delete_Reason IS NULL);";

                addNewRecords.ExecuteReader();
                c.Close();
            }
            catch (SnowflakeDbException sfe)
            {
                MessageBox.Show(sfe.ToString());
            }
        }

        /// <summary>
        /// Soft deletes any active records in HDS which are not present in the new Landing data.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="tableName"></param>
        public void HdsDelete(IDbConnection c, string tableName)
        {
            try
            {
                c.Open();

                // Delete sql only requires the primary key for the deletion.
                List<string> columnName = GetColumnName(c, tableName);
                string primaryKey = columnName[0];

                IDbCommand deleteOldRecords = c.CreateCommand();

                // Update all active ids in HDS that are not present in Landing to soft delete.
                deleteOldRecords.CommandText = "UPDATE Hds." + tableName + " " +
                                               "SET Delete_Reason = 1, Delete_Datetime = CURRENT_DATE " +
                                               "WHERE Delete_Reason IS NULL " +
                                               "AND " + primaryKey + " NOT IN " +
                                               "(" +
                                                 "SELECT l." + primaryKey + " " +
                                                 "FROM Landing." + tableName + " l);";

                deleteOldRecords.ExecuteReader();
                c.Close();
            }
            catch (SnowflakeDbException sfe)
            {
                MessageBox.Show(sfe.ToString());
            }
        }

        /// <summary>
        /// Runs through a basic boolean logic to determine if a record needs to be updated or not.
        /// Records that have to be updated are soft deleted in the HDS.
        /// New records are added in the add method rather than here.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="tableName"></param>
        public void HdsUpdate(IDbConnection c, string tableName)
        {
            try
            {
                c.Open();

                // Generate where statement based on column names
                List<string> columnName = GetColumnName(c, tableName);

                // Save the first column as the primary key. Primary key will determine which records to compare.
                string primaryKey = columnName[0];
                columnName.RemoveAt(0);

                // Leave out the last column (Load_timedate) as we only care about the hard contents of the records.
                int width = columnName.Count() - 1;
                string whereClause = "";

                for (int i = 0; i < width; i++)
                {
                    string attr = columnName[i];
                    whereClause += "((" +
                                   "h." + attr + " <> " + "l." + attr + " " +
                                   "OR h." + attr + " IS NULL " +       
                                   "OR l." + attr + " IS NULL) " +
                                   "AND NOT (" +
                                   "h." + attr + " IS NULL " +
                                   "AND l." + attr + " IS NULL)) ";
                    
                    if(i < width - 1)
                    {
                        whereClause += "OR ";
                    }
                }

                IDbCommand updateChangedRecords = c.CreateCommand();

                // Update records that have been changed to be soft deleted.
                updateChangedRecords.CommandText = "UPDATE Hds." + tableName + " h " +
                                                   "SET Delete_Reason = 2, Delete_datetime = CURRENT_DATE " +
                                                   "FROM Landing." + tableName + " l " +
                                                   "WHERE Delete_reason IS NULL " +
                                                   "AND h." + primaryKey + " = l." + primaryKey + " " +
                                                   "AND (" + whereClause + ");";

                updateChangedRecords.ExecuteReader();
                c.Close();
            }
            catch (SnowflakeDbException sfe)
            {
                MessageBox.Show(sfe.ToString());
            }
        }

        /// <summary>
        /// Truncates the landing table for next use.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="tableName"></param>
        public void TruncateLanding(IDbConnection c, string tableName)
        {
            try
            {
                c.Open();
                IDbCommand truncateLanding = c.CreateCommand();

                truncateLanding.CommandText = "TRUNCATE TABLE Landing." + tableName + ";";

                truncateLanding.ExecuteReader();
                c.Close();
            }
            catch (SnowflakeDbException sfe)
            {
                MessageBox.Show(sfe.ToString());
            }
        }

        /// <summary>
        /// Grabs the list of column names in the specified table.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<string> GetColumnName(IDbConnection c, string tableName)
        {
            List<string> columnName = new List<string>();

            try
            {
                c.Open();

                IDbCommand getColumns = c.CreateCommand();

                // Snowflake sql query to retrieve column names
                getColumns.CommandText = "SHOW COLUMNS " +
                                         "IN TABLE Landing." + tableName;

                // Parse the result to return only the column names.
                // Snowflake defaults the third index [2] to column name.
                IDataReader reader = getColumns.ExecuteReader();

                while (reader.Read())
                {
                    columnName.Add(reader.GetString(2));
                }
            }
            catch (SnowflakeDbException sfe)
            {
                MessageBox.Show(sfe.ToString());
            }

            return columnName;
        }
    }
}
