using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cohesion_DAO;
using Cohesion_DTO;

namespace Cohesion_Project.Service
{
    public class Srv_Ship
    {
        public List<SalesOrder_DTO> SelectOrderListToShip()
        {
            Order_DAO dao = new Order_DAO();
            var list = dao.SelectOrderListToShip();
            dao.Dispose();
            return list;
        }
        public List<LOT_STS_DTO> SelectProductListInStore(string productCode)
        {
            Ship_DAO dao = new Ship_DAO();
            var list = dao.SelectProductInStore(productCode);
            dao.Dispose();
            return list;
        }
        public bool InsertShipInfo(SalesOrder_DTO orderInfo, Dictionary<string, decimal> lotNumList)
        {
            Ship_DAO dao = new Ship_DAO();
            var result = dao.InsertShipInfo(orderInfo, lotNumList);
            dao.Dispose();
            return result;
        }
    }
}
