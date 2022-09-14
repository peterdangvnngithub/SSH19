namespace TAKAKO_ERP_3LAYER
{
    partial class Form_Export_Data
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Export_Data));
            this.panelTop = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.picBox_BackToMain = new System.Windows.Forms.PictureBox();
            this.picBox_Min = new System.Windows.Forms.PictureBox();
            this.picBox_Max = new System.Windows.Forms.PictureBox();
            this.picBox_Close = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch_Inv = new System.Windows.Forms.Button();
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dtpRevenue = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.panel_Header = new System.Windows.Forms.Panel();
            this.dtpDueDateTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDueDateFrom = new System.Windows.Forms.DateTimePicker();
            this.lbl_ReceiveNo = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbTemplate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExport_Test = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_BackToMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Close)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel_Header.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.panel2);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(921, 30);
            this.panelTop.TabIndex = 5;
            this.panelTop.DoubleClick += new System.EventHandler(this.panel_Top_DoubleClick);
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.panelTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseMove);
            this.panelTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.picBox_BackToMain);
            this.panel2.Controls.Add(this.picBox_Min);
            this.panel2.Controls.Add(this.picBox_Max);
            this.panel2.Controls.Add(this.picBox_Close);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(782, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(139, 30);
            this.panel2.TabIndex = 2;
            // 
            // picBox_BackToMain
            // 
            this.picBox_BackToMain.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.Arrow_pointing;
            this.picBox_BackToMain.Location = new System.Drawing.Point(8, 2);
            this.picBox_BackToMain.Name = "picBox_BackToMain";
            this.picBox_BackToMain.Size = new System.Drawing.Size(25, 25);
            this.picBox_BackToMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_BackToMain.TabIndex = 9;
            this.picBox_BackToMain.TabStop = false;
            this.picBox_BackToMain.Click += new System.EventHandler(this.picBox_BackToMain_Click);
            this.picBox_BackToMain.MouseEnter += new System.EventHandler(this.picBox_BackToMain_MouseEnter);
            this.picBox_BackToMain.MouseLeave += new System.EventHandler(this.picBox_BackToMain_MouseLeave);
            this.picBox_BackToMain.MouseHover += new System.EventHandler(this.picBox_BackToMain_MouseHover);
            // 
            // picBox_Min
            // 
            this.picBox_Min.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.Minimize_window;
            this.picBox_Min.Location = new System.Drawing.Point(42, 2);
            this.picBox_Min.Name = "picBox_Min";
            this.picBox_Min.Size = new System.Drawing.Size(25, 25);
            this.picBox_Min.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Min.TabIndex = 8;
            this.picBox_Min.TabStop = false;
            this.picBox_Min.Click += new System.EventHandler(this.picBox_Min_Click);
            this.picBox_Min.MouseEnter += new System.EventHandler(this.picBox_Min_MouseEnter);
            this.picBox_Min.MouseLeave += new System.EventHandler(this.picBox_Min_MouseLeave);
            // 
            // picBox_Max
            // 
            this.picBox_Max.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.Maximize_window;
            this.picBox_Max.Location = new System.Drawing.Point(76, 2);
            this.picBox_Max.Name = "picBox_Max";
            this.picBox_Max.Size = new System.Drawing.Size(25, 25);
            this.picBox_Max.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Max.TabIndex = 7;
            this.picBox_Max.TabStop = false;
            this.picBox_Max.Click += new System.EventHandler(this.picBox_Max_Click);
            this.picBox_Max.MouseEnter += new System.EventHandler(this.picBox_Max_MouseEnter);
            this.picBox_Max.MouseLeave += new System.EventHandler(this.picBox_Max_MouseLeave);
            // 
            // picBox_Close
            // 
            this.picBox_Close.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.Close_window;
            this.picBox_Close.Location = new System.Drawing.Point(110, 2);
            this.picBox_Close.Name = "picBox_Close";
            this.picBox_Close.Size = new System.Drawing.Size(25, 25);
            this.picBox_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Close.TabIndex = 6;
            this.picBox_Close.TabStop = false;
            this.picBox_Close.Click += new System.EventHandler(this.picBox_Close_Click);
            this.picBox_Close.MouseEnter += new System.EventHandler(this.picBox_Close_MouseEnter);
            this.picBox_Close.MouseLeave += new System.EventHandler(this.picBox_Close_MouseLeave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.panel_Header);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(921, 151);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CONDITION SEARCH";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch_Inv);
            this.panel1.Controls.Add(this.txtInvoiceNo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 97);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(915, 40);
            this.panel1.TabIndex = 8;
            // 
            // btnSearch_Inv
            // 
            this.btnSearch_Inv.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.Icon_search;
            this.btnSearch_Inv.Location = new System.Drawing.Point(365, 8);
            this.btnSearch_Inv.Name = "btnSearch_Inv";
            this.btnSearch_Inv.Size = new System.Drawing.Size(23, 23);
            this.btnSearch_Inv.TabIndex = 7;
            this.btnSearch_Inv.UseVisualStyleBackColor = true;
            this.btnSearch_Inv.Click += new System.EventHandler(this.btnSearch_Inv_Click);
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.txtInvoiceNo.Location = new System.Drawing.Point(113, 9);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(252, 21);
            this.txtInvoiceNo.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.label3.Location = new System.Drawing.Point(9, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "Invoice No";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dtpRevenue);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 57);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(915, 40);
            this.panel3.TabIndex = 9;
            // 
            // dtpRevenue
            // 
            this.dtpRevenue.CustomFormat = "MM/yyyy";
            this.dtpRevenue.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.dtpRevenue.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRevenue.Location = new System.Drawing.Point(113, 9);
            this.dtpRevenue.Name = "dtpRevenue";
            this.dtpRevenue.Size = new System.Drawing.Size(114, 21);
            this.dtpRevenue.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.label5.Location = new System.Drawing.Point(9, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 21);
            this.label5.TabIndex = 5;
            this.label5.Text = "Revenue";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel_Header
            // 
            this.panel_Header.Controls.Add(this.dtpDueDateTo);
            this.panel_Header.Controls.Add(this.label1);
            this.panel_Header.Controls.Add(this.dtpDueDateFrom);
            this.panel_Header.Controls.Add(this.lbl_ReceiveNo);
            this.panel_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Header.Location = new System.Drawing.Point(3, 17);
            this.panel_Header.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel_Header.Name = "panel_Header";
            this.panel_Header.Size = new System.Drawing.Size(915, 40);
            this.panel_Header.TabIndex = 7;
            // 
            // dtpDueDateTo
            // 
            this.dtpDueDateTo.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.dtpDueDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDueDateTo.Location = new System.Drawing.Point(251, 9);
            this.dtpDueDateTo.Name = "dtpDueDateTo";
            this.dtpDueDateTo.Size = new System.Drawing.Size(114, 21);
            this.dtpDueDateTo.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(229, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "-";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpDueDateFrom
            // 
            this.dtpDueDateFrom.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.dtpDueDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDueDateFrom.Location = new System.Drawing.Point(113, 9);
            this.dtpDueDateFrom.Name = "dtpDueDateFrom";
            this.dtpDueDateFrom.Size = new System.Drawing.Size(114, 21);
            this.dtpDueDateFrom.TabIndex = 6;
            // 
            // lbl_ReceiveNo
            // 
            this.lbl_ReceiveNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_ReceiveNo.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.lbl_ReceiveNo.Location = new System.Drawing.Point(9, 9);
            this.lbl_ReceiveNo.Name = "lbl_ReceiveNo";
            this.lbl_ReceiveNo.Size = new System.Drawing.Size(103, 21);
            this.lbl_ReceiveNo.TabIndex = 5;
            this.lbl_ReceiveNo.Text = "ETD";
            this.lbl_ReceiveNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbTemplate);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("Century Gothic", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 231);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(921, 54);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "TEMPLETE OUTPUT";
            // 
            // cbTemplate
            // 
            this.cbTemplate.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.cbTemplate.FormattingEnabled = true;
            this.cbTemplate.Location = new System.Drawing.Point(116, 22);
            this.cbTemplate.Name = "cbTemplate";
            this.cbTemplate.Size = new System.Drawing.Size(252, 24);
            this.cbTemplate.TabIndex = 7;
            this.cbTemplate.SelectedValueChanged += new System.EventHandler(this.cbTemplate_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 24);
            this.label2.TabIndex = 6;
            this.label2.Text = "Template";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(92, 290);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 25);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExport_Test
            // 
            this.btnExport_Test.Location = new System.Drawing.Point(11, 290);
            this.btnExport_Test.Name = "btnExport_Test";
            this.btnExport_Test.Size = new System.Drawing.Size(75, 25);
            this.btnExport_Test.TabIndex = 11;
            this.btnExport_Test.Text = "&Export";
            this.btnExport_Test.UseVisualStyleBackColor = true;
            this.btnExport_Test.Click += new System.EventHandler(this.btnExport_Test_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.label4);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 30);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(921, 50);
            this.panelHeader.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(70)))), ((int)(((byte)(1)))));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(921, 50);
            this.label4.TabIndex = 0;
            this.label4.Text = "EXPORT DATA";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form_Export_Data
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 332);
            this.Controls.Add(this.btnExport_Test);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form_Export_Data";
            this.Text = " ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_ExportData_FormClosing);
            this.Load += new System.EventHandler(this.Form_Export_Data_Load);
            this.panelTop.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_BackToMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Close)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel_Header.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox picBox_BackToMain;
        private System.Windows.Forms.PictureBox picBox_Min;
        private System.Windows.Forms.PictureBox picBox_Max;
        private System.Windows.Forms.PictureBox picBox_Close;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel_Header;
        private System.Windows.Forms.Label lbl_ReceiveNo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtpDueDateFrom;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtInvoiceNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDueDateTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbTemplate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExport_Test;
        private System.Windows.Forms.Button btnSearch_Inv;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpRevenue;
    }
}