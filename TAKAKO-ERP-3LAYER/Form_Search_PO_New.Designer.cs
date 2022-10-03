namespace TAKAKO_ERP_3LAYER
{
    partial class Form_Search_PO_New
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Search_PO_New));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barBtn_ImportData = new DevExpress.XtraBars.BarButtonItem();
            this.barBtn_Output_Data_Excel = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.gridControl_Search_PO = new DevExpress.XtraGrid.GridControl();
            this.View_PO_List = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_Search_PO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_PO_List)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.barBtn_ImportData,
            this.barBtn_Output_Data_Excel});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 3;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(957, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // barBtn_ImportData
            // 
            this.barBtn_ImportData.Caption = "&Import Data Excel";
            this.barBtn_ImportData.Id = 1;
            this.barBtn_ImportData.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtn_ImportData.ImageOptions.Image")));
            this.barBtn_ImportData.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtn_ImportData.ImageOptions.LargeImage")));
            this.barBtn_ImportData.Name = "barBtn_ImportData";
            this.barBtn_ImportData.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn_ImportData_ItemClick);
            // 
            // barBtn_Output_Data_Excel
            // 
            this.barBtn_Output_Data_Excel.Caption = "&Output Data Excel";
            this.barBtn_Output_Data_Excel.Id = 2;
            this.barBtn_Output_Data_Excel.ImageOptions.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.exporttoxlsx_16x162;
            this.barBtn_Output_Data_Excel.ImageOptions.LargeImage = global::TAKAKO_ERP_3LAYER.Properties.Resources.exporttoxlsx_32x322;
            this.barBtn_Output_Data_Excel.Name = "barBtn_Output_Data_Excel";
            this.barBtn_Output_Data_Excel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn_SaveData_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barBtn_ImportData);
            this.ribbonPageGroup1.ItemLinks.Add(this.barBtn_Output_Data_Excel);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 605);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(957, 24);
            // 
            // gridControl_Search_PO
            // 
            this.gridControl_Search_PO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_Search_PO.Location = new System.Drawing.Point(0, 158);
            this.gridControl_Search_PO.MainView = this.View_PO_List;
            this.gridControl_Search_PO.MenuManager = this.ribbon;
            this.gridControl_Search_PO.Name = "gridControl_Search_PO";
            this.gridControl_Search_PO.Size = new System.Drawing.Size(957, 447);
            this.gridControl_Search_PO.TabIndex = 2;
            this.gridControl_Search_PO.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.View_PO_List});
            this.gridControl_Search_PO.Click += new System.EventHandler(this.gridControl_Search_PO_Click);
            // 
            // View_PO_List
            // 
            this.View_PO_List.GridControl = this.gridControl_Search_PO;
            this.View_PO_List.Name = "View_PO_List";
            this.View_PO_List.OptionsScrollAnnotations.ShowSelectedRows = DevExpress.Utils.DefaultBoolean.True;
            this.View_PO_List.DoubleClick += new System.EventHandler(this.View_PO_List_DoubleClick);
            // 
            // Form_Search_PO_New
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 629);
            this.Controls.Add(this.gridControl_Search_PO);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "Form_Search_PO_New";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "SEARCH PO";
            this.Load += new System.EventHandler(this.Form_Import_PO_To_Shipping_Excel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_Search_PO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_PO_List)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem barBtn_ImportData;
        private DevExpress.XtraBars.BarButtonItem barBtn_Output_Data_Excel;
        private DevExpress.XtraGrid.GridControl gridControl_Search_PO;
        private DevExpress.XtraGrid.Views.Grid.GridView View_PO_List;
    }
}