using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cohesion_Project
{
   public partial class Pop_Purchase : Cohesion_Project.Base.Frm_BasePop
   {
      public Pop_Purchase()
      {
         InitializeComponent();
      }

      private void Btn_Close_Click(object sender, EventArgs e)
      {
         this.Close();
      }
   }
}
