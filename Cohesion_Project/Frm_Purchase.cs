using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cohesion_Project
{
   public partial class Frm_Purchase : Cohesion_Project.Frm_Base3Line
   {
      public Frm_Purchase()
      {
         InitializeComponent();
      }

      private void Btn_Purchase_Click(object sender, EventArgs e)
      {
         Pop_Purchase pop = new Pop_Purchase();
         pop.ShowDialog();
      }
   }
}
