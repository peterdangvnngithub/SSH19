using DevExpress.DataAccess.Excel;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using TAKAKO_ERP_3LAYER.DAL;

namespace TAKAKO_ERP_3LAYER
{
    public partial class Form_Shipping_Output_PackingList : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private string shippingNo;
        private SYSTEM_DAL system = new SYSTEM_DAL();

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

        public Form_Shipping_Output_PackingList(SYSTEM_DAL _system, string _shippingNo)
        {
            InitializeComponent();
            system = _system;
            shippingNo = _shippingNo;
        }

        private void Form_Shipping_Output_PackingList_Load(object sender, EventArgs e)
        {
            Define_GridView();

            using (Takako_Entities db = new Takako_Entities())
            {
                var PackingList = db.TVC_SHIPPING_PL_DETAIL.Where(x => x.SHIPPING_NO.Equals(shippingNo)).ToList();
                gridControl_Output_PackingList.DataSource = PackingList;
            }
        }

        private void Define_GridView()
        {
            View_Output_PackingList.OptionsPrint.AutoWidth = false;
            View_Output_PackingList.OptionsView.ColumnAutoWidth = false;

            // COMPANY_CODE
            gridCol_Company_Code.Name = "gridCol_Company_Code";
            gridCol_Company_Code.Caption = "COMPANY_CODE";
            gridCol_Company_Code.FieldName = "COMPANY_CODE";
            gridCol_Company_Code.VisibleIndex = 0;
            gridCol_Company_Code.Width = 120;
            gridCol_Company_Code.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            // CUSTOMER CODE
            gridCol_Customer_Code.Name = "gridCol_Customer_Code";
            gridCol_Customer_Code.Caption = "CUSTOMER_CODE";
            gridCol_Customer_Code.FieldName = "CUSTOMER_CODE";
            gridCol_Customer_Code.VisibleIndex = 0;
            gridCol_Customer_Code.Width = 120;
            gridCol_Customer_Code.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            // SHIPPING NO
            gridCol_Shipping_No.Name = "gridCol_Shipping_No";
            gridCol_Shipping_No.Caption = "SHIPPING_NO";
            gridCol_Shipping_No.FieldName = "SHIPPING_NO";
            gridCol_Shipping_No.VisibleIndex = 0;
            gridCol_Shipping_No.Width = 120;
            gridCol_Shipping_No.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            // INVOICE NO
            gridCol_Invoice_No.Name = "gridCol_Invoice_No";
            gridCol_Invoice_No.Caption = "INVOICE_NO";
            gridCol_Invoice_No.FieldName = "INVOICE_NO";
            gridCol_Invoice_No.VisibleIndex = 0;
            gridCol_Invoice_No.Width = 120;
            gridCol_Invoice_No.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridCol_Invoice_No.Visible = false;

            // REVISE NO
            gridCol_Revise_No.Name = "gridCol_Revise_No";
            gridCol_Revise_No.Caption = "REVISE_NO";
            gridCol_Revise_No.FieldName = "REVISE_NO";
            gridCol_Revise_No.VisibleIndex = 0;
            gridCol_Revise_No.Width = 140;
            gridCol_Revise_No.Visible = false;

            // REVISE DATE
            gridCol_Revise_Date.Name = "gridCol_Revise_Date";
            gridCol_Revise_Date.Caption = "REVISE_DATE";
            gridCol_Revise_Date.FieldName = "REVISE_DATE";
            gridCol_Revise_Date.VisibleIndex = 0;
            gridCol_Revise_Date.Width = 120;
            gridCol_Revise_Date.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridCol_Revise_Date.DisplayFormat.FormatString = "dd/MM/yyyy";
            gridCol_Revise_Date.DisplayFormat.FormatType = FormatType.DateTime;
            gridCol_Revise_Date.Visible = false;

            // REVISE VERSION
            gridCol_Revise_Version.Name = "gridCol_Revise_Version";
            gridCol_Revise_Version.Caption = "REVISE_VERSION";
            gridCol_Revise_Version.FieldName = "REVISE_VERSION";
            gridCol_Revise_Version.VisibleIndex = 0;
            gridCol_Revise_Version.Width = 120;
            gridCol_Revise_Version.Visible = false;

            // PACKAGES NO
            gridCol_Packages_No.Name = "gridCol_Packages_No";
            gridCol_Packages_No.Caption = "PACKAGES_NO";
            gridCol_Packages_No.FieldName = "PACKAGES_NO";
            gridCol_Packages_No.VisibleIndex = 0;
            gridCol_Packages_No.Width = 120;
            gridCol_Revise_Date.DisplayFormat.FormatType = FormatType.None;
            gridCol_Packages_No.AppearanceCell.BackColor = Color.Yellow;
            gridCol_Packages_No.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            // CUSTOMER ITEM CODE
            gridCol_Cus_Item_Code.Name = "gridCol_Cus_Item_Code";
            gridCol_Cus_Item_Code.Caption = "CUS_ITEM_CODE";
            gridCol_Cus_Item_Code.FieldName = "CUS_ITEM_CODE";
            gridCol_Cus_Item_Code.VisibleIndex = 0;
            gridCol_Cus_Item_Code.Width = 140;
            gridCol_Cus_Item_Code.AppearanceCell.BackColor = Color.Yellow;
            gridCol_Cus_Item_Code.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
            gridCol_Cus_Item_Code.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            // TVC ITEM CODE
            gridCol_TVC_Item_Code.Name = "gridCol_TVC_Item_Code";
            gridCol_TVC_Item_Code.Caption = "TVC_ITEM_CODE";
            gridCol_TVC_Item_Code.FieldName = "TVC_ITEM_CODE";
            gridCol_TVC_Item_Code.VisibleIndex = 0;
            gridCol_TVC_Item_Code.Width = 150;
            gridCol_TVC_Item_Code.AppearanceCell.BackColor = Color.Yellow;
            gridCol_TVC_Item_Code.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
            gridCol_TVC_Item_Code.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            // CUSTOMER PO
            gridCol_Customer_PO.Name = "gridCol_Customer_PO";
            gridCol_Customer_PO.Caption = "CUSTOMER_PO";
            gridCol_Customer_PO.FieldName = "CUSTOMER_PO";
            gridCol_Customer_PO.VisibleIndex = 0;
            gridCol_Customer_PO.Width = 140;
            gridCol_Customer_PO.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
            gridCol_Customer_PO.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            // QTY CARTON
            gridCol_Qty_Carton.Name = "gridCol_Qty_Carton";
            gridCol_Qty_Carton.Caption = "QTY_CARTON";
            gridCol_Qty_Carton.FieldName = "QTY_CARTON";
            gridCol_Qty_Carton.VisibleIndex = 0;
            gridCol_Qty_Carton.Width = 120;
            gridCol_Qty_Carton.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridCol_Qty_Carton.DisplayFormat.FormatString = "#,##0";
            gridCol_Qty_Carton.DisplayFormat.FormatType = FormatType.Numeric;
            gridCol_Qty_Carton.AppearanceCell.BackColor = Color.Yellow;

            // QTY PER CARTON
            gridCol_Qty_Per_Carton.Name = "gridCol_Qty_Per_Carton";
            gridCol_Qty_Per_Carton.Caption = "QTY_PER_CARTON";
            gridCol_Qty_Per_Carton.FieldName = "QTY_PER_CARTON";
            gridCol_Qty_Per_Carton.VisibleIndex = 0;
            gridCol_Qty_Per_Carton.Width = 140;
            gridCol_Qty_Per_Carton.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridCol_Qty_Per_Carton.DisplayFormat.FormatString = "#,##0";
            gridCol_Qty_Per_Carton.DisplayFormat.FormatType = FormatType.Numeric;
            gridCol_Qty_Per_Carton.AppearanceCell.BackColor = Color.Yellow;

            // QTY TOTAL
            gridCol_Qty_Total.Name = "gridCol_Qty_Total";
            gridCol_Qty_Total.Caption = "QTY_TOTAL";
            gridCol_Qty_Total.FieldName = "QTY_TOTAL";
            gridCol_Qty_Total.VisibleIndex = 0;
            gridCol_Qty_Total.Width = 120;
            gridCol_Qty_Total.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridCol_Qty_Total.DisplayFormat.FormatString = "#,##0";
            gridCol_Qty_Total.DisplayFormat.FormatType = FormatType.Numeric;
            gridCol_Qty_Total.AppearanceCell.BackColor = Color.Yellow;

            // QTY TOTAL REVISE
            gridCol_Qty_Revise.Name = "gridCol_Qty_Revise";
            gridCol_Qty_Revise.Caption = "QTY_TOTAL_REVISE";
            gridCol_Qty_Revise.FieldName = "QTY_TOTAL_REVISE";
            gridCol_Qty_Revise.VisibleIndex = 0;
            gridCol_Qty_Revise.Width = 140;
            gridCol_Qty_Revise.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridCol_Qty_Revise.DisplayFormat.FormatString = "#,##0";
            gridCol_Qty_Revise.DisplayFormat.FormatType = FormatType.Numeric;
            gridCol_Qty_Revise.Visible = false;

            // NET WEIGHT
            gridCol_Net_Weight.Name = "gridCol_Net_Weight";
            gridCol_Net_Weight.Caption = "NET_WEIGHT";
            gridCol_Net_Weight.FieldName = "NET_WEIGHT";
            gridCol_Net_Weight.VisibleIndex = 0;
            gridCol_Net_Weight.Width = 120;
            gridCol_Net_Weight.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridCol_Net_Weight.DisplayFormat.FormatString = "#,##0";
            gridCol_Net_Weight.DisplayFormat.FormatType = FormatType.Numeric;
            gridCol_Net_Weight.AppearanceCell.BackColor = Color.Yellow;

            // NET WEIGHT TOTAL
            gridCol_Net_Weight_Total.Name = "gridCol_Net_Weight_Total";
            gridCol_Net_Weight_Total.Caption = "NET_WEIGHT_TOTAL";
            gridCol_Net_Weight_Total.FieldName = "NET_WEIGHT_TOTAL";
            gridCol_Net_Weight_Total.VisibleIndex = 0;
            gridCol_Net_Weight_Total.Width = 140;
            gridCol_Net_Weight_Total.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridCol_Net_Weight_Total.DisplayFormat.FormatString = "#,##0";
            gridCol_Net_Weight_Total.DisplayFormat.FormatType = FormatType.Numeric;
            gridCol_Net_Weight_Total.AppearanceCell.BackColor = Color.Yellow;

            // GROSS WEIGHT
            gridCol_Gross_Weight.Name = "gridCol_Gross_Weight";
            gridCol_Gross_Weight.Caption = "GROSS_WEIGHT";
            gridCol_Gross_Weight.FieldName = "GROSS_WEIGHT";
            gridCol_Gross_Weight.VisibleIndex = 0;
            gridCol_Gross_Weight.Width = 120;
            gridCol_Gross_Weight.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridCol_Gross_Weight.DisplayFormat.FormatString = "#,##0";
            gridCol_Gross_Weight.DisplayFormat.FormatType = FormatType.Numeric;
            gridCol_Gross_Weight.AppearanceCell.BackColor = Color.Yellow;

            // LOT NO
            gridCol_Lot_No.Name = "gridCol_Lot_No";
            gridCol_Lot_No.Caption = "LOT_NO";
            gridCol_Lot_No.FieldName = "LOT_NO";
            gridCol_Lot_No.VisibleIndex = 0;
            gridCol_Lot_No.Width = 150;
            gridCol_Lot_No.AppearanceCell.BackColor = Color.Yellow;

            // CREATE BY
            gridCol_Create_By.Name = "gridCol_Lot_No";
            gridCol_Create_By.Caption = "CREATE_BY";
            gridCol_Create_By.FieldName = "CREATE_BY";
            gridCol_Create_By.VisibleIndex = 0;
            gridCol_Create_By.Width = 150;
            gridCol_Create_By.Visible = false;

            // CREATE AT
            gridCol_Create_At.Name = "gridCol_Create_At";
            gridCol_Create_At.Caption = "CREATE_AT";
            gridCol_Create_At.FieldName = "CREATE_AT";
            gridCol_Create_At.VisibleIndex = 0;
            gridCol_Create_At.Width = 150;
            gridCol_Create_At.Visible = false;

            // EDIT BY
            gridCol_Edit_By.Name = "gridCol_Edit_By";
            gridCol_Edit_By.Caption = "EDIT_BY";
            gridCol_Edit_By.FieldName = "EDIT_BY";
            gridCol_Edit_By.VisibleIndex = 0;
            gridCol_Edit_By.Width = 150;
            gridCol_Edit_By.Visible = false;

            // EDIT AT
            gridCol_Edit_At.Name = "gridCol_Edit_At";
            gridCol_Edit_At.Caption = "EDIT_AT";
            gridCol_Edit_At.FieldName = "EDIT_AT";
            gridCol_Edit_At.VisibleIndex = 0;
            gridCol_Edit_At.Width = 150;
            gridCol_Edit_At.Visible = false;

            // Add column to gridview
            View_Output_PackingList.Columns.Add(gridCol_Company_Code);
            View_Output_PackingList.Columns.Add(gridCol_Customer_Code);
            View_Output_PackingList.Columns.Add(gridCol_Shipping_No);
            View_Output_PackingList.Columns.Add(gridCol_Invoice_No);
            View_Output_PackingList.Columns.Add(gridCol_Revise_No);
            View_Output_PackingList.Columns.Add(gridCol_Revise_Date);
            View_Output_PackingList.Columns.Add(gridCol_Revise_Version);
            View_Output_PackingList.Columns.Add(gridCol_Packages_No);
            View_Output_PackingList.Columns.Add(gridCol_Cus_Item_Code);
            View_Output_PackingList.Columns.Add(gridCol_TVC_Item_Code);
            View_Output_PackingList.Columns.Add(gridCol_Customer_PO);
            View_Output_PackingList.Columns.Add(gridCol_Qty_Carton);
            View_Output_PackingList.Columns.Add(gridCol_Qty_Per_Carton);
            View_Output_PackingList.Columns.Add(gridCol_Qty_Total);
            View_Output_PackingList.Columns.Add(gridCol_Qty_Revise);
            View_Output_PackingList.Columns.Add(gridCol_Net_Weight);
            View_Output_PackingList.Columns.Add(gridCol_Net_Weight_Total);
            View_Output_PackingList.Columns.Add(gridCol_Gross_Weight);
            View_Output_PackingList.Columns.Add(gridCol_Lot_No);
            View_Output_PackingList.Columns.Add(gridCol_Create_By);
            View_Output_PackingList.Columns.Add(gridCol_Create_At);
            View_Output_PackingList.Columns.Add(gridCol_Edit_By);
            View_Output_PackingList.Columns.Add(gridCol_Edit_At);

            // Set common attribute
            foreach (GridColumn c in View_Output_PackingList.Columns)
            {
                c.AppearanceHeader.Options.UseFont = true;
                c.AppearanceHeader.Options.UseForeColor = true;
                c.AppearanceHeader.Options.UseTextOptions = true;
                c.AppearanceHeader.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                c.AppearanceHeader.ForeColor = Color.Black;
                c.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                c.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                c.AppearanceCell.Options.UseTextOptions = true;
                c.AppearanceCell.Options.UseBackColor = true;
            }
        }

