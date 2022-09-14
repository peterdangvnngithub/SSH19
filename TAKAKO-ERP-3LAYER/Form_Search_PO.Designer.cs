namespace TAKAKO_ERP_3LAYER
{
    partial class Form_Search_PO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Search_PO));
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSelect = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.picBox_Min = new System.Windows.Forms.PictureBox();
            this.picBox_Max = new System.Windows.Forms.PictureBox();
            this.picBox_Close = new System.Windows.Forms.PictureBox();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cboxTypeSearch = new System.Windows.Forms.ComboBox();
            this.cboxCompany = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCustomerPO = new System.Windows.Forms.TextBox();
            this.lblCustomerPO = new System.Windows.Forms.Label();
            this.radCurrencyUSD = new System.Windows.Forms.RadioButton();
            this.radCurrencyJPY = new System.Windows.Forms.RadioButton();
            this.txtKeySearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.GridView_Search = new System.Windows.Forms.DataGridView();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.panelBottom.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Close)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.panelDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView_Search)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.panel1);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 467);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(840, 32);
            this.panelBottom.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSelectAll);
            this.panel1.Controls.Add(this.btnSelect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(603, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(237, 32);
            this.panel1.TabIndex = 0;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(157, 4);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 25);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.panel3);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(840, 30);
            this.panelTop.TabIndex = 5;
            this.panelTop.DoubleClick += new System.EventHandler(this.panelTop_DoubleClick);
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.panelTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseMove);
            this.panelTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.picBox_Min);
            this.panel3.Controls.Add(this.picBox_Max);
            this.panel3.Controls.Add(this.picBox_Close);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(732, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(108, 30);
            this.panel3.TabIndex = 2;
            // 
            // picBox_Min
            // 
            this.picBox_Min.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.Minimize_window;
            this.picBox_Min.Location = new System.Drawing.Point(7, 2);
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
            this.picBox_Max.Location = new System.Drawing.Point(41, 2);
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
            this.picBox_Close.Location = new System.Drawing.Point(75, 2);
            this.picBox_Close.Name = "picBox_Close";
            this.picBox_Close.Size = new System.Drawing.Size(25, 25);
            this.picBox_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Close.TabIndex = 6;
            this.picBox_Close.TabStop = false;
            this.picBox_Close.Click += new System.EventHandler(this.picBox_Close_Click);
            this.picBox_Close.MouseEnter += new System.EventHandler(this.picBox_Close_MouseEnter);
            this.picBox_Close.MouseLeave += new System.EventHandler(this.picBox_Close_MouseLeave);
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.label2);
            this.panelHeader.Controls.Add(this.cboxTypeSearch);
            this.panelHeader.Controls.Add(this.cboxCompany);
            this.panelHeader.Controls.Add(this.label1);
            this.panelHeader.Controls.Add(this.txtCustomerPO);
            this.panelHeader.Controls.Add(this.lblCustomerPO);
            this.panelHeader.Controls.Add(this.radCurrencyUSD);
            this.panelHeader.Controls.Add(this.radCurrencyJPY);
            this.panelHeader.Controls.Add(this.txtKeySearch);
            this.panelHeader.Controls.Add(this.lblSearch);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 30);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panelHeader.Size = new System.Drawing.Size(840, 88);
            this.panelHeader.TabIndex = 0;
            this.panelHeader.TabStop = true;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(385, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 22);
            this.label2.TabIndex = 6;
            this.label2.Text = "Type Search";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboxTypeSearch
            // 
            this.cboxTypeSearch.FormattingEnabled = true;
            this.cboxTypeSearch.Location = new System.Drawing.Point(486, 26);
            this.cboxTypeSearch.Name = "cboxTypeSearch";
            this.cboxTypeSearch.Size = new System.Drawing.Size(121, 22);
            this.cboxTypeSearch.TabIndex = 5;
            this.cboxTypeSearch.TabStop = false;
            this.cboxTypeSearch.TextChanged += new System.EventHandler(this.CboxTypeSearch_TextChanged);
            // 
            // cboxCompany
            // 
            this.cboxCompany.FormattingEnabled = true;
            this.cboxCompany.ItemHeight = 14;
            this.cboxCompany.Location = new System.Drawing.Point(106, 2);
            this.cboxCompany.Name = "cboxCompany";
            this.cboxCompany.Size = new System.Drawing.Size(128, 22);
            this.cboxCompany.TabIndex = 0;
            this.cboxCompany.TabStop = false;
            this.cboxCompany.TextChanged += new System.EventHandler(this.cbxCompany_TextChanged);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 22);
            this.label1.TabIndex = 5;
            this.label1.Text = "Company";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCustomerPO
            // 
            this.txtCustomerPO.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerPO.Location = new System.Drawing.Point(106, 49);
            this.txtCustomerPO.Multiline = true;
            this.txtCustomerPO.Name = "txtCustomerPO";
            this.txtCustomerPO.Size = new System.Drawing.Size(273, 36);
            this.txtCustomerPO.TabIndex = 4;
            this.txtCustomerPO.Validated += new System.EventHandler(this.txtCustomerPO_Validated);
            // 
            // lblCustomerPO
            // 
            this.lblCustomerPO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCustomerPO.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerPO.Location = new System.Drawing.Point(5, 49);
            this.lblCustomerPO.Name = "lblCustomerPO";
            this.lblCustomerPO.Size = new System.Drawing.Size(100, 36);
            this.lblCustomerPO.TabIndex = 4;
            this.lblCustomerPO.Text = "Customer PO";
            this.lblCustomerPO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radCurrencyUSD
            // 
            this.radCurrencyUSD.AutoSize = true;
            this.radCurrencyUSD.Location = new System.Drawing.Point(338, 5);
            this.radCurrencyUSD.Name = "radCurrencyUSD";
            this.radCurrencyUSD.Size = new System.Drawing.Size(46, 18);
            this.radCurrencyUSD.TabIndex = 2;
            this.radCurrencyUSD.Text = "USD";
            this.radCurrencyUSD.UseVisualStyleBackColor = true;
            this.radCurrencyUSD.CheckedChanged += new System.EventHandler(this.radCurrencyUSD_CheckedChanged);
            // 
            // radCurrencyJPY
            // 
            this.radCurrencyJPY.AutoSize = true;
            this.radCurrencyJPY.Checked = true;
            this.radCurrencyJPY.Location = new System.Drawing.Point(282, 5);
            this.radCurrencyJPY.Name = "radCurrencyJPY";
            this.radCurrencyJPY.Size = new System.Drawing.Size(44, 18);
            this.radCurrencyJPY.TabIndex = 1;
            this.radCurrencyJPY.TabStop = true;
            this.radCurrencyJPY.Text = "JPY";
            this.radCurrencyJPY.UseVisualStyleBackColor = true;
            this.radCurrencyJPY.CheckedChanged += new System.EventHandler(this.radCurrencyJPY_CheckedChanged);
            // 
            // txtKeySearch
            // 
            this.txtKeySearch.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKeySearch.Location = new System.Drawing.Point(106, 26);
            this.txtKeySearch.Name = "txtKeySearch";
            this.txtKeySearch.Size = new System.Drawing.Size(273, 21);
            this.txtKeySearch.TabIndex = 3;
            this.txtKeySearch.Validated += new System.EventHandler(this.txtKeySearch_Validated);
            // 
            // lblSearch
            // 
            this.lblSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSearch.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(5, 26);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(100, 21);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Item Code";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelDetail
            // 
            this.panelDetail.Controls.Add(this.GridView_Search);
            this.panelDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetail.Location = new System.Drawing.Point(0, 118);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Padding = new System.Windows.Forms.Padding(5, 2, 5, 5);
            this.panelDetail.Size = new System.Drawing.Size(840, 349);
            this.panelDetail.TabIndex = 1;
            // 
            // GridView_Search
            // 
            this.GridView_Search.AllowUserToAddRows = false;
            this.GridView_Search.AllowUserToDeleteRows = false;
            this.GridView_Search.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridView_Search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridView_Search.Location = new System.Drawing.Point(5, 2);
            this.GridView_Search.Name = "GridView_Search";
            this.GridView_Search.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridView_Search.Size = new System.Drawing.Size(830, 342);
            this.GridView_Search.TabIndex = 0;
            this.GridView_Search.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.GridView_Search_RowPostPaint);
            this.GridView_Search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridView_Search_KeyDown);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(45, 4);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(111, 25);
            this.btnSelectAll.TabIndex = 1;
            this.btnSelectAll.Text = "Select/Deselect All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.BtnSelectAll_Click);
            // 
            // Form_Search_PO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 499);
            this.Controls.Add(this.panelDetail);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelBottom);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Search_PO";
            this.Text = "Search Data PO";
            this.Load += new System.EventHandler(this.Form_Search_Load);
            this.panelBottom.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Close)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridView_Search)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox picBox_Min;
        private System.Windows.Forms.PictureBox picBox_Max;
        private System.Windows.Forms.PictureBox picBox_Close;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.TextBox txtKeySearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Panel panelDetail;
        private System.Windows.Forms.DataGridView GridView_Search;
        private System.Windows.Forms.RadioButton radCurrencyUSD;
        private System.Windows.Forms.RadioButton radCurrencyJPY;
        private System.Windows.Forms.TextBox txtCustomerPO;
        private System.Windows.Forms.Label lblCustomerPO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboxCompany;
        private System.Windows.Forms.ComboBox cboxTypeSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelectAll;
    }
}