using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cohesion_DAO;
using Cohesion_DTO;

namespace Cohesion_Project
{
    public class Srv_Purchase
    {
        public List<PURCHASE_ORDER_MST_DTO> GetAllPurchaseList()
        {
            Purchase_DAO db = new Purchase_DAO();
            List<PURCHASE_ORDER_MST_DTO> list = db.GetAllPurchaseList();
            db.Dispose();

            return list;
        }

        public List<PURCHASE_ORDER_MST_DTO> SelectPurchaseList(string orderId)
        {
            Purchase_DAO db = new Purchase_DAO();
            List<PURCHASE_ORDER_MST_DTO> list = db.SelectPurchaseList(orderId);
            db.Dispose();

            return list;
        }

        public bool UpdatePurchaseData(List<PURCHASE_ORDER_MST_DTO> dto)
        {
            Purchase_DAO db = new Purchase_DAO();
            bool result = db.UpdatePurchaseData(dto);
            db.Dispose();

            return result;
        }
    }
}
