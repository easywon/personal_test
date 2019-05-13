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

        public List<int> getHdsAdd(IDbConnection c, string tableName)
        {
            List<int> result = new List<int>();
            try
            {
                c.Open();
                IDbCommand getAddList = c.CreateCommand();

                // Select all active ids in Landing that are not present in HDS.
                getAddList.CommandText = "SELECT l.Id " +
                                         "FROM Landing." + tableName + " l " +
                                         "WHERE l.Id NOT IN " +
                                         "(" +
                                         "SELECT h.Id " +
                                         "FROM Hds." + tableName + " h " +
                                         "WHERE h.Delete_Reason IS NULL);";

                IDataReader addReader = getAddList.ExecuteReader();

                // Adds the result of the query into the return result
                while (addReader.Read())
                {
                    result.Add(addReader.GetInt32(0));
                }

                c.Close();
            }
            catch (SnowflakeDbException sfe)
            {
                MessageBox.Show(sfe.ToString());
            }

            return result;
        }
    }
}
