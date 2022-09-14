using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using DevExpress.XtraBars;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;

namespace TAKAKO_ERP_3LAYER
{
    public partial class Form_Import_PO_To_Shipping_Excel : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private GridColumn gridCol_Company_Code = new GridColumn();
        private GridColumn gridCol_Customer_Code = new GridColumn();
        private GridColumn gridCol_Shipping_No = new GridColumn();
        private GridColumn gridCol_Invoice_No = new GridColumn();
        private GridColumn gridCol_Revise_No = new GridColumn();
        private GridColumn gridCol_Revise_Date = new GridColumn();
        private GridColumn gridCol_Revise_Version = new GridColumn();
        private GridColumn gridCol_Packages_No = new GridColumn();
        private GridColumn gridCol_Cus_Item_Code = new GridColumn();
        private GridColumn gridCol_TVC_Item_Code = new GridColumn();
        private GridColumn gridCol_Customer_PO = new GridColumn();
        private GridColumn gridCol_Qty_Carton = new GridColumn();
        private GridColumn gridCol_Qty_Per_Carton = new GridColumn();
        private GridColumn gridCol_Qty_Total = new GridColumn();
        private GridColumn gridCol_Qty_Revise = new GridColumn();
        private GridColumn gridCol_Net_Weight = new GridColumn();
        private GridColumn gridCol_Net_Weight_Total = new GridColumn();
        private GridColumn gridCol_Gross_Weight = new GridColumn();
        private GridColumn gridCol_Lot_No = new GridColumn();
        private GridColumn gridCol_Create_By = new GridColumn();
        private GridColumn gridCol_Create_At = new GridColumn();
        private GridColumn gridCol_Edit_By = new GridColumn();
        private GridColumn gridCol_Edit_At = new GridColumn();

        public Form_Import_PO_To_Shipping_Excel()
        {
            InitializeComponent();
        }

        private void Form_Import_PO_To_Shipping_Excel_Load(object sender, EventArgs e)
        {
            Define_GridView();
        }

