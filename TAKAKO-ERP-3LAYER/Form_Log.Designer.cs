namespace TAKAKO_ERP_3LAYER
{
    partial class Form_Log
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Log));
            this.panel_Top = new System.Windows.Forms.Panel();
            this.panel29 = new System.Windows.Forms.Panel();
            this.picBox_BackToMain = new System.Windows.Forms.PictureBox();
            this.picBox_Min = new System.Windows.Forms.PictureBox();
            this.picBox_Max = new System.Windows.Forms.PictureBox();
            this.picBox_Close = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSearchLog = new System.Windows.Forms.Button();
            this.cbxTypeSearch = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpCreateDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpCreateDateFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.panel_Detail = new System.Windows.Forms.Panel();
            this.GridViewLog = new System.Windows.Forms.DataGridView();
            this.panel_Top.SuspendLayout();
            this.panel29.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_BackToMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Close)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel_Detail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewLog)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_Top
            // 
            this.panel_Top.Controls.Add(this.panel29);
            this.panel_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Top.Location = new System.Drawing.Point(0, 0);
            this.panel_Top.Name = "panel_Top";
            this.panel_Top.Size = new System.Drawing.Size(800, 30);
            this.panel_Top.TabIndex = 1;
            this.panel_Top.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDoubleClick);
            this.panel_Top.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.panel_Top.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseMove);
            this.panel_Top.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // panel29
            // 
            this.panel29.Controls.Add(this.picBox_BackToMain);
            this.panel29.Controls.Add(this.picBox_Min);
            this.panel29.Controls.Add(this.picBox_Max);
            this.panel29.Controls.Add(this.picBox_Close);
            this.panel29.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel29.Location = new System.Drawing.Point(658, 0);
            this.panel29.Name = "panel29";
            this.panel29.Size = new System.Drawing.Size(142, 30);
            this.panel29.TabIndex = 3;
            // 
            // picBox_BackToMain
            // 
            this.picBox_BackToMain.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.Arrow_pointing;
            this.picBox_BackToMain.Location = new System.Drawing.Point(8, 3);
            this.picBox_BackToMain.Name = "picBox_BackToMain";
            this.picBox_BackToMain.Size = new System.Drawing.Size(25, 25);
            this.picBox_BackToMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_BackToMain.TabIndex = 9;
            this.picBox_BackToMain.TabStop = false;
            this.picBox_BackToMain.Click += new System.EventHandler(this.picBox_BackToMain_Click);
            this.picBox_BackToMain.MouseEnter += new System.EventHandler(this.picBox_BackToMain_MouseEnter);
            this.picBox_BackToMain.MouseLeave += new System.EventHandler(this.picBox_BackToMain_MouseLeave);
            // 
            // picBox_Min
            // 
            this.picBox_Min.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.Minimize_window;
            this.picBox_Min.Location = new System.Drawing.Point(42, 3);
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
            this.picBox_Max.Location = new System.Drawing.Point(76, 3);
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
            this.picBox_Close.Location = new System.Drawing.Point(110, 3);
            this.picBox_Close.Name = "picBox_Close";
            this.picBox_Close.Size = new System.Drawing.Size(25, 25);
            this.picBox_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Close.TabIndex = 6;
            this.picBox_Close.TabStop = false;
            this.picBox_Close.Click += new System.EventHandler(this.picBox_Close_Click);
            this.picBox_Close.MouseEnter += new System.EventHandler(this.picBox_Close_MouseEnter);
            this.picBox_Close.MouseLeave += new System.EventHandler(this.picBox_Close_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 40);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(70)))), ((int)(((byte)(1)))));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(800, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "DATA LOG";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSearchLog);
            this.panel2.Controls.Add(this.cbxTypeSearch);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.dtpCreateDateTo);
            this.panel2.Controls.Add(this.dtpCreateDateFrom);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 70);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 60);
            this.panel2.TabIndex = 3;
            // 
            // btnSearchLog
            // 
            this.btnSearchLog.Location = new System.Drawing.Point(380, 33);
            this.btnSearchLog.Name = "btnSearchLog";
            this.btnSearchLog.Size = new System.Drawing.Size(75, 23);
            this.btnSearchLog.TabIndex = 3;
            this.btnSearchLog.Text = "&Search";
            this.btnSearchLog.UseVisualStyleBackColor = true;
            this.btnSearchLog.Click += new System.EventHandler(this.btnSearchLog_Click);
            // 
            // cbxTypeSearch
            // 
            this.cbxTypeSearch.FormattingEnabled = true;
            this.cbxTypeSearch.Location = new System.Drawing.Point(106, 34);
            this.cbxTypeSearch.Name = "cbxTypeSearch";
            this.cbxTypeSearch.Size = new System.Drawing.Size(120, 21);
            this.cbxTypeSearch.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "Type Log";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(228, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "-";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpCreateDateTo
            // 
            this.dtpCreateDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCreateDateTo.Location = new System.Drawing.Point(255, 9);
            this.dtpCreateDateTo.Name = "dtpCreateDateTo";
            this.dtpCreateDateTo.Size = new System.Drawing.Size(120, 20);
            this.dtpCreateDateTo.TabIndex = 1;
            // 
            // dtpCreateDateFrom
            // 
            this.dtpCreateDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCreateDateFrom.Location = new System.Drawing.Point(106, 9);
            this.dtpCreateDateFrom.Name = "dtpCreateDateFrom";
            this.dtpCreateDateFrom.Size = new System.Drawing.Size(120, 20);
            this.dtpCreateDateFrom.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Date Create";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel_Detail
            // 
            this.panel_Detail.Controls.Add(this.GridViewLog);
            this.panel_Detail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Detail.Location = new System.Drawing.Point(0, 130);
            this.panel_Detail.Name = "panel_Detail";
            this.panel_Detail.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.panel_Detail.Size = new System.Drawing.Size(800, 320);
            this.panel_Detail.TabIndex = 5;
            // 
            // GridViewLog
            // 
            this.GridViewLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridViewLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridViewLog.Location = new System.Drawing.Point(5, 0);
            this.GridViewLog.Name = "GridViewLog";
            this.GridViewLog.Size = new System.Drawing.Size(790, 315);
            this.GridViewLog.TabIndex = 4;
            // 
            // Form_Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel_Detail);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_Top);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Log";
            this.Text = "Form_Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Log_FormClosing);
            this.Load += new System.EventHandler(this.Form_Log_Load);
            this.panel_Top.ResumeLayout(false);
            this.panel29.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_BackToMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Close)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel_Detail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridViewLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Top;
        private System.Windows.Forms.Panel panel29;
        private System.Windows.Forms.PictureBox picBox_BackToMain;
        private System.Windows.Forms.PictureBox picBox_Min;
        private System.Windows.Forms.PictureBox picBox_Max;
        private System.Windows.Forms.PictureBox picBox_Close;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpCreateDateFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpCreateDateTo;
        private System.Windows.Forms.ComboBox cbxTypeSearch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel_Detail;
        private System.Windows.Forms.DataGridView GridViewLog;
        private System.Windows.Forms.Button btnSearchLog;
    }
}