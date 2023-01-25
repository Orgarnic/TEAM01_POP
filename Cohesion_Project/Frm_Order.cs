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
         if(dia == DialogResult.OK)
         {
            txtOrder.Text = pop.order.WORK_ORDER_ID;
            txtCustomerCode.Text = pop.order.CUSTOMER_CODE;
            txtCustomerName.Text = pop.order.CUSTOMER_NAME;
            lblOrderStatus.Text = pop.order.ORDER_STATUS;
            lblOrderQty.Text = pop.order.ORDER_QTY.ToString();
            lblProductQty.Text = pop.order.PRODUCT_QTY.ToString();
            lblDefectQty.Text = pop.order.DEFECT_QTY.ToString();
            txtProductCode.Text = pop.order.PRODUCT_CODE;
            txtProductName.Text = pop.order.PRODUCT_NAME;
            txtTotalQty.Text = (pop.order.PRODUCT_QTY - pop.order.DEFECT_QTY).ToString();
            order = pop.order;
         }
         if (order.ORDER_STATUS.Equals("OPEN"))
         {
            string lot = "LOT_" + order.WORK_ORDER_ID.Replace("WO_","");
            txtLotId.Text = lot;
            lblDesc.Text = "생성된 LOT에 주석을 적어주세요.";
            lblDesc.Visible = true;
            operation = srvOrder.SelectOperation(order.PRODUCT_CODE);
            if(operation == null)
            {
               MboxUtil.MboxWarn("해당 제품은 공정이 등록되지 않은 제품입니다.");
               txtOperationCode.Text = txtOperationName.Text = string.Empty;
               lblDesc.Visible = false;
               return;
            }
            txtOperationCode.Text = operation.OPERATION_CODE;
            txtOperationName.Text = operation.OPERATION_NAME;
         }
         else if (order.ORDER_STATUS.Equals("PROC"))
         {

         }
      }

      private void Btn_Close_Click(object sender, EventArgs e)
      {
         this.Close();
      }
   }
}
