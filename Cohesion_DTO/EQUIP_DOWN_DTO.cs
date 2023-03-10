using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cohesion_DTO
{
	public class EQUIP_DOWN_DTO
	{
		public string EQUIPMENT_CODE { get; set; }	 //설비 코드
		public string DT_DATE { get; set; }	 //비가동 일자
		public DateTime DT_START_TIME { get; set; }	 //비가동 시작 시간
		public DateTime DT_END_TIME { get; set; }	 //비가동 종료 시간
		public decimal DT_TIME { get; set; }	 //비가동 시간(분)
		public string DT_CODE { get; set; }	 //비가동 코드
		public string DT_COMMENT { get; set; }	 //비가동 주석
		public string DT_USER_ID { get; set; }	 //비가동 등록자
		public string ACTION_COMMENT { get; set; }	 //조치 내역
		public DateTime CONFIRM_TIME { get; set; }	 //확인 시간
		public string CONFIRM_USER_ID { get; set; }	 //확인자
	}
}