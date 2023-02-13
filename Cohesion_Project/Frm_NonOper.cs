using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cohesion_DTO;
using System.Linq;


namespace Cohesion_Project
{
    public partial class Frm_NonOper : Cohesion_Project.Frm_Base3Line
    {
        private List<EQUIP_DOWN_DTO> edList;
        private Srv_ED srv_ED = new Srv_ED();
        List<CODE_DATA_MST_DTO> list;


        public Frm_NonOper()
        {
            InitializeComponent();
        }


        private void Frm_NonOper_Load(object sender, EventArgs e)
        {
            list = srv_ED.Combo();
            comboBox1.Items.AddRange(list.Select((Q)=> Q.KEY_1).ToArray());
            comboBox2.Items.AddRange(srv_ED.EQCombo().Select((q) => q.EQUIPMENT_CODE).ToArray());
            DgvInit();
            DataGridViewFill();
        }

        private void DataGridViewFill()
        {
        
            edList = srv_ED.SelectEDown();
            dataGridView1.DataSource = edList;

        }

        private void DgvInit()
        {

            DgvUtil.DgvInit(dataGridView1);
            DgvUtil.AddTextCol(dataGridView1, "설비 코드", "EQUIPMENT_CODE", width: 80);
            DgvUtil.AddTextCol(dataGridView1, "비가동 일자", "DT_DATE", width: 180, readOnly: true);
            DgvUtil.AddTextCol(dataGridView1, "비가동 시작 시간", "DT_START_TIME", width: 180, readOnly: true);
            DgvUtil.AddTextCol(dataGridView1, "비가동 종료 시간", "DT_END_TIME", width: 180, readOnly: true);
            DgvUtil.AddTextCol(dataGridView1, "비가동 시간(분)", "DT_TIME", width: 250);
            DgvUtil.AddTextCol(dataGridView1, "비가동 코드", "DT_CODE", width: 130, readOnly: true);
            DgvUtil.AddTextCol(dataGridView1, "비가동 주석", "DT_COMMENT", width: 130, readOnly: true);
            DgvUtil.AddTextCol(dataGridView1, "비가동 등록자", "DT_USER_ID", width: 130, readOnly: true);
            DgvUtil.AddTextCol(dataGridView1, "조치 내역", "ACTION_COMMENT", width: 130, readOnly: true);
       //     DgvUtil.AddTextCol(dataGridView1, "확인 시간", "CONFIRM_TIME", width: 130, readOnly: true);
          // DgvUtil.AddTextCol(dataGridView1, "확인자", "CONFIRM_USER_ID", width: 130, readOnly: true);
            dataGridView1.Font = new Font("맑은 고딕", 10, FontStyle.Bold);


        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        public string dtFrom
        {
            get { return dateTimePicker3.Value.ToString("yyyy-MM-dd"); }
            set { dateTimePicker3.Value = Convert.ToDateTime(value); }
        }

        public string dtTo
        {
            get { return dateTimePicker4.Value.AddDays(1).ToString("yyyy-MM-dd"); }
            set { dateTimePicker4.Value = Convert.ToDateTime(value); }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string dtFrom = dateTimePicker3.Value.ToString("yyyyMMdd");

            string dtTo = dateTimePicker4.Value.ToString("yyyyMMdd");


            edList = srv_ED.SelectEDown1(dtFrom, dtTo);
           // var dtlist = edList.FindAll((o) => Convert.ToDateTime(o.DT_START_TIME) < from && Convert.ToDateTime(o.DT_START_TIME) > to);
            dataGridView1.DataSource = edList;

        }


        private void button1_Click(object sender, EventArgs e)
        {

            EQUIP_DOWN_DTO dto = new EQUIP_DOWN_DTO();
            {
                dto.EQUIPMENT_CODE = comboBox2.Text;
                dto.DT_CODE = comboBox1.Text;
                dto.DT_COMMENT = textBox3.Text;
                dto.DT_TIME = Convert.ToDecimal(textBox1.Text);
                dto.DT_START_TIME = dateTimePicker1.Value;
                dto.DT_END_TIME = dateTimePicker2.Value;
                dto.ACTION_COMMENT = textBox8.Text;
                dto.DT_USER_ID = "김민식";
                dto.DT_DATE = DateTime.Now.ToString("yyyyMMdd");
                //  dto.CONFIRM_TIME =  null;
                //  dto.CONFIRM_USER_ID = "김민식";




            }
            bool result = srv_ED.InsertEDown(dto);
            if (result)
            {
                MboxUtil.MboxInfo("공정이 등록되었습니다.");
                DataGridViewFill();
            }
            else
                MboxUtil.MboxError("오류가 발생했습니다.");
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime StartDate = dateTimePicker1.Value;

            DateTime EndDate = dateTimePicker2.Value;

            TimeSpan timedipp = (dateTimePicker2.Value) - (dateTimePicker1.Value);
           // timedipp = TimeSpan.FromMinutes(Convert.ToDouble(timedipp));

            textBox1.Text = timedipp.TotalMinutes.ToString().Split('.')[0];

        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = list.Find((q) => q.KEY_1.Equals(comboBox1.Text)).DATA_1;
            textBox7.Text = str;
        }
    }
}
