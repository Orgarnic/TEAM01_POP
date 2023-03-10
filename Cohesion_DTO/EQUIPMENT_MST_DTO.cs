using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cohesion_DTO
{
	public class EQUIPMENT_MST_DTO
	{
		public string EQUIPMENT_CODE { get; set; }	 //설비코드
		public string EQUIPMENT_NAME { get; set; }	 //설비명
		public string EQUIPMENT_TYPE { get; set; }	 //설비 유형. 장비 : EQUIP, 도구 : TOOL, 측정기 : INSP
		public string EQUIPMENT_STATUS { get; set; }	 //설비 상태. 가동 : PROC, 고장비가동 : DOWN, 일반비가동 : WAIT
		public DateTime LAST_DOWN_TIME { get; set; }	 //최근 고장비가동 시간
		public DateTime CREATE_TIME { get; set; }	 //생성 시간
		public string CREATE_USER_ID { get; set; }	 //생성 사용자
		public DateTime UPDATE_TIME { get; set; }	 //변경 시간
		public string UPDATE_USER_ID { get; set; }	 //변경 사용자
	}
}