using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cohesion_DTO
{
	public class CODE_TABLE_MST_DTO
	{
		public string CODE_TABLE_NAME { get; set; }	 //코드 테이블명
		public string CODE_TABLE_DESC { get; set; }	 //테이블 설명
		public string KEY_1_NAME { get; set; }	 //키 1 이름
		public string KEY_2_NAME { get; set; }	 //키 2 이름
		public string KEY_3_NAME { get; set; }	 //키 3 이름
		public string DATA_1_NAME { get; set; }	 //데이터 1 이름
		public string DATA_2_NAME { get; set; }	 //데이터 2 이름
		public string DATA_3_NAME { get; set; }	 //데이터 3 이름
		public string DATA_4_NAME { get; set; }	 //데이터 4 이름
		public string DATA_5_NAME { get; set; }	 //데이터 5 이름
		public DateTime CREATE_TIME { get; set; }	 //생성 시간
		public string CREATE_USER_ID { get; set; }	 //생성 사용자
		public DateTime UPDATE_TIME { get; set; }	 //변경 시간
		public string UPDATE_USER_ID { get; set; }	 //변경 사용자
	}
}