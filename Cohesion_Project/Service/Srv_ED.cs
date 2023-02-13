using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cohesion_DAO;
using Cohesion_DTO;

namespace Cohesion_Project 
{ 
    class Srv_ED
    {

        public List<EQUIP_DOWN_DTO> SelectEDownSearch(string from, string to)
        {
            EDown_DAO dao = new EDown_DAO();
            List<EQUIP_DOWN_DTO> list = dao.SelectEDownSearch(from ,to);
            dao.Dispose();

            return list;
        }

        public List<EQUIP_DOWN_DTO> SelectEDown()
        {
            EDown_DAO dao = new EDown_DAO();
            List<EQUIP_DOWN_DTO> list = dao.SelectEDown();
            dao.Dispose();

            return list;
        }

        public List<EQUIP_DOWN_DTO> SelectEDown1(string from , string to)
        {
            EDown_DAO dao = new EDown_DAO();
            List<EQUIP_DOWN_DTO> list = dao.SelectEDown1(from, to);
            dao.Dispose();

            return list;
        }

        public bool InsertEDown(EQUIP_DOWN_DTO dto)
        {
            EDown_DAO dao = new EDown_DAO();
            bool result = dao.InsertEDown(dto);
            dao.Dispose();
            return result;
        }

        public List<CODE_DATA_MST_DTO> Combo()
        {
            EDown_DAO dao = new EDown_DAO();
            List<CODE_DATA_MST_DTO> list = dao.Combo();
            dao.Dispose();
            return list;
        }

        public List<EQUIPMENT_MST_DTO> EQCombo()
        {
            EDown_DAO dao = new EDown_DAO();
            List<EQUIPMENT_MST_DTO> list = dao.EQCombo();
            dao.Dispose();
            return list;
        }


    }
}
