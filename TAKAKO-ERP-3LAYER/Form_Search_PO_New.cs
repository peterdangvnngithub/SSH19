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
using TAKAKO_ERP_3LAYER.DAL;

namespace TAKAKO_ERP_3LAYER
{
    public partial class Form_Search_PO_New : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        SYSTEM_DAL systemDAL;
        string companyCode;
        string unitCurrency;
        DateTime dateCreateInvoice;

        private GridColumn gridCol_Company_Code = new GridColumn();
        private GridColumn gridCol_Customer_Code = new GridColumn();
        private GridColumn gridCol_ReceiveNo = new GridColumn();
        private GridColumn gridCol_Cus_Item_Code = new GridColumn();
        private GridColumn gridCol_TVC_Item_Code = new GridColumn();        
        private GridColumn gridCol_Part_Name = new GridColumn();
        private GridColumn gridCol_Item_Name = new GridColumn();
        private GridColumn gridCol_DueDate = new GridColumn();
        private GridColumn gridCol_OrderDate = new GridColumn();
        private GridColumn gridCol_Unit_Currency = new GridColumn();
        private GridColumn gridCol_Customer_PO = new GridColumn();
        private GridColumn gridCol_Qty_Balance_Qty = new GridColumn();
        private GridColumn gridCol_Order_Price = new GridColumn();
        private GridColumn gridCol_Note = new GridColumn();        

        public Form_Search_PO_New(SYSTEM_DAL _systemDAL,string _CompanyCode, string _unitCurrency, DateTime _dateCreateInvoice)
        {
            InitializeComponent();

            systemDAL = _systemDAL;
            companyCode = _CompanyCode;
            unitCurrency = _unitCurrency;
            dateCreateInvoice = _dateCreateInvoice;
        }

        private void Form_Import_PO_To_Shipping_Excel_Load(object sender, EventArgs e)
        {
            Define_GridView();

            gridControl_Search_PO.DataSource = GetData();
        }

        private List<TVC_PO_MS> GetData()
        {
            List<TVC_PO_MS> result = new List<TVC_PO_MS>();
            using(Takako_Entities db = new Takako_Entities())
            {
                result = db.TVC_PO_MS.Where(x => (x.COMPANY_CODE.Equals(companyCode) 
                                               && x.UNIT_CURRENCY.Equals(unitCurrency))).ToList();
            }    
            return result;
        }

