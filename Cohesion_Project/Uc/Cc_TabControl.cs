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
   // 닫기 이미지를 그리기 위해서 사용자 지정 컨트롤을 탭을 생성
   // 1. DrawMode 속성을 OwnerDrawFixed 로 준다.
   // 2. Tab이 그려질 때 DrawItem 이벤트를 구현한다.

   public partial class Cc_TabControl : TabControl
   {
      public Cc_TabControl()
      {
         InitializeComponent();

         this.DrawMode = TabDrawMode.OwnerDrawFixed; // 그리기위해서 Owner로 준다.
      }

      protected override void OnPaint(PaintEventArgs pe)
      {
         base.OnPaint(pe);
      }

      // System.Drawing
      protected override void OnDrawItem(DrawItemEventArgs e)
      {
         base.OnDrawItem(e);
         try
         {
            Rectangle r = e.Bounds;
            r = this.GetTabRect(e.Index);   
            r.Offset(2, 2);

            if (this.SelectedIndex == e.Index)
            {
               e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(178, 199, 213)), e.Bounds);
            }
            //탭의 글씨
            SolidBrush titleBrush = new SolidBrush(Color.Black);
            string title = this.TabPages[e.Index].Text;
            Font f = this.Font;
            e.Graphics.DrawString(title, f, titleBrush, new Point(r.X, r.Y));

            Image img;
            if (this.SelectedIndex == e.Index)
               img = imageList1.Images[1];
            else
               img = imageList1.Images[0];

            Point imgLocation = new Point(18, 5);

            e.Graphics.DrawImage(img, new Point(r.X + this.GetTabRect(e.Index).Width - imgLocation.X, imgLocation.Y));

            img.Dispose();
            img = null;
         }
         catch
         {

         }
      }
   }
}
