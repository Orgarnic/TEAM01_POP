
namespace Cohesion_Project
{
   partial class Frm_Ship
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
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOrderNum = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCustomerCode = new System.Windows.Forms.TextBox();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpOrderDate = new System.Windows.Forms.DateTimePicker();
            this.btnSelectStock = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtTotalQty = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.Btn_Ship = new System.Windows.Forms.Button();
            this.dgvStockProduct = new System.Windows.Forms.DataGridView();
            this.lstLot = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSelectStock);
            this.panel1.Controls.Add(this.Btn_Ship);
            this.panel1.Controls.Add(this.txtOrderNum);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Size = new System.Drawing.Size(1454, 99);
            this.panel1.Controls.SetChildIndex(this.label9, 0);
            this.panel1.Controls.SetChildIndex(this.label1, 0);
            this.panel1.Controls.SetChildIndex(this.label8, 0);
            this.panel1.Controls.SetChildIndex(this.txtOrderNum, 0);
            this.panel1.Controls.SetChildIndex(this.Btn_Ship, 0);
            this.panel1.Controls.SetChildIndex(this.btnSelectStock, 0);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtProductCode);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.dtpOrderDate);
            this.panel2.Controls.Add(this.txtCustomerCode);
            this.panel2.Controls.Add(this.txtCustomerName);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.txtQty);
            this.panel2.Controls.Add(this.txtProductName);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Location = new System.Drawing.Point(12, 124);
            this.panel2.Size = new System.Drawing.Size(1453, 209);
            this.panel2.Controls.SetChildIndex(this.label6, 0);
            this.panel2.Controls.SetChildIndex(this.label7, 0);
            this.panel2.Controls.SetChildIndex(this.txtProductName, 0);
            this.panel2.Controls.SetChildIndex(this.label2, 0);
            this.panel2.Controls.SetChildIndex(this.txtQty, 0);
            this.panel2.Controls.SetChildIndex(this.label13, 0);
            this.panel2.Controls.SetChildIndex(this.label12, 0);
            this.panel2.Controls.SetChildIndex(this.label11, 0);
            this.panel2.Controls.SetChildIndex(this.label10, 0);
            this.panel2.Controls.SetChildIndex(this.txtCustomerName, 0);
            this.panel2.Controls.SetChildIndex(this.txtCustomerCode, 0);
            this.panel2.Controls.SetChildIndex(this.dtpOrderDate, 0);
            this.panel2.Controls.SetChildIndex(this.label5, 0);
            this.panel2.Controls.SetChildIndex(this.txtProductCode, 0);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgvStockProduct);
            this.panel3.Location = new System.Drawing.Point(12, 346);
            this.panel3.Size = new System.Drawing.Size(1105, 534);
            this.panel3.Controls.SetChildIndex(this.label3, 0);
            this.panel3.Controls.SetChildIndex(this.dgvStockProduct, 0);
            // 
            // label1
            // 
            this.label1.Size = new System.Drawing.Size(1452, 32);
            this.label1.Text = "제품 주문서";
            // 
            // label2
            // 
            this.label2.Size = new System.Drawing.Size(1451, 32);
            this.label2.Text = "제품 주문서 정보";
            // 
            // label3
            // 
            this.label3.Size = new System.Drawing.Size(1103, 32);
            this.label3.Text = "제품 주문서 창고 재고 목록";
            // 
            // Btn_Close
            // 
            this.Btn_Close.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(56)))), ((int)(((byte)(67)))));
            this.button1.Text = "출하";
            this.button1.Click += new System.EventHandler(this.btnShip_Click);
            // 
            // txtQty
            // 
            this.txtQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQty.Enabled = false;
            this.txtQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtQty.Location = new System.Drawing.Point(125, 162);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(265, 29);
            this.txtQty.TabIndex = 14;
            // 
            // txtProductName
            // 
            this.txtProductName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProductName.Enabled = false;
            this.txtProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtProductName.Location = new System.Drawing.Point(125, 105);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(265, 29);
            this.txtProductName.TabIndex = 13;
            // 
            // txtProductCode
            // 
            this.txtProductCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProductCode.Enabled = false;
            this.txtProductCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtProductCode.Location = new System.Drawing.Point(125, 47);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(265, 29);
            this.txtProductCode.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(16, 161);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 32);
            this.label7.TabIndex = 11;
            this.label7.Text = "◾ 주문수량";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(16, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 32);
            this.label6.TabIndex = 10;
            this.label6.Text = "◾ 품명";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(16, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 32);
            this.label5.TabIndex = 9;
            this.label5.Text = "◾ 품번";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOrderNum
            // 
            this.txtOrderNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOrderNum.Enabled = false;
            this.txtOrderNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtOrderNum.Location = new System.Drawing.Point(125, 50);
            this.txtOrderNum.Name = "txtOrderNum";
            this.txtOrderNum.Size = new System.Drawing.Size(265, 29);
            this.txtOrderNum.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(16, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(138, 32);
            this.label8.TabIndex = 16;
            this.label8.Text = "◾ 주문서";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(16, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(138, 32);
            this.label9.TabIndex = 15;
            this.label9.Text = "◾ label4";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomerCode.Enabled = false;
            this.txtCustomerCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtCustomerCode.Location = new System.Drawing.Point(537, 47);
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.Size = new System.Drawing.Size(265, 29);
            this.txtCustomerCode.TabIndex = 19;
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomerName.Enabled = false;
            this.txtCustomerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtCustomerName.Location = new System.Drawing.Point(537, 105);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(265, 29);
            this.txtCustomerName.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(428, 46);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(138, 32);
            this.label10.TabIndex = 16;
            this.label10.Text = "◾ 납품처";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(428, 44);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(138, 32);
            this.label11.TabIndex = 15;
            this.label11.Text = "◾ label4";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(847, 46);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(138, 32);
            this.label12.TabIndex = 18;
            this.label12.Text = "◾ 주문일자";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(428, 103);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(138, 32);
            this.label13.TabIndex = 17;
            this.label13.Text = "◾ 납품처명";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpOrderDate
            // 
            this.dtpOrderDate.Enabled = false;
            this.dtpOrderDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtpOrderDate.Location = new System.Drawing.Point(956, 46);
            this.dtpOrderDate.Name = "dtpOrderDate";
            this.dtpOrderDate.Size = new System.Drawing.Size(265, 29);
            this.dtpOrderDate.TabIndex = 21;
            // 
            // btnSelectStock
            // 
            this.btnSelectStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(34)))), ((int)(((byte)(41)))));
            this.btnSelectStock.FlatAppearance.BorderSize = 0;
            this.btnSelectStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSelectStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSelectStock.Location = new System.Drawing.Point(456, 50);
            this.btnSelectStock.Name = "btnSelectStock";
            this.btnSelectStock.Size = new System.Drawing.Size(104, 31);
            this.btnSelectStock.TabIndex = 19;
            this.btnSelectStock.Text = "제품 재고 조회";
            this.btnSelectStock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelectStock.UseVisualStyleBackColor = false;
            this.btnSelectStock.Click += new System.EventHandler(this.btnSelectStock_Click);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lstLot);
            this.panel4.Controls.Add(this.txtTotalQty);
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.label17);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Location = new System.Drawing.Point(1128, 346);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(336, 534);
            this.panel4.TabIndex = 30;
            // 
            // txtTotalQty
            // 
            this.txtTotalQty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalQty.Enabled = false;
            this.txtTotalQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtTotalQty.Location = new System.Drawing.Point(23, 277);
            this.txtTotalQty.Name = "txtTotalQty";
            this.txtTotalQty.Size = new System.Drawing.Size(289, 29);
            this.txtTotalQty.TabIndex = 25;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(19, 46);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(138, 32);
            this.label14.TabIndex = 22;
            this.label14.Text = "◾ LOT";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(19, 242);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(138, 32);
            this.label17.TabIndex = 23;
            this.label17.Text = "◾ 총 수량";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(56)))), ((int)(((byte)(67)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(334, 32);
            this.label15.TabIndex = 2;
            this.label15.Text = "출하 정보";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Btn_Ship
            // 
            this.Btn_Ship.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(34)))), ((int)(((byte)(41)))));
            this.Btn_Ship.FlatAppearance.BorderSize = 0;
            this.Btn_Ship.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Ship.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Btn_Ship.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Btn_Ship.Image = global::Cohesion_Project.Properties.Resources.Search;
            this.Btn_Ship.Location = new System.Drawing.Point(396, 50);
            this.Btn_Ship.Name = "Btn_Ship";
            this.Btn_Ship.Size = new System.Drawing.Size(55, 31);
            this.Btn_Ship.TabIndex = 18;
            this.Btn_Ship.Text = "조회";
            this.Btn_Ship.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Btn_Ship.UseVisualStyleBackColor = false;
            this.Btn_Ship.Click += new System.EventHandler(this.Btn_Ship_Click);
            // 
            // dgvStockProduct
            // 
            this.dgvStockProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStockProduct.Location = new System.Drawing.Point(0, 32);
            this.dgvStockProduct.Name = "dgvStockProduct";
            this.dgvStockProduct.RowTemplate.Height = 23;
            this.dgvStockProduct.Size = new System.Drawing.Size(1103, 500);
            this.dgvStockProduct.TabIndex = 3;
            // 
            // lstLot
            // 
            this.lstLot.FormattingEnabled = true;
            this.lstLot.ItemHeight = 16;
            this.lstLot.Location = new System.Drawing.Point(27, 82);
            this.lstLot.Name = "lstLot";
            this.lstLot.Size = new System.Drawing.Size(289, 132);
            this.lstLot.TabIndex = 26;
            // 
            // Frm_Ship
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(1478, 937);
            this.Controls.Add(this.panel4);
            this.Name = "Frm_Ship";
            this.Text = "제품 출고";
            this.Load += new System.EventHandler(this.Frm_Ship_Load);
            this.Controls.SetChildIndex(this.panel4, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.panel3, 0);
            this.Controls.SetChildIndex(this.Btn_Close, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockProduct)).EndInit();
            this.ResumeLayout(false);

      }

      #endregion

      protected System.Windows.Forms.TextBox txtOrderNum;
      protected System.Windows.Forms.Label label8;
      protected System.Windows.Forms.Label label9;
      protected System.Windows.Forms.TextBox txtProductCode;
      protected System.Windows.Forms.TextBox txtQty;
      protected System.Windows.Forms.TextBox txtProductName;
      protected System.Windows.Forms.Label label5;
      protected System.Windows.Forms.Label label7;
      protected System.Windows.Forms.Label label6;
      protected System.Windows.Forms.TextBox txtCustomerCode;
      protected System.Windows.Forms.TextBox txtCustomerName;
      protected System.Windows.Forms.Label label10;
      protected System.Windows.Forms.Label label11;
      protected System.Windows.Forms.Label label12;
      protected System.Windows.Forms.Label label13;
      private System.Windows.Forms.DateTimePicker dtpOrderDate;
      private System.Windows.Forms.Button Btn_Ship;
      private System.Windows.Forms.Button btnSelectStock;
      private System.Windows.Forms.Panel panel4;
      protected System.Windows.Forms.Label label15;
      protected System.Windows.Forms.TextBox txtTotalQty;
      protected System.Windows.Forms.Label label14;
      protected System.Windows.Forms.Label label17;
        private System.Windows.Forms.DataGridView dgvStockProduct;
        private System.Windows.Forms.ListBox lstLot;
    }
}
