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

      // ----------------------------------------- 진짜 너무 귀찮아서 CMD 재활용 전혀 안함 ----------------------------------------------------//
      public List<LOT_STS_DTO> SelectOrderLot(string orderId)
      {
         List<LOT_STS_DTO> list = null;
         try
         {
            SqlCommand cmd = new SqlCommand();
            string sql = @"SELECT 
                           L.LOT_ID, LOT_DESC, L.PRODUCT_CODE, L.OPERATION_CODE, O.OPERATION_NAME OPERATION_NAME, L.STORE_CODE, LOT_QTY, CREATE_QTY,
						         CASE WHEN (SELECT SUM(DEFECT_QTY) FROM LOT_DEFECT_HIS WHERE LOT_ID = L.LOT_ID) IS NULL THEN 0 
						         ELSE (SELECT SUM(DEFECT_QTY) FROM LOT_DEFECT_HIS WHERE LOT_ID = L.LOT_ID) END LOT_DEFECT_QTY,
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
                           LAST_TRAN_CODE IN ('CREATE' ,'END')
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
      public List<LOT_STS_DTO> SelectOrderLotEnd(string orderId)
      {
         List<LOT_STS_DTO> list = null;
         try
         {
            SqlCommand cmd = new SqlCommand();
            string sql = @"SELECT 
                           L.LOT_ID, LOT_DESC, L.PRODUCT_CODE, L.OPERATION_CODE, O.OPERATION_NAME OPERATION_NAME, L.STORE_CODE, LOT_QTY, CREATE_QTY,
						         CASE WHEN (SELECT SUM(DEFECT_QTY) FROM LOT_DEFECT_HIS WHERE LOT_ID = L.LOT_ID) IS NULL THEN 0 
						         ELSE (SELECT SUM(DEFECT_QTY) FROM LOT_DEFECT_HIS WHERE LOT_ID = L.LOT_ID) END LOT_DEFECT_QTY,
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
                           LAST_TRAN_CODE IN ('START', 'DEFECT', 'INSPECT', 'INPUT')
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
      public OPERATION_MST_DTO SelectOperation(string operation)
      {
         OPERATION_MST_DTO temp = null;
         try
         {
            SqlCommand cmd = new SqlCommand();
            string sql = @"SELECT 
                           OPERATION_CODE, OPERATION_NAME, CHECK_DEFECT_FLAG, CHECK_INSPECT_FLAG, CHECK_MATERIAL_FLAG, CREATE_TIME, CREATE_USER_ID, UPDATE_TIME, UPDATE_USER_ID
                           FROM OPERATION_MST
                           WHERE OPERATION_CODE = @OPERATION_CODE";
            cmd.Parameters.AddWithValue("@OPERATION_CODE", operation);
            cmd.CommandText = sql.ToString();
            cmd.Connection = conn;
            conn.Open();
            temp = Helper.DataReaderMapToDTO<OPERATION_MST_DTO>(cmd.ExecuteReader());
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
         return temp;
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
                           LOT_QTY = @LOT_QTY,
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
            cmd.Parameters.AddWithValue("@LOT_QTY", dto.LOT_QTY);
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
      public bool EndWork(LOT_STS_DTO dto, LOT_END_HIS_DTO end, bool finish)
      {
         conn.Open();
         SqlTransaction trans = conn.BeginTransaction();
         try
         {
            string sql = @"UPDATE LOT_STS
                           SET 
                           START_FLAG = NULL, 
                           START_QTY = NULL, 
                           START_TIME = NULL, 
                           START_EQUIPMENT_CODE = NULL,
                           END_FLAG = @END_FLAG,
						         END_TIME = @END_TIME,
						         END_EQUIPMENT_CODE = @END_EQUIPMENT_CODE,
						         OPER_IN_QTY = @OPER_IN_QTY,
						         OPER_IN_TIME = @OPER_IN_TIME,
						         OPERATION_CODE = @OPERATION_CODE,
                           LAST_TRAN_CODE = @LAST_TRAN_CODE,
                           LAST_TRAN_TIME = @LAST_TRAN_TIME,
                           LAST_TRAN_USER_ID = @LAST_TRAN_USER_ID,
                           LAST_TRAN_COMMENT = @LAST_TRAN_COMMENT, 
                           LAST_HIST_SEQ = @LAST_HIST_SEQ
                           WHERE 
                           LOT_ID = @LOT_ID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@LOT_ID", dto.LOT_ID);
            cmd.Parameters.AddWithValue("@END_FLAG", dto.END_FLAG);
            cmd.Parameters.AddWithValue("@END_TIME", dto.END_TIME);
            cmd.Parameters.AddWithValue("@END_EQUIPMENT_CODE", dto.END_EQUIPMENT_CODE);
            cmd.Parameters.AddWithValue("@OPER_IN_QTY", dto.OPER_IN_QTY);
            cmd.Parameters.AddWithValue("@OPER_IN_TIME", dto.OPER_IN_TIME);
            cmd.Parameters.AddWithValue("@OPERATION_CODE", string.IsNullOrWhiteSpace(dto.OPERATION_CODE) ? (object)DBNull.Value : dto.OPERATION_CODE);
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

            sql = @"INSERT INTO LOT_END_HIS
                   (LOT_ID, HIST_SEQ, TRAN_TIME, WORK_DATE, PRODUCT_CODE, OPERATION_CODE,
                   EQUIPMENT_CODE, TRAN_USER_ID, TRAN_COMMENT, TO_OPERATION_CODE, OPER_IN_QTY,
                   START_QTY, END_QTY, OPER_IN_TIME, START_TIME, PROC_TIME, WORK_ORDER_ID)
                   VALUES
                   (@LOT_ID, @HIST_SEQ, @TRAN_TIME, @WORK_DATE, @PRODUCT_CODE, @OPERATION_CODE,
                   @EQUIPMENT_CODE, @TRAN_USER_ID, @TRAN_COMMENT, @TO_OPERATION_CODE, @OPER_IN_QTY,
                   @START_QTY, @END_QTY, @OPER_IN_TIME, @START_TIME, @PROC_TIME, @WORK_ORDER_ID)";
            SqlCommand cmd3 = new SqlCommand(sql, conn);
            cmd3.Parameters.AddWithValue("@LOT_ID", end.LOT_ID);
            cmd3.Parameters.AddWithValue("@HIST_SEQ", end.HIST_SEQ);
            cmd3.Parameters.AddWithValue("@TRAN_TIME", end.TRAN_TIME);
            cmd3.Parameters.AddWithValue("@WORK_DATE", end.WORK_DATE);
            cmd3.Parameters.AddWithValue("@PRODUCT_CODE", end.PRODUCT_CODE);
            cmd3.Parameters.AddWithValue("@OPERATION_CODE", end.OPERATION_CODE);
            cmd3.Parameters.AddWithValue("@EQUIPMENT_CODE", end.EQUIPMENT_CODE);
            cmd3.Parameters.AddWithValue("@TRAN_USER_ID", end.TRAN_USER_ID);
            cmd3.Parameters.AddWithValue("@TRAN_COMMENT", end.TRAN_COMMENT);
            cmd3.Parameters.AddWithValue("@TO_OPERATION_CODE", string.IsNullOrWhiteSpace(end.TO_OPERATION_CODE) ? (object)DBNull.Value : end.TO_OPERATION_CODE);
            cmd3.Parameters.AddWithValue("@OPER_IN_QTY", end.OPER_IN_QTY);
            cmd3.Parameters.AddWithValue("@START_QTY", end.START_QTY);
            cmd3.Parameters.AddWithValue("@END_QTY", end.END_QTY);
            cmd3.Parameters.AddWithValue("@OPER_IN_TIME", end.OPER_IN_TIME);
            cmd3.Parameters.AddWithValue("@START_TIME", end.START_TIME);
            cmd3.Parameters.AddWithValue("@PROC_TIME", end.PROC_TIME);
            cmd3.Parameters.AddWithValue("@WORK_ORDER_ID", end.WORK_ORDER_ID);

            cmd3.Transaction = trans;
            cmd3.ExecuteNonQuery();

            if (finish)
            {
               dto.LAST_HIST_SEQ += 1;
               dto.LAST_TRAN_CODE = "MOVE";
               dto.PRODUCTION_TIME = DateTime.Now;
               dto.OPER_IN_TIME = DateTime.Now;
               dto.LAST_TRAN_COMMENT = "생산 제품 이동";
               sql = @"DECLARE @TYPE VARCHAR(30)
                       SET @TYPE = (SELECT PRODUCT_TYPE FROM PRODUCT_MST WHERE PRODUCT_CODE = @PRODUCT_CODE);
                       UPDATE LOT_STS
                       SET 
                       OPERATION_CODE = NULL,
                       STORE_CODE = (CASE WHEN (@TYPE = 'FERT') THEN 'ST_FERT' WHEN (@TYPE = 'HALB') THEN 'ST_HALB' WHEN (@TYPE = 'ROH') THEN 'ST_ROH' END),
                       START_QTY = NULL,
                       START_FLAG = NULL,
                       START_TIME = NULL,
                       START_EQUIPMENT_CODE= NULL,
                       END_FLAG = NULL,
                       END_TIME = NULL,
                       END_EQUIPMENT_CODE =NULL,
                       PRODUCTION_TIME = @PRODUCTION_TIME,
                       OPER_IN_TIME = @OPER_IN_TIME,
                       LAST_TRAN_CODE = @LAST_TRAN_CODE,
                       LAST_TRAN_TIME = @LAST_TRAN_TIME,
                       LAST_TRAN_USER_ID = @LAST_TRAN_USER_ID,
                       LAST_TRAN_COMMENT = @LAST_TRAN_COMMENT, 
                       LAST_HIST_SEQ = @LAST_HIST_SEQ
                       WHERE LOT_ID = @LOT_ID";
               SqlCommand cmd4 = new SqlCommand(sql, conn);
               cmd4.Transaction = trans;
               cmd4.Parameters.AddWithValue("@PRODUCT_CODE", dto.PRODUCT_CODE);
               cmd4.Parameters.AddWithValue("@PRODUCTION_TIME", DateTime.Now);
               cmd4.Parameters.AddWithValue("@OPER_IN_TIME", DateTime.Now);
               cmd4.Parameters.AddWithValue("@LOT_ID", dto.LOT_ID);
               cmd4.Parameters.AddWithValue("@LAST_TRAN_TIME", DateTime.Now);
               cmd4.Parameters.AddWithValue("@LAST_TRAN_CODE", "MOVE");
               cmd4.Parameters.AddWithValue("@LAST_TRAN_USER_ID", dto.LAST_TRAN_USER_ID);
               cmd4.Parameters.AddWithValue("@LAST_TRAN_COMMENT", "생산 제품 이동");
               cmd4.Parameters.AddWithValue("@LAST_HIST_SEQ", dto.LAST_HIST_SEQ);
               cmd4.ExecuteNonQuery();

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
               SqlCommand cmd5 = Helper.LotHisCmd(dto);
               cmd5.Connection = conn;
               cmd5.CommandText = sql;
               cmd5.Transaction = trans;
               cmd5.ExecuteNonQuery();

               /*sql = @"UPDATE WORK_ORDER_MST
                       SET 
                       ORDER_STATUS = 'CLOSE',
                       PRODUCT_QTY = @PRODUCT_QTY,
                       WORK_CLOSE_TIME = @WORK_CLOSE_TIME,
                       WORK_CLOSE_USER_ID = @WORK_CLOSE_USER_ID,
                       UPDATE_TIME = @UPDATE_TIME,
                       UPDATE_USER_ID = @UPDATE_USER_ID
                       WHERE 
                       WORK_ORDER_ID = @WORK_ORDER_ID";
               SqlCommand cmd6 = new SqlCommand(sql, conn);
               cmd6.Parameters.AddWithValue("@PRODUCT_QTY", dto.LOT_QTY);
               cmd6.Parameters.AddWithValue("@WORK_CLOSE_TIME", DateTime.Now);
               cmd6.Parameters.AddWithValue("@WORK_CLOSE_USER_ID", dto.LAST_TRAN_USER_ID);
               cmd6.Parameters.AddWithValue("@UPDATE_TIME", DateTime.Now);
               cmd6.Parameters.AddWithValue("@UPDATE_USER_ID", dto.LAST_TRAN_USER_ID);
               cmd6.Parameters.AddWithValue("@WORK_ORDER_ID", dto.WORK_ORDER_ID);
               cmd6.Transaction = trans;
               cmd6.ExecuteNonQuery();*/
            }

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
      public string EndWorkCondition(string lotId, string operation)
      {
         string workCondition = null;
         try
         {
            SqlCommand cmd = new SqlCommand();
            string sql = @"SP_GETLOTHIS";
            cmd.Parameters.AddWithValue("@LOT_ID", lotId);
            cmd.Parameters.AddWithValue("@OPERATION_CODE", operation);
            cmd.CommandText = sql.ToString();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            conn.Open();
            workCondition = cmd.ExecuteScalar().ToString();
            conn.Close();
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
         return workCondition;
      }
      public void Dispose()
      {
         if (conn != null || conn.State == ConnectionState.Open)
            conn.Close();
      }
   }
}
