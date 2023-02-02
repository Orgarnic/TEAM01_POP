using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cohesion_DTO;

namespace Cohesion_Project
{
   public partial class Frm_MateriarFlag : Frm_BaseNone
   {
      private WORK_ORDER_MST_DTO order = null;
      private List<PRODUCT_OPERATION_REL_DTO> operations = null;
      private List<LOT_STS_DTO> Lots = null;
      private List<LOT_STS_DTO> LotsMaterialr = null;
      private LOT_STS_DTO Lot = null;
      private Srv_Work srvWork = new Srv_Work();
      private Srv_Flag srvFlag = new Srv_Flag();

      public Frm_MateriarFlag()
      {
         InitializeComponent();
      }
      private void Frm_WORK_ORDER_Load(object sender, EventArgs e)
      {
         operations = srvWork.SelectOperations();
         DgvInit();
      }
      private void ComboBoxBinding()
      {
         txtLotDesc.Text = string.Empty;
         cboLotId.Items.Clear();
         cboLotId.Items.Add("선택");
         if (Lots != null && Lots.Count > 0)
         {
            foreach (var Lot in Lots)
               cboLotId.Items.Add(Lot.LOT_ID);
         }
         cboLotId.SelectedIndex = 0;
      }
      private void DgvInit()
      {
         DgvUtil.DgvInit(dgvMateriar);
         DgvUtil.AddTextCol(dgvMateriar, "자 품번", "PRODUCT_CODE", width: 200, readOnly: true, frozen: true);
         DgvUtil.AddTextCol(dgvMateriar, "자 품명", "PRODUCT_NAME", width: 200, readOnly: true, frozen: true);
         DgvUtil.AddTextCol(dgvMateriar, "단위 수량", "REQUIRE_QTY", width: 200, readOnly: true, frozen: true);
         DgvUtil.AddTextCol(dgvMateriar, "자재 LOT", "LOT_ID", width: 250);
         DgvUtil.AddTextCol(dgvMateriar, "자재 LOT 수량", "LOT_QTY", width: 200, readOnly: true);
         DgvUtil.AddTextCol(dgvMateriar, "사용 LOT 수량", "TOTAL", width: 200, readOnly: true);
         DgvUtil.AddTextCol(dgvMateriar, "자 품번 재고", "LOT_QTY_TOTAL", width: 200, readOnly: true);
         dgvMateriar.SelectionMode = DataGridViewSelectionMode.CellSelect;
         dgvMateriar.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
      }
      private void btnOrder_Click(object sender, EventArgs e)
      {
         Pop_Purchase pop = new Pop_Purchase();
         DialogResult dia = pop.ShowDialog();

         if (dia == DialogResult.OK)
         {
            order = pop.order;
            Lots = null;
            Lots = srvFlag.SelectOrderLotInspect(order.WORK_ORDER_ID);

            if (Lots == null || Lots.Count < 1)
            {
               MboxUtil.MboxInfo("해당 작업지시 LOT ID 가 존재하지 않습니다.");
               return;
            }
            ComboBoxBinding();
            txtOrder.Text = order.WORK_ORDER_ID;
            lblOrderStatus.Text = order.ORDER_STATUS;
            lblOrderQty.Text = Convert.ToInt32(order.ORDER_QTY).ToString();

            txtProductCode.Text = order.PRODUCT_CODE;
            txtProductName.Text = order.PRODUCT_NAME;
            txtCustomerCode.Text = order.CUSTOMER_CODE;
            txtCustomerName.Text = order.CUSTOMER_NAME;
         }
      }
      private void cboLotId_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (cboLotId.SelectedIndex < 1)
         {
            CommonUtil.ResetControls(txtOperationCode, txtOperationName, txtTotal, txtOperationName, txtLotDesc);
            lblDefectQty.Text = "0"; lblProductQty.Text = "0";
            flwOperation.Controls.Clear();
            return;
         }
         Lot = Lots.Find((l) => l.LOT_ID.Equals(cboLotId.Text));
         if (Lot != null)
         {
            txtLotDesc.Text = Lot.LOT_DESC;
            txtOperationCode.Text = Lot.OPERATION_CODE;
            txtOperationName.Text = Lot.OPERATION_NAME;
            txtTotal.Text = Convert.ToInt32(Lot.START_QTY).ToString();
            lblProductQty.Text = Convert.ToInt32(Lot.LOT_QTY).ToString();
            lblDefectQty.Text = Convert.ToInt32(Lot.LOT_DEFECT_QTY).ToString();

            var list = operations.FindAll((o) => o.PRODUCT_CODE.Equals(Lot.PRODUCT_CODE)).OrderBy((o) => o.FLOW_SEQ).ToList();
            var operation = operations.Find((o) => o.PRODUCT_CODE.Equals(Lot.PRODUCT_CODE) && o.OPERATION_CODE.Equals(Lot.OPERATION_CODE));
            if (list.Count > 0)
            {
               int size = flwOperation.Width / list.Count;
               flwOperation.Controls.Clear();
               foreach (var item in list)
               {
                  Label label = new Label();
                  label.Margin = new Padding(0);
                  label.AutoSize = false;
                  label.BorderStyle = BorderStyle.FixedSingle;
                  label.Size = new Size(size, 35);
                  label.Text = item.OPERATION_NAME + "[완료]";
                  label.Font = new Font("맑은 고딕", 11, FontStyle.Bold);
                  label.BackColor = Color.YellowGreen;
                  label.TextAlign = ContentAlignment.MiddleCenter;
                  if (item.FLOW_SEQ == operation.FLOW_SEQ)
                  {
                     label.Text = item.OPERATION_NAME + "[진행 중]";
                     label.BackColor = Color.Gold;
                  }
                  else if (item.FLOW_SEQ > operation.FLOW_SEQ)
                  {
                     label.Text = item.OPERATION_NAME + "[대기 중]";
                     label.BackColor = Color.Gray;
                  }
                  flwOperation.Controls.Add(label);
               }
            }
            else
               MboxUtil.MboxError("공정 진행정보를 불러오는데 오류가 발생했습니다.");
            LotsMaterialr = srvFlag.SelectLotMateriars(Lot.PRODUCT_CODE);
            dgvMateriar.DataSource = null;
            var temp = LotsMaterialr.Distinct();
            dgvMateriar.Rows.Clear();
            foreach (var item in temp)
               Row(item);
         }
         else
         {
            MboxUtil.MboxError("LOT 이력을 불러오는데 오류가 발생했습니다.");
            return;
         }
      }
      private void Row(LOT_STS_DTO item)
      {
         DataGridViewRow row = new DataGridViewRow();
         DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
         cell.Value = item.PRODUCT_CODE;
         row.Cells.Add(cell);
         DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
         cell2.Value = item.PRODUCT_NAME;
         row.Cells.Add(cell2);
         DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
         cell3.Value = item.REQUIRE_QTY;
         row.Cells.Add(cell3);
         DataGridViewComboBoxCell col04 = new DataGridViewComboBoxCell();
         List<string> items = new List<string> { "선택" };
         LotsMaterialr.Where((m) => m.PRODUCT_CODE.Equals(item.PRODUCT_CODE)).Select((m) => m.LOT_ID).ToList().ForEach((l) => items.Add(l));
         col04.Items.AddRange(items.ToArray());
         col04.Value = "선택";
         row.Cells.Add(col04);
         DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
         cell5.Value = 0;
         row.Cells.Add(cell5);
         DataGridViewTextBoxCell cell6 = new DataGridViewTextBoxCell();
         cell6.Value = Convert.ToInt32(item.REQUIRE_QTY * Lot.START_QTY);
         row.Cells.Add(cell6);
         DataGridViewTextBoxCell cell7 = new DataGridViewTextBoxCell();
         cell7.Value = Convert.ToInt32((from meaterial in LotsMaterialr where meaterial.PRODUCT_CODE.Equals(item.PRODUCT_CODE) select meaterial).Sum(m => m.LOT_QTY));
         row.Cells.Add(cell7);
         dgvMateriar.Rows.Add(row);
      }
      private void dgvMateriar_CellEndEdit(object sender, DataGridViewCellEventArgs e)
      {
         int row = e.RowIndex;
         int col = e.ColumnIndex;
         string type = dgvMateriar["LOT_ID", row].Value.ToString();

         if (type.Equals("선택"))
         {
            dgvMateriar["LOT_QTY", row].Value = "0";
         }
         else
         {
            decimal logQty = LotsMaterialr.Find((m) => m.LOT_ID.Equals(type)).LOT_QTY;
            dgvMateriar["LOT_QTY", row].Value = Convert.ToInt32(logQty);
         }
         
      }
      private void btnStart_Click(object sender, EventArgs e)
      {
         if (string.IsNullOrWhiteSpace(txtOrder.Text))
         {
            MboxUtil.MboxWarn("작업 지시서를 선택해주십시오.");
            return;
         }
         if (cboLotId.SelectedIndex < 1)
         {
            MboxUtil.MboxWarn("LOT 정보를 선택해주십시오.");
            return;
         }
         if (LotsMaterialr == null)
         {
            MboxUtil.MboxWarn("자 품번 LOT가 존재하지 않습니다.");
            return;
         }

         Lot.LAST_TRAN_CODE = "INPUT";
         Lot.LAST_TRAN_TIME = DateTime.Now;
         Lot.LAST_TRAN_USER_ID = "TEST";
         Lot.LAST_TRAN_COMMENT = txtDesc.Text;
         Lot.LAST_HIST_SEQ += 1;
         List<LOT_MATERIAL_HIS_DTO> hisMaterial = new List<LOT_MATERIAL_HIS_DTO>();
         
         /*if (!result)
         {
            MboxUtil.MboxError("오류가 발생했습니다.");
            return;
         }
         MboxUtil.MboxInfo("검사 데이터가 등록되었습니다.");
         for (int i = 0; i < dgvMateriar.Rows.Count; i++)
         {
            if (dgvMateriar.Rows[i].Cells["INPUT"] is DataGridViewTextBoxCell)
               dgvMateriar.Rows[i].Cells["INPUT"].Value = "0";
            else
               dgvMateriar.Rows[i].Cells["INPUT"].Value = "선택";
            dgvMateriar.Rows[i].Cells["Checked"].Value = "";
         }
         Lots = srvFlag.SelectOrderLotBed(txtOrder.Text);*/
      }
   }
}
