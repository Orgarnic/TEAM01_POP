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
   public class Flag_DAO : IDisposable
   {
      SqlConnection conn = null;
      readonly string DB = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

      // ----------------------------------------- 진짜 너무 귀찮아서 CMD 재활용 전혀 안함 ----------------------------------------------------//
      public Flag_DAO()
      {
         conn = new SqlConnection(DB);
      }
      public List<LOT_STS_DTO> SelectOrderLotBed(string orderId)
      {
         List<LOT_STS_DTO> list = null;
         try
         {
            SqlCommand cmd = new SqlCommand();
            string sql = @"SELECT 
                           LOT_ID, LOT_DESC, L.PRODUCT_CODE, L.OPERATION_CODE, O.OPERATION_NAME OPERATION_NAME, STORE_CODE, LOT_QTY, CREATE_QTY,
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
                           LAST_TRAN_CODE in ('START', 'DEFECT', 'INSPECT', 'INPUT') 
						         AND LOT_DELETE_FLAG IS NULL
						         AND W.ORDER_STATUS <> 'CLOSE'
                           AND L.WORK_ORDER_ID = @WORK_ORDER_ID
                           AND O.CHECK_DEFECT_FLAG = 'Y'";
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
      public List<LOT_STS_DTO> SelectOrderLotInspect(string orderId)
      {
         List<LOT_STS_DTO> list = null;
         try
         {
            SqlCommand cmd = new SqlCommand();
            string sql = @"SELECT 
                           LOT_ID, LOT_DESC, L.PRODUCT_CODE, L.OPERATION_CODE, O.OPERATION_NAME OPERATION_NAME, STORE_CODE, LOT_QTY, CREATE_QTY,
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
                           LAST_TRAN_CODE in ('START', 'DEFECT', 'INSPECT', 'INPUT') 
						         AND LOT_DELETE_FLAG IS NULL
						         AND W.ORDER_STATUS <> 'CLOSE'
                           AND L.WORK_ORDER_ID = @WORK_ORDER_ID
                           AND O.CHECK_INSPECT_FLAG = 'Y'";
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
      public List<CODE_DATA_MST_DTO> SelectBedCodes()
      {
         List<CODE_DATA_MST_DTO> list = null;
         try
         {
            SqlCommand cmd = new SqlCommand();
            string sql = @"SELECT CODE_TABLE_NAME, KEY_1, KEY_2, KEY_3, DATA_1, DATA_2, DATA_3, DATA_4, DATA_5, DISPLAY_SEQ, CREATE_TIME, CREATE_USER_ID, UPDATE_TIME, UPDATE_USER_ID
                           FROM CODE_DATA_MST
                           WHERE CODE_TABLE_NAME = 'CM_DEFECT_CODE' ";
            cmd.CommandText = sql.ToString();
            cmd.Connection = conn;
            conn.Open();
            list = Helper.DataReaderMapToList<CODE_DATA_MST_DTO>(cmd.ExecuteReader());
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
      public List<INSPECT_ITEM_MST_DTO> SelectInspects(string operation)
      {
         List<INSPECT_ITEM_MST_DTO> list = null;
         try
         {
            SqlCommand cmd = new SqlCommand();
            string sql = @"SELECT I.INSPECT_ITEM_CODE, INSPECT_ITEM_NAME, VALUE_TYPE, SPEC_LSL, SPEC_TARGET, SPEC_USL, I.CREATE_TIME, I.CREATE_USER_ID, I.UPDATE_TIME, I.UPDATE_USER_ID
                           FROM INSPECT_ITEM_MST I INNER JOIN INSPECT_ITEM_OPERATION_REL O ON I.INSPECT_ITEM_CODE = O.INSPECT_ITEM_CODE
                           WHERE O.OPERATION_CODE = @OPERATION_CODE";
            cmd.Parameters.AddWithValue("@OPERATION_CODE", operation);
            cmd.CommandText = sql.ToString();
            cmd.Connection = conn;
            conn.Open();
            list = Helper.DataReaderMapToList<INSPECT_ITEM_MST_DTO>(cmd.ExecuteReader());
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
      public List<LOT_STS_DTO> SelectLotMateriars(string prodId, string operation)
      {
         List<LOT_STS_DTO> list = null;
         try
         {
            SqlCommand cmd = new SqlCommand();
            string sql = @"SELECT 
                           LOT_ID, LOT_DESC, L.PRODUCT_CODE, P.PRODUCT_NAME, B.REQUIRE_QTY, B.ALTER_PRODUCT_CODE, B.OPERATION_CODE, STORE_CODE, LOT_QTY, CREATE_QTY,
                           OPER_IN_QTY, START_FLAG, START_QTY, START_TIME, START_EQUIPMENT_CODE, END_FLAG,
                           END_TIME, END_EQUIPMENT_CODE, SHIP_FLAG, SHIP_CODE, SHIP_TIME, PRODUCTION_TIME,
                           L.CREATE_TIME, OPER_IN_TIME, WORK_ORDER_ID, LOT_DELETE_FLAG, LOT_DELETE_CODE,
                           LOT_DELETE_TIME, LAST_TRAN_CODE, LAST_TRAN_TIME, LAST_TRAN_USER_ID, LAST_TRAN_COMMENT,
                           LAST_HIST_SEQ
                           FROM 
                           LOT_STS L INNER JOIN (SELECT CHILD_PRODUCT_CODE, REQUIRE_QTY, ALTER_PRODUCT_CODE, OPERATION_CODE FROM BOM_MST WHERE PRODUCT_CODE = @PRODUCT_CODE) B ON L.PRODUCT_CODE = B.CHILD_PRODUCT_CODE
                           		     INNER JOIN PRODUCT_MST P ON P.PRODUCT_CODE = B.CHILD_PRODUCT_CODE
						         WHERE B.OPERATION_CODE = @OPERATION_CODE
                           AND L.LOT_QTY > 0 AND L.LOT_DELETE_FLAG IS NULL";
            cmd.Parameters.AddWithValue("@PRODUCT_CODE", prodId);
            cmd.Parameters.AddWithValue("@OPERATION_CODE", operation);
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
      public List<LOT_STS_DTO> SelectMateriarLot(string lots)
      {
         List<LOT_STS_DTO> list = null;
         try
         {
            SqlCommand cmd = new SqlCommand();
            string sql = $@"SELECT 
                           LOT_ID, LOT_DESC, L.PRODUCT_CODE, L.OPERATION_CODE, STORE_CODE, LOT_QTY, CREATE_QTY
                           OPER_IN_QTY, START_FLAG, START_QTY, START_TIME, START_EQUIPMENT_CODE, END_FLAG,
                           END_TIME, END_EQUIPMENT_CODE, SHIP_FLAG, SHIP_CODE, SHIP_TIME, PRODUCTION_TIME,
                           L.CREATE_TIME, OPER_IN_TIME, L.WORK_ORDER_ID, LOT_DELETE_FLAG, LOT_DELETE_CODE, LOT_DELETE_TIME,
                           LAST_TRAN_CODE, LAST_TRAN_TIME, LAST_TRAN_USER_ID, LAST_TRAN_COMMENT, LAST_HIST_SEQ
                           FROM 
                           LOT_STS L
                           WHERE 
                              LAST_TRAN_CODE in ('CREATE', 'MOVE', 'INPUT') 
						            AND LOT_DELETE_FLAG IS NULL
								      AND STORE_CODE IS NOT NULL
								      AND LOT_ID IN ({lots})";
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
      public bool BedRegCheck(string operation)
      {
         try
         {
            SqlCommand cmd = new SqlCommand();
            string sql = @"SELECT COUNT(*)
                           FROM OPERATION_MST
                           WHERE OPERATION_CODE = @OPERATION_CODE AND CHECK_DEFECT_FLAG = 'Y'";
            cmd.Parameters.AddWithValue("@OPERATION_CODE", operation);
            cmd.CommandText = sql.ToString();
            cmd.Connection = conn;
            conn.Open();
            int temp = Convert.ToInt32(cmd.ExecuteScalar());

            return temp > 0;
         }
         catch (Exception err)
         {
            Debug.WriteLine(err.StackTrace);
            Debug.WriteLine(err.Message);
            return false;
         }
         finally
         {
            conn.Close();
         }
      }
      public bool InsertBedReg(LOT_STS_DTO dto, List<LOT_DEFECT_HIS_DTO> defects)
      {
         conn.Open();
         SqlTransaction trans = conn.BeginTransaction();
         try
         {
            string sql = @"UPDATE LOT_STS
                           SET 
                           LOT_QTY = @LOT_QTY,
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

            sql = @"INSERT INTO LOT_DEFECT_HIS
                    (LOT_ID, HIST_SEQ, DEFECT_CODE, DEFECT_QTY, TRAN_TIME, WORK_DATE, 
                     PRODUCT_CODE, OPERATION_CODE, STORE_CODE, EQUIPMENT_CODE, TRAN_USER_ID, TRAN_COMMENT)
                    VALUES 
                    (@LOT_ID, @HIST_SEQ, @DEFECT_CODE, @DEFECT_QTY, @TRAN_TIME, @WORK_DATE,
                     @PRODUCT_CODE, @OPERATION_CODE, @STORE_CODE, @EQUIPMENT_CODE, @TRAN_USER_ID, @TRAN_COMMENT)";
            SqlCommand cmd3 = new SqlCommand(sql, conn);
            cmd3.Transaction = trans;
            cmd3.Parameters.AddWithValue("@LOT_ID", dto.LOT_ID);
            cmd3.Parameters.AddWithValue("@HIST_SEQ", dto.LAST_HIST_SEQ);
            cmd3.Parameters.Add(new SqlParameter("@DEFECT_CODE", SqlDbType.VarChar));
            cmd3.Parameters.Add(new SqlParameter("@DEFECT_QTY", SqlDbType.Decimal));
            cmd3.Parameters.AddWithValue("@TRAN_TIME", dto.LAST_TRAN_TIME);
            cmd3.Parameters.AddWithValue("@WORK_DATE", DateTime.Now.ToString("yyyyMMdd"));
            cmd3.Parameters.AddWithValue("@PRODUCT_CODE", string.IsNullOrWhiteSpace(dto.PRODUCT_CODE) ? (object)DBNull.Value : dto.PRODUCT_CODE);
            cmd3.Parameters.AddWithValue("@OPERATION_CODE", string.IsNullOrWhiteSpace(dto.OPERATION_CODE) ? (object)DBNull.Value : dto.OPERATION_CODE);
            cmd3.Parameters.AddWithValue("@STORE_CODE", string.IsNullOrWhiteSpace(dto.STORE_CODE) ? (object)DBNull.Value : dto.STORE_CODE);
            cmd3.Parameters.Add(new SqlParameter("@EQUIPMENT_CODE", SqlDbType.VarChar));
            cmd3.Parameters.AddWithValue("@TRAN_USER_ID", string.IsNullOrWhiteSpace(dto.LAST_TRAN_USER_ID) ? (object)DBNull.Value : dto.LAST_TRAN_USER_ID);
            cmd3.Parameters.AddWithValue("@TRAN_COMMENT", string.IsNullOrWhiteSpace(dto.LAST_TRAN_COMMENT) ? (object)DBNull.Value : dto.LAST_TRAN_COMMENT);
            foreach (var defect in defects)
            {
               cmd3.Parameters["@DEFECT_CODE"].Value = defect.DEFECT_CODE;
               cmd3.Parameters["@DEFECT_QTY"].Value = defect.DEFECT_QTY;
               cmd3.Parameters["@EQUIPMENT_CODE"].Value = string.IsNullOrWhiteSpace(defect.EQUIPMENT_CODE) ? (object)DBNull.Value : defect.EQUIPMENT_CODE;
               cmd3.ExecuteNonQuery();
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
      public bool InsertInspect(LOT_STS_DTO dto, List<LOT_INSPECT_HIS_DTO> inspects)
      {
         conn.Open();
         SqlTransaction trans = conn.BeginTransaction();
         try
         {
            string sql = @"UPDATE LOT_STS
                           SET 
                           LAST_TRAN_CODE = @LAST_TRAN_CODE,
                           LAST_TRAN_TIME = @LAST_TRAN_TIME,
                           LAST_TRAN_USER_ID = @LAST_TRAN_USER_ID,
                           LAST_TRAN_COMMENT = @LAST_TRAN_COMMENT, 
                           LAST_HIST_SEQ = @LAST_HIST_SEQ
                           WHERE 
                           LOT_ID = @LOT_ID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@LOT_ID", dto.LOT_ID);
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

            sql = @"INSERT INTO LOT_INSPECT_HIS
                        (LOT_ID, HIST_SEQ, INSPECT_ITEM_CODE, INSPECT_ITEM_NAME, VALUE_TYPE, SPEC_LSL, SPEC_TARGET, SPEC_USL,
					         INSPECT_VALUE, INSPECT_RESULT, TRAN_TIME, WORK_DATE, PRODUCT_CODE, OPERATION_CODE, STORE_CODE,
					         EQUIPMENT_CODE, TRAN_USER_ID, TRAN_COMMENT)
                   VALUES 
                       (@LOT_ID, @HIST_SEQ, @INSPECT_ITEM_CODE, @INSPECT_ITEM_NAME, @VALUE_TYPE, @SPEC_LSL, @SPEC_TARGET, 
					        @SPEC_USL, @INSPECT_VALUE, @INSPECT_RESULT, @TRAN_TIME, @WORK_DATE, @PRODUCT_CODE, @OPERATION_CODE, 
					        @STORE_CODE, @EQUIPMENT_CODE, @TRAN_USER_ID, @TRAN_COMMENT)";
            SqlCommand cmd3 = new SqlCommand(sql, conn);
            cmd3.Transaction = trans;
            cmd3.Parameters.AddWithValue("@LOT_ID", dto.LOT_ID);
            cmd3.Parameters.AddWithValue("@HIST_SEQ", dto.LAST_HIST_SEQ);
            cmd3.Parameters.Add(new SqlParameter("@INSPECT_ITEM_CODE", SqlDbType.VarChar));
            cmd3.Parameters.Add(new SqlParameter("@INSPECT_ITEM_NAME", SqlDbType.VarChar));
            cmd3.Parameters.Add(new SqlParameter("@VALUE_TYPE", SqlDbType.Char));
            cmd3.Parameters.Add(new SqlParameter("@SPEC_LSL", SqlDbType.VarChar));
            cmd3.Parameters.Add(new SqlParameter("@SPEC_TARGET", SqlDbType.VarChar));
            cmd3.Parameters.Add(new SqlParameter("@SPEC_USL", SqlDbType.VarChar));
            cmd3.Parameters.Add(new SqlParameter("@INSPECT_VALUE", SqlDbType.VarChar));
            cmd3.Parameters.Add(new SqlParameter("@INSPECT_RESULT", SqlDbType.VarChar));
            cmd3.Parameters.AddWithValue("@TRAN_TIME", dto.LAST_TRAN_TIME);
            cmd3.Parameters.AddWithValue("@WORK_DATE", DateTime.Now.ToString("yyyyMMdd"));
            cmd3.Parameters.AddWithValue("@PRODUCT_CODE", string.IsNullOrWhiteSpace(dto.PRODUCT_CODE) ? (object)DBNull.Value : dto.PRODUCT_CODE);
            cmd3.Parameters.AddWithValue("@OPERATION_CODE", string.IsNullOrWhiteSpace(dto.OPERATION_CODE) ? (object)DBNull.Value : dto.OPERATION_CODE);
            cmd3.Parameters.AddWithValue("@STORE_CODE", string.IsNullOrWhiteSpace(dto.STORE_CODE) ? (object)DBNull.Value : dto.STORE_CODE);
            cmd3.Parameters.Add(new SqlParameter("@EQUIPMENT_CODE", SqlDbType.VarChar));
            cmd3.Parameters.AddWithValue("@TRAN_USER_ID", string.IsNullOrWhiteSpace(dto.LAST_TRAN_USER_ID) ? (object)DBNull.Value : dto.LAST_TRAN_USER_ID);
            cmd3.Parameters.AddWithValue("@TRAN_COMMENT", string.IsNullOrWhiteSpace(dto.LAST_TRAN_COMMENT) ? (object)DBNull.Value : dto.LAST_TRAN_COMMENT);
            foreach (var inspect in inspects)
            {
               cmd3.Parameters["@INSPECT_ITEM_CODE"].Value = string.IsNullOrWhiteSpace(inspect.INSPECT_ITEM_CODE) ? (object)DBNull.Value : inspect.INSPECT_ITEM_CODE;
               cmd3.Parameters["@INSPECT_ITEM_NAME"].Value = string.IsNullOrWhiteSpace(inspect.INSPECT_ITEM_NAME) ? (object)DBNull.Value : inspect.INSPECT_ITEM_NAME;
               cmd3.Parameters["@VALUE_TYPE"].Value = inspect.VALUE_TYPE == '\0' ? (object)DBNull.Value : inspect.VALUE_TYPE;
               cmd3.Parameters["@SPEC_LSL"].Value = string.IsNullOrWhiteSpace(inspect.SPEC_LSL) ? (object)DBNull.Value : inspect.SPEC_LSL;
               cmd3.Parameters["@SPEC_TARGET"].Value = string.IsNullOrWhiteSpace(inspect.SPEC_TARGET) ? (object)DBNull.Value : inspect.SPEC_TARGET;
               cmd3.Parameters["@SPEC_USL"].Value = string.IsNullOrWhiteSpace(inspect.SPEC_USL) ? (object)DBNull.Value : inspect.SPEC_USL;
               cmd3.Parameters["@INSPECT_VALUE"].Value = string.IsNullOrWhiteSpace(inspect.INSPECT_VALUE) ? (object)DBNull.Value : inspect.INSPECT_VALUE;
               cmd3.Parameters["@INSPECT_RESULT"].Value = string.IsNullOrWhiteSpace(inspect.INSPECT_RESULT) ? (object)DBNull.Value : inspect.INSPECT_RESULT;
               cmd3.Parameters["@EQUIPMENT_CODE"].Value = string.IsNullOrWhiteSpace(inspect.EQUIPMENT_CODE) ? (object)DBNull.Value : inspect.EQUIPMENT_CODE;
               cmd3.ExecuteNonQuery();
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
      public bool InsertMateriar(LOT_STS_DTO dto, List<LOT_STS_DTO> dto2, List<LOT_MATERIAL_HIS_DTO> materiars)
      {
         conn.Open();
         SqlTransaction trans = conn.BeginTransaction();
         try
         {
            string sql = @"UPDATE LOT_STS
                           SET 
                           LAST_TRAN_CODE = @LAST_TRAN_CODE,
                           LAST_TRAN_TIME = @LAST_TRAN_TIME,
                           LAST_TRAN_USER_ID = @LAST_TRAN_USER_ID,
                           LAST_TRAN_COMMENT = @LAST_TRAN_COMMENT, 
                           LAST_HIST_SEQ = @LAST_HIST_SEQ
                           WHERE 
                           LOT_ID = @LOT_ID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@LOT_ID", dto.LOT_ID);
            cmd.Parameters.AddWithValue("@LAST_TRAN_CODE", dto.LAST_TRAN_CODE);
            cmd.Parameters.AddWithValue("@LAST_TRAN_TIME", dto.LAST_TRAN_TIME);
            cmd.Parameters.AddWithValue("@LAST_TRAN_USER_ID", dto.LAST_TRAN_USER_ID);
            cmd.Parameters.AddWithValue("@LAST_TRAN_COMMENT", string.IsNullOrWhiteSpace(dto.LAST_TRAN_COMMENT) ? (object)DBNull.Value : dto.LAST_TRAN_COMMENT);
            cmd.Parameters.AddWithValue("@LAST_HIST_SEQ", dto.LAST_HIST_SEQ);

            cmd.Transaction = trans;
            cmd.ExecuteNonQuery();

            sql = @"IF (@LOT_QTY = 0)
                     	BEGIN 
                     		UPDATE LOT_STS
                                    SET 
                                    LOT_QTY = @LOT_QTY,
                     			      LOT_DELETE_FLAG = @LOT_DELETE_FLAG,
                     			      LOT_DELETE_CODE = @LOT_DELETE_CODE,
                     			      LOT_DELETE_TIME = @LOT_DELETE_TIME,
                                    LAST_TRAN_CODE = @LAST_TRAN_CODE,
                                    LAST_TRAN_TIME = @LAST_TRAN_TIME,
                                    LAST_TRAN_USER_ID = @LAST_TRAN_USER_ID,
                                    LAST_TRAN_COMMENT = @LAST_TRAN_COMMENT, 
                                    LAST_HIST_SEQ = @LAST_HIST_SEQ
                                    WHERE 
                                    LOT_ID = @LOT_ID
                     	END
                     ELSE
                     	BEGIN
                     			UPDATE LOT_STS
                                  SET 
                                  LOT_QTY = @LOT_QTY,
                                  LAST_TRAN_CODE = @LAST_TRAN_CODE,
                                  LAST_TRAN_TIME = @LAST_TRAN_TIME,
                                  LAST_TRAN_USER_ID = @LAST_TRAN_USER_ID,
                                  LAST_TRAN_COMMENT = @LAST_TRAN_COMMENT, 
                                  LAST_HIST_SEQ = @LAST_HIST_SEQ
                                  WHERE 
                                  LOT_ID = @LOT_ID
                     	END";
            SqlCommand cmd2 = new SqlCommand(sql, conn);
            cmd2.Parameters.Add("@LOT_ID", SqlDbType.VarChar);
            cmd2.Parameters.Add("@LOT_QTY", SqlDbType.Decimal);
            cmd2.Parameters.Add("@LOT_DELETE_FLAG", SqlDbType.Char);
            cmd2.Parameters.Add("@LOT_DELETE_CODE", SqlDbType.VarChar);
            cmd2.Parameters.Add("@LOT_DELETE_TIME", SqlDbType.DateTime);
            cmd2.Parameters.Add("@LAST_TRAN_CODE", SqlDbType.VarChar);
            cmd2.Parameters.Add("@LAST_TRAN_TIME", SqlDbType.DateTime);
            cmd2.Parameters.Add("@LAST_TRAN_USER_ID", SqlDbType.VarChar);
            cmd2.Parameters.Add("@LAST_TRAN_COMMENT", SqlDbType.VarChar);
            cmd2.Parameters.Add("@LAST_HIST_SEQ", SqlDbType.Decimal);

            foreach (LOT_STS_DTO item in dto2)
            {
               cmd2.Parameters["@LOT_ID"].Value = item.LOT_ID;
               cmd2.Parameters["@LOT_QTY"].Value = item.LOT_QTY;
               cmd2.Parameters["@LOT_DELETE_FLAG"].Value = 'Y';
               cmd2.Parameters["@LOT_DELETE_CODE"].Value = "EMPTY";
               cmd2.Parameters["@LOT_DELETE_TIME"].Value = DateTime.Now;
               cmd2.Parameters["@LAST_TRAN_CODE"].Value = item.LAST_TRAN_CODE;
               cmd2.Parameters["@LAST_TRAN_TIME"].Value = item.LAST_TRAN_TIME;
               cmd2.Parameters["@LAST_TRAN_USER_ID"].Value = item.LAST_TRAN_USER_ID;
               cmd2.Parameters["@LAST_TRAN_COMMENT"].Value = string.IsNullOrWhiteSpace(item.LAST_TRAN_COMMENT) ? (object)DBNull.Value : item.LAST_TRAN_COMMENT;
               cmd2.Parameters["@LAST_HIST_SEQ"].Value = item.LAST_HIST_SEQ;
               cmd2.Transaction = trans;
               cmd2.ExecuteNonQuery();
            }
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
            SqlCommand cmd3 = Helper.LotHisCmd(dto);
            cmd3.Connection = conn;
            cmd3.CommandText = sql;
            cmd3.Transaction = trans;
            cmd3.ExecuteNonQuery();

            SqlCommand cmd4 = new SqlCommand();
            cmd4.Connection = conn;
            cmd4.CommandText = sql;
            cmd4.Transaction = trans;

            foreach (LOT_STS_DTO item in dto2)
            {
               cmd4 = Helper.LotHisCmd(item, cmd4);
               cmd4.ExecuteNonQuery();
            }
            sql = @"INSERT INTO LOT_MATERIAL_HIS
                    (LOT_ID, HIST_SEQ, MATERIAL_LOT_ID, MATERIAL_LOT_HIST_SEQ, INPUT_QTY,
                    CHILD_PRODUCT_CODE, MATERIAL_STORE_CODE, TRAN_TIME, WORK_DATE, PRODUCT_CODE,
                    OPERATION_CODE, EQUIPMENT_CODE, TRAN_USER_ID, TRAN_COMMENT)
                    VALUES
                    (@LOT_ID, @HIST_SEQ, @MATERIAL_LOT_ID, @MATERIAL_LOT_HIST_SEQ, @INPUT_QTY,
                    @CHILD_PRODUCT_CODE, @MATERIAL_STORE_CODE, @TRAN_TIME, @WORK_DATE, @PRODUCT_CODE,
                    @OPERATION_CODE, @EQUIPMENT_CODE, @TRAN_USER_ID, @TRAN_COMMENT)";
            SqlCommand cmd5 = new SqlCommand(sql, conn);
            cmd5.Transaction = trans;

            cmd5.Parameters.Add("@LOT_ID", SqlDbType.VarChar);
            cmd5.Parameters.Add("@HIST_SEQ", SqlDbType.Decimal);
            cmd5.Parameters.Add("@MATERIAL_LOT_ID", SqlDbType.VarChar);
            cmd5.Parameters.Add("@MATERIAL_LOT_HIST_SEQ", SqlDbType.Decimal);
            cmd5.Parameters.Add("@INPUT_QTY", SqlDbType.VarChar);
            cmd5.Parameters.Add("@CHILD_PRODUCT_CODE", SqlDbType.VarChar);
            cmd5.Parameters.Add("@MATERIAL_STORE_CODE", SqlDbType.VarChar);
            cmd5.Parameters.Add("@TRAN_TIME", SqlDbType.DateTime);
            cmd5.Parameters.Add("@WORK_DATE", SqlDbType.VarChar);
            cmd5.Parameters.Add("@PRODUCT_CODE", SqlDbType.VarChar);
            cmd5.Parameters.Add("@OPERATION_CODE", SqlDbType.VarChar);
            cmd5.Parameters.Add("@EQUIPMENT_CODE", SqlDbType.VarChar);
            cmd5.Parameters.Add("@TRAN_USER_ID", SqlDbType.VarChar);
            cmd5.Parameters.Add("@TRAN_COMMENT", SqlDbType.VarChar);

            foreach (LOT_MATERIAL_HIS_DTO item in materiars)
            {
               cmd5.Parameters["@LOT_ID"].Value = item.LOT_ID;
               cmd5.Parameters["@HIST_SEQ"].Value = item.HIST_SEQ;
               cmd5.Parameters["@MATERIAL_LOT_ID"].Value = item.MATERIAL_LOT_ID;
               cmd5.Parameters["@MATERIAL_LOT_HIST_SEQ"].Value = item.MATERIAL_LOT_HIST_SEQ;
               cmd5.Parameters["@INPUT_QTY"].Value = item.INPUT_QTY;
               cmd5.Parameters["@CHILD_PRODUCT_CODE"].Value = string.IsNullOrWhiteSpace(item.CHILD_PRODUCT_CODE) ? (object)DBNull.Value : item.CHILD_PRODUCT_CODE;
               cmd5.Parameters["@MATERIAL_STORE_CODE"].Value = item.MATERIAL_STORE_CODE;
               cmd5.Parameters["@TRAN_TIME"].Value = item.TRAN_TIME;
               cmd5.Parameters["@WORK_DATE"].Value = item.WORK_DATE;
               cmd5.Parameters["@OPERATION_CODE"].Value = item.OPERATION_CODE;
               cmd5.Parameters["@PRODUCT_CODE"].Value = item.PRODUCT_CODE;
               cmd5.Parameters["@EQUIPMENT_CODE"].Value = item.EQUIPMENT_CODE;
               cmd5.Parameters["@TRAN_USER_ID"].Value = item.TRAN_USER_ID;
               cmd5.Parameters["@TRAN_COMMENT"].Value = item.TRAN_COMMENT;
               cmd5.ExecuteNonQuery();
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
      public void Dispose()
      {
         if (conn != null || conn.State == ConnectionState.Open)
            conn.Close();
      }
   }
}
