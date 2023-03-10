using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cohesion_DTO
{
	public class LOT_END_HIS_DTO
	{
		public string LOT_ID { get; set; }	 //LOT ID. 원자재, 반제품, 완제품의 모든 LOT ID
		public decimal HIST_SEQ { get; set; }	 //이력 순번
		public DateTime TRAN_TIME { get; set; }	 //처리 시간
		public string WORK_DATE { get; set; }	 //작업일자
		public string PRODUCT_CODE { get; set; }	 //품번. 원자재인 경우 원자재 품번, 반제품은 반제품 품번, 완제품은 완제품 품번을 가짐
		public string OPERATION_CODE { get; set; }	 //LOT 작업 완료 공정
		public string EQUIPMENT_CODE { get; set; }	 //작업 완료 설비 코드
		public string TRAN_USER_ID { get; set; }	 //처리 사용자
		public string TRAN_COMMENT { get; set; }	 //처리 주석
		public string TO_OPERATION_CODE { get; set; }	 //작업 완료되어 이동된 공정 코드
		public decimal OPER_IN_QTY { get; set; }	 //LOT 이 공정 투입될때의 수량
		public decimal START_QTY { get; set; }	 //작업 시작 시 수량
		public decimal END_QTY { get; set; }	 //작업 완료 시 수량
		public DateTime OPER_IN_TIME { get; set; }	 //LOT 이 공정 투입될때의 시간
		public DateTime START_TIME { get; set; }	 //작업 시작 시간
		public decimal PROC_TIME { get; set; }	 //작업 완료 공정에서의 총 작업 시간(분)
		public string WORK_ORDER_ID { get; set; }	 //작업지시
	}
}