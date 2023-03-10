using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cohesion_DTO
{
	public class BOM_MST_DTO
   {
		public string PRODUCT_CODE { get; set; }	 //제품 코드, 품번
		public string CHILD_PRODUCT_CODE { get; set; }	 //자품번
		public decimal REQUIRE_QTY { get; set; }	 //단위 수량
		public string ALTER_PRODUCT_CODE { get; set; }	 //대체 품번
		public DateTime CREATE_TIME { get; set; }	 //생성 시간
		public string CREATE_USER_ID { get; set; }	 //생성 사용자
		public DateTime UPDATE_TIME { get; set; }	 //변경 시간
		public string UPDATE_USER_ID { get; set; }    //변경 사용자
      public string PRODUCT_NAME { get; set; }

      /*		// 추가 사항
            public string LOT_ID { get; set; }    // 자재 LOT 아이디
            public string CHILD_PRODUCT_NAME { get; set; }   //자품번
            public decimal LOT_QTY { get; set; }    // 자재 LOT 수량
            public decimal LOT_QTY_TOTAL { get; set; }    // 자재 LOT 토탈
            public decimal LAST_HIST_SEQ { get; set; }    // 자재 LOT 토탈*/
   }
}