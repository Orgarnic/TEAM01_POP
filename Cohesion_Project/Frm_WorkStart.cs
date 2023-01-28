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
      private List<LOT_STS_DTO> Lots = null;
      private LOT_STS_DTO Lot = null;
      private Srv_Order srvOrder = new Srv_Order();
      private Srv_Work srvWork = new Srv_Work();

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
            Lots = srvWork.SelectOrderLot(txtOrder.Text);
            if (Lots.Count > 0)
            {
               cboLotId.Items.Clear();
               cboLotId.Items.Add("선택");
               foreach (var Lot in Lots)
                  cboLotId.Items.Add(Lot.LOT_ID);
               cboLotId.SelectedIndex = 0;
            }
            else
            {
               MboxUtil.MboxWarn("선택하신 작업지시 LOT ID가 존재하지 않습니다.");
               return;
            }
         }
      }
      private void cboLotId_SelectedIndexChanged(object sender, EventArgs e)
      {
         Lot = Lots.Find((l) => l.LOT_ID.Equals(cboLotId.Text));
         if(Lot != null)
         {

         }
         else
         {
            Lot = null;
         }
      }
   }
}
