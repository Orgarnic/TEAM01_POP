using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cohesion_Project.Base
{
   public partial class Frm_BasePop : Form
   {
      public Frm_BasePop()
      {
         InitializeComponent();
      }

      private void Btn_Down_Click(object sender, EventArgs e)
      {
         this.DialogResult = DialogResult.Cancel;
         this.Close();
      }
   }
}