        private void Define_GridView()
        {
            View_PO_List.OptionsPrint.AutoWidth = false;
            View_PO_List.OptionsView.ColumnAutoWidth = false;

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

            // RECEIVE NO
            gridCol_ReceiveNo.Name = "gridCol_ReceiveNo";
            gridCol_ReceiveNo.Caption = "RECEIVE NO";
            gridCol_ReceiveNo.FieldName = "RECEIVE_NO";
            gridCol_ReceiveNo.VisibleIndex = 0;
            gridCol_ReceiveNo.Width = 120;
            gridCol_ReceiveNo.AppearanceCell.Options.UseTextOptions = true;
            gridCol_ReceiveNo.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

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

            // PART NAME
            gridCol_Part_Name.Name = "gridCol_Part_Name";
            gridCol_Part_Name.Caption = "ITEM NAME";
            gridCol_Part_Name.FieldName = "PARTS_NAME";
            gridCol_Part_Name.VisibleIndex = 0;
            gridCol_Part_Name.Width = 180;
            gridCol_Part_Name.AppearanceCell.Options.UseTextOptions = true;
            gridCol_Part_Name.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            // DUE DATE
            gridCol_DueDate.Name = "gridCol_DueDate";
            gridCol_DueDate.Caption = "DUE DATE";
            gridCol_DueDate.FieldName = "DUE_DATE";
            gridCol_DueDate.VisibleIndex = 0;
            gridCol_DueDate.Width = 150;
            gridCol_DueDate.AppearanceCell.Options.UseTextOptions = true;
            gridCol_DueDate.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridCol_DueDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            gridCol_DueDate.DisplayFormat.FormatType = FormatType.DateTime;

            // ORDER DATE
            gridCol_OrderDate.Name = "gridCol_Qty_Total";
            gridCol_OrderDate.Caption = "QTY_TOTAL";
            gridCol_OrderDate.FieldName = "QTY_TOTAL";
            gridCol_OrderDate.VisibleIndex = 0;
            gridCol_OrderDate.Width = 150;
            gridCol_OrderDate.AppearanceCell.Options.UseTextOptions = true;
            gridCol_OrderDate.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridCol_OrderDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            gridCol_OrderDate.DisplayFormat.FormatType = FormatType.DateTime;

            // UNIT CURRENCY
            gridCol_Unit_Currency.Name = "gridCol_Unit_Currency";
            gridCol_Unit_Currency.Caption = "UNIT CURRENTCY";
            gridCol_Unit_Currency.FieldName = "UNIT_CURRENCY";
            gridCol_Unit_Currency.VisibleIndex = 0;
            gridCol_Unit_Currency.Width = 150;
            gridCol_Unit_Currency.AppearanceCell.Options.UseTextOptions = true;
            gridCol_Unit_Currency.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            // QUANTITY BALANCE
            gridCol_Qty_Balance_Qty.Name = "gridCol_Qty_Balance_Qty";
            gridCol_Qty_Balance_Qty.Caption = "QUANTITY BALANCE";
            gridCol_Qty_Balance_Qty.FieldName = "BALANCE";
            gridCol_Qty_Balance_Qty.VisibleIndex = 0;
            gridCol_Qty_Balance_Qty.Width = 150;
            gridCol_Qty_Balance_Qty.AppearanceCell.Options.UseTextOptions = true;
            gridCol_Qty_Balance_Qty.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridCol_Qty_Balance_Qty.DisplayFormat.FormatString = "#,##0";
            gridCol_Qty_Balance_Qty.DisplayFormat.FormatType = FormatType.Numeric;

            // ORDER PRICE
            gridCol_Order_Price.Name = "gridCol_Order_Price";
            gridCol_Order_Price.Caption = "NET_WEIGHT_TOTAL";
            gridCol_Order_Price.FieldName = "NET_WEIGHT_TOTAL";
            gridCol_Order_Price.VisibleIndex = 0;
            gridCol_Order_Price.Width = 150;
            gridCol_Order_Price.AppearanceCell.Options.UseTextOptions = true;
            gridCol_Order_Price.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gridCol_Order_Price.DisplayFormat.FormatString = "#,##0";
            gridCol_Order_Price.DisplayFormat.FormatType = FormatType.Numeric;

            // NOTE
            gridCol_Note.Name = "gridCol_Note";
            gridCol_Note.Caption = "NOTE";
            gridCol_Note.FieldName = "NOTE";
            gridCol_Note.VisibleIndex = 0;
            gridCol_Note.Width = 150;

            // Add column to gridview
            View_PO_List.Columns.Add(gridCol_Company_Code);
            View_PO_List.Columns.Add(gridCol_Customer_Code);
            View_PO_List.Columns.Add(gridCol_ReceiveNo);
            View_PO_List.Columns.Add(gridCol_Cus_Item_Code);
            View_PO_List.Columns.Add(gridCol_TVC_Item_Code);
            View_PO_List.Columns.Add(gridCol_Part_Name);
            View_PO_List.Columns.Add(gridCol_Item_Name);
            View_PO_List.Columns.Add(gridCol_DueDate);
            View_PO_List.Columns.Add(gridCol_OrderDate);
            View_PO_List.Columns.Add(gridCol_Unit_Currency);
            View_PO_List.Columns.Add(gridCol_Customer_PO);
            View_PO_List.Columns.Add(gridCol_Qty_Balance_Qty);
            View_PO_List.Columns.Add(gridCol_Order_Price);
            View_PO_List.Columns.Add(gridCol_Note); 

            // Set common attribute
            foreach (GridColumn c in View_PO_List.Columns)
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

        private void barBtn_ImportData_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}