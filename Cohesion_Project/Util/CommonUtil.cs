using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
   }
}
