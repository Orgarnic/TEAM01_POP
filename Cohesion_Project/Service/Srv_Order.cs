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
      public List<WORK_ORDER_MST_DTO> SelectOrder()
      {
         Order_DAO dao = new Order_DAO();
         List<WORK_ORDER_MST_DTO> list = dao.SelectOrder();
         dao.Dispose();

         return list;
      }
   }
}
