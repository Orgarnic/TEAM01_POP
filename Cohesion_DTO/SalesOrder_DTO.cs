using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cohesion_DTO
{
    public class SalesOrder_DTO
    {
        public string SALES_ORDER_ID { get; set; }
        public DateTime ORDER_DATE { get; set; }
        public string CUSTOMER_CODE { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string PRODUCT_CODE { get; set; }
        public string PRODUCT_NAME { get; set; }
        public decimal ORDER_QTY { get; set; }
        public string CONFIRM_FLAG { get; set; }
        public string SHIP_FLAG { get; set; }
    }
}
