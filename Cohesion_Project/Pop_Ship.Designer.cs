
namespace Cohesion_Project
{
   partial class Pop_Ship
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
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvOrderList = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderList)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtSearch.Location = new System.Drawing.Point(6, 9);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(837, 29);
            this.txtSearch.TabIndex = 60;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dgvOrderList);
            this.panel2.Location = new System.Drawing.Point(6, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(898, 494);
            this.panel2.TabIndex = 62;
            // 
            // dgvOrderList
            // 
            this.dgvOrderList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrderList.Location = new System.Drawing.Point(0, 0);
            this.dgvOrderList.Name = "dgvOrderList";
            this.dgvOrderList.RowTemplate.Height = 23;
            this.dgvOrderList.Size = new System.Drawing.Size(896, 492);
            this.dgvOrderList.TabIndex = 4;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(34)))), ((int)(((byte)(41)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSearch.Image = global::Cohesion_Project.Properties.Resources.Search;
            this.btnSearch.Location = new System.Drawing.Point(849, 9);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(55, 31);
            this.btnSearch.TabIndex = 61;
            this.btnSearch.Text = "검색";
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(56)))), ((int)(((byte)(67)))));
            this.btnConfirm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(595, 546);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(0);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(150, 43);
            this.btnConfirm.TabIndex = 64;
            this.btnConfirm.Text = "선택";
            this.btnConfirm.UseVisualStyleBackColor = false;
            // 
            // Btn_Close
            // 
            this.Btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Close.BackColor = System.Drawing.Color.Tomato;
            this.Btn_Close.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Btn_Close.FlatAppearance.BorderSize = 0;
            this.Btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Btn_Close.ForeColor = System.Drawing.Color.White;
            this.Btn_Close.Location = new System.Drawing.Point(754, 546);
            this.Btn_Close.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(150, 43);
            this.Btn_Close.TabIndex = 63;
            this.Btn_Close.Text = "닫기";
            this.Btn_Close.UseVisualStyleBackColor = false;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Pop_Ship
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(911, 595);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Name = "Pop_Ship";
            this.Text = "주문서 선택";
            this.Load += new System.EventHandler(this.Pop_Ship_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button btnSearch;
      protected System.Windows.Forms.TextBox txtSearch;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.DataGridView dgvOrderList;
      protected System.Windows.Forms.Button btnConfirm;
      protected System.Windows.Forms.Button Btn_Close;
   }
}
