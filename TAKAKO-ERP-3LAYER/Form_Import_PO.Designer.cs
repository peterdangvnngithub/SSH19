namespace TAKAKO_ERP_3LAYER
{
    partial class Form_Import_PO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Import_PO));
            this.panelTop = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.picBox_Close = new System.Windows.Forms.PictureBox();
            this.picBox_BackToMain = new System.Windows.Forms.PictureBox();
            this.picBox_Min = new System.Windows.Forms.PictureBox();
            this.picBox_Max = new System.Windows.Forms.PictureBox();
            this.panel_Header = new System.Windows.Forms.Panel();
            this.btnSearch_ReciveNo = new System.Windows.Forms.Button();
            this.lbl_ReceiveNo = new System.Windows.Forms.Label();
            this.txtReceiveNo = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btn_ChoseFile = new System.Windows.Forms.Button();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.GridView_ImportPO = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_BackToMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Max)).BeginInit();
            this.panel_Header.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView_ImportPO)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.panel2);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(984, 30);
            this.panelTop.TabIndex = 5;
            this.panelTop.DoubleClick += new System.EventHandler(this.panel_Top_DoubleClick);
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.panelTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseMove);
            this.panelTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.picBox_Close);
            this.panel2.Controls.Add(this.picBox_BackToMain);
            this.panel2.Controls.Add(this.picBox_Min);
            this.panel2.Controls.Add(this.picBox_Max);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(750, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(234, 30);
            this.panel2.TabIndex = 2;
            // 
            // picBox_Close
            // 
            this.picBox_Close.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.Close_window;
            this.picBox_Close.Location = new System.Drawing.Point(202, 2);
            this.picBox_Close.Name = "picBox_Close";
            this.picBox_Close.Size = new System.Drawing.Size(25, 25);
            this.picBox_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Close.TabIndex = 10;
            this.picBox_Close.TabStop = false;
            this.picBox_Close.Click += new System.EventHandler(this.picBox_Close_Click);
            // 
            // picBox_BackToMain
            // 
            this.picBox_BackToMain.Location = new System.Drawing.Point(0, 0);
            this.picBox_BackToMain.Name = "picBox_BackToMain";
            this.picBox_BackToMain.Size = new System.Drawing.Size(100, 30);
            this.picBox_BackToMain.TabIndex = 11;
            this.picBox_BackToMain.TabStop = false;
            // 
            // picBox_Min
            // 
            this.picBox_Min.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.Minimize_window;
            this.picBox_Min.Location = new System.Drawing.Point(136, 2);
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
            this.picBox_Max.Location = new System.Drawing.Point(169, 2);
            this.picBox_Max.Name = "picBox_Max";
            this.picBox_Max.Size = new System.Drawing.Size(25, 25);
            this.picBox_Max.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Max.TabIndex = 7;
            this.picBox_Max.TabStop = false;
            this.picBox_Max.Click += new System.EventHandler(this.picBox_Max_Click);
            this.picBox_Max.MouseEnter += new System.EventHandler(this.picBox_Max_MouseEnter);
            this.picBox_Max.MouseLeave += new System.EventHandler(this.picBox_Max_MouseLeave);
            // 
            // panel_Header
            // 
            this.panel_Header.Controls.Add(this.btnSearch_ReciveNo);
            this.panel_Header.Controls.Add(this.lbl_ReceiveNo);
            this.panel_Header.Controls.Add(this.txtReceiveNo);
            this.panel_Header.Controls.Add(this.btnClear);
            this.panel_Header.Controls.Add(this.btnSave);
            this.panel_Header.Controls.Add(this.btn_ChoseFile);
            this.panel_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Header.Location = new System.Drawing.Point(0, 30);
            this.panel_Header.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel_Header.Name = "panel_Header";
            this.panel_Header.Size = new System.Drawing.Size(984, 40);
            this.panel_Header.TabIndex = 6;
            // 
            // btnSearch_ReciveNo
            // 
            this.btnSearch_ReciveNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch_ReciveNo.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.Icon_search;
            this.btnSearch_ReciveNo.Location = new System.Drawing.Point(625, 8);
            this.btnSearch_ReciveNo.Name = "btnSearch_ReciveNo";
            this.btnSearch_ReciveNo.Size = new System.Drawing.Size(23, 23);
            this.btnSearch_ReciveNo.TabIndex = 6;
            this.btnSearch_ReciveNo.TabStop = false;
            this.btnSearch_ReciveNo.UseVisualStyleBackColor = true;
            this.btnSearch_ReciveNo.Click += new System.EventHandler(this.btnSearch_ReciveNo_Click);
            // 
            // lbl_ReceiveNo
            // 
            this.lbl_ReceiveNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_ReceiveNo.Location = new System.Drawing.Point(321, 9);
            this.lbl_ReceiveNo.Name = "lbl_ReceiveNo";
            this.lbl_ReceiveNo.Size = new System.Drawing.Size(83, 21);
            this.lbl_ReceiveNo.TabIndex = 5;
            this.lbl_ReceiveNo.Text = "Receive No";
            this.lbl_ReceiveNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtReceiveNo
            // 
            this.txtReceiveNo.Location = new System.Drawing.Point(405, 9);
            this.txtReceiveNo.Name = "txtReceiveNo";
            this.txtReceiveNo.Size = new System.Drawing.Size(220, 21);
            this.txtReceiveNo.TabIndex = 4;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(199, 6);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(87, 28);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(105, 6);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 28);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btn_ChoseFile
            // 
            this.btn_ChoseFile.Location = new System.Drawing.Point(10, 6);
            this.btn_ChoseFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_ChoseFile.Name = "btn_ChoseFile";
            this.btn_ChoseFile.Size = new System.Drawing.Size(87, 28);
            this.btn_ChoseFile.TabIndex = 0;
            this.btn_ChoseFile.Text = "Choose &File";
            this.btn_ChoseFile.UseVisualStyleBackColor = true;
            this.btn_ChoseFile.Click += new System.EventHandler(this.btn_ChoseFile_Click);
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.GridView_ImportPO);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(0, 70);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(984, 391);
            this.panelGrid.TabIndex = 7;
            // 
            // GridView_ImportPO
            // 
            this.GridView_ImportPO.AllowUserToAddRows = false;
            this.GridView_ImportPO.AllowUserToDeleteRows = false;
            this.GridView_ImportPO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridView_ImportPO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridView_ImportPO.Location = new System.Drawing.Point(0, 0);
            this.GridView_ImportPO.Name = "GridView_ImportPO";
            this.GridView_ImportPO.ReadOnly = true;
            this.GridView_ImportPO.Size = new System.Drawing.Size(984, 391);
            this.GridView_ImportPO.TabIndex = 0;
            this.GridView_ImportPO.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.GridView_ImportPO_RowPostPaint);
            // 
            // Form_Import_PO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.panel_Header);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form_Import_PO";
            this.Text = "Import Data";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Import_PO_FormClosing);
            this.Load += new System.EventHandler(this.Form_Import_PO_Load);
            this.panelTop.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_BackToMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Max)).EndInit();
            this.panel_Header.ResumeLayout(false);
            this.panel_Header.PerformLayout();
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridView_ImportPO)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox picBox_BackToMain;
        private System.Windows.Forms.PictureBox picBox_Min;
        private System.Windows.Forms.PictureBox picBox_Max;
        private System.Windows.Forms.Panel panel_Header;
        private System.Windows.Forms.Label lbl_ReceiveNo;
        private System.Windows.Forms.TextBox txtReceiveNo;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btn_ChoseFile;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.DataGridView GridView_ImportPO;
        private System.Windows.Forms.PictureBox picBox_Close;
        private System.Windows.Forms.Button btnSearch_ReciveNo;
    }
}