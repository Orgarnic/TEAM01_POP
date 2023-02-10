using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cohesion_DTO;
using Cohesion_DAO;

namespace Cohesion_Project.Service
{
    class Srv_Inspect
    {
        public List<LOT_INSPECT_HIS_DTO> GetInspectHisAllList()
        {
            Inspect_DAO db = new Inspect_DAO();
            List<LOT_INSPECT_HIS_DTO> list = db.GetInspectHisAllList();
            db.Dispose();

            return list;
        }
    }
}
