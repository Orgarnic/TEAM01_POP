using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Cohesion_DTO;

namespace Cohesion_Project
{
    public partial class Frm_Purchase : Cohesion_Project.Frm_Base3Line
    {
        Srv_Purchase srv = new Srv_Purchase();
        List<PURCHASE_ORDER_MST_DTO> purchase = null;
        public Frm_Purchase()
        {
            InitializeComponent();
        }

        private void Frm_Purchase_Load(object sender, EventArgs e)
        {
            DataGridViewInit();
            purchase = srv.GetAllPurchaseList();
            cboPurchaseID.Items.AddRange(purchase.Distinct().Select((s)=>s.PURCHASE_ORDER_ID).ToArray());
            //ComboUtil.Store = (from s in )
        }

        private void Btn_Purchase_Click(object sender, EventArgs e)
        {
            dgvPurchaseList.DataSource = srv.SelectPurchaseList(cboPurchaseID.Text);
        }

        private void DataGridViewInit()
        {
            // PURCHASE_ORDER_ID, PURCHASE_SEQ, SALES_ORDER_ID, ORDER_DATE, VENDOR_CODE, MATERIAL_CODE, ORDER_QTY, STOCK_IN_FLAG, STOCK_IN_STORE_CODE, STOCK_IN_LOT_ID
            DgvUtil.DgvInit(dgvPurchaseList);
            DgvUtil.AddTextCol(dgvPurchaseList, "구매 납품서 코드", "PURCHASE_ORDER_ID", width: 200, readOnly: true, frozen: true);
            DgvUtil.AddTextCol(dgvPurchaseList, "고객 주문서 코드", "SALES_ORDER_ID", width: 200, readOnly: true, frozen: true);
            DgvUtil.AddTextCol(dgvPurchaseList, "구매발주 일자", "ORDER_DATE", width: 200, readOnly: true, 1);
            DgvUtil.AddTextCol(dgvPurchaseList, "납품처 코드", "VENDOR_CODE", width: 200, readOnly: true);
            DgvUtil.AddTextCol(dgvPurchaseList, "납품처명", "CUSTOMER_NAME", width: 150, readOnly: true);
            DgvUtil.AddTextCol(dgvPurchaseList, "자재 품번", "MATERIAL_CODE", width: 150, readOnly: true);
            DgvUtil.AddTextCol(dgvPurchaseList, "자재 품명", "PRODUCT_NAME", width: 150, readOnly: true);
            DgvUtil.AddTextCol(dgvPurchaseList, "발주 수량", "ORDER_QTY", width: 100, readOnly: true, 2);
            DgvUtil.AddTextCol(dgvPurchaseList, "입하 여부", "STOCK_IN_FLAG", width: 100, readOnly: true, 1);
            DgvUtil.AddTextCol(dgvPurchaseList, "입하 창고 코드", "STOCK_IN_STORE_CODE", width: 150, readOnly: true);
            DgvUtil.AddTextCol(dgvPurchaseList, "입하 자재 LOT ID", "STOCK_IN_LOT_ID", width: 200, readOnly: true);
            dgvPurchaseList.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
        }

        private void InitDataList(List<PURCHASE_ORDER_MST_DTO> list)
        {
        }

        private void dgvPurchaseList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //txt
        }
    }
}
