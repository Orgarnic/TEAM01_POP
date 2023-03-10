using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cohesion_DTO
{
	public class WORK_ORDER_MST_DTO
	{
		public string WORK_ORDER_ID { get; set; }	 //생산 작업지시 코드
		public DateTime ORDER_DATE { get; set; }	 //작업 일자
		public string PRODUCT_CODE { get; set; }	 //생산 제품코드, 품번
		public string CUSTOMER_CODE { get; set; }	 //고객사 코드
		public decimal ORDER_QTY { get; set; }	 //계획 수량
		public string ORDER_STATUS { get; set; }	 //지시 상태. 최초 : OPEN, 생산 중 : PROC, 마감 : CLOSE
		public decimal PRODUCT_QTY { get; set; }	 //생산 수량
		public decimal DEFECT_QTY { get; set; }	 //불량 수량
		public DateTime WORK_START_TIME { get; set; }	 //작업 시작 시간
		public DateTime WORK_CLOSE_TIME { get; set; }	 //지시 마감 시간
		public string WORK_CLOSE_USER_ID { get; set; }	 //지시 마감자
		public DateTime CREATE_TIME { get; set; }	 //작업지시 생성 시간
		public string CREATE_USER_ID { get; set; }	 //작업지시 생성 사용자
		public DateTime UPDATE_TIME { get; set; }	 //작업지시 변경 시간
		public string UPDATE_USER_ID { get; set; }    //작업지시 변경 사용자

		public string PRODUCT_NAME { get; set; }   //생산 제품명
		public string CUSTOMER_NAME { get; set; }   //고객사 명
	}
}