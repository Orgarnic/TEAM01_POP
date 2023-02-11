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
    }
}
