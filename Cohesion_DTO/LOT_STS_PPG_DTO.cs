using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace Cohesion_DTO
{
    public class LOT_STS_PPG_DTO
    {
		//LOT ID. 원자재, 반제품, 완제품의 모든 LOT ID
		[Category("속성"), Description("LOT_ID"), DisplayName("LOT ID")]
		public string		LOT_ID					{ get; set; }

		//LOT 설명
		[Category("속성"), Description("LOT_DESC"), DisplayName("LOT 설명")]
		public string		LOT_DESC				{ get; set; }

		//품번. 원자재인 경우 원자재 품번, 반제품은 반제품 품번, 완제품은 완제품 품번을 가짐
		[Category("속성"), Description("PRODUCT_CODE"), DisplayName("제품 코드")]
		public string		PRODUCT_CODE			{ get; set; }

		//품명
		[Category("속성"), Description("PRODUCT_NAME"), DisplayName("제품 명")]
		public string		PRODUCT_NAME			{ get; set; }

		//공정 코드. 생산 중인 경우 공정 코드를 가짐
		[Category("속성"), Description("OPERATION_CODE"), DisplayName("공정 코드")]
		public string		OPERATION_CODE			{ get; set; }

		//창고 코드. 창고에 들어간 경우 창고 코드를 가짐
		[Category("속성"), Description("STORE_CODE"), DisplayName("창고 코드")]
		public string		STORE_CODE				{ get; set; }

		//LOT 수량
		[Category("속성"), Description("LOT_QTY"), DisplayName("LOT 수량")]
		public decimal		LOT_QTY					{ get; set; }

		//LOT 생성 시 수량
		[Category("속성"), Description("CREATE_QTY"), DisplayName("LOT 생성 시 수량")]
		public decimal		CREATE_QTY				{ get; set; }

		//LOT 이 공정 투입될때의 수량
		[Category("속성"), Description("OPER_IN_QTY"), DisplayName("LOT 생성 수량")]
		public decimal		OPER_IN_QTY				{ get; set; }

		//LOT 공정 작업 시작 여부
		[Category("속성"), Description("START_FLAG"), DisplayName("공정 시작 여부")]
		public char			START_FLAG				{ get; set; }

		//작업 시작 시 수량
		[Category("속성"), Description("START_QTY"), DisplayName("작업 시작 수량")]
		public decimal		START_QTY				{ get; set; }

		//작업 시작 시간
		[Category("속성"), Description("START_TIME"), DisplayName("작업 시작 시간")]
		public DateTime		START_TIME				{ get; set; }

		//작업 시작 설비
		[Category("속성"), Description("START_EQUIPMENT_CODE"), DisplayName("작업 시작 설비")]
		public string		START_EQUIPMENT_CODE	{ get; set; }

		//LOT 공정 작업 완료 여부
		[Category("속성"), Description("END_FLAG"), DisplayName("공정 완료 여부")]
		public char			END_FLAG				{ get; set; }

		//작업 완료 시간
		[Category("속성"), Description("END_TIME"), DisplayName("공정 완료 시간")]
		public DateTime		END_TIME				{ get; set; }

		//작업 완료 설비
		[Category("속성"), Description("END_EQUIPMENT_CODE"), DisplayName("작업 완료 설비")]
		public string		END_EQUIPMENT_CODE		{ get; set; }

		//출하 여부
		[Category("속성"), Description("SHIP_FLAG"), DisplayName("출하 여부")]
		public char			SHIP_FLAG				{ get; set; }

		//출하 코드
		[Category("속성"), Description("SHIP_CODE"), DisplayName("출하 코드")]
		public string		SHIP_CODE				{ get; set; }

		//출하 시간
		[Category("속성"), Description("SHIP_TIME"), DisplayName("출하 시간")]
		public DateTime		SHIP_TIME				{ get; set; }

		//LOT 생산 일자. 원자재인 경우 납품업체에서의 생산 시간, 완제품인 경우 완제품 작업 완료 시간
		[Category("속성"), Description("PRODUCTION_TIME"), DisplayName("LOT 생산 일자")]
		public DateTime		PRODUCTION_TIME			{ get; set; }

		//LOT 생성 시간
		[Category("속성"), Description("CREATE_TIME"), DisplayName("LOT 생산 시간")]
		public DateTime		CREATE_TIME				{ get; set; }

		//LOT 이 공정 투입될때의 시간
		[Category("속성"), Description("OPER_IN_TIME"), DisplayName("LOT 공정 투입 시간")]
		public DateTime		OPER_IN_TIME			{ get; set; }

		//작업지시
		[Category("속성"), Description("WORK_ORDER_ID"), DisplayName("작업지시")]
		public string		WORK_ORDER_ID			{ get; set; }

		//LOT 삭제 여부
		[Category("추적"), Description("LOT_DELETE_FLAG"), DisplayName("LOT 삭제 여부"), ReadOnly(true)]
		public char			LOT_DELETE_FLAG			{ get; set; }

		//LOT 삭제 코드
		[Category("추적"), Description("LOT_DELETE_CODE"), DisplayName("LOT 삭제 코드"), ReadOnly(true)]
		public string		LOT_DELETE_CODE			{ get; set; }

		//LOT 삭제 시간
		[Category("추적"), Description("LOT_DELETE_TIME"), DisplayName("LOT 삭제 시간"), ReadOnly(true)]
		public DateTime		LOT_DELETE_TIME			{ get; set; }

		//마지막 처리 코드
		[Category("추적"), Description("LAST_TRAN_CODE"), DisplayName("마지막 처리 코드"), ReadOnly(true)]
		public string		LAST_TRAN_CODE			{ get; set; }

		//마지막 처리 시간
		[Category("추적"), Description("LAST_TRAN_TIME"), DisplayName("마지막 처리 시간"), ReadOnly(true)]
		public DateTime		LAST_TRAN_TIME			{ get; set; }

		//마지막 처리 사용자
		[Category("추적"), Description("LAST_TRAN_USER_ID"), DisplayName("마지막 처리 사용자"), ReadOnly(true)]
		public string		LAST_TRAN_USER_ID		{ get; set; }

		//마지막 처리 주석
		[Category("추적"), Description("LAST_TRAN_COMMENT"), DisplayName("마지막 처리 주석"), ReadOnly(true)]
		public string		LAST_TRAN_COMMENT		{ get; set; }

		//마지막 이력 순번
		[Category("추적"), Description("LAST_HIST_SEQ"), DisplayName("마지막 이력 순번"), ReadOnly(true)]
		public decimal		LAST_HIST_SEQ			{ get; set; }   
	}

	public class LOT_STS_VO
    {
		public string		LOT_ID					{ get; set; }
		public string		LOT_DESC				{ get; set; }
		public string		PRODUCT_CODE			{ get; set; }
		public string		PRODUCT_NAME			{ get; set; }
		public string		OPERATION_CODE			{ get; set; }
		public string		STORE_CODE				{ get; set; }
		public decimal		LOT_QTY					{ get; set; }
		public decimal		CREATE_QTY				{ get; set; }
		public decimal		OPER_IN_QTY				{ get; set; }
		public char			START_FLAG				{ get; set; }
		public decimal		START_QTY				{ get; set; }
		public DateTime		START_TIME				{ get; set; }
		public string		START_EQUIPMENT_CODE	{ get; set; }
		public char			END_FLAG				{ get; set; }
		public DateTime		END_TIME				{ get; set; }
		public string		END_EQUIPMENT_CODE		{ get; set; }
		public char			SHIP_FLAG				{ get; set; }
		public string		SHIP_CODE				{ get; set; }
		public DateTime		SHIP_TIME				{ get; set; }
		public DateTime		PRODUCTION_TIME			{ get; set; }
		public DateTime		CREATE_TIME				{ get; set; }
		public DateTime		OPER_IN_TIME			{ get; set; }
		public string		WORK_ORDER_ID			{ get; set; }
		public char			LOT_DELETE_FLAG			{ get; set; }
		public string		LOT_DELETE_CODE			{ get; set; }
		public DateTime		LOT_DELETE_TIME			{ get; set; }
		public string		LAST_TRAN_CODE			{ get; set; }
		public DateTime		LAST_TRAN_TIME			{ get; set; }
		public string		LAST_TRAN_USER_ID		{ get; set; }
		public string		LAST_TRAN_COMMENT		{ get; set; }
		public decimal		LAST_HIST_SEQ			{ get; set; }
	}
	public class LOT_HIS_VO
    {
		public string LOT_ID { get; set; }   //LOT ID. 원자재, 반제품, 완제품의 모든 LOT ID
		public decimal HIST_SEQ { get; set; }    //이력 순번
		public DateTime TRAN_TIME { get; set; }  //처리 시간
		public string TRAN_CODE { get; set; }    //처리 코드
		public string LOT_DESC { get; set; }     //LOT 설명
		public string PRODUCT_CODE { get; set; }     //품번. 원자재인 경우 원자재 품번, 반제품은 반제품 품번, 완제품은 완제품 품번을 가짐
		public string OPERATION_CODE { get; set; }   //공정 코드. 생산 중인 경우 공정 코드를 가짐
		public string OPERATION_NAME { get; set; }
		public string STORE_CODE { get; set; }   //창고 코드. 창고에 들어간 경우 창고 코드를 가짐
		public decimal LOT_QTY { get; set; }     //LOT 수량
		public decimal CREATE_QTY { get; set; }  //LOT 생성 시 수량
		public decimal OPER_IN_QTY { get; set; }     //LOT 이 공정 투입될때의 수량
		public char START_FLAG { get; set; }     //LOT 공정 작업 시작 여부
		public decimal START_QTY { get; set; }   //작업 시작 시 수량
		public DateTime START_TIME { get; set; }     //작업 시작 시간
		public string START_EQUIPMENT_CODE { get; set; }     //작업 시작 설비
		public char END_FLAG { get; set; }   //LOT 공정 작업 완료 여부
		public DateTime END_TIME { get; set; }   //작업 완료 시간
		public string END_EQUIPMENT_CODE { get; set; }   //작업 완료 설비
		public char SHIP_FLAG { get; set; }  //출하 여부
		public string SHIP_CODE { get; set; }    //출하 코드
		public DateTime SHIP_TIME { get; set; }  //출하 시간
		public DateTime PRODUCTION_TIME { get; set; }    //LOT 생산 일자. 원자재인 경우 납품업체에서의 생산 시간, 완제품인 경우 완제품 작업 완료 시간
		public DateTime CREATE_TIME { get; set; }    //LOT 생성 시간
		public DateTime OPER_IN_TIME { get; set; }   //LOT 이 공정 투입될때의 시간
		public string WORK_ORDER_ID { get; set; }    //작업지시
		public char LOT_DELETE_FLAG { get; set; }    //LOT 삭제 여부
		public string LOT_DELETE_CODE { get; set; }  //LOT 삭제 코드
		public DateTime LOT_DELETE_TIME { get; set; }    //LOT 삭제 시간
		public string WORK_DATE { get; set; }    //작업 일자
		public string TRAN_USER_ID { get; set; }     //처리 사용자
		public string TRAN_COMMENT { get; set; }     //처리 주석
		public string OLD_PRODUCT_CODE { get; set; }     //이전 이력의 품번
		public string OLD_OPERATION_CODE { get; set; }   //이전 이력의 공정
		public string OLD_STORE_CODE { get; set; }   //이전 이력의 창고
		public decimal OLD_LOT_QTY { get; set; }     //이전 이력의 LOT 수량
	}
	public class LOT_DTO_Search
	{
		[Category("검색조건"), Description("CM_OPERATION"), DisplayName("공정"), TypeConverter(typeof(ComboStringConverter))]
		public string OPERATION_CODE { get; set; }


		[Category("검색조건"), Description("CM_ST_CODE"), DisplayName("창고"), TypeConverter(typeof(ComboStringConverter))]
		public string STORE_CODE { get; set; }


		[Category("검색조건"), Description("CM_PRODUCT_CODE"), DisplayName("품번"), TypeConverter(typeof(ComboStringConverter))]
		public string PRODUCT_CODE { get; set; }


		[Category("검색조건"), Description("LOT_ID"), DisplayName("LOT ID")]
		public string LOT_ID { get; set; }
	}

}
