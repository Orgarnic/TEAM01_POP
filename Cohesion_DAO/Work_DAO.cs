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
   public class Work_DAO
   {
      SqlConnection conn = null;
      readonly string DB = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
      public Work_DAO()
      {
         conn = new SqlConnection(DB);
      }

      public List<LOT_STS_DTO> SelectOrderLot(string orderId)
      {
         List<LOT_STS_DTO> list = null;
         try
         {
            SqlCommand cmd = new SqlCommand();
            string sql = @"SELECT 
                           LOT_ID, LOT_DESC, L.PRODUCT_CODE, L.OPERATION_CODE, O.OPERATION_NAME OPERATION_NAME, STORE_CODE, LOT_QTY, CREATE_QTY,
                           OPER_IN_QTY, START_FLAG, START_QTY, START_TIME, START_EQUIPMENT_CODE, END_FLAG,
                           END_TIME, END_EQUIPMENT_CODE, SHIP_FLAG, SHIP_CODE, SHIP_TIME, PRODUCTION_TIME,
                           L.CREATE_TIME, OPER_IN_TIME, L.WORK_ORDER_ID, LOT_DELETE_FLAG, LOT_DELETE_CODE, LOT_DELETE_TIME,
                           LAST_TRAN_CODE, LAST_TRAN_TIME, LAST_TRAN_USER_ID, LAST_TRAN_COMMENT, LAST_HIST_SEQ
                           FROM 
                           LOT_STS L INNER JOIN PRODUCT_OPERATION_REL P ON L.PRODUCT_CODE = P.PRODUCT_CODE AND L.OPERATION_CODE = P.OPERATION_CODE
                           		    INNER JOIN OPERATION_MST O ON L.OPERATION_CODE = O.OPERATION_CODE
                                     INNER JOIN WORK_ORDER_MST W ON W.WORK_ORDER_ID = L.WORK_ORDER_ID
                           WHERE 
                           (LAST_TRAN_CODE = 'END' AND 
                           P.FLOW_SEQ < 
                           (SELECT 
                           MAX(FLOW_SEQ)
                           FROM PRODUCT_OPERATION_REL
                           WHERE PRODUCT_CODE = L.PRODUCT_CODE AND OPERATION_CODE = L.OPERATION_CODE)) OR
                           LAST_TRAN_CODE = 'CREATE'
                           AND LOT_DELETE_FLAG IS NULL
						         AND W.ORDER_STATUS <> 'CLOSE'
                           AND L.WORK_ORDER_ID = @WORK_ORDER_ID";
            cmd.Parameters.AddWithValue("@WORK_ORDER_ID", orderId);
            cmd.CommandText = sql.ToString();
            cmd.Connection = conn;
            conn.Open();
            list = Helper.DataReaderMapToList<LOT_STS_DTO>(cmd.ExecuteReader());
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
         return list;
      }

      public List<PRODUCT_OPERATION_REL_DTO> SelectOperations()
      {
         List<PRODUCT_OPERATION_REL_DTO> list = null;
         try
         {
            SqlCommand cmd = new SqlCommand();
            string sql = @"SELECT PRODUCT_CODE, P.OPERATION_CODE, O.OPERATION_NAME OPERATION_NAME, FLOW_SEQ, P.CREATE_TIME, P.CREATE_USER_ID, P.UPDATE_TIME, P.UPDATE_USER_ID
                           FROM PRODUCT_OPERATION_REL P INNER JOIN OPERATION_MST O ON P.OPERATION_CODE = O.OPERATION_CODE";
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
         return list;
      }

      public List<EQUIPMENT_OPERATION_REL_DTO> SelectEquipments()
      {
         List<EQUIPMENT_OPERATION_REL_DTO> list = null;
         try
         {
            SqlCommand cmd = new SqlCommand();
            string sql = @"SELECT E.OPERATION_CODE, E.EQUIPMENT_CODE, O.EQUIPMENT_NAME, E.CREATE_TIME, E.CREATE_USER_ID, E.UPDATE_TIME, E.UPDATE_USER_ID
                           FROM EQUIPMENT_OPERATION_REL E INNER JOIN EQUIPMENT_MST O ON E.EQUIPMENT_CODE = O.EQUIPMENT_CODE";
            cmd.CommandText = sql.ToString();
            cmd.Connection = conn;
            conn.Open();
            list = Helper.DataReaderMapToList<EQUIPMENT_OPERATION_REL_DTO>(cmd.ExecuteReader());
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
         return list;
      }

      public bool StartWork(LOT_STS_DTO dto)
      {
         conn.Open();
         SqlTransaction trans = conn.BeginTransaction();
         try
         {
            string sql = @"UPDATE LOT_STS
                           SET 
                           START_FLAG = @START_FLAG, 
                           START_QTY = @START_QTY, 
                           START_TIME = @START_TIME, 
                           START_EQUIPMENT_CODE = @START_EQUIPMENT_CODE,
                           LAST_TRAN_CODE = @LAST_TRAN_CODE,
                           LAST_TRAN_TIME = @LAST_TRAN_TIME,
                           LAST_TRAN_USER_ID = @LAST_TRAN_USER_ID,
                           LAST_TRAN_COMMENT = @LAST_TRAN_COMMENT, 
                           LAST_HIST_SEQ = @LAST_HIST_SEQ
                           WHERE 
                           LOT_ID = @LOT_ID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@LOT_ID", dto.LOT_ID);
            cmd.Parameters.AddWithValue("@START_FLAG", dto.START_FLAG);
            cmd.Parameters.AddWithValue("@START_QTY", dto.START_QTY);
            cmd.Parameters.AddWithValue("@START_TIME", dto.START_TIME);
            cmd.Parameters.AddWithValue("@START_EQUIPMENT_CODE", dto.START_EQUIPMENT_CODE);
            cmd.Parameters.AddWithValue("@LAST_TRAN_CODE", dto.LAST_TRAN_CODE);
            cmd.Parameters.AddWithValue("@LAST_TRAN_TIME", dto.LAST_TRAN_TIME);
            cmd.Parameters.AddWithValue("@LAST_TRAN_USER_ID", dto.LAST_TRAN_USER_ID);
            cmd.Parameters.AddWithValue("@LAST_TRAN_COMMENT", dto.LAST_TRAN_COMMENT);
            cmd.Parameters.AddWithValue("@LAST_HIST_SEQ", dto.LAST_HIST_SEQ);

            cmd.Transaction = trans;
            cmd.ExecuteNonQuery();
            sql = @"INSERT INTO LOT_HIS
                    (
                    LOT_ID, HIST_SEQ, TRAN_TIME, TRAN_CODE, LOT_DESC,
                    PRODUCT_CODE, OPERATION_CODE, STORE_CODE, LOT_QTY,
                    CREATE_QTY, OPER_IN_QTY, START_FLAG, START_QTY, 
                    START_TIME, START_EQUIPMENT_CODE, END_FLAG, END_TIME,
                    END_EQUIPMENT_CODE, SHIP_FLAG, SHIP_CODE, SHIP_TIME, 
                    PRODUCTION_TIME, CREATE_TIME, OPER_IN_TIME, WORK_ORDER_ID,
                    LOT_DELETE_FLAG, LOT_DELETE_CODE, LOT_DELETE_TIME, WORK_DATE,
                    TRAN_USER_ID, TRAN_COMMENT, OLD_PRODUCT_CODE, OLD_OPERATION_CODE,
                    OLD_STORE_CODE, OLD_LOT_QTY
                    ) 
                    VALUES 
                    (
                    @LOT_ID, @HIST_SEQ, @TRAN_TIME, @TRAN_CODE, @LOT_DESC, 
                    @PRODUCT_CODE, @OPERATION_CODE, @STORE_CODE, @LOT_QTY, 
                    @CREATE_QTY, @OPER_IN_QTY, @START_FLAG, @START_QTY, 
                    @START_TIME, @START_EQUIPMENT_CODE, @END_FLAG, @END_TIME, 
                    @END_EQUIPMENT_CODE, @SHIP_FLAG, @SHIP_CODE, @SHIP_TIME, 
                    @PRODUCTION_TIME, @CREATE_TIME, @OPER_IN_TIME, @WORK_ORDER_ID,
                    @LOT_DELETE_FLAG, @LOT_DELETE_CODE, @LOT_DELETE_TIME, @WORK_DATE, 
                    @TRAN_USER_ID, @TRAN_COMMENT, @OLD_PRODUCT_CODE, @OLD_OPERATION_CODE,
                    @OLD_STORE_CODE, @OLD_LOT_QTY
                    )";
            SqlCommand cmd2 = Helper.LotHisCmd(dto);
            cmd2.Connection = conn;
            cmd2.CommandText = sql;
            cmd2.Transaction = trans;
            cmd2.ExecuteNonQuery();

            sql = @"UPDATE WORK_ORDER_MST
                                SET ORDER_STATUS = 'PROC', UPDATE_TIME = GETDATE(), UPDATE_USER_ID = @UPDATE_USER_ID
                                WHERE WORK_ORDER_ID = @WORK_ORDER_ID";
            SqlCommand cmd3 = new SqlCommand(sql, conn);
            cmd3.Parameters.AddWithValue("@UPDATE_USER_ID", dto.LAST_TRAN_USER_ID);
            cmd3.Parameters.AddWithValue("@WORK_ORDER_ID", dto.WORK_ORDER_ID);
            cmd3.Transaction = trans;
            cmd3.ExecuteNonQuery();

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

      public void Dispose()
      {
         if (conn != null || conn.State == ConnectionState.Open)
            conn.Close();
      }
   }
}
