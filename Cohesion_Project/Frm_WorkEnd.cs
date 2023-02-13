using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Cohesion_DTO;

namespace Cohesion_Project
{
   public partial class Frm_WorkEnd : Frm_BaseNone
   {
      private WORK_ORDER_MST_DTO order = null;
      private List<PRODUCT_OPERATION_REL_DTO> operations = null;
      private List<EQUIPMENT_OPERATION_REL_DTO> Equipments = null;
      private List<LOT_STS_DTO> Lots = null;
      private LOT_STS_DTO Lot = null;
      private Srv_Work srvWork = new Srv_Work();
      private Label[] state = new Label[3];
      private bool isEndFlag = false;

      public Frm_WorkEnd()
      {
         InitializeComponent();
      }
      private void Frm_WORK_ORDER_Load(object sender, EventArgs e)
      {
         operations = srvWork.SelectOperations();
         Equipments = srvWork.SelectEquipments();
         state[0] = lblDefect;
         state[1] = lblInspect;
         state[2] = lblMateriar;
      }
      private void btnOrder_Click(object sender, EventArgs e)
      {
         Pop_Purchase pop = new Pop_Purchase();
         DialogResult dia = pop.ShowDialog();

         if (dia == DialogResult.OK)
         {
            cboEquipment.Items.Clear();
            cboEquipment.Text = string.Empty;
            order = pop.order;
            Lots = null;
            Lots = srvWork.SelectOrderLotEnd(order.WORK_ORDER_ID);
            if (Lots == null || Lots.Count < 1)
            {
               MboxUtil.MboxInfo("해당 작업지시 LOT ID 가 존재하지 않습니다.");
               return;
            }
            txtOrder.Text = order.WORK_ORDER_ID;
            ComboBoxBinding();
         }
      }
      private void cboLotId_SelectedIndexChanged(object sender, EventArgs e)
      {
         cboEquipment.Items.Clear();
         cboEquipment.Text = string.Empty;
         if (cboLotId.SelectedIndex < 1)
         {
            CommonUtil.ResetControls(txtProductCode, lblOrderStatus, lblOrderQty, txtProductCode, txtProductName, txtCustomerCode, txtCustomerName, txtOperationCode, txtOperationName, txtTotal, txtLotDesc, txtEquipment, state[0], state[1], state[2], lblDefectCheck, lblInspectCheck, lblMateriarCheck);
            foreach (var s in state)
               s.BackColor = Color.Gray;
            lblDefectQty.Text = "0"; lblProductQty.Text = "0";
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

            lblProductQty.Text = Convert.ToInt32(Lot.LOT_QTY).ToString();
            lblDefectQty.Text = Convert.ToInt32(Lot.LOT_DEFECT_QTY).ToString();

            txtTotal.Text = Convert.ToInt32(Lot.CREATE_QTY).ToString();

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

            var operationMst = srvWork.SelectOperation(operation.OPERATION_CODE);
            char[] operationCheck = new char[3] { operationMst.CHECK_DEFECT_FLAG, operationMst.CHECK_INSPECT_FLAG, operationMst.CHECK_MATERIAL_FLAG };
            if (operationMst.CHECK_DEFECT_FLAG == 'Y')
               lblDefectCheck.Text = "V";
            else
               lblDefectCheck.Text = "X";
            if (operationMst.CHECK_INSPECT_FLAG == 'Y')
               lblInspectCheck.Text = "V";
            else
               lblInspectCheck.Text = "X";
            if (operationMst.CHECK_MATERIAL_FLAG == 'Y')
               lblMateriarCheck.Text = "V";
            else
               lblMateriarCheck.Text = "X";

            string workState = srvWork.EndWorkCondition(Lot.LOT_ID, operation.OPERATION_CODE);
            string[] states = workState.Split('_');
            for (int i = 0; i < states.Length; i++)
            {
               if (states[i].Equals("1") && operationCheck[i].Equals('Y'))
               {
                  state[i].Text = "입력 완료";
                  state[i].BackColor = Color.YellowGreen;
               }
               else if (states[i].Equals("0") && operationCheck[i].Equals('Y'))
               {
                  state[i].Text = "입력 필요";
                  state[i].BackColor = Color.Red;
               }
               else if (states[i].Equals("1") && !operationCheck[i].Equals('Y'))
               {
                  state[i].Text = "입력 완료";
                  state[i].BackColor = Color.Gray;
               }
               else
               {
                  state[i].Text = "입력 없음";
                  state[i].BackColor = Color.Gray;
               }
            }
            if ((operationCheck[1] == 'Y' && states[1] == "1") && (operationCheck[2] == 'Y' && states[2] == "1"))
               isEndFlag = true;
            else if ((operationCheck[1] == 'N' && (operationCheck[2] == 'Y' && states[2] == "1")))
               isEndFlag = true;
            else if ((operationCheck[2] == 'N' && (operationCheck[1] == 'Y' && states[1] == "1")))
               isEndFlag = true;
            var temp = Equipments.FindAll((q) => q.OPERATION_CODE.Equals(operation.OPERATION_CODE));
            if (temp.Count > 0)
            {
               cboEquipment.Items.Add("선택");
               foreach (var item in temp)
                  cboEquipment.Items.Add(item.EQUIPMENT_CODE);
               cboEquipment.SelectedIndex = 0;
            }
            cboEquipment.Focus();
         }
         else
         {
            Lot = null;
            MboxUtil.MboxError("LOT 이력을 불러오는데 오류가 발생했습니다.");
            return;
         }
      }
      private void cboEquipment_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (cboEquipment.SelectedIndex < 1)
         {
            txtEquipment.Text = string.Empty;
            return;
         }
         txtEquipment.Text = (Equipments.Find((q) => q.EQUIPMENT_CODE.Equals(cboEquipment.Text))).EQUIPMENT_NAME;
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
         if (cboEquipment.SelectedIndex < 1)
         {
            MboxUtil.MboxWarn("설비 정보를 입력해주세요.");
            return;
         }
         if (!isEndFlag)
         {
            MboxUtil.MboxWarn("미 입력된 항목이 존재합니다.");
            return;
         }
         decimal maxSeq = operations.FindAll((o) => o.PRODUCT_CODE.Equals(Lot.PRODUCT_CODE)).Max(o => o.FLOW_SEQ);
         decimal nowSeq = operations.Find((o) => o.PRODUCT_CODE.Equals(Lot.PRODUCT_CODE) && o.OPERATION_CODE.Equals(txtOperationCode.Text)).FLOW_SEQ;
         bool result = false;
         string operCode = null;
         if(maxSeq > nowSeq)
            operCode = operations.Find((o) => o.PRODUCT_CODE.Equals(Lot.PRODUCT_CODE) && o.FLOW_SEQ.Equals(nowSeq + 1)).OPERATION_CODE;
         Lot.OPERATION_CODE = operCode;
         Lot.OPER_IN_QTY = Convert.ToDecimal(lblProductQty.Text);
         Lot.END_FLAG = 'Y';
         Lot.END_TIME = order.CREATE_TIME;
         Lot.END_EQUIPMENT_CODE = cboEquipment.Text;
         Lot.OPER_IN_TIME = order.CREATE_TIME;
         Lot.LAST_TRAN_CODE = "END";
         Lot.LAST_TRAN_TIME = order.CREATE_TIME;
         Lot.LAST_TRAN_USER_ID = "유기현";
         Lot.LAST_TRAN_COMMENT = txtDesc.Text;
         Lot.LAST_HIST_SEQ += 1;
         LOT_END_HIS_DTO end = new LOT_END_HIS_DTO
         {
            LOT_ID = Lot.LOT_ID,
            HIST_SEQ = Lot.LAST_HIST_SEQ,
            TRAN_TIME = Lot.LAST_TRAN_TIME,
            WORK_DATE = order.CREATE_TIME.ToString("yyyyMMdd"),
            PRODUCT_CODE = Lot.PRODUCT_CODE,
            OPERATION_CODE = txtOperationCode.Text,
            EQUIPMENT_CODE = Lot.END_EQUIPMENT_CODE,
            TRAN_USER_ID = Lot.LAST_TRAN_USER_ID,
            TRAN_COMMENT = Lot.LAST_TRAN_COMMENT,
            TO_OPERATION_CODE = Lot.OPERATION_CODE,
            OPER_IN_QTY = Lot.OPER_IN_QTY,
            START_QTY = Lot.START_QTY,
            END_QTY = Lot.OPER_IN_QTY,
            OPER_IN_TIME = Lot.OPER_IN_TIME,
            START_TIME = Lot.START_TIME,
            WORK_ORDER_ID = Lot.WORK_ORDER_ID,
            PROC_TIME = 90
         };
         if(maxSeq != nowSeq)
            result = srvWork.EndWork(Lot, end, false);
         else
            result = srvWork.EndWork(Lot, end, true);
         if (!result)
         {
            MboxUtil.MboxError("오류가 발생했습니다.");
            return;
         }
         MboxUtil.MboxInfo("작업이 완료되었습니다.");
         Lots = null;
         Lots = srvWork.SelectOrderLot(txtOrder.Text);
         ComboBoxBinding();
         cboLotId.SelectedIndex = 0;
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
   }
}
