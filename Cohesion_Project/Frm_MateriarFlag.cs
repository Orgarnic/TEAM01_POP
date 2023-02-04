using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cohesion_DTO;

namespace Cohesion_Project
{
   public partial class Frm_MateriarFlag : Frm_BaseNone
   {
      private WORK_ORDER_MST_DTO order = null;
      private List<PRODUCT_OPERATION_REL_DTO> operations = null;
      private List<LOT_STS_DTO> Lots = null;
      private List<LOT_STS_DTO> LotsMaterialr = null;
      private List<LOT_STS_DTO> SelectedLotsMateriar = null;
      private LOT_STS_DTO Lot = null;
      private Srv_Work srvWork = new Srv_Work();
      private Srv_Flag srvFlag = new Srv_Flag();
      private bool isPass = false;

      public Frm_MateriarFlag()
      {
         InitializeComponent();
      }
      private void Frm_WORK_ORDER_Load(object sender, EventArgs e)
      {
         operations = srvWork.SelectOperations();
         DgvInit();
      }
      private void ComboBoxBinding()
      {
         txtLotDesc.Text = string.Empty;
         cboLotId.Items.Clear();
         cboLotId.Items.Add("선택");
         if (Lots != null && Lots.Count > 0)
         {
            foreach (var Lot in Lots)
               cboLotId.Items.Add(Lot.LOT_ID);
         }
         cboLotId.SelectedIndex = 0;
      }
      private void DgvInit()
      {
         DgvUtil.DgvInit(dgvMateriar);
         DgvUtil.AddCheckBoxCol(dgvMateriar, "선택", "CHECK", width: 50, frozen:true);
         DgvUtil.AddTextCol(dgvMateriar, "자 품번", "PRODUCT_CODE", width: 180, readOnly: true, frozen: true);
         DgvUtil.AddTextCol(dgvMateriar, "자 품명", "PRODUCT_NAME", width: 180, readOnly: true, frozen: true);
         DgvUtil.AddTextCol(dgvMateriar, "단위 수량", "REQUIRE_QTY", width: 120, readOnly: true);
         DgvUtil.AddTextCol(dgvMateriar, "자재 LOT", "LOT_ID", width: 250);
         DgvUtil.AddTextCol(dgvMateriar, "LOT 수량", "LOT_QTY", width: 130, readOnly: true);
         dgvMateriar.Font = new Font("맑은 고딕", 10, FontStyle.Bold);

         DgvUtil.DgvInit(dgvMateriarInput);
         DgvUtil.AddTextCol(dgvMateriarInput, "필요 품번", "PRODUCT_CODE", width: 180, readOnly: true, frozen: true);
         DgvUtil.AddTextCol(dgvMateriarInput, "필요 품명", "PRODUCT_NAME", width: 180, readOnly: true, frozen: true);
         DgvUtil.AddTextCol(dgvMateriarInput, "선택 수량", "INPUT", width: 130, readOnly: true);
         DgvUtil.AddTextCol(dgvMateriarInput, "필요 수량", "TOTAL", width: 130, readOnly: true);
         dgvMateriarInput.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
         dgvMateriarInput.Enabled = false;
      }
      private void btnOrder_Click(object sender, EventArgs e)
      {
         Pop_Purchase pop = new Pop_Purchase();
         DialogResult dia = pop.ShowDialog();

         if (dia == DialogResult.OK)
         {
            order = pop.order;
            Lots = null;
            Lots = srvFlag.SelectOrderLotInspect(order.WORK_ORDER_ID);

            if (Lots == null || Lots.Count < 1)
            {
               MboxUtil.MboxInfo("해당 작업지시 LOT ID 가 존재하지 않습니다.");
               return;
            }
            ComboBoxBinding();
            txtOrder.Text = order.WORK_ORDER_ID;
         }
      }
      private void cboLotId_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (cboLotId.SelectedIndex < 1)
         {
            CommonUtil.ResetControls(lblOrderStatus, txtProductCode, txtProductName, txtCustomerCode, txtCustomerName, txtOperationCode, txtOperationName, txtTotal, txtLotDesc);
            lblDefectQty.Text = "0"; lblProductQty.Text = "0"; lblOrderQty.Text = "0";
            dgvMateriar.DataSource = null;
            dgvMateriarInput.DataSource = null;
            flwOperation.Controls.Clear();
            return;
         }
         Lot = Lots.Find((l) => l.LOT_ID.Equals(cboLotId.Text));
         if (Lot != null)
         {
            lblOrderStatus.Text = order.ORDER_STATUS;
            lblOrderQty.Text = Convert.ToInt32(order.ORDER_QTY).ToString();

            txtProductCode.Text = order.PRODUCT_CODE;
            txtProductName.Text = order.PRODUCT_NAME;
            txtCustomerCode.Text = order.CUSTOMER_CODE;
            txtCustomerName.Text = order.CUSTOMER_NAME;

            txtLotDesc.Text = Lot.LOT_DESC;
            txtOperationCode.Text = Lot.OPERATION_CODE;
            txtOperationName.Text = Lot.OPERATION_NAME;
            txtTotal.Text = Convert.ToInt32(Lot.START_QTY).ToString();
            lblProductQty.Text = Convert.ToInt32(Lot.LOT_QTY).ToString();
            lblDefectQty.Text = Convert.ToInt32(Lot.LOT_DEFECT_QTY).ToString();

            var list = operations.FindAll((o) => o.PRODUCT_CODE.Equals(Lot.PRODUCT_CODE)).OrderBy((o) => o.FLOW_SEQ).ToList();
            var operation = operations.Find((o) => o.PRODUCT_CODE.Equals(Lot.PRODUCT_CODE) && o.OPERATION_CODE.Equals(Lot.OPERATION_CODE));
            if (list.Count > 0)
            {
               int size = flwOperation.Width / list.Count;
               flwOperation.Controls.Clear();
               foreach (var item in list)
               {
                  Label label = new Label();
                  label.Margin = new Padding(0);
                  label.AutoSize = false;
                  label.BorderStyle = BorderStyle.FixedSingle;
                  label.Size = new Size(size, 35);
                  label.Text = item.OPERATION_NAME + "[완료]";
                  label.Font = new Font("맑은 고딕", 11, FontStyle.Bold);
                  label.BackColor = Color.YellowGreen;
                  label.TextAlign = ContentAlignment.MiddleCenter;
                  if (item.FLOW_SEQ == operation.FLOW_SEQ)
                  {
                     label.Text = item.OPERATION_NAME + "[진행 중]";
                     label.BackColor = Color.Gold;
                  }
                  else if (item.FLOW_SEQ > operation.FLOW_SEQ)
                  {
                     label.Text = item.OPERATION_NAME + "[대기 중]";
                     label.BackColor = Color.Gray;
                  }
                  flwOperation.Controls.Add(label);
               }
            }
            else
               MboxUtil.MboxError("공정 진행정보를 불러오는데 오류가 발생했습니다.");
            SelectedLotsMateriar = new List<LOT_STS_DTO>();
            LotsMaterialr = srvFlag.SelectLotMateriars(Lot.PRODUCT_CODE, operation.OPERATION_CODE);
            dgvMateriar.DataSource = null;
            var temp = LotsMaterialr.Distinct();
            dgvMateriar.DataSource = LotsMaterialr;
            dgvMateriarInput.DataSource = (from t in temp select new { PRODUCT_CODE = t.PRODUCT_CODE, PRODUCT_NAME = t.PRODUCT_NAME, REQUIRE_QTY = t.REQUIRE_QTY, 
                                           TOTAL = Convert.ToInt32(t.REQUIRE_QTY * Convert.ToDecimal(txtTotal.Text)) }).ToList();
            dgvMateriarInput.ClearSelection();
         }
         else
         {
            MboxUtil.MboxError("LOT 이력을 불러오는데 오류가 발생했습니다.");
            return;
         }
      }
      private void dgvMateriar_CellValueChanged(object sender, DataGridViewCellEventArgs e)
      {
         int row = e.RowIndex;
         int col = e.ColumnIndex;
         if (row < 0) return;
         string prodCode = dgvMateriar["PRODUCT_CODE", row].Value.ToString();
         string lotId = dgvMateriar["LOT_ID", row].Value.ToString();
         int idx = 0;
         foreach (DataGridViewRow Row in dgvMateriarInput.Rows)
         {
            if (Equals(Row.Cells["PRODUCT_CODE"].Value.ToString(), prodCode))
               break;
            idx++;
         }
         if (col == 0)
         {
            bool check = dgvMateriar["CHECK", row].Value == null ? false : Convert.ToBoolean(dgvMateriar["CHECK", row].Value.ToString());
            int input = Convert.ToInt32(dgvMateriarInput["INPUT", idx].Value);
            int total = Convert.ToInt32(dgvMateriarInput["TOTAL", idx].Value);
            int inputQty = 0;
            foreach (DataGridViewRow row2 in dgvMateriar.Rows)
            {
               if (Convert.ToBoolean(row2.Cells[0].Value))
               {
                  inputQty += Convert.ToInt32(row2.Cells["LOT_QTY"].Value);
               }
            }
            if (check)
            {
               if (input >= total)
               {
                  MboxUtil.MboxWarn("필요 수량 이상 선택할 수 없습니다.");
                  dgvMateriar["CHECK", row].Value = false;
                  return;
               }
               dgvMateriarInput["INPUT", idx].Value = inputQty >= total ? total : inputQty;
               SelectedLotsMateriar.Add(LotsMaterialr.Find((m) => m.LOT_ID.Equals(lotId)));
            }
            else
            {
               dgvMateriarInput["INPUT", idx].Value = inputQty <= 0 ? 0 : inputQty;
               SelectedLotsMateriar.Remove(LotsMaterialr.Find((m) => m.LOT_ID.Equals(lotId)));
            }

            if (inputQty >= total)
               isPass = true;
            else
               isPass = false;
         }
      }
      private void btnStart_Click(object sender, EventArgs e)
      {
         if (string.IsNullOrWhiteSpace(txtOrder.Text))
         {
            MboxUtil.MboxWarn("작업 지시서를 선택해주십시오.");
            return;
         }
         if (cboLotId.SelectedIndex < 1)
         {
            MboxUtil.MboxWarn("LOT 정보를 선택해주십시오.");
            return;
         }
         if (LotsMaterialr == null)
         {
            MboxUtil.MboxWarn("자 품번 LOT가 존재하지 않습니다.");
            return;
         }
         if (!isPass)
         {
            MboxUtil.MboxWarn("자재 필요 수량과 사용 수량이 일치하지 않습니다.");
            return;
         }

         Lot.LAST_TRAN_CODE = "INPUT";
         Lot.LAST_TRAN_TIME = DateTime.Now;
         Lot.LAST_TRAN_USER_ID = "TEST";
         Lot.LAST_TRAN_COMMENT = txtDesc.Text;
         Lot.LAST_HIST_SEQ += 1;

         List<LOT_MATERIAL_HIS_DTO> hisMaterial = new List<LOT_MATERIAL_HIS_DTO>();
         var groupMateriar = SelectedLotsMateriar.GroupBy((m) => m.PRODUCT_CODE).Select((m) => new { GroupKey = m.Key, Materiar = m.OrderBy((o) => o.CREATE_TIME)});
         foreach (var group in groupMateriar)
         {
            decimal total = Convert.ToInt32(LotsMaterialr.Find((m) => m.PRODUCT_CODE.Equals(group.GroupKey)).REQUIRE_QTY * Convert.ToDecimal(txtTotal.Text));
            foreach (var item in group.Materiar)
            {
               int idx = LotsMaterialr.FindIndex((m) => m.LOT_ID.Equals(item.LOT_ID));
               decimal qty = total <= item.LOT_QTY ? total : item.LOT_QTY; 
               total -= item.LOT_QTY;
               LotsMaterialr[idx].LOT_QTY = LotsMaterialr[idx].LOT_QTY - qty;
               LotsMaterialr[idx].LAST_HIST_SEQ = LotsMaterialr[idx].LAST_HIST_SEQ;
               LotsMaterialr[idx].LAST_TRAN_CODE = "INPUT";
               LotsMaterialr[idx].LAST_TRAN_TIME = DateTime.Now;
               LotsMaterialr[idx].LAST_TRAN_COMMENT = "자재 사용";
               LotsMaterialr[idx].LAST_TRAN_USER_ID = "TEST";
               LOT_MATERIAL_HIS_DTO dto = new LOT_MATERIAL_HIS_DTO
               {
                  LOT_ID = Lot.LOT_ID,
                  HIST_SEQ = Lot.LAST_HIST_SEQ,
                  MATERIAL_LOT_ID = item.LOT_ID,
                  MATERIAL_LOT_HIST_SEQ = item.LAST_HIST_SEQ,
                  INPUT_QTY = qty.ToString(),
                  CHILD_PRODUCT_CODE = item.PRODUCT_CODE,
                  MATERIAL_STORE_CODE = item.STORE_CODE,
                  TRAN_TIME = item.LAST_TRAN_TIME,
                  WORK_DATE = DateTime.Now.ToString("yyyyMMdd"),
                  PRODUCT_CODE = Lot.PRODUCT_CODE,
                  OPERATION_CODE = Lot.OPERATION_CODE,
                  EQUIPMENT_CODE = Lot.START_EQUIPMENT_CODE,
                  TRAN_USER_ID = item.LAST_TRAN_USER_ID,
                  TRAN_COMMENT = item.LAST_TRAN_COMMENT
               };
               hisMaterial.Add(dto);
               LOT_MATERIAL_HIS_DTO dto2 = new LOT_MATERIAL_HIS_DTO
               {
                  LOT_ID = item.LOT_ID,
                  HIST_SEQ = item.LAST_HIST_SEQ,
                  MATERIAL_LOT_ID = Lot.LOT_ID,
                  MATERIAL_LOT_HIST_SEQ = Lot.LAST_HIST_SEQ,
                  INPUT_QTY = qty.ToString(),
                  CHILD_PRODUCT_CODE = null,
                  MATERIAL_STORE_CODE = item.STORE_CODE,
                  TRAN_TIME = item.LAST_TRAN_TIME,
                  WORK_DATE = DateTime.Now.ToString("yyyyMMdd"),
                  PRODUCT_CODE = item.PRODUCT_CODE,
                  OPERATION_CODE = Lot.OPERATION_CODE,
                  EQUIPMENT_CODE = Lot.START_EQUIPMENT_CODE,
                  TRAN_USER_ID = item.LAST_TRAN_USER_ID,
                  TRAN_COMMENT = item.LAST_TRAN_COMMENT
               };
               hisMaterial.Add(dto2);
            }
         }
         bool result = srvFlag.InsertMateriar(Lot, LotsMaterialr, hisMaterial);
         if (!result)
         {
            MboxUtil.MboxError("오류가 발생했습니다.");
            return;
         }
         MboxUtil.MboxInfo("자재가 사용되었습니다.");
         cboLotId.SelectedIndex = 0;
         Lots = srvFlag.SelectOrderLotBed(txtOrder.Text);
      }
   }
}
