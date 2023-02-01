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
   public partial class Frm_Main : Form
   {
      Button ActivBtn;

      public Frm_Main()
      {
         InitializeComponent();
      }
      private void Frm_Main_Load(object sender, EventArgs e)
      {

      }
      private void Btn_Close_Click(object sender, EventArgs e)
      {
         Application.Exit();
      }

      private void Btn_Income_Click(object sender, EventArgs e)
      {
         Activte_BtnChanged(sender as Button);
         Frm_Purchase frm = new Frm_Purchase();
         frm.WindowState = FormWindowState.Maximized;
         frm.MdiParent = this;
         Lbl_MenuText.Text = frm.Text;
         frm.Show();
      }
      private void Mst_Main_ItemAdded(object sender, ToolStripItemEventArgs e)
      {
         if (e.Item.Text == "" || e.Item.Text == "최소화(&N)" || e.Item.Text == "이전 크기로(&R)" || e.Item.Text == "닫기(&C)")
         {
            e.Item.Visible = false;
         }
      }
      private void button11_Click(object sender, EventArgs e)
      {
         Activte_BtnChanged(sender as Button);
         Frm_InspectFlag frm = new Frm_InspectFlag();
         frm.WindowState = FormWindowState.Maximized;
         frm.MdiParent = this;
         Lbl_MenuText.Text = frm.Text;
         frm.Show();
      }
      private void Activte_BtnChanged(Button btn)
      {
         if (ActivBtn == null)
         {
            ActivBtn = btn;
            ActivBtn.BackColor = Color.FromArgb(49, 56, 67);
            return;
         }
         ActivBtn.BackColor = Color.Transparent;
         ActivBtn = btn;
         ActivBtn.BackColor = Color.FromArgb(49, 56, 67);
      }

      private void button6_Click(object sender, EventArgs e)
      {
         Activte_BtnChanged(sender as Button);
         Frm_Ship frm = new Frm_Ship();
         frm.WindowState = FormWindowState.Maximized;
         frm.MdiParent = this;
         Lbl_MenuText.Text = frm.Text;
         frm.Show();
      }

      private void button3_Click(object sender, EventArgs e)
      {
         Activte_BtnChanged(sender as Button);
         Frm_Order frm = new Frm_Order();
         frm.WindowState = FormWindowState.Maximized;
         frm.MdiParent = this;
         Lbl_MenuText.Text = frm.Text;
         frm.Show();
      }

      private void button5_Click(object sender, EventArgs e)
      {
         Activte_BtnChanged(sender as Button);
         Frm_WorkStart frm = new Frm_WorkStart();
         frm.WindowState = FormWindowState.Maximized;
         frm.MdiParent = this;
         Lbl_MenuText.Text = frm.Text;
         frm.Show();
      }

      private void button4_Click(object sender, EventArgs e)
      {
         Activte_BtnChanged(sender as Button);
         Frm_LookUp frm = new Frm_LookUp();
         frm.WindowState = FormWindowState.Maximized;
         frm.MdiParent = this;
         Lbl_MenuText.Text = frm.Text;
         frm.Show();
      }

      private void button9_Click(object sender, EventArgs e)
      {
         Activte_BtnChanged(sender as Button);
         Frm_BedFlag frm = new Frm_BedFlag();
         frm.WindowState = FormWindowState.Maximized;
         frm.MdiParent = this;
         Lbl_MenuText.Text = frm.Text;
         frm.Show();
      }

      private void button10_Click(object sender, EventArgs e)
      {
         Activte_BtnChanged(sender as Button);
         Frm_BedFlag frm = new Frm_BedFlag();
         frm.WindowState = FormWindowState.Maximized;
         frm.MdiParent = this;
         Lbl_MenuText.Text = frm.Text;
         frm.Show();
      }

      private void button8_Click(object sender, EventArgs e)
      {
         Activte_BtnChanged(sender as Button);
         Frm_MateriarFlag frm = new Frm_MateriarFlag();
         frm.WindowState = FormWindowState.Maximized;
         frm.MdiParent = this;
         Lbl_MenuText.Text = frm.Text;
         frm.Show();
      }

      private void button7_Click(object sender, EventArgs e)
      {
         Activte_BtnChanged(sender as Button);
         Frm_WorkEnd frm = new Frm_WorkEnd();
         frm.WindowState = FormWindowState.Maximized;
         frm.MdiParent = this;
         Lbl_MenuText.Text = frm.Text;
         frm.Show();
      }

      private void button12_Click(object sender, EventArgs e)
      {
         Activte_BtnChanged(sender as Button);
         Frm_NonOper frm = new Frm_NonOper();
         frm.WindowState = FormWindowState.Maximized;
         frm.MdiParent = this;
         Lbl_MenuText.Text = frm.Text;
         frm.Show();
      }

      private void button2_Click(object sender, EventArgs e)
      {
         Activte_BtnChanged(sender as Button);
         Frm_NonOperLookUp frm = new Frm_NonOperLookUp();
         frm.WindowState = FormWindowState.Maximized;
         frm.MdiParent = this;
         Lbl_MenuText.Text = frm.Text;
         frm.Show();
      }

      private void button1_Click(object sender, EventArgs e)
      {
         Activte_BtnChanged(sender as Button);
         Frm_InspectLookUp frm = new Frm_InspectLookUp();
         frm.WindowState = FormWindowState.Maximized;
         frm.MdiParent = this;
         Lbl_MenuText.Text = frm.Text;
         frm.Show();
      }
   }
}
