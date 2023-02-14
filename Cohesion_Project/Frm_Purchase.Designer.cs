
namespace Cohesion_Project
{
   partial class Frm_Purchase
   {
      /// <summary>
      /// 필수 디자이너 변수입니다.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// 사용 중인 모든 리소스를 정리합니다.
      /// </summary>
      /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form 디자이너에서 생성한 코드

      /// <summary>
      /// 디자이너 지원에 필요한 메서드입니다. 
      /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
      /// </summary>
      private void InitializeComponent()
      {
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.Btn_Purchase = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.cboPurchaseID = new System.Windows.Forms.ComboBox();
            this.dgvPurchaseList = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboPurchaseID);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.Btn_Purchase);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Size = new System.Drawing.Size(1454, 93);
            this.panel1.Controls.SetChildIndex(this.label9, 0);
            this.panel1.Controls.SetChildIndex(this.label1, 0);
            this.panel1.Controls.SetChildIndex(this.label8, 0);
            this.panel1.Controls.SetChildIndex(this.Btn_Purchase, 0);
            this.panel1.Controls.SetChildIndex(this.button3, 0);
            this.panel1.Controls.SetChildIndex(this.cboPurchaseID, 0);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Location = new System.Drawing.Point(12, 794);
            this.panel2.Size = new System.Drawing.Size(1453, 31);
            this.panel2.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgvPurchaseList);
            this.panel3.Location = new System.Drawing.Point(12, 116);
            this.panel3.Size = new System.Drawing.Size(1454, 709);
            this.panel3.Controls.SetChildIndex(this.label3, 0);
            this.panel3.Controls.SetChildIndex(this.dgvPurchaseList, 0);
            // 
            // label1
            // 
            this.label1.Text = "구매 납품서";
            // 
            // label2
            // 
            this.label2.Text = "구매 납품서 정보";
            // 
            // label3
            // 
            this.label3.Text = "구매 납품서 입고 목록";
            // 
            // Btn_Close
            // 
            this.Btn_Close.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(56)))), ((int)(((byte)(67)))));
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(16, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(138, 30);
            this.label8.TabIndex = 16;
            this.label8.Text = "◾ 납품서";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(16, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(138, 30);
            this.label9.TabIndex = 15;
            this.label9.Text = "◾ label4";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Btn_Purchase
            // 
            this.Btn_Purchase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(34)))), ((int)(((byte)(41)))));
            this.Btn_Purchase.FlatAppearance.BorderSize = 0;
            this.Btn_Purchase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Purchase.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Purchase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Btn_Purchase.Image = global::Cohesion_Project.Properties.Resources.Search;
            this.Btn_Purchase.Location = new System.Drawing.Point(396, 47);
            this.Btn_Purchase.Name = "Btn_Purchase";
            this.Btn_Purchase.Size = new System.Drawing.Size(55, 29);
            this.Btn_Purchase.TabIndex = 18;
            this.Btn_Purchase.Text = "조회";
            this.Btn_Purchase.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Btn_Purchase.UseVisualStyleBackColor = false;
            this.Btn_Purchase.Click += new System.EventHandler(this.Btn_Purchase_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(34)))), ((int)(((byte)(41)))));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button3.Location = new System.Drawing.Point(456, 47);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 29);
            this.button3.TabIndex = 19;
            this.button3.Text = "납품 목록 조회";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Visible = false;
            // 
            // cboPurchaseID
            // 
            this.cboPurchaseID.Font = new System.Drawing.Font("나눔고딕", 14.25F);
            this.cboPurchaseID.FormattingEnabled = true;
            this.cboPurchaseID.Location = new System.Drawing.Point(125, 46);
            this.cboPurchaseID.Name = "cboPurchaseID";
            this.cboPurchaseID.Size = new System.Drawing.Size(265, 29);
            this.cboPurchaseID.TabIndex = 20;
            // 
            // dgvPurchaseList
            // 
            this.dgvPurchaseList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPurchaseList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPurchaseList.Location = new System.Drawing.Point(0, 30);
            this.dgvPurchaseList.Name = "dgvPurchaseList";
            this.dgvPurchaseList.RowTemplate.Height = 23;
            this.dgvPurchaseList.Size = new System.Drawing.Size(1452, 677);
            this.dgvPurchaseList.TabIndex = 4;
            this.dgvPurchaseList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPurchaseList_CellClick);
            // 
            // Frm_Purchase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(1478, 878);
            this.Name = "Frm_Purchase";
            this.Text = "구매 입고";
            this.Load += new System.EventHandler(this.Frm_Purchase_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseList)).EndInit();
            this.ResumeLayout(false);

      }

      #endregion
      protected System.Windows.Forms.Label label8;
      protected System.Windows.Forms.Label label9;
      private System.Windows.Forms.Button Btn_Purchase;
      private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox cboPurchaseID;
        private System.Windows.Forms.DataGridView dgvPurchaseList;
    }
}
