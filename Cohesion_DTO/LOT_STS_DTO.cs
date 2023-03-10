using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cohesion_DTO
{
	public class LOT_STS_DTO : IEquatable<LOT_STS_DTO>
	{
		public string LOT_ID { get; set; }	 //LOT ID. 원자재, 반제품, 완제품의 모든 LOT ID
		public string LOT_DESC { get; set; }	 //LOT 설명
		public string PRODUCT_CODE { get; set; }	 //품번. 원자재인 경우 원자재 품번, 반제품은 반제품 품번, 완제품은 완제품 품번을 가짐
		public string OPERATION_CODE { get; set; }	 //공정 코드. 생산 중인 경우 공정 코드를 가짐
		public string STORE_CODE { get; set; }	 //창고 코드. 창고에 들어간 경우 창고 코드를 가짐
		public decimal LOT_QTY { get; set; }	 //LOT 수량
		public decimal CREATE_QTY { get; set; }	 //LOT 생성 시 수량
		public decimal OPER_IN_QTY { get; set; }	 //LOT 이 공정 투입될때의 수량
		public char START_FLAG { get; set; }	 //LOT 공정 작업 시작 여부
		public decimal START_QTY { get; set; }	 //작업 시작 시 수량
		public DateTime START_TIME { get; set; }	 //작업 시작 시간
		public string START_EQUIPMENT_CODE { get; set; }	 //작업 시작 설비
		public char END_FLAG { get; set; }	 //LOT 공정 작업 완료 여부
		public DateTime END_TIME { get; set; }	 //작업 완료 시간
		public string END_EQUIPMENT_CODE { get; set; }	 //작업 완료 설비
		public char SHIP_FLAG { get; set; }	 //출하 여부
		public string SHIP_CODE { get; set; }	 //출하 코드
		public DateTime SHIP_TIME { get; set; }	 //출하 시간
		public DateTime PRODUCTION_TIME { get; set; }	 //LOT 생산 일자. 원자재인 경우 납품업체에서의 생산 시간, 완제품인 경우 완제품 작업 완료 시간
		public DateTime CREATE_TIME { get; set; }	 //LOT 생성 시간
		public DateTime OPER_IN_TIME { get; set; }	 //LOT 이 공정 투입될때의 시간
		public string WORK_ORDER_ID { get; set; }	 //작업지시
		public char LOT_DELETE_FLAG { get; set; }	 //LOT 삭제 여부
		public string LOT_DELETE_CODE { get; set; }	 //LOT 삭제 코드
		public DateTime LOT_DELETE_TIME { get; set; }	 //LOT 삭제 시간
		public string LAST_TRAN_CODE { get; set; }	 //마지막 처리 코드
		public DateTime LAST_TRAN_TIME { get; set; }	 //마지막 처리 시간
		public string LAST_TRAN_USER_ID { get; set; }	 //마지막 처리 사용자
		public string LAST_TRAN_COMMENT { get; set; }	 //마지막 처리 주석
		public decimal LAST_HIST_SEQ { get; set; }   //마지막 이력 순번

		// 추가 
      public long DISPLAY_SEQ { get; set; }         // 출고시 순번 부여 지환 추가
		public string OPERATION_NAME { get; set; }    //공정 코드. 생산 중인 경우 공정 코드를 가짐
		public decimal LOT_DEFECT_QTY { get; set; }    //공정 코드. 생산 중인 경우 공정 코드를 가짐
		public decimal REQUIRE_QTY { get; set; }   //단위 수량
		public string ALTER_PRODUCT_CODE { get; set; }   //대체 품번
		public string PRODUCT_NAME { get; set; }   //품명

      public bool Equals(LOT_STS_DTO other)
      {
			return PRODUCT_CODE.Equals(other.PRODUCT_CODE);
      }
      public override int GetHashCode()
      {
			return PRODUCT_CODE.GetHashCode();
      }
   }
}