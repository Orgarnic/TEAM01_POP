using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cohesion_DTO
{
    public class Ship_DTO
    {
        public string SALES_ORDER_ID { get; set; }
        public string LOT_ID { get; set; }
        public string PRODUCT_CODE { get; set; }
        public int SHIP_QTY { get; set; }
        public string SHIP_USER_ID { get; set; }
        public DateTime SHIP_TIME { get; set; }
    }
}
