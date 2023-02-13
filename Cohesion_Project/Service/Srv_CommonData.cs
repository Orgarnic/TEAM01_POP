using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cohesion_DTO;
using Cohesion_DAO;

namespace Cohesion_Project.Service
{
    public class Srv_CommonData
    {
        public List<CODE_TABLE_MST_DTO> SelectCommonTable()
        {
            Common_DAO dao = new Common_DAO();
            List<CODE_TABLE_MST_DTO> list = dao.SelectCommonTable();
            dao.Dispose();

            return list;
        }

        public List<CODE_DATA_MST_DTO> SelectAllCommonTableData()
        {
            Common_DAO dao = new Common_DAO();
            List<CODE_DATA_MST_DTO> list = dao.SelectAllCommonTableData();
            dao.Dispose();

            return list;
        }
    }
}
