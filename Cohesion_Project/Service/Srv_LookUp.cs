using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cohesion_DAO;
using Cohesion_DTO;


namespace Cohesion_Project.Service
{
    public class Srv_LookUp
    {
        public List<LOT_STS_PPG_DTO> SelectLOT_STS_List()
        {
            LookUp_DAO db = new LookUp_DAO();
            List<LOT_STS_PPG_DTO> list = db.SelectLOT_STS_List();
            db.Dispose();

            return list;
        }

        public List<LOT_HIS_VO> SelectLOT_HIS_List(string lotID)
        {
            LookUp_DAO db = new LookUp_DAO();
            List<LOT_HIS_VO> list = db.SelectLOT_HIS_List(lotID);
            db.Dispose();

            return list;
        }

        public List<LOT_STS_VO> SelectLOTWithCondition(LOT_STS_VO condition)
        {
            LookUp_DAO db = new LookUp_DAO();
            List<LOT_STS_VO> list = db.SelectLOTWithCondition(condition);
            db.Dispose();

            return list;
        }
    }
}
