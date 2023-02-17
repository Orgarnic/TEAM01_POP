using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cohesion_DTO;
using Cohesion_Project.Service;

namespace Cohesion_Project
{
   public partial class Frm_LookUp : Form
   {
        private LOT_STS_PPG_DTO iProperty = new LOT_STS_PPG_DTO();
        private LOT_DTO_Search sProperty = new LOT_DTO_Search();
        List<LOT_STS_PPG_DTO> srvList;
        Srv_LookUp srvLookUp = new Srv_LookUp();
        bool stateSearchCondition = false;
        Util.ComboUtil comboUtil = new Util.ComboUtil();
        public Frm_LookUp()
        {
            InitializeComponent();
        }
        private void Frm_LookUp_Load(object sender, EventArgs e)
        {
            DataGridViewBinding();
            FormCondition();
        }
        private void DataGridViewBinding()
        {
            DgvUtil.DgvInit(dgv_LOT_List);
            DgvUtil.AddTextCol(dgv_LOT_List, "LOT ID", "LOT_ID", readOnly: true, width: 220, align: 0, frozen: true);
            DgvUtil.AddTextCol(dgv_LOT_List, "제품 코드", "PRODUCT_CODE", readOnly: true, width: 150, align: 0, frozen: true);
            DgvUtil.AddTextCol(dgv_LOT_List, "제품 명", "PRODUCT_NAME", readOnly: true, width: 150, align: 0, frozen: true);
            DgvUtil.AddTextCol(dgv_LOT_List, "공정 코드", "OPERATION_CODE", readOnly: true, width: 150, align: 0, frozen: true);
            DgvUtil.AddTextCol(dgv_LOT_List, "창고 코드", "STORE_CODE", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_List, "LOT 수량", "LOT_QTY", readOnly: true, width: 150, align: 2);
            DgvUtil.AddTextCol(dgv_LOT_List, "LOT 생성 시 수량", "CREATE_QTY", readOnly: true, width: 150, align: 2);
            DgvUtil.AddTextCol(dgv_LOT_List, "LOT 생성 수량", "OPER_IN_QTY", readOnly: true, width: 150, align: 2);
            DgvUtil.AddTextCol(dgv_LOT_List, "공정 시작 여부", "START_FLAG", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_List, "작업 시작 수량", "START_QTY", readOnly: true, width: 150, align: 2);
            DgvUtil.AddTextCol(dgv_LOT_List, "작업 시작 시간", "START_TIME", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_List, "작업 시작 설비", "START_EQUIPMENT_CODE", readOnly: true, width: 150, align: 0);
            DgvUtil.AddTextCol(dgv_LOT_List, "공정 완료 여부", "END_FLAG", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_List, "공정 완료 시간", "END_TIME", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_List, "작업 완료 설비", "END_EQUIPMENT_CODE", readOnly: true, width: 150, align: 0);
            DgvUtil.AddTextCol(dgv_LOT_List, "출하 여부", "SHIP_FLAG", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_List, "출하 코드", "SHIP_CODE", readOnly: true, width: 150, align: 0);
            DgvUtil.AddTextCol(dgv_LOT_List, "출하 시간", "SHIP_TIME", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_List, "LOT 생산 일자", "PRODUCTION_TIME", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_List, "LOT 생산 시간", "CREATE_TIME", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_List, "LOT 공정 투입 시간", "OPER_IN_TIME", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_List, "작업지시", "WORK_ORDER_ID", readOnly: true, width: 150, align: 0);
            DgvUtil.AddTextCol(dgv_LOT_List, "LOT 삭제 여부", "LOT_DELETE_FLAG", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_List, "LOT 삭제 코드", "LOT_DELETE_CODE", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_List, "LOT 삭제 시간", "LOT_DELETE_TIME", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_List, "마지막 처리 코드", "LAST_TRAN_CODE", readOnly: true, width: 150, align: 0);
            DgvUtil.AddTextCol(dgv_LOT_List, "마지막 처리 시간", "LAST_TRAN_TIME", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_List, "마지막 처리 사용자", "LAST_TRAN_USER_ID", readOnly: true, width: 150, align: 0);
            DgvUtil.AddTextCol(dgv_LOT_List, "마지막 처리 주석", "LAST_TRAN_COMMENT", readOnly: true, width: 150, align: 0);
            DgvUtil.AddTextCol(dgv_LOT_List, "마지막 이력 순번", "LAST_HIST_SEQ", readOnly: true, width: 150, align: 0);
            DgvUtil.AddTextCol(dgv_LOT_List, "LOT 설명", "LOT_DESC", readOnly: true, width: 150, align: 0);

            DgvUtil.DgvInit(dgv_LOT_Detail);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "LOT ID", "LOT_ID", readOnly: true, width: 220, align: 0, frozen: true,visible:false);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "이력 순번", "HIST_SEQ", readOnly: true, width: 100, align: 0, frozen: true);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "처리 시간", "TRAN_TIME", readOnly: true, width: 150, align: 1, frozen: true);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "처리 코드", "TRAN_CODE", readOnly: true, width: 100, align: 0, frozen: true);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "품번", "PRODUCT_CODE", readOnly: true, width: 150, align: 0, visible: false, frozen: true);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "공정 코드", "OPERATION_CODE", readOnly: true, width: 150, align: 0, frozen: true);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "공정 명", "OPERATION_NAME", readOnly: true, width: 150, align: 0, frozen: true);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "LOT 수량", "LOT_QTY", readOnly: true, width: 150, align: 2);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "공정 작업 시작 여부", "START_FLAG", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "LOT 생성 시 수량", "CREATE_QTY", readOnly: true, width: 150, align: 2);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "공정 투입 시 수량", "OPER_IN_QTY", readOnly: true, width: 150, align: 2);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "작업 시작 시 수량", "START_QTY", readOnly: true, width: 150, align: 2);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "작업 시작 시간", "START_TIME", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "작업 시작 설비", "START_EQUIPMENT_CODE", readOnly: true, width: 150, align: 0);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "LOT 공정 작업 완료 여부", "END_FLAG", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "작업 완료 시간", "END_TIME", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "작업 완료 설비", "END_EQUIPMENT_CODE", readOnly: true, width: 150, align: 0);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "출하 여부", "SHIP_FLAG", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "출하 코드", "SHIP_CODE", readOnly: true, width: 150, align: 0);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "출하 시간", "SHIP_TIME", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "LOT 생산 일자", "PRODUCTION_TIME", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "LOT 생성 시간", "CREATE_TIME", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "공정 투입 시 시간", "OPER_IN_TIME", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "작업지시", "WORK_ORDER_ID", readOnly: true, width: 150, align: 0);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "LOT 삭제 여부", "LOT_DELETE_FLAG", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "LOT 삭제 코드", "LOT_DELETE_CODE", readOnly: true, width: 150, align: 0);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "LOT 삭제 시간", "LOT_DELETE_TIME", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "작업 일자", "WORK_DATE", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "처리 사용자", "TRAN_USER_ID", readOnly: true, width: 150, align: 0);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "처리 주석", "TRAN_COMMENT", readOnly: true, width: 150, align: 0);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "이전 이력의 품번", "OLD_PRODUCT_CODE", readOnly: true, width: 150, align: 0);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "이전 이력의 공정", "OLD_OPERATION_CODE", readOnly: true, width: 150, align: 0);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "이전 이력의 창고", "OLD_STORE_CODE", readOnly: true, width: 150, align: 1);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "이전 이력의 LOT 수량", "OLD_LOT_QTY", readOnly: true, width: 150, align: 2);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "창고 코드", "STORE_CODE", readOnly: true, width: 150, align: 0);
            DgvUtil.AddTextCol(dgv_LOT_Detail, "LOT 설명", "LOT_DESC", readOnly: true, width: 150, align: 0);

            LoadData();
        }

        private void FormCondition()
        {
            ppg_LotCondition.PropertySort = PropertySort.Categorized;
            ppg_LotCondition.SelectedObject = iProperty;

            ppg_LotCondition.Enabled = false;
        }
        private void LoadData()
        {
            srvList = srvLookUp.SelectLOT_STS_List();
            dgv_LOT_List.DataSource = null;
            dgv_LOT_List.DataSource = srvList.OrderByDescending((o) => o.LAST_TRAN_TIME).ToList();
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_LOT_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            iProperty = dgv_LOT_List.Rows[e.RowIndex].DataBoundItem as LOT_STS_PPG_DTO;
            ppg_LotCondition.SelectedObject = iProperty;

            string lotID = dgv_LOT_List.Rows[e.RowIndex].Cells["LOT_ID"].Value.ToString();
            LOT_HIS_VO vo = new LOT_HIS_VO
            {
                LOT_ID = lotID
            };

            dgv_LOT_Detail.DataSource = null;
            dgv_LOT_Detail.DataSource = srvLookUp.SelectLOT_HIS_List(lotID);
            ppg_LotCondition.Enabled = false;
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            if (!ppg_LotCondition.Enabled)
            {
                MboxUtil.MboxError("검색조건을 활성화 시켜주세요.");
                return;
            }
            else
            {
                var t = ppg_LotCondition.SelectedObject as LOT_DTO_Search;
                if (t == null)
                {
                    MboxUtil.MboxError("검색조건을 입력해십시오.");
                    return;
                }
                else
                {
                    LOT_STS_VO dto = new LOT_STS_VO
                    {
                        OPERATION_CODE = t.OPERATION_CODE,
                        STORE_CODE = t.STORE_CODE,
                        PRODUCT_CODE = t.PRODUCT_CODE,
                        LOT_ID = t.LOT_ID
                    };
                    var list = srvLookUp.SelectLOTWithCondition(dto);
                    dgv_LOT_List.DataSource = list.Select((i) => new
                    {
                        LOT_ID                   = i.LOT_ID				  ,
                        LOT_DESC                 = i.LOT_DESC			  ,
                        PRODUCT_CODE             = i.PRODUCT_CODE		  ,
                        PRODUCT_NAME             = i.PRODUCT_NAME		  ,
                        OPERATION_CODE           = i.OPERATION_CODE		  ,
                        STORE_CODE               = i.STORE_CODE			  ,
                        LOT_QTY                  = i.LOT_QTY			  ,
                        CREATE_QTY               = i.CREATE_QTY			  ,
                        OPER_IN_QTY              = i.OPER_IN_QTY		  ,
                        START_FLAG               = i.START_FLAG			  ,
                        START_QTY                = i.START_QTY			  ,
                        START_TIME               = i.START_TIME			  ,
                        START_EQUIPMENT_CODE     = i.START_EQUIPMENT_CODE ,
                        END_FLAG                 = i.END_FLAG			  ,
                        END_TIME                 = i.END_TIME			  ,
                        END_EQUIPMENT_CODE       = i.END_EQUIPMENT_CODE	  ,
                        SHIP_FLAG                = i.SHIP_FLAG			  ,
                        SHIP_CODE                = i.SHIP_CODE			  ,
                        SHIP_TIME                = i.SHIP_TIME			  ,
                        PRODUCTION_TIME          = i.PRODUCTION_TIME	  ,
                        CREATE_TIME              = i.CREATE_TIME		  ,
                        OPER_IN_TIME             = i.OPER_IN_TIME		  ,
                        WORK_ORDER_ID            = i.WORK_ORDER_ID		  ,
                        LOT_DELETE_FLAG          = i.LOT_DELETE_FLAG	  ,
                        LOT_DELETE_CODE          = i.LOT_DELETE_CODE	  ,
                        LOT_DELETE_TIME          = i.LOT_DELETE_TIME	  ,
                        LAST_TRAN_CODE           = i.LAST_TRAN_CODE		  ,
                        LAST_TRAN_TIME           = i.LAST_TRAN_TIME		  ,
                        LAST_TRAN_USER_ID        = i.LAST_TRAN_USER_ID	  ,
                        LAST_TRAN_COMMENT        = i.LAST_TRAN_COMMENT	  ,
                        LAST_HIST_SEQ            = i.LAST_HIST_SEQ
                    }).OrderByDescending((o) => o.LAST_TRAN_TIME).ToList();
                }
            }
        }

        private void Btn_SearchCondition_Click(object sender, EventArgs e)
        {
            if (!stateSearchCondition)
            {
                label4.Text = "▶ LOT 정보 검색";
                ppg_LotCondition.SelectedObject = sProperty;
                stateSearchCondition = true;
                ppg_LotCondition.Enabled = true;
            }
            else
            {
                label4.Text = "▶ LOT 정보 및 상태";
                ppg_LotCondition.SelectedObject = iProperty;
                stateSearchCondition = false;   
                ppg_LotCondition.Enabled = false;
            }
        }
    }
}
