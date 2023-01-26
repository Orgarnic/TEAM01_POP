using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cohesion_DTO;

namespace Cohesion_DAO
{
    public class Ship_DAO : IDisposable
    {
        SqlConnection conn = null;
        readonly string DB = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        public Ship_DAO()
        {
            conn = new SqlConnection(DB);
        }
        public void Dispose()
        {
            if (conn != null || conn.State == ConnectionState.Open)
                conn.Close();
        }
        
        public List<LOTState_DTO> SelectProductInStore(string productCode)
        {
            try
            {
                string sql = @"SELECT LOT_ID
	                                , LOT_QTY
	                                , LAST_TRAN_TIME
	                                , RANK()OVER(ORDER BY LAST_TRAN_TIME ASC) DISPLAY_SEQ
                               FROM LOT_STS 
                               WHERE LAST_TRAN_CODE = 'MOVE'
                                 AND PRODUCT_CODE = @PRODUCT_CODE
                                 AND STORE_CODE IN (SELECT STORE_CODE FROM STORE_MST WHERE STORE_TYPE = 'FS')";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@PRODUCT_CODE", productCode);
                conn.Open();
                var list =Helper.DataReaderMapToList<LOTState_DTO>(cmd.ExecuteReader());
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

    }
}
