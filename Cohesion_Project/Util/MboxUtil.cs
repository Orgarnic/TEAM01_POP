using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cohesion_Project
{
   class MboxUtil
   {
      public static void MboxInfo(string msg)
      {
         MessageBox.Show(msg, "알람", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      public static void MboxWarn(string msg)
      {
         MessageBox.Show(msg, "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
      public static void MboxError(string msg)
      {
         MessageBox.Show(msg, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      public static bool MboxInfo_(string msg)
      {
         return MessageBox.Show(msg, "알람", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK;
      }
      public static bool MboxWarn_(string msg)
      {
         return MessageBox.Show(msg, "경고", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK;
      }
      public static bool MboxError_(string msg)
      {
          return MessageBox.Show(msg, "오류", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK;
      }
   }
}
