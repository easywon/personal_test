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
        public int HdsAdd(IDbCommand c, string tableName)
        {
            List<string> columnName = GetColumnName(c, tableName);
            int width = columnName.Count();
            string primaryKey = columnName[0];

            // columnVariable represents the names of the column parameter that is used to fill the hds table.
            // Formatted to "(col1, col2, col3, ..., colx)"
            string columnVariable = "";

            for (int i = 0; i < width; i++)
            {
                columnVariable += columnName[i];
                if (i + 1 < width)
                {
                    columnVariable += ", ";
                }
            }

            // Add all active ids in Landing that are not present in HDS to HDS.
            c.CommandText = "INSERT INTO HDS." + tableName + " (" + columnVariable + ") " +
                            "SELECT " + columnVariable + " " +
                            "FROM Landing." + tableName + " l " +
                            "WHERE l." + primaryKey + " NOT IN " +
                            "(" +
                                "SELECT h." + primaryKey + " " +
                                "FROM Hds." + tableName + " h " +
                                "WHERE h.Delete_Reason IS NULL);";

            return c.ExecuteNonQuery();
        }

        /// <summary>
        /// Soft deletes any active records in HDS which are not present in the new Landing data.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="tableName"></param>
        public int HdsDelete(IDbCommand c, string tableName)
        {
            // Delete sql only requires the primary key for the deletion.
            List<string> columnName = GetColumnName(c, tableName);

            string primaryKey = columnName[0];

            // Update all active ids in HDS that are not present in Landing to soft delete.
            c.CommandText = "UPDATE Hds." + tableName + " " +
                            "SET Delete_Reason = 1, Delete_Datetime = (SELECT $Jobstart) " +
                            "WHERE Delete_Reason IS NULL " +
                            "AND " + primaryKey + " NOT IN " +
                            "(" +
                               "SELECT l." + primaryKey + " " +
                               "FROM Landing." + tableName + " l);";

            return c.ExecuteNonQuery();
        }

        /// <summary>
        /// Runs through a basic boolean logic to determine if a record needs to be updated or not.
        /// Records that have to be updated are soft deleted in the HDS.
        /// New records are added in the add method rather than here.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="tableName"></param>
        public int HdsUpdate(IDbCommand c, string tableName)
        {

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

                if (i < width - 1)
                {
                    whereClause += "OR ";
                }
            }

            // Update records that have been changed to be soft deleted.
            c.CommandText = "UPDATE Hds." + tableName + " h " +
                            "SET Delete_Reason = 2, Delete_datetime = $Jobstart " +                 
                            "FROM Landing." + tableName + " l " +                  
                            "WHERE Delete_reason IS NULL " +               
                            "AND h." + primaryKey + " = l." + primaryKey + " " +            
                            "AND (" + whereClause + ");";

            return c.ExecuteNonQuery();
        }

        /// <summary>
        /// Truncates the landing table for next use.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="tableName"></param>
        public void TruncateLanding(IDbCommand c, string tableName)
        {
            c.CommandText = "TRUNCATE TABLE Landing." + tableName + ";";

            c.ExecuteReader();
        }

        /// <summary>
        /// Grabs the list of column names in the specified table.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<string> GetColumnName(IDbCommand c, string tableName)
        {
            List<string> columnName = new List<string>();

            // Snowflake sql query to retrieve column names
            c.CommandText = "SHOW COLUMNS " +
                            "IN TABLE Landing." + tableName;

            // Parse the result to return only the column names.
            // Snowflake defaults the third index [2] to column name.
            IDataReader reader = c.ExecuteReader();

            while (reader.Read())
            {
                columnName.Add(reader.GetString(2));
            }
            reader.Close();

            return columnName;
        }

        public void SetJobstart(IDbCommand c)
        {
            c.CommandText = "SET Jobstart = CURRENT_TIMESTAMP;";
            c.ExecuteReader();
        }
    }
}
