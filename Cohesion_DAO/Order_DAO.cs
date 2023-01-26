using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using Cohesion_DTO;

namespace Cohesion_DAO
{
   public class Order_DAO : IDisposable
   {
      SqlConnection conn = null;
      readonly string DB = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
      public Order_DAO()
      {
         conn = new SqlConnection(DB);
      }

      public bool CreateLot(LOT_STS_DTO dto)
      {
         conn.Open();
         SqlTransaction trans = conn.BeginTransaction();
         try
         {
            string sql = @"INSERT INTO LOT_STS
                           (LOT_ID, LOT_DESC, PRODUCT_CODE, OPERATION_CODE
                           ,LOT_QTY, CREATE_QTY, OPER_IN_QTY, CREATE_TIME, OPER_IN_TIME, WORK_ORDER_ID
                           ,LAST_TRAN_CODE, LAST_TRAN_TIME, LAST_TRAN_USER_ID, LAST_TRAN_COMMENT, LAST_HIST_SEQ)
                           VALUES 
                           (@LOT_ID, @LOT_DESC, @PRODUCT_CODE, @OPERATION_CODE, @LOT_QTY, @CREATE_QTY, @OPER_IN_QTY, @CREATE_TIME, @OPER_IN_TIME, @WORK_ORDER_ID, 
                            @LAST_TRAN_CODE, @LAST_TRAN_TIME, @LAST_TRAN_USER_ID, @LAST_TRAN_COMMENT, @LAST_HIST_SEQ)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@LOT_ID", dto.LOT_ID);
            cmd.Parameters.AddWithValue("@LOT_DESC", dto.LOT_DESC);
            cmd.Parameters.AddWithValue("@PRODUCT_CODE", dto.PRODUCT_CODE);
            cmd.Parameters.AddWithValue("@OPERATION_CODE", dto.OPERATION_CODE);
            cmd.Parameters.AddWithValue("@LOT_QTY", dto.LOT_QTY);
            cmd.Parameters.AddWithValue("@CREATE_QTY", dto.CREATE_QTY);
            cmd.Parameters.AddWithValue("@OPER_IN_QTY", dto.OPER_IN_QTY);
            cmd.Parameters.AddWithValue("@CREATE_TIME", dto.CREATE_TIME);
            cmd.Parameters.AddWithValue("@OPER_IN_TIME", dto.OPER_IN_TIME);
            cmd.Parameters.AddWithValue("@WORK_ORDER_ID", dto.WORK_ORDER_ID);
            cmd.Parameters.AddWithValue("@LAST_TRAN_CODE", dto.LAST_TRAN_CODE);
            cmd.Parameters.AddWithValue("@LAST_TRAN_TIME", dto.LAST_TRAN_TIME);
            cmd.Parameters.AddWithValue("@LAST_TRAN_USER_ID", dto.LAST_TRAN_USER_ID);
            cmd.Parameters.AddWithValue("@LAST_TRAN_COMMENT", dto.LAST_TRAN_COMMENT);
            cmd.Parameters.AddWithValue("@LAST_HIST_SEQ", dto.LAST_HIST_SEQ);

            cmd.Transaction = trans;
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            LOT_HIS_DTO his = Helper.LotStsToLotHis(dto);
            sql = @"INSERT INTO LOT_HIS
                    (LOT_ID, LOT_DESC, PRODUCT_CODE, OPERATION_CODE, LOT_QTY, CREATE_QTY, OPER_IN_QTY, CREATE_TIME,
                    OPER_IN_TIME, WORK_ORDER_ID, TRAN_CODE, TRAN_TIME, TRAN_USER_ID, TRAN_COMMENT,
                    HIST_SEQ)
                    VALUES 
                    (@LOT_ID, @LOT_DESC, @PRODUCT_CODE, @OPERATION_CODE, @LOT_QTY, @CREATE_QTY, @OPER_IN_QTY, @CREATE_TIME,
                    @OPER_IN_TIME, @WORK_ORDER_ID, @TRAN_CODE, @TRAN_TIME, @TRAN_USER_ID, @TRAN_COMMENT,
                    @HIST_SEQ)";
            SqlCommand cmd2 = new SqlCommand(sql, conn);
            cmd2.Parameters.AddWithValue("@LOT_ID", his.LOT_ID);
            cmd2.Parameters.AddWithValue("@LOT_DESC", his.LOT_DESC);
            cmd2.Parameters.AddWithValue("@PRODUCT_CODE", his.PRODUCT_CODE);
            cmd2.Parameters.AddWithValue("@OPERATION_CODE", his.OPERATION_CODE);
            cmd2.Parameters.AddWithValue("@LOT_QTY", his.LOT_QTY);
            cmd2.Parameters.AddWithValue("@CREATE_QTY", his.CREATE_QTY);
            cmd2.Parameters.AddWithValue("@OPER_IN_QTY", his.OPER_IN_QTY);
            cmd2.Parameters.AddWithValue("@CREATE_TIME", his.CREATE_TIME);
            cmd2.Parameters.AddWithValue("@OPER_IN_TIME", his.OPER_IN_TIME);
            cmd2.Parameters.AddWithValue("@WORK_ORDER_ID", his.WORK_ORDER_ID);
            cmd2.Parameters.AddWithValue("@TRAN_CODE", his.TRAN_CODE);
            cmd2.Parameters.AddWithValue("@TRAN_TIME", his.TRAN_TIME);
            cmd2.Parameters.AddWithValue("@TRAN_USER_ID", his.TRAN_USER_ID);
            cmd2.Parameters.AddWithValue("@TRAN_COMMENT", his.TRAN_COMMENT);
            cmd2.Parameters.AddWithValue("@HIST_SEQ", his.HIST_SEQ);
            cmd2.Parameters.AddWithValue("@WORK_DATE", his.WORK_DATE);
            cmd2.Transaction = trans;
            cmd2.CommandText = sql;
            cmd2.ExecuteNonQuery();
            trans.Commit();
            return true;
         }
         catch (Exception err)
         {
            trans.Rollback();
            Debug.WriteLine(err.StackTrace);
            Debug.WriteLine(err.Message);
            return false;
         }
         finally
         {
            conn.Close();
         }
      }

