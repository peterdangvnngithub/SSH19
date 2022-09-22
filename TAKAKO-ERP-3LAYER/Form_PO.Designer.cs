namespace TAKAKO_ERP_3LAYER
{
    partial class Form_PO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_PO));
            this.panel_Bottom = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button_Cancel_PO = new System.Windows.Forms.Button();
            this.panel_BottomRight = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.button_Clear = new System.Windows.Forms.Button();
            this.button_Import = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.picBox_BackToMain = new System.Windows.Forms.PictureBox();
            this.picBox_Min = new System.Windows.Forms.PictureBox();
            this.picBox_Max = new System.Windows.Forms.PictureBox();
            this.picBox_Close = new System.Windows.Forms.PictureBox();
            this.panel_Top = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel_Header = new System.Windows.Forms.Panel();
            this.btnSearch_ShippingNo = new System.Windows.Forms.Button();
            this.txtShippingNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtp_DueDateTo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch_POCustomer = new System.Windows.Forms.Button();
            this.txtPOCustomer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch_CodeTVC = new System.Windows.Forms.Button();
            this.txtCodeTVC = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSearch_ReciveNo = new System.Windows.Forms.Button();
            this.btnSearch_Customer = new System.Windows.Forms.Button();
            this.dtp_DueDateFrom = new System.Windows.Forms.DateTimePicker();
            this.txt_CustomerName = new System.Windows.Forms.TextBox();
            this.txtReceiveNo = new System.Windows.Forms.TextBox();
            this.txt_CustomerCode = new System.Windows.Forms.TextBox();
            this.lbl_ReceiveDate = new System.Windows.Forms.Label();
            this.lbl_ReceiveNo = new System.Windows.Forms.Label();
            this.lbl_Customer = new System.Windows.Forms.Label();
            this.panel_GridView = new System.Windows.Forms.Panel();
            this.GridView_PO = new System.Windows.Forms.DataGridView();
            this.panel_Bottom.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel_BottomRight.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_BackToMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Close)).BeginInit();
            this.panel_Top.SuspendLayout();
            this.panel_Header.SuspendLayout();
            this.panel_GridView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView_PO)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_Bottom
            // 
            this.panel_Bottom.Controls.Add(this.panel3);
            this.panel_Bottom.Controls.Add(this.panel_BottomRight);
            this.panel_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Bottom.Location = new System.Drawing.Point(0, 575);
            this.panel_Bottom.Name = "panel_Bottom";
            this.panel_Bottom.Size = new System.Drawing.Size(1084, 40);
            this.panel_Bottom.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button_Cancel_PO);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(284, 40);
            this.panel3.TabIndex = 4;
            this.panel3.TabStop = true;
            // 
            // button_Cancel_PO
            // 
            this.button_Cancel_PO.Location = new System.Drawing.Point(1, 8);
            this.button_Cancel_PO.Name = "button_Cancel_PO";
            this.button_Cancel_PO.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel_PO.TabIndex = 3;
            this.button_Cancel_PO.Text = "C&ancel PO";
            this.button_Cancel_PO.UseVisualStyleBackColor = true;
            this.button_Cancel_PO.Click += new System.EventHandler(this.button_Cancel_PO_Click);
            // 
            // panel_BottomRight
            // 
            this.panel_BottomRight.Controls.Add(this.btnExport);
            this.panel_BottomRight.Controls.Add(this.btn_Cancel);
            this.panel_BottomRight.Controls.Add(this.button_Clear);
            this.panel_BottomRight.Controls.Add(this.button_Import);
            this.panel_BottomRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_BottomRight.Location = new System.Drawing.Point(451, 0);
            this.panel_BottomRight.Name = "panel_BottomRight";
            this.panel_BottomRight.Size = new System.Drawing.Size(633, 40);
            this.panel_BottomRight.TabIndex = 2;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(345, 8);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(109, 23);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "&Export Data";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(541, 8);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(91, 23);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // button_Clear
            // 
            this.button_Clear.Location = new System.Drawing.Point(460, 8);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(75, 23);
            this.button_Clear.TabIndex = 2;
            this.button_Clear.Text = "&Clear All";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // button_Import
            // 
            this.button_Import.Location = new System.Drawing.Point(230, 8);
            this.button_Import.Name = "button_Import";
            this.button_Import.Size = new System.Drawing.Size(109, 23);
            this.button_Import.TabIndex = 0;
            this.button_Import.Text = "&Import Data";
            this.button_Import.UseVisualStyleBackColor = true;
            this.button_Import.Click += new System.EventHandler(this.button_Import_Click);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.panel2);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1084, 30);
            this.panelTop.TabIndex = 4;
            this.panelTop.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDoubleClick);
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
            this.panel2.Location = new System.Drawing.Point(949, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(135, 30);
            this.panel2.TabIndex = 2;
            // 
            // picBox_BackToMain
            // 
            this.picBox_BackToMain.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.Arrow_pointing;
            this.picBox_BackToMain.Location = new System.Drawing.Point(7, 2);
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
            this.picBox_Min.Location = new System.Drawing.Point(41, 2);
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
            this.picBox_Max.Location = new System.Drawing.Point(75, 2);
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
            this.picBox_Close.Location = new System.Drawing.Point(109, 2);
            this.picBox_Close.Name = "picBox_Close";
            this.picBox_Close.Size = new System.Drawing.Size(25, 25);
            this.picBox_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Close.TabIndex = 6;
            this.picBox_Close.TabStop = false;
            this.picBox_Close.Click += new System.EventHandler(this.picBox_Close_Click);
            this.picBox_Close.MouseEnter += new System.EventHandler(this.picBox_Close_MouseEnter);
            this.picBox_Close.MouseLeave += new System.EventHandler(this.picBox_Close_MouseLeave);
            // 
            // panel_Top
            // 
            this.panel_Top.Controls.Add(this.label4);
            this.panel_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Top.Location = new System.Drawing.Point(0, 30);
            this.panel_Top.Name = "panel_Top";
            this.panel_Top.Size = new System.Drawing.Size(1084, 40);
            this.panel_Top.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(70)))), ((int)(((byte)(1)))));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1084, 40);
            this.label4.TabIndex = 0;
            this.label4.Text = "CUSTOMER ORDER";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_Header
            // 
            this.panel_Header.Controls.Add(this.btnSearch_ShippingNo);
            this.panel_Header.Controls.Add(this.txtShippingNo);
            this.panel_Header.Controls.Add(this.label5);
            this.panel_Header.Controls.Add(this.dtp_DueDateTo);
            this.panel_Header.Controls.Add(this.label3);
            this.panel_Header.Controls.Add(this.btnSearch_POCustomer);
            this.panel_Header.Controls.Add(this.txtPOCustomer);
            this.panel_Header.Controls.Add(this.label2);
            this.panel_Header.Controls.Add(this.btnSearch_CodeTVC);
            this.panel_Header.Controls.Add(this.txtCodeTVC);
            this.panel_Header.Controls.Add(this.label1);
            this.panel_Header.Controls.Add(this.btnSearch);
            this.panel_Header.Controls.Add(this.btnSearch_ReciveNo);
            this.panel_Header.Controls.Add(this.btnSearch_Customer);
            this.panel_Header.Controls.Add(this.dtp_DueDateFrom);
            this.panel_Header.Controls.Add(this.txt_CustomerName);
            this.panel_Header.Controls.Add(this.txtReceiveNo);
            this.panel_Header.Controls.Add(this.txt_CustomerCode);
            this.panel_Header.Controls.Add(this.lbl_ReceiveDate);
            this.panel_Header.Controls.Add(this.lbl_ReceiveNo);
            this.panel_Header.Controls.Add(this.lbl_Customer);
            this.panel_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Header.Location = new System.Drawing.Point(0, 70);
            this.panel_Header.Name = "panel_Header";
            this.panel_Header.Size = new System.Drawing.Size(1084, 100);
            this.panel_Header.TabIndex = 0;
            // 
            // btnSearch_ShippingNo
            // 
            this.btnSearch_ShippingNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch_ShippingNo.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.Icon_search;
            this.btnSearch_ShippingNo.Location = new System.Drawing.Point(803, 9);
            this.btnSearch_ShippingNo.Name = "btnSearch_ShippingNo";
            this.btnSearch_ShippingNo.Size = new System.Drawing.Size(24, 24);
            this.btnSearch_ShippingNo.TabIndex = 17;
            this.btnSearch_ShippingNo.TabStop = false;
            this.btnSearch_ShippingNo.UseVisualStyleBackColor = true;
            // 
            // txtShippingNo
            // 
            this.txtShippingNo.Location = new System.Drawing.Point(643, 10);
            this.txtShippingNo.Name = "txtShippingNo";
            this.txtShippingNo.Size = new System.Drawing.Size(160, 22);
            this.txtShippingNo.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(523, 10);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.label5.Size = new System.Drawing.Size(119, 22);
            this.label5.TabIndex = 16;
            this.label5.Text = "Shipping No";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtp_DueDateTo
            // 
            this.dtp_DueDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_DueDateTo.Location = new System.Drawing.Point(255, 7);
            this.dtp_DueDateTo.Name = "dtp_DueDateTo";
            this.dtp_DueDateTo.Size = new System.Drawing.Size(122, 22);
            this.dtp_DueDateTo.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(228, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 22);
            this.label3.TabIndex = 14;
            this.label3.Text = "-";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnSearch_POCustomer
            // 
            this.btnSearch_POCustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch_POCustomer.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.Icon_search;
            this.btnSearch_POCustomer.Location = new System.Drawing.Point(803, 68);
            this.btnSearch_POCustomer.Name = "btnSearch_POCustomer";
            this.btnSearch_POCustomer.Size = new System.Drawing.Size(24, 24);
            this.btnSearch_POCustomer.TabIndex = 13;
            this.btnSearch_POCustomer.TabStop = false;
            this.btnSearch_POCustomer.UseVisualStyleBackColor = true;
            this.btnSearch_POCustomer.Click += new System.EventHandler(this.btnSearch_POCustomer_Click);
            // 
            // txtPOCustomer
            // 
            this.txtPOCustomer.Location = new System.Drawing.Point(643, 69);
            this.txtPOCustomer.Name = "txtPOCustomer";
            this.txtPOCustomer.Size = new System.Drawing.Size(160, 22);
            this.txtPOCustomer.TabIndex = 5;
            this.txtPOCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPOCustomer_KeyDown);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(523, 69);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.label2.Size = new System.Drawing.Size(119, 22);
            this.label2.TabIndex = 11;
            this.label2.Text = "PO of Customer";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSearch_CodeTVC
            // 
            this.btnSearch_CodeTVC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch_CodeTVC.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.Icon_search;
            this.btnSearch_CodeTVC.Location = new System.Drawing.Point(803, 37);
            this.btnSearch_CodeTVC.Name = "btnSearch_CodeTVC";
            this.btnSearch_CodeTVC.Size = new System.Drawing.Size(24, 24);
            this.btnSearch_CodeTVC.TabIndex = 10;
            this.btnSearch_CodeTVC.TabStop = false;
            this.btnSearch_CodeTVC.UseVisualStyleBackColor = true;
            this.btnSearch_CodeTVC.Click += new System.EventHandler(this.btnSearch_CodeTVC_Click);
            // 
            // txtCodeTVC
            // 
            this.txtCodeTVC.Location = new System.Drawing.Point(643, 38);
            this.txtCodeTVC.Name = "txtCodeTVC";
            this.txtCodeTVC.Size = new System.Drawing.Size(160, 22);
            this.txtCodeTVC.TabIndex = 4;
            this.txtCodeTVC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodeTVC_KeyDown);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(523, 38);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.label1.Size = new System.Drawing.Size(119, 22);
            this.label1.TabIndex = 8;
            this.label1.Text = "Item Code of TVC";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(848, 69);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "&Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSearch_ReciveNo
            // 
            this.btnSearch_ReciveNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch_ReciveNo.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.Icon_search;
            this.btnSearch_ReciveNo.Location = new System.Drawing.Point(226, 68);
            this.btnSearch_ReciveNo.Name = "btnSearch_ReciveNo";
            this.btnSearch_ReciveNo.Size = new System.Drawing.Size(24, 24);
            this.btnSearch_ReciveNo.TabIndex = 3;
            this.btnSearch_ReciveNo.TabStop = false;
            this.btnSearch_ReciveNo.UseVisualStyleBackColor = true;
            this.btnSearch_ReciveNo.Click += new System.EventHandler(this.btnSearch_ReciveNo_Click);
            // 
            // btnSearch_Customer
            // 
            this.btnSearch_Customer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch_Customer.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.Icon_search;
            this.btnSearch_Customer.Location = new System.Drawing.Point(451, 37);
            this.btnSearch_Customer.Name = "btnSearch_Customer";
            this.btnSearch_Customer.Size = new System.Drawing.Size(24, 24);
            this.btnSearch_Customer.TabIndex = 3;
            this.btnSearch_Customer.TabStop = false;
            this.btnSearch_Customer.UseVisualStyleBackColor = true;
            this.btnSearch_Customer.Click += new System.EventHandler(this.btnSearch_Customer_Click);
            // 
            // dtp_DueDateFrom
            // 
            this.dtp_DueDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_DueDateFrom.Location = new System.Drawing.Point(104, 7);
            this.dtp_DueDateFrom.Name = "dtp_DueDateFrom";
            this.dtp_DueDateFrom.Size = new System.Drawing.Size(122, 22);
            this.dtp_DueDateFrom.TabIndex = 0;
            // 
            // txt_CustomerName
            // 
            this.txt_CustomerName.Location = new System.Drawing.Point(227, 38);
            this.txt_CustomerName.Name = "txt_CustomerName";
            this.txt_CustomerName.ReadOnly = true;
            this.txt_CustomerName.Size = new System.Drawing.Size(224, 22);
            this.txt_CustomerName.TabIndex = 6;
            this.txt_CustomerName.TabStop = false;
            // 
            // txtReceiveNo
            // 
            this.txtReceiveNo.Location = new System.Drawing.Point(104, 69);
            this.txtReceiveNo.Name = "txtReceiveNo";
            this.txtReceiveNo.Size = new System.Drawing.Size(122, 22);
            this.txtReceiveNo.TabIndex = 3;
            this.txtReceiveNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReceiveNo_KeyDown);
            // 
            // txt_CustomerCode
            // 
            this.txt_CustomerCode.Location = new System.Drawing.Point(104, 38);
            this.txt_CustomerCode.Name = "txt_CustomerCode";
            this.txt_CustomerCode.Size = new System.Drawing.Size(122, 22);
            this.txt_CustomerCode.TabIndex = 2;
            this.txt_CustomerCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_CustomerCode_KeyDown);
            // 
            // lbl_ReceiveDate
            // 
            this.lbl_ReceiveDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_ReceiveDate.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ReceiveDate.Location = new System.Drawing.Point(3, 7);
            this.lbl_ReceiveDate.Name = "lbl_ReceiveDate";
            this.lbl_ReceiveDate.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.lbl_ReceiveDate.Size = new System.Drawing.Size(100, 22);
            this.lbl_ReceiveDate.TabIndex = 0;
            this.lbl_ReceiveDate.Text = "Due Date";
            this.lbl_ReceiveDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_ReceiveNo
            // 
            this.lbl_ReceiveNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_ReceiveNo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_ReceiveNo.Location = new System.Drawing.Point(3, 69);
            this.lbl_ReceiveNo.Name = "lbl_ReceiveNo";
            this.lbl_ReceiveNo.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.lbl_ReceiveNo.Size = new System.Drawing.Size(100, 22);
            this.lbl_ReceiveNo.TabIndex = 1;
            this.lbl_ReceiveNo.Text = "Receive No";
            this.lbl_ReceiveNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_Customer
            // 
            this.lbl_Customer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Customer.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_Customer.Location = new System.Drawing.Point(3, 38);
            this.lbl_Customer.Name = "lbl_Customer";
            this.lbl_Customer.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.lbl_Customer.Size = new System.Drawing.Size(100, 22);
            this.lbl_Customer.TabIndex = 2;
            this.lbl_Customer.Text = "Customer";
            this.lbl_Customer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel_GridView
            // 
            this.panel_GridView.Controls.Add(this.GridView_PO);
            this.panel_GridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_GridView.Location = new System.Drawing.Point(0, 170);
            this.panel_GridView.Name = "panel_GridView";
            this.panel_GridView.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.panel_GridView.Size = new System.Drawing.Size(1084, 405);
            this.panel_GridView.TabIndex = 1;
            // 
            // GridView_PO
            // 
            this.GridView_PO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridView_PO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridView_PO.Location = new System.Drawing.Point(2, 0);
            this.GridView_PO.Name = "GridView_PO";
            this.GridView_PO.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridView_PO.Size = new System.Drawing.Size(1080, 405);
            this.GridView_PO.TabIndex = 0;
            this.GridView_PO.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.GridView_PO_RowPostPaint);
            // 
            // Form_PO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 615);
            this.Controls.Add(this.panel_GridView);
            this.Controls.Add(this.panel_Header);
            this.Controls.Add(this.panel_Top);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panel_Bottom);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form_PO";
            this.Text = "Form_PO";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_PO_FormClosing);
            this.Load += new System.EventHandler(this.Form_PO_Load);
            this.panel_Bottom.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel_BottomRight.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_BackToMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Close)).EndInit();
            this.panel_Top.ResumeLayout(false);
            this.panel_Header.ResumeLayout(false);
            this.panel_Header.PerformLayout();
            this.panel_GridView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridView_PO)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_Bottom;
        private System.Windows.Forms.Button button_Import;
        private System.Windows.Forms.Panel panel_BottomRight;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panel_Top;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel_Header;
        private System.Windows.Forms.Button btnSearch_ReciveNo;
        private System.Windows.Forms.Button btnSearch_Customer;
        private System.Windows.Forms.DateTimePicker dtp_DueDateFrom;
        private System.Windows.Forms.TextBox txtReceiveNo;
        private System.Windows.Forms.Label lbl_ReceiveDate;
        private System.Windows.Forms.Label lbl_ReceiveNo;
        private System.Windows.Forms.Label lbl_Customer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel_GridView;
        private System.Windows.Forms.DataGridView GridView_PO;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.PictureBox picBox_Min;
        private System.Windows.Forms.PictureBox picBox_Max;
        private System.Windows.Forms.PictureBox picBox_Close;
        private System.Windows.Forms.PictureBox picBox_BackToMain;
        public System.Windows.Forms.TextBox txt_CustomerName;
        public System.Windows.Forms.TextBox txt_CustomerCode;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnSearch_CodeTVC;
        private System.Windows.Forms.TextBox txtCodeTVC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch_POCustomer;
        private System.Windows.Forms.TextBox txtPOCustomer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtp_DueDateTo;
        private System.Windows.Forms.Button btnSearch_ShippingNo;
        private System.Windows.Forms.TextBox txtShippingNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_Cancel_PO;
    }
}