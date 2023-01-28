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
                           LOT_ID, LOT_DESC, L.PRODUCT_CODE, L.OPERATION_CODE, STORE_CODE, LOT_QTY, CREATE_QTY,
                           OPER_IN_QTY, START_FLAG, START_QTY, START_TIME, START_EQUIPMENT_CODE, END_FLAG,
                           END_TIME, END_EQUIPMENT_CODE, SHIP_FLAG, SHIP_CODE, SHIP_TIME, PRODUCTION_TIME,
                           L.CREATE_TIME, OPER_IN_TIME, WORK_ORDER_ID, LOT_DELETE_FLAG, LOT_DELETE_CODE, LOT_DELETE_TIME,
                           LAST_TRAN_CODE, LAST_TRAN_TIME, LAST_TRAN_USER_ID, LAST_TRAN_COMMENT, LAST_HIST_SEQ
                           FROM 
                           LOT_STS L INNER JOIN PRODUCT_OPERATION_REL P ON L.PRODUCT_CODE = P.PRODUCT_CODE AND L.OPERATION_CODE = P.OPERATION_CODE
                           WHERE 
                           (LAST_TRAN_CODE = 'END' AND 
                           P.FLOW_SEQ < 
                           (SELECT 
                           MAX(FLOW_SEQ)
                           FROM PRODUCT_OPERATION_REL
                           WHERE PRODUCT_CODE = L.PRODUCT_CODE AND OPERATION_CODE = L.OPERATION_CODE)) OR
                           LAST_TRAN_CODE = 'CREATE'
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

      public void Dispose()
      {
         if (conn != null || conn.State == ConnectionState.Open)
            conn.Close();
      }
   }
}