        private void Define_GridView()
        {
            View_Import_PackingList.OptionsPrint.AutoWidth = false;
            View_Import_PackingList.OptionsView.ColumnAutoWidth = false;

            // COMPANY_CODE
            gridCol_Company_Code.Name = "gridCol_Company_Code";
            gridCol_Company_Code.Caption = "COMPANY_CODE";
            gridCol_Company_Code.FieldName = "COMPANY_CODE";
            gridCol_Company_Code.VisibleIndex = 0;
            gridCol_Company_Code.Width = 120;

            // CUSTOMER CODE
            gridCol_Customer_Code.Name = "gridCol_Customer_Code";
            gridCol_Customer_Code.Caption = "CUSTOMER_CODE";
            gridCol_Customer_Code.FieldName = "CUSTOMER_CODE";
            gridCol_Customer_Code.VisibleIndex = 0;
            gridCol_Customer_Code.Width = 120;
            gridCol_Customer_Code.AppearanceCell.Options.UseTextOptions = true;
            gridCol_Customer_Code.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            // SHIPPING NO
            gridCol_Shipping_No.Name = "gridCol_Shipping_No";
            gridCol_Shipping_No.Caption = "SHIPPING_NO";
            gridCol_Shipping_No.FieldName = "SHIPPING_NO";
            gridCol_Shipping_No.VisibleIndex = 0;
            gridCol_Shipping_No.Width = 120;
            gridCol_Shipping_No.AppearanceCell.Options.UseTextOptions = true;
            gridCol_Shipping_No.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            // INVOICE NO
            gridCol_Invoice_No.Name = "gridCol_Invoice_No";
            gridCol_Invoice_No.Caption = "INVOICE_NO";
            gridCol_Invoice_No.FieldName = "INVOICE_NO";
            gridCol_Invoice_No.VisibleIndex = 0;
            gridCol_Invoice_No.Width = 120;

            // REVISE NO
            gridCol_Revise_No.Name = "gridCol_Revise_No";
            gridCol_Revise_No.Caption = "REVISE_NO";
            gridCol_Revise_No.FieldName = "REVISE_NO";
            gridCol_Revise_No.VisibleIndex = 0;
            gridCol_Revise_No.Width = 140;

            // REVISE DATE
            gridCol_Revise_Date.Name = "gridCol_Revise_Date";
            gridCol_Revise_Date.Caption = "REVISE_DATE";
            gridCol_Revise_Date.FieldName = "REVISE_DATE";
            gridCol_Revise_Date.VisibleIndex = 0;
            gridCol_Revise_Date.Width = 120;
            gridCol_Revise_Date.AppearanceCell.Options.UseTextOptions = true;
            gridCol_Revise_Date.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridCol_Revise_Date.DisplayFormat.FormatString = "dd/MM/yyyy";
            gridCol_Revise_Date.DisplayFormat.FormatType = FormatType.DateTime;

            // REVISE VERSION
            gridCol_Revise_Version.Name = "gridCol_Revise_Version";
            gridCol_Revise_Version.Caption = "REVISE_VERSION";
            gridCol_Revise_Version.FieldName = "REVISE_VERSION";
            gridCol_Revise_Version.VisibleIndex = 0;
            gridCol_Revise_Version.Width = 120;

            // PACKAGES NO
            gridCol_Packages_No.Name = "gridCol_Packages_No";
            gridCol_Packages_No.Caption = "PACKAGES_NO";
            gridCol_Packages_No.FieldName = "PACKAGES_NO";
            gridCol_Packages_No.VisibleIndex = 0;
            gridCol_Packages_No.Width = 120;

            // CUSTOMER ITEM CODE
            gridCol_Cus_Item_Code.Name = "gridCol_Cus_Item_Code";
            gridCol_Cus_Item_Code.Caption = "CUS_ITEM_CODE";
            gridCol_Cus_Item_Code.FieldName = "CUS_ITEM_CODE";
            gridCol_Cus_Item_Code.VisibleIndex = 0;
            gridCol_Cus_Item_Code.Width = 140;
            gridCol_Cus_Item_Code.AppearanceCell.Options.UseTextOptions = true;
            gridCol_Cus_Item_Code.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            // TVC ITEM CODE
            gridCol_TVC_Item_Code.Name = "gridCol_TVC_Item_Code";
            gridCol_TVC_Item_Code.Caption = "TVC_ITEM_CODE";
            gridCol_TVC_Item_Code.FieldName = "TVC_ITEM_CODE";
            gridCol_TVC_Item_Code.VisibleIndex = 0;
            gridCol_TVC_Item_Code.Width = 150;

            // CUSTOMER PO
            gridCol_Customer_PO.Name = "gridCol_Customer_PO";
            gridCol_Customer_PO.Caption = "CUSTOMER_PO";
            gridCol_Customer_PO.FieldName = "CUSTOMER_PO";
            gridCol_Customer_PO.VisibleIndex = 0;
            gridCol_Customer_PO.Width = 150;

            // QTY CARTON
            gridCol_Qty_Carton.Name = "gridCol_Qty_Carton";
            gridCol_Qty_Carton.Caption = "QTY_CARTON";
            gridCol_Qty_Carton.FieldName = "QTY_CARTON";
            gridCol_Qty_Carton.VisibleIndex = 0;
            gridCol_Qty_Carton.Width = 180;
            gridCol_Qty_Carton.AppearanceCell.Options.UseTextOptions = true;
            gridCol_Qty_Carton.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridCol_Qty_Carton.DisplayFormat.FormatString = "#,##0";
            gridCol_Qty_Carton.DisplayFormat.FormatType = FormatType.Numeric;

            // QTY PER CARTON
            gridCol_Qty_Per_Carton.Name = "gridCol_Qty_Per_Carton";
            gridCol_Qty_Per_Carton.Caption = "QTY_PER_CARTON";
            gridCol_Qty_Per_Carton.FieldName = "QTY_PER_CARTON";
            gridCol_Qty_Per_Carton.VisibleIndex = 0;
            gridCol_Qty_Per_Carton.Width = 150;
            gridCol_Qty_Per_Carton.AppearanceCell.Options.UseTextOptions = true;
            gridCol_Qty_Per_Carton.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridCol_Qty_Per_Carton.DisplayFormat.FormatString = "#,##0";
            gridCol_Qty_Per_Carton.DisplayFormat.FormatType = FormatType.Numeric;

            // QTY TOTAL
            gridCol_Qty_Total.Name = "gridCol_Qty_Total";
            gridCol_Qty_Total.Caption = "QTY_TOTAL";
            gridCol_Qty_Total.FieldName = "QTY_TOTAL";
            gridCol_Qty_Total.VisibleIndex = 0;
            gridCol_Qty_Total.Width = 150;
            gridCol_Qty_Per_Carton.AppearanceCell.Options.UseTextOptions = true;
            gridCol_Qty_Per_Carton.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridCol_Qty_Per_Carton.DisplayFormat.FormatString = "#,##0";
            gridCol_Qty_Per_Carton.DisplayFormat.FormatType = FormatType.Numeric;

            // QTY TOTAL REVISE
            gridCol_Qty_Revise.Name = "gridCol_Qty_Revise";
            gridCol_Qty_Revise.Caption = "QTY_TOTAL_REVISE";
            gridCol_Qty_Revise.FieldName = "QTY_TOTAL_REVISE";
            gridCol_Qty_Revise.VisibleIndex = 0;
            gridCol_Qty_Revise.Width = 150;
            gridCol_Qty_Revise.AppearanceCell.Options.UseTextOptions = true;
            gridCol_Qty_Revise.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridCol_Qty_Revise.DisplayFormat.FormatString = "#,##0";
            gridCol_Qty_Revise.DisplayFormat.FormatType = FormatType.Numeric;

            // NET WEIGHT
            gridCol_Net_Weight.Name = "gridCol_Net_Weight";
            gridCol_Net_Weight.Caption = "NET_WEIGHT";
            gridCol_Net_Weight.FieldName = "NET_WEIGHT";
            gridCol_Net_Weight.VisibleIndex = 0;
            gridCol_Net_Weight.Width = 150;
            gridCol_Net_Weight.AppearanceCell.Options.UseTextOptions = true;
            gridCol_Net_Weight.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridCol_Net_Weight.DisplayFormat.FormatString = "#,##0";
            gridCol_Net_Weight.DisplayFormat.FormatType = FormatType.Numeric;

            // NET WEIGHT TOTAL
            gridCol_Net_Weight_Total.Name = "gridCol_Net_Weight_Total";
            gridCol_Net_Weight_Total.Caption = "NET_WEIGHT_TOTAL";
            gridCol_Net_Weight_Total.FieldName = "NET_WEIGHT_TOTAL";
            gridCol_Net_Weight_Total.VisibleIndex = 0;
            gridCol_Net_Weight_Total.Width = 150;
            gridCol_Net_Weight_Total.AppearanceCell.Options.UseTextOptions = true;
            gridCol_Net_Weight_Total.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridCol_Net_Weight_Total.DisplayFormat.FormatString = "#,##0";
            gridCol_Net_Weight_Total.DisplayFormat.FormatType = FormatType.Numeric;

            // GROSS WEIGHT
            gridCol_Gross_Weight.Name = "gridCol_Gross_Weight";
            gridCol_Gross_Weight.Caption = "GROSS_WEIGHT";
            gridCol_Gross_Weight.FieldName = "GROSS_WEIGHT";
            gridCol_Gross_Weight.VisibleIndex = 0;
            gridCol_Gross_Weight.Width = 150;
            gridCol_Gross_Weight.AppearanceCell.Options.UseTextOptions = true;
            gridCol_Gross_Weight.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridCol_Gross_Weight.DisplayFormat.FormatString = "#,##0";
            gridCol_Gross_Weight.DisplayFormat.FormatType = FormatType.Numeric;

            // LOT NO
            gridCol_Lot_No.Name = "gridCol_Lot_No";
            gridCol_Lot_No.Caption = "LOT_NO";
            gridCol_Lot_No.FieldName = "LOT_NO";
            gridCol_Lot_No.VisibleIndex = 0;
            gridCol_Lot_No.Width = 150;

            // CREATE BY
            gridCol_Create_By.Name = "gridCol_Lot_No";
            gridCol_Create_By.Caption = "CREATE_BY";
            gridCol_Create_By.FieldName = "CREATE_BY";
            gridCol_Create_By.VisibleIndex = 0;
            gridCol_Create_By.Width = 150;

            // CREATE AT
            gridCol_Create_At.Name = "gridCol_Create_At";
            gridCol_Create_At.Caption = "CREATE_AT";
            gridCol_Create_At.FieldName = "CREATE_AT";
            gridCol_Create_At.VisibleIndex = 0;
            gridCol_Create_At.Width = 150;

            // EDIT BY
            gridCol_Edit_By.Name = "gridCol_Edit_By";
            gridCol_Edit_By.Caption = "EDIT_BY";
            gridCol_Edit_By.FieldName = "EDIT_BY";
            gridCol_Edit_By.VisibleIndex = 0;
            gridCol_Edit_By.Width = 150;

            // EDIT AT
            gridCol_Edit_At.Name = "gridCol_Edit_At";
            gridCol_Edit_At.Caption = "EDIT AT";
            gridCol_Edit_At.FieldName = "EDIT_AT";
            gridCol_Edit_At.VisibleIndex = 0;
            gridCol_Edit_At.Width = 150;

            // Add column to gridview
            View_Import_PackingList.Columns.Add(gridCol_Company_Code);
            View_Import_PackingList.Columns.Add(gridCol_Customer_Code);
            View_Import_PackingList.Columns.Add(gridCol_Shipping_No);
            View_Import_PackingList.Columns.Add(gridCol_Invoice_No);
            View_Import_PackingList.Columns.Add(gridCol_Revise_No);
            View_Import_PackingList.Columns.Add(gridCol_Revise_Date);
            View_Import_PackingList.Columns.Add(gridCol_Revise_Version);
            View_Import_PackingList.Columns.Add(gridCol_Packages_No);
            View_Import_PackingList.Columns.Add(gridCol_Cus_Item_Code);
            View_Import_PackingList.Columns.Add(gridCol_TVC_Item_Code);
            View_Import_PackingList.Columns.Add(gridCol_Customer_PO);
            View_Import_PackingList.Columns.Add(gridCol_Qty_Carton);
            View_Import_PackingList.Columns.Add(gridCol_Qty_Per_Carton);
            View_Import_PackingList.Columns.Add(gridCol_Qty_Total);
            View_Import_PackingList.Columns.Add(gridCol_Qty_Revise);
            View_Import_PackingList.Columns.Add(gridCol_Net_Weight);
            View_Import_PackingList.Columns.Add(gridCol_Net_Weight_Total);
            View_Import_PackingList.Columns.Add(gridCol_Gross_Weight);
            View_Import_PackingList.Columns.Add(gridCol_Lot_No);
            View_Import_PackingList.Columns.Add(gridCol_Create_By);
            View_Import_PackingList.Columns.Add(gridCol_Create_At);
            View_Import_PackingList.Columns.Add(gridCol_Edit_By);
            View_Import_PackingList.Columns.Add(gridCol_Edit_At);

            // Set common attribute
            foreach (GridColumn c in View_Import_PackingList.Columns)
            {
                c.AppearanceHeader.Options.UseFont = true;
                c.AppearanceHeader.Options.UseForeColor = true;
                c.AppearanceHeader.Options.UseTextOptions = true;
                c.AppearanceHeader.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                c.AppearanceHeader.ForeColor = Color.Black;
                c.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                c.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
            }
        }

        private void barBtn_SaveData_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}