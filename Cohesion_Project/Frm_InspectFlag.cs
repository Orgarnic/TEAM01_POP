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
      private List<LOT_DEFECT_HIS_DTO> LotDefects = null;
      private List<LOT_STS_DTO> Lots = null;
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
         DgvUtil.AddTextCol(dgvInspect, "검사 유형", "VALUE_TYPE", width: 100, readOnly: true, frozen: true);
         DgvUtil.AddTextCol(dgvInspect, "스펙 하한", "SPEC_LSL", width: 200, readOnly: true);
         DgvUtil.AddTextCol(dgvInspect, "스펙 타겟", "SPEC_TARGET", width: 200, readOnly: true);
         DgvUtil.AddTextCol(dgvInspect, "스펙 상한", "SPEC_USL", width: 200, readOnly: true);
         DgvUtil.AddTextCol(dgvInspect, "검사 데이터", "", width: 200);
         DgvUtil.AddTextCol(dgvInspect, "유효성", "", width: 100, readOnly: true);
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
         if(Lot != null)
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
            var inspect = srvFlag.SelectInspects(operation.OPERATION_CODE);
            dgvInspect.DataSource = null;
            dgvInspect.DataSource = inspect;
         }
         else
         {
            MboxUtil.MboxError("LOT 이력을 불러오는데 오류가 발생했습니다.");
            return;
         }
      }
      private void dgvInspect_CellEndEdit(object sender, DataGridViewCellEventArgs e)
      {

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

         Lots = srvFlag.SelectOrderLotBed(txtOrder.Text);
         txtLotDesc.Text = string.Empty;
      }

   }
}
