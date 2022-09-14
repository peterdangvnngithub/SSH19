namespace TAKAKO_ERP_3LAYER
{
    partial class Form_Select_DB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Select_DB));
            this.lblCompany = new System.Windows.Forms.Label();
            this.cbxServerName = new System.Windows.Forms.ComboBox();
            this.btnSelectCompany = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbBoxDataBase = new System.Windows.Forms.ComboBox();
            this.btnTestConnecttion = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCompany
            // 
            this.lblCompany.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCompany.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(89)))), ((int)(((byte)(1)))));
            this.lblCompany.Location = new System.Drawing.Point(54, 52);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(112, 30);
            this.lblCompany.TabIndex = 0;
            this.lblCompany.Text = "Server Name";
            this.lblCompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbxServerName
            // 
            this.cbxServerName.FormattingEnabled = true;
            this.cbxServerName.Location = new System.Drawing.Point(172, 58);
            this.cbxServerName.Name = "cbxServerName";
            this.cbxServerName.Size = new System.Drawing.Size(227, 21);
            this.cbxServerName.TabIndex = 1;
            // 
            // btnSelectCompany
            // 
            this.btnSelectCompany.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectCompany.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(89)))), ((int)(((byte)(1)))));
            this.btnSelectCompany.Location = new System.Drawing.Point(171, 212);
            this.btnSelectCompany.Name = "btnSelectCompany";
            this.btnSelectCompany.Size = new System.Drawing.Size(86, 30);
            this.btnSelectCompany.TabIndex = 2;
            this.btnSelectCompany.Text = "Select";
            this.btnSelectCompany.UseVisualStyleBackColor = true;
            this.btnSelectCompany.Click += new System.EventHandler(this.btnSelectCompany_Click);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(89)))), ((int)(((byte)(1)))));
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(441, 40);
            this.panelTop.TabIndex = 3;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.panelTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseMove);
            this.panelTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(441, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "SELECT DATABASE";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseMove);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(89)))), ((int)(((byte)(1)))));
            this.btn_Cancel.Location = new System.Drawing.Point(262, 212);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(86, 30);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(89)))), ((int)(((byte)(1)))));
            this.label2.Location = new System.Drawing.Point(54, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 30);
            this.label2.TabIndex = 5;
            this.label2.Text = "DataBase";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(172, 133);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(227, 20);
            this.txtUserName.TabIndex = 7;
            // 
            // txtPassWord
            // 
            this.txtPassWord.Location = new System.Drawing.Point(172, 170);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.Size = new System.Drawing.Size(227, 20);
            this.txtPassWord.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(89)))), ((int)(((byte)(1)))));
            this.label3.Location = new System.Drawing.Point(54, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 30);
            this.label3.TabIndex = 9;
            this.label3.Text = "UserName";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(89)))), ((int)(((byte)(1)))));
            this.label4.Location = new System.Drawing.Point(54, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 30);
            this.label4.TabIndex = 10;
            this.label4.Text = "PassWord";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbBoxDataBase
            // 
            this.cbBoxDataBase.FormattingEnabled = true;
            this.cbBoxDataBase.Location = new System.Drawing.Point(172, 94);
            this.cbBoxDataBase.Name = "cbBoxDataBase";
            this.cbBoxDataBase.Size = new System.Drawing.Size(227, 21);
            this.cbBoxDataBase.TabIndex = 11;
            // 
            // btnTestConnecttion
            // 
            this.btnTestConnecttion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestConnecttion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(89)))), ((int)(((byte)(1)))));
            this.btnTestConnecttion.Location = new System.Drawing.Point(73, 212);
            this.btnTestConnecttion.Name = "btnTestConnecttion";
            this.btnTestConnecttion.Size = new System.Drawing.Size(93, 30);
            this.btnTestConnecttion.TabIndex = 12;
            this.btnTestConnecttion.Text = "Test Connect";
            this.btnTestConnecttion.UseVisualStyleBackColor = true;
            // 
            // Form_Select_DB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 258);
            this.Controls.Add(this.btnTestConnecttion);
            this.Controls.Add(this.cbBoxDataBase);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPassWord);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.btnSelectCompany);
            this.Controls.Add(this.cbxServerName);
            this.Controls.Add(this.lblCompany);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Select_DB";
            this.Text = "Form_Select_Company";
            this.Load += new System.EventHandler(this.Form_Select_Company_Load);
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.ComboBox cbxServerName;
        private System.Windows.Forms.Button btnSelectCompany;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbBoxDataBase;
        private System.Windows.Forms.Button btnTestConnecttion;
    }
}