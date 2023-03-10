using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cohesion_DTO
{
	public class CODE_DATA_MST_DTO
	{
		public string CODE_TABLE_NAME { get; set; }	 //코드 테이블명
		public string KEY_1 { get; set; }	 //키 1 값
		public string KEY_2 { get; set; }	 //키 2 값
		public string KEY_3 { get; set; }	 //키 3 값
		public string DATA_1 { get; set; }	 //데이터 1 값
		public string DATA_2 { get; set; }	 //데이터 2 값
		public string DATA_3 { get; set; }	 //데이터 3 값
		public string DATA_4 { get; set; }	 //데이터 4 값
		public string DATA_5 { get; set; }	 //데이터 5 값
		public decimal DISPLAY_SEQ { get; set; }	 //값 표시 순서
		public DateTime CREATE_TIME { get; set; }	 //생성 시간
		public string CREATE_USER_ID { get; set; }	 //생성 사용자
		public DateTime UPDATE_TIME { get; set; }	 //변경 시간
		public string UPDATE_USER_ID { get; set; }	 //변경 사용자
	}
}