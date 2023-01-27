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
   public partial class Frm_WorkStart : Frm_BaseNone
   {
      private WORK_ORDER_MST_DTO order = null;
      private PRODUCT_OPERATION_REL_DTO operation = null;
      private Srv_Order srvOrder = new Srv_Order();

      public Frm_WorkStart()
      {
         InitializeComponent();
      }

      private void Frm_WORK_ORDER_Load(object sender, EventArgs e)
      {

      }

      private void btnOrder_Click(object sender, EventArgs e)
      {
         Pop_Purchase pop = new Pop_Purchase();
         DialogResult dia = pop.ShowDialog();

         if (dia == DialogResult.OK)
         {
            order = pop.order;
            txtOrder.Text = order.WORK_ORDER_ID;
         }
         
      }

      private void ComboBoxBinding(List<LOT_HIS_DTO> list)
      {

      }
   }
}
