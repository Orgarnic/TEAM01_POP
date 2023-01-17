
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
         this.textBox1 = new System.Windows.Forms.TextBox();
         this.panel2 = new System.Windows.Forms.Panel();
         this.dataGridView1 = new System.Windows.Forms.DataGridView();
         this.button3 = new System.Windows.Forms.Button();
         this.button2 = new System.Windows.Forms.Button();
         this.Btn_Close = new System.Windows.Forms.Button();
         this.panel2.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
         this.SuspendLayout();
         // 
         // textBox1
         // 
         this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.textBox1.Enabled = false;
         this.textBox1.Font = new System.Drawing.Font("나눔고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
         this.textBox1.Location = new System.Drawing.Point(6, 8);
         this.textBox1.Name = "textBox1";
         this.textBox1.Size = new System.Drawing.Size(837, 29);
         this.textBox1.TabIndex = 60;
         // 
         // panel2
         // 
         this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.panel2.Controls.Add(this.dataGridView1);
         this.panel2.Location = new System.Drawing.Point(6, 43);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(898, 463);
         this.panel2.TabIndex = 62;
         // 
         // dataGridView1
         // 
         this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataGridView1.Location = new System.Drawing.Point(0, 0);
         this.dataGridView1.Name = "dataGridView1";
         this.dataGridView1.RowTemplate.Height = 23;
         this.dataGridView1.Size = new System.Drawing.Size(896, 461);
         this.dataGridView1.TabIndex = 4;
         // 
         // button3
         // 
         this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(34)))), ((int)(((byte)(41)))));
         this.button3.FlatAppearance.BorderSize = 0;
         this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.button3.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
         this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
         this.button3.Image = global::Cohesion_Project.Properties.Resources.Search;
         this.button3.Location = new System.Drawing.Point(849, 8);
         this.button3.Name = "button3";
         this.button3.Size = new System.Drawing.Size(55, 29);
         this.button3.TabIndex = 61;
         this.button3.Text = "검색";
         this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
         this.button3.UseVisualStyleBackColor = false;
         // 
         // button2
         // 
         this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(56)))), ((int)(((byte)(67)))));
         this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
         this.button2.FlatAppearance.BorderSize = 0;
         this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.button2.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
         this.button2.ForeColor = System.Drawing.Color.White;
         this.button2.Location = new System.Drawing.Point(595, 512);
         this.button2.Margin = new System.Windows.Forms.Padding(0);
         this.button2.Name = "button2";
         this.button2.Size = new System.Drawing.Size(150, 40);
         this.button2.TabIndex = 64;
         this.button2.Text = "선택";
         this.button2.UseVisualStyleBackColor = false;
         // 
         // Btn_Close
         // 
         this.Btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.Btn_Close.BackColor = System.Drawing.Color.Tomato;
         this.Btn_Close.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
         this.Btn_Close.FlatAppearance.BorderSize = 0;
         this.Btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.Btn_Close.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
         this.Btn_Close.ForeColor = System.Drawing.Color.White;
         this.Btn_Close.Location = new System.Drawing.Point(754, 512);
         this.Btn_Close.Margin = new System.Windows.Forms.Padding(0);
         this.Btn_Close.Name = "Btn_Close";
         this.Btn_Close.Size = new System.Drawing.Size(150, 40);
         this.Btn_Close.TabIndex = 63;
         this.Btn_Close.Text = "닫기";
         this.Btn_Close.UseVisualStyleBackColor = false;
         this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
         // 
         // Pop_Ship
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
         this.ClientSize = new System.Drawing.Size(911, 558);
         this.Controls.Add(this.button2);
         this.Controls.Add(this.Btn_Close);
         this.Controls.Add(this.panel2);
         this.Controls.Add(this.button3);
         this.Controls.Add(this.textBox1);
         this.Name = "Pop_Ship";
         this.Text = "주문서 선택";
         this.panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button button3;
      protected System.Windows.Forms.TextBox textBox1;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.DataGridView dataGridView1;
      protected System.Windows.Forms.Button button2;
      protected System.Windows.Forms.Button Btn_Close;
   }
}
