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
    public class Inspect_DAO
    {
        SqlConnection conn = null;
        readonly string DB = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        public Inspect_DAO()
        {
            conn = new SqlConnection(DB);
        }
        public void Dispose()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public List<LOT_INSPECT_HIS_DTO> GetInspectHisAllList()
        {            
            try
            {
                string sql = @"select LOT_ID, HIST_SEQ, INSPECT_ITEM_NAME, SPEC_LSL, SPEC_TARGET, SPEC_USL, INSPECT_VALUE, INSPECT_RESULT, TRAN_TIME, WORK_DATE, p.PRODUCT_NAME PRODUCT_CODE, o.OPERATION_NAME OPERATION_CODE, STORE_CODE , e.EQUIPMENT_NAME EQUIPMENT_CODE, TRAN_USER_ID, TRAN_COMMENT
                               from LOT_INSPECT_HIS ih inner join PRODUCT_MST p on ih.PRODUCT_CODE = p.PRODUCT_CODE
						                               inner join OPERATION_MST o on ih.OPERATION_CODE = o.OPERATION_CODE
						                               inner join EQUIPMENT_MST e on ih.EQUIPMENT_CODE = e.EQUIPMENT_CODE";

                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();
                List<LOT_INSPECT_HIS_DTO> list = Helper.DataReaderMapToList<LOT_INSPECT_HIS_DTO>(cmd.ExecuteReader());

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

        public List<LOT_INSPECT_HIS_DTO> GetLotInspectHisInfo(string id = null, string inspect = null, string isvalue = null)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                string sql = @"select LOT_ID, HIST_SEQ, INSPECT_ITEM_NAME, SPEC_LSL, SPEC_TARGET, SPEC_USL, INSPECT_VALUE, INSPECT_RESULT, TRAN_TIME, WORK_DATE, p.PRODUCT_NAME PRODUCT_CODE, o.OPERATION_NAME OPERATION_CODE, e.EQUIPMENT_NAME EQUIPMENT_CODE, TRAN_USER_ID, TRAN_COMMENT
                               from LOT_INSPECT_HIS ih inner join PRODUCT_MST p on ih.PRODUCT_CODE = p.PRODUCT_CODE
						                               inner join OPERATION_MST o on ih.OPERATION_CODE = o.OPERATION_CODE
						                               inner join EQUIPMENT_MST e on ih.EQUIPMENT_CODE = e.EQUIPMENT_CODE
                               where 1 = 1";

                sb.Append(sql);
                SqlCommand cmd = new SqlCommand();

                if(!string.IsNullOrWhiteSpace(id))
                    sb.Append($" and LOT_ID = '" + id + "'");
                if (!string.IsNullOrWhiteSpace(inspect))
                    sb.Append($" and INSPECT_ITEM_NAME = '" + inspect + "'");
                if (!string.IsNullOrWhiteSpace(isvalue))
                    sb.Append($" and INSPECT_VALUE = '" + isvalue + "'");

                cmd.CommandText = sb.ToString();
                cmd.Connection = conn;


                conn.Open();
                List<LOT_INSPECT_HIS_DTO> list = Helper.DataReaderMapToList<LOT_INSPECT_HIS_DTO>(cmd.ExecuteReader());

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

        public List<LOT_INSPECT_HIS_DTO> GetLOTInspectID()
        {
            try
            {
                string sql = @"select LOT_ID from LOT_INSPECT_HIS group by LOT_ID";

                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();
                List<LOT_INSPECT_HIS_DTO> list = Helper.DataReaderMapToList<LOT_INSPECT_HIS_DTO>(cmd.ExecuteReader());

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
        public List<INSPECT_ITEM_MST_DTO> GetInspectInfo()
        {
            try
            {
                string sql = @"select INSPECT_ITEM_NAME
                               from INSPECT_ITEM_MST
                               group by INSPECT_ITEM_NAME";

                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();
                List<INSPECT_ITEM_MST_DTO> list = Helper.DataReaderMapToList<INSPECT_ITEM_MST_DTO>(cmd.ExecuteReader());

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
