using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using TAKAKO_ERP_3LAYER.DAL;
using TAKAKO_ERP_3LAYER.DAO;
using static TAKAKO_ERP_3LAYER.Model;
using static TAKAKO_ERP_3LAYER.Common;


namespace TAKAKO_ERP_3LAYER
{
    public partial class Form_Shipping_Instruction : Form
    {

        public SHIPPING_DAO _shippingDAO;

        public SEARCH_DAO _searchDAO;

        public SYSTEM_DAL _systemDAL;

        public LOG_DAO _logDAO;

        //DataGridViewRow lastSelected;

        DataTable Header_Data = new DataTable();

        DataTable Grid_Invoice = new DataTable();

        DataTable Grid_PackingList = new DataTable();

        string createBy = "";

        public enum EnumRevise
        {
             Normal = 0
            ,Lock = 1
            ,Revise
        };

        public Form_Shipping_Instruction(SYSTEM_DAL _formMainSystemDAL)
        {
            InitializeComponent();

            //Setting no title but have border
            this.ControlBox = false;
            this.Text = String.Empty;

            _systemDAL = _formMainSystemDAL;
        }

        private void Form_Shipping_Instruction_Load(object sender, EventArgs e)
        {
            _shippingDAO = new SHIPPING_DAO();

            _searchDAO = new SEARCH_DAO();

            _logDAO = new LOG_DAO();

            //Setting Init combobox Freight
            SetInit_CboxFreight();
            cb_Freight.SelectedValue = 99;

            //Define column Gridview
            Define_GridView(gridView_Invoice);
            Define_GridView(gridView_PackingList);
            Define_SearchLookUp_View(sLookUp_ShippingNo);
            Define_SearchLookUp_View(sLookUp_IssuedTo_CompanyCode);
            Define_SearchLookUp_View(sLookUp_ShipTo_CompanyCode);
            Define_SearchLookUp_View(sLookUp_PortLoading);
            Define_SearchLookUp_View(sLookUp_PortDestination);
            Define_SearchLookUp_View(sLookUp_PriceCondition);

            //Setting init date Renueve
            dateEdit_Revenue.EditValue = DateTime.Now;

            //Define combobox Company
            SetInit_CbCompanyCode();           

            if (_systemDAL.CompanyCode == "00001")
            {
                cb_CompanyCode.SelectedValue = "00001";
            }
            else if (_systemDAL.CompanyCode == "00002")
            {
                cb_CompanyCode.SelectedValue = "00002";
            }
            else
            {
                cb_CompanyCode.SelectedValue = "00000";
            }

            //
            AddColumnDataSet();

            // Setting init value sLookUpEdit
            GetInfo_Shipping();
            GetInfo_Customer(sLookUp_IssuedTo_CompanyCode);
            GetInfo_Customer(sLookUp_ShipTo_CompanyCode);
            GetInfoDestination(sLookUp_PortLoading);
            GetInfoDestination(sLookUp_PortDestination);
            GetInfoPriceCondition(sLookUp_PriceCondition);
            GetInfoPaymentTerm(sLookUp_PaymentTerm);

            //Setting enable item
            //SettingInitGridView();
        }

        #region Setting Init
        public void Define_SearchLookUp_View(SearchLookUpEdit _sLookUpItem)
        {
            if (_sLookUpItem.Name.Equals(sLookUp_ShippingNo.Name))
            {
                GridColumn gridCol_CustomerCode = new GridColumn();
                GridColumn gridCol_ShipTo = new GridColumn();
                GridColumn gridCol_UnitCurrency = new GridColumn();
                GridColumn gridCol_Amount = new GridColumn();
                GridColumn gridCol_DateCreate = new GridColumn();
                GridColumn gridCol_DateETD = new GridColumn();
                GridColumn gridCol_ShippingNo = new GridColumn();
                GridColumn gridCol_InvoiceNo = new GridColumn();
                GridColumn gridCol_LockStatus = new GridColumn();

                // CUSTOMER CODE
                // HEADER
                gridCol_CustomerCode.Name = "gridCol_CustomerCode";
                gridCol_CustomerCode.Caption = "ISSUEDTO CUSTOMER CODE";
                gridCol_CustomerCode.FieldName = "ISSUEDTO_CUSTOMER_CODE";
                gridCol_CustomerCode.VisibleIndex = 0;
                gridCol_CustomerCode.Width = 100;
                // CELL
                gridCol_CustomerCode.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                gridCol_CustomerCode.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                // SHIP TO
                // HEADER
                gridCol_ShipTo.Name = "gridCol_ShipTo";
                gridCol_ShipTo.Caption = "SHIP TO";
                gridCol_ShipTo.FieldName = "SHIPTO_CUSTOMER_CODE";
                gridCol_ShipTo.VisibleIndex = 0;
                gridCol_ShipTo.Width = 100;
                // CELL
                gridCol_ShipTo.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                gridCol_ShipTo.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                // SHIPPING NO
                // HEADER
                gridCol_ShippingNo.Name = "gridCol_ShippingNo";
                gridCol_ShippingNo.Caption = "SHIPPING NO";
                gridCol_ShippingNo.FieldName = "SHIPPING_NO";
                gridCol_ShippingNo.VisibleIndex = 0;
                gridCol_ShippingNo.Width = 160;
                // CELL
                gridCol_ShippingNo.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                gridCol_ShippingNo.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                //INVOICE NO
                // HEADER
                gridCol_InvoiceNo.Name = "gridCol_InvoiceNo";
                gridCol_InvoiceNo.Caption = "INVOICE NO";
                gridCol_InvoiceNo.FieldName = "INVOICE_NO";
                gridCol_InvoiceNo.VisibleIndex = 0;
                gridCol_InvoiceNo.Width = 140;
                // CELL
                gridCol_InvoiceNo.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                gridCol_InvoiceNo.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                //LOCK STATUS
                // HEADER
                gridCol_LockStatus.Name = "gridCol_LockStatus";
                gridCol_LockStatus.Caption = "LOCK STATUS";
                gridCol_LockStatus.FieldName = "LOCK_STATUS";
                gridCol_LockStatus.VisibleIndex = 0;
                gridCol_LockStatus.Width = 100;
                // CELL
                gridCol_LockStatus.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                gridCol_LockStatus.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                //UNIT CURRENCY
                // HEADER
                gridCol_UnitCurrency.Name = "gridCol_UnitCurrency";
                gridCol_UnitCurrency.Caption = "UNIT CURRENCY";
                gridCol_UnitCurrency.FieldName = "UNIT_CURRENCY";
                gridCol_UnitCurrency.VisibleIndex = 0;
                gridCol_UnitCurrency.Width = 90;
                // CELL
                gridCol_UnitCurrency.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                gridCol_UnitCurrency.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                // ETD
                // HEADER
                gridCol_DateETD.Name = "gridCol_DateETD";
                gridCol_DateETD.Caption = "ETD";
                gridCol_DateETD.FieldName = "ETD";
                gridCol_DateETD.VisibleIndex = 0;
                gridCol_DateETD.Width = 100;
                gridCol_DateETD.DisplayFormat.FormatString = "dd/MM/yyyy";
                gridCol_DateETD.DisplayFormat.FormatType = FormatType.DateTime;
                // CELL
                gridCol_DateETD.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                //DATE CREATE
                // HEADER
                gridCol_DateCreate.Name = "gridCol_DateCreate";
                gridCol_DateCreate.Caption = "DATE CREATE";
                gridCol_DateCreate.FieldName = "DATE_CREATE";
                gridCol_DateCreate.VisibleIndex = 0;
                gridCol_DateCreate.Width = 100;
                gridCol_DateCreate.DisplayFormat.FormatString = "dd/MM/yyyy";
                gridCol_DateCreate.DisplayFormat.FormatType = FormatType.DateTime;
                // CELL
                gridCol_DateCreate.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                //-----AMOUNT-----//
                // HEADER
                gridCol_Amount.Name = "gridCol_Amount";
                gridCol_Amount.Caption = "AMOUNT";
                gridCol_Amount.FieldName = "AMOUNT";
                gridCol_Amount.VisibleIndex = 0;
                gridCol_Amount.Width = 100;
                gridCol_Amount.DisplayFormat.FormatString = "#,##0.00##";
                gridCol_Amount.DisplayFormat.FormatType = FormatType.Numeric;
                // CELL
                gridCol_Amount.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

                sLookUp_ShippingNo_View.Columns.Add(gridCol_CustomerCode);
                sLookUp_ShippingNo_View.Columns.Add(gridCol_ShipTo);
                sLookUp_ShippingNo_View.Columns.Add(gridCol_ShippingNo);
                sLookUp_ShippingNo_View.Columns.Add(gridCol_InvoiceNo);
                sLookUp_ShippingNo_View.Columns.Add(gridCol_LockStatus);
                sLookUp_ShippingNo_View.Columns.Add(gridCol_UnitCurrency);
                sLookUp_ShippingNo_View.Columns.Add(gridCol_DateETD);
                sLookUp_ShippingNo_View.Columns.Add(gridCol_DateCreate);
                sLookUp_ShippingNo_View.Columns.Add(gridCol_Amount);

                sLookUp_ShippingNo.Properties.PopupFormSize = new Size(998, 0);

                sLookUp_ShippingNo_View.OptionsView.ColumnAutoWidth = false;

                // SET COMMON ATTRIBUTE
                foreach (GridColumn c in sLookUp_ShippingNo_View.Columns)
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
            else if (_sLookUpItem.Name.Equals(sLookUp_IssuedTo_CompanyCode.Name))
            {
                GridColumn gridCol_CustomerCode = new GridColumn();
                GridColumn gridCol_CustomerName1 = new GridColumn();
                GridColumn gridCol_Address = new GridColumn();
                GridColumn gridCol_TelNo = new GridColumn();
                GridColumn gridCol_FaxNo = new GridColumn();
                GridColumn gridCol_InvoiceFormat = new GridColumn();

                // CUSTOMER CODE
                // HEADER
                gridCol_CustomerCode.Name = "gridCol_CustomerCode";
                gridCol_CustomerCode.Caption = "ISSUED TO";
                gridCol_CustomerCode.FieldName = "CUSTOMER_CODE";
                gridCol_CustomerCode.VisibleIndex = 0;
                gridCol_CustomerCode.Width = 100;
                // CELL
                gridCol_CustomerCode.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                gridCol_CustomerCode.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                // CUSTOMER_NAME1
                // HEADER
                gridCol_CustomerName1.Name = "gridCol_CustomerName1";
                gridCol_CustomerName1.Caption = "CUSTOMER NAME";
                gridCol_CustomerName1.FieldName = "CUSTOMER_NAME1";
                gridCol_CustomerName1.VisibleIndex = 0;
                gridCol_CustomerName1.Width = 120;
                // CELL
                gridCol_CustomerName1.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                gridCol_CustomerName1.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                // ADDRESS
                // HEADER
                gridCol_Address.Name = "gridCol_Address";
                gridCol_Address.Caption = "ADDRESS";
                gridCol_Address.FieldName = "ADDRESS";
                gridCol_Address.VisibleIndex = 0;
                gridCol_Address.Width = 150;
                // CELL
                gridCol_Address.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                gridCol_Address.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                // TEL NUMBER
                // HEADER
                gridCol_TelNo.Name = "gridCol_TelNo";
                gridCol_TelNo.Caption = "TEL NO";
                gridCol_TelNo.FieldName = "TEL_NO";
                gridCol_TelNo.VisibleIndex = 0;
                gridCol_TelNo.Width = 100;
                // CELL
                gridCol_TelNo.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                gridCol_TelNo.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                // FAX NUMBER
                // HEADER
                gridCol_FaxNo.Name = "gridCol_FaxNo";
                gridCol_FaxNo.Caption = "FAX NO";
                gridCol_FaxNo.FieldName = "FAX_NO";
                gridCol_FaxNo.VisibleIndex = 0;
                gridCol_FaxNo.Width = 90;
                // CELL
                gridCol_FaxNo.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                gridCol_FaxNo.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                // INVOICE FORMAT
                // HEADER
                gridCol_InvoiceFormat.Name = "gridCol_InvoiceFormat";
                gridCol_InvoiceFormat.Caption = "INVOICE FORMAT";
                gridCol_InvoiceFormat.FieldName = "INVOICE_FORMAT";
                gridCol_InvoiceFormat.VisibleIndex = 0;
                gridCol_InvoiceFormat.Width = 100;
                // CELL
                gridCol_FaxNo.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                gridCol_InvoiceFormat.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                sLookUp_IssuedTo_CompanyCode_View.Columns.Add(gridCol_CustomerCode);
                sLookUp_IssuedTo_CompanyCode_View.Columns.Add(gridCol_CustomerName1);
                sLookUp_IssuedTo_CompanyCode_View.Columns.Add(gridCol_Address);
                sLookUp_IssuedTo_CompanyCode_View.Columns.Add(gridCol_TelNo);
                sLookUp_IssuedTo_CompanyCode_View.Columns.Add(gridCol_FaxNo);
                sLookUp_IssuedTo_CompanyCode_View.Columns.Add(gridCol_InvoiceFormat);

                sLookUp_IssuedTo_CompanyCode.Properties.PopupFormSize = new Size(720, 0);

                sLookUp_IssuedTo_CompanyCode_View.OptionsView.ColumnAutoWidth = false;

                // SET COMMON ATTRIBUTE
                foreach (GridColumn c in sLookUp_IssuedTo_CompanyCode_View.Columns)
                {
                    c.AppearanceHeader.Options.UseFont = true;
                    c.AppearanceHeader.Options.UseForeColor = true;
                    c.AppearanceHeader.Options.UseTextOptions = true;
                    c.AppearanceHeader.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    c.AppearanceHeader.ForeColor = Color.Black;
                    c.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                    c.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    c.AppearanceCell.Options.UseTextOptions = true;
                    c.AppearanceCell.Options.UseBackColor = true;
                }
            }
            else if (_sLookUpItem.Name.Equals(sLookUp_ShipTo_CompanyCode.Name))
            {
                sLookUp_ShipTo_CompanyCode.Properties.PopupFormSize = new Size(720, 0);

                GridColumn gridCol_CustomerCode = new GridColumn();
                GridColumn gridCol_CustomerName1 = new GridColumn();
                GridColumn gridCol_Address = new GridColumn();
                GridColumn gridCol_TelNo = new GridColumn();
                GridColumn gridCol_FaxNo = new GridColumn();

                // CUSTOMER CODE
                // HEADER
                gridCol_CustomerCode.Name = "gridCol_CustomerCode";
                gridCol_CustomerCode.Caption = "SHIP TO";
                gridCol_CustomerCode.FieldName = "CUSTOMER_CODE";
                gridCol_CustomerCode.VisibleIndex = 0;
                gridCol_CustomerCode.Width = 100;
                // CELL
                gridCol_CustomerCode.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                gridCol_CustomerCode.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                // CUSTOMER_NAME1
                // HEADER
                gridCol_CustomerName1.Name = "gridCol_CustomerName1";
                gridCol_CustomerName1.Caption = "CUSTOMER NAME";
                gridCol_CustomerName1.FieldName = "CUSTOMER_NAME1";
                gridCol_CustomerName1.VisibleIndex = 0;
                gridCol_CustomerName1.Width = 120;
                // CELL
                gridCol_CustomerName1.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                gridCol_CustomerName1.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                // ADDRESS
                // HEADER
                gridCol_Address.Name = "gridCol_Address";
                gridCol_Address.Caption = "ADDRESS";
                gridCol_Address.FieldName = "ADDRESS";
                gridCol_Address.VisibleIndex = 0;
                gridCol_Address.Width = 150;
                // CELL
                gridCol_Address.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                gridCol_Address.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                // TEL NUMBER
                // HEADER
                gridCol_TelNo.Name = "gridCol_TelNo";
                gridCol_TelNo.Caption = "TEL NO";
                gridCol_TelNo.FieldName = "TEL_NO";
                gridCol_TelNo.VisibleIndex = 0;
                gridCol_TelNo.Width = 100;
                // CELL
                gridCol_TelNo.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                gridCol_TelNo.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                // FAX NUMBER
                // HEADER
                gridCol_FaxNo.Name = "gridCol_FaxNo";
                gridCol_FaxNo.Caption = "FAX NO";
                gridCol_FaxNo.FieldName = "FAX_NO";
                gridCol_FaxNo.VisibleIndex = 0;
                gridCol_FaxNo.Width = 90;
                // CELL
                gridCol_FaxNo.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                gridCol_FaxNo.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                
                // Add column
                sLookUp_ShipTo_CompanyCode_View.Columns.Add(gridCol_CustomerCode);
                sLookUp_ShipTo_CompanyCode_View.Columns.Add(gridCol_CustomerName1);
                sLookUp_ShipTo_CompanyCode_View.Columns.Add(gridCol_Address);
                sLookUp_ShipTo_CompanyCode_View.Columns.Add(gridCol_TelNo);
                sLookUp_ShipTo_CompanyCode_View.Columns.Add(gridCol_FaxNo);

                sLookUp_ShipTo_CompanyCode_View.OptionsView.ColumnAutoWidth = false;

                // SET COMMON ATTRIBUTE
                foreach (GridColumn c in sLookUp_ShipTo_CompanyCode_View.Columns)
                {
                    c.AppearanceHeader.Options.UseFont = true;
                    c.AppearanceHeader.Options.UseForeColor = true;
                    c.AppearanceHeader.Options.UseTextOptions = true;
                    c.AppearanceHeader.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    c.AppearanceHeader.ForeColor = Color.Black;
                    c.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                    c.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    c.AppearanceCell.Options.UseTextOptions = true;
                    c.AppearanceCell.Options.UseBackColor = true;
                }
            }
            else if (_sLookUpItem.Name.Equals(sLookUp_PortLoading.Name))
            {
                sLookUp_PortLoading.Properties.PopupFormSize = new Size(360, 0);

                GridColumn gridCol_Destination = new GridColumn();

                // DESTINATION ID
                // HEADER
                gridCol_Destination.Name = "gridCol_Destination";
                gridCol_Destination.Caption = "PORT OF LOADING";
                gridCol_Destination.FieldName = "DESTINATION_ID";
                gridCol_Destination.VisibleIndex = 0;
                gridCol_Destination.Width = 250;
                // CELL
                gridCol_Destination.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;

                // Add column
                sLookUp_PortLoading_View.Columns.Add(gridCol_Destination);

                sLookUp_PortLoading_View.OptionsView.ColumnAutoWidth = false;

                // SET COMMON ATTRIBUTE
                foreach (GridColumn c in sLookUp_PortLoading_View.Columns)
                {
                    c.AppearanceHeader.Options.UseFont = true;
                    c.AppearanceHeader.Options.UseForeColor = true;
                    c.AppearanceHeader.Options.UseTextOptions = true;
                    c.AppearanceHeader.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    c.AppearanceHeader.ForeColor = Color.Black;
                    c.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                    c.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    c.AppearanceCell.Options.UseTextOptions = true;
                    c.AppearanceCell.Options.UseBackColor = true;
                }
            }
            else if (_sLookUpItem.Name.Equals(sLookUp_PortDestination.Name))
            {
                sLookUp_PortDestination.Properties.PopupFormSize = new Size(360, 0);

                GridColumn gridCol_Destination = new GridColumn();

                // DESTINATION ID
                // HEADER
                gridCol_Destination.Name = "gridCol_Destination";
                gridCol_Destination.Caption = "PORT OF DESTINATION";
                gridCol_Destination.FieldName = "DESTINATION_ID";
                gridCol_Destination.VisibleIndex = 0;
                gridCol_Destination.Width = 250;
                // CELL
                gridCol_Destination.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;

                // Add column
                sLookUp_PortDestination_View.Columns.Add(gridCol_Destination);

                sLookUp_PortDestination_View.OptionsView.ColumnAutoWidth = false;

                // SET COMMON ATTRIBUTE
                foreach (GridColumn c in sLookUp_PortDestination_View.Columns)
                {
                    c.AppearanceHeader.Options.UseFont = true;
                    c.AppearanceHeader.Options.UseForeColor = true;
                    c.AppearanceHeader.Options.UseTextOptions = true;
                    c.AppearanceHeader.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    c.AppearanceHeader.ForeColor = Color.Black;
                    c.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                    c.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

                    c.AppearanceCell.Options.UseBackColor = true;
                    c.AppearanceCell.Options.UseTextOptions = true;
                }
            }
            else if (_sLookUpItem.Name.Equals(sLookUp_PriceCondition.Name))
            {
                sLookUp_PriceCondition.Properties.PopupFormSize = new Size(360, 0);

                GridColumn gridCol_Price_Condition = new GridColumn();

                // PRICE CONDITION
                // HEADER
                gridCol_Price_Condition.Name = "gridCol_Price_Condition";
                gridCol_Price_Condition.Caption = "PRICE CONDITION";
                gridCol_Price_Condition.FieldName = "PRICE_COND";
                gridCol_Price_Condition.VisibleIndex = 0;
                gridCol_Price_Condition.Width = 200;
                // CELL
                gridCol_Price_Condition.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;

                // Add column
                sLookUp_PriceCondition_View.Columns.Add(gridCol_Price_Condition);

                sLookUp_PriceCondition_View.OptionsView.ColumnAutoWidth = false;

                // SET COMMON ATTRIBUTE
                foreach (GridColumn c in sLookUp_PriceCondition_View.Columns)
                {
                    c.AppearanceHeader.Options.UseFont = true;
                    c.AppearanceHeader.Options.UseForeColor = true;
                    c.AppearanceHeader.Options.UseTextOptions = true;
                    c.AppearanceHeader.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    c.AppearanceHeader.ForeColor = Color.Black;
                    c.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                    c.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    c.AppearanceCell.Options.UseTextOptions = true;
                    c.AppearanceCell.Options.UseBackColor = true;
                }
            }
            else if (_sLookUpItem.Name.Equals(sLookUp_PaymentTerm.Name))
            {
                sLookUp_PaymentTerm.Properties.PopupFormSize = new Size(450, 0);

                GridColumn gridCol_Payment_Term = new GridColumn();

                // PRICE CONDITION
                // HEADER
                gridCol_Payment_Term.Name = "gridCol_Payment_Term";
                gridCol_Payment_Term.Caption = "PAYMENT TERM";
                gridCol_Payment_Term.FieldName = "PAYMENT_ID";
                gridCol_Payment_Term.VisibleIndex = 0;
                gridCol_Payment_Term.Width = 450;
                // CELL
                gridCol_Payment_Term.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;

                // Add column
                sLookUp_PaymentTerm_View.Columns.Add(gridCol_Payment_Term);

                sLookUp_PaymentTerm_View.OptionsView.ColumnAutoWidth = false;

                // SET COMMON ATTRIBUTE
                foreach (GridColumn c in sLookUp_PaymentTerm_View.Columns)
                {
                    c.AppearanceHeader.Options.UseFont = true;
                    c.AppearanceHeader.Options.UseForeColor = true;
                    c.AppearanceHeader.Options.UseTextOptions = true;
                    c.AppearanceHeader.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    c.AppearanceHeader.ForeColor = Color.Black;
                    c.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                    c.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    c.AppearanceCell.Options.UseTextOptions = true;
                    c.AppearanceCell.Options.UseBackColor = true;
                }
            }
        }
        /// <summary>
        /// Set Init combobox Freight
        /// </summary>
        public void SetInit_CboxFreight()
        {
            cb_Freight.DisplayMember = "Text";
            cb_Freight.ValueMember = "Value";

            var items = new[] {
                new { Text = ""       , Value = 99 },
                new { Text = "COLLECT", Value = 0 },
                new { Text = "PREPAID", Value = 1 },
            };

            cb_Freight.DataSource = items;
        }

        /// <summary>
        /// Set Init combobox Company Code
        /// </summary>
        public void SetInit_CbCompanyCode()
        {
            cb_CompanyCode.DisplayMember = "Text";
            cb_CompanyCode.ValueMember = "Value";

            var items = new[] {
                new { Text = "*-No Selected-*"  , Value = "00000" },
                new { Text = "TVC1"             , Value = "00001" },
                new { Text = "TVC2"             , Value = "00002" },
            };

            cb_CompanyCode.DataSource = items;
        }

        public void AddColumnDataSet()
        {
            Grid_Invoice.Columns.Add("Shipping_No", typeof(System.String));
            Grid_Invoice.Columns.Add("Customer_Code", typeof(System.String));
            Grid_Invoice.Columns.Add("Part_Description", typeof(System.String));
            Grid_Invoice.Columns.Add("Cus_Item_Code", typeof(System.String));
            Grid_Invoice.Columns.Add("TVC_Item_Code", typeof(System.String));
            Grid_Invoice.Columns.Add("Customer_PO", typeof(System.String));
            Grid_Invoice.Columns.Add("Third_Party_PO", typeof(System.String));
            Grid_Invoice.Columns.Add("Order_Date", typeof(System.DateTime));
            Grid_Invoice.Columns.Add("Due_Date_PO", typeof(System.DateTime));
            Grid_Invoice.Columns.Add("Quantity", typeof(System.Int32));
            Grid_Invoice.Columns.Add("Quantity_Revise", typeof(System.Int32));
            Grid_Invoice.Columns.Add("Balance", typeof(System.Int32));
            Grid_Invoice.Columns.Add("Unit_Currency", typeof(System.String));
            Grid_Invoice.Columns.Add("USD_Rate", typeof(System.Decimal));
            Grid_Invoice.Columns.Add("Order_Price", typeof(System.Decimal));
            Grid_Invoice.Columns.Add("Order_Price_Revise", typeof(System.Decimal));
            Grid_Invoice.Columns.Add("Global_Price", typeof(System.Decimal));
            Grid_Invoice.Columns.Add("Amount", typeof(System.Decimal));
        }
        #endregion

        private void btn_ClearData_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void ClearData()
        {
            Action<Control.ControlCollection> func = null;
            //Default radio button Normal
            radNormal.Checked = true;
            //Clear textbox
            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else if (control is DateTimePicker)
                        (control as DateTimePicker).Value = DateTime.Now;
                    else
                        func(control.Controls);
            };

            func(Controls);

            //
            sLookUp_ShippingNo.Enabled = true;

            //Clear Gridview Invoice
            gridControl_Invoice.DataSource = null;

            //Clear Gridview PL
            gridControl_PackingList.DataSource = null;

            //
            tabControl.SelectedIndex = 0;

            //
            cb_CompanyCode.SelectedIndex = 0;

            //
            dateEdit_Revenue.EditValue = DateTime.Now;

            //
            btnUnlockData.Enabled = false;

            //
            cb_Freight.SelectedValue = 99;

            //
            sLookUp_ShippingNo.Focus();
        }

