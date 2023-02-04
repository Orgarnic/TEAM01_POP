using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cohesion_DTO;
using Cohesion_DAO;

namespace Cohesion_Project
{
   class Srv_Work
   {
      public List<LOT_STS_DTO> SelectOrderLot(string orderId)
      {
         Work_DAO dao = new Work_DAO();
         List<LOT_STS_DTO> list = dao.SelectOrderLot(orderId);
         dao.Dispose();

         return list;
      }
      public List<LOT_STS_DTO> SelectOrderLotEnd(string orderId)
      {
         Work_DAO dao = new Work_DAO();
         List<LOT_STS_DTO> list = dao.SelectOrderLotEnd(orderId);
         dao.Dispose();

         return list;
      }
      public List<PRODUCT_OPERATION_REL_DTO> SelectOperations()
      {
         Work_DAO dao = new Work_DAO();
         List<PRODUCT_OPERATION_REL_DTO> list = dao.SelectOperations();
         dao.Dispose();

         return list;
      }
      public OPERATION_MST_DTO SelectOperation(string operation)
      {
         Work_DAO dao = new Work_DAO();
         OPERATION_MST_DTO temp = dao.SelectOperation(operation);
         dao.Dispose();

         return temp;
      }
      public List<EQUIPMENT_OPERATION_REL_DTO> SelectEquipments()
      {
         Work_DAO dao = new Work_DAO();
         List<EQUIPMENT_OPERATION_REL_DTO> list = dao.SelectEquipments();
         dao.Dispose();

         return list;
      }
      public bool StartWork(LOT_STS_DTO dto)
      {
         Work_DAO dao = new Work_DAO();
         bool temp = dao.StartWork(dto);
         dao.Dispose();

         return temp;
      }
      public bool EndWork(LOT_STS_DTO dto, LOT_END_HIS_DTO end, bool finish)
      {
         Work_DAO dao = new Work_DAO();
         bool temp = dao.EndWork(dto, end, finish);
         dao.Dispose();

         return temp;
      }
      public string EndWorkCondition(string lotId, string operation)
      {
         Work_DAO dao = new Work_DAO();
         string temp = dao.EndWorkCondition(lotId, operation);
         dao.Dispose();

         return temp;
      }
   }
}
