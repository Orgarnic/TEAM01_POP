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
    public class LookUp_DAO
    {
        SqlConnection conn;
        public LookUp_DAO()
        {
            string connStr = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            conn = new SqlConnection(connStr);
        }
        public void Dispose()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
        public List<LOT_STS_PPG_DTO> SelectLOT_STS_List()
        {
            string sql = @"SELECT LOT_ID, LOT_DESC, LS.PRODUCT_CODE, PRODUCT_NAME, OPERATION_CODE, STORE_CODE, LOT_QTY, CREATE_QTY
                                , OPER_IN_QTY, START_FLAG, START_QTY, START_TIME, START_EQUIPMENT_CODE, END_FLAG, END_TIME
                                , END_EQUIPMENT_CODE, SHIP_FLAG, SHIP_CODE, SHIP_TIME, PRODUCTION_TIME, LS.CREATE_TIME
                                , OPER_IN_TIME, WORK_ORDER_ID, LOT_DELETE_FLAG, LOT_DELETE_CODE, LOT_DELETE_TIME, LAST_TRAN_CODE
                                , LAST_TRAN_TIME, LAST_TRAN_USER_ID, LAST_TRAN_COMMENT, LAST_HIST_SEQ
                           FROM LOT_STS LS INNER JOIN PRODUCT_MST PM ON LS.PRODUCT_CODE = PM.PRODUCT_CODE";

            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();

            List<LOT_STS_PPG_DTO> list = Helper.DataReaderMapToList<LOT_STS_PPG_DTO>(cmd.ExecuteReader());

            return list;
        }
        public List<LOT_HIS_VO> SelectLOT_HIS_List(string lotID)
        {
            List<LOT_HIS_VO> list = null;
            try
            {
                string sql = @"SELECT LOT_ID, HIST_SEQ, TRAN_TIME, TRAN_CODE, LOT_DESC, PRODUCT_CODE, LH.OPERATION_CODE, OM.OPERATION_NAME,STORE_CODE, 
                           	          LOT_QTY, CREATE_QTY, OPER_IN_QTY, START_FLAG, START_QTY, START_TIME, START_EQUIPMENT_CODE, 
                           	          END_FLAG, END_TIME, END_EQUIPMENT_CODE, SHIP_FLAG, SHIP_CODE, SHIP_TIME, PRODUCTION_TIME, 
                           	          LH.CREATE_TIME, OPER_IN_TIME, WORK_ORDER_ID, LOT_DELETE_FLAG, LOT_DELETE_CODE, LOT_DELETE_TIME, 
                           	          WORK_DATE, TRAN_USER_ID, TRAN_COMMENT, OLD_PRODUCT_CODE, OLD_OPERATION_CODE, OLD_STORE_CODE, OLD_LOT_QTY
                               FROM LOT_HIS LH LEFT JOIN OPERATION_MST OM ON LH.OPERATION_CODE = OM.OPERATION_CODE
                               WHERE LOT_ID = @lotID ";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@lotID", lotID);

                conn.Open();

                list = Helper.DataReaderMapToList<LOT_HIS_VO>(cmd.ExecuteReader());
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

        public List<LOT_STS_VO> SelectLOTWithCondition(LOT_STS_VO condition)
        {
            List<LOT_STS_VO> list = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                StringBuilder sql = new StringBuilder(
                    @"SELECT LOT_ID, LOT_DESC, LS.PRODUCT_CODE, PRODUCT_NAME, OPERATION_CODE, STORE_CODE, LOT_QTY, CREATE_QTY
                                , OPER_IN_QTY, START_FLAG, START_QTY, START_TIME, START_EQUIPMENT_CODE, END_FLAG, END_TIME
                                , END_EQUIPMENT_CODE, SHIP_FLAG, SHIP_CODE, SHIP_TIME, PRODUCTION_TIME, LS.CREATE_TIME
                                , OPER_IN_TIME, WORK_ORDER_ID, LOT_DELETE_FLAG, LOT_DELETE_CODE, LOT_DELETE_TIME, LAST_TRAN_CODE
                                , LAST_TRAN_TIME, LAST_TRAN_USER_ID, LAST_TRAN_COMMENT, LAST_HIST_SEQ
                      FROM LOT_STS LS INNER JOIN PRODUCT_MST PM ON LS.PRODUCT_CODE = PM.PRODUCT_CODE
                      WHERE 1 = 1 ");

                if (!string.IsNullOrWhiteSpace(condition.OPERATION_CODE))
                {
                    sql.Append(" and OPERATION_CODE = @OPERATION_CODE ");
                    cmd.Parameters.AddWithValue("@OPERATION_CODE", condition.OPERATION_CODE);
                }
                if (!string.IsNullOrWhiteSpace(condition.STORE_CODE))
                {
                    sql.Append(" AND STORE_CODE = @STORE_CODE ");
                    cmd.Parameters.AddWithValue("@STORE_CODE", condition.STORE_CODE);
                }
                if (!string.IsNullOrWhiteSpace(condition.PRODUCT_CODE))
                {
                    sql.Append(" AND LS.PRODUCT_CODE = @PRODUCT_CODE ");
                    cmd.Parameters.AddWithValue("@PRODUCT_CODE", condition.PRODUCT_CODE);
                }
                if (!string.IsNullOrWhiteSpace(condition.LOT_ID))
                {
                    sql.Append(" AND LOT_ID = @LOT_ID ");
                    cmd.Parameters.AddWithValue("@LOT_ID", condition.LOT_ID);
                }

                //sql.Append(" ORDER BY  ");
                cmd.CommandText = sql.ToString();
                cmd.Connection = conn;
                conn.Open();
                list = Helper.DataReaderMapToList<LOT_STS_VO>(cmd.ExecuteReader());
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
    }
}
