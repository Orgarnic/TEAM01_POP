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
	                                , L.PRODUCT_CODE
									, P.PRODUCT_NAME
                                    , LOT_QTY
                                    , STORE_CODE
									, CDM.DATA_1 STORE_NAME
                                    , ISNULL(LOT_DELETE_FLAG,'N') LOT_DELETE_FLAG
	                                , LAST_TRAN_TIME
                                    , PRODUCTION_TIME
	                                , RANK()OVER(ORDER BY PRODUCTION_TIME ASC) DISPLAY_SEQ
                               FROM LOT_STS L
							   INNER JOIN PRODUCT_MST P ON L.PRODUCT_CODE = P.PRODUCT_CODE
							   INNER JOIN CODE_DATA_MST CDM ON L.STORE_CODE = CDM.KEY_1
                               WHERE LAST_TRAN_CODE = 'MOVE'
                                 AND L.PRODUCT_CODE = @PRODUCT_CODE
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
                //LOT_STS에서 해당 로트를 출고상태로 바꿔주고, LOT_HIS에 출고 기록, SHIP_LOT_HIS에도 출고 기록을 작성함
                //1개의 주문에 여러개의 LOT가 발생할 수 있으니 반복문을 수행
                string sql = @"UPDATE LOT_STS SET SHIP_FLAG = (CASE WHEN (@LOT_QTY - @SHIP_QTY) = 0 THEN 'Y' ELSE NULL END)
                                                , SHIP_CODE = (CASE WHEN (@LOT_QTY - @SHIP_QTY) = 0 THEN 'SHIP' ELSE NULL END)
                                                , SHIP_TIME = (CASE WHEN (@LOT_QTY - @SHIP_QTY) = 0 THEN GETDATE() ELSE NULL END)
                                                , LOT_DELETE_CODE = (CASE WHEN (@LOT_QTY - @SHIP_QTY) = 0 THEN 'SHIP' ELSE NULL END)
                                                , LOT_DELETE_FLAG = (CASE WHEN (@LOT_QTY - @SHIP_QTY) = 0 THEN 'Y' ELSE NULL END)
                                                , LOT_DELETE_TIME = (CASE WHEN (@LOT_QTY - @SHIP_QTY) = 0 THEN GETDATE() ELSE NULL END)
                                                , LAST_TRAN_CODE = (CASE WHEN (@LOT_QTY - @SHIP_QTY) = 0 THEN 'SHIP' ELSE NULL END)
                                                , LAST_TRAN_TIME = (CASE WHEN (@LOT_QTY - @SHIP_QTY) = 0 THEN GETDATE() ELSE NULL END)
                                                , LAST_TRAN_USER_ID = @LAST_TRAN_USER_ID
                                                , LAST_HIST_SEQ = (SELECT LAST_HIST_SEQ+1 FROM LOT_STS WHERE LOT_ID = @LOT_ID)
                               WHERE LOT_ID = @LOT_ID;
                
                
                               INSERT INTO LOT_HIS
                               SELECT TOP(1) LOT_ID
                                           , HIST_SEQ+1 HIST_SEQ
                                           , GETDATE() TRAN_TIME
                                           , 'SHIP' TRAN_CODE
                                           , LOT_DESC
                                           , PRODUCT_CODE
                                           , null OPERATION_CODE
                                           , STORE_CODE
                                           , (LOT_QTY - @SHIP_QTY) LOT_QTY
                                           , CREATE_QTY
                                           , OPER_IN_QTY
                                           , null START_FLAG, null START_QTY, null START_TIME, null START_EQUIPMENT_CODE, null END_FLAG, null END_TIME, null END_EQUIPMENT_CODE
                                           , CASE WHEN (LOT_QTY - @SHIP_QTY) = 0 THEN 'Y' ELSE NULL END SHIP_FLAG
                                           , CASE WHEN (LOT_QTY - @SHIP_QTY) = 0 THEN 'TO_CUSTOMER' ELSE NULL END SHIP_CODE
                                           , CASE WHEN (LOT_QTY - @SHIP_QTY) = 0 THEN  GETDATE() ELSE NULL END SHIP_TIME
                                           , PRODUCTION_TIME
                                           , CREATE_TIME
                                           , OPER_IN_TIME
                                           , WORK_ORDER_ID
                                           , CASE WHEN (LOT_QTY - @SHIP_QTY) = 0 THEN 'Y' ELSE NULL END LOT_DELETE_FLAG
                                           , CASE WHEN (LOT_QTY - @SHIP_QTY) = 0 THEN 'SHIP' ELSE NULL END LOT_DELETE_CODE
                                           , CASE WHEN (LOT_QTY - @SHIP_QTY) = 0 THEN GETDATE() ELSE NULL END LOT_DELETE_TIME
                                           , Convert(varchar(8),getdate(),112) WORK_DATE
                                           , @LAST_TRAN_USER_ID TRAN_USER_ID
                                           , CASE WHEN (LOT_QTY - @SHIP_QTY) = 0 THEN '생산 제품 출고' ELSE NULL END TRAN_COMMENT
                                           , PRODUCT_CODE OLD_PRODUCT_CODE
                                           , OPERATION_CODE OLD_OPERATION_CODE
                                           , STORE_CODE OLD_STORE_CODE
                                           , LOT_QTY OLD_LOT_QTY
                               FROM LOT_HIS
                               where LOT_ID = @LOT_ID
                               Order by HIST_SEQ DESC;


                               INSERT INTO SHIP_LOT_HIS (SALES_ORDER_ID, LOT_ID, SHIP_TIME, PRODUCT_CODE, SHIP_QTY, SHIP_USER_ID)
                               	   VALUES (@SALES_ORDER_ID, @LOT_ID, GETDATE(), @PRODUCT_CODE, @SHIP_QTY, @LAST_TRAN_USER_ID)";


                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@SALES_ORDER_ID", orderInfo.SALES_ORDER_ID);
                cmd.Parameters.AddWithValue("@PRODUCT_CODE", orderInfo.PRODUCT_CODE);

                cmd.Parameters.Add("@LOT_ID",SqlDbType.VarChar);
                cmd.Parameters.Add("@LOT_QTY", SqlDbType.Decimal);
                cmd.Parameters.Add("@LAST_TRAN_USER_ID", SqlDbType.VarChar);
                cmd.Parameters.Add("@SHIP_QTY", SqlDbType.Decimal);
                cmd.Transaction = trans;

                decimal reqQty = orderInfo.ORDER_QTY;

                foreach (var item in lotNumList)
                {
                    cmd.Parameters["@LOT_ID"].Value = item.Key;
                    cmd.Parameters["@LOT_QTY"].Value = item.Value;
                    cmd.Parameters["@LAST_TRAN_USER_ID"].Value = "서지환";
                    cmd.Parameters["@SHIP_QTY"].Value = (item.Value < reqQty)? item.Value : reqQty;
                    if (item.Value < reqQty)
                    {
                        cmd.ExecuteNonQuery();
                        reqQty -= item.Value;
                    }
                    if (Convert.ToDecimal(cmd.Parameters["@SHIP_QTY"].Value) == reqQty)
                    {
                        cmd.ExecuteNonQuery();
                        break;
                    }

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
