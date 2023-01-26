using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cohesion_DAO;
using Cohesion_DTO;

namespace Cohesion_Project
{
   class Srv_Order
   {
      public bool CreateLot(LOT_STS_DTO dto)
      {
         Order_DAO dao = new Order_DAO();
         bool temp = dao.CreateLot(dto);
         dao.Dispose();

         return temp;
      }

      public List<WORK_ORDER_MST_DTO> SelectOrderList()
      {
         Order_DAO dao = new Order_DAO();
         List<WORK_ORDER_MST_DTO> list = dao.SelectOrderList();
         dao.Dispose();

         return list;
      }
      public PRODUCT_OPERATION_REL_DTO SelectOperation(string product_code)
      {
         Order_DAO dao = new Order_DAO();
         PRODUCT_OPERATION_REL_DTO temp = dao.SelectOperation(product_code);
         dao.Dispose();

         return temp;
      }

      public string SPGetLot(string orderId)
      {
         Order_DAO dao = new Order_DAO();
         string temp = dao.SPGetLot(orderId);
         dao.Dispose();

         return temp;
      }
   }
}
