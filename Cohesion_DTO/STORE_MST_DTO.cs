using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cohesion_DTO
{
	public class STORE_MST_DTO
	{
		public string STORE_CODE { get; set; }	 //창고 코드
		public string STORE_NAME { get; set; }	 //창고명
		public string STORE_TYPE { get; set; }	 //창고 타입. 원자재창고 : RS, 반제품창고 : HS, 완제품창고 : FS
		public char FIFO_FLAG { get; set; }	 //선입선출여부. 미선입선출 : null, 선입선출 : 'Y'
		public DateTime CREATE_TIME { get; set; }	 //생성 시간
		public string CREATE_USER_ID { get; set; }	 //생성 사용자
		public DateTime UPDATE_TIME { get; set; }	 //변경 시간
		public string UPDATE_USER_ID { get; set; }	 //변경 사용자
	}
}