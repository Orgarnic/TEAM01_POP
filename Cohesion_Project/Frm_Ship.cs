using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cohesion_DTO;

namespace Cohesion_Project
{
    public partial class Frm_Ship : Cohesion_Project.Frm_Base3Line
    {
        SalesOrder_DTO OrderInfo = new SalesOrder_DTO();
        public Frm_Ship()
        {
            InitializeComponent();
        }

        private void Btn_Ship_Click(object sender, EventArgs e)
        {
            Pop_Ship pop = new Pop_Ship();
            pop.ShowDialog();
            OrderInfo = pop.SelectOrder;
        }
    }
}
