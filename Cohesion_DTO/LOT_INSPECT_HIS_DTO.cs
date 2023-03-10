using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cohesion_DTO
{
	public class LOT_INSPECT_HIS_DTO
	{
		public string LOT_ID { get; set; }	 //LOT ID. 원자재, 반제품, 완제품의 모든 LOT ID
		public decimal HIST_SEQ { get; set; }	 //이력 순번
		public string INSPECT_ITEM_CODE { get; set; }	 //검사 항목 코드
		public string INSPECT_ITEM_NAME { get; set; }	 //검사 항목명
		public char VALUE_TYPE { get; set; }	 //검사 데이터 유형. 문자 : C, 숫자 : N 스펙 하한값
		public string SPEC_LSL { get; set; }	 //스펙 하한값
		public string SPEC_TARGET { get; set; }	 //스펙 타겟값
		public string SPEC_USL { get; set; }	 //스펙 상한값
		public string INSPECT_VALUE { get; set; }	 //검사 데이터 값
		public string INSPECT_RESULT { get; set; }	 //검사 결과. OK/NG
		public DateTime TRAN_TIME { get; set; }	 //처리 시간
		public string WORK_DATE { get; set; }	 //작업일자
		public string PRODUCT_CODE { get; set; }	 //품번. 원자재인 경우 원자재 품번, 반제품은 반제품 품번, 완제품은 완제품 품번을 가짐
		public string OPERATION_CODE { get; set; }	 //공정 코드. 생산 중인 경우 공정 코드를 가짐
		public string STORE_CODE { get; set; }	 //창고 코드. 창고에 들어간 경우 창고 코드를 가짐
		public string EQUIPMENT_CODE { get; set; }	 //설비 코드
		public string TRAN_USER_ID { get; set; }	 //처리 사용자
		public string TRAN_COMMENT { get; set; }	 //처리 주석
	}
}