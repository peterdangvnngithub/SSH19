using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TAKAKO_ERP_3LAYER.DAL;
using TAKAKO_ERP_3LAYER.DAO;
using static TAKAKO_ERP_3LAYER.Model;

namespace TAKAKO_ERP_3LAYER
{
    public partial class Form_Shipping_Instruction_New : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public SYSTEM_DAL _systemDAL;

        public SHIPPING_DAO _shippingDAO = new SHIPPING_DAO();

        public enum EnumRevise
        {
              Normal = 0
            , Lock = 1
        };

        #region Form Event
        public Form_Shipping_Instruction_New(SYSTEM_DAL _formMainSystemDAL)
        {
            InitializeComponent();

            _systemDAL = _formMainSystemDAL;
        }

        private void Form_Shipping_Instruction_New_Load(object sender, EventArgs e)
        {
            //Define column Gridview
            Define_GridView(gridView_Invoice);
            Define_GridView(gridView_PackingList);
            Define_SearchLookUp_View(sLookUp_ShippingNo);
            Define_SearchLookUp_View(sLookUp_IssuedTo_CompanyCode);
            Define_SearchLookUp_View(sLookUp_ShipTo_CompanyCode);
            Define_SearchLookUp_View(sLookUp_PortLoading);
            Define_SearchLookUp_View(sLookUp_PortDestination);
            Define_SearchLookUp_View(sLookUp_PriceCondition);

            // Get init value sLookUpEdit
            GetInfo_sLookUpEdit(sLookUp_ShippingNo);
            GetInfo_sLookUpEdit(sLookUp_IssuedTo_CompanyCode);
            GetInfo_sLookUpEdit(sLookUp_ShipTo_CompanyCode);
            GetInfo_sLookUpEdit(sLookUp_PortLoading);
            GetInfo_sLookUpEdit(sLookUp_PortDestination);
            GetInfo_sLookUpEdit(sLookUp_PriceCondition);
            GetInfo_sLookUpEdit(sLookUp_PaymentTerm);

            GetInfo_LookUpEdit(lookUpEdit_CompanyCode);
            GetInfo_LookUpEdit(lookUpEdit_Freight);

            Format_Item();

            Setting_Status_Item();

            Setting_Init_Item();
        }

        private void Form_Shipping_Instruction_New_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có muốn thoát khỏi phần mềm SSH19", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.OK)
                {
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;    // Stopping Form Close perocess.
                }
            }
        }
        #endregion

        #region Setting Init
        private void Format_Item()
        {

        }

        private void Setting_Status_Item()
        {
            // INVOICE NO
            txtInvoiceNo.Enabled = false;

            // ISSUED TO
            txtIssuedTo_CompanyName.Enabled = false;
            memo_IssuedTo_CompanyAddress.Enabled = false;
            txtIssuedTo_TelNo.Enabled = false;
            txtIssuedTo_FaxNo.Enabled = false;

            // SHIP TO
            txtShipTo_CompanyName.Enabled = false;
            memo_ShipTo_CompanyAddress.Enabled = false;
            txtShipTo_TelNo.Enabled = false;
            txtShipTo_FaxNo.Enabled = false;
        }

        private void Setting_Init_Item()
        {
            DateTime today = DateTime.Now;
            // Date Create Shipping
            dateEdit_DateCreateShipping.EditValue = today;

            //ETD
            dateEdit_ETD.EditValue = today;

            //ETA
            dateEdit_ETA.EditValue = today.AddDays(7);

            // Revenue
            dateEdit_Revenue.EditValue = today;

            //Set init focus
            sLookUp_ShippingNo.Focus();
        }

        public void Define_SearchLookUp_View(SearchLookUpEdit _sLookUpItem)
        {
            if (_sLookUpItem.Name.Equals(sLookUp_ShippingNo.Name))
            {
                sLookUp_ShippingNo.Properties.PopupFormSize = new Size(1043, 0);

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
                sLookUp_IssuedTo_CompanyCode.Properties.PopupFormSize = new Size(713, 0);

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
                sLookUp_ShipTo_CompanyCode.Properties.PopupFormSize = new Size(713, 0);

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
                sLookUp_PortLoading.Properties.PopupFormSize = new Size(303, 0);

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
                sLookUp_PortDestination.Properties.PopupFormSize = new Size(303, 0);

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
                sLookUp_PriceCondition.Properties.PopupFormSize = new Size(253, 0);

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

        private void GetInfo_LookUpEdit(LookUpEdit _lookUpEdit)
        {
            if(_lookUpEdit.Equals(lookUpEdit_CompanyCode))
            {
                var items = new[] {
                new { Text = "*-No Selected-*"  , Value = "00000" },
                new { Text = "TVC1"             , Value = "00001" },
                new { Text = "TVC2"             , Value = "00002" },
                };

                _lookUpEdit.Properties.DataSource = items;
                _lookUpEdit.Properties.DisplayMember = "Text";
                _lookUpEdit.Properties.ValueMember = "Value";
            } else if (_lookUpEdit.Equals(lookUpEdit_Freight))
            {
                var items = new[] {
                    new { Text = "*-No Selected-*"  , Value = 99 },
                    new { Text = "COLLECT"          , Value = 0 },
                    new { Text = "PREPAID"          , Value = 1 },
                };

                _lookUpEdit.Properties.DataSource = items;
                _lookUpEdit.Properties.DisplayMember = "Text";
                _lookUpEdit.Properties.ValueMember = "Value";
            }    
        }

        private void GetInfo_sLookUpEdit(SearchLookUpEdit _sLookUpItem)
        {
            using (Takako_Entities db = new Takako_Entities())
            {
                if (_sLookUpItem.Equals(sLookUp_ShippingNo))
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
                } else if(_sLookUpItem.Equals(sLookUp_IssuedTo_CompanyCode) || _sLookUpItem.Equals(sLookUp_ShipTo_CompanyCode))
                {
                    var result =
                    (from cus in db.CUSTOMMFs
                     where
                         cus.CUSTOMER_CLASS.Equals("1")
                     || cus.CUSTOMER_CLASS.Equals("3")
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
                        _sLookUpItem.Properties.DataSource = result;
                        _sLookUpItem.Properties.ValueMember = "CUSTOMER_CODE";
                        _sLookUpItem.Properties.DisplayMember = "CUSTOMER_CODE";
                    }
                    else
                    {
                        _sLookUpItem.Properties.DataSource = "";
                    }
                } else if (_sLookUpItem.Equals(sLookUp_PortLoading) || _sLookUpItem.Equals(sLookUp_PortDestination))
                {
                    List<DESTINATIONMF> result = db.DESTINATIONMFs.ToList();

                    if (result.Count > 0)
                    {
                        _sLookUpItem.Properties.DataSource = result;
                        _sLookUpItem.Properties.ValueMember = "DESTINATION_ID";
                        _sLookUpItem.Properties.DisplayMember = "DESTINATION_ID";
                    }
                    else
                    {
                        _sLookUpItem.Properties.DataSource = "";
                    }
                } else if (_sLookUpItem.Equals(sLookUp_PriceCondition))
                {
                    List<PRICE_CONDITIONMF> result = db.PRICE_CONDITIONMF.ToList();

                    if (result.Count > 0)
                    {
                        _sLookUpItem.Properties.DataSource = result;
                        _sLookUpItem.Properties.ValueMember = "PRICE_COND";
                        _sLookUpItem.Properties.DisplayMember = "PRICE_COND";
                    }
                    else
                    {
                        _sLookUpItem.Properties.DataSource = "";
                    }
                } else if (_sLookUpItem.Equals(sLookUp_PaymentTerm))
                {
                    List<PAYMENT_TERMMF> result = db.PAYMENT_TERMMF.ToList();

                    if (result.Count > 0)
                    {
                        _sLookUpItem.Properties.DataSource = result;
                        _sLookUpItem.Properties.ValueMember = "PAYMENT_ID";
                        _sLookUpItem.Properties.DisplayMember = "PAYMENT_ID";
                    }
                    else
                    {
                        _sLookUpItem.Properties.DataSource = "";
                    }
                }    
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
                gridCol_Balance.FieldName = "BALANCE";
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
                gridCol_Global_Price.FieldName = "GLOBAL_PRICE";
                gridCol_Global_Price.VisibleIndex = 16;
                gridCol_Global_Price.Width = 130;
                gridCol_Global_Price.DisplayFormat.FormatString = "#,##0.####";
                gridCol_Global_Price.DisplayFormat.FormatType = FormatType.Numeric;

                gridCol_Global_Price.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

                // AMOUNT (JPY)
                //-----Header-----//
                gridCol_Amount_Total.Name = "gridCol_Amount_Total";
                gridCol_Amount_Total.Caption = "AMOUNT TOTAL";
                gridCol_Amount_Total.FieldName = "AMOUNT_JPY";
                gridCol_Amount_Total.VisibleIndex = 17;
                gridCol_Amount_Total.Width = 120;
                gridCol_Amount_Total.UnboundType = UnboundColumnType.Integer;
                gridCol_Amount_Total.UnboundExpression = "[QUANTITY] * [ORDER_PRICE]";
                gridCol_Amount_Total.DisplayFormat.FormatType = FormatType.Numeric;
                gridCol_Amount_Total.DisplayFormat.FormatString = "#,##0.####";
                //-----Detail-----//
                gridCol_Amount_Total.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                //-----Sumary-----//
                gridCol_Amount_Total.SummaryItem.SummaryType = SummaryItemType.Sum;
                gridCol_Amount_Total.SummaryItem.DisplayFormat = "{0:#,##0.####}";

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
                GridColumn gridCol_Packages_No = new GridColumn();
                GridColumn gridCol_Cus_Item_Code = new GridColumn();
                GridColumn gridCol_TVC_Item_Code = new GridColumn();
                GridColumn gridCol_Customer_PO = new GridColumn();
                GridColumn gridCol_Qty_Carton = new GridColumn();
                GridColumn gridCol_Qty_Per_Carton = new GridColumn();
                GridColumn gridCol_Qty_Total = new GridColumn();
                GridColumn gridCol_Qty_Total_Revise = new GridColumn();
                GridColumn gridCol_Net_Weight = new GridColumn();
                GridColumn gridCol_Net_Weight_Total = new GridColumn();
                GridColumn gridCol_Gross_Weight = new GridColumn();
                GridColumn gridCol_LotNo = new GridColumn();

                //CUSTOMER CODE
                gridCol_Customer_Code.Name = "gridCol_Customer_Code";
                gridCol_Customer_Code.Caption = "CUSTOMER CODE";
                gridCol_Customer_Code.FieldName = "CUSTOMER_CODE";
                gridCol_Customer_Code.VisibleIndex = 0;
                gridCol_Customer_Code.Width = 100;

                gridCol_Customer_Code.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                //PACKAGES NO
                gridCol_Packages_No.Name = "gridCol_Packages_No";
                gridCol_Packages_No.Caption = "PACKAGES NO";
                gridCol_Packages_No.FieldName = "PACKAGES_NO";
                gridCol_Packages_No.VisibleIndex = 1;
                gridCol_Packages_No.Width = 90;

                gridCol_Packages_No.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

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

                //QTY CARTON
                gridCol_Qty_Carton.Name = "gridCol_Qty_Carton";
                gridCol_Qty_Carton.Caption = "QUANTITY CARTON";
                gridCol_Qty_Carton.FieldName = "QTY_CARTON";
                gridCol_Qty_Carton.VisibleIndex = 5;
                gridCol_Qty_Carton.Width = 90;
                gridCol_Qty_Carton.DisplayFormat.FormatString = "#,##0.##";
                gridCol_Qty_Carton.DisplayFormat.FormatType = FormatType.Numeric;
                gridCol_Qty_Carton.SummaryItem.SummaryType = SummaryItemType.Sum;
                gridCol_Qty_Carton.SummaryItem.DisplayFormat = "{0:#,##0.####}";

                //QTY PER CARTON
                gridCol_Qty_Per_Carton.Name = "gridCol_Qty_Per_Carton";
                gridCol_Qty_Per_Carton.Caption = "QUANTITY / CARTON (PCS)";
                gridCol_Qty_Per_Carton.FieldName = "QTY_PER_CARTON";
                gridCol_Qty_Per_Carton.VisibleIndex = 6;
                gridCol_Qty_Per_Carton.Width = 90;
                gridCol_Qty_Per_Carton.DisplayFormat.FormatString = "#,##0.##";
                gridCol_Qty_Per_Carton.DisplayFormat.FormatType = FormatType.Numeric;

                //QTY TOTAL
                gridCol_Qty_Total.Name = "gridCol_Qty_Total";
                gridCol_Qty_Total.Caption = "QUANTITY TOTAL (PCS)";
                gridCol_Qty_Total.FieldName = "QTY_TOTAL";
                gridCol_Qty_Total.VisibleIndex = 7;
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
                gridCol_Qty_Total_Revise.VisibleIndex = 8;
                gridCol_Qty_Total_Revise.Width = 90;
                gridCol_Qty_Total_Revise.DisplayFormat.FormatString = "#,##0.##";
                gridCol_Qty_Total_Revise.DisplayFormat.FormatType = FormatType.Numeric;

                gridCol_Gross_Weight.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

                //NET WEIGHT
                gridCol_Net_Weight.Name = "gridCol_Net_Weight";
                gridCol_Net_Weight.Caption = "N / W (KG)";
                gridCol_Net_Weight.FieldName = "NET_WEIGHT";
                gridCol_Net_Weight.VisibleIndex = 9;
                gridCol_Net_Weight.Width = 90;
                gridCol_Net_Weight.DisplayFormat.FormatString = "#,##0.##";
                gridCol_Net_Weight.DisplayFormat.FormatType = FormatType.Numeric;
                gridCol_Net_Weight.SummaryItem.SummaryType = SummaryItemType.Sum;
                gridCol_Net_Weight.SummaryItem.DisplayFormat = "{0:#,##0.####}";

                gridCol_Gross_Weight.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

                //NET WEIGHT TOTAL
                gridCol_Net_Weight_Total.Name = "gridCol_Net_Weight_Total";
                gridCol_Net_Weight_Total.Caption = "N / W TOTAL";
                gridCol_Net_Weight_Total.FieldName = "NET_WEIGHT_TOTAL";
                gridCol_Net_Weight_Total.VisibleIndex = 10;
                gridCol_Net_Weight_Total.Width = 90;
                gridCol_Net_Weight_Total.DisplayFormat.FormatString = "#,##0.##";
                gridCol_Net_Weight_Total.DisplayFormat.FormatType = FormatType.Numeric;

                gridCol_Gross_Weight.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

                //GROSS WEIGHT
                gridCol_Gross_Weight.Name = "gridCol_Gross_Weight";
                gridCol_Gross_Weight.Caption = "G / W (KG)";
                gridCol_Gross_Weight.FieldName = "GROSS_WEIGHT";
                gridCol_Gross_Weight.VisibleIndex = 11;
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
                gridCol_LotNo.VisibleIndex = 12;
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
        #endregion

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
                _detailInvoice = _shippingDAO.GetDetail_ShipInv(_shippingNo);
                _detailPackingList = db.TVC_SHIPPING_PL_DETAIL.Where(x => x.SHIPPING_NO.Equals(_shippingNo)).ToList();
            }

            if (_headerShipping != null)
            {
                if (_headerShipping.LOCK_STATUS.Equals(0))
                {
                    radNormal.Checked = true;
                }
                else if (_headerShipping.LOCK_STATUS.Equals(1))
                {
                    radLock.Checked = true;
                }

                //COMPANY CODE
                lookUpEdit_CompanyCode.EditValue = _headerShipping.COMPANY_CODE;

                //DATE CREATE SHIPPING
                dateEdit_DateCreateShipping.EditValue = _headerShipping.DATE_CREATE;

                //INVOICE NO
                txtInvoiceNo.Text = _headerShipping.INVOICE_NO;

                //ISSUEDTO
                sLookUp_IssuedTo_CompanyCode.EditValue = _headerShipping.ISSUEDTO_CUSTOMER_CODE;
                txtIssuedTo_CompanyName.EditValue = _headerShipping.ISSUEDTO_CUSTOMER_NAME;
                memo_IssuedTo_CompanyAddress.EditValue = _headerShipping.ISSUEDTO_CUSTOMER_ADDRESS;
                txtIssuedTo_TelNo.EditValue = _headerShipping.ISSUEDTO_CUSTOMER_TEL_NO;
                txtIssuedTo_FaxNo.EditValue = _headerShipping.ISSUEDTO_CUSTOMER_FAX_NO;

                if (_headerShipping.ISSUEDTO_CUSTOMER_CODE.Equals("TTC"))
                {
                    gridView_PackingList.Columns["CUSTOMER_PO"].Visible = true;
                }
                else
                {
                    gridView_PackingList.Columns["CUSTOMER_PO"].Visible = false;
                }

                //SHIPTO
                sLookUp_ShipTo_CompanyCode.EditValue = _headerShipping.SHIPTO_CUSTOMER_CODE;
                txtShipTo_CompanyName.EditValue = _headerShipping.SHIPTO_CUSTOMER_NAME;
                memo_ShipTo_CompanyAddress.EditValue = _headerShipping.SHIPTO_CUSTOMER_ADDRESS;
                txtShipTo_TelNo.EditValue = _headerShipping.SHIPTO_CUSTOMER_TEL_NO;
                txtShipTo_FaxNo.EditValue = _headerShipping.SHIPTO_CUSTOMER_FAX_NO;

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
                txtShipVia.Text = _headerShipping.SHIP_VIA;

                //FREIGHT
                lookUpEdit_Freight.EditValue = _headerShipping.FREIGHT;

                //VESSEL
                txtVessel.Text = _headerShipping.VESSEL;

                //PORT OF LOADING
                sLookUp_PortLoading.EditValue = _headerShipping.PORT_OF_LOADING;

                //PORT OF DESTINATION
                sLookUp_PortDestination.EditValue = _headerShipping.PORT_OF_DESTINATION;

                //ETD
                dateEdit_ETD.EditValue = _headerShipping.ETD;

                //ETA
                dateEdit_ETD.EditValue = _headerShipping.ETA;

                //TRADE CONDITION
                sLookUp_PriceCondition.Text = _headerShipping.TRADE_CONDITION;

                //PAYMENT TERM
                sLookUp_PaymentTerm.Text = _headerShipping.PAYMENT_TERM;
            }

            if (_detailInvoice.Count > 0)
            {
                gridControl_Invoice.DataSource = _detailInvoice;
            }

            if (_detailPackingList.Count > 0)
            {
                gridControl_PackingList.DataSource = _detailPackingList;
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

            string _customerName = Convert.ToString(view.GetRowCellValue(index, view.Columns["CUSTOMER_NAME1"]));
            string _customerAddress = Convert.ToString(view.GetRowCellValue(index, view.Columns["ADDRESS"]));
            string _customerTelNo = Convert.ToString(view.GetRowCellValue(index, view.Columns["TEL_NO"]));
            string _customerFaxNo = Convert.ToString(view.GetRowCellValue(index, view.Columns["FAX_NO"]));

            txtShipTo_CompanyName.EditValue = _customerName != null ? _customerName : null;
            memo_ShipTo_CompanyAddress.EditValue = _customerAddress != null ? _customerAddress : null;
            txtShipTo_TelNo.EditValue = _customerTelNo != null ? _customerTelNo : null;
            txtShipTo_FaxNo.EditValue = _customerFaxNo != null ? _customerFaxNo : null;
        }

        private void barBtn_Lock_Data_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barBtn_Unlock_Data_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barBtn_Save_Data_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barBtn_ClearData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barBtn_Import_Export_PackingList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barBtn_Add_New_PO_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Form_Search_PO_New _formPONew = new Form_Search_PO_New(_systemDAL, );
            //_formPONew.StartPosition = FormStartPosition.CenterParent();
        }

        private void barBtn_Back_To_Main_Menu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show(
                    "Bạn muốn trở về màn hình chính?", 
                    "Xác nhận", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                Form_Main _formMain = new Form_Main(_systemDAL);
                _formMain.StartPosition = FormStartPosition.CenterScreen;
                _formMain.Show();
            }
        }
    }
}