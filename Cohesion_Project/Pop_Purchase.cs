using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cohesion_Project
{
   public partial class Pop_Purchase : Cohesion_Project.Base.Frm_BasePop
   {
      public Pop_Purchase()
      {
         InitializeComponent();
      }
      private void Pop_Purchase_Load(object sender, EventArgs e)
      {
         DgvInit();
      }
      private void DgvInit()
      {
         DgvUtil.DgvInit(dgvOrder);
         DgvUtil.AddTextCol(dgvOrder, "작업 지시 코드", "WORK_ORDER_ID", width: 140, readOnly: true, frozen: true);
         DgvUtil.AddTextCol(dgvOrder, "작업 일자", "ORDER_DATE", width: 140, readOnly: true, frozen: true);
         DgvUtil.AddTextCol(dgvOrder, "고객 코드", "CHECK_DEFECT_FLAG", width: 140, readOnly: true, frozen: true);
         //DgvUtil.AddTextCol(dgvOrder, "고객 사", "CUSTOMER_CODE", width: 140, readOnly: true, frozen: true);
         DgvUtil.AddTextCol(dgvOrder, "품번 코드", "PRODUCT_CODE", width: 140, readOnly: true, frozen: true);
         //DgvUtil.AddTextCol(dgvOrder, "품번 명", "CHECK_INSPECT_FLAG", width: 140, readOnly: true);
         DgvUtil.AddTextCol(dgvOrder, "계획 수량", "ORDER_QTY", width: 140, readOnly: true);
         DgvUtil.AddTextCol(dgvOrder, "생산 수량", "PRODUCT_QTY", width: 150, readOnly: true);
         DgvUtil.AddTextCol(dgvOrder, "불량 수량", "DEFECT_QTY", width: 195, readOnly: true);
         DgvUtil.AddTextCol(dgvOrder, "지시 상태", "ORDER_STATUS", width: 195, readOnly: true);
      }
      private void btnSearch_Click(object sender, EventArgs e)
      {

      }
      private void Btn_Close_Click(object sender, EventArgs e)
      {
         this.Close();
      }

   }
}
