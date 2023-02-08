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
        PURCHASE_ORDER_MST_DTO purchaseDTO = null;
        string flag;
        int orderQty = 0;
        public Frm_Purchase()
        {
            InitializeComponent();
        }

        private void Frm_Purchase_Load(object sender, EventArgs e)
        {
            DataGridViewInit();
            purchase = srv.GetAllPurchaseList();
            dgvPurchaseList.DataSource = purchase;
            cboPurchaseID.Items.AddRange(purchase.Distinct().Select((s)=>s.PURCHASE_ORDER_ID).ToArray());
            //ComboUtil.Store = (from s in )
        }

        private void Btn_Purchase_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cboPurchaseID.Text))
                dgvPurchaseList.DataSource = srv.GetAllPurchaseList();
            else
            {
                dgvPurchaseList.DataSource = srv.SelectPurchaseList(cboPurchaseID.Text);
            }
        }

        private void DataGridViewInit()
        {
            // PURCHASE_ORDER_ID, PURCHASE_SEQ, SALES_ORDER_ID, ORDER_DATE, VENDOR_CODE, MATERIAL_CODE, ORDER_QTY, STOCK_IN_FLAG, STOCK_IN_STORE_CODE, STOCK_IN_LOT_ID
            DgvUtil.DgvInit(dgvPurchaseList);
            DgvUtil.AddCheckBoxCol(dgvPurchaseList, "Check", "Check", 50, frozen: true);
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

        private void button1_Click(object sender, EventArgs e)
        {
            int num = 0;
            List<PURCHASE_ORDER_MST_DTO> inProd = new List<PURCHASE_ORDER_MST_DTO>();
            for (int i = 0; i < dgvPurchaseList.Rows.Count; i++)
            {
                bool checkBox = dgvPurchaseList.Rows[i].Cells[0].Value == null ? false : Convert.ToBoolean(dgvPurchaseList.Rows[i].Cells[0].Value);
                if (!checkBox)
                {
                    continue;
                }
                else
                {
                    // 입하 여부에 따라서 자재를 입고시킨다.
                    //if (flag != "N") return;
                    //int allQty = Convert.ToInt32(orderQty);
                    //string product = txtProductName.Text;
                    //if (!MboxUtil.MboxInfo_($"해당 자재를 입고시키시겠습니까?\n\n입고 물품 : {product}\n입고 수량 : {allQty} ea")) return;
                    //else
                    purchaseDTO = (PURCHASE_ORDER_MST_DTO)dgvPurchaseList.Rows[i].DataBoundItem;
                    inProd.Add(purchaseDTO);
                    num++;
                }
            }
            if (purchase == null) return;
            bool result = srv.UpdatePurchaseData(inProd);
            if (!result) MboxUtil.MboxWarn("입고 도중 오류가 발생했습니다.\n다시 시도해주세요.");
            else
            {
                MboxUtil.MboxInfo("자재가 입고되었습니다.");
                dgvPurchaseList.DataSource = null;
                dgvPurchaseList.DataSource = srv.GetAllPurchaseList();
                txtDesc.Enabled = false;
            }
        }

        private List<PURCHASE_ORDER_MST_DTO> CreatePurchaseLot(PURCHASE_ORDER_MST_DTO dto)
        {
            List<PURCHASE_ORDER_MST_DTO> list = new List<PURCHASE_ORDER_MST_DTO>();
            string id, prod;
            string flag;
            for (int i = dgvPurchaseList.Rows.Count - 1; i >= 0; i--)
            {
                id = (string)dgvPurchaseList.Rows[i].Cells["PURCHASE_ORDER_ID"].Value;
                flag = dgvPurchaseList.Rows[i].Cells["STOCK_IN_FLAG"].Value.ToString();
                prod = (string)dgvPurchaseList.Rows[i].Cells["MATERIAL_CODE"].Value;
                if (id == null && flag != "N" && prod == null) continue;
                else
                {
                    purchaseDTO = (PURCHASE_ORDER_MST_DTO)dgvPurchaseList.Rows[dgvPurchaseList.CurrentRow.Index].DataBoundItem;
                    list.Add(purchaseDTO);
                }
            }
            return list;
        }

        private void dgvPurchaseList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            flag = dgvPurchaseList["STOCK_IN_FLAG", e.RowIndex].Value.ToString();
            purchaseDTO = (PURCHASE_ORDER_MST_DTO)dgvPurchaseList.Rows[dgvPurchaseList.CurrentRow.Index].DataBoundItem;
            orderQty = (int)Math.Round(Convert.ToDecimal(dgvPurchaseList["ORDER_QTY", dgvPurchaseList.CurrentRow.Index].Value), 0);
            string Qty = string.Format("{0:#,0}", orderQty);
            txtOrderQty.Text = Qty + " EA";
            txtProductName.Text = dgvPurchaseList["PRODUCT_NAME", dgvPurchaseList.CurrentRow.Index].Value.ToString();
        }
    }
}