      public List<WORK_ORDER_MST_DTO> SelectOrderList()
      {
         List<WORK_ORDER_MST_DTO> list = null;
         try
         {
            SqlCommand cmd = new SqlCommand();
            string sql = @"SELECT WORK_ORDER_ID
                                  ,ORDER_DATE
                           	   ,W.PRODUCT_CODE
                           	   ,P.PRODUCT_NAME PRODUCT_NAME
                           	   ,W.CUSTOMER_CODE
                           	   ,C.DATA_1 CUSTOMER_NAME
                           	   ,ORDER_QTY
                           	   ,ORDER_STATUS
                           	   ,PRODUCT_QTY
                           	   ,DEFECT_QTY
                           	   ,WORK_START_TIME
                           	   ,WORK_CLOSE_TIME
                           	   ,WORK_CLOSE_USER_ID
                           	   ,W.CREATE_TIME
                           	   ,W.CREATE_USER_ID
                           	   ,W.UPDATE_TIME
                           	   ,W.UPDATE_USER_ID
                                  FROM WORK_ORDER_MST W INNER JOIN CODE_DATA_MST C ON W.CUSTOMER_CODE = C.KEY_1
                                  INNER JOIN PRODUCT_MST P ON W.PRODUCT_CODE = P.PRODUCT_CODE 
                                  WHERE ORDER_STATUS <> 'CLOSE'";
            cmd.CommandText = sql.ToString();
            cmd.Connection = conn;
            conn.Open();
            list = Helper.DataReaderMapToList<WORK_ORDER_MST_DTO>(cmd.ExecuteReader());
         }
         catch (Exception err)
         {
            Debug.WriteLine(err.StackTrace);
            Debug.WriteLine(err.Message);
         }
         finally
         {
            conn.Close();
         }
         return list;
      }

      public PRODUCT_OPERATION_REL_DTO SelectOperation(string product_code)
      {
         List<PRODUCT_OPERATION_REL_DTO> list = null;
         try
         {
            SqlCommand cmd = new SqlCommand();
            string sql = @"SELECT O.PRODUCT_CODE, P.PRODUCT_NAME, O.OPERATION_CODE, M.OPERATION_NAME, FLOW_SEQ, O.CREATE_TIME, O.CREATE_USER_ID, O.UPDATE_TIME, O.UPDATE_USER_ID
                           FROM PRODUCT_OPERATION_REL O INNER JOIN PRODUCT_MST P ON O.PRODUCT_CODE = P.PRODUCT_CODE
                           							        INNER JOIN OPERATION_MST M ON O.OPERATION_CODE = M.OPERATION_CODE
                           WHERE O.PRODUCT_CODE = @PRODUCT_CODE AND FLOW_SEQ = 1";
            cmd.Parameters.AddWithValue("@PRODUCT_CODE", product_code);
            cmd.CommandText = sql.ToString();
            cmd.Connection = conn;
            conn.Open();
            list = Helper.DataReaderMapToList<PRODUCT_OPERATION_REL_DTO>(cmd.ExecuteReader());
         }
         catch (Exception err)
         {
            Debug.WriteLine(err.StackTrace);
            Debug.WriteLine(err.Message);
            return null;
         }
         finally
         {
            conn.Close();
         }
         if (list.Count < 1) return null;
         else return list[0];
      }

      public List<SalesOrder_DTO> SelectOrderListToShip()
      {
         try
         {
            string sql = @"SELECT SALES_ORDER_ID,
                                      ORDER_DATE,
                                      C.KEY_1 CUSTOMER_NAME,
                                      PRODUCT_CODE,
                                      ORDER_QTY,
                                      CONFIRM_FLAG,
                                      SHIP_FLAG
                               FROM   SALES_ORDER_MST S 
                               INNER JOIN CODE_DATA_MST C ON S.CUSTOMER_CODE = C.KEY_1 
                               INNER JOIN CODE_TABLE_MST T ON C.CODE_TABLE_NAME = T.CODE_TABLE_NAME
                               WHERE  SHIP_FLAG IS NULL
                                      AND CONFIRM_FLAG IS NOT NULL ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            var list = Helper.DataReaderMapToList<SalesOrder_DTO>(cmd.ExecuteReader());
            return list;
         }
         catch (Exception err)
         {
            Debug.WriteLine(err.StackTrace);
            Debug.WriteLine(err.Message);
            return null;
         }
         finally
         {
            conn.Close();
         }
      }
      public string SPGetLot(string orderId)
      {
         string lot = null;
         try
         {
            SqlCommand cmd = new SqlCommand();
            string sql = @"SP_GetLot";
            cmd.Parameters.AddWithValue("@ORDERID", orderId);
            cmd.CommandText = sql.ToString();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            conn.Open();
            lot = cmd.ExecuteScalar().ToString();
         }
         catch (Exception err)
         {
            Debug.WriteLine(err.StackTrace);
            Debug.WriteLine(err.Message);
            return null;
         }
         finally
         {
            conn.Close();
         }
         return lot;
      }

      public void Dispose()
      {
         if (conn != null || conn.State == ConnectionState.Open)
            conn.Close();
      }
   }
}
