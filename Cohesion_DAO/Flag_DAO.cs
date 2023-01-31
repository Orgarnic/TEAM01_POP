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

            sql = @"
                     DECLARE @SEQ INT
                     SET @SEQ = (SELECT CASE WHEN (SELECT COUNT(*) FROM LOT_DEFECT_HIS WHERE  LOT_ID = @LOT_ID AND PRODUCT_CODE = @PRODUCT_CODE) IS NULL 
                     THEN 0 ELSE (SELECT COUNT(*) FROM LOT_DEFECT_HIS WHERE  LOT_ID = @LOT_ID AND PRODUCT_CODE = @PRODUCT_CODE) END + 1)
                     INSERT INTO LOT_DEFECT_HIS
                    (LOT_ID, HIST_SEQ, DEFECT_CODE, DEFECT_QTY, TRAN_TIME, WORK_DATE, 
                     PRODUCT_CODE, OPERATION_CODE, STORE_CODE, EQUIPMENT_CODE, TRAN_USER_ID, TRAN_COMMENT)
                    VALUES 
                    (@LOT_ID, @SEQ, @DEFECT_CODE, @DEFECT_QTY, @TRAN_TIME, @WORK_DATE,
                     @PRODUCT_CODE, @OPERATION_CODE, @STORE_CODE, @EQUIPMENT_CODE, @TRAN_USER_ID, @TRAN_COMMENT)";
            SqlCommand cmd3 = new SqlCommand(sql, conn);
            cmd3.Transaction = trans;
            cmd3.Parameters.AddWithValue("@LOT_ID", dto.LOT_ID);
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
      public void Dispose()
      {
         if (conn != null || conn.State == ConnectionState.Open)
            conn.Close();
      }
   }
}
