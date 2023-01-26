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
        
        public List<LOT_STS_DTO> SelectProductInStore(string productCode)
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
                var list =Helper.DataReaderMapToList<LOT_STS_DTO>(cmd.ExecuteReader());
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
        public bool InsertShipInfo(SalesOrder_DTO orderInfo, Dictionary<string,decimal> lotNumList)
        {
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            try
            {
                string sql = @"UPDATE LOT_STS SET SHIP_FLAG = 'Y'
                                                , SHIP_CODE = 'DELIVERY'
                                                , SHIP_TIME = GETDATE()
                                                , LOT_DELETE_CODE = 'SHIP'
                                                , LOT_DELETE_FLAG = 'Y'
                                                , LOT_DELETE_TIME = GETDATE()
                                                , LAST_TRAN_CODE = 'SHIP'
                                                , LAST_TRAN_TIME = GETDATE()
                                                , LAST_TRAN_USER_ID = @LAST_TRAN_USER_ID
                               WHERE LOT_ID = @LOT_ID;

                               INSERT INTO SHIP_LOT_HIS (SALES_ORDER_ID, LOT_ID, SHIP_TIME, PRODUCT_CODE, SHIP_QTY, SHIP_USER_ID)
                               	   VALUES (@SALES_ORDER_ID, @LOT_ID, GETDATE(), @PRODUCT_CODE, @SHIP_QTY, @SHIP_USER_ID)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@SALES_ORDER_ID", orderInfo.SALES_ORDER_ID);
                cmd.Parameters.AddWithValue("@PRODUCT_CODE", orderInfo.PRODUCT_CODE);

                cmd.Parameters.Add("@LOT_ID",SqlDbType.VarChar);
                cmd.Parameters.Add("@LAST_TRAN_USER_ID", SqlDbType.VarChar);
                cmd.Parameters.Add("@SHIP_QTY", SqlDbType.Decimal);
                cmd.Parameters.Add("@SHIP_USER_ID", SqlDbType.VarChar);
                cmd.Transaction = trans;

                foreach (var item in lotNumList)
                {
                    cmd.Parameters["@LOT_ID"].Value = item.Key;
                    cmd.Parameters["@LAST_TRAN_USER_ID"].Value = "서지환";
                    cmd.Parameters["@SHIP_QTY"].Value = item.Value;
                    cmd.Parameters["@SHIP_USER_ID"].Value = "서지환";
                    cmd.ExecuteNonQuery();
                }

                cmd.Parameters.Clear();
                cmd.CommandText = @"UPDATE SALES_ORDER_MST SET SHIP_FLAG = 'Y'
                                                             , UPDATE_TIME = GETDATE()
                                                             , UPDATE_USER_ID = @UPDATE_USER_ID
                                    WHERE SALES_ORDER_ID = @SALES_ORDER_ID";
                cmd.Parameters.AddWithValue("@SALES_ORDER_ID", orderInfo.SALES_ORDER_ID);
                cmd.Parameters.AddWithValue("@UPDATE_USER_ID", "서지환");
                int iRowAffect = cmd.ExecuteNonQuery();


                trans.Commit();
                return iRowAffect>0;
            }
            catch (Exception err)
            {
                trans.Rollback();
                Debug.WriteLine(err.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
