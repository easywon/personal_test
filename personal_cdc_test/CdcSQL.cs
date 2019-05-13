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
        /// Creates a join on Landing and HDS to find all items in HDS that do not exist in Landing
        /// </summary>
        /// <param name="c"></param>
        /// <param name="tableName"></param>
        /// <returns>
        /// List of Id's (int) from HDS that must be deleted.
        /// </returns>
        public List<int> getHdsDelete(IDbConnection c, string tableName)
        {
            List<int> result = new List<int>();
            try
            {
                c.Open();
                IDbCommand getDeleteList = c.CreateCommand();

                // Select all active ids in hds that are not present in landing.
                getDeleteList.CommandText = "SELECT h.Id " +
                                            "FROM Hds." + tableName + " AS h " +
                                            "WHERE h.Delete_Reason IS NULL " +
                                            "AND h.Id NOT IN " +
                                            "(" +
                                            "SELECT l.Id " +
                                            "FROM Landing." + tableName + " AS l);";

                IDataReader delReader = getDeleteList.ExecuteReader();

                // Adds the result of the query into the return result
                while (delReader.Read())
                {
                    result.Add(delReader.GetInt32(0));
                }

                c.Close();
            }
            catch (SnowflakeDbException sfe)
            {
                MessageBox.Show(sfe.ToString());
            }

            return result;
        }

        public void HdsAdd(IDbConnection c, string tableName)
        {
            try
            {
                c.Open();
                IDbCommand getAddList = c.CreateCommand();

                List<string> columnName = GetColumnName(c, tableName);
                int width = columnName.Count();

                // Add all active ids in Landing that are not present in HDS to HDS.
                getAddList.CommandText = "INSERT INTO HDS.Customer (Id, Name, Load_DateTime)" +
                                         "INSERT INTO HDS." + tableName + 
                                         " (" + columnName[0] + ", Name, Load_DateTime)" +
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
