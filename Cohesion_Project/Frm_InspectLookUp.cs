using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cohesion_DTO;
using Cohesion_Project.Service;

namespace Cohesion_Project
{

    public partial class Frm_NonOperLookUp : Form
    {
        Srv_Inspect srv = new Srv_Inspect();
        List<LOT_INSPECT_HIS_DTO> inspect = null;
        public Frm_NonOperLookUp()
        {
            InitializeComponent();
        }

        private void Frm_NonOperLookUp_Load(object sender, EventArgs e)
        {
            DataGridView();
            DgvDataBinding();
        }

        private void DataGridView()
        {
            DgvUtil.DgvInit(dgvInspectList);
            DgvUtil.AddTextCol(dgvInspectList, "자재 코드", "LOT_ID", width: 190, readOnly: true, 0, frozen: true);
            DgvUtil.AddTextCol(dgvInspectList, "순번", "HIST_SEQ", width: 70, readOnly: true, 2, frozen: true);
            DgvUtil.AddTextCol(dgvInspectList, "검사 항목명", "INSPECT_ITEM_NAME", width: 180, readOnly: true, 1);
            DgvUtil.AddTextCol(dgvInspectList, "스펙 하한값", "SPEC_LSL", width: 120, readOnly: true, 2);
            DgvUtil.AddTextCol(dgvInspectList, "스펙 타겟값", "SPEC_TARGET", width: 120, readOnly: true, 2);
            DgvUtil.AddTextCol(dgvInspectList, "스펙 상한값", "SPEC_USL", width: 120, readOnly: true, 2);
            DgvUtil.AddTextCol(dgvInspectList, "검사 데이터 값", "INSPECT_VALUE", width: 150, readOnly: true, 2);
            DgvUtil.AddTextCol(dgvInspectList, "검사 결과", "INSPECT_RESULT", width: 100, readOnly: true, 2);
            DgvUtil.AddTextCol(dgvInspectList, "검사 처리 시간", "TRAN_TIME", width: 200, readOnly: true, 1);
            DgvUtil.AddTextCol(dgvInspectList, "작업 일자", "WORK_DATE", width: 100, readOnly: true, 1);
            DgvUtil.AddTextCol(dgvInspectList, "자재명", "PRODUCT_CODE", width: 150, readOnly: true, 0);
            DgvUtil.AddTextCol(dgvInspectList, "공정명", "OPERATION_CODE", width: 150, readOnly: true, 0);
            DgvUtil.AddTextCol(dgvInspectList, "창고명", "STORE_CODE", width: 100, readOnly: true, 0);
            DgvUtil.AddTextCol(dgvInspectList, "설비명", "EQUIPMENT_CODE", width: 80, readOnly: true, 0);
            DgvUtil.AddTextCol(dgvInspectList, "처리 사용자", "TRAN_USER_ID", width: 120, readOnly: true, 0);
            DgvUtil.AddTextCol(dgvInspectList, "처리 주석", "TRAN_COMMENT", width: 100, readOnly: true, 0);
            dgvInspectList.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
        }

        private void DgvDataBinding()
        {
            inspect = new List<LOT_INSPECT_HIS_DTO>();
            inspect = srv.GetInspectHisAllList();
            dgvInspectList.DataSource = null;
            dgvInspectList.DataSource = inspect;
        }
    }
}
