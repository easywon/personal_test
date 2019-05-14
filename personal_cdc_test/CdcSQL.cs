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
                IDbCommand getAddList = c.CreateCommand();

                List<string> columnName = GetColumnName(c, tableName);
                int width = columnName.Count();

                // columnVariable represents the names of the column parameter that is used to fill the hds table.
                // Formatted to "(col1, col2, col3, ..., colx)"
                string columnVariable = " (";

                for(int i = 0; i < width; i++)
                {
                    columnVariable += columnName[i];
                    if (i + 1 < width)
                    {
                        columnVariable += ", ";
                    }
                    else
                    {
                        columnVariable += ") ";
                    }
                }

                // Add all active ids in Landing that are not present in HDS to HDS.
                getAddList.CommandText = "INSERT INTO HDS." + tableName + columnVariable +
                                         "SELECT Id, Name, Load_Datetime " +
                                         "FROM Landing." + tableName + " l " +
                                         "WHERE l.Id NOT IN " +
                                         "(" +
                                         "SELECT h.Id " +
                                         "FROM Hds." + tableName + " h " +
                                         "WHERE h.Delete_Reason IS NULL);";

                getAddList.ExecuteReader();
                c.Close();
            }
            catch (SnowflakeDbException sfe)
            {
                MessageBox.Show(sfe.ToString());
            }
        }

        /// <summary>
        /// Deletes any active records in HDS which are not present in the new Landing data.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="tableName"></param>
        public void HdsDelete(IDbConnection c, string tableName)
        {
            try
            {
                c.Open();
                IDbCommand getAddList = c.CreateCommand();

                // Delete sql only requires the primary key for the deletion.
                List<string> columnName = GetColumnName(c, tableName);
                string primaryKey = columnName[0];
                

                // Delete all active ids in HDS that are not present in Landing.
                getAddList.CommandText = "DELETE FROM Hds." + tableName + " " +
                                         "WHERE " + primaryKey + " IN " +
                                         "(" +
                                           "SELECT h." + primaryKey + " " +
                                           "FROM Hds." + tableName + " AS h " +
                                           "WHERE h.Delete_Reason IS NULL " +
                                           "AND h." + primaryKey + " NOT IN " +
                                           "(" +
                                             "SELECT l." + primaryKey + " " +
                                             "FROM Landing." + tableName + " AS l));";

                getAddList.ExecuteReader();
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
