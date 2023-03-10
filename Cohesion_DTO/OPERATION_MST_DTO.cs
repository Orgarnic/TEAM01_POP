using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cohesion_DTO
{
	public class OPERATION_MST_DTO
	{
		public string OPERATION_CODE { get; set; }	 //공정 코드
		public string OPERATION_NAME { get; set; }	 //공정명
		public char CHECK_DEFECT_FLAG { get; set; }	 //불량 입력 체크 여부. 미체크 : null, 체크 : Y
		public char CHECK_INSPECT_FLAG { get; set; }	 //검사 데이터 입력 체크 여부. 미체크 : null, 체크 : Y
		public char CHECK_MATERIAL_FLAG { get; set; }	 //자재 사용 체크 여부. 미체크 : null, 체크 : Y
		public DateTime CREATE_TIME { get; set; }	 //생성 시간
		public string CREATE_USER_ID { get; set; }	 //생성 사용자
		public DateTime UPDATE_TIME { get; set; }	 //변경 시간
		public string UPDATE_USER_ID { get; set; }	 //변경 사용자
	}
}