﻿using System;
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
            GetCategoriry();
        }

        private void DataGridView()
        {
            DgvUtil.DgvInit(dgvInspectList);
            DgvUtil.AddTextCol(dgvInspectList, "   자재 코드", "LOT_ID", width: 210, readOnly: true, 0, frozen: true);
            DgvUtil.AddTextCol(dgvInspectList, "   순번", "HIST_SEQ", width: 80, readOnly: true, 2, frozen: true);
            DgvUtil.AddTextCol(dgvInspectList, "   검사 항목명", "INSPECT_ITEM_NAME", width: 200, readOnly: true, 0);
            DgvUtil.AddTextCol(dgvInspectList, "   스펙 하한값", "SPEC_LSL", width: 130, readOnly: true, 2);
            DgvUtil.AddTextCol(dgvInspectList, "   스펙 타겟값", "SPEC_TARGET", width: 130, readOnly: true, 2);
            DgvUtil.AddTextCol(dgvInspectList, "   스펙 상한값", "SPEC_USL", width: 130, readOnly: true, 2);
            DgvUtil.AddTextCol(dgvInspectList, "   검사 데이터 값", "INSPECT_VALUE", width: 150, readOnly: true, 2);
            DgvUtil.AddTextCol(dgvInspectList, "   검사 결과", "INSPECT_RESULT", width: 120, readOnly: true, 1);
            DgvUtil.AddTextCol(dgvInspectList, "   검사 처리 시간", "TRAN_TIME", width: 200, readOnly: true, 1);
            DgvUtil.AddTextCol(dgvInspectList, "   작업 일자", "WORK_DATE", width: 120, readOnly: true, 0);
            DgvUtil.AddTextCol(dgvInspectList, "   자재명", "PRODUCT_CODE", width: 150, readOnly: true, 0);
            DgvUtil.AddTextCol(dgvInspectList, "   공정명", "OPERATION_CODE", width: 150, readOnly: true, 0);
            DgvUtil.AddTextCol(dgvInspectList, "   창고명", "STORE_CODE", width: 100, readOnly: true, 0);
            DgvUtil.AddTextCol(dgvInspectList, "   설비명", "EQUIPMENT_CODE", width: 100, readOnly: true, 0);
            DgvUtil.AddTextCol(dgvInspectList, "   처리 사용자", "TRAN_USER_ID", width: 130, readOnly: true, 0);
            DgvUtil.AddTextCol(dgvInspectList, "   처리 주석", "TRAN_COMMENT", width: 120, readOnly: true, 0);
            dgvInspectList.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
        }

        private void DgvDataBinding()
        {
            inspect = new List<LOT_INSPECT_HIS_DTO>();
            inspect = srv.GetInspectHisAllList();
            dgvInspectList.DataSource = null;
            dgvInspectList.DataSource = inspect;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetCategoriry()
        {
            var list = srv.GetLOTInspectID();
            var list2 = srv.GetInspectInfo();
            cboCategory.Items.AddRange(list.Select((p)=>p.LOT_ID).ToArray());
            cboInspectList.Items.AddRange(list2.Select((p) => p.INSPECT_ITEM_NAME).ToArray());
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(cboCategory.Text) && string.IsNullOrWhiteSpace(cboInspectList.Text) && string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MboxUtil.MboxWarn("검색조건을 입력해주세요.");
                return;
            }
            else if(string.IsNullOrWhiteSpace(cboCategory.Text) && string.IsNullOrWhiteSpace(cboInspectList.Text) && !string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                var list = srv.GetInspectHisAllList();
                list = list.FindAll((i)=>i.INSPECT_VALUE.Equals(txtSearch.Text));
                dgvInspectList.DataSource = null;
                dgvInspectList.DataSource = list;
                return;
            }
            else if (string.IsNullOrWhiteSpace(cboCategory.Text) && !string.IsNullOrWhiteSpace(cboInspectList.Text) && string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                var list = srv.GetInspectHisAllList();
                list = list.FindAll((i) => i.INSPECT_ITEM_NAME.Equals(cboInspectList.Text));
                dgvInspectList.DataSource = null;
                dgvInspectList.DataSource = list;
                return;
            }
            int row = 0;
            row = dgvInspectList.SelectedRows[0].Index;
            string id = dgvInspectList["LOT_ID", row].Value.ToString();
            if(cboCategory.SelectedItem.ToString().Equals(id))
            {
                inspect = srv.GetLotInspectHisInfo(id);
            }
            else
            {
                MboxUtil.MboxWarn("해당 LOT의 검사이력이 존재하지 않습니다.\n검사 후, 조회가 가능합니다.");
                inspect = srv.GetInspectHisAllList();
                return;
            }
            dgvInspectList.DataSource = inspect;
        }

        private void btnAllSearch_Click(object sender, EventArgs e)
        {
            inspect = srv.GetInspectHisAllList();
            dgvInspectList.DataSource = null;
            dgvInspectList.DataSource = inspect;
        }
    }
}
