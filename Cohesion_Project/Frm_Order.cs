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
   public partial class Frm_Order : Frm_BaseNone
   {
      private WORK_ORDER_MST_DTO order = null;
      private PRODUCT_OPERATION_REL_DTO operation = null;
      private Srv_Order srvOrder = new Srv_Order();

      public Frm_Order()
      {
         InitializeComponent();
      }

      private void Frm_Order_Load(object sender, EventArgs e)
      {

      }
      private void btnOrder_Click(object sender, EventArgs e)
      {
         Pop_Purchase pop = new Pop_Purchase();
         DialogResult dia = pop.ShowDialog();
         int total = 0;
         if (dia == DialogResult.OK)
         {
            order = pop.order;
            operation = srvOrder.SelectOperation(order.PRODUCT_CODE);
         }
         if (operation == null)
         {
            MboxUtil.MboxWarn("해당 제품은 공정이 등록되지 않은 제품입니다.");
            CommonUtil.ResetControls(txtOperationCode, txtOperationName, txtCustomerCode, txtCustomerName, txtOrder);
            CommonUtil.ResetControls(txtLotId, lblOrderStatus, lblProductQty, lblOrderQty, lblDefectQty, txtProductCode, txtProductName);
            lblDesc.Visible = false;
            return;
         }
         txtLotId.Text = srvOrder.SPGetLot(order.WORK_ORDER_ID, out total);
         lblDesc.Text = "생성된 LOT에 주석을 적어주세요.";
         lblDesc.Visible = true;

         txtOperationCode.Text = operation.OPERATION_CODE;
         txtOperationName.Text = operation.OPERATION_NAME;

         txtOrder.Text = order.WORK_ORDER_ID;
         txtCustomerCode.Text = order.CUSTOMER_CODE;
         txtCustomerName.Text = order.CUSTOMER_NAME;
         lblOrderStatus.Text = order.ORDER_STATUS;
         lblProductQty.Text = Convert.ToInt32(order.PRODUCT_QTY).ToString();
         lblOrderQty.Text = Convert.ToInt32(order.ORDER_QTY).ToString();
         lblDefectQty.Text = Convert.ToInt32(order.DEFECT_QTY).ToString();
         txtProductCode.Text = order.PRODUCT_CODE;
         txtProductName.Text = order.PRODUCT_NAME;
         txtTotal.Text = total.ToString();
      }
      private void txtTotalQty_KeyPress(object sender, KeyPressEventArgs e)
      {
         if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            e.Handled = true;
      }
      private void btnCreate_Click(object sender, EventArgs e)
      {
         if (string.IsNullOrWhiteSpace(txtLotId.Text))
         {
            MboxUtil.MboxWarn("작업지시서를 선택해 주십시오.");
            return;
         }
         if (string.IsNullOrWhiteSpace(txtTotalQty.Text))
         {
            MboxUtil.MboxWarn("생산수량은 필수 입력입니다.");
            return;
         }
         LOT_STS_DTO dto = new LOT_STS_DTO
         {
            LOT_ID = txtLotId.Text,
            LOT_DESC = txtLotDesc.Text,
            PRODUCT_CODE = txtProductCode.Text,
            OPERATION_CODE = txtOperationCode.Text,
            LOT_QTY = Convert.ToInt32(txtTotalQty.Text),
            CREATE_QTY = Convert.ToInt32(txtTotalQty.Text),
            OPER_IN_QTY = Convert.ToInt32(txtTotalQty.Text),
            CREATE_TIME = DateTime.Now,
            OPER_IN_TIME = DateTime.Now,
            WORK_ORDER_ID = txtOrder.Text,
            LAST_TRAN_CODE = "CREATE",
            LAST_TRAN_TIME = DateTime.Now,
            LAST_TRAN_USER_ID = "TEST",
            LAST_TRAN_COMMENT = txtOrderDesc.Text,
            LAST_HIST_SEQ = 1
         };
         bool result = srvOrder.CreateLot(dto);
         if (!result)
         {
            MboxUtil.MboxError("오류가 발생했습니다.");
            return;
         }
         MboxUtil.MboxInfo("LOT가 생성되었습니다.");
         CommonUtil.ResetControls(txtOrder, txtCustomerCode, txtCustomerName, txtLotId, txtLotDesc, txtProductCode, txtProductName,
                                  txtOperationCode, txtOperationName, txtTotal, txtTotalQty, txtOrderDesc, lblOrderQty, lblDefectQty, lblOrderStatus, lblProductQty);
         order = null;
      }

      private void Btn_Close_Click(object sender, EventArgs e)
      {
         this.Close();
      }
   }
}