        #region Moveable
        bool mouseDown = false;
        Point startPoint = new Point(0, 0);

        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void panelTop_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void panelTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - startPoint.X, p.Y - startPoint.Y);
            }
        }

        private void picBox_BackToMain_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(picBox_BackToMain,"Back to main screen");
        }

        //Draw number order
        private void GridView_Invoice_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void GridView_PackingList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }
        #endregion

        #region ButtonTop
        private void picBox_Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void picBox_Min_MouseEnter(object sender, EventArgs e)
        {
            picBox_Min.Size = new Size(27, 27);
        }

        private void picBox_Min_MouseLeave(object sender, EventArgs e)
        {
            picBox_Min.Size = new Size(25, 25);
        }

        private void picBox_Close_MouseEnter(object sender, EventArgs e)
        {
            picBox_Close.Size = new Size(27, 27);
        }

        private void picBox_Close_MouseLeave(object sender, EventArgs e)
        {
            picBox_Close.Size = new Size(25, 25);
        }

        private void picBox_Max_MouseEnter(object sender, EventArgs e)
        {
            picBox_Max.Size = new Size(27, 27);
        }

        private void picBox_Max_MouseLeave(object sender, EventArgs e)
        {
            picBox_Max.Size = new Size(25, 25);
        }

        private void picBox_BackToMain_MouseEnter(object sender, EventArgs e)
        {
            picBox_BackToMain.Size = new Size(27, 27);
        }

        private void picBox_BackToMain_MouseLeave(object sender, EventArgs e)
        {
            picBox_BackToMain.Size = new Size(25, 25);
        }

        private void Form_Invoice_PL_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void picBox_Close_Click(object sender, EventArgs e)
        {
            string exitMessageText = "Are you sure you want to exit?";
            string exitCaption = "Confirm";
            MessageBoxButtons button = MessageBoxButtons.YesNo;
            DialogResult res = MessageBox.Show(exitMessageText, exitCaption, button, MessageBoxIcon.Exclamation);
            if (res == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void picBox_Max_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.CenterScreen;
                picBox_Max.Image = Properties.Resources.Maximize_window;
                this.ShowInTaskbar = true;
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                picBox_Max.Image = Properties.Resources.Zoom_full;
                this.ShowInTaskbar = false;
            }

        }

        private void panel_Top_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                picBox_Max.Image = Properties.Resources.Zoom_full;
                this.ShowInTaskbar = false;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.CenterScreen;
                picBox_Max.Image = Properties.Resources.Maximize_window;
                this.ShowInTaskbar = true;
            }
        }
        #endregion

        #region GridView
        public void Define_GridView(GridView _dataGridView)
        {
            if (_dataGridView.Name == "gridView_Invoice")
            {
                // Setting GridView
                gridView_Invoice.OptionsNavigation.AutoFocusNewRow = true;
                gridView_Invoice.OptionsView.ColumnAutoWidth = false;
                gridView_Invoice.OptionsView.ShowFooter = true;
                gridView_Invoice.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;
                gridView_Invoice.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;

                GridColumn gridCol_Customer_Code = new GridColumn();
                GridColumn gridCol_Item_Name = new GridColumn();
                GridColumn gridCol_Cus_Item_Code = new GridColumn();
                GridColumn gridCol_TVC_Item_Code = new GridColumn();
                GridColumn gridCol_Customer_PO = new GridColumn();
                GridColumn gridCol_Third_Party_Item_Code = new GridColumn();
                GridColumn gridCol_Third_Party_PO = new GridColumn();
                GridColumn gridCol_OrderDate = new GridColumn();
                GridColumn gridCol_Due_Date_PO = new GridColumn();
                GridColumn gridCol_Qty = new GridColumn();
                GridColumn gridCol_Qty_Revise = new GridColumn();
                GridColumn gridCol_Balance = new GridColumn();
                GridColumn gridCol_Unit_Currency = new GridColumn();
                GridColumn gridCol_InvEx_Rate = new GridColumn();
                GridColumn gridCol_Order_Price = new GridColumn();
                GridColumn gridCol_Order_Price_Revise = new GridColumn();
                GridColumn gridCol_Global_Price = new GridColumn();
                GridColumn gridCol_Amount_Total = new GridColumn();

                //CUSTOMER CODE
                gridCol_Customer_Code.Name = "gridCol_Customer_Code";
                gridCol_Customer_Code.Caption = "CUSTOMER CODE";
                gridCol_Customer_Code.FieldName = "CUSTOMER_CODE";
                gridCol_Customer_Code.VisibleIndex = 0;
                gridCol_Customer_Code.Width = 100;

                gridCol_Customer_Code.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                // ITEM NAME
                gridCol_Item_Name.Name = "gridCol_Item_Name";
                gridCol_Item_Name.Caption = "PART DESCRIPTION";
                gridCol_Item_Name.FieldName = "PART_DESCRIPTION";
                gridCol_Item_Name.VisibleIndex = 1;
                gridCol_Item_Name.Width = 140;

                gridCol_Item_Name.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                // CUSTOMER ITEM CODE
                gridCol_Cus_Item_Code.Name = "gridCol_Cus_Item_Code";
                gridCol_Cus_Item_Code.Caption = "CUSTOMER ITEM CODE";
                gridCol_Cus_Item_Code.FieldName = "CUS_ITEM_CODE";
                gridCol_Cus_Item_Code.VisibleIndex = 2;
                gridCol_Cus_Item_Code.Width = 120;

                gridCol_Cus_Item_Code.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                // TVC ITEM CODE
                gridCol_TVC_Item_Code.Name = "gridCol_TVC_Item_Code";
                gridCol_TVC_Item_Code.Caption = "TVC ITEM CODE";
                gridCol_TVC_Item_Code.FieldName = "TVC_ITEM_CODE";
                gridCol_TVC_Item_Code.VisibleIndex = 3;
                gridCol_TVC_Item_Code.Width = 120;

                gridCol_TVC_Item_Code.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                // CUSTOMER PO
                gridCol_Customer_PO.Name = "gridCol_Customer_PO";
                gridCol_Customer_PO.Caption = "CUSTOMER PO";
                gridCol_Customer_PO.FieldName = "CUSTOMER_PO";
                gridCol_Customer_PO.VisibleIndex = 4;
                gridCol_Customer_PO.Width = 120;

                gridCol_Customer_PO.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                //THIRD PARTY ITEM CODE
                gridCol_Third_Party_Item_Code.Name = "gridCol_Third_Party_Item_Code";
                gridCol_Third_Party_Item_Code.Caption = "THIRD PARTY CODE";
                gridCol_Third_Party_Item_Code.FieldName = "THIRD_PARTY_ITEM_CODE";
                gridCol_Third_Party_Item_Code.VisibleIndex = 5;
                gridCol_Third_Party_Item_Code.Width = 120;

                gridCol_Third_Party_Item_Code.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                //THIRD PARTY PO
                gridCol_Third_Party_PO.Name = "gridCol_Third_Party_PO";
                gridCol_Third_Party_PO.Caption = "THIRD PARTY PO";
                gridCol_Third_Party_PO.FieldName = "THIRD_PARTY_PO";
                gridCol_Third_Party_Item_Code.VisibleIndex = 6;
                gridCol_Third_Party_PO.Width = 100;

                gridCol_Third_Party_PO.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                // ORDER DATE
                gridCol_OrderDate.Name = "gridCol_OrderDate";
                gridCol_OrderDate.Caption = "ORDER DATE PO";
                gridCol_OrderDate.FieldName = "ORDER_DATE";
                gridCol_OrderDate.VisibleIndex = 7;
                gridCol_OrderDate.Width = 110;
                gridCol_OrderDate.DisplayFormat.FormatString = "dd/MM/yyyy";
                gridCol_OrderDate.DisplayFormat.FormatType = FormatType.DateTime;

                gridCol_OrderDate.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                //DUE DATE PO
                gridCol_Due_Date_PO.Name = "gridCol_Due_Date_PO";
                gridCol_Due_Date_PO.Caption = "DUE DATE PO";
                gridCol_Due_Date_PO.FieldName = "DUE_DATE_PO";
                gridCol_Due_Date_PO.VisibleIndex = 8;
                gridCol_Due_Date_PO.Width = 120;
                gridCol_Due_Date_PO.DisplayFormat.FormatString = "dd/MM/yyyy";
                gridCol_Due_Date_PO.DisplayFormat.FormatType = FormatType.DateTime;

                gridCol_Due_Date_PO.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                //QUANTITY
                gridCol_Qty.Name = "gridCol_Qty";
                gridCol_Qty.Caption = "QUANTITY";
                gridCol_Qty.FieldName = "QUANTITY";
                gridCol_Qty.VisibleIndex = 9;
                gridCol_Qty.Width = 120;
                gridCol_Qty.DisplayFormat.FormatString = "#,##0";
                gridCol_Qty.DisplayFormat.FormatType = FormatType.Numeric;
                gridCol_Qty.SummaryItem.SummaryType = SummaryItemType.Sum;
                gridCol_Qty.SummaryItem.DisplayFormat = "{0:#,##0.####}";

                gridCol_Qty.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

                //QUANTITY REVISE
                gridCol_Qty_Revise.Name = "gridCol_Qty_Revise";
                gridCol_Qty_Revise.Caption = "QUANTITY REVISE";
                gridCol_Qty_Revise.FieldName = "QUANTITY_REVISE";
                gridCol_Qty_Revise.VisibleIndex = 10;
                gridCol_Qty_Revise.Width = 120;
                gridCol_Qty_Revise.DisplayFormat.FormatString = "#,##0";
                gridCol_Qty_Revise.DisplayFormat.FormatType = FormatType.Numeric;
                gridCol_Qty_Revise.Visible = false;

                gridCol_Qty_Revise.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

                // BALANCE
                gridCol_Balance.Name = "gridCol_Balance";
                gridCol_Balance.Caption = "BALANCE";
                gridCol_Balance.FieldName = "INV_BALANCE";
                gridCol_Balance.VisibleIndex = 11;
                gridCol_Balance.Width = 120;
                gridCol_Balance.DisplayFormat.FormatString = "#,##0";
                gridCol_Balance.DisplayFormat.FormatType = FormatType.Numeric;

                gridCol_Balance.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

                // UNIT CURRENCY
                gridCol_Unit_Currency.Name = "gridCol_Unit_Currency";
                gridCol_Unit_Currency.Caption = "UNIT CURRENTCY";
                gridCol_Unit_Currency.FieldName = "UNIT_CURRENCY";
                gridCol_Unit_Currency.VisibleIndex = 12;
                gridCol_Unit_Currency.Width = 100;

                gridCol_Unit_Currency.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                //EX RATE
                gridCol_InvEx_Rate.Name = "gridCol_InvEx_Rate";
                gridCol_InvEx_Rate.Caption = "Ex. RATE";
                gridCol_InvEx_Rate.FieldName = "USD_RATE";
                gridCol_InvEx_Rate.VisibleIndex = 13;
                gridCol_InvEx_Rate.Width = 100;
                gridCol_InvEx_Rate.DisplayFormat.FormatString = "#,##0.####";
                gridCol_InvEx_Rate.DisplayFormat.FormatType = FormatType.Numeric;

                gridCol_InvEx_Rate.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

                // ORDER PRICE
                gridCol_Order_Price.Name = "gridCol_Order_Price";
                gridCol_Order_Price.Caption = "ORDER PRICE";
                gridCol_Order_Price.FieldName = "ORDER_PRICE";
                gridCol_Order_Price.VisibleIndex = 14;
                gridCol_Order_Price.Width = 120;
                gridCol_Order_Price.DisplayFormat.FormatString = "#,##0.####";
                gridCol_Order_Price.DisplayFormat.FormatType = FormatType.Numeric;

                gridCol_Order_Price.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

                //ORDER PRICE REVISE
                gridCol_Order_Price_Revise.Name = "gridCol_Order_Price_Revise";
                gridCol_Order_Price_Revise.Caption = "ORDER PRICE REVISE";
                gridCol_Order_Price_Revise.FieldName = "ORDER_PRICE_REVISE";
                gridCol_Order_Price_Revise.VisibleIndex = 15;
                gridCol_Order_Price_Revise.Width = 130;
                gridCol_Order_Price_Revise.DisplayFormat.FormatString = "#,##0.####";
                gridCol_Order_Price_Revise.DisplayFormat.FormatType = FormatType.Numeric;
                gridCol_Order_Price_Revise.Visible = false;

                gridCol_Order_Price_Revise.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

                // GLOBAL PRICE
                gridCol_Global_Price.Name = "gridCol_Global_Price";
                gridCol_Global_Price.Caption = "GLOBAL PRICE";
                gridCol_Global_Price.FieldName = "PRICE";
                gridCol_Global_Price.VisibleIndex = 16;
                gridCol_Global_Price.Width = 130;
                gridCol_Global_Price.DisplayFormat.FormatString = "#,##0.####";
                gridCol_Global_Price.DisplayFormat.FormatType = FormatType.Numeric;

                gridCol_Global_Price.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

                // AMOUNT (JPY)
                gridCol_Amount_Total.Name = "gridCol_Amount_Total";
                gridCol_Amount_Total.Caption = "AMOUNT TOTAL";
                gridCol_Amount_Total.FieldName = "AMOUNT_JPY";
                gridCol_Amount_Total.VisibleIndex = 17;
                gridCol_Amount_Total.Width = 120;
                gridCol_Amount_Total.DisplayFormat.FormatString = "#,##0.####";
                gridCol_Amount_Total.DisplayFormat.FormatType = FormatType.Numeric;
                gridCol_Amount_Total.SummaryItem.SummaryType = SummaryItemType.Sum;

                gridCol_Amount_Total.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

                // Add column to gridview
                gridView_Invoice.Columns.Add(gridCol_Customer_Code);
                gridView_Invoice.Columns.Add(gridCol_Item_Name);
                gridView_Invoice.Columns.Add(gridCol_Cus_Item_Code);
                gridView_Invoice.Columns.Add(gridCol_TVC_Item_Code);
                gridView_Invoice.Columns.Add(gridCol_Customer_PO);
                gridView_Invoice.Columns.Add(gridCol_Third_Party_Item_Code);
                gridView_Invoice.Columns.Add(gridCol_Third_Party_PO);
                gridView_Invoice.Columns.Add(gridCol_OrderDate);
                gridView_Invoice.Columns.Add(gridCol_Due_Date_PO);
                gridView_Invoice.Columns.Add(gridCol_Qty);
                gridView_Invoice.Columns.Add(gridCol_Qty_Revise);
                gridView_Invoice.Columns.Add(gridCol_Balance);
                gridView_Invoice.Columns.Add(gridCol_Unit_Currency);
                gridView_Invoice.Columns.Add(gridCol_InvEx_Rate);
                gridView_Invoice.Columns.Add(gridCol_Order_Price);
                gridView_Invoice.Columns.Add(gridCol_Order_Price_Revise);
                gridView_Invoice.Columns.Add(gridCol_Global_Price);
                gridView_Invoice.Columns.Add(gridCol_Amount_Total);

                // Set common attribute
                foreach (GridColumn c in gridView_Invoice.Columns)
                {
                    c.AppearanceHeader.Options.UseFont = true;
                    c.AppearanceHeader.Options.UseForeColor = true;
                    c.AppearanceHeader.Options.UseTextOptions = true;
                    c.AppearanceHeader.ForeColor = Color.Black;
                    c.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                    c.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    c.AppearanceHeader.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

                    c.AppearanceCell.Options.UseBackColor = true;
                    c.AppearanceCell.Options.UseTextOptions = true;
                }
            }
            else if (_dataGridView.Name == "gridView_PackingList")
            {
                // Setting GridView
                gridView_PackingList.OptionsNavigation.AutoFocusNewRow = true;
                gridView_PackingList.OptionsView.ColumnAutoWidth = false;
                gridView_PackingList.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;
                gridView_PackingList.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;

                GridColumn gridCol_Customer_Code = new GridColumn();
                GridColumn gridCol_Cus_Item_Code = new GridColumn();
                GridColumn gridCol_TVC_Item_Code = new GridColumn();
                GridColumn gridCol_Customer_PO = new GridColumn();
                GridColumn gridCol_Packages_No= new GridColumn();
                GridColumn gridCol_Qty_Carton= new GridColumn();
                GridColumn gridCol_Qty_Per_Carton= new GridColumn();
                GridColumn gridCol_Qty_Total= new GridColumn();
                GridColumn gridCol_Qty_Total_Revise= new GridColumn();
                GridColumn gridCol_Net_Weight= new GridColumn();
                GridColumn gridCol_Net_Weight_Total= new GridColumn();
                GridColumn gridCol_Gross_Weight = new GridColumn();
                GridColumn gridCol_LotNo = new GridColumn();

                //CUSTOMER CODE
                gridCol_Customer_Code.Name = "gridCol_Customer_Code";
                gridCol_Customer_Code.Caption = "CUSTOMER CODE";
                gridCol_Customer_Code.FieldName = "CUSTOMER_CODE";
                gridCol_Customer_Code.VisibleIndex = 0;
                gridCol_Customer_Code.Width = 100;

                gridCol_Customer_Code.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                // CUSTOMER ITEM CODE
                gridCol_Cus_Item_Code.Name = "gridCol_Cus_Item_Code";
                gridCol_Cus_Item_Code.Caption = "CUSTOMER ITEM CODE";
                gridCol_Cus_Item_Code.FieldName = "CUS_ITEM_CODE";
                gridCol_Cus_Item_Code.VisibleIndex = 0;
                gridCol_Cus_Item_Code.Width = 120;

                gridCol_Cus_Item_Code.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                // TVC ITEM CODE
                gridCol_TVC_Item_Code.Name = "gridCol_TVC_Item_Code";
                gridCol_TVC_Item_Code.Caption = "TVC ITEM CODE";
                gridCol_TVC_Item_Code.FieldName = "TVC_ITEM_CODE";
                gridCol_TVC_Item_Code.VisibleIndex = 0;
                gridCol_TVC_Item_Code.Width = 120;

                gridCol_TVC_Item_Code.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                // CUSTOMER PO
                gridCol_Customer_PO.Name = "gridCol_Customer_PO";
                gridCol_Customer_PO.Caption = "CUSTOMER PO";
                gridCol_Customer_PO.FieldName = "CUSTOMER_PO";
                gridCol_Customer_PO.VisibleIndex = 0;
                gridCol_Customer_PO.Width = 120;

                gridCol_Customer_PO.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                //PACKAGES NO
                gridCol_Packages_No.Name = "gridCol_Packages_No";
                gridCol_Packages_No.Caption = "PACKAGES NO";
                gridCol_Packages_No.FieldName = "PACKAGES_NO";
                gridCol_Packages_No.VisibleIndex = 0;
                gridCol_Packages_No.Width = 90;

                gridCol_Packages_No.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                //QTY CARTON
                gridCol_Qty_Carton.Name = "gridCol_Qty_Carton";
                gridCol_Qty_Carton.Caption = "QUANTITY CARTON";
                gridCol_Qty_Carton.FieldName = "QTY_CARTON";
                gridCol_Qty_Carton.VisibleIndex = 0;
                gridCol_Qty_Carton.Width = 90;
                gridCol_Qty_Carton.DisplayFormat.FormatString = "#,##0.##";
                gridCol_Qty_Carton.DisplayFormat.FormatType = FormatType.Numeric;
                gridCol_Qty_Carton.SummaryItem.SummaryType = SummaryItemType.Sum;
                gridCol_Qty_Carton.SummaryItem.DisplayFormat = "{0:#,##0.####}";

                //QTY PER CARTON
                gridCol_Qty_Per_Carton.Name = "gridCol_Qty_Per_Carton";
                gridCol_Qty_Per_Carton.Caption = "QUANTITY / CARTON (PCS)";
                gridCol_Qty_Per_Carton.FieldName = "BOX_QUANTITY";
                gridCol_Qty_Per_Carton.VisibleIndex = 0;
                gridCol_Qty_Per_Carton.Width = 90;
                gridCol_Qty_Per_Carton.DisplayFormat.FormatString = "#,##0.##";
                gridCol_Qty_Per_Carton.DisplayFormat.FormatType = FormatType.Numeric;

                //QTY TOTAL
                gridCol_Qty_Total.Name = "gridCol_Qty_Total";
                gridCol_Qty_Total.Caption = "QUANTITY TOTAL (PCS)";
                gridCol_Qty_Total.FieldName = "QTY_TOTAL";
                gridCol_Qty_Total.VisibleIndex = 0;
                gridCol_Qty_Total.Width = 90;
                gridCol_Qty_Total.DisplayFormat.FormatString = "#,##0.##";
                gridCol_Qty_Total.DisplayFormat.FormatType = FormatType.Numeric;
                gridCol_Qty_Total.SummaryItem.SummaryType = SummaryItemType.Sum;
                gridCol_Qty_Total.SummaryItem.DisplayFormat = "{0:#,##0.####}";

                gridCol_Gross_Weight.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

                //QTY TOTAL REVISE
                gridCol_Qty_Total_Revise.Name = "gridCol_Qty_Total_Revise";
                gridCol_Qty_Total_Revise.Caption = "QTY TOTAL REVISE (PCS)";
                gridCol_Qty_Total_Revise.FieldName = "QTY_TOTAL_REVISE";
                gridCol_Qty_Total_Revise.VisibleIndex = 0;
                gridCol_Qty_Total_Revise.Width = 90;
                gridCol_Qty_Total_Revise.DisplayFormat.FormatString = "#,##0.##";
                gridCol_Qty_Total_Revise.DisplayFormat.FormatType = FormatType.Numeric;

                gridCol_Gross_Weight.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

                //NET WEIGHT
                gridCol_Net_Weight.Name = "gridCol_Net_Weight";
                gridCol_Net_Weight.Caption = "N / W (KG)";
                gridCol_Net_Weight.FieldName = "WEIGHT";
                gridCol_Net_Weight.VisibleIndex = 0;
                gridCol_Net_Weight.Width = 90;
                gridCol_Net_Weight.DisplayFormat.FormatString = "#,##0.##";
                gridCol_Net_Weight.DisplayFormat.FormatType = FormatType.Numeric;
                gridCol_Net_Weight.SummaryItem.SummaryType = SummaryItemType.Sum;
                gridCol_Net_Weight.SummaryItem.DisplayFormat = "{0:#,##0.####}";

                gridCol_Gross_Weight.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

                //NET WEIGHT TOTAL
                gridCol_Net_Weight_Total.Name = "gridCol_Net_Weight_Total";
                gridCol_Net_Weight_Total.Caption = "N / W TOTAL";
                gridCol_Net_Weight_Total.FieldName = "WEIGHT_TOTAL";
                gridCol_Net_Weight_Total.VisibleIndex = 0;
                gridCol_Net_Weight_Total.Width = 90;
                gridCol_Net_Weight_Total.DisplayFormat.FormatString = "#,##0.##";
                gridCol_Net_Weight_Total.DisplayFormat.FormatType = FormatType.Numeric;

                gridCol_Gross_Weight.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

                //GROSS WEIGHT
                gridCol_Gross_Weight.Name = "gridCol_Gross_Weight";
                gridCol_Gross_Weight.Caption = "G / W (KG)";
                gridCol_Gross_Weight.FieldName = "GROSS_WEIGHT";
                gridCol_Gross_Weight.VisibleIndex = 0;
                gridCol_Gross_Weight.Width = 90;
                gridCol_Gross_Weight.DisplayFormat.FormatString = "#,##0.##";
                gridCol_Gross_Weight.DisplayFormat.FormatType = FormatType.Numeric;
                gridCol_Gross_Weight.SummaryItem.SummaryType = SummaryItemType.Sum;
                gridCol_Gross_Weight.SummaryItem.DisplayFormat = "{0:#,##0.####}";

                gridCol_Gross_Weight.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

                //LOT NO
                gridCol_LotNo.Name = "gridCol_LotNo";
                gridCol_LotNo.Caption = "LOT NO";
                gridCol_LotNo.FieldName = "LOT_NO";
                gridCol_LotNo.VisibleIndex = 0;
                gridCol_LotNo.Width = 150;

                gridCol_LotNo.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                // Add column to gridview
                gridView_PackingList.Columns.Add(gridCol_Customer_Code);
                gridView_PackingList.Columns.Add(gridCol_Packages_No);
                gridView_PackingList.Columns.Add(gridCol_Cus_Item_Code);
                gridView_PackingList.Columns.Add(gridCol_TVC_Item_Code);
                gridView_PackingList.Columns.Add(gridCol_Customer_PO);
                gridView_PackingList.Columns.Add(gridCol_Qty_Carton);
                gridView_PackingList.Columns.Add(gridCol_Qty_Per_Carton);
                gridView_PackingList.Columns.Add(gridCol_Qty_Total);
                gridView_PackingList.Columns.Add(gridCol_Qty_Total_Revise);
                gridView_PackingList.Columns.Add(gridCol_Net_Weight);
                gridView_PackingList.Columns.Add(gridCol_Net_Weight_Total);
                gridView_PackingList.Columns.Add(gridCol_Gross_Weight);
                gridView_PackingList.Columns.Add(gridCol_LotNo);

                // Setting GridView
                gridView_PackingList.OptionsNavigation.AutoFocusNewRow = true;
                gridView_PackingList.OptionsView.ColumnAutoWidth = false;
                gridView_PackingList.OptionsView.ShowFooter = true;
                gridView_PackingList.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;
                gridView_PackingList.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;

                // Set common attribute
                foreach (GridColumn c in gridView_PackingList.Columns)
                {
                    c.AppearanceHeader.Options.UseFont = true;
                    c.AppearanceHeader.Options.UseForeColor = true;
                    c.AppearanceHeader.Options.UseTextOptions = true;
                    c.AppearanceHeader.ForeColor = Color.Black;
                    c.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                    c.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    c.AppearanceHeader.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

                    c.AppearanceCell.Options.UseBackColor = true;
                    c.AppearanceCell.Options.UseTextOptions = true;
                }
            }
        }

        /// <summary>
        /// Setting column Revise
        /// </summary>
        /// <param name="IsRevise"></param>
        public void SettingInitGridView()
        {
            if (radNormal.Checked == true)
            {
                //--------------------- Header ---------------------//
                cb_CompanyCode.Enabled                  = true;
                dateEdit_ETA.Enabled                    = true;
                dateEdit_ETD.Enabled                    = true;
                dateEdit_DateCreateShipping.Enabled     = true;
                txtInvoiceNo.ReadOnly                   = false;
                sLookUp_IssuedTo_CompanyCode.ReadOnly   = false;
                txtIssuedTo_CompanyName.ReadOnly        = false;
                memo_IssuedTo_CompanyAddress.ReadOnly   = false;
                txtIssuedTo_TelNo.ReadOnly              = false;
                txtIssuedTo_FaxNo.ReadOnly              = false;

                sLookUp_ShipTo_CompanyCode.ReadOnly     = false;
                txtShipTo_CompanyName.ReadOnly          = false;
                memo_ShipTo_CompanyAddress.ReadOnly     = false;
                txtShipTo_TelNo.ReadOnly                = false;
                txtShipTo_FaxNo.ReadOnly                = false;

                txtShipVia.ReadOnly                     = false;
                dateEdit_Revenue.Enabled                      = true;
                cb_Freight.Enabled                      = true;
                txtVessel.ReadOnly                      = false;
                sLookUp_PortLoading.ReadOnly            = false;
                sLookUp_PortDestination.ReadOnly        = false;

                sLookUp_PriceCondition.ReadOnly         = false;
                sLookUp_PaymentTerm.ReadOnly            = false;

                //Button Lock, Unlock & Revise Data
                btnLockData.Enabled                     = true;
                btnUnlockData.Enabled                   = false;

                //
                btnSave_Data.Enabled                    = true;

                gridView_Invoice.OptionsBehavior.AllowDeleteRows = DefaultBoolean.True;
                gridView_PackingList.OptionsBehavior.AllowDeleteRows = DefaultBoolean.True;

                //---------------- Gridview Invoice ----------------//
                //---- Enable ----//
                gridView_Invoice.Columns["gridCol_Customer_Code"].OptionsColumn.ReadOnly = true;
                gridView_Invoice.Columns["gridCol_Customer_Code"].AppearanceCell.BackColor = Color.Gray;

                gridView_Invoice.Columns["gridCol_Item_Name"].OptionsColumn.ReadOnly  = false;
                gridView_Invoice.Columns["gridCol_Item_Name"].AppearanceCell.BackColor = Color.White;

                gridView_Invoice.Columns["gridCol_Cus_Item_Code"].OptionsColumn.ReadOnly  = true;
                gridView_Invoice.Columns["gridCol_Cus_Item_Code"].AppearanceCell.BackColor = Color.Gray;

                gridView_Invoice.Columns["gridCol_TVC_Item_Code"].OptionsColumn.ReadOnly  = true;
                gridView_Invoice.Columns["gridCol_TVC_Item_Code"].AppearanceCell.BackColor = Color.Gray;

                gridView_Invoice.Columns["gridCol_Customer_PO"].OptionsColumn.ReadOnly  = false;
                gridView_Invoice.Columns["gridCol_Customer_PO"].AppearanceCell.BackColor = Color.White;

                gridView_Invoice.Columns["gridCol_Third_Party_PO"].OptionsColumn.ReadOnly  = true;
                gridView_Invoice.Columns["gridCol_Third_Party_PO"].AppearanceCell.BackColor = Color.Gray;

                gridView_Invoice.Columns["gridCol_OrderDate"].OptionsColumn.ReadOnly  = true;
                gridView_Invoice.Columns["gridCol_OrderDate"].AppearanceCell.BackColor = Color.Gray;

                gridView_Invoice.Columns["gridCol_Due_Date_PO"].OptionsColumn.ReadOnly  = true;
                gridView_Invoice.Columns["gridCol_Due_Date_PO"].AppearanceCell.BackColor = Color.Gray;

                gridView_Invoice.Columns["gridCol_Qty"].OptionsColumn.ReadOnly  = false;
                gridView_Invoice.Columns["gridCol_Qty"].AppearanceCell.BackColor = Color.White;

                gridView_Invoice.Columns["gridCol_Balance"].OptionsColumn.ReadOnly  = true;
                gridView_Invoice.Columns["gridCol_Balance"].AppearanceCell.BackColor = Color.Gray;

                gridView_Invoice.Columns["gridCol_Unit_Currency"].OptionsColumn.ReadOnly  = true;
                gridView_Invoice.Columns["gridCol_Unit_Currency"].AppearanceCell.BackColor = Color.Gray;

                gridView_Invoice.Columns["gridCol_InvEx_Rate"].OptionsColumn.ReadOnly  = false;
                gridView_Invoice.Columns["gridCol_InvEx_Rate"].AppearanceCell.BackColor = Color.White;

                gridView_Invoice.Columns["gridCol_Order_Price"].OptionsColumn.ReadOnly  = false;
                gridView_Invoice.Columns["gridCol_Order_Price"].AppearanceCell.BackColor = Color.White;

                gridView_Invoice.Columns["gridCol_Global_Price"].OptionsColumn.ReadOnly  = true;
                gridView_Invoice.Columns["gridCol_Global_Price"].AppearanceCell.BackColor = Color.Gray;

                gridView_Invoice.Columns["gridCol_Amount_Total"].OptionsColumn.ReadOnly  = true;
                gridView_Invoice.Columns["gridCol_Amount_Total"].AppearanceCell.BackColor = Color.Gray;

                //------ Disable------//
                gridView_Invoice.Columns["gridCol_Qty_Revise"].Visible = false;
                gridView_Invoice.Columns["gridCol_Qty_Revise"].AppearanceCell.BackColor = Color.Gray;

                gridView_Invoice.Columns["gridCol_Order_Price_Revise"].Visible = false;
                gridView_Invoice.Columns["gridCol_Order_Price_Revise"].AppearanceCell.BackColor = Color.Gray;

                //------------ Gridview PackingLisst ---------------//
                //------ Disable------//
                gridView_PackingList.Columns["gridCol_Qty_Total_Revise"].Visible = false;
                gridView_PackingList.Columns["gridCol_Qty_Total_Revise"].OptionsColumn.ReadOnly = true;
                gridView_PackingList.Columns["gridCol_Qty_Total_Revise"].AppearanceCell.BackColor = Color.Gray;

                ////---------------- Gridview PackingList ----------------//
                ////---- Enable ----//
                //foreach (DataGridViewRow dtRow in GridView_PackingList.Rows)
                //{
                //    if (!dtRow.IsNewRow)
                //    { 
                //        dtRow.Cells["Customer_Code"].ReadOnly = true;
                //        dtRow.Cells["Customer_Code"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Packages_No"].ReadOnly = false;
                //        dtRow.Cells["Packages_No"].Style.BackColor = Color.White;

                //        dtRow.Cells["Customer_ItemCode"].ReadOnly = true;
                //        dtRow.Cells["Customer_ItemCode"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["TVC_ItemCode"].ReadOnly = true;
                //        dtRow.Cells["TVC_ItemCode"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Customer_PO"].ReadOnly = false;
                //        dtRow.Cells["Customer_PO"].Style.BackColor = Color.White;

                //        dtRow.Cells["Qty_Carton"].ReadOnly = false;
                //        dtRow.Cells["Qty_Carton"].Style.BackColor = Color.White;

                //        dtRow.Cells["Qty_Per_Carton"].ReadOnly = false;
                //        dtRow.Cells["Qty_Per_Carton"].Style.BackColor = Color.White;

                //        dtRow.Cells["Qty_Total"].ReadOnly = true;
                //        dtRow.Cells["Qty_Total"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Net_Weight"].ReadOnly = false;
                //        dtRow.Cells["Net_Weight"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Net_Weight_Total"].ReadOnly = false;
                //        dtRow.Cells["Net_Weight_Total"].Style.BackColor = Color.White;

                //        dtRow.Cells["Gross_Weight"].ReadOnly = false;
                //        dtRow.Cells["Gross_Weight"].Style.BackColor = Color.White;

                //        dtRow.Cells["Lot_No"].ReadOnly = false;
                //        dtRow.Cells["Lot_No"].Style.BackColor = Color.White;
                //    }
                //}
            }
            else if (radLock.Checked == true)
            {
                //--------------------- Header ---------------------//
                //---- Disable ----//
                cb_CompanyCode.Enabled = false;
                dateEdit_ETA.Enabled = false;
                dateEdit_ETD.Enabled  = false;
                dateEdit_DateCreateShipping.Enabled = false;
                txtInvoiceNo.ReadOnly = true;
                sLookUp_IssuedTo_CompanyCode.ReadOnly = true;
                txtIssuedTo_CompanyName.ReadOnly = true;
                memo_IssuedTo_CompanyAddress.ReadOnly = true;
                txtIssuedTo_TelNo.ReadOnly = true;
                txtIssuedTo_FaxNo.ReadOnly = true;

                sLookUp_ShipTo_CompanyCode.ReadOnly = true;
                txtShipTo_CompanyName.ReadOnly = true;
                memo_ShipTo_CompanyAddress.ReadOnly = true;
                txtShipTo_TelNo.ReadOnly = true;
                txtShipTo_FaxNo.ReadOnly = true;

                txtShipVia.ReadOnly = true;
                dateEdit_Revenue.Enabled = false;
                cb_Freight.Enabled = false;
                txtVessel.ReadOnly = true;
                sLookUp_PortLoading.ReadOnly = true;
                sLookUp_PortDestination.ReadOnly = true;

                sLookUp_PriceCondition.ReadOnly = true;
                sLookUp_PaymentTerm.ReadOnly = true;

                btnLockData.Enabled = false;
                if(String.Equals(createBy,_systemDAL.UserName) && String.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                {
                    btnUnlockData.Enabled = true;
                }
                else
                {
                    btnUnlockData.Enabled = false;
                }
                btnSave_Data.Enabled = false;

                gridView_Invoice.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
                gridView_PackingList.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;

                //---------------- Gridview Invoice ----------------//
                //---- Disable ----// 
                gridView_Invoice.Columns["gridCol_Qty_Revise"].Visible = false;
                gridView_Invoice.Columns["gridCol_Qty_Revise"].AppearanceCell.BackColor = Color.Gray;

                gridView_Invoice.Columns["gridCol_Order_Price_Revise"].Visible = false;
                gridView_Invoice.Columns["gridCol_Order_Price_Revise"].AppearanceCell.BackColor = Color.Gray;

                ////---- Disable ----//
                //foreach (DataGridViewRow dtRow in GridView_Invoice.Rows)
                //{
                //    if (!dtRow.IsNewRow)
                //    {
                //        dtRow.Cells["Customer_Code"].ReadOnly = true;
                //        dtRow.Cells["Customer_Code"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Part_Description"].ReadOnly = true;
                //        dtRow.Cells["Part_Description"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Cus_ItemCode"].ReadOnly = true;
                //        dtRow.Cells["Cus_ItemCode"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Tvc_ItemCode"].ReadOnly = true;
                //        dtRow.Cells["Tvc_ItemCode"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Customer_PO"].ReadOnly = true;
                //        dtRow.Cells["Customer_PO"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Order_Date"].ReadOnly = true;
                //        dtRow.Cells["Order_Date"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["ThirdParty_PO"].ReadOnly = true;
                //        dtRow.Cells["ThirdParty_PO"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["DueDate_PO"].ReadOnly = true;
                //        dtRow.Cells["DueDate_PO"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Quantity"].ReadOnly = true;
                //        dtRow.Cells["Quantity"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Balance"].ReadOnly = true;
                //        dtRow.Cells["Balance"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Unit_Currency"].ReadOnly = true;
                //        dtRow.Cells["Unit_Currency"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["USD_Rate"].ReadOnly = true;
                //        dtRow.Cells["USD_Rate"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Order_Price"].ReadOnly = true;
                //        dtRow.Cells["Order_Price"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Global_Price"].ReadOnly = true;
                //        dtRow.Cells["Global_Price"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Amount_Jpy"].ReadOnly = true;
                //        dtRow.Cells["Amount_Jpy"].Style.BackColor = Color.Gray;
                //    }
                //}

                ////---------------- Gridview PackingList ----------------//
                ////---- Disable ----//
                //foreach (DataGridViewRow dtRow in GridView_PackingList.Rows)
                //{
                //    if (!dtRow.IsNewRow)
                //    { 
                //        dtRow.Cells["Customer_Code"].ReadOnly = true;
                //        dtRow.Cells["Customer_Code"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Packages_No"].ReadOnly = true;
                //        dtRow.Cells["Packages_No"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Customer_ItemCode"].ReadOnly = true;
                //        dtRow.Cells["Customer_ItemCode"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["TVC_ItemCode"].ReadOnly = true;
                //        dtRow.Cells["TVC_ItemCode"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Customer_PO"].ReadOnly = true;
                //        dtRow.Cells["Customer_PO"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Qty_Carton"].ReadOnly = true;
                //        dtRow.Cells["Qty_Carton"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Qty_Per_Carton"].ReadOnly = true;
                //        dtRow.Cells["Qty_Per_Carton"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Qty_Total"].ReadOnly = true;
                //        dtRow.Cells["Qty_Total"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Net_Weight"].ReadOnly = true;
                //        dtRow.Cells["Net_Weight"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Net_Weight_Total"].ReadOnly = true;
                //        dtRow.Cells["Net_Weight_Total"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Gross_Weight"].ReadOnly = true;
                //        dtRow.Cells["Gross_Weight"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Lot_No"].ReadOnly = true;
                //        dtRow.Cells["Lot_No"].Style.BackColor = Color.Gray;
                //    }
                //}
            }
            else if(radRevise.Checked == true)
            {
                //--------------------- Header ---------------------//
                cb_CompanyCode.Enabled = false;
                dateEdit_ETA.Enabled = false;
                dateEdit_ETD.Enabled  = false;
                dateEdit_DateCreateShipping.Enabled = false;
                txtInvoiceNo.ReadOnly = true;
                sLookUp_IssuedTo_CompanyCode.ReadOnly = true;
                txtIssuedTo_CompanyName.ReadOnly = true;
                memo_IssuedTo_CompanyAddress.ReadOnly = true;
                txtIssuedTo_TelNo.ReadOnly = true;
                txtIssuedTo_FaxNo.ReadOnly = true;

                sLookUp_ShipTo_CompanyCode.ReadOnly = true;
                txtShipTo_CompanyName.ReadOnly = true;
                memo_ShipTo_CompanyAddress.ReadOnly = true;
                txtShipTo_TelNo.ReadOnly = true;
                txtShipTo_FaxNo.ReadOnly = true;

                txtShipVia.ReadOnly = true;
                dateEdit_Revenue.Enabled = false;
                cb_Freight.Enabled = false;
                txtVessel.ReadOnly = true;
                sLookUp_PortLoading.ReadOnly = true;
                sLookUp_PortDestination.ReadOnly = true;

                sLookUp_PriceCondition.ReadOnly = true;
                sLookUp_PaymentTerm.ReadOnly = true;

                btnLockData.Enabled = false;
                btnUnlockData.Enabled = false;
                btnSave_Data.Enabled = true;

                gridView_Invoice.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
                gridView_PackingList.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;

                //---- Enable ----//
                //Gridview Invoice
                gridView_Invoice.Columns["gridCol_Qty_Revise"].Visible = true;
                gridView_Invoice.Columns["gridCol_Qty_Revise"].OptionsColumn.ReadOnly = false;
                gridView_Invoice.Columns["gridCol_Qty_Revise"].AppearanceCell.BackColor = Color.White;
                //
                gridView_Invoice.Columns["gridCol_Order_Price_Revise"].Visible = true;
                gridView_Invoice.Columns["gridCol_Order_Price_Revise"].OptionsColumn.ReadOnly = false;
                gridView_Invoice.Columns["gridCol_Order_Price_Revise"].AppearanceCell.BackColor = Color.White;

                //Gridview PackingList
                gridView_PackingList.Columns["gridCol_Qty_Total_Revise"].Visible = true;
                gridView_PackingList.Columns["gridCol_Qty_Total_Revise"].OptionsColumn.ReadOnly = false;
                gridView_PackingList.Columns["gridCol_Qty_Total_Revise"].AppearanceCell.BackColor = Color.White;

                ////---- Disable ----//
                //foreach(DataGridViewRow dtRow in GridView_Invoice.Rows)
                //{
                //    if (!dtRow.IsNewRow)
                //    {
                //        dtRow.Cells["Customer_Code"].ReadOnly = true;
                //        dtRow.Cells["Customer_Code"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Part_Description"].ReadOnly = true;
                //        dtRow.Cells["Part_Description"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Tvc_ItemCode"].ReadOnly = true;
                //        dtRow.Cells["Tvc_ItemCode"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Customer_PO"].ReadOnly = true;
                //        dtRow.Cells["Customer_PO"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Order_Date"].ReadOnly = true;
                //        dtRow.Cells["Order_Date"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["ThirdParty_PO"].ReadOnly = true;
                //        dtRow.Cells["ThirdParty_PO"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["DueDate_PO"].ReadOnly = true;
                //        dtRow.Cells["DueDate_PO"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Quantity"].ReadOnly = true;
                //        dtRow.Cells["Quantity"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Balance"].ReadOnly = true;
                //        dtRow.Cells["Balance"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Unit_Currency"].ReadOnly = true;
                //        dtRow.Cells["Unit_Currency"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["USD_Rate"].ReadOnly = true;
                //        dtRow.Cells["USD_Rate"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Order_Price"].ReadOnly = true;
                //        dtRow.Cells["Order_Price"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Global_Price"].ReadOnly = true;
                //        dtRow.Cells["Global_Price"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Amount_Jpy"].ReadOnly = true;
                //        dtRow.Cells["Amount_Jpy"].Style.BackColor = Color.Gray;
                //    }
                //}

                ////---------------- Gridview PackingList ----------------//
                ////---- Disable ----//
                //foreach (DataGridViewRow dtRow in GridView_PackingList.Rows)
                //{
                //    if (!dtRow.IsNewRow)
                //    { 
                //        dtRow.Cells["Customer_Code"].ReadOnly = true;
                //        dtRow.Cells["Customer_Code"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Packages_No"].ReadOnly = true;
                //        dtRow.Cells["Packages_No"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Customer_ItemCode"].ReadOnly = true;
                //        dtRow.Cells["Customer_ItemCode"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["TVC_ItemCode"].ReadOnly = true;
                //        dtRow.Cells["TVC_ItemCode"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Customer_PO"].ReadOnly = true;
                //        dtRow.Cells["Customer_PO"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Qty_Carton"].ReadOnly = true;
                //        dtRow.Cells["Qty_Carton"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Qty_Per_Carton"].ReadOnly = true;
                //        dtRow.Cells["Qty_Per_Carton"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Qty_Total"].ReadOnly = true;
                //        dtRow.Cells["Qty_Total"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Qty_Total_Revise"].ReadOnly = true;
                //        dtRow.Cells["Qty_Total_Revise"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Net_Weight"].ReadOnly = true;
                //        dtRow.Cells["Net_Weight"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Net_Weight_Total"].ReadOnly = true;
                //        dtRow.Cells["Net_Weight_Total"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Gross_Weight"].ReadOnly = true;
                //        dtRow.Cells["Gross_Weight"].Style.BackColor = Color.Gray;

                //        dtRow.Cells["Lot_No"].ReadOnly = true;
                //        dtRow.Cells["Lot_No"].Style.BackColor = Color.Gray;
                //    }
                //}
            }
        }

        private void GridView_Invoice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int columnIndex = 0;
            int rowIndex = 0;
            String _unitCurrency = "";
            DateTime _dateCreateInvoice = Convert.ToDateTime(dateEdit_DateCreateShipping.EditValue);

            if (e.RowIndex >= 0)
            {
                rowIndex = e.RowIndex;
                columnIndex = e.ColumnIndex;

                //if (GridView_Invoice.Columns[columnIndex] is DataGridViewButtonColumn)
                //{
                //    _listItemCodeInfo = null;
                    //if(GridView_Invoice.Rows.Count > 1)
                    //{
                    //    foreach (DataGridViewRow dr in GridView_Invoice.Rows)
                    //    {
                    //        if (dr.Cells["Unit_Currency"].Value != null)
                    //        {
                    //            _unitCurrency = dr.Cells["Unit_Currency"].Value.ToString();
                    //            break;
                    //        }
                    //    }
                    //}
                    Form_Search_PO_New _formSearch = new Form_Search_PO_New(
                        _systemDAL
                        , Convert.ToString(cb_CompanyCode.SelectedValue)
                        , Convert.ToString(sLookUp_IssuedTo_CompanyCode.EditValue)
                        , _unitCurrency
                        , _dateCreateInvoice
                        , this);
                    _formSearch.StartPosition = FormStartPosition.CenterParent;
                    _formSearch.Show();

                    ////Move data from form search to current Form
                    //GetSelectedItem(_listItemCodeInfo);

                    ////Sum Invoice
                    //btnSumInvoice_Click(sender, e);

                    ////Sum packing List
                    //btnSumPL_Click(sender, e);

                    //GridView_Invoice.ClearSelection();
                    //int nRowIndex = GridView_Invoice.Rows.Count - 1;

                    //GridView_Invoice.Rows[nRowIndex].Selected = true;
                    //GridView_Invoice.Rows[nRowIndex].Cells[8].Selected = true;
                //}
            }
        }

        public void RefreshGridView(object sender, EventArgs e)
        {
            ////Load Invoice
            //btn_SearchShipping_Click(sender, e);

            ////Setting enable item
            //SettingInitGridView();
            MessageBox.Show("Clicked");
        }

        private void GridView_Invoice_KeyDown(object sender, KeyEventArgs e)
        {
            //int _RowIndex = 0;
            //int _ColumnIndex = 0;

            if (e.KeyCode == Keys.Tab)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }

            if (e.KeyCode == Keys.F5)
            {
                ////Get row index
                //int rowIndex = 0;
                //rowIndex = GridView_Invoice.CurrentCell.RowIndex;
                ////Get column index
                //int columnIndex = 0;
                //columnIndex = GridView_Invoice.CurrentCell.ColumnIndex;
                //String _unitCurrency = "";
                //DateTime _dateCreateInvoice = dtpDateCreateShipping.Value;

                //if (rowIndex >= 0)
                //{
                //    _listItemCodeInfo = null;
                //    if (GridView_Invoice.Rows.Count > 1)
                //    {
                //        foreach (DataGridViewRow dr in GridView_Invoice.Rows)
                //        {
                //            if (dr.Cells["Unit_Currency"].Value != null)
                //            {
                //                _unitCurrency = dr.Cells["Unit_Currency"].Value.ToString();
                //                break;
                //            }
                //        }
                //    }
                //    Form_Search_PO _formSearch = new Form_Search_PO(_systemDAL,"btnSearch_ItemCode", txtIssuedTo_CompanyCode.Text, _unitCurrency, _dateCreateInvoice);
                //    _formSearch.StartPosition = FormStartPosition.CenterParent;
                //    _formSearch.ShowDialog();

                //    //
                //    GetSelectedItem(_listItemCodeInfo);

                //    //Sum Invoice
                //    btnSumInvoice_Click(sender, e);

                //    //Sum packing List
                //    btnSumPL_Click(sender, e);

                //    GridView_Invoice.ClearSelection();
                //    int nRowIndex = GridView_Invoice.Rows.Count - 1;

                //    GridView_Invoice.Rows[nRowIndex].Selected = true;
                //    GridView_Invoice.Rows[nRowIndex].Cells[8].Selected = true;
                //}
            }

            //if (e.Control && e.KeyCode == Keys.C)
            //{
            //    if (GridView_Invoice.SelectedCells.Count == 1
            //        && (GridView_Invoice.CurrentCell.Value != null))
            //    {
            //        _RowIndex = GridView_Invoice.CurrentCell.RowIndex;
            //        _ColumnIndex = GridView_Invoice.CurrentCell.ColumnIndex;
            //         Clipboard.SetText(GridView_Invoice.Rows[_RowIndex].Cells[_ColumnIndex].Value.ToString());
            //    }
            //}

            //if (e.Control && e.KeyCode == Keys.V)
            //{
            //    if (GridView_Invoice.SelectedCells.Count == 1)
            //    {
            //        _RowIndex = GridView_Invoice.CurrentCell.RowIndex;
            //        _ColumnIndex = GridView_Invoice.CurrentCell.ColumnIndex;
            //        GridView_Invoice.Rows[_RowIndex].Cells[_ColumnIndex].Value = Clipboard.GetText();
            //    }
            //}

            //if (e.KeyCode == Keys.Delete)
            //{
            //    string exitMessageText = "Do you want to remove this row?";
            //    string exitCaption = "Confirm";
            //    MessageBoxButtons button = MessageBoxButtons.YesNo;
            //    DialogResult res = MessageBox.Show(exitMessageText, exitCaption, button, MessageBoxIcon.Exclamation);
            //    if (res == DialogResult.Yes)
            //    {
            //        //Get row index
            //        int rowIndex = GridView_Invoice.CurrentRow.Index;
            //        string Inv_Customer_PO = "";
            //        string PL_Customer_PO = "";
            //        int index = 0;
            //        List<int> list = new List<int>();

            //        if (GridView_Invoice.Rows[rowIndex].Cells["Customer_PO"].Value != null)
            //        {
            //            Inv_Customer_PO = GridView_Invoice.Rows[rowIndex].Cells["Customer_PO"].Value.ToString();
            //        }

            //        if (!String.IsNullOrEmpty(Inv_Customer_PO))
            //        { 
            //            foreach (DataGridViewRow row in GridView_PackingList.Rows)
            //            {
            //                PL_Customer_PO = Convert.ToString(row.Cells["Customer_PO"].Value);
            //                if (PL_Customer_PO == Inv_Customer_PO)
            //                {
            //                    tabControl.SelectedIndex = 1;
            //                    //
            //                    exitMessageText = "Do you want to remove row 「" + (index + 1) + "」.\nCustomer PO「" + Inv_Customer_PO + "」 of 「Packing List」 ?";
            //                    exitCaption = "Confirm";
            //                    res = MessageBox.Show(exitMessageText, exitCaption, button, MessageBoxIcon.Exclamation);
            //                    if (res == DialogResult.Yes)
            //                    {
            //                        list.Add(index);
            //                    }
            //                }
            //                index++;
            //            }
            //            foreach (int indexRemove in list.AsEnumerable().Reverse())
            //            {
            //                GridView_PackingList.Rows.RemoveAt(indexRemove);
            //            }
            //        }
            //    } else
            //    {
            //        e.Handled = true;
            //    }
            //}

            //if (e.Control && e.KeyCode == Keys.F)
            //{
            //    txt_Search_Grid.Focus();
            //}

        }

        private void GridView_Invoice_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //decimal quantity, quantity_revise, price, price_revise, price_compare, amount, USD_Rate, balance;
            //string _customer_PO = "";

            //if (e.RowIndex != GridView_Invoice.NewRowIndex)
            //{
            //    //Normal data
            //    if (radNormal.Checked == true)
            //    {
            //        if (GridView_Invoice.Rows[e.RowIndex].Cells["Quantity"].Value != null
            //        && GridView_Invoice.Rows[e.RowIndex].Cells["Order_Price"].Value != null)
            //        {
            //            if (decimal.TryParse(GridView_Invoice.Rows[e.RowIndex].Cells["Quantity"].Value.ToString(), out quantity)
            //            && decimal.TryParse(GridView_Invoice.Rows[e.RowIndex].Cells["Order_Price"].Value.ToString(), out price))
            //            {
            //                if (GridView_Invoice.Rows[e.RowIndex].Cells["Balance"].Value != null)
            //                {
            //                    decimal.TryParse(GridView_Invoice.Rows[e.RowIndex].Cells["Balance"].Value.ToString(), out balance);
            //                    if (quantity > balance)
            //                    {
            //                        MessageBox.Show("Số lượng còn lại: " + balance + ".\nSố lượng nhập: " + quantity, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                    }
            //                }

            //                if (GridView_Invoice.Rows[e.RowIndex].Cells["Customer_PO"].Value != null)
            //                {
            //                    _customer_PO = GridView_Invoice.Rows[e.RowIndex].Cells["Customer_PO"].Value.ToString();
            //                }

            //                if (GridView_Invoice.Rows[e.RowIndex].Cells["Unit_Currency"].Value.ToString().ToUpper() == "JPY")
            //                {
            //                    //Cal Amount
            //                    amount = quantity * price;
            //                    GridView_Invoice.Rows[e.RowIndex].Cells["Amount_Jpy"].Value = Math.Round(amount, 0, MidpointRounding.AwayFromZero);
            //                }
            //                else if (GridView_Invoice.Rows[e.RowIndex].Cells["Unit_Currency"].Value.ToString().ToUpper() == "USD")
            //                {
            //                    //Cal Amount(USD)
            //                    amount = quantity * price;
            //                    GridView_Invoice.Rows[e.RowIndex].Cells["Amount_Jpy"].Value = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
            //                }
            //            }
            //        }

            //        //Background red when quantity = 0
            //        if (GridView_Invoice.Rows[e.RowIndex].Cells["Quantity"].Value != null)
            //        {
            //            if (Convert.ToDecimal(GridView_Invoice.Rows[e.RowIndex].Cells["Quantity"].Value) == 0)
            //            {
            //                GridView_Invoice.Rows[e.RowIndex].Cells["Quantity"].Style.BackColor = Color.Red;
            //            }
            //            else
            //            {
            //                GridView_Invoice.Rows[e.RowIndex].Cells["Quantity"].Style.BackColor = Color.White;
            //            }
            //        }

            //        //Background red when order different from global price
            //        if (GridView_Invoice.Rows[e.RowIndex].Cells["Order_Price"].Value != null
            //         && GridView_Invoice.Rows[e.RowIndex].Cells["Global_Price"].Value != null)
            //        {
            //            if (decimal.TryParse(GridView_Invoice.Rows[e.RowIndex].Cells["Order_Price"].Value.ToString(), out price)
            //             && decimal.TryParse(GridView_Invoice.Rows[e.RowIndex].Cells["Global_Price"].Value.ToString(), out price_compare))
            //            {
            //                if (price == price_compare)
            //                {
            //                    GridView_Invoice.Rows[e.RowIndex].Cells["Order_Price"].Style.BackColor = Color.White;
            //                    GridView_Invoice.Rows[e.RowIndex].Cells["Global_Price"].Style.BackColor = Color.White;
            //                }
            //                else
            //                {
            //                    GridView_Invoice.Rows[e.RowIndex].Cells["Order_Price"].Style.BackColor = Color.Red;
            //                    GridView_Invoice.Rows[e.RowIndex].Cells["Global_Price"].Style.BackColor = Color.Red;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            GridView_Invoice.Rows[e.RowIndex].Cells["Order_Price"].Style.BackColor = Color.Red;
            //            GridView_Invoice.Rows[e.RowIndex].Cells["Global_Price"].Style.BackColor = Color.Red;
            //        }

            //        if (GridView_Invoice.Rows[e.RowIndex].Cells["USD_Rate"].Value != null)
            //        {
            //            USD_Rate = decimal.Parse(GridView_Invoice.Rows[e.RowIndex].Cells["USD_Rate"].Value.ToString());
            //            foreach (DataGridViewRow dr in GridView_Invoice.Rows)
            //            {
            //                if (dr.Cells["TVC_ItemCode"].Value != null)
            //                {
            //                    dr.Cells["USD_Rate"].Value = USD_Rate;
            //                }
            //            }
            //        }
            //    }

            //    //Revise data
            //    if (radRevise.Checked == true)
            //    {
            //        if (GridView_Invoice.Rows[e.RowIndex].Cells["Quantity_Revise"].Value != null
            //         && GridView_Invoice.Rows[e.RowIndex].Cells["Order_Price_Revise"].Value != null)
            //        {
            //            if (decimal.TryParse(GridView_Invoice.Rows[e.RowIndex].Cells["Quantity_Revise"].Value.ToString(), out quantity_revise)
            //             && decimal.TryParse(GridView_Invoice.Rows[e.RowIndex].Cells["Order_Price_Revise"].Value.ToString(), out price_revise))
            //            {
            //                if (GridView_Invoice.Rows[e.RowIndex].Cells["Unit_Currency"].Value.ToString().ToUpper() == "JPY")
            //                {
            //                    //Cal Amount
            //                    amount = quantity_revise * price_revise;
            //                    GridView_Invoice.Rows[e.RowIndex].Cells["Amount_Jpy"].Value = Math.Round(amount, 0, MidpointRounding.AwayFromZero);
            //                }
            //                else if (GridView_Invoice.Rows[e.RowIndex].Cells["Unit_Currency"].Value.ToString().ToUpper() == "USD")
            //                {
            //                    //Cal Amount(USD)
            //                    amount = quantity_revise * price_revise;
            //                    GridView_Invoice.Rows[e.RowIndex].Cells["Amount_Jpy"].Value = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
            //                }
            //            }
            //        }
            //    }

            //    //Sum Invoice
            //    btnSumInvoice_Click(sender, e);

            //    //Sum packing List
            //    btnSumPL_Click(sender, e);
            //}
        }

        private void GridView_PackingList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }

        }
        #endregion

        #region Event
        private void radNormal_CheckedChanged(object sender, EventArgs e)
        {
            SettingInitGridView();
        }

        private void radLock_CheckedChanged(object sender, EventArgs e)
        {
            SettingInitGridView();
        }

        private void radRevise_CheckedChanged(object sender, EventArgs e)
        {
            SettingInitGridView();
        }

        private void btnSave_Data_Click(object sender, EventArgs e)
        {
            //int Version = 0;
            if ((MessageBox.Show("Xác nhận lưu dữ liệu?", "Xác Nhận"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Question
                , MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                if (CheckError() == true)
                {
                    string _invoiceNo = txtInvoiceNo.Text.Trim();
                    string _shippingNo = Convert.ToString(sLookUp_ShippingNo.EditValue).Trim();
                    DataTable dtInvoiceMS = new DataTable();
                    DataTable dtInvoiceDetail = new DataTable();
                    DataTable dtPackingListDetail = new DataTable();

                    #region define dtInvoiceMS
                    //Define dtInvoiceMS 
                    //LOCK STATUS (0 : NORMAL, 1 : LOCK, 2 : REVISE)
                    dtInvoiceMS.Columns.Add("LockStatus");
                    dtInvoiceMS.Columns["LockStatus"].DataType = typeof(Int32);
                    //COMPANY CODE
                    dtInvoiceMS.Columns.Add("CompanyCode");
                    //INVOICE NO
                    dtInvoiceMS.Columns.Add("ShippingNo");
                    //INVOICE NO
                    dtInvoiceMS.Columns.Add("InvoiceNo");
                    //DATE CREATE INVOIE
                    dtInvoiceMS.Columns.Add("DateCreate");
                    dtInvoiceMS.Columns["DateCreate"].DataType = typeof(DateTime);
                    //ISSUED TO
                    dtInvoiceMS.Columns.Add("IssuedToCode");
                    dtInvoiceMS.Columns.Add("IssuedToName");
                    dtInvoiceMS.Columns.Add("IssuedToAddress");
                    dtInvoiceMS.Columns.Add("IssuedTelNo");
                    dtInvoiceMS.Columns.Add("IssuedFaxNo");
                    //SHIP TO
                    dtInvoiceMS.Columns.Add("ShipToCode");
                    dtInvoiceMS.Columns.Add("ShipToName");
                    dtInvoiceMS.Columns.Add("ShipToAddress");
                    dtInvoiceMS.Columns.Add("ShipToTelNo");
                    dtInvoiceMS.Columns.Add("ShipToFaxNo");
                    //REVENUE
                    dtInvoiceMS.Columns.Add("Revenue");
                    //SHIP VIA
                    dtInvoiceMS.Columns.Add("ShipVia");
                    //FREIGHT
                    dtInvoiceMS.Columns.Add("Freight");
                    dtInvoiceMS.Columns["Freight"].DataType = typeof(Int32);
                    //VESSEL
                    dtInvoiceMS.Columns.Add("Vessel");
                    //PORT OF LOADING
                    dtInvoiceMS.Columns.Add("PortLoading");
                    //PORT OF DESTINATION
                    dtInvoiceMS.Columns.Add("PortDestination");
                    //ETD
                    dtInvoiceMS.Columns.Add("ETD");
                    dtInvoiceMS.Columns["ETD"].DataType = typeof(DateTime);
                    //ETA
                    dtInvoiceMS.Columns.Add("ETA");
                    dtInvoiceMS.Columns["ETA"].DataType = typeof(DateTime);
                    //TRADE CONDITION
                    dtInvoiceMS.Columns.Add("TradeCondition");
                    //PAYMENT TERM
                    dtInvoiceMS.Columns.Add("PaymentTerm");
                    //CREATE BY
                    dtInvoiceMS.Columns.Add("CreateBy");
                    //CREATE AT
                    dtInvoiceMS.Columns.Add("CreateAt");
                    dtInvoiceMS.Columns["CreateAt"].DataType = typeof(DateTime);
                    //EDIT BY
                    dtInvoiceMS.Columns.Add("EditBy");
                    //EDIT AT
                    dtInvoiceMS.Columns.Add("EditAt");
                    dtInvoiceMS.Columns["EditAt"].DataType = typeof(DateTime);
                    #endregion

                    #region define dtInvoiceDetail
                    //Define dtInvoiceDetail
                    //COMPANY CODE
                    dtInvoiceDetail.Columns.Add("CompanyCode");
                    //CUSTOMER CODE
                    dtInvoiceDetail.Columns.Add("CustomerCode");
                    //SHIPPING NO
                    dtInvoiceDetail.Columns.Add("ShippingNo");
                    //INVOICE NO
                    dtInvoiceDetail.Columns.Add("InvoiceNo");
                    //REVISE NO
                    dtInvoiceDetail.Columns.Add("ReviseNo");
                    //REVISE DATE
                    dtInvoiceDetail.Columns.Add("ReviseDate");
                    dtInvoiceDetail.Columns["ReviseDate"].DataType = typeof(DateTime);
                    //VERSION
                    dtInvoiceDetail.Columns.Add("Version");
                    dtInvoiceDetail.Columns["Version"].DataType = typeof(Int32);
                    //ITEM NAME
                    dtInvoiceDetail.Columns.Add("ItemName");
                    //CUS ITEM CODE
                    dtInvoiceDetail.Columns.Add("Cus_ItemCode");
                    //TVC ITEM CODE
                    dtInvoiceDetail.Columns.Add("Tvc_ItemCode");
                    //REF PO No.
                    dtInvoiceDetail.Columns.Add("Customer_PO");
                    //THIRD PARTY ITEM CODE
                    dtInvoiceDetail.Columns.Add("ThirdParty_ItemCode");
                    //THIRD PARTY PO
                    dtInvoiceDetail.Columns.Add("ThirdParty_PO");
                    //ORDER DATE
                    dtInvoiceDetail.Columns.Add("Order_Date");
                    dtInvoiceDetail.Columns["Order_Date"].DataType = typeof(DateTime);
                    //DUE DATE PO
                    dtInvoiceDetail.Columns.Add("DueDate_PO");
                    dtInvoiceDetail.Columns["DueDate_PO"].DataType = typeof(DateTime);
                    //QUANTITY
                    dtInvoiceDetail.Columns.Add("Quantity");
                    dtInvoiceDetail.Columns["Quantity"].DataType = typeof(Decimal);
                    //QUANTITY REVISE
                    dtInvoiceDetail.Columns.Add("QuantityRevise");
                    dtInvoiceDetail.Columns["QuantityRevise"].DataType = typeof(Decimal);
                    //BALANCE
                    dtInvoiceDetail.Columns.Add("Balance");
                    dtInvoiceDetail.Columns["Balance"].DataType = typeof(Decimal);
                    //UNIT PRICE
                    dtInvoiceDetail.Columns.Add("Unit_Currency");
                    //USD RATE
                    dtInvoiceDetail.Columns.Add("USD_Rate");
                    dtInvoiceDetail.Columns["USD_Rate"].DataType = typeof(Decimal);
                    //ORDER PRICE
                    dtInvoiceDetail.Columns.Add("OrderPrice");
                    dtInvoiceDetail.Columns["OrderPrice"].DataType = typeof(Decimal);
                    //ORDER PRICE REVISE
                    dtInvoiceDetail.Columns.Add("OrderPriceRevise");
                    dtInvoiceDetail.Columns["OrderPriceRevise"].DataType = typeof(Decimal);
                    //GLOBAL PRICE
                    dtInvoiceDetail.Columns.Add("Global_Price");
                    dtInvoiceDetail.Columns["Global_Price"].DataType = typeof(Decimal);
                    //AMOUNT
                    dtInvoiceDetail.Columns.Add("Amount");
                    dtInvoiceDetail.Columns["Amount"].DataType = typeof(Decimal);
                    //NOTE
                    dtInvoiceDetail.Columns.Add("Note");
                    //CREATE BY
                    dtInvoiceDetail.Columns.Add("CreateBy");
                    //CREATE AT
                    dtInvoiceDetail.Columns.Add("CreateAt");
                    dtInvoiceDetail.Columns["CreateAt"].DataType = typeof(DateTime);
                    //EDIT BY
                    dtInvoiceDetail.Columns.Add("EditBy");
                    //EDIT AT
                    dtInvoiceDetail.Columns.Add("EditAt");
                    dtInvoiceDetail.Columns["EditAt"].DataType = typeof(DateTime);
                    #endregion

                    #region define dtPackingListDetail
                    //Define dtPackingListDetail
                    //COMPANY CODE
                    dtPackingListDetail.Columns.Add("CompanyCode");
                    //CUSTOMER CODE
                    dtPackingListDetail.Columns.Add("CustomerCode");
                    //SHIPPING NO
                    dtPackingListDetail.Columns.Add("ShippingNo");
                    //INVOICE NO
                    dtPackingListDetail.Columns.Add("InvoiceNo");
                    //REVISE NO
                    dtPackingListDetail.Columns.Add("ReviseNo");
                    //REVISE DATE
                    dtPackingListDetail.Columns.Add("ReviseDate");
                    dtPackingListDetail.Columns["ReviseDate"].DataType = typeof(DateTime);
                    //VERSION
                    dtPackingListDetail.Columns.Add("Version");
                    dtPackingListDetail.Columns["Version"].DataType = typeof(Int32);
                    //PACKAGES NO
                    dtPackingListDetail.Columns.Add("PackagesNo");
                    //CUSTOMER ITEM CODE
                    dtPackingListDetail.Columns.Add("Customer_ItemCode");
                    //TVC ITEM CODE
                    dtPackingListDetail.Columns.Add("TVC_ItemCode");
                    //CUSTOMER PO
                    dtPackingListDetail.Columns.Add("Customer_PO");
                    //QUANTITY OF CARTON
                    dtPackingListDetail.Columns.Add("QtyCarton");
                    //QUANTITY PER CARTON
                    dtPackingListDetail.Columns.Add("QtyPerCarton");
                    //QUANTITY TOTAL
                    dtPackingListDetail.Columns.Add("QuantityTotal");
                    //QUANTITY TOTAL REVISE
                    dtPackingListDetail.Columns.Add("QuantityTotalRevise");
                    dtPackingListDetail.Columns["QuantityTotalRevise"].DataType = typeof(Decimal);
                    //NET WEIGHT
                    dtPackingListDetail.Columns.Add("NetWeight");
                    dtPackingListDetail.Columns["NetWeight"].DataType = typeof(Decimal);
                    //NET WEIGHT TOTAL
                    dtPackingListDetail.Columns.Add("NetWeight_Total");
                    dtPackingListDetail.Columns["NetWeight_Total"].DataType = typeof(Decimal);
                    //GROSS WEIGHT
                    dtPackingListDetail.Columns.Add("GrossWeight");
                    dtPackingListDetail.Columns["GrossWeight"].DataType = typeof(Decimal);
                    //LOT NO
                    dtPackingListDetail.Columns.Add("LotNo");
                    //CREATE BY
                    dtPackingListDetail.Columns.Add("CreateBy");
                    //CREATE AT
                    dtPackingListDetail.Columns.Add("CreateAt");
                    dtPackingListDetail.Columns["CreateAt"].DataType = typeof(DateTime);
                    //EDIT BY
                    dtPackingListDetail.Columns.Add("EditBy");
                    //EDIT AT
                    dtPackingListDetail.Columns.Add("EditAt");
                    dtPackingListDetail.Columns["EditAt"].DataType = typeof(DateTime);
                    #endregion

                    //Add data to dtInvoiceMS
                    DataRow invoiceMS = dtInvoiceMS.NewRow();
                    if (radNormal.Checked == true)
                    {
                        invoiceMS["LockStatus"] = EnumRevise.Normal;
                    }
                    else if (radLock.Checked == true)
                    {
                        invoiceMS["LockStatus"] = EnumRevise.Lock;
                    }
                    else if (radRevise.Checked == true)
                    {
                        invoiceMS["LockStatus"] = EnumRevise.Revise;
                    }
                    invoiceMS["CompanyCode"] = cb_CompanyCode.SelectedValue;
                    invoiceMS["ShippingNo"] = _shippingNo;
                    invoiceMS["InvoiceNo"] = _invoiceNo;
                    invoiceMS["InvoiceNo"] = "";
                    invoiceMS["DateCreate"] = dateEdit_DateCreateShipping.EditValue;
                    invoiceMS["IssuedToCode"] = Convert.ToString(sLookUp_IssuedTo_CompanyCode.EditValue).Trim();
                    invoiceMS["IssuedToName"] = txtIssuedTo_CompanyName.Text.Trim();
                    invoiceMS["IssuedToAddress"] = Convert.ToString(memo_IssuedTo_CompanyAddress.EditValue).Trim();
                    invoiceMS["IssuedTelNo"] = txtIssuedTo_TelNo.Text.Trim();
                    invoiceMS["IssuedFaxNo"] = txtIssuedTo_FaxNo.Text.Trim();
                    invoiceMS["ShipToCode"] = sLookUp_ShipTo_CompanyCode.Text.Trim();
                    invoiceMS["ShipToName"] = txtShipTo_CompanyName.Text.Trim();
                    invoiceMS["ShipToAddress"] = Convert.ToString(memo_ShipTo_CompanyAddress.EditValue).Trim();
                    invoiceMS["ShipToTelNo"] = txtShipTo_TelNo.Text.Trim();
                    invoiceMS["ShipToFaxNo"] = txtShipTo_FaxNo.Text.Trim();
                    invoiceMS["Revenue"] = Convert.ToString(dateEdit_Revenue.EditValue);
                    invoiceMS["ShipVia"] = txtShipVia.Text.Trim();
                    invoiceMS["Freight"] = Convert.ToInt32(cb_Freight.SelectedValue);
                    invoiceMS["Vessel"] = txtVessel.Text.Trim();
                    invoiceMS["PortLoading"] = Convert.ToString(sLookUp_PortLoading.EditValue).Trim();
                    invoiceMS["PortDestination"] = Convert.ToString(sLookUp_PortDestination).Trim();
                    invoiceMS["ETD"] = dateEdit_ETD.EditValue;
                    invoiceMS["ETA"] = dateEdit_ETA.EditValue;
                    invoiceMS["TradeCondition"] = Convert.ToString(sLookUp_PriceCondition.EditValue).Trim();
                    invoiceMS["PaymentTerm"] = Convert.ToString(sLookUp_PaymentTerm.EditValue).Trim();
                    invoiceMS["CreateBy"] = _systemDAL.UserName;
                    invoiceMS["CreateAt"] = DateTime.Now;
                    invoiceMS["EditBy"] = _systemDAL.UserName;
                    invoiceMS["EditAt"] = DateTime.Now;
                    dtInvoiceMS.Rows.Add(invoiceMS);

                    ////Grid Invoice
                    //foreach (DataGridViewRow row in GridView_Invoice.Rows)
                    //{
                    //    if (row.Cells["Tvc_ItemCode"].Value != null)
                    //    {
                    //        DataRow invoiceDetail = dtInvoiceDetail.NewRow();
                    //        invoiceDetail["CompanyCode"] = _systemDAL.CompanyCode;
                    //        invoiceDetail["CustomerCode"] = row.Cells["Customer_Code"].Value.ToString();
                    //        invoiceDetail["ShippingNo"] = _shippingNo;
                    //        invoiceDetail["InvoiceNo"] = _invoiceNo;
                    //        //Mode: Add new
                    //        if (radNormal.Checked == true)
                    //        {
                    //            invoiceDetail["ReviseNo"] = "";
                    //            invoiceDetail["ReviseDate"] = DateTime.Now;
                    //            invoiceDetail["Version"] = Number.Zero;                     //Init = 0
                    //        }
                    //        else if (radRevise.Checked == true)
                    //        {
                    //            invoiceDetail["ReviseNo"] = _shippingNo + "_Revise_" + Convert.ToInt32(Version + 1);
                    //            invoiceDetail["ReviseDate"] = DateTime.Now;
                    //            invoiceDetail["Version"] = Convert.ToInt32(Version + 1);    //Init = 0
                    //        }
                    //        invoiceDetail["ItemName"] = row.Cells["Part_Description"].Value.ToString();
                    //        invoiceDetail["Cus_ItemCode"] = row.Cells["Cus_ItemCode"].Value.ToString();
                    //        invoiceDetail["Tvc_ItemCode"] = row.Cells["Tvc_ItemCode"].Value.ToString();
                    //        invoiceDetail["Customer_PO"] = row.Cells["Customer_PO"].Value.ToString();
                    //        if (row.Cells["ThirdParty_ItemCode"].Value != null)
                    //        { 
                    //            invoiceDetail["ThirdParty_ItemCode"] = row.Cells["ThirdParty_ItemCode"].Value.ToString();
                    //        }
                    //        if (row.Cells["ThirdParty_PO"].Value != null) {
                    //            invoiceDetail["ThirdParty_PO"] = row.Cells["ThirdParty_PO"].Value.ToString();
                    //        }
                    //        if (row.Cells["Order_Date"].Value != null)
                    //        { 
                    //            invoiceDetail["Order_Date"] = Convert.ToDateTime(row.Cells["Order_Date"].Value);
                    //        }
                    //        if (row.Cells["DueDate_PO"].Value != null) { 
                    //            invoiceDetail["DueDate_PO"] = Convert.ToDateTime(row.Cells["DueDate_PO"].Value);
                    //        }
                    //        invoiceDetail["Quantity"] = Convert.ToDecimal(row.Cells["Quantity"].Value);
                    //        if (radRevise.Checked == true)
                    //        { 
                    //            invoiceDetail["QuantityRevise"] = Convert.ToDecimal(row.Cells["Quantity_Revise"].Value);
                    //        } else if (radNormal.Checked == true || radLock.Checked == true)
                    //        {
                    //            invoiceDetail["QuantityRevise"] = 0;
                    //        }
                    //        invoiceDetail["Balance"] = Convert.ToDecimal(row.Cells["Balance"].Value);
                    //        invoiceDetail["Unit_Currency"] = row.Cells["Unit_Currency"].Value.ToString();
                    //        invoiceDetail["USD_Rate"] = row.Cells["USD_Rate"].Value;
                    //        invoiceDetail["OrderPrice"] = Convert.ToDecimal(row.Cells["Order_Price"].Value);
                    //        if (radRevise.Checked == true) { 
                    //            invoiceDetail["OrderPriceRevise"] = Convert.ToDecimal(row.Cells["Order_Price_Revise"].Value);
                    //        }
                    //        else if (radNormal.Checked == true || radLock.Checked== true)
                    //        {
                    //            invoiceDetail["OrderPriceRevise"] = 0;
                    //        }
                    //        invoiceDetail["Global_Price"] = Convert.ToDecimal(row.Cells["Global_Price"].Value);
                    //        invoiceDetail["Amount"] = Convert.ToDecimal(row.Cells["Amount_Jpy"].Value);
                    //        dtInvoiceDetail.Rows.Add(invoiceDetail);
                    //    }
                    //}

                    //Check trùng dữ liệu Invoice trước khi lưu
                    var count_Duplication_Inv = dtPackingListDetail.AsEnumerable().GroupBy(
                                                            x => new {
                                                                CompanyCode = x.Field<string>("CompanyCode"),
                                                                ShippingNo = x.Field<string>("ShippingNo"),
                                                                Tvc_ItemCode = x.Field<string>("Tvc_ItemCode"),
                                                                Customer_PO = x.Field<string>("Customer_PO"),
                                                                DueDate_PO = x.Field<DateTime>("DueDate_PO"),
                                                            }).Where(x => x.Count() > 1);

                    //Nếu có dữ liệu trùng thì xuất ra thông báo lỗi
                    if (count_Duplication_Inv != null && count_Duplication_Inv.Any())
                    {
                        foreach (var group in count_Duplication_Inv)
                        {
                            foreach (DataRow row in group)
                            {
                                MessageBox.Show("Trùng dữ liệu Invoice." + "\nTVC Item Code: " + row.Field<string>("Tvc_ItemCode")
                                                    + "\nCustomer_PO: " + row.Field<string>("Customer_PO")
                                                    + "\nShipping No: " + row.Field<string>("ShippingNo")
                                                    , "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                            break;
                        }
                        return;
                    }

                    ////Grid Packing List
                    //foreach (DataGridViewRow row in GridView_PackingList.Rows)
                    //{
                    //    if (row.Cells["Customer_ItemCode"].Value != null)
                    //    {
                    //        DataRow packingListDetail = dtPackingListDetail.NewRow();
                    //        packingListDetail["CompanyCode"] = _systemDAL.CompanyCode;
                    //        packingListDetail["CustomerCode"] = row.Cells["Customer_Code"].Value.ToString();
                    //        packingListDetail["ShippingNo"] = _shippingNo;
                    //        packingListDetail["InvoiceNo"] = _invoiceNo;
                    //        //Mode: Add new
                    //        if (radNormal.Checked == true)
                    //        {
                    //            packingListDetail["ReviseNo"] = _shippingNo;
                    //            packingListDetail["ReviseDate"] = DateTime.Now;
                    //            packingListDetail["Version"] = Number.Zero;                     //Init = 0
                    //        }
                    //        else if (radRevise.Checked == true)
                    //        {
                    //            packingListDetail["ReviseNo"] = _shippingNo + "_Revise_" + Convert.ToInt32(Version + 1);
                    //            packingListDetail["ReviseDate"] = DateTime.Now;
                    //            packingListDetail["Version"] = Convert.ToInt32(Version + 1);    //Init = 0
                    //        }
                    //        if (row.Cells["Packages_No"].Value != null)
                    //        { 
                    //            packingListDetail["PackagesNo"] = row.Cells["Packages_No"].Value.ToString();
                    //        }
                    //        packingListDetail["Customer_ItemCode"] = row.Cells["Customer_ItemCode"].Value.ToString();
                    //        packingListDetail["TVC_ItemCode"] = row.Cells["TVC_ItemCode"].Value.ToString();
                    //        packingListDetail["Customer_PO"] = row.Cells["Customer_PO"].Value.ToString();
                    //        packingListDetail["QtyCarton"] = Convert.ToDecimal(row.Cells["Qty_Carton"].Value);
                    //        packingListDetail["QtyPerCarton"] = row.Cells["Qty_Per_Carton"].Value.ToString();
                    //        packingListDetail["QuantityTotal"] = Convert.ToDecimal(row.Cells["Qty_Total"].Value);
                    //        if (radRevise.Checked == true)
                    //        {
                    //            packingListDetail["QuantityTotalRevise"] = Convert.ToDecimal(row.Cells["Qty_Total_Revise"].Value);
                    //        } else if (radNormal.Checked == true || radLock.Checked == true)
                    //        {
                    //            packingListDetail["QuantityTotalRevise"] = 0;
                    //        }
                    //        packingListDetail["NetWeight"] = Convert.ToDecimal(row.Cells["Net_Weight"].Value);
                    //        packingListDetail["NetWeight_Total"] = Convert.ToDecimal(row.Cells["Net_Weight_Total"].Value);
                    //        packingListDetail["GrossWeight"] = Convert.ToDecimal(row.Cells["Gross_Weight"].Value);
                    //        packingListDetail["LotNo"] = row.Cells["Lot_No"].Value.ToString();
                    //        dtPackingListDetail.Rows.Add(packingListDetail);
                    //    }
                    //}

                    //Check trùng dữ liệu Packing List trước khi lưu
                    var count_Duplication_PL = dtPackingListDetail.AsEnumerable().GroupBy(
                                                            x => new {
                                                                CompanyCode = x.Field<string>("CompanyCode")
                                                                ,ShippingNo = x.Field<string>("ShippingNo")
                                                                ,PackagesNo = x.Field<string>("PackagesNo")
                                                                ,Customer_ItemCode = x.Field<string>("Customer_ItemCode")
                                                                ,Customer_PO = x.Field<string>("Customer_PO")
                                                                ,QtyPerCarton = x.Field<string>("QtyPerCarton")
                                                                ,QuantityTotal = x.Field<string>("QuantityTotal")
                                                                ,NetWeight = x.Field<decimal>("NetWeight")
                                                                ,LotNo = x.Field<string>("LotNo")
                                                            }).Where(x => x.Count() > 1);

                    //Nếu có dữ liệu trùng thì xuất ra thông báo lỗi
                    if (count_Duplication_PL != null && count_Duplication_PL.Any())
                    {
                        foreach (var group in count_Duplication_PL)
                        {
                            foreach (DataRow row in group)
                            {
                                MessageBox.Show("Trùng dữ liệu Packing List."  + "\nCustomer Item Code: " + row.Field<string>("Customer_ItemCode")
                                                    + "\nCustomer_PO: " + row.Field<string>("Customer_PO")
                                                    + "\nShipping No: " + row.Field<string>("ShippingNo")
                                                    , "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                            break;
                        }
                        return;
                    }

                    try
                    {
                        //if (GridView_Invoice.Rows.Count == 0)
                        //{

                        //}

                        //Insert new data
                        if (_shippingDAO.insertShipping(dtInvoiceMS,Grid_Invoice,dtInvoiceDetail, dtPackingListDetail) == true)
                        {
                            string Message = "Lưu thành công shipping : \"" + Convert.ToString(sLookUp_ShippingNo.EditValue) + "\"!";
                            MessageBox.Show(Message, "Notify", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //Write Lock
                            string _typeLock = "";
                            DataTable _tempTable = new DataTable();
                            _tempTable = _shippingDAO.Check_Shipping(_shippingNo);
                            if(_tempTable.Rows.Count > 0)
                            {
                                _typeLock = "EDIT";
                            } else if (_tempTable.Rows.Count == 0)
                            {
                                _typeLock = "NEW";
                            }
                            //Message += " Computer: " + Environment.MachineName + ". Invoice " + (GridView_Invoice.Rows.Count - 1) + " rows";
                            //Message += ". Packing List " + (GridView_PackingList.Rows.Count - 1) + " rows";
                            _logDAO.InsertLog(_systemDAL.CompanyCode, _systemDAL.UserName, _typeLock, Message);

                            //Clear data
                            ClearData();
                        }
                    }
                    catch (ApplicationException ex)
                    {
                        MessageBox.Show(ex.Message, "Insert Fail!");
                    }
                }
            }
            else
            {
                return;
            }
        }

        private void btnExport_Instruction_Click(object sender, EventArgs e)
        {
            //OpenFileDialog theDialog = new OpenFileDialog();
            //theDialog.Title = "Choose Template Instruction";
            //theDialog.Filter = "Files Excel|*.xlsx";
            //if (theDialog.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        string _filePath = theDialog.FileName;

            //        //Create fileinfo object of an excel file
            //        FileInfo _fileInfo = new FileInfo(_filePath);

            //        //Create a new Excel package from the file
            //        using (ExcelPackage _excelPackage = new ExcelPackage(_fileInfo))
            //        {
            //            int rowCount = 0;
            //            DataRow drLocal = null;
            //            int _valueCbFreight = Convert.ToInt32(cb_Freight.SelectedValue);

            //            #region Export Instruction
            //            ExcelWorksheet _InstructionSheet = _excelPackage.Workbook.Worksheets[1];
            //            _InstructionSheet.Cells["B3"].Value = txtShipVia.Text.Trim();

            //            _InstructionSheet.Cells["B4"].Value = txtShippingNo.Text.Trim();

            //            _InstructionSheet.Cells["B5"].Value = DateTime.Now.ToString("MMMM dd,yyyy");

            //            _InstructionSheet.Cells["C6"].Value = txtIssuedTo_CompanyName.Text.Trim();

            //            _InstructionSheet.Cells["C7"].Value = txtShipTo_CompanyName.Text.Trim();

            //            _InstructionSheet.Cells["C9"].Value = dtpETD.Value.ToString("MMMM dd,yyyy");

            //            _InstructionSheet.Cells["C13"].Value = txtPriceCondition.Text.Trim();

            //            //
            //            DataTable _tempShipping_Instruction = new DataTable();
            //            _tempShipping_Instruction.Columns.Add("No");
            //            _tempShipping_Instruction.Columns["No"].DataType = typeof(Int32);
            //            _tempShipping_Instruction.Columns.Add("Item_Name");
            //            _tempShipping_Instruction.Columns.Add("Tvc_ItemCode");
            //            _tempShipping_Instruction.Columns.Add("Customer_ItemCode");
            //            _tempShipping_Instruction.Columns.Add("ThirdParty_Code");
            //            _tempShipping_Instruction.Columns.Add("Customer_PO");
            //            _tempShipping_Instruction.Columns.Add("ThirdParty_PO");
            //            _tempShipping_Instruction.Columns.Add("Quantity");
            //            _tempShipping_Instruction.Columns["Quantity"].DataType = typeof(Decimal);
            //            _tempShipping_Instruction.Columns.Add("Order_Price");
            //            _tempShipping_Instruction.Columns["Order_Price"].DataType = typeof(Decimal);
            //            _tempShipping_Instruction.Columns.Add("Order_Date");
            //            _tempShipping_Instruction.Columns["Order_Date"].DataType = typeof(DateTime);
            //            _tempShipping_Instruction.Columns.Add("Note");

            //            int _noIndex = 1;
            //            foreach (DataGridViewRow dr in GridView_Invoice.Rows)
            //            {
            //                if (!dr.IsNewRow)
            //                {
            //                    drLocal = _tempShipping_Instruction.NewRow();
            //                    drLocal["No"] = _noIndex;
            //                    drLocal["Item_Name"] = dr.Cells["Part_Description"].Value;
            //                    drLocal["Tvc_ItemCode"] = dr.Cells["Tvc_ItemCode"].Value;
            //                    drLocal["Customer_ItemCode"] = dr.Cells["Cus_ItemCode"].Value;
            //                    if (dr.Cells["ThirdParty_ItemCode"].Value != null) { 
            //                        drLocal["ThirdParty_Code"] = dr.Cells["ThirdParty_ItemCode"].Value;
            //                    }
            //                    drLocal["Customer_PO"] = dr.Cells["Customer_PO"].Value;
            //                    if (dr.Cells["ThirdParty_PO"].Value != null)
            //                    {
            //                        drLocal["ThirdParty_PO"] = dr.Cells["ThirdParty_PO"].Value;
            //                    }
            //                    drLocal["Quantity"] = dr.Cells["Quantity"].Value;
            //                    drLocal["Order_Price"] = dr.Cells["Order_Price"].Value;
            //                    if(dr.Cells["Order_Date"].Value != null)
            //                    {
            //                        drLocal["Order_Date"] = Convert.ToDateTime(dr.Cells["Order_Date"].Value).ToString("MMMM dd,yyyy");
            //                    }
            //                    _noIndex++;
            //                    _tempShipping_Instruction.Rows.Add(drLocal);
            //                }
            //            }

            //            rowCount = _tempShipping_Instruction.Rows.Count;

            //            if (rowCount > 0)
            //            {
            //                _InstructionSheet.InsertRow(17, rowCount - 1, 16);
            //                _InstructionSheet.Cells["A16"].LoadFromDataTable(_tempShipping_Instruction, false);
            //            }
            //            #endregion

            //            //Focus A1, sheet Invoice
            //            _InstructionSheet.Select("A1");

            //            byte[] bin = _excelPackage.GetAsByteArray();

            //            //Create a SaveFileDialog instance with some properties
            //            SaveFileDialog _saveFileDialog = new SaveFileDialog();
            //            _saveFileDialog.Title = "Save file Shipping Instruction";
            //            _saveFileDialog.Filter = "Excel files|*.xlxs|All files|*.*";
            //            _saveFileDialog.FileName = "Shipping Instruction_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";

            //            //Check if user clicked the save button
            //            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            //            {
            //                //write the file to the disk
            //                File.WriteAllBytes(_saveFileDialog.FileName, bin);
            //                MessageBox.Show("    Export Shipping Instruction complete !!!    ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Error: Can't read file. Original error: " + ex.Message);
            //    }
            //}
            string shippingNo = Convert.ToString(sLookUp_ShippingNo.EditValue).Trim();

            Form_Shipping_Output_PackingList form_Shipping_Output_PackingList = new Form_Shipping_Output_PackingList(_systemDAL, shippingNo);
            form_Shipping_Output_PackingList.StartPosition = FormStartPosition.CenterParent;
            form_Shipping_Output_PackingList.ShowDialog();

            ClearData();

            //txtShippingNo.Text = shippingNo;

            //Load Invoice
            //btn_SearchShipping_Click(sender, e);

            //Setting enable item
            SettingInitGridView();
        }

        private void btnLockData_Click(object sender, EventArgs e)
        {
            string _shippingNo = Convert.ToString(sLookUp_ShippingNo.EditValue).Trim();
            if (radNormal.Checked == true)
            {
                if ((MessageBox.Show("Do you want to Lock Shipping Instruction?", "Confirm"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    , MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    if (CheckError_Lock() == true)
                    { 
                        try
                        {
                            if (String.IsNullOrEmpty(_shippingNo))
                            {
                                MessageBox.Show("Shipping No is empty!");
                            }
                            else
                            {
                                DataTable _tempTable = new DataTable();
                                _tempTable = _shippingDAO.Check_Shipping(_shippingNo);

                                if (_tempTable.Rows.Count == 0)
                                {
                                    MessageBox.Show("You should save Shipping Instruction before Lock!","Cảnh Báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else if (_tempTable.Rows.Count == 1)
                                {
                                    btnSave_Data_Click(sender, e);
                                    bool _result = _shippingDAO.Lock_ShippingInstruction(_shippingNo);
                                    if (_result)
                                    {
                                        MessageBox.Show("Lock shipping \"" + _shippingNo + "\" complete!", "Hoàn Thành",MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        radLock.Checked = true;
                                    }
                                }
                                else if (_tempTable.Rows.Count > 1)
                                {
                                    MessageBox.Show("Shipping Instruction duplicate!");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            else if (radRevise.Checked == true)
            {
                MessageBox.Show("Data has been lock before!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUnlockData_Click(object sender, EventArgs e)
        {
            string _shippingNo = Convert.ToString(sLookUp_ShippingNo.EditValue).Trim();
            if (radLock.Checked == true)
            {
                if ((MessageBox.Show("Do you want to Unlock Shipping Instruction?", "Confirm"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    , MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    try
                    {   
                        if (String.IsNullOrEmpty(_shippingNo))
                        {
                            MessageBox.Show("Shipping No đang rỗng!");
                        }
                        else
                        {
                            DataTable _tempTable = new DataTable();
                            _tempTable = _shippingDAO.Check_Shipping(_shippingNo);

                            if (_tempTable.Rows.Count == 0)
                            {
                                MessageBox.Show("Không tìm thấy shipping!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else if (_tempTable.Rows.Count == 1)
                            {
                                bool _result = _shippingDAO.UnLock_ShippingInstruction(_shippingNo);
                                if (_result)
                                {
                                    MessageBox.Show("Mở khóa shipping \"" + _shippingNo + "\" thành công!", "Hoàn Thành", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    radNormal.Checked = true;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Can't unlock!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRevise_Click(object sender, EventArgs e)
        {
            string _shippingNo = Convert.ToString(sLookUp_ShippingNo.EditValue).Trim();
            if (radLock.Checked == true)
            {
                if ((MessageBox.Show("Do you want to Revise Shipping Instruction?", "Confirm"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    , MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    try
                    {
                        if (String.IsNullOrEmpty(_shippingNo))
                        {
                            MessageBox.Show("Shipping Instruction rỗng!");
                        }
                        else
                        {
                            DataTable _tempTable = new DataTable();
                            _tempTable = _shippingDAO.Check_Shipping(_shippingNo);

                            if (_tempTable.Rows.Count == 0)
                            {
                                MessageBox.Show("Không có dữ liệu Shipping Instruction!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else if (_tempTable.Rows.Count == 1)
                            {
                                bool _result = _shippingDAO.Revise_ShippingInstruction(_shippingNo);
                                if (_result)
                                {
                                    MessageBox.Show("Revise Shipping Instruction \"" + _shippingNo + "\" thành công!", "Hoàn thành", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //
                                    ClearData();
                                    //
                                    //txtShippingNo.Text = _shippingNo;
                                    //Reload Shipping Instruction
                                    //btn_SearchShipping_Click(sender, e);
                                    radRevise.Checked = true;
                                    SettingInitGridView();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Không thể unlock!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_SearchShipping_Click(object sender, EventArgs e)
        {
            string _shippingNo = Convert.ToString(sLookUp_ShippingNo.EditValue).Trim();
            try
            {
                //Get data header
                Header_Data = _shippingDAO.GetHeader_ShipInv(_shippingNo);
                if (Header_Data.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Header_Data.Rows.Count >= 1)
                {
                    //Header data
                    foreach (DataRow row in Header_Data.Rows)
                    { 
                        //COMPANY CODE
                        cb_CompanyCode.SelectedValue = Convert.ToString(row["COMPANY_CODE"]);

                        //DATE CREATE SHIPPING
                        dateEdit_DateCreateShipping.EditValue = Convert.ToDateTime(row["DATE_CREATE"]);

                        ////SHIPPING NO
                        //txtShippingNo.Text = row["SHIPPING_NO"].ToString();
                        //txtShippingNo.Enabled = false;

                        //INVOICE NO
                        txtInvoiceNo.Text = row["INVOICE_NO"].ToString();

                        //ISSUEDTO
                        sLookUp_IssuedTo_CompanyCode.EditValue  = Convert.ToString(row["ISSUEDTO_CUSTOMER_CODE"]);
                        txtIssuedTo_CompanyName.EditValue       = Convert.ToString(row["ISSUEDTO_CUSTOMER_NAME"]);
                        memo_IssuedTo_CompanyAddress.EditValue  = Convert.ToString(row["ISSUEDTO_CUSTOMER_ADDRESS"]);
                        txtIssuedTo_TelNo.EditValue             = Convert.ToString(row["ISSUEDTO_CUSTOMER_TEL_NO"]);
                        txtIssuedTo_FaxNo.EditValue             = Convert.ToString(row["ISSUEDTO_CUSTOMER_FAX_NO"]);

                        //if (row["ISSUEDTO_CUSTOMER_CODE"].Equals("TTC"))
                        //{
                        //    this.GridView_PackingList.Columns["Customer_PO"].Visible = true;
                        //} else
                        //{
                        //    this.GridView_PackingList.Columns["Customer_PO"].Visible = false;
                        //}

                        //SHIPTO
                        sLookUp_ShipTo_CompanyCode.EditValue = Convert.ToString(row["SHIPTO_CUSTOMER_CODE"]);
                        txtShipTo_CompanyName.EditValue = Convert.ToString(row["SHIPTO_CUSTOMER_NAME"]);
                        memo_ShipTo_CompanyAddress.EditValue = Convert.ToString(row["SHIPTO_CUSTOMER_ADDRESS"]);
                        txtShipTo_TelNo.EditValue = Convert.ToString(row["SHIPTO_CUSTOMER_TEL_NO"]);
                        txtShipTo_FaxNo.EditValue = Convert.ToString(row["SHIPTO_CUSTOMER_FAX_NO"]);

                        if (!String.IsNullOrEmpty(Convert.ToString(row["REVENUE"])))
                        {
                            //REVENUE
                            dateEdit_Revenue.EditValue = Convert.ToDateTime(row["REVENUE"]);
                        }
                        else
                        {
                            //REVENUE
                            dateEdit_Revenue.EditValue = DateTime.MinValue;
                        }

                        //SHIP VIA
                        txtShipVia.Text = row["SHIP_VIA"].ToString();

                        //FREIGHT
                        cb_Freight.SelectedValue = Convert.ToInt32(row["FREIGHT"]);

                        //VESSEL
                        txtVessel.Text = row["VESSEL"].ToString();

                        //PORT OF LOADING
                        sLookUp_PortLoading.EditValue = Convert.ToString(row["PORT_OF_LOADING"]);

                        //PORT OF DESTINATION
                        sLookUp_PortDestination.EditValue = Convert.ToString(row["PORT_OF_DESTINATION"]);

                        //ETD
                        dateEdit_ETD.EditValue = Convert.ToDateTime(row["ETD"]);

                        //ETA
                        dateEdit_ETA.EditValue = Convert.ToDateTime(row["ETA"]);

                        //TRADE CONDITION
                        sLookUp_PriceCondition.Text = Convert.ToString(row["TRADE_CONDITION"]);

                        //PAYMENT TERM
                        sLookUp_PaymentTerm.Text = Convert.ToString(row["PAYMENT_TERM"]);

                        //CREATE BY
                        createBy = row["CREATE_BY"].ToString();
                    }

                    //Grid data
                    //Grid_Invoice = _shippingDAO.GetDetail_ShipInv(_shippingNo);
                    if (Grid_Invoice.Rows.Count == 0)
                    {
                        //MessageBox.Show("Không có dữ liệu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (Grid_Invoice.Rows.Count >= 1)
                    {
                        ////Clear DataGridView
                        //GridView_Invoice.Rows.Clear();
                        //GridView_PackingList.Rows.Clear();
                        //foreach (DataRow row in Grid_Invoice.Rows)
                        //{
                        //    if (!String.IsNullOrEmpty(row["INV_CUSTOMER_CODE"].ToString()))
                        //    {
                        //        int indexInv = GridView_Invoice.Rows.Add();
                        //        //Binding for Invoice
                        //        GridView_Invoice.Rows[indexInv].Cells["Customer_Code"].Value = row["INV_CUSTOMER_CODE"].ToString();
                        //        GridView_Invoice.Rows[indexInv].Cells["Part_Description"].Value = row["INV_ITEM_NAME"].ToString();
                        //        GridView_Invoice.Rows[indexInv].Cells["Cus_ItemCode"].Value = row["INV_CUS_ITEM_CODE"].ToString();
                        //        GridView_Invoice.Rows[indexInv].Cells["Tvc_ItemCode"].Value = row["INV_ITEM_CODE"].ToString();
                        //        GridView_Invoice.Rows[indexInv].Cells["Customer_PO"].Value = row["INV_REF_PO_NO"].ToString();
                        //        GridView_Invoice.Rows[indexInv].Cells["ThirdParty_PO"].Value = row["THIRD_PARTY_PO"].ToString();
                        //        if (!String.IsNullOrEmpty(Convert.ToString(row["ORDER_DATE"])))
                        //        {
                        //            GridView_Invoice.Rows[indexInv].Cells["Order_Date"].Value = Convert.ToDateTime(row["ORDER_DATE"]).ToString("dd/MM/yyyy");
                        //        }
                        //        GridView_Invoice.Rows[indexInv].Cells["DueDate_PO"].Value = Convert.ToDateTime(row["DUE_DATE_PO"]).ToString("dd/MM/yyyy");
                        //        GridView_Invoice.Rows[indexInv].Cells["Quantity"].Value = row["INV_QUANTITY"];
                        //        GridView_Invoice.Rows[indexInv].Cells["Quantity_Revise"].Value = row["INV_QUANTITY_REVISE"];
                        //        GridView_Invoice.Rows[indexInv].Cells["Balance"].Value = row["INV_BALANCE"];
                        //        GridView_Invoice.Rows[indexInv].Cells["Unit_Currency"].Value = row["INV_UNIT_CURRENCY"];
                        //        GridView_Invoice.Rows[indexInv].Cells["USD_Rate"].Value = row["INV_USD_RATE"];
                        //        GridView_Invoice.Rows[indexInv].Cells["Order_Price"].Value = row["INV_ORDER_PRICE"];
                        //        GridView_Invoice.Rows[indexInv].Cells["Order_Price_Revise"].Value = row["INV_ORDER_PRICE_REVISE"];
                        //        GridView_Invoice.Rows[indexInv].Cells["Global_Price"].Value = row["GLOBAL_PRICE"];
                        //        GridView_Invoice.Rows[indexInv].Cells["Amount_Jpy"].Value = row["INV_AMOUNT"];
                        //    }
                        //}

                        //
                        Grid_PackingList = _shippingDAO.GetDetail_ShipPL(_shippingNo);
                        if (Grid_PackingList.Rows.Count >= 1)
                        {
                            foreach (DataRow row in Grid_PackingList.Rows)
                            {
                                if (row["PL_CUSTOMER_CODE"] != null)
                                {
                                    //int indexPL = GridView_PackingList.Rows.Add();
                                    //GridView_PackingList.Rows[indexPL].Cells["Customer_Code"].Value = row["PL_CUSTOMER_CODE"].ToString();
                                    //GridView_PackingList.Rows[indexPL].Cells["Packages_No"].Value = row["PL_PACKAGES_NO"].ToString();
                                    //GridView_PackingList.Rows[indexPL].Cells["Customer_ItemCode"].Value = row["PL_ITEM_CODE"].ToString();
                                    //GridView_PackingList.Rows[indexPL].Cells["TVC_ItemCode"].Value = row["PL_TVC_ITEM_CODE"].ToString();
                                    //GridView_PackingList.Rows[indexPL].Cells["Customer_PO"].Value = row["PL_CUSTOMER_PO"].ToString();
                                    //GridView_PackingList.Rows[indexPL].Cells["Qty_Carton"].Value = Convert.ToDecimal(row["PL_QTY_CARTON"]);
                                    //GridView_PackingList.Rows[indexPL].Cells["Qty_Per_Carton"].Value = Convert.ToDecimal(row["PL_QTY_PER_CARTON"]);
                                    //GridView_PackingList.Rows[indexPL].Cells["Qty_Total"].Value = Convert.ToDecimal(row["PL_QTY_TOTAL"]);
                                    //GridView_PackingList.Rows[indexPL].Cells["Qty_Total_Revise"].Value = row["PL_QTY_TOTAL_REVISE"];
                                    //GridView_PackingList.Rows[indexPL].Cells["Net_Weight"].Value = Convert.ToDecimal(row["PL_NET_WEIGHT"]);
                                    //GridView_PackingList.Rows[indexPL].Cells["Net_Weight_Total"].Value = Convert.ToDecimal(row["PL_NET_WEIGHT_TOTAL"]);
                                    //GridView_PackingList.Rows[indexPL].Cells["Gross_Weight"].Value = Convert.ToDecimal(row["PL_GROSS_WEIGHT"]);
                                    //GridView_PackingList.Rows[indexPL].Cells["Lot_No"].Value = row["PL_LOT_NO"].ToString();
                                }
                            }
                        }

                        ////Sum Invoice
                        //btnSumInvoice_Click(sender, e);
                        ////Sum Packing List
                        //btnSumPL_Click(sender, e);
                    }
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void picBox_BackToMain_Click(object sender, EventArgs e)
        {
            string exitMessageText = "Bạn muốn trở về màn hình chính?";
            string exitCaption = "Xác nhận";
            MessageBoxButtons button = MessageBoxButtons.YesNo;
            DialogResult res = MessageBox.Show(exitMessageText, exitCaption, button, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                this.Hide();
                Form_Main _formMain = new Form_Main(_systemDAL);
                _formMain.StartPosition = FormStartPosition.CenterScreen;
                _formMain.Show();
            }
        }

        public Boolean CheckError()
        {

            if (cb_CompanyCode.SelectedIndex == 0)
            {
                MessageBox.Show("Xin hãy chọn 「Company Code」!");
                cb_CompanyCode.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(Convert.ToString(sLookUp_ShippingNo.EditValue)))
            {
                MessageBox.Show("Xin hãy nhập 「Shipping No」!");
                sLookUp_ShippingNo.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtIssuedTo_CompanyName.Text.Trim()))
            {
                MessageBox.Show("Xin hãy nhập 「Issued To」!");
                txtIssuedTo_CompanyName.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtShipTo_CompanyName.Text.Trim()))
            {
                MessageBox.Show("Xin hãy nhập 「Ship To」!");
                txtShipTo_CompanyName.Focus();
                return false;
            }

            if (Convert.ToDateTime(dateEdit_Revenue.EditValue) == DateTime.MinValue)
            {
                MessageBox.Show("Xin hãy chọn「Revenue」!\nShipping thuộc doanh thu của tháng nào!");
                dateEdit_Revenue.Focus();
                return false;
            }

            return true;
        }

        public Boolean CheckError_Lock()
        {
            ////Check Total Invocie
            //int _totalQtyInv = 0;
            //int _totalQtyPL = 0;
            //if (!String.IsNullOrEmpty(txtTotalQuantity.Text))
            //{
            //    _totalQtyInv = int.Parse(txtTotalQuantity.Text, System.Globalization.NumberStyles.AllowThousands);
            //}
            //if (!String.IsNullOrEmpty(txtTotal_Qty.Text))
            //{
            //    _totalQtyPL = int.Parse(txtTotal_Qty.Text, System.Globalization.NumberStyles.AllowThousands);
            //}

            //if (_totalQtyInv > 0 && _totalQtyPL > 0)
            //{
            //    if (!Equals(_totalQtyInv, _totalQtyPL))
            //    {
            //        MessageBox.Show("Số lượng(quantity) của Invoice đang khác với số lượng(quantity) của Packing List!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        txtTotalQuantity.Focus();
            //        return false;
            //    }
            //}

            return true;
        }

        private void TxtShipTo_CompanyCode_TextChanged(object sender, EventArgs e)
        {
            //if (txtShipTo_CompanyCode.Text.Trim().ToUpper() == "TTC")
            //{
            //    this.GridView_PackingList.Columns["Customer_PO"].Visible = true;
            //}
            //else
            //{
            //    this.GridView_PackingList.Columns["Customer_PO"].Visible = false;
            //}
        }

        private void GetInfo_Shipping()
        {
            using (Takako_Entities db = new Takako_Entities())
            {
                string sqlQuery =
                    @"SELECT 
                             MS.[ISSUEDTO_CUSTOMER_CODE]
                            ,MS.[SHIPTO_CUSTOMER_CODE]
                            ,MS.[SHIPPING_NO]
                            ,MS.[INVOICE_NO]
                            ,CASE
                                WHEN MS.[LOCK_STATUS] = 0 THEN 'NORMAL'
                                WHEN MS.[LOCK_STATUS] = 1 THEN 'LOCK'
                                WHEN MS.[LOCK_STATUS] = 2 THEN 'REVISE'
                        	 END AS LOCK_STATUS  
                            ,DT.[UNIT_CURRENCY]
                            ,MS.[ETD]
                            ,MS.[DATE_CREATE]
	                        ,DT.[AMOUNT]
                        FROM 
                            [dbo].[TVC_SHIPPING_MS] MS
                        INNER JOIN
                        	(
                        		SELECT
                        			 MS.SHIPPING_NO
                                    ,DT.UNIT_CURRENCY
                        			,MAX(DT.REVISE_VERSION)     AS REVISE_VERSION
                                    ,SUM(ISNULL(DT.AMOUNT,0))   AS AMOUNT	
                        		FROM
                                    [dbo].[TVC_SHIPPING_MS] MS
                                LEFT JOIN
                                    [dbo].[TVC_SHIPPING_INV_DETAIL] DT
                                ON  MS.SHIPPING_NO = DT.SHIPPING_NO
                                GROUP BY
                                     MS.SHIPPING_NO
                                    ,DT.UNIT_CURRENCY
                            ) DT
                        ON	MS.SHIPPING_NO = DT.SHIPPING_NO
                        ORDER BY
                             MS.[LOCK_STATUS] ASC
                            ,MS.[ETD] DESC";
                List<Search_Shipping> result = db.Database.SqlQuery<Search_Shipping>(sqlQuery).ToList();

                if (result.Count > 0)
                {
                    sLookUp_ShippingNo.Properties.DataSource = result;
                    sLookUp_ShippingNo.Properties.ValueMember = "SHIPPING_NO";
                    sLookUp_ShippingNo.Properties.DisplayMember = "SHIPPING_NO";
                }
                else
                {
                    sLookUp_ShippingNo.Properties.DataSource = "";
                }
            }
        }

        private void GetInfo_Customer(SearchLookUpEdit _sLookUpEdit)
        {
            using (Takako_Entities db = new Takako_Entities())
            {
                var result = 
                    (from cus in db.CUSTOMMFs
                    where
                        cus.CUSTOMER_CLASS.Equals("1")
                    ||  cus.CUSTOMER_CLASS.Equals("3")
                    select new
                    {
                        cus.CUSTOMER_CODE,
                        cus.CUSTOMER_NAME1,
                        ADDRESS = cus.ADDRESS1 + cus.ADDRESS2 + cus.ADDRESS3,
                        cus.TEL_NO,
                        cus.FAX_NO,
                        cus.INVOICE_FORMAT
                    }).ToList();

                if (result.Count > 0)
                {
                    _sLookUpEdit.Properties.DataSource = result;
                    _sLookUpEdit.Properties.ValueMember = "CUSTOMER_CODE";
                    _sLookUpEdit.Properties.DisplayMember = "CUSTOMER_CODE";
                }
                else
                {
                    _sLookUpEdit.Properties.DataSource = "";
                }
            }
        }

        public void GetInfoDestination(SearchLookUpEdit _sLookUpEdit)
        {
            using (Takako_Entities db = new Takako_Entities())
            {
                List<DESTINATIONMF> result = db.DESTINATIONMFs.ToList();

                if (result.Count > 0)
                {
                    _sLookUpEdit.Properties.DataSource = result;
                    _sLookUpEdit.Properties.ValueMember = "DESTINATION_ID";
                    _sLookUpEdit.Properties.DisplayMember = "DESTINATION_ID";
                }
                else
                {
                    _sLookUpEdit.Properties.DataSource = "";
                }
            }
        }

        public void GetInfoPriceCondition(SearchLookUpEdit _sLookUpEdit)
        {
            using (Takako_Entities db = new Takako_Entities())
            {
                List<PRICE_CONDITIONMF> result = db.PRICE_CONDITIONMF.ToList();

                if (result.Count > 0)
                {
                    _sLookUpEdit.Properties.DataSource = result;
                    _sLookUpEdit.Properties.ValueMember = "PRICE_COND";
                    _sLookUpEdit.Properties.DisplayMember = "PRICE_COND";
                }
                else
                {
                    _sLookUpEdit.Properties.DataSource = "";
                }
            }
        }

        public void GetInfoPaymentTerm(SearchLookUpEdit _sLookUpEdit)
        {
            using (Takako_Entities db = new Takako_Entities())
            {
                List<PAYMENT_TERMMF> result = db.PAYMENT_TERMMF.ToList();

                if (result.Count > 0)
                {
                    _sLookUpEdit.Properties.DataSource = result;
                    _sLookUpEdit.Properties.ValueMember = "PAYMENT_ID";
                    _sLookUpEdit.Properties.DisplayMember = "PAYMENT_ID";
                }
                else
                {
                    _sLookUpEdit.Properties.DataSource = "";
                }
            }
        }

        private void sLookUp_IssuedTo_CompanyCode_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = sLookUp_IssuedTo_CompanyCode_View;
            int index = sLookUp_IssuedTo_CompanyCode.Properties.GetIndexByKeyValue(this.sLookUp_IssuedTo_CompanyCode.EditValue);

            if (index != 0)
            {
                string _customerName = Convert.ToString(view.GetRowCellValue(index, view.Columns["CUSTOMER_NAME1"]));
                string _customerAddress = Convert.ToString(view.GetRowCellValue(index, view.Columns["ADDRESS"]));
                string _customerTelNo = Convert.ToString(view.GetRowCellValue(index, view.Columns["TEL_NO"]));
                string _customerFaxNo = Convert.ToString(view.GetRowCellValue(index, view.Columns["FAX_NO"]));

                txtIssuedTo_CompanyName.EditValue = _customerName;
                memo_IssuedTo_CompanyAddress.EditValue = _customerAddress;
                txtIssuedTo_TelNo.EditValue = _customerTelNo;
                txtIssuedTo_FaxNo.EditValue = _customerFaxNo;
            }
        }

        private void sLookUp_ShipTo_CompanyCode_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = sLookUp_ShipTo_CompanyCode_View;
            int index = sLookUp_ShipTo_CompanyCode.Properties.GetIndexByKeyValue(this.sLookUp_ShipTo_CompanyCode.EditValue);

            string _customerName    = Convert.ToString(view.GetRowCellValue(index, view.Columns["CUSTOMER_NAME1"]));
            string _customerAddress = Convert.ToString(view.GetRowCellValue(index, view.Columns["ADDRESS"]));
            string _customerTelNo   = Convert.ToString(view.GetRowCellValue(index, view.Columns["TEL_NO"]));
            string _customerFaxNo   = Convert.ToString(view.GetRowCellValue(index, view.Columns["FAX_NO"]));

            txtShipTo_CompanyName.EditValue         = _customerName     != null ? _customerName     : null;
            memo_ShipTo_CompanyAddress.EditValue    = _customerAddress  != null ? _customerAddress  : null;
            txtShipTo_TelNo.EditValue               = _customerTelNo    != null ? _customerTelNo    : null;
            txtShipTo_FaxNo.EditValue               = _customerFaxNo    != null ? _customerFaxNo    : null;
        }

        private void sLookUp_ShippingNo_EditValueChanged(object sender, EventArgs e)
        {
            string _shippingNo = Convert.ToString(this.sLookUp_ShippingNo.EditValue);
            TVC_SHIPPING_MS _headerShipping = new TVC_SHIPPING_MS();
            List<TVC_SHIPPING_INV_DETAIL> _detailInvoice = new List<TVC_SHIPPING_INV_DETAIL>();
            List<TVC_SHIPPING_PL_DETAIL> _detailPackingList = new List<TVC_SHIPPING_PL_DETAIL>();

            // Get Data Shipping No
            using (Takako_Entities db = new Takako_Entities())
            {
                _headerShipping = db.TVC_SHIPPING_MS.Where(x => x.SHIPPING_NO.Equals(_shippingNo)).FirstOrDefault();
                _detailInvoice = db.TVC_SHIPPING_INV_DETAIL.Where(x => x.SHIPPING_NO.Equals(_shippingNo)).ToList();
                _detailPackingList = db.TVC_SHIPPING_PL_DETAIL.Where(x => x.SHIPPING_NO.Equals(_shippingNo)).ToList();
            }

            if(_headerShipping != null)
            {
                //COMPANY CODE
                cb_CompanyCode.SelectedValue            = _headerShipping.COMPANY_CODE;

                //DATE CREATE SHIPPING
                dateEdit_DateCreateShipping.EditValue   = _headerShipping.DATE_CREATE;

                //INVOICE NO
                txtInvoiceNo.Text                       = _headerShipping.INVOICE_NO;

                //ISSUEDTO
                sLookUp_IssuedTo_CompanyCode.EditValue  = _headerShipping.ISSUEDTO_CUSTOMER_CODE;
                txtIssuedTo_CompanyName.EditValue       = _headerShipping.ISSUEDTO_CUSTOMER_NAME;
                memo_IssuedTo_CompanyAddress.EditValue  = _headerShipping.ISSUEDTO_CUSTOMER_ADDRESS;
                txtIssuedTo_TelNo.EditValue             = _headerShipping.ISSUEDTO_CUSTOMER_TEL_NO;
                txtIssuedTo_FaxNo.EditValue             = _headerShipping.ISSUEDTO_CUSTOMER_FAX_NO;

                //if (row["ISSUEDTO_CUSTOMER_CODE"].Equals("TTC"))
                //{
                //    this.GridView_PackingList.Columns["Customer_PO"].Visible = true;
                //} else
                //{
                //    this.GridView_PackingList.Columns["Customer_PO"].Visible = false;
                //}

                //SHIPTO
                sLookUp_ShipTo_CompanyCode.EditValue    = _headerShipping.SHIPTO_CUSTOMER_CODE;
                txtShipTo_CompanyName.EditValue         = _headerShipping.SHIPTO_CUSTOMER_NAME;
                memo_ShipTo_CompanyAddress.EditValue    = _headerShipping.SHIPTO_CUSTOMER_ADDRESS;
                txtShipTo_TelNo.EditValue               = _headerShipping.SHIPTO_CUSTOMER_TEL_NO;
                txtShipTo_FaxNo.EditValue               = _headerShipping.SHIPTO_CUSTOMER_FAX_NO;

                if (!String.IsNullOrEmpty(_headerShipping.REVENUE))
                {
                    //REVENUE
                    dateEdit_Revenue.EditValue = Convert.ToDateTime(_headerShipping.REVENUE);
                }
                else
                {
                    //REVENUE
                    dateEdit_Revenue.EditValue = Convert.ToDateTime(DateTime.MinValue);
                }

                //SHIP VIA
                txtShipVia.Text                         = _headerShipping.SHIP_VIA;

                //FREIGHT
                cb_Freight.SelectedValue                = _headerShipping.FREIGHT;

                //VESSEL
                txtVessel.Text                          = _headerShipping.VESSEL;

                //PORT OF LOADING
                sLookUp_PortLoading.EditValue           = _headerShipping.PORT_OF_LOADING;

                //PORT OF DESTINATION
                sLookUp_PortDestination.EditValue       = _headerShipping.PORT_OF_DESTINATION;

                //ETD
                dateEdit_ETD.EditValue                  = _headerShipping.ETD;

                //ETA
                dateEdit_ETD.EditValue                  = _headerShipping.ETA;

                //TRADE CONDITION
                sLookUp_PriceCondition.Text             = _headerShipping.TRADE_CONDITION;

                //PAYMENT TERM
                sLookUp_PaymentTerm.Text                = _headerShipping.PAYMENT_TERM;

                //CREATE BY
                createBy                                = _headerShipping.CREATE_BY;
            }

            if(_detailInvoice.Count > 0)
            {
                gridControl_Invoice.DataSource = _detailInvoice;
            }

            if (_detailPackingList.Count > 0)
            {
                gridControl_PackingList.DataSource = _detailPackingList;
            }
        }
    }
}
