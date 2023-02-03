using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cohesion_DTO;

namespace Cohesion_Project
{
   public partial class Pop_Purchase : Cohesion_Project.Base.Frm_BasePop
   {
      private List<WORK_ORDER_MST_DTO> orders = null;
      private Srv_Order srv_Order = new Srv_Order();
      public WORK_ORDER_MST_DTO order { get; set; }

      public Pop_Purchase()
      {
         InitializeComponent();
      }
      private void Pop_Purchase_Load(object sender, EventArgs e)
      {
         DgvInit();
         orders = srv_Order.SelectOrderList();
         dgvOrder.DataSource = orders;
      }
      private void DgvInit()
      {
         DgvUtil.DgvInit(dgvOrder);
         DgvUtil.AddTextCol(dgvOrder, "작업 지시 코드", "WORK_ORDER_ID", width: 200, readOnly: true, frozen: true);
         DgvUtil.AddTextCol(dgvOrder, "작업 일자", "ORDER_DATE", width: 150, readOnly: true, frozen: true);
         DgvUtil.AddTextCol(dgvOrder, "고객 코드", "CUSTOMER_CODE", width: 150, readOnly: true, frozen: true);
         DgvUtil.AddTextCol(dgvOrder, "품번 코드", "PRODUCT_CODE", width: 150, readOnly: true, frozen: true);
         DgvUtil.AddTextCol(dgvOrder, "고객 사", "CUSTOMER_NAME", width: 140, readOnly: true);
         DgvUtil.AddTextCol(dgvOrder, "품번 명", "PRODUCT_NAME", width: 140, readOnly: true);
         DgvUtil.AddTextCol(dgvOrder, "계획 수량", "ORDER_QTY", width: 140, readOnly: true);
         DgvUtil.AddTextCol(dgvOrder, "생산 수량", "PRODUCT_QTY", width: 150, readOnly: true);
         DgvUtil.AddTextCol(dgvOrder, "불량 수량", "DEFECT_QTY", width: 195, readOnly: true);
         DgvUtil.AddTextCol(dgvOrder, "지시 상태", "ORDER_STATUS", width: 195, readOnly: true);
      }
      private void btnSearch_Click(object sender, EventArgs e)
      {
         var list = orders.FindAll((o) => o.WORK_ORDER_ID.Contains(txtSearch.Text.ToUpper()));
         dgvOrder.DataSource = list;
      }
      private void Btn_Close_Click(object sender, EventArgs e)
      {
         this.Close();
      }
      private void btnOk_Click(object sender, EventArgs e)
      {
         if (dgvOrder.SelectedRows.Count < 1) return;
         //if (!MboxUtil.MboxInfo_("해당 작업지시서를 선택하시겠습니까 ?")) return;
         order = DgvUtil.DgvToDto<WORK_ORDER_MST_DTO>(dgvOrder);
         this.DialogResult = DialogResult.OK;
         this.Close();
      }
      private void Pop_Purchase_KeyPress(object sender, KeyPressEventArgs e)
      {
         if (e.KeyChar == 13)
            btnOk.PerformClick();
      }
      private void dgvOrder_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
      {
         btnOk.PerformClick();
      }
   }
}
