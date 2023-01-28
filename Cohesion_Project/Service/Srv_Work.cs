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
   }
}
