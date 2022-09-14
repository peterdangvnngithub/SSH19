namespace TAKAKO_ERP_3LAYER
{
    partial class Form_Search
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Search));
            this.panel_Top = new System.Windows.Forms.Panel();
            this.panel_TopRight1 = new System.Windows.Forms.Panel();
            this.picBox_Min = new System.Windows.Forms.PictureBox();
            this.picBox_Max = new System.Windows.Forms.PictureBox();
            this.picBox_Close = new System.Windows.Forms.PictureBox();
            this.panel_Header = new System.Windows.Forms.Panel();
            this.txtFilter1 = new System.Windows.Forms.TextBox();
            this.lblFilter1 = new System.Windows.Forms.Label();
            this.txtKeySearch1 = new System.Windows.Forms.TextBox();
            this.lblSearch1 = new System.Windows.Forms.Label();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.GridView_Search = new System.Windows.Forms.DataGridView();
            this.panel_Top.SuspendLayout();
            this.panel_TopRight1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Close)).BeginInit();
            this.panel_Header.SuspendLayout();
            this.panelDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView_Search)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_Top
            // 
            this.panel_Top.Controls.Add(this.panel_TopRight1);
            this.panel_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Top.Location = new System.Drawing.Point(0, 0);
            this.panel_Top.Name = "panel_Top";
            this.panel_Top.Size = new System.Drawing.Size(614, 30);
            this.panel_Top.TabIndex = 2;
            this.panel_Top.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDoubleClick);
            this.panel_Top.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.panel_Top.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseMove);
            this.panel_Top.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // panel_TopRight1
            // 
            this.panel_TopRight1.Controls.Add(this.picBox_Min);
            this.panel_TopRight1.Controls.Add(this.picBox_Max);
            this.panel_TopRight1.Controls.Add(this.picBox_Close);
            this.panel_TopRight1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_TopRight1.Location = new System.Drawing.Point(508, 0);
            this.panel_TopRight1.Name = "panel_TopRight1";
            this.panel_TopRight1.Size = new System.Drawing.Size(106, 30);
            this.panel_TopRight1.TabIndex = 5;
            // 
            // picBox_Min
            // 
            this.picBox_Min.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.Minimize_window;
            this.picBox_Min.Location = new System.Drawing.Point(8, 3);
            this.picBox_Min.Name = "picBox_Min";
            this.picBox_Min.Size = new System.Drawing.Size(25, 25);
            this.picBox_Min.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Min.TabIndex = 5;
            this.picBox_Min.TabStop = false;
            this.picBox_Min.Click += new System.EventHandler(this.picBox_Min_Click);
            this.picBox_Min.MouseEnter += new System.EventHandler(this.picBox_Min_MouseEnter);
            this.picBox_Min.MouseLeave += new System.EventHandler(this.picBox_Min_MouseLeave);
            // 
            // picBox_Max
            // 
            this.picBox_Max.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.Maximize_window;
            this.picBox_Max.Location = new System.Drawing.Point(42, 3);
            this.picBox_Max.Name = "picBox_Max";
            this.picBox_Max.Size = new System.Drawing.Size(25, 25);
            this.picBox_Max.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Max.TabIndex = 4;
            this.picBox_Max.TabStop = false;
            this.picBox_Max.Click += new System.EventHandler(this.picBox_Max_Click);
            this.picBox_Max.MouseEnter += new System.EventHandler(this.picBox_Max_MouseEnter);
            this.picBox_Max.MouseLeave += new System.EventHandler(this.picBox_Max_MouseLeave);
            // 
            // picBox_Close
            // 
            this.picBox_Close.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.Close_window;
            this.picBox_Close.Location = new System.Drawing.Point(76, 3);
            this.picBox_Close.Name = "picBox_Close";
            this.picBox_Close.Size = new System.Drawing.Size(25, 25);
            this.picBox_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Close.TabIndex = 3;
            this.picBox_Close.TabStop = false;
            this.picBox_Close.Click += new System.EventHandler(this.picBox_Close_Click);
            this.picBox_Close.MouseEnter += new System.EventHandler(this.picBox_Close_MouseEnter);
            this.picBox_Close.MouseLeave += new System.EventHandler(this.picBox_Close_MouseLeave);
            // 
            // panel_Header
            // 
            this.panel_Header.Controls.Add(this.txtFilter1);
            this.panel_Header.Controls.Add(this.lblFilter1);
            this.panel_Header.Controls.Add(this.txtKeySearch1);
            this.panel_Header.Controls.Add(this.lblSearch1);
            this.panel_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Header.Location = new System.Drawing.Point(0, 30);
            this.panel_Header.Name = "panel_Header";
            this.panel_Header.Size = new System.Drawing.Size(614, 54);
            this.panel_Header.TabIndex = 3;
            // 
            // txtFilter1
            // 
            this.txtFilter1.Font = new System.Drawing.Font("Arial", 8.25F);
            this.txtFilter1.Location = new System.Drawing.Point(106, 26);
            this.txtFilter1.Name = "txtFilter1";
            this.txtFilter1.Size = new System.Drawing.Size(273, 20);
            this.txtFilter1.TabIndex = 3;
            this.txtFilter1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilter1_KeyDown);
            this.txtFilter1.Validated += new System.EventHandler(this.txtFilter1_Validated);
            // 
            // lblFilter1
            // 
            this.lblFilter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFilter1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter1.Location = new System.Drawing.Point(5, 26);
            this.lblFilter1.Name = "lblFilter1";
            this.lblFilter1.Size = new System.Drawing.Size(100, 20);
            this.lblFilter1.TabIndex = 2;
            this.lblFilter1.Text = "Filter Value";
            this.lblFilter1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtKeySearch1
            // 
            this.txtKeySearch1.Font = new System.Drawing.Font("Arial", 8.25F);
            this.txtKeySearch1.Location = new System.Drawing.Point(106, 4);
            this.txtKeySearch1.Name = "txtKeySearch1";
            this.txtKeySearch1.Size = new System.Drawing.Size(273, 20);
            this.txtKeySearch1.TabIndex = 1;
            this.txtKeySearch1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKeySearch1_KeyDown);
            this.txtKeySearch1.Validated += new System.EventHandler(this.txtKeySearch1_Validated);
            // 
            // lblSearch1
            // 
            this.lblSearch1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSearch1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch1.Location = new System.Drawing.Point(5, 4);
            this.lblSearch1.Name = "lblSearch1";
            this.lblSearch1.Size = new System.Drawing.Size(100, 20);
            this.lblSearch1.TabIndex = 0;
            this.lblSearch1.Text = "Search Value";
            this.lblSearch1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelDetail
            // 
            this.panelDetail.Controls.Add(this.GridView_Search);
            this.panelDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetail.Location = new System.Drawing.Point(0, 84);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Padding = new System.Windows.Forms.Padding(5, 2, 5, 5);
            this.panelDetail.Size = new System.Drawing.Size(614, 371);
            this.panelDetail.TabIndex = 4;
            // 
            // GridView_Search
            // 
            this.GridView_Search.AllowUserToAddRows = false;
            this.GridView_Search.AllowUserToDeleteRows = false;
            this.GridView_Search.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridView_Search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridView_Search.Location = new System.Drawing.Point(5, 2);
            this.GridView_Search.Name = "GridView_Search";
            this.GridView_Search.ReadOnly = true;
            this.GridView_Search.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridView_Search.Size = new System.Drawing.Size(604, 364);
            this.GridView_Search.TabIndex = 0;
            this.GridView_Search.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.GridView_Search_RowPostPaint);
            this.GridView_Search.DoubleClick += new System.EventHandler(this.GridView_Search_DoubleClick);
            this.GridView_Search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridView_Search_KeyDown);
            // 
            // Form_Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 455);
            this.Controls.Add(this.panelDetail);
            this.Controls.Add(this.panel_Header);
            this.Controls.Add(this.panel_Top);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Search";
            this.Text = "Search Data";
            this.Load += new System.EventHandler(this.Form_Search_Load);
            this.panel_Top.ResumeLayout(false);
            this.panel_TopRight1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Close)).EndInit();
            this.panel_Header.ResumeLayout(false);
            this.panel_Header.PerformLayout();
            this.panelDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridView_Search)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_Top;
        private System.Windows.Forms.Panel panel_TopRight1;
        private System.Windows.Forms.PictureBox picBox_Min;
        private System.Windows.Forms.PictureBox picBox_Max;
        private System.Windows.Forms.PictureBox picBox_Close;
        private System.Windows.Forms.Panel panel_Header;
        private System.Windows.Forms.TextBox txtKeySearch1;
        private System.Windows.Forms.Label lblSearch1;
        private System.Windows.Forms.Panel panelDetail;
        private System.Windows.Forms.DataGridView GridView_Search;
        private System.Windows.Forms.TextBox txtFilter1;
        private System.Windows.Forms.Label lblFilter1;
    }
}