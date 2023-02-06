using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cohesion_Project
{
   public partial class Frm_LookUp : Form
   {
      public Frm_LookUp()
      {
         InitializeComponent();
      }
        private void DataGridViewBinding()
        {
            DgvUtil.DgvInit(dgv_LOT_List);
            DgvUtil.AddTextCol(dgv_LOT_List, "LOT ID", "LOT_ID", readOnly: true, align: 0);

        }
   }
}
