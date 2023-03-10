using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cohesion_DTO
{
	public class PRODUCT_OPERATION_REL_DTO
	{
		public string PRODUCT_CODE { get; set; }	 //제품 코드, 품번
		public string OPERATION_CODE { get; set; }	 //생산 공정
		public decimal FLOW_SEQ { get; set; }	 //공정흐름 순번
		public DateTime CREATE_TIME { get; set; }	 //생성 시간
		public string CREATE_USER_ID { get; set; }	 //생성 사용자
		public DateTime UPDATE_TIME { get; set; }	 //변경 시간
		public string UPDATE_USER_ID { get; set; }    //변경 사용자

		public string PRODUCT_NAME { get; set; }   //제품 코드, 품번
		public string OPERATION_NAME { get; set; }    //생산 공정
	}
}