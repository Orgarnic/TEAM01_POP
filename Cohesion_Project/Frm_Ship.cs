using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cohesion_DTO;
using Cohesion_Project.Service;

namespace Cohesion_Project
{
    public partial class Frm_Ship : Cohesion_Project.Frm_Base3Line
    {
        SalesOrder_DTO OrderInfo = new SalesOrder_DTO();
        Srv_Ship svShip = new Srv_Ship();
        Dictionary<string, decimal> lotNumList = new Dictionary<string, decimal>();

        public Frm_Ship()
        {
            InitializeComponent();
        }
        private void Frm_Ship_Load(object sender, EventArgs e)
        {
            DgvUtil.DgvInit(dgvStockProduct);
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            chk.HeaderText = "";
            dgvStockProduct.Columns.Add(chk);
            DgvUtil.AddTextCol(dgvStockProduct, "    순서", "DISPLAY_SEQ", 100, readOnly: true, align: 1);
            DgvUtil.AddTextCol(dgvStockProduct, "    LOT 번호", "LOT_ID", 180, readOnly: true, align: 0);
            DgvUtil.AddTextCol(dgvStockProduct, "    제품 코드", "PRODUCT_CODE", 150, readOnly: true, align: 0);
            DgvUtil.AddTextCol(dgvStockProduct, "    제품명", "PRODUCT_NAME", 150, readOnly: true, align: 0);
            DgvUtil.AddTextCol(dgvStockProduct, "    수량", "LOT_QTY", 150, readOnly: true, align: 2);
            DgvUtil.AddTextCol(dgvStockProduct, "    창고 코드", "STORE_CODE", 150, readOnly: true, align:0);
            DgvUtil.AddTextCol(dgvStockProduct, "    LOT 삭제 여부", "LOT_DELETE_FLAG", 150, readOnly: true, align: 1);
            DgvUtil.AddTextCol(dgvStockProduct, "    생산 완료 시간", "PRODUCTION_TIME", 200, readOnly: true);
            DgvUtil.AddTextCol(dgvStockProduct, "    창고 입고 시간", "LAST_TRAN_TIME", 200, readOnly: true);



            dgvStockProduct.CellValueChanged += DgvStockProduct_CellValueChanged;
        }

        private void DgvStockProduct_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dgvStockProduct.EndEdit();
            if (e.RowIndex<0)
            {
                return;
            }
            Decimal totQty = (txtTotalQty.Text =="")? 0 : Convert.ToDecimal(txtTotalQty.Text);
            if ((Boolean)dgvStockProduct.Rows[e.RowIndex].Cells[0].Value == true)
            {
                totQty += Convert.ToDecimal(dgvStockProduct.Rows[e.RowIndex].Cells["LOT_QTY"].Value);
                txtTotalQty.Text = totQty.ToString();
                lstLot.Items.Add(dgvStockProduct.Rows[e.RowIndex].Cells["LOT_ID"].Value);
                lotNumList[dgvStockProduct.Rows[e.RowIndex].Cells["LOT_ID"].Value.ToString()] = Convert.ToDecimal(dgvStockProduct.Rows[e.RowIndex].Cells["LOT_QTY"].Value);
            }
            if ((Boolean)dgvStockProduct.Rows[e.RowIndex].Cells[0].Value == false)
            {
                totQty -= Convert.ToInt32(dgvStockProduct.Rows[e.RowIndex].Cells["LOT_QTY"].Value);
                txtTotalQty.Text = totQty.ToString();
                lstLot.Items.Remove(dgvStockProduct.Rows[e.RowIndex].Cells["LOT_ID"].Value);
                lotNumList.Remove(dgvStockProduct.Rows[e.RowIndex].Cells["LOT_ID"].Value.ToString());
            }
            dgvStockProduct.RefreshEdit();

        }

        private void Btn_Ship_Click(object sender, EventArgs e)
        {
            Pop_Ship pop = new Pop_Ship();
            if (pop.ShowDialog() == DialogResult.OK)
            {
                OrderInfo = pop.SelectOrder;
                txtOrderNum.Text = OrderInfo.SALES_ORDER_ID;
                txtProductCode.Text = OrderInfo.PRODUCT_CODE;
                txtProductName.Text = OrderInfo.PRODUCT_NAME;
                txtCustomerCode.Text = OrderInfo.CUSTOMER_CODE;
                txtCustomerName.Text = OrderInfo.CUSTOMER_NAME;
                txtQty.Text = string.Format("{0:N0}",OrderInfo.ORDER_QTY);
                dtpOrderDate.Value = OrderInfo.ORDER_DATE;
            }
        }

        private void btnSelectStock_Click(object sender, EventArgs e)
        {
            var list = svShip.SelectProductListInStore(txtProductCode.Text);
            dgvStockProduct.DataSource = list;
        }

        private void btnShip_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtQty.Text.Replace(",", "")) > Convert.ToDecimal(txtTotalQty.Text))
            {
                MboxUtil.MboxError("출고량이 주문수량보다 작습니다.");
                return;
            }
            bool result = svShip.InsertShipInfo(OrderInfo, lotNumList);
            if (result)
            {
                MboxUtil.MboxInfo("해당 주문의 출고가 완료되었습니다.");
            }
            else
            {
                MboxUtil.MboxError("출고 처리 중 오류가 발생했습니다.");
            }
        }
    }
}
