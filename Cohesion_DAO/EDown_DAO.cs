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
    public class EDown_DAO : IDisposable
    {
        SqlConnection conn = null;
        readonly string DB = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public void Dispose()
        {
            if (conn != null || conn.State == ConnectionState.Open)
                conn.Close();
        }
        public EDown_DAO()
        {
            conn = new SqlConnection(DB);
        }

        public List<EQUIP_DOWN_DTO> SelectEDownSearch(string from, string to)
        {
            List<EQUIP_DOWN_DTO> list = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                string sql = "Select EQUIPMENT_CODE, DT_DATE, DT_START_TIME, DT_END_TIME, DT_TIME, DT_CODE, DT_COMMENT, DT_USER_ID, ACTION_COMMENT  from EQUIP_DOWN_HIS";

                
                cmd.CommandText = sql.ToString();
                cmd.Connection = conn;
                conn.Open();
                list = Helper.DataReaderMapToList<EQUIP_DOWN_DTO>(cmd.ExecuteReader());
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

        public List<EQUIP_DOWN_DTO> SelectEDown()
        {
            List<EQUIP_DOWN_DTO> list = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                string sql = "Select EQUIPMENT_CODE, DT_DATE, DT_START_TIME, DT_END_TIME, DT_TIME, DT_CODE, DT_COMMENT, DT_USER_ID, ACTION_COMMENT from EQUIP_DOWN_HIS";

                cmd.CommandText = sql.ToString();
                cmd.Connection = conn;
                conn.Open();
                list = Helper.DataReaderMapToList<EQUIP_DOWN_DTO>(cmd.ExecuteReader());
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

        public List<EQUIP_DOWN_DTO> SelectEDown1(string from , string to)
        {
            List<EQUIP_DOWN_DTO> list = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                string sql = "Select EQUIPMENT_CODE, DT_DATE, DT_START_TIME, DT_END_TIME, DT_TIME, DT_CODE, DT_COMMENT, DT_USER_ID, ACTION_COMMENT  from EQUIP_DOWN_HIS" +
                            "  where convert(datetime, DT_DATE, 23) between convert(datetime, @from, 23) and convert(datetime, @to, 23)";


                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);
                cmd.CommandText = sql.ToString();
                cmd.Connection = conn;
                conn.Open();
                list = Helper.DataReaderMapToList<EQUIP_DOWN_DTO>(cmd.ExecuteReader());
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


        public bool InsertEDown(EQUIP_DOWN_DTO dto)
        {
            try
            {
                conn.Open();
                string sql = @"insert into EQUIP_DOWN_HIS(EQUIPMENT_CODE, DT_DATE, DT_START_TIME, DT_END_TIME, DT_TIME, DT_CODE, DT_COMMENT, DT_USER_ID, ACTION_COMMENT)
values (@EQUIPMENT_CODE, @DT_DATE, @DT_START_TIME, @DT_END_TIME, @DT_TIME, @DT_CODE, @DT_COMMENT, @DT_USER_ID, @ACTION_COMMENT )";
                SqlCommand cmd = Helper.UpsertCmdValue<EQUIP_DOWN_DTO>(dto, sql, conn);
            
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception err)
            {
                Debug.WriteLine(err);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<CODE_DATA_MST_DTO> Combo()
        {
            List<CODE_DATA_MST_DTO> list = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                string sql = @"select CODE_TABLE_NAME, KEY_1, KEY_2, KEY_3, DATA_1, DATA_2, DATA_3, DATA_4, DATA_5, DISPLAY_SEQ, CREATE_TIME, CREATE_USER_ID, UPDATE_TIME, UPDATE_USER_ID
from CODE_DATA_MST
where CODE_TABLE_NAME = @CODE_TABLE_NAME";


                cmd.Parameters.AddWithValue("@CODE_TABLE_NAME", "CM_DT_CODE");

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

        public List<EQUIPMENT_MST_DTO> EQCombo()
        {
            List<EQUIPMENT_MST_DTO> list = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                string sql = @"select EQUIPMENT_CODE, EQUIPMENT_NAME, EQUIPMENT_TYPE, EQUIPMENT_STATUS, LAST_DOWN_TIME, CREATE_TIME, CREATE_USER_ID, UPDATE_TIME, UPDATE_USER_ID
from EQUIPMENT_MST
";


                cmd.CommandText = sql.ToString();
                cmd.Connection = conn;
                conn.Open();
                list = Helper.DataReaderMapToList<EQUIPMENT_MST_DTO>(cmd.ExecuteReader());
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
    }


}