        private void barButton_Output_Data_Excel_ItemClick(object sender, ItemClickEventArgs e)
        {
            string get_date_str = DateTime.Now.ToString("ddMMyyyyHHmmss", CultureInfo.InvariantCulture);

            if ((MessageBox.Show($"Xuất dữ liệu PackingList của Shipping: \"{shippingNo}\" ?"
                , "Xác Nhận"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Question
                , MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Title = "Save PackingList Data",
                    FileName = $"{shippingNo}_" + get_date_str,
                    Filter = "Files Excel|*.xlsx;*.xls"
                };
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = saveDialog.FileName;
                    gridControl_Output_PackingList.ExportToXlsx(path);

                    // Open the created XLSX file with the default application.
                    Process.Start(path);
                }
            }
        }

        private void barButton_Import_Data_PackingList_ItemClick(object sender, ItemClickEventArgs e)
        {
            string get_date_str = DateTime.Now.ToString("ddMMyyyyHHmmss", CultureInfo.InvariantCulture);

            // Clear data header
            gridControl_Output_PackingList.DataSource = null;

            //Get link file excel
            OpenFileDialog theDialog = new OpenFileDialog
            {
                Title = "Chọn file PackingList cần import",
                Filter = "Files Excel|*.xls;*.xlsx"
            };
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                // Create a new Excel data source.
                ExcelDataSource excelDataSource = new ExcelDataSource
                {
                    FileName = theDialog.FileName
                };

                // Select a required worksheet.
                ExcelWorksheetSettings excelWorksheetSettings = new ExcelWorksheetSettings
                {
                    WorksheetIndex = 0
                };

                // Specify import settings.
                ExcelSourceOptions excelSourceOptions = new ExcelSourceOptions
                {
                    ImportSettings = excelWorksheetSettings,
                    SkipHiddenRows = false,
                    SkipHiddenColumns = false,
                    UseFirstRowAsHeader = true
                };
                excelDataSource.SourceOptions = excelSourceOptions;

                excelDataSource.Fill();

                DataTable tableEmployee = new DataTable();
                tableEmployee.Columns.Add("COMPANY_CODE"        , typeof(string));
                tableEmployee.Columns.Add("CUSTOMER_CODE"       , typeof(string));
                tableEmployee.Columns.Add("SHIPPING_NO"         , typeof(string));
                tableEmployee.Columns.Add("INVOICE_NO"          , typeof(string));
                tableEmployee.Columns.Add("REVISE_NO"           , typeof(string));
                tableEmployee.Columns.Add("REVISE_DATE"         , typeof(DateTime));
                tableEmployee.Columns.Add("REVISE_VERSION"      , typeof(int));
                tableEmployee.Columns.Add("PACKAGES_NO"         , typeof(string));
                tableEmployee.Columns.Add("CUS_ITEM_CODE"       , typeof(string));
                tableEmployee.Columns.Add("TVC_ITEM_CODE"       , typeof(string));
                tableEmployee.Columns.Add("CUSTOMER_PO"         , typeof(string));
                tableEmployee.Columns.Add("QTY_CARTON"          , typeof(decimal));
                tableEmployee.Columns.Add("QTY_PER_CARTON"      , typeof(decimal));
                tableEmployee.Columns.Add("QTY_TOTAL"           , typeof(decimal));
                tableEmployee.Columns.Add("QTY_TOTAL_REVISE"    , typeof(decimal));
                tableEmployee.Columns.Add("NET_WEIGHT"          , typeof(decimal));
                tableEmployee.Columns.Add("NET_WEIGHT_TOTAL"    , typeof(decimal));
                tableEmployee.Columns.Add("GROSS_WEIGHT"        , typeof(decimal));
                tableEmployee.Columns.Add("LOT_NO"              , typeof(string));
                tableEmployee.Columns.Add("CREATE_BY"           , typeof(string));
                tableEmployee.Columns.Add("CREATE_AT"           , typeof(DateTime));
                tableEmployee.Columns.Add("EDIT_BY"             , typeof(string));
                tableEmployee.Columns.Add("EDIT_AT"             , typeof(DateTime));

                tableEmployee = excelDataSource.ExcelToDataTable();

                using (Takako_Entities db = new Takako_Entities())
                {
                    List<TVC_SHIPPING_PL_DETAIL> pl = db.TVC_SHIPPING_PL_DETAIL.Where(x => x.SHIPPING_NO.Equals(shippingNo)).ToList();

                    foreach(TVC_SHIPPING_PL_DETAIL item in pl)
                    {
                        db.TVC_SHIPPING_PL_DETAIL.Remove(item);
                    }

                    //Excute
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Loại lỗi: {ex.GetType()}.\nLỗi: {ex}", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error); // What is the real exception?
                    }

                    foreach (DataRow row in tableEmployee.Rows)
                    {
                        string _companyCode = Convert.ToString(row["COMPANY_CODE"]);
                        string _shippingNo = Convert.ToString(row["SHIPPING_NO"]);
                        string _packagesNo = Convert.ToString(row["PACKAGES_NO"]);
                        string _cusItemCode = Convert.ToString(row["CUS_ITEM_CODE"]);
                        string _customerPO = Convert.ToString(row["CUSTOMER_PO"]);
                        decimal _qtyTotal = Convert.ToDecimal(row["QTY_TOTAL"]);
                        string _lotNo = Convert.ToString(row["LOT_NO"]);

                        //Otherwise add new
                        TVC_SHIPPING_PL_DETAIL newPL = new TVC_SHIPPING_PL_DETAIL
                        {
                            COMPANY_CODE        = _companyCode,
                            CUSTOMER_CODE       = Convert.ToString(row["CUSTOMER_CODE"]),
                            SHIPPING_NO         = _shippingNo,
                            INVOICE_NO          = "",
                            REVISE_NO           = _shippingNo,
                            REVISE_DATE         = null,
                            REVISE_VERSION      = 1,
                            PACKAGES_NO         = _packagesNo,
                            CUS_ITEM_CODE       = _cusItemCode,
                            TVC_ITEM_CODE       = Convert.ToString(row["TVC_ITEM_CODE"]),
                            CUSTOMER_PO         = _customerPO,
                            QTY_CARTON          = row["QTY_CARTON"] == null ? 0 : Convert.ToDecimal(row["QTY_CARTON"]),
                            QTY_PER_CARTON      = row["QTY_PER_CARTON"] == null ? 0 : Convert.ToDecimal(row["QTY_PER_CARTON"]),
                            QTY_TOTAL           = row["QTY_TOTAL"] == null ? 0 : Convert.ToDecimal(row["QTY_TOTAL"]),
                            QTY_TOTAL_REVISE    = 0,
                            NET_WEIGHT          = row["NET_WEIGHT"] == null ? 0 : Convert.ToDecimal(row["NET_WEIGHT"]),
                            NET_WEIGHT_TOTAL    = row["NET_WEIGHT_TOTAL"] == null ? 0 : Convert.ToDecimal(row["NET_WEIGHT_TOTAL"]),
                            GROSS_WEIGHT        = row["GROSS_WEIGHT"] == null ? 0 : Convert.ToDecimal(row["GROSS_WEIGHT"]),
                            LOT_NO              = Convert.ToString(row["LOT_NO"]),
                            CREATE_BY           = Convert.ToString(system.UserName),
                            CREATE_AT           = DateTime.Now,
                            EDIT_BY             = "",
                            EDIT_AT             = null
                        };
                        db.TVC_SHIPPING_PL_DETAIL.Add(newPL);

                        //Excute
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Loại lỗi: {ex.GetType()}.\nLỗi: {ex}", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error); // What is the real exception?
                        }
                    }
                }

                MessageBox.Show($"Import Packing List cho {shippingNo} thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void barButton_ClearData_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridControl_Output_PackingList.DataSource = null;
        }
    }
}