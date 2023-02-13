using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;
using Cohesion_DTO;

namespace Cohesion_DAO
{
    public class Common_DAO
    {
        SqlConnection conn = null;
        readonly string DB = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        public Common_DAO()
        {
            conn = new SqlConnection(DB);
        }
        public void Dispose()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
        public List<CODE_TABLE_MST_DTO> SelectCommonTable()
        {
            try
            {
                string sql = @"SELECT   CODE_TABLE_NAME, CODE_TABLE_DESC
                                      , KEY_1_NAME, KEY_2_NAME, KEY_3_NAME
                                      , DATA_1_NAME, DATA_2_NAME, DATA_3_NAME, DATA_4_NAME, DATA_5_NAME
                                      , CREATE_TIME
                                      , CREATE_USER_ID
                                      , UPDATE_TIME
                                      , UPDATE_USER_ID
                               FROM CODE_TABLE_MST";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                var list = Helper.DataReaderMapToList<CODE_TABLE_MST_DTO>(cmd.ExecuteReader());
                conn.Close();
                return list;
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return null;
            }

            finally
            {
                conn.Close();
            }
        }
        public List<CODE_DATA_MST_DTO> SelectAllCommonTableData()
        {
            string sql = @"SELECT   CODE_TABLE_NAME
                                  , DISPLAY_SEQ
                                  , KEY_1 , KEY_2 , KEY_3 
                                  , DATA_1 , DATA_2 , DATA_3 , DATA_4 , DATA_5 
                           FROM CODE_DATA_MST";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            var list = Helper.DataReaderMapToList<CODE_DATA_MST_DTO>(cmd.ExecuteReader());
            conn.Close();
            return list;
        }
    }
}
