using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cohesion_DTO
{
	public class PURCHASE_ORDER_MST_DTO : IEquatable<PURCHASE_ORDER_MST_DTO>
	{
		public string PURCHASE_ORDER_ID { get; set; }	 //구매 납품서 코드
		public decimal PURCHASE_SEQ { get; set; }	 //
		public string SALES_ORDER_ID { get; set; }	 //고객 주문서 코드
		public DateTime ORDER_DATE { get; set; }	 //구매발주 일자
		public string VENDOR_CODE { get; set; }	 //납품처 코드
		public string MATERIAL_CODE { get; set; }	 //자재 품번
		public decimal ORDER_QTY { get; set; }	 //발주 수량
		public char STOCK_IN_FLAG { get; set; }	 //입하 여부. 미입하 : null, 입하 : Y
		public string STOCK_IN_STORE_CODE { get; set; }	 //입하 창고 코드
		public string STOCK_IN_LOT_ID { get; set; }  //입하 자재 LOT ID

		// ===== 김재형 추가 =====
		public string PRODUCT_NAME { get; set; } // 제품명
        public string CUSTOMER_NAME { get; set; } // 회사명

        public bool Equals(PURCHASE_ORDER_MST_DTO other)
		{
			return PURCHASE_ORDER_ID.Equals(other.PURCHASE_ORDER_ID);
		}
		public override int GetHashCode()
		{
			return PURCHASE_ORDER_ID.GetHashCode();
		}
	}
}