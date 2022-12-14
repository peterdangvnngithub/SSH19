
namespace TAKAKO_ERP_3LAYER
{
    partial class Form_Shipping_Instruction_New
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
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barBtn_Back_To_Main_Menu = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barBtn_Lock_Data = new DevExpress.XtraBars.BarButtonItem();
            this.barBtn_Unlock_Data = new DevExpress.XtraBars.BarButtonItem();
            this.barBtn_Import_Export_PackingList = new DevExpress.XtraBars.BarButtonItem();
            this.barBtn_Save_Data = new DevExpress.XtraBars.BarButtonItem();
            this.barBtn_ClearData = new DevExpress.XtraBars.BarButtonItem();
            this.barBtn_Add_New_PO = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbon_Action = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbon_GridView = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.panel_Header = new DevExpress.XtraEditors.PanelControl();
            this.lookUpEdit_Freight = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEdit_CompanyCode = new DevExpress.XtraEditors.LookUpEdit();
            this.dateEdit_ETA = new DevExpress.XtraEditors.DateEdit();
            this.dateEdit_ETD = new DevExpress.XtraEditors.DateEdit();
            this.dateEdit_Revenue = new DevExpress.XtraEditors.DateEdit();
            this.label22 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.sLookUp_PortLoading = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.sLookUp_PortLoading_View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label12 = new System.Windows.Forms.Label();
            this.sLookUp_PortDestination = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.sLookUp_PortDestination_View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.sLookUp_TradeCondition = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.sLookUp_PriceCondition_View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label16 = new System.Windows.Forms.Label();
            this.sLookUp_PaymentTerm = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.sLookUp_PaymentTerm_View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label18 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.dateEdit_DateCreateShipping = new DevExpress.XtraEditors.DateEdit();
            this.txtShipVia = new DevExpress.XtraEditors.TextEdit();
            this.txtVessel = new DevExpress.XtraEditors.TextEdit();
            this.txtInvoiceNo = new DevExpress.XtraEditors.TextEdit();
            this.radLock = new System.Windows.Forms.RadioButton();
            this.sLookUp_ShippingNo = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.sLookUp_ShippingNo_View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.radNormal = new System.Windows.Forms.RadioButton();
            this.label26 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblShippingNo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIssuedTo_CompanyName = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.memo_IssuedTo_CompanyAddress = new DevExpress.XtraEditors.MemoEdit();
            this.sLookUp_IssuedTo_CompanyCode = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.sLookUp_IssuedTo_CompanyCode_View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtIssuedTo_FaxNo = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.txtShipTo_CompanyName = new DevExpress.XtraEditors.TextEdit();
            this.txtIssuedTo_TelNo = new DevExpress.XtraEditors.TextEdit();
            this.memo_ShipTo_CompanyAddress = new DevExpress.XtraEditors.MemoEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.txtShipTo_TelNo = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.sLookUp_ShipTo_CompanyCode = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.sLookUp_ShipTo_CompanyCode_View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtShipTo_FaxNo = new DevExpress.XtraEditors.TextEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel_Detail = new DevExpress.XtraEditors.PanelControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTab_Invoice = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl_Invoice = new DevExpress.XtraGrid.GridControl();
            this.gridView_Invoice = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtraTab_PackingList = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl_PackingList = new DevExpress.XtraGrid.GridControl();
            this.gridView_PackingList = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel_Header)).BeginInit();
            this.panel_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit_Freight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit_CompanyCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_ETA.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_ETA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_ETD.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_ETD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_Revenue.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_Revenue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_PortLoading.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_PortLoading_View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_PortDestination.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_PortDestination_View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_TradeCondition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_PriceCondition_View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_PaymentTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_PaymentTerm_View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_DateCreateShipping.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_DateCreateShipping.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipVia.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVessel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvoiceNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_ShippingNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_ShippingNo_View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIssuedTo_CompanyName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memo_IssuedTo_CompanyAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_IssuedTo_CompanyCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_IssuedTo_CompanyCode_View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIssuedTo_FaxNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipTo_CompanyName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIssuedTo_TelNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memo_ShipTo_CompanyAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipTo_TelNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_ShipTo_CompanyCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_ShipTo_CompanyCode_View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipTo_FaxNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel_Detail)).BeginInit();
            this.panel_Detail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTab_Invoice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_Invoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_Invoice)).BeginInit();
            this.xtraTab_PackingList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_PackingList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_PackingList)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.CaptionBarItemLinks.Add(this.barBtn_Back_To_Main_Menu);
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barBtn_Back_To_Main_Menu,
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.barButtonItem1,
            this.barBtn_Lock_Data,
            this.barBtn_Unlock_Data,
            this.barBtn_Import_Export_PackingList,
            this.barBtn_Save_Data,
            this.barBtn_ClearData,
            this.barBtn_Add_New_PO});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 9;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(1198, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // barBtn_Back_To_Main_Menu
            // 
            this.barBtn_Back_To_Main_Menu.Caption = "barButtonItem2";
            this.barBtn_Back_To_Main_Menu.Id = 8;
            this.barBtn_Back_To_Main_Menu.ImageOptions.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.icons8_undo_52;
            this.barBtn_Back_To_Main_Menu.Name = "barBtn_Back_To_Main_Menu";
            this.barBtn_Back_To_Main_Menu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn_Back_To_Main_Menu_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 7;
            this.barButtonItem1.ImageOptions.SvgImage = global::TAKAKO_ERP_3LAYER.Properties.Resources.prev;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barBtn_Lock_Data
            // 
            this.barBtn_Lock_Data.Caption = "&Lock Data";
            this.barBtn_Lock_Data.Id = 1;
            this.barBtn_Lock_Data.ImageOptions.SvgImage = global::TAKAKO_ERP_3LAYER.Properties.Resources.bo_security_permission;
            this.barBtn_Lock_Data.Name = "barBtn_Lock_Data";
            this.barBtn_Lock_Data.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn_Lock_Data_ItemClick);
            // 
            // barBtn_Unlock_Data
            // 
            this.barBtn_Unlock_Data.Caption = "&Unlock Data";
            this.barBtn_Unlock_Data.Id = 2;
            this.barBtn_Unlock_Data.ImageOptions.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.unprotectdocument_16x16;
            this.barBtn_Unlock_Data.ImageOptions.LargeImage = global::TAKAKO_ERP_3LAYER.Properties.Resources.unprotectdocument_32x32;
            this.barBtn_Unlock_Data.Name = "barBtn_Unlock_Data";
            this.barBtn_Unlock_Data.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn_Unlock_Data_ItemClick);
            // 
            // barBtn_Import_Export_PackingList
            // 
            this.barBtn_Import_Export_PackingList.Caption = "&Import / Export PL";
            this.barBtn_Import_Export_PackingList.Id = 3;
            this.barBtn_Import_Export_PackingList.ImageOptions.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.exporttoxlsx_16x161;
            this.barBtn_Import_Export_PackingList.ImageOptions.LargeImage = global::TAKAKO_ERP_3LAYER.Properties.Resources.exporttoxlsx_32x321;
            this.barBtn_Import_Export_PackingList.Name = "barBtn_Import_Export_PackingList";
            this.barBtn_Import_Export_PackingList.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn_Import_Export_PackingList_ItemClick);
            // 
            // barBtn_Save_Data
            // 
            this.barBtn_Save_Data.Caption = "&Save Data";
            this.barBtn_Save_Data.Id = 4;
            this.barBtn_Save_Data.ImageOptions.SvgImage = global::TAKAKO_ERP_3LAYER.Properties.Resources.saveas;
            this.barBtn_Save_Data.Name = "barBtn_Save_Data";
            this.barBtn_Save_Data.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn_Save_Data_ItemClick);
            // 
            // barBtn_ClearData
            // 
            this.barBtn_ClearData.Caption = "&Clear Data";
            this.barBtn_ClearData.Id = 5;
            this.barBtn_ClearData.ImageOptions.SvgImage = global::TAKAKO_ERP_3LAYER.Properties.Resources.clearpivottable;
            this.barBtn_ClearData.Name = "barBtn_ClearData";
            this.barBtn_ClearData.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn_ClearData_ItemClick);
            // 
            // barBtn_Add_New_PO
            // 
            this.barBtn_Add_New_PO.Caption = "&Add New PO";
            this.barBtn_Add_New_PO.Id = 6;
            this.barBtn_Add_New_PO.ImageOptions.SvgImage = global::TAKAKO_ERP_3LAYER.Properties.Resources.insertgroupfooter;
            this.barBtn_Add_New_PO.Name = "barBtn_Add_New_PO";
            this.barBtn_Add_New_PO.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn_Add_New_PO_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbon_Action,
            this.ribbon_GridView});
            this.ribbonPage1.Name = "ribbonPage1";
            // 
            // ribbon_Action
            // 
            this.ribbon_Action.ItemLinks.Add(this.barBtn_Lock_Data);
            this.ribbon_Action.ItemLinks.Add(this.barBtn_Unlock_Data);
            this.ribbon_Action.ItemLinks.Add(this.barBtn_Save_Data);
            this.ribbon_Action.ItemLinks.Add(this.barBtn_ClearData);
            this.ribbon_Action.Name = "ribbon_Action";
            this.ribbon_Action.Text = "Action";
            // 
            // ribbon_GridView
            // 
            this.ribbon_GridView.ItemLinks.Add(this.barBtn_Import_Export_PackingList);
            this.ribbon_GridView.ItemLinks.Add(this.barBtn_Add_New_PO);
            this.ribbon_GridView.Name = "ribbon_GridView";
            this.ribbon_GridView.Text = "GridView";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 783);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1198, 24);
            // 
            // panel_Header
            // 
            this.panel_Header.Controls.Add(this.lookUpEdit_Freight);
            this.panel_Header.Controls.Add(this.lookUpEdit_CompanyCode);
            this.panel_Header.Controls.Add(this.dateEdit_ETA);
            this.panel_Header.Controls.Add(this.dateEdit_ETD);
            this.panel_Header.Controls.Add(this.dateEdit_Revenue);
            this.panel_Header.Controls.Add(this.label22);
            this.panel_Header.Controls.Add(this.label10);
            this.panel_Header.Controls.Add(this.label11);
            this.panel_Header.Controls.Add(this.sLookUp_PortLoading);
            this.panel_Header.Controls.Add(this.label12);
            this.panel_Header.Controls.Add(this.sLookUp_PortDestination);
            this.panel_Header.Controls.Add(this.label13);
            this.panel_Header.Controls.Add(this.label14);
            this.panel_Header.Controls.Add(this.label15);
            this.panel_Header.Controls.Add(this.sLookUp_TradeCondition);
            this.panel_Header.Controls.Add(this.label16);
            this.panel_Header.Controls.Add(this.sLookUp_PaymentTerm);
            this.panel_Header.Controls.Add(this.label18);
            this.panel_Header.Controls.Add(this.label25);
            this.panel_Header.Controls.Add(this.dateEdit_DateCreateShipping);
            this.panel_Header.Controls.Add(this.txtShipVia);
            this.panel_Header.Controls.Add(this.txtVessel);
            this.panel_Header.Controls.Add(this.txtInvoiceNo);
            this.panel_Header.Controls.Add(this.radLock);
            this.panel_Header.Controls.Add(this.sLookUp_ShippingNo);
            this.panel_Header.Controls.Add(this.radNormal);
            this.panel_Header.Controls.Add(this.label26);
            this.panel_Header.Controls.Add(this.lblStatus);
            this.panel_Header.Controls.Add(this.lblShippingNo);
            this.panel_Header.Controls.Add(this.label2);
            this.panel_Header.Controls.Add(this.txtIssuedTo_CompanyName);
            this.panel_Header.Controls.Add(this.label3);
            this.panel_Header.Controls.Add(this.memo_IssuedTo_CompanyAddress);
            this.panel_Header.Controls.Add(this.sLookUp_IssuedTo_CompanyCode);
            this.panel_Header.Controls.Add(this.txtIssuedTo_FaxNo);
            this.panel_Header.Controls.Add(this.label4);
            this.panel_Header.Controls.Add(this.txtShipTo_CompanyName);
            this.panel_Header.Controls.Add(this.txtIssuedTo_TelNo);
            this.panel_Header.Controls.Add(this.memo_ShipTo_CompanyAddress);
            this.panel_Header.Controls.Add(this.label6);
            this.panel_Header.Controls.Add(this.txtShipTo_TelNo);
            this.panel_Header.Controls.Add(this.label5);
            this.panel_Header.Controls.Add(this.sLookUp_ShipTo_CompanyCode);
            this.panel_Header.Controls.Add(this.txtShipTo_FaxNo);
            this.panel_Header.Controls.Add(this.label7);
            this.panel_Header.Controls.Add(this.label8);
            this.panel_Header.Controls.Add(this.label9);
            this.panel_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Header.Location = new System.Drawing.Point(0, 158);
            this.panel_Header.Name = "panel_Header";
            this.panel_Header.Size = new System.Drawing.Size(1198, 272);
            this.panel_Header.TabIndex = 2;
            // 
            // lookUpEdit_Freight
            // 
            this.lookUpEdit_Freight.Location = new System.Drawing.Point(648, 81);
            this.lookUpEdit_Freight.Name = "lookUpEdit_Freight";
            this.lookUpEdit_Freight.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit_Freight.Size = new System.Drawing.Size(110, 20);
            this.lookUpEdit_Freight.TabIndex = 57;
            // 
            // lookUpEdit_CompanyCode
            // 
            this.lookUpEdit_CompanyCode.Location = new System.Drawing.Point(148, 25);
            this.lookUpEdit_CompanyCode.MenuManager = this.ribbon;
            this.lookUpEdit_CompanyCode.Name = "lookUpEdit_CompanyCode";
            this.lookUpEdit_CompanyCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit_CompanyCode.Size = new System.Drawing.Size(110, 20);
            this.lookUpEdit_CompanyCode.TabIndex = 57;
            // 
            // dateEdit_ETA
            // 
            this.dateEdit_ETA.EditValue = null;
            this.dateEdit_ETA.Location = new System.Drawing.Point(648, 201);
            this.dateEdit_ETA.Name = "dateEdit_ETA";
            this.dateEdit_ETA.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_ETA.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_ETA.Size = new System.Drawing.Size(110, 20);
            this.dateEdit_ETA.TabIndex = 13;
            // 
            // dateEdit_ETD
            // 
            this.dateEdit_ETD.EditValue = null;
            this.dateEdit_ETD.Location = new System.Drawing.Point(648, 177);
            this.dateEdit_ETD.Name = "dateEdit_ETD";
            this.dateEdit_ETD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_ETD.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_ETD.Size = new System.Drawing.Size(110, 20);
            this.dateEdit_ETD.TabIndex = 12;
            // 
            // dateEdit_Revenue
            // 
            this.dateEdit_Revenue.EditValue = null;
            this.dateEdit_Revenue.Location = new System.Drawing.Point(648, 33);
            this.dateEdit_Revenue.Name = "dateEdit_Revenue";
            this.dateEdit_Revenue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_Revenue.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_Revenue.Properties.DisplayFormat.FormatString = "MM/yyyy";
            this.dateEdit_Revenue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit_Revenue.Properties.Name = "dateEdit_Revenue";
            this.dateEdit_Revenue.Size = new System.Drawing.Size(110, 20);
            this.dateEdit_Revenue.TabIndex = 6;
            // 
            // label22
            // 
            this.label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label22.Location = new System.Drawing.Point(513, 33);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(134, 20);
            this.label22.TabIndex = 55;
            this.label22.Text = "REVENUE";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(513, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(134, 20);
            this.label10.TabIndex = 45;
            this.label10.Text = "SHIP VIA";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(513, 81);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(134, 20);
            this.label11.TabIndex = 47;
            this.label11.Text = "FREIGHT";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // sLookUp_PortLoading
            // 
            this.sLookUp_PortLoading.Location = new System.Drawing.Point(648, 129);
            this.sLookUp_PortLoading.Name = "sLookUp_PortLoading";
            this.sLookUp_PortLoading.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sLookUp_PortLoading.Properties.PopupView = this.sLookUp_PortLoading_View;
            this.sLookUp_PortLoading.Size = new System.Drawing.Size(335, 20);
            this.sLookUp_PortLoading.TabIndex = 10;
            // 
            // sLookUp_PortLoading_View
            // 
            this.sLookUp_PortLoading_View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.sLookUp_PortLoading_View.Name = "sLookUp_PortLoading_View";
            this.sLookUp_PortLoading_View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.sLookUp_PortLoading_View.OptionsView.ShowGroupPanel = false;
            // 
            // label12
            // 
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(513, 105);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(134, 20);
            this.label12.TabIndex = 49;
            this.label12.Text = "VESSEL";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // sLookUp_PortDestination
            // 
            this.sLookUp_PortDestination.Location = new System.Drawing.Point(648, 153);
            this.sLookUp_PortDestination.Name = "sLookUp_PortDestination";
            this.sLookUp_PortDestination.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sLookUp_PortDestination.Properties.PopupView = this.sLookUp_PortDestination_View;
            this.sLookUp_PortDestination.Size = new System.Drawing.Size(335, 20);
            this.sLookUp_PortDestination.TabIndex = 11;
            // 
            // sLookUp_PortDestination_View
            // 
            this.sLookUp_PortDestination_View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.sLookUp_PortDestination_View.Name = "sLookUp_PortDestination_View";
            this.sLookUp_PortDestination_View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.sLookUp_PortDestination_View.OptionsView.ShowGroupPanel = false;
            // 
            // label13
            // 
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(513, 129);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(134, 20);
            this.label13.TabIndex = 44;
            this.label13.Text = "PORT OF LOADING";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(513, 153);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(134, 20);
            this.label14.TabIndex = 51;
            this.label14.Text = "PORT OF DESTINATION";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(513, 177);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(134, 20);
            this.label15.TabIndex = 46;
            this.label15.Text = "ETD";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // sLookUp_TradeCondition
            // 
            this.sLookUp_TradeCondition.Location = new System.Drawing.Point(648, 225);
            this.sLookUp_TradeCondition.Name = "sLookUp_TradeCondition";
            this.sLookUp_TradeCondition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sLookUp_TradeCondition.Properties.PopupView = this.sLookUp_PriceCondition_View;
            this.sLookUp_TradeCondition.Size = new System.Drawing.Size(335, 20);
            this.sLookUp_TradeCondition.TabIndex = 14;
            // 
            // sLookUp_PriceCondition_View
            // 
            this.sLookUp_PriceCondition_View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.sLookUp_PriceCondition_View.Name = "sLookUp_PriceCondition_View";
            this.sLookUp_PriceCondition_View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.sLookUp_PriceCondition_View.OptionsView.ShowGroupPanel = false;
            // 
            // label16
            // 
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label16.Location = new System.Drawing.Point(513, 201);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(134, 20);
            this.label16.TabIndex = 52;
            this.label16.Text = "ETA";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // sLookUp_PaymentTerm
            // 
            this.sLookUp_PaymentTerm.Location = new System.Drawing.Point(648, 249);
            this.sLookUp_PaymentTerm.Name = "sLookUp_PaymentTerm";
            this.sLookUp_PaymentTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sLookUp_PaymentTerm.Properties.PopupView = this.sLookUp_PaymentTerm_View;
            this.sLookUp_PaymentTerm.Size = new System.Drawing.Size(335, 20);
            this.sLookUp_PaymentTerm.TabIndex = 15;
            // 
            // sLookUp_PaymentTerm_View
            // 
            this.sLookUp_PaymentTerm_View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.sLookUp_PaymentTerm_View.Name = "sLookUp_PaymentTerm_View";
            this.sLookUp_PaymentTerm_View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.sLookUp_PaymentTerm_View.OptionsView.ShowGroupPanel = false;
            // 
            // label18
            // 
            this.label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label18.Location = new System.Drawing.Point(513, 225);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(134, 20);
            this.label18.TabIndex = 48;
            this.label18.Text = "TRADE CONDITION";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label25
            // 
            this.label25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label25.Location = new System.Drawing.Point(513, 249);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(134, 20);
            this.label25.TabIndex = 43;
            this.label25.Text = "PAYMENT TERM";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateEdit_DateCreateShipping
            // 
            this.dateEdit_DateCreateShipping.EditValue = null;
            this.dateEdit_DateCreateShipping.Location = new System.Drawing.Point(148, 67);
            this.dateEdit_DateCreateShipping.Name = "dateEdit_DateCreateShipping";
            this.dateEdit_DateCreateShipping.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_DateCreateShipping.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_DateCreateShipping.Size = new System.Drawing.Size(110, 20);
            this.dateEdit_DateCreateShipping.TabIndex = 3;
            // 
            // txtShipVia
            // 
            this.txtShipVia.Location = new System.Drawing.Point(648, 57);
            this.txtShipVia.Name = "txtShipVia";
            this.txtShipVia.Size = new System.Drawing.Size(335, 20);
            this.txtShipVia.TabIndex = 7;
            // 
            // txtVessel
            // 
            this.txtVessel.Location = new System.Drawing.Point(648, 105);
            this.txtVessel.Name = "txtVessel";
            this.txtVessel.Size = new System.Drawing.Size(335, 20);
            this.txtVessel.TabIndex = 9;
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.Location = new System.Drawing.Point(148, 88);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(335, 20);
            this.txtInvoiceNo.TabIndex = 5;
            // 
            // radLock
            // 
            this.radLock.AutoSize = true;
            this.radLock.Enabled = false;
            this.radLock.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.radLock.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLock.Location = new System.Drawing.Point(246, 4);
            this.radLock.Name = "radLock";
            this.radLock.Size = new System.Drawing.Size(57, 20);
            this.radLock.TabIndex = 1;
            this.radLock.Text = "Lock";
            this.radLock.UseVisualStyleBackColor = true;
            // 
            // sLookUp_ShippingNo
            // 
            this.sLookUp_ShippingNo.Location = new System.Drawing.Point(148, 46);
            this.sLookUp_ShippingNo.Name = "sLookUp_ShippingNo";
            this.sLookUp_ShippingNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sLookUp_ShippingNo.Properties.PopupView = this.sLookUp_ShippingNo_View;
            this.sLookUp_ShippingNo.Size = new System.Drawing.Size(334, 20);
            this.sLookUp_ShippingNo.TabIndex = 2;
            this.sLookUp_ShippingNo.EditValueChanged += new System.EventHandler(this.sLookUp_ShippingNo_EditValueChanged);
            // 
            // sLookUp_ShippingNo_View
            // 
            this.sLookUp_ShippingNo_View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.sLookUp_ShippingNo_View.Name = "sLookUp_ShippingNo_View";
            this.sLookUp_ShippingNo_View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.sLookUp_ShippingNo_View.OptionsView.ShowGroupPanel = false;
            // 
            // radNormal
            // 
            this.radNormal.AutoSize = true;
            this.radNormal.Checked = true;
            this.radNormal.Enabled = false;
            this.radNormal.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.radNormal.Font = new System.Drawing.Font("Segoe UI", 9.25F, System.Drawing.FontStyle.Bold);
            this.radNormal.Location = new System.Drawing.Point(162, 4);
            this.radNormal.Name = "radNormal";
            this.radNormal.Size = new System.Drawing.Size(78, 22);
            this.radNormal.TabIndex = 0;
            this.radNormal.TabStop = true;
            this.radNormal.Text = "Normal";
            this.radNormal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radNormal.UseVisualStyleBackColor = false;
            // 
            // label26
            // 
            this.label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label26.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label26.Location = new System.Drawing.Point(13, 25);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(134, 20);
            this.label26.TabIndex = 35;
            this.label26.Text = "COMPANY CODE";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatus
            // 
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(13, 3);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(134, 21);
            this.lblStatus.TabIndex = 14;
            this.lblStatus.Text = "STATUS";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblShippingNo
            // 
            this.lblShippingNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblShippingNo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblShippingNo.Location = new System.Drawing.Point(13, 46);
            this.lblShippingNo.Name = "lblShippingNo";
            this.lblShippingNo.Size = new System.Drawing.Size(134, 20);
            this.lblShippingNo.TabIndex = 16;
            this.lblShippingNo.Text = "SHIPPING NO";
            this.lblShippingNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(13, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "DATE";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIssuedTo_CompanyName
            // 
            this.txtIssuedTo_CompanyName.Location = new System.Drawing.Point(213, 109);
            this.txtIssuedTo_CompanyName.Name = "txtIssuedTo_CompanyName";
            this.txtIssuedTo_CompanyName.Size = new System.Drawing.Size(270, 20);
            this.txtIssuedTo_CompanyName.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(13, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "INVOICE No.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // memo_IssuedTo_CompanyAddress
            // 
            this.memo_IssuedTo_CompanyAddress.EditValue = "";
            this.memo_IssuedTo_CompanyAddress.Location = new System.Drawing.Point(148, 130);
            this.memo_IssuedTo_CompanyAddress.Name = "memo_IssuedTo_CompanyAddress";
            this.memo_IssuedTo_CompanyAddress.Size = new System.Drawing.Size(335, 35);
            this.memo_IssuedTo_CompanyAddress.TabIndex = 22;
            // 
            // sLookUp_IssuedTo_CompanyCode
            // 
            this.sLookUp_IssuedTo_CompanyCode.Location = new System.Drawing.Point(148, 109);
            this.sLookUp_IssuedTo_CompanyCode.Name = "sLookUp_IssuedTo_CompanyCode";
            this.sLookUp_IssuedTo_CompanyCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sLookUp_IssuedTo_CompanyCode.Properties.PopupView = this.sLookUp_IssuedTo_CompanyCode_View;
            this.sLookUp_IssuedTo_CompanyCode.Properties.EditValueChanged += new System.EventHandler(this.sLookUp_IssuedTo_CompanyCode_EditValueChanged);
            this.sLookUp_IssuedTo_CompanyCode.Size = new System.Drawing.Size(64, 20);
            this.sLookUp_IssuedTo_CompanyCode.TabIndex = 4;
            // 
            // sLookUp_IssuedTo_CompanyCode_View
            // 
            this.sLookUp_IssuedTo_CompanyCode_View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.sLookUp_IssuedTo_CompanyCode_View.Name = "sLookUp_IssuedTo_CompanyCode_View";
            this.sLookUp_IssuedTo_CompanyCode_View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.sLookUp_IssuedTo_CompanyCode_View.OptionsView.ShowGroupPanel = false;
            // 
            // txtIssuedTo_FaxNo
            // 
            this.txtIssuedTo_FaxNo.Location = new System.Drawing.Point(363, 166);
            this.txtIssuedTo_FaxNo.Name = "txtIssuedTo_FaxNo";
            this.txtIssuedTo_FaxNo.Size = new System.Drawing.Size(120, 20);
            this.txtIssuedTo_FaxNo.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(13, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 20);
            this.label4.TabIndex = 20;
            this.label4.Text = "ISSUED TO";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtShipTo_CompanyName
            // 
            this.txtShipTo_CompanyName.Location = new System.Drawing.Point(213, 187);
            this.txtShipTo_CompanyName.Name = "txtShipTo_CompanyName";
            this.txtShipTo_CompanyName.Size = new System.Drawing.Size(270, 20);
            this.txtShipTo_CompanyName.TabIndex = 19;
            // 
            // txtIssuedTo_TelNo
            // 
            this.txtIssuedTo_TelNo.Location = new System.Drawing.Point(184, 166);
            this.txtIssuedTo_TelNo.Name = "txtIssuedTo_TelNo";
            this.txtIssuedTo_TelNo.Size = new System.Drawing.Size(120, 20);
            this.txtIssuedTo_TelNo.TabIndex = 18;
            // 
            // memo_ShipTo_CompanyAddress
            // 
            this.memo_ShipTo_CompanyAddress.EditValue = "";
            this.memo_ShipTo_CompanyAddress.Location = new System.Drawing.Point(148, 208);
            this.memo_ShipTo_CompanyAddress.Name = "memo_ShipTo_CompanyAddress";
            this.memo_ShipTo_CompanyAddress.Size = new System.Drawing.Size(335, 40);
            this.memo_ShipTo_CompanyAddress.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(327, 166);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 20);
            this.label6.TabIndex = 34;
            this.label6.Text = "FAX:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtShipTo_TelNo
            // 
            this.txtShipTo_TelNo.Location = new System.Drawing.Point(184, 249);
            this.txtShipTo_TelNo.Name = "txtShipTo_TelNo";
            this.txtShipTo_TelNo.Size = new System.Drawing.Size(120, 20);
            this.txtShipTo_TelNo.TabIndex = 27;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(148, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 20);
            this.label5.TabIndex = 31;
            this.label5.Text = "TEL:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sLookUp_ShipTo_CompanyCode
            // 
            this.sLookUp_ShipTo_CompanyCode.Location = new System.Drawing.Point(148, 187);
            this.sLookUp_ShipTo_CompanyCode.Name = "sLookUp_ShipTo_CompanyCode";
            this.sLookUp_ShipTo_CompanyCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sLookUp_ShipTo_CompanyCode.Properties.PopupView = this.sLookUp_ShipTo_CompanyCode_View;
            this.sLookUp_ShipTo_CompanyCode.Properties.EditValueChanged += new System.EventHandler(this.sLookUp_ShipTo_CompanyCode_EditValueChanged);
            this.sLookUp_ShipTo_CompanyCode.Size = new System.Drawing.Size(64, 20);
            this.sLookUp_ShipTo_CompanyCode.TabIndex = 5;
            // 
            // sLookUp_ShipTo_CompanyCode_View
            // 
            this.sLookUp_ShipTo_CompanyCode_View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.sLookUp_ShipTo_CompanyCode_View.Name = "sLookUp_ShipTo_CompanyCode_View";
            this.sLookUp_ShipTo_CompanyCode_View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.sLookUp_ShipTo_CompanyCode_View.OptionsView.ShowGroupPanel = false;
            // 
            // txtShipTo_FaxNo
            // 
            this.txtShipTo_FaxNo.Location = new System.Drawing.Point(363, 249);
            this.txtShipTo_FaxNo.Name = "txtShipTo_FaxNo";
            this.txtShipTo_FaxNo.Size = new System.Drawing.Size(120, 20);
            this.txtShipTo_FaxNo.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(13, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(134, 20);
            this.label7.TabIndex = 30;
            this.label7.Text = "SHIP TO";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(327, 249);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 20);
            this.label8.TabIndex = 33;
            this.label8.Text = "FAX:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(148, 249);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 20);
            this.label9.TabIndex = 32;
            this.label9.Text = "TEL:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel_Detail
            // 
            this.panel_Detail.Controls.Add(this.xtraTabControl1);
            this.panel_Detail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Detail.Location = new System.Drawing.Point(0, 430);
            this.panel_Detail.Name = "panel_Detail";
            this.panel_Detail.Size = new System.Drawing.Size(1198, 353);
            this.panel_Detail.TabIndex = 3;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(2, 2);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTab_Invoice;
            this.xtraTabControl1.Size = new System.Drawing.Size(1194, 349);
            this.xtraTabControl1.TabIndex = 59;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTab_Invoice,
            this.xtraTab_PackingList});
            // 
            // xtraTab_Invoice
            // 
            this.xtraTab_Invoice.Controls.Add(this.gridControl_Invoice);
            this.xtraTab_Invoice.Name = "xtraTab_Invoice";
            this.xtraTab_Invoice.Size = new System.Drawing.Size(1192, 324);
            this.xtraTab_Invoice.Text = "Invoice";
            // 
            // gridControl_Invoice
            // 
            this.gridControl_Invoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_Invoice.Location = new System.Drawing.Point(0, 0);
            this.gridControl_Invoice.MainView = this.gridView_Invoice;
            this.gridControl_Invoice.MenuManager = this.ribbon;
            this.gridControl_Invoice.Name = "gridControl_Invoice";
            this.gridControl_Invoice.Size = new System.Drawing.Size(1192, 324);
            this.gridControl_Invoice.TabIndex = 0;
            this.gridControl_Invoice.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_Invoice});
            // 
            // gridView_Invoice
            // 
            this.gridView_Invoice.GridControl = this.gridControl_Invoice;
            this.gridView_Invoice.Name = "gridView_Invoice";
            // 
            // xtraTab_PackingList
            // 
            this.xtraTab_PackingList.Controls.Add(this.gridControl_PackingList);
            this.xtraTab_PackingList.Name = "xtraTab_PackingList";
            this.xtraTab_PackingList.Size = new System.Drawing.Size(1192, 324);
            this.xtraTab_PackingList.Text = "Packing List";
            // 
            // gridControl_PackingList
            // 
            this.gridControl_PackingList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_PackingList.Location = new System.Drawing.Point(0, 0);
            this.gridControl_PackingList.MainView = this.gridView_PackingList;
            this.gridControl_PackingList.MenuManager = this.ribbon;
            this.gridControl_PackingList.Name = "gridControl_PackingList";
            this.gridControl_PackingList.Size = new System.Drawing.Size(1192, 324);
            this.gridControl_PackingList.TabIndex = 1;
            this.gridControl_PackingList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_PackingList});
            // 
            // gridView_PackingList
            // 
            this.gridView_PackingList.GridControl = this.gridControl_PackingList;
            this.gridView_PackingList.Name = "gridView_PackingList";
            // 
            // Form_Shipping_Instruction_New
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 807);
            this.Controls.Add(this.panel_Detail);
            this.Controls.Add(this.panel_Header);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "Form_Shipping_Instruction_New";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "SHIPPING INSTRUCTION";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Shipping_Instruction_New_FormClosing);
            this.Load += new System.EventHandler(this.Form_Shipping_Instruction_New_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel_Header)).EndInit();
            this.panel_Header.ResumeLayout(false);
            this.panel_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit_Freight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit_CompanyCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_ETA.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_ETA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_ETD.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_ETD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_Revenue.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_Revenue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_PortLoading.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_PortLoading_View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_PortDestination.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_PortDestination_View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_TradeCondition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_PriceCondition_View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_PaymentTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_PaymentTerm_View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_DateCreateShipping.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_DateCreateShipping.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipVia.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVessel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvoiceNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_ShippingNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_ShippingNo_View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIssuedTo_CompanyName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memo_IssuedTo_CompanyAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_IssuedTo_CompanyCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_IssuedTo_CompanyCode_View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIssuedTo_FaxNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipTo_CompanyName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIssuedTo_TelNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memo_ShipTo_CompanyAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipTo_TelNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_ShipTo_CompanyCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUp_ShipTo_CompanyCode_View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipTo_FaxNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel_Detail)).EndInit();
            this.panel_Detail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTab_Invoice.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_Invoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_Invoice)).EndInit();
            this.xtraTab_PackingList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_PackingList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_PackingList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbon_Action;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraEditors.PanelControl panel_Header;
        private DevExpress.XtraEditors.PanelControl panel_Detail;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbon_GridView;
        private DevExpress.XtraBars.BarButtonItem barBtn_Lock_Data;
        private DevExpress.XtraBars.BarButtonItem barBtn_Unlock_Data;
        private DevExpress.XtraBars.BarButtonItem barBtn_Import_Export_PackingList;
        private DevExpress.XtraBars.BarButtonItem barBtn_Save_Data;
        private DevExpress.XtraBars.BarButtonItem barBtn_ClearData;
        private DevExpress.XtraEditors.DateEdit dateEdit_DateCreateShipping;
        private DevExpress.XtraEditors.TextEdit txtInvoiceNo;
        private System.Windows.Forms.RadioButton radLock;
        private DevExpress.XtraEditors.SearchLookUpEdit sLookUp_ShippingNo;
        private DevExpress.XtraGrid.Views.Grid.GridView sLookUp_ShippingNo_View;
        private System.Windows.Forms.RadioButton radNormal;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblShippingNo;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtIssuedTo_CompanyName;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.MemoEdit memo_IssuedTo_CompanyAddress;
        private DevExpress.XtraEditors.SearchLookUpEdit sLookUp_IssuedTo_CompanyCode;
        private DevExpress.XtraGrid.Views.Grid.GridView sLookUp_IssuedTo_CompanyCode_View;
        private DevExpress.XtraEditors.TextEdit txtIssuedTo_FaxNo;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit txtShipTo_CompanyName;
        private DevExpress.XtraEditors.TextEdit txtIssuedTo_TelNo;
        private DevExpress.XtraEditors.MemoEdit memo_ShipTo_CompanyAddress;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit txtShipTo_TelNo;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.SearchLookUpEdit sLookUp_ShipTo_CompanyCode;
        private DevExpress.XtraGrid.Views.Grid.GridView sLookUp_ShipTo_CompanyCode_View;
        private DevExpress.XtraEditors.TextEdit txtShipTo_FaxNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.DateEdit dateEdit_ETA;
        private DevExpress.XtraEditors.DateEdit dateEdit_ETD;
        private DevExpress.XtraEditors.DateEdit dateEdit_Revenue;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.SearchLookUpEdit sLookUp_PortLoading;
        private DevExpress.XtraGrid.Views.Grid.GridView sLookUp_PortLoading_View;
        private System.Windows.Forms.Label label12;
        private DevExpress.XtraEditors.SearchLookUpEdit sLookUp_PortDestination;
        private DevExpress.XtraGrid.Views.Grid.GridView sLookUp_PortDestination_View;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private DevExpress.XtraEditors.SearchLookUpEdit sLookUp_TradeCondition;
        private DevExpress.XtraGrid.Views.Grid.GridView sLookUp_PriceCondition_View;
        private System.Windows.Forms.Label label16;
        private DevExpress.XtraEditors.SearchLookUpEdit sLookUp_PaymentTerm;
        private DevExpress.XtraGrid.Views.Grid.GridView sLookUp_PaymentTerm_View;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label25;
        private DevExpress.XtraGrid.GridControl gridControl_Invoice;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_Invoice;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTab_Invoice;
        private DevExpress.XtraTab.XtraTabPage xtraTab_PackingList;
        private DevExpress.XtraGrid.GridControl gridControl_PackingList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_PackingList;
        private DevExpress.XtraBars.BarButtonItem barBtn_Add_New_PO;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraEditors.TextEdit txtVessel;
        private DevExpress.XtraEditors.TextEdit txtShipVia;
        private DevExpress.XtraBars.BarButtonItem barBtn_Back_To_Main_Menu;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit_CompanyCode;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit_Freight;
    }
}