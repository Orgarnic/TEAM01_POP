using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cohesion_DTO
{
	public class INSPECT_ITEM_OPERATION_REL_DTO
	{
		public string OPERATION_CODE { get; set; }	 //공정코드
		public string INSPECT_ITEM_CODE { get; set; }	 //검사항목 코드
		public DateTime CREATE_TIME { get; set; }	 //생성 시간
		public string CREATE_USER_ID { get; set; }	 //생성 사용자
		public DateTime UPDATE_TIME { get; set; }	 //변경 시간
		public string UPDATE_USER_ID { get; set; }	 //변경 사용자
	}
}