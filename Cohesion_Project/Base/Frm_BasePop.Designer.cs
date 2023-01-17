
namespace Cohesion_Project.Base
{
   partial class Frm_BasePop
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
         this.SuspendLayout();
         // 
         // Frm_BasePop
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(217)))), ((int)(((byte)(226)))));
         this.ClientSize = new System.Drawing.Size(911, 558);
         this.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
         this.Name = "Frm_BasePop";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "Frm_BasePop";
         this.ResumeLayout(false);

      }

      #endregion
      private System.ComponentModel.BackgroundWorker backgroundWorker1;
   }
}