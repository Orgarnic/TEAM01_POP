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
   public partial class Frm_InspectFlag : Frm_BaseNone
   {
      private WORK_ORDER_MST_DTO order = null;
      private List<PRODUCT_OPERATION_REL_DTO> operations = null;
      private List<LOT_STS_DTO> Lots = null;
      private List<INSPECT_ITEM_MST_DTO> inspects;
      private LOT_STS_DTO Lot = null;
      private Srv_Work srvWork = new Srv_Work();
      private Srv_Flag srvFlag = new Srv_Flag();

      public Frm_InspectFlag()
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
         DgvUtil.DgvInit(dgvInspect);
         DgvUtil.AddTextCol(dgvInspect, "검사 코드", "INSPECT_ITEM_CODE", width: 200, readOnly: true, frozen: true);
         DgvUtil.AddTextCol(dgvInspect, "검사 항목", "INSPECT_ITEM_NAME", width: 200, readOnly: true, frozen: true);
         DgvUtil.AddTextCol(dgvInspect, "검사 유형", "VALUE_TYPE", width: 120, readOnly: true, frozen: true, align:1);
         DgvUtil.AddTextCol(dgvInspect, "스펙 하한", "SPEC_LSL", width: 200, readOnly: true, align: 1);
         DgvUtil.AddTextCol(dgvInspect, "스펙 타겟", "SPEC_TARGET", width: 200, readOnly: true, align: 1);
         DgvUtil.AddTextCol(dgvInspect, "스펙 상한", "SPEC_USL", width: 200, readOnly: true, align: 1);
         DgvUtil.AddTextCol(dgvInspect, "검사 데이터", "INPUT", width: 200, align: 1);
         DgvUtil.AddTextCol(dgvInspect, "유효성", "Checked", width: 120, readOnly: true, align: 1);
         dgvInspect.SelectionMode = DataGridViewSelectionMode.CellSelect;
         dgvInspect.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
      }
      private void DgvInspect_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
      {
         if (dgvInspect.CurrentCell.ColumnIndex == 6)
         {
            e.Control.KeyPress += DgvInspect_KeyPress;
         }
      }
      private void DgvInspect_KeyPress(object sender, KeyPressEventArgs e)
      {
         if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            e.Handled = true;
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
         }
      }
      private void cboLotId_SelectedIndexChanged(object sender, EventArgs e)
      {
         dgvInspect.Rows.Clear();
         if (cboLotId.SelectedIndex < 1)
         {
            CommonUtil.ResetControls(txtOperationCode, txtOperationName, txtTotal, txtOperationName, txtLotDesc, lblOrderStatus, txtProductCode, txtProductName, txtCustomerCode, txtCustomerName);
            lblDefectQty.Text = "0"; lblProductQty.Text = "0"; lblOrderQty.Text = "0";
            flwOperation.Controls.Clear();
            return;
         }
         Lot = Lots.Find((l) => l.LOT_ID.Equals(cboLotId.Text));
         if (Lot != null)
         {
            lblOrderStatus.Text = order.ORDER_STATUS;
            lblOrderQty.Text = Convert.ToInt32(order.ORDER_QTY).ToString();

            txtProductCode.Text = order.PRODUCT_CODE;
            txtProductName.Text = order.PRODUCT_NAME;
            txtCustomerCode.Text = order.CUSTOMER_CODE;
            txtCustomerName.Text = order.CUSTOMER_NAME;

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
            inspects = srvFlag.SelectInspects(operation.OPERATION_CODE);
            dgvInspect.DataSource = null;
            foreach (var item in inspects)
            {
               DataGridViewRow row = new DataGridViewRow();
               for (int i = 0; i < item.GetType().GetProperties().Length - 4; i++)
               {
                  DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                  cell.Value = item.GetType().GetProperties()[i].GetValue(item);
                  row.Cells.Add(cell);
               }
               if (item.VALUE_TYPE.Equals('N'))
               {
                  DataGridViewTextBoxCell col07 = new DataGridViewTextBoxCell();
                  col07.Value = "0";
                  row.Cells.Add(col07);
               }
               else if (item.VALUE_TYPE.Equals('C'))
               {
                  DataGridViewComboBoxCell col07 = new DataGridViewComboBoxCell();
                  col07.Items.AddRange(new string[3] { "선택", "양호", "불량" });
                  col07.Value = "선택";
                  row.Cells.Add(col07);
               }
               dgvInspect.Rows.Add(row);
            }
            if (dgvInspect.Rows.Count > 0)
               dgvInspect.Focus();
         }
         else
         {
            MboxUtil.MboxError("LOT 이력을 불러오는데 오류가 발생했습니다.");
            return;
         }
      }
      private void cboLotId_Leave(object sender, EventArgs e)
      {
         if (cboLotId.Items.Count < 1) return;
         if (cboLotId.Items.Contains(cboLotId.Text))
         {
            int idx = cboLotId.Items.IndexOf(cboLotId.Text);
            cboLotId.SelectedIndex = idx;
         }
         else
         {
            cboLotId.SelectedIndex = 0;
         }
      }
      private void dgvInspect_CellEndEdit(object sender, DataGridViewCellEventArgs e)
      {
         // SPEC_LSL SPEC_USL
         int row = e.RowIndex;
         int col = e.ColumnIndex;
         string type = dgvInspect["VALUE_TYPE", row].Value.ToString();
         if (type.Equals("N"))
         {
            decimal lsl = Convert.ToDecimal(dgvInspect["SPEC_LSL", row].Value);
            decimal usl = Convert.ToDecimal(dgvInspect["SPEC_USL", row].Value);
            decimal input = Convert.ToDecimal(dgvInspect["INPUT", row].Value);
            if (input >= lsl && input <= usl)
            {
               dgvInspect["Checked", row].Value = "OK";
               dgvInspect["Checked", row].Style.ForeColor = Color.YellowGreen;
            }
            else
            {
               dgvInspect["Checked", row].Value = "NG";
               dgvInspect["Checked", row].Style.ForeColor = Color.Tomato;
               dgvInspect.Rows[row].DefaultCellStyle.BackColor = Color.Gold;
            }
         }
         else if(type.Equals("C"))
         {
            string input = dgvInspect["INPUT", row].Value.ToString();
            if (input.Equals("양호"))
            {
               dgvInspect["Checked", row].Value = "OK";
               dgvInspect["Checked", row].Style.ForeColor = Color.YellowGreen;
            }
            else if (input.Equals("불량"))
            {
               dgvInspect["Checked", row].Value = "NG";
               dgvInspect["Checked", row].Style.ForeColor = Color.Tomato;
               dgvInspect.Rows[row].DefaultCellStyle.BackColor = Color.Gold;
            }
            else
               dgvInspect["Checked", row].Value = "";
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
         foreach (DataGridViewRow row in dgvInspect.Rows)
         {
            if (string.IsNullOrWhiteSpace((string)row.Cells["Checked"].Value))
            {
               MboxUtil.MboxWarn("모든 검사 데이터를 입력하십시오.");
               return;
            }
         }
         Lot.LAST_TRAN_CODE = "INSPECT";
         Lot.LAST_TRAN_TIME = DateTime.Now;
         Lot.LAST_TRAN_USER_ID = "TEST";
         Lot.LAST_TRAN_COMMENT = txtDesc.Text;
         Lot.LAST_HIST_SEQ += 1;
         List<LOT_INSPECT_HIS_DTO> hisInspects = new List<LOT_INSPECT_HIS_DTO>();
         for (int i = 0; i < inspects.Count; i++)
         {
            LOT_INSPECT_HIS_DTO his = new LOT_INSPECT_HIS_DTO();
            his.INSPECT_ITEM_CODE = inspects[i].INSPECT_ITEM_CODE;
            his.INSPECT_ITEM_NAME = inspects[i].INSPECT_ITEM_NAME;
            his.VALUE_TYPE = inspects[i].VALUE_TYPE;
            his.SPEC_LSL = inspects[i].SPEC_LSL;
            his.SPEC_TARGET = inspects[i].SPEC_TARGET;
            his.SPEC_USL = inspects[i].SPEC_USL;
            his.INSPECT_VALUE = dgvInspect["INPUT", i].Value.ToString();
            his.INSPECT_RESULT = dgvInspect["Checked", i].Value.ToString();
            his.EQUIPMENT_CODE = Lot.START_EQUIPMENT_CODE;
            hisInspects.Add(his);
         }
         bool result = srvFlag.InsertInspect(Lot, hisInspects);
         if (!result)
         {
            MboxUtil.MboxError("오류가 발생했습니다.");
            return;
         }
         MboxUtil.MboxInfo("검사 데이터가 등록되었습니다.");
         for (int i = 0; i < dgvInspect.Rows.Count; i++)
         {
            if(dgvInspect.Rows[i].Cells["INPUT"] is DataGridViewTextBoxCell)
               dgvInspect.Rows[i].Cells["INPUT"].Value = "0";
            else
               dgvInspect.Rows[i].Cells["INPUT"].Value = "선택";
            dgvInspect.Rows[i].Cells["Checked"].Value = "";
         }
         cboLotId.SelectedIndex = 0;
         Lots = srvFlag.SelectOrderLotBed(txtOrder.Text);
      }
   }
}
