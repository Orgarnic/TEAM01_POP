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
   }
}
