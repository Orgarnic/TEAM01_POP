using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cohesion_DTO;


namespace Cohesion_Project
{
   class CommonUtil
   {
      public static void ResetControls(params object[] controls)
      {
         foreach (var control in controls)
         {
            if(control is TextBox txt)
               txt.Text = string.Empty;
            if (control is Label lbl)
               lbl.Text = string.Empty;
            if (control is ComboBox cbo)
               cbo.SelectedIndex = 0;
            if (control is DateTimePicker dtp)
               dtp.Value = DateTime.Now;
            if (control is NumericUpDown nud)
               nud.Value = 0;
            if (control is ListBox lst)
               lst.Items.Clear();
            if (control is TreeView tvw)
               tvw.Nodes.Clear();
            if (control is DataGridView dgv)
               dgv.DataSource = null;
         }
      }

      public static bool VelidControls(params object[] controls)
      {
         StringBuilder sb = new StringBuilder();
         foreach (var control in controls)
         {
            if (control is TextBox txt && string.IsNullOrWhiteSpace(txt.Text))
            {
               sb.Append($"[{txt.Tag}], ");
            }
            if (control is ComboBox cbo && cbo.SelectedIndex <= 0)
               sb.Append($"[{cbo.Tag}], ");
         }
         if(sb.Length > 0)
         {
            MboxUtil.MboxWarn(sb.ToString().Trim().TrimEnd(',') + Environment.NewLine + "목록은 필수 입력사항입니다.");
            return false;
         }
         return true;
      }

      public LOT_HIS_DTO LotStsToLotHis(LOT_STS_DTO dto)
      {
         LOT_HIS_DTO his = new LOT_HIS_DTO
         {
            LOT_ID = dto.LOT_ID,
            LOT_DESC = dto.LOT_DESC,
            PRODUCT_CODE = dto.PRODUCT_CODE,
            OPERATION_CODE = dto.OPERATION_CODE,
            STORE_CODE = dto.STORE_CODE,
            LOT_QTY = dto.LOT_QTY,
            CREATE_QTY = dto.CREATE_QTY,
            OPER_IN_QTY = dto.OPER_IN_QTY,
            START_FLAG = dto.START_FLAG,
            START_QTY = dto.START_QTY,
            START_TIME = dto.START_TIME,
            START_EQUIPMENT_CODE = dto.START_EQUIPMENT_CODE,
            END_FLAG = dto.END_FLAG,
            END_TIME = dto.END_TIME,
            END_EQUIPMENT_CODE = dto.END_EQUIPMENT_CODE,
            SHIP_FLAG = dto.SHIP_FLAG,
            SHIP_CODE = dto.SHIP_CODE,
            SHIP_TIME = dto.SHIP_TIME,
            PRODUCTION_TIME = dto.PRODUCTION_TIME,
            CREATE_TIME = dto.CREATE_TIME,
            OPER_IN_TIME = dto.OPER_IN_TIME,
            WORK_ORDER_ID = dto.WORK_ORDER_ID,
            LOT_DELETE_FLAG = dto.LOT_DELETE_FLAG,
            LOT_DELETE_CODE = dto.LOT_DELETE_CODE,
            LOT_DELETE_TIME = dto.LOT_DELETE_TIME,
            TRAN_CODE = dto.LAST_TRAN_CODE,
            TRAN_TIME = dto.LAST_TRAN_TIME,
            TRAN_USER_ID = dto.LAST_TRAN_USER_ID,
            TRAN_COMMENT = dto.LAST_TRAN_COMMENT,
            HIST_SEQ = dto.LAST_HIST_SEQ,
            WORK_DATE = DateTime.Now.ToString("yyyy-MM-dd")
         };
         return his;
      }
   }
}
