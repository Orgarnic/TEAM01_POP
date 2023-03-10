using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cohesion_DTO
{
	public class INSPECT_ITEM_MST_DTO
	{
		public string INSPECT_ITEM_CODE { get; set; }	 //검사항목 코드
		public string INSPECT_ITEM_NAME { get; set; }	 //검사항목명
		public char VALUE_TYPE { get; set; }	 //검사 데이터 유형. 문자 : C, 숫자 : N
		public string SPEC_LSL { get; set; }	 //스펙 하한값
		public string SPEC_TARGET { get; set; }	 //스펙 타겟값
		public string SPEC_USL { get; set; }	 //스펙 상한값
		public DateTime CREATE_TIME { get; set; }	 //생성 시간 생성 시간
		public string CREATE_USER_ID { get; set; }	 //생성 사용자
		public DateTime UPDATE_TIME { get; set; }	 //변경 시간
		public string UPDATE_USER_ID { get; set; }	 //변경 사용자
	}
}