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
    public class Purchase_DAO : IDisposable
    {
        SqlConnection conn = null;
        readonly string DB = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        public Purchase_DAO()
        {
            conn = new SqlConnection(DB);
        }
        public void Dispose()
        {
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }    
        }

        public List<PURCHASE_ORDER_MST_DTO> GetAllPurchaseList()
        {
            try
            {
                string sql = @"select PURCHASE_ORDER_ID, PURCHASE_SEQ, SALES_ORDER_ID, ORDER_DATE, VENDOR_CODE, MATERIAL_CODE, ORDER_QTY, STOCK_IN_FLAG, STOCK_IN_STORE_CODE, STOCK_IN_LOT_ID
                               from PURCHASE_ORDER_MST";

                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();
                List<PURCHASE_ORDER_MST_DTO> list = Helper.DataReaderMapToList<PURCHASE_ORDER_MST_DTO>(cmd.ExecuteReader());

                return list;
            }
            catch(Exception err)
            {
                Debug.WriteLine(err.Message);
                Debug.WriteLine(err.StackTrace);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<PURCHASE_ORDER_MST_DTO> SelectPurchaseList(string orderId)
        {
            List<PURCHASE_ORDER_MST_DTO> list = new List<PURCHASE_ORDER_MST_DTO>();
            try
            {
                string sql = @"select PURCHASE_ORDER_ID, PURCHASE_SEQ, SALES_ORDER_ID, ORDER_DATE, p.VENDOR_CODE, MATERIAL_CODE, pm.PRODUCT_NAME, ORDER_QTY, STOCK_IN_FLAG, STOCK_IN_STORE_CODE, STOCK_IN_LOT_ID, c.Data_1 CUSTOMER_NAME
                               from PURCHASE_ORDER_MST p inner join CODE_DATA_MST c on p.VENDOR_CODE = c.KEY_1
						                                 inner join PRODUCT_MST pm on p.MATERIAL_CODE = pm.PRODUCT_CODE
                               where PURCHASE_ORDER_ID = @orderId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@orderId", orderId);

                conn.Open();
                list = Helper.DataReaderMapToList<PURCHASE_ORDER_MST_DTO>(cmd.ExecuteReader());

                return list;
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                Debug.WriteLine(err.StackTrace);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
