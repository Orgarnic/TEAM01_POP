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
                string sql = @"select PURCHASE_ORDER_ID, PURCHASE_SEQ, SALES_ORDER_ID, ORDER_DATE, p.VENDOR_CODE, c.Data_1 CUSTOMER_NAME, MATERIAL_CODE, pm.PRODUCT_NAME PRODUCT_NAME, ORDER_QTY, STOCK_IN_FLAG, STOCK_IN_STORE_CODE, STOCK_IN_LOT_ID
                               from PURCHASE_ORDER_MST p inner join CODE_DATA_MST c on p.VENDOR_CODE = c.KEY_1
														 inner join PRODUCT_MST pm on p.MATERIAL_CODE = pm.PRODUCT_CODE";

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
            try
            {
                string sql = @"select PURCHASE_ORDER_ID, PURCHASE_SEQ, SALES_ORDER_ID, ORDER_DATE, p.VENDOR_CODE, p.MATERIAL_CODE, pm.PRODUCT_NAME, ORDER_QTY, STOCK_IN_FLAG, STOCK_IN_STORE_CODE, STOCK_IN_LOT_ID, c.Data_1 CUSTOMER_NAME
                               from PURCHASE_ORDER_MST p inner join CODE_DATA_MST c on p.VENDOR_CODE = c.KEY_1
						                                 inner join PRODUCT_MST pm on p.MATERIAL_CODE = pm.PRODUCT_CODE
                               where PURCHASE_ORDER_ID = @PURCHASE_ORDER_ID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@PURCHASE_ORDER_ID", orderId);

                conn.Open();
                List<PURCHASE_ORDER_MST_DTO> list = Helper.DataReaderMapToList<PURCHASE_ORDER_MST_DTO>(cmd.ExecuteReader());

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

        public bool UpdatePurchaseData(List<PURCHASE_ORDER_MST_DTO> dto)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_CreatePurchaseLOT", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@PURCHASE_ORDER_ID", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@VENDOR_CODE", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@STOCK_IN_FLAG", SqlDbType.Char));
                cmd.Parameters.Add(new SqlParameter("@MATERIAL_CODE", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@LOT_QTY", SqlDbType.Decimal));

                for (int i = 0; i < dto.Count; i++)
                {
                    cmd.Parameters["@PURCHASE_ORDER_ID"].Value = dto[i].PURCHASE_ORDER_ID;
                    cmd.Parameters["@VENDOR_CODE"].Value = dto[i].VENDOR_CODE;
                    cmd.Parameters["@STOCK_IN_FLAG"].Value = dto[i].STOCK_IN_FLAG.ToString();
                    cmd.Parameters["@MATERIAL_CODE"].Value = dto[i].MATERIAL_CODE;
                    cmd.Parameters["@LOT_QTY"].Value = dto[i].ORDER_QTY;

                    int iRowAffect = cmd.ExecuteNonQuery();
                    string sss = $"'{dto[i].PURCHASE_ORDER_ID}', '{dto[i].VENDOR_CODE}', '{dto[i].MATERIAL_CODE}', '{dto[i].STOCK_IN_FLAG.ToString()}', {dto[i].ORDER_QTY}";
                }
                return true;

            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                Debug.WriteLine(err.StackTrace);
                return false;
            }
            finally
            {
                conn.Close();
            }

        }
    }
}
