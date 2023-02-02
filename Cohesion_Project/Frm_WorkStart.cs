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
   public partial class Frm_WorkStart : Frm_BaseNone
   {
      private WORK_ORDER_MST_DTO order = null;
      private List<PRODUCT_OPERATION_REL_DTO> operations = null;
      private List<EQUIPMENT_OPERATION_REL_DTO> Equipments = null;
      private List<LOT_STS_DTO> Lots = null;
      private LOT_STS_DTO Lot = null;
      private Srv_Work srvWork = new Srv_Work();

      public Frm_WorkStart()
      {
         InitializeComponent();
      }
      private void Frm_WORK_ORDER_Load(object sender, EventArgs e)
      {
         operations = srvWork.SelectOperations();
         Equipments = srvWork.SelectEquipments();
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
            Lots = srvWork.SelectOrderLot(order.WORK_ORDER_ID);
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
            CommonUtil.ResetControls(txtProductCode, lblOrderStatus, lblOrderQty, txtProductCode, txtProductName, txtCustomerCode, txtCustomerName, txtOperationCode, txtOperationName, txtTotal, txtLotDesc, txtEquipment);
            lblDefectQty.Text = "0"; lblProductQty.Text = "0";
            flwOperation.Controls.Clear();
            return;
         }
         Lot = Lots.Find((l) => l.LOT_ID.Equals(cboLotId.Text));
         if(Lot != null)
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
                  else if(item.FLOW_SEQ > operation.FLOW_SEQ)
                  {
                     label.Text = item.OPERATION_NAME + "[대기 중]";
                     label.BackColor = Color.Gray;
                  }
                  flwOperation.Controls.Add(label);
               }
            }
            else
               MboxUtil.MboxError("공정 진행정보를 불러오는데 오류가 발생했습니다.");

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
         Lot.LOT_QTY = Convert.ToInt32(txtTotal.Text);
         Lot.START_FLAG = 'Y';
         Lot.START_QTY = Convert.ToInt32(txtTotal.Text);
         Lot.START_TIME = DateTime.Now;
         Lot.START_EQUIPMENT_CODE = cboEquipment.SelectedIndex < 1 ? "" : cboEquipment.Text;
         Lot.LAST_TRAN_CODE = "START";
         Lot.LAST_TRAN_TIME = DateTime.Now;
         Lot.LAST_TRAN_USER_ID = "TEST";
         Lot.LAST_TRAN_COMMENT = txtDesc.Text;
         Lot.LAST_HIST_SEQ += 1;

         bool result = srvWork.StartWork(Lot);
         if (!result)
         {
            MboxUtil.MboxError("오류가 발생했습니다.");
            return;
         }
         MboxUtil.MboxInfo("작업이 시작되었습니다.");
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
