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
   public partial class Frm_BedFlag : Frm_BaseNone
   {
      private WORK_ORDER_MST_DTO order = null;
      private List<PRODUCT_OPERATION_REL_DTO> operations = null;
      private List<LOT_DEFECT_HIS_DTO> LotDefects = null;
      private List<LOT_STS_DTO> Lots = null;
      private List<CODE_DATA_MST_DTO> Beds = null;
      private LOT_STS_DTO Lot = null;
      private Srv_Work srvWork = new Srv_Work();
      private Srv_Flag srvFlag = new Srv_Flag();

      public Frm_BedFlag()
      {
         InitializeComponent();
      }

      private void Frm_WORK_ORDER_Load(object sender, EventArgs e)
      {
         operations = srvWork.SelectOperations();
         Beds = srvFlag.SelectBedCodes();
         DgvInit();
         ComboBoxBind();
      }

      private void ComboBoxBind()
      {
         cboBedReg.Items.Insert(0, "선택");
         cboBedReg.SelectedIndex = 0;
         Beds.ForEach((b) => cboBedReg.Items.Add(b.KEY_1));
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
         DgvUtil.DgvInit(dgvDefect);
         DgvUtil.AddTextCol(dgvDefect, "불량 코드", "DEFECT_CODE", width: 250, readOnly: true, frozen: true);
         DgvUtil.AddTextCol(dgvDefect, "불량 명칭", "DEFECT_NAME", width: 250, readOnly: true, frozen: true);
         DgvUtil.AddTextCol(dgvDefect, "입력 수량", "DEFECT_QTY", width: 250, readOnly: true, frozen: true);
         DgvUtil.AddButtonCol(dgvDefect, "삭제 하기", "Delete", width: 150, cellText:"삭제");
      }

      private void btnOrder_Click(object sender, EventArgs e)
      {
         Pop_Purchase pop = new Pop_Purchase();
         DialogResult dia = pop.ShowDialog();

         if (dia == DialogResult.OK)
         {
            order = pop.order;
            Lots = null;
            Lots = srvFlag.SelectOrderLotBed(order.WORK_ORDER_ID);

            if (Lots == null || Lots.Count < 1)
            {
               MboxUtil.MboxInfo("해당 작업지시 LOT ID 가 존재하지 않습니다.");
               return;
            }
            ResetDefectItems();
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
            ResetDefectItems();
            flwOperation.Controls.Clear();
            return;
         }
         Lot = Lots.Find((l) => l.LOT_ID.Equals(cboLotId.Text));
         if(Lot != null)
         {
            txtLotDesc.Text = Lot.LOT_DESC;
            txtOperationCode.Text = Lot.OPERATION_CODE;
            txtOperationName.Text = Lot.OPERATION_NAME;
            txtTotal.Text = Convert.ToInt32(Lot.LOT_QTY).ToString();
            txtLotQty.Text = Convert.ToInt32(Lot.LOT_QTY).ToString();
            lblProductQty.Text = Convert.ToInt32(Lot.LOT_QTY).ToString();
            lblDefectQty.Text = Convert.ToInt32(Lot.LOT_DEFECT_QTY).ToString();

            var list = operations.FindAll((o) => o.PRODUCT_CODE.Equals(Lot.PRODUCT_CODE));
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
         }
         else
         {
            MboxUtil.MboxError("LOT 이력을 불러오는데 오류가 발생했습니다.");
            return;
         }
      }
      private void cboBedReg_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (cboBedReg.SelectedIndex < 1)
         {
            txtBedRegName.Text = string.Empty;
            return;
         }
         txtBedRegName.Text = Beds.Find((b) => b.KEY_1.Equals(cboBedReg.Text)).DATA_1;
         txtBedQty.Text = "0";
      }
      private void txtBedQty_KeyPress(object sender, KeyPressEventArgs e) { if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8) e.Handled = true; }
      private void btnBedRegAdd_Click(object sender, EventArgs e)
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
         if (cboBedReg.SelectedIndex < 1)
         {
            MboxUtil.MboxWarn("불량 항목을 입력해 주십시오.");
            return;
         }
         if (Convert.ToInt32(txtBedQty.Text) < 1 || string.IsNullOrWhiteSpace(txtBedQty.Text))
         {
            MboxUtil.MboxWarn("불량 수량을 입력해 주십시오.");
            return;
         }
         if(Convert.ToInt32(txtTotal.Text) < Convert.ToInt32(txtBedQty.Text))
         {
            MboxUtil.MboxWarn("불량 수량은 생산 수량 보다 클 수 없습니다.");
            return;
         }
         if (LotDefects == null)
            LotDefects = new List<LOT_DEFECT_HIS_DTO>();
         var temp = (from b in Beds
                     where b.KEY_1.Equals(cboBedReg.Text)
                     select new LOT_DEFECT_HIS_DTO { DEFECT_CODE = b.KEY_1, DEFECT_NAME = b.DATA_1, DEFECT_QTY = Convert.ToInt32(txtBedQty.Text), EQUIPMENT_CODE = Lot.START_EQUIPMENT_CODE}).FirstOrDefault();
         if(temp != null)
         {
            if(LotDefects.Find((d)=> d.DEFECT_CODE.Equals(temp.DEFECT_CODE)) == null)
               LotDefects.Add(temp);
            else
               LotDefects.Find((d) => d.DEFECT_CODE.Equals(temp.DEFECT_CODE)).DEFECT_QTY += temp.DEFECT_QTY;
            QtyRange();
         }
      }
      private void dgvDefect_CellClick(object sender, DataGridViewCellEventArgs e)
      {
         int row = e.RowIndex;
         int col = e.ColumnIndex;
         if (row < 0) return;
         if(col == 3)
         {
            if (!MboxUtil.MboxInfo_($"{dgvDefect[0, row].Value} 불량 목록을 삭제하시겠습니까 ? ")) return;
            int idx = LotDefects.FindIndex((d) => d.DEFECT_CODE.Equals(dgvDefect[0, row].Value.ToString()));
            LotDefects.RemoveAt(idx);
            QtyRange();
         }
      }

      private void QtyRange()
      {
         dgvDefect.DataSource = null;
         dgvDefect.DataSource = LotDefects;
         int total = 0;
         LotDefects.ForEach((d) => total += Convert.ToInt32(d.DEFECT_QTY));
         txtBedRegTotal.Text = total.ToString();
         txtLotQty.Text = (Convert.ToInt32(txtTotal.Text) - Convert.ToInt32(txtBedRegTotal.Text)) > 0 ? (Convert.ToInt32(txtTotal.Text) - Convert.ToInt32(txtBedRegTotal.Text)).ToString() : "0";
      }

      private void ResetDefectItems()
      {
         dgvDefect.DataSource = null;
         txtBedRegName.Text = string.Empty;
         cboBedReg.Text = string.Empty;
         cboBedReg.SelectedIndex = 0;
         LotDefects = null;
         txtBedQty.Text = "0";
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
         Lot.LOT_QTY = Convert.ToDecimal(txtLotQty.Text);
         Lot.LAST_TRAN_CODE = "DEFECT";
         Lot.LAST_TRAN_TIME = DateTime.Now;
         Lot.LAST_TRAN_USER_ID = "TEST";
         Lot.LAST_TRAN_COMMENT = txtDesc.Text;
         Lot.LAST_HIST_SEQ += 1;

         bool result = srvFlag.InsertBedReg(Lot, LotDefects);
         if (!result)
         {
            MboxUtil.MboxError("오류가 발생했습니다.");
            return;
         }
         MboxUtil.MboxInfo("불량이 등록되었습니다.");

         lblDefectQty.Text = (Convert.ToInt32(lblDefectQty.Text) + Convert.ToInt32(txtBedRegTotal.Text)).ToString();
         lblProductQty.Text = (Convert.ToInt32(lblProductQty.Text) - Convert.ToInt32(txtBedRegTotal.Text)).ToString();

         CommonUtil.ResetControls(txtBedRegName, txtBedRegTotal);
         Lots = srvFlag.SelectOrderLotBed(txtOrder.Text);
         txtLotDesc.Text = string.Empty;
         ResetDefectItems();
      }
   }
}
