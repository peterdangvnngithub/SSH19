
namespace TAKAKO_ERP_3LAYER
{
    partial class Form_Shipping_Output_PackingList
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
            this.barButton_Output_Data_PackingList = new DevExpress.XtraBars.BarButtonItem();
            this.barButton_Import_Data_PackingList = new DevExpress.XtraBars.BarButtonItem();
            this.barButton_ClearData = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.gridControl_Output_PackingList = new DevExpress.XtraGrid.GridControl();
            this.View_Output_PackingList = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_Output_PackingList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Output_PackingList)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.barButton_Output_Data_PackingList,
            this.barButton_Import_Data_PackingList,
            this.barButton_ClearData});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 4;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(1118, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // barButton_Output_Data_PackingList
            // 
            this.barButton_Output_Data_PackingList.Caption = "Output Data PackingList";
            this.barButton_Output_Data_PackingList.Id = 1;
            this.barButton_Output_Data_PackingList.ImageOptions.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.outbox_16x16;
            this.barButton_Output_Data_PackingList.ImageOptions.LargeImage = global::TAKAKO_ERP_3LAYER.Properties.Resources.outbox_32x32;
            this.barButton_Output_Data_PackingList.Name = "barButton_Output_Data_PackingList";
            this.barButton_Output_Data_PackingList.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButton_Output_Data_Excel_ItemClick);
            // 
            // barButton_Import_Data_PackingList
            // 
            this.barButton_Import_Data_PackingList.Caption = "Import Data Packing List";
            this.barButton_Import_Data_PackingList.Id = 2;
            this.barButton_Import_Data_PackingList.ImageOptions.Image = global::TAKAKO_ERP_3LAYER.Properties.Resources.exporttoxlsx_16x16;
            this.barButton_Import_Data_PackingList.ImageOptions.LargeImage = global::TAKAKO_ERP_3LAYER.Properties.Resources.exporttoxlsx_32x32;
            this.barButton_Import_Data_PackingList.Name = "barButton_Import_Data_PackingList";
            this.barButton_Import_Data_PackingList.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButton_Import_Data_PackingList_ItemClick);
            // 
            // barButton_ClearData
            // 
            this.barButton_ClearData.Caption = "Clear Data";
            this.barButton_ClearData.Id = 3;
            this.barButton_ClearData.ImageOptions.SvgImage = global::TAKAKO_ERP_3LAYER.Properties.Resources.removesheet;
            this.barButton_ClearData.Name = "barButton_ClearData";
            this.barButton_ClearData.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButton_ClearData_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barButton_Import_Data_PackingList);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButton_Output_Data_PackingList);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButton_ClearData);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 468);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1118, 24);
            // 
            // gridControl_Output_PackingList
            // 
            this.gridControl_Output_PackingList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_Output_PackingList.Location = new System.Drawing.Point(0, 158);
            this.gridControl_Output_PackingList.MainView = this.View_Output_PackingList;
            this.gridControl_Output_PackingList.MenuManager = this.ribbon;
            this.gridControl_Output_PackingList.Name = "gridControl_Output_PackingList";
            this.gridControl_Output_PackingList.Size = new System.Drawing.Size(1118, 310);
            this.gridControl_Output_PackingList.TabIndex = 2;
            this.gridControl_Output_PackingList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.View_Output_PackingList});
            // 
            // View_Output_PackingList
            // 
            this.View_Output_PackingList.GridControl = this.gridControl_Output_PackingList;
            this.View_Output_PackingList.Name = "View_Output_PackingList";
            // 
            // Form_Shipping_Output_PackingList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 492);
            this.Controls.Add(this.gridControl_Output_PackingList);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "Form_Shipping_Output_PackingList";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Import & Export Packing List";
            this.Load += new System.EventHandler(this.Form_Shipping_Output_PackingList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_Output_PackingList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Output_PackingList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraGrid.GridControl gridControl_Output_PackingList;
        private DevExpress.XtraGrid.Views.Grid.GridView View_Output_PackingList;
        private DevExpress.XtraBars.BarButtonItem barButton_Output_Data_PackingList;
        private DevExpress.XtraBars.BarButtonItem barButton_Import_Data_PackingList;
        private DevExpress.XtraBars.BarButtonItem barButton_ClearData;
    }
}