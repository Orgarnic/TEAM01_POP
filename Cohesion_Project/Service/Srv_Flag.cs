using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cohesion_DAO;
using Cohesion_DTO;

namespace Cohesion_Project
{
   class Srv_Flag
   {
      public List<LOT_STS_DTO> SelectOrderLotBed(string orderId)
      {
         Flag_DAO dao = new Flag_DAO();
         List<LOT_STS_DTO> list = dao.SelectOrderLotBed(orderId);
         dao.Dispose();

         return list;
      }
      public List<LOT_STS_DTO> SelectOrderLotInspect(string orderId)
      {
         Flag_DAO dao = new Flag_DAO();
         List<LOT_STS_DTO> list = dao.SelectOrderLotInspect(orderId);
         dao.Dispose();

         return list;
      }
      public List<CODE_DATA_MST_DTO> SelectBedCodes()
      {
         Flag_DAO dao = new Flag_DAO();
         List<CODE_DATA_MST_DTO> list = dao.SelectBedCodes();
         dao.Dispose();

         return list;
      }
      public List<INSPECT_ITEM_MST_DTO> SelectInspects(string operation)
      {
         Flag_DAO dao = new Flag_DAO();
         List<INSPECT_ITEM_MST_DTO> list = dao.SelectInspects(operation);
         dao.Dispose();

         return list;
      }
      public List<LOT_STS_DTO> SelectLotMateriars(string prodId, string operation)
      {
         Flag_DAO dao = new Flag_DAO();
         List<LOT_STS_DTO> list = dao.SelectLotMateriars(prodId, operation);
         dao.Dispose();

         return list;
      }
      public List<LOT_STS_DTO> SelectMateriarLot(string lots)
      {
         Flag_DAO dao = new Flag_DAO();
         List<LOT_STS_DTO> list = dao.SelectMateriarLot(lots);
         dao.Dispose();

         return list;
      }
      public bool BedRegCheck(string operation)
      {
         Flag_DAO dao = new Flag_DAO();
         bool temp = dao.BedRegCheck(operation);
         dao.Dispose();

         return temp;
      }
      public bool InsertBedReg(LOT_STS_DTO dto, List<LOT_DEFECT_HIS_DTO> defects)
      {
         Flag_DAO dao = new Flag_DAO();
         bool temp = dao.InsertBedReg(dto, defects);
         dao.Dispose();

         return temp;
      }
      public bool InsertInspect(LOT_STS_DTO dto, List<LOT_INSPECT_HIS_DTO> inspects)
      {
         Flag_DAO dao = new Flag_DAO();
         bool temp = dao.InsertInspect(dto, inspects);
         dao.Dispose();

         return temp;
      }
      public bool InsertMateriar(LOT_STS_DTO dto, List<LOT_STS_DTO> dto2, List<LOT_MATERIAL_HIS_DTO> materiars)
      {
         Flag_DAO dao = new Flag_DAO();
         bool temp = dao.InsertMateriar(dto, dto2, materiars);
         dao.Dispose();

         return temp;
      }
   }
}
