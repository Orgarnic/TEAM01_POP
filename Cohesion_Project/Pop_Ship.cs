using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cohesion_DTO;
using System.Linq;
using Cohesion_Project.Service;

namespace Cohesion_Project
{
    public partial class Pop_Ship : Cohesion_Project.Base.Frm_BasePop
    {
        Srv_Ship sv = new Srv_Ship();

        List<SalesOrder_DTO> srcList;

        public SalesOrder_DTO SelectOrder { get; set; }

        public Pop_Ship()
        {
            InitializeComponent();
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Pop_Ship_Load(object sender, EventArgs e)
        {
            DgvUtil.DgvInit(dgvOrderList);
            DgvUtil.AddTextCol(dgvOrderList, "    고객 주문서 코드", "SALES_ORDER_ID", 180, readOnly: true, align: 1, frozen: true);      //0   
            DgvUtil.AddTextCol(dgvOrderList, "    주문 일자", "ORDER_DATE", 150, readOnly: true, align: 1, frozen: true);                //1 
            DgvUtil.AddTextCol(dgvOrderList, "    고객사명", "CUSTOMER_NAME", 100, readOnly: true);                                      //2
            DgvUtil.AddTextCol(dgvOrderList, "    제품 코드", "PRODUCT_CODE", 100, readOnly: true);                                    //3
            DgvUtil.AddTextCol(dgvOrderList, "    주문 제품명", "PRODUCT_NAME", 120, readOnly: true);                                    //3
            DgvUtil.AddTextCol(dgvOrderList, "    주문 수량", "ORDER_QTY", 100, readOnly: true, align:2);                                         //4
            DgvUtil.AddTextCol(dgvOrderList, "    확정 여부", "CONFIRM_FLAG", 100, readOnly: true);                                      //5
            DgvUtil.AddTextCol(dgvOrderList, "    출하 여부", "SHIP_FLAG", 100, readOnly: true);                                         //6
            DgvUtil.AddTextCol(dgvOrderList, "    고객사 코드", "CUSTOMER_CODE", 100, readOnly: true, visible:false);                    //7
            DgvUtil.AddTextCol(dgvOrderList, "    주문 코드", "PRODUCT_CODE", 100, readOnly: true, visible: false);                      //8


            srcList = sv.SelectOrderListToShip();
            dgvOrderList.DataSource = srcList.OrderByDescending((o) => o.ORDER_DATE).ToList();
            dgvOrderList.CellDoubleClick += DgvOrderList_CellDoubleClick;
            dgvOrderList.CellFormatting += DgvOrderList_CellFormatting;
        }

        private void DgvOrderList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex<0)
            {
                return;
            }
            if (e.ColumnIndex == 4)
            {
                //string.Format("{0:#,##0}", Convert.ToInt32(dgvOrderList.Rows[e.RowIndex].Cells[4].Value));
                dgvOrderList.Columns[e.ColumnIndex].DefaultCellStyle.Format = "#,##0";

            }
        }

        private void DgvOrderList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSelect.PerformClick();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.ToUpper();
            var list = dgvOrderList.DataSource as List<SalesOrder_DTO>;
            if (string.IsNullOrWhiteSpace(searchText))
            {
                dgvOrderList.DataSource = srcList.OrderByDescending((o) => o.ORDER_DATE).ToList();
                return;
            }
            //고객사명으로 조회, 주문 제품 코드, 주문서 코드
            dgvOrderList.DataSource = list.FindAll((c) =>c.CUSTOMER_NAME.Contains(searchText) || c.PRODUCT_CODE.Contains(searchText) || c.SALES_ORDER_ID.Contains(searchText));
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (MboxUtil.MboxInfo_("해당 주문을 출고처리 하시겠습니까?"))
            {
                SelectOrder = (SalesOrder_DTO)dgvOrderList.Rows[dgvOrderList.CurrentRow.Index].DataBoundItem;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                return;
            }
        }
    }
}
