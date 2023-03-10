using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cohesion_DTO
{
	public class LOT_MATERIAL_HIS_DTO
	{
		public string LOT_ID { get; set; }	 //LOT ID. 원자재, 반제품, 완제품의 모든 LOT ID
		public decimal HIST_SEQ { get; set; }	 //이력 순번
		public string MATERIAL_LOT_ID { get; set; }	 //자재 LOT ID
		public decimal MATERIAL_LOT_HIST_SEQ { get; set; }	 //자재 LOT 이력 순번
		public string INPUT_QTY { get; set; }	 //자재 사용 수량
		public string CHILD_PRODUCT_CODE { get; set; }	 //자재 품번
		public string MATERIAL_STORE_CODE { get; set; }	 //자재 LOT 이 위치한 창고 코드
		public DateTime TRAN_TIME { get; set; }	 //처리 시간
		public string WORK_DATE { get; set; }	 //작업일자
		public string PRODUCT_CODE { get; set; }	 //품번. 원자재인 경우 원자재 품번, 반제품은 반제품 품번, 완제품은 완제품 품번을 가짐
		public string OPERATION_CODE { get; set; }	 //공정 코드. 생산 중인 경우 공정 코드를 가짐
		public string EQUIPMENT_CODE { get; set; }	 //설비 코드
		public string TRAN_USER_ID { get; set; }	 //처리 사용자
		public string TRAN_COMMENT { get; set; }	 //처리 주석
	}
}