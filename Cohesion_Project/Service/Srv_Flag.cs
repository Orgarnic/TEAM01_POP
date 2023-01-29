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
      public List<LOT_STS_DTO> SelectOrderLot(string orderId)
      {
         Flag_DAO dao = new Flag_DAO();
         List<LOT_STS_DTO> list = dao.SelectOrderLot(orderId);
         dao.Dispose();

         return list;
      }
   }
}
