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

      public List<WORK_ORDER_MST_DTO> SelectOrder()
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
                                  INNER JOIN PRODUCT_MST P ON W.PRODUCT_CODE = P.PRODUCT_CODE ";
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

      public void Dispose()
      {
         if (conn != null || conn.State == ConnectionState.Open)
            conn.Close();
      }
   }
}
