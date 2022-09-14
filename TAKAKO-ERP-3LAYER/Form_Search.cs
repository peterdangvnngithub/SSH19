using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TAKAKO_ERP_3LAYER.DAO;
using static TAKAKO_ERP_3LAYER.Common;

namespace TAKAKO_ERP_3LAYER
{
    public partial class Form_Search : Form
    {
        public static string _caseSearch;

        public static string _formCall;

        public CompanyInfo _companyInfo;

        public POInfo _PoInfo;

        public ShippingInfo _shippingInfo;

        public InvoiceInfo _InvoiceInfo;

        public DestinationInfo _destinationInfo;

        public SEARCH_DAO _searchDAO;

        public PriceConditionInfo _priceCondition;

        public PaymentTermInfo _paymentTerm;

        public ItemCodeInfo _itemCodeInfo;
        public Form_Search(string _valueTransmission,string _nameFormSearch)
        {
            InitializeComponent();

            //Setting no title but have border
            this.ControlBox = false;
            this.Text = String.Empty;

            //
            _companyInfo = new CompanyInfo();

            //
            _PoInfo = new POInfo();

            //
            _shippingInfo = new ShippingInfo();

            //
            _InvoiceInfo = new InvoiceInfo();

            //
            _paymentTerm = new PaymentTermInfo();

            //
            _destinationInfo = new DestinationInfo();

            //
            _itemCodeInfo = new ItemCodeInfo();

            //
            _searchDAO = new SEARCH_DAO();

            //
            _priceCondition = new PriceConditionInfo();

            //
            _caseSearch = _valueTransmission;

            //
            _formCall = _nameFormSearch;
        }

        private void Form_Search_Load(object sender, EventArgs e)
        {
            string _searchValue = txtKeySearch1.Text.Trim();
            string _filter1 = txtFilter1.Text.Trim();

            //
            AddColumnGridView();

            //
            SettingInit(_searchValue, _filter1);

            //
            AddLabelSearch();

            //
            Resize_Form();
        }

        public void AddLabelSearch()
        {
            if (_caseSearch == "btnSearch_Customer")
            {
                lblSearch1.Text = "Customer ID";
            } else if (_caseSearch == "btnSearch_ReciveNo")
            {
                lblSearch1.Text = "Receive No";
            }
            else if (_caseSearch == "btnSearch_ShippingNo")
            {
                lblFilter1.Text = "IssuedTo";
                lblSearch1.Text = "Shipping No";
            }
            else if (_caseSearch == "btnSearch_Inv")
            {
                lblSearch1.Text = "Invoice No";
            } else if (_caseSearch == "btnSearch_TradeCondition")
            {
                lblSearch1.Text = "Trade Condition";
            } else if (_caseSearch == "btnSearch_PaymentTerm")
            {
                lblSearch1.Text = "Payment Term";
            } else if (_caseSearch == "btnSearch_CodeTVC")
            {
                lblSearch1.Text = "Item Code TVC";
            }
        }

        public void Resize_Form()
        {
            if (_caseSearch == "btnSearch_Customer")
            {
                this.Size = new Size(666, 450);
            }
            else if (_caseSearch == "btnSearch_ReciveNo")
            {
                this.Size = new Size(510, 450);
            }
            else if (_caseSearch == "btnSearch_Inv")
            {
                this.Size = new Size(976, 450);
            }
            else if (_caseSearch == "btnSearch_ShippingNo")
            {
                this.Size = new Size(1056, 450);
            }
            else if (_caseSearch == "btnSearch_TradeCondition")
            {
                lblSearch1.Text = "Trade Condition";
            }
            else if (_caseSearch == "btnSearch_PaymentTerm")
            {
                lblSearch1.Text = "Payment Term";
            } else if (_caseSearch == "btnSearch_CodeTVC")
            {
                this.Size = new Size(506, 450);
            }
            else if (_caseSearch == "btnSearch_POCustomer")
            {
                this.Size = new Size(700, 450);
            }
        }

        public void AddColumnGridView()
        {
            if(_caseSearch == "btnSearch_Customer")
            { 
                System.Windows.Forms.DataGridViewTextBoxColumn CustomerCode_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn CustomerName_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn Address_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn TelNo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn FaxNo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn InvoiceFormat_col = new System.Windows.Forms.DataGridViewTextBoxColumn();

                this.GridView_Search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] 
                    {CustomerCode_col, CustomerName_col, Address_col, TelNo_col, FaxNo_col,InvoiceFormat_col});

                //CUSTOMER
                CustomerCode_col.HeaderText = "CUSTOMER";
                CustomerCode_col.DataPropertyName = "CUSTOMER_CODE";
                CustomerCode_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                CustomerCode_col.Name = "Customer_Code";
                CustomerCode_col.Width = 90;

                //CUSTOMER NAME
                CustomerName_col.HeaderText = "CUSTOMER NAME";
                CustomerName_col.DataPropertyName = "CUSTOMER_NAME1";
                CustomerName_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                CustomerName_col.Name = "Customer_Name1";
                CustomerName_col.Width = 220;

                //ADDRESS
                Address_col.HeaderText = "ADDRESS";
                Address_col.DataPropertyName = "ADDRESS";
                Address_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                Address_col.Name = "Address";
                Address_col.Width = 220;

                //TEL_NO
                TelNo_col.HeaderText = "TELEPHONE NUMBER";
                TelNo_col.DataPropertyName = "TEL_NO";
                TelNo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                TelNo_col.Name = "Tel_No";
                TelNo_col.Width = 180;

                //FAX_NO
                FaxNo_col.HeaderText = "FAX NUMBER";
                FaxNo_col.DataPropertyName = "FAX_NO";
                FaxNo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                FaxNo_col.Name = "Fax_No";
                FaxNo_col.Width = 120;

                //INVOICE FORMAT
                InvoiceFormat_col.HeaderText = "INVOICE FORMAT";
                InvoiceFormat_col.DataPropertyName = "INVOICE_FORMAT";
                InvoiceFormat_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                InvoiceFormat_col.Name = "Invoice_Format";
                InvoiceFormat_col.Width = 140;

                //CUSTOMER CODE
                this.GridView_Search.Columns["Customer_Code"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //TEL_NO
                this.GridView_Search.Columns["Tel_No"].Visible = false;

                //FAX_NO
                this.GridView_Search.Columns["Fax_No"].Visible = false;

                //
                this.GridView_Search.Columns["Invoice_Format"].Visible = false;
            }
            else if (_caseSearch == "btnSearch_ReciveNo")
            {
                System.Windows.Forms.DataGridViewTextBoxColumn ReceiveNo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn DueDateFrom_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn DueDateTo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();

                this.GridView_Search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] 
                    {
                         ReceiveNo_col
                        ,DueDateFrom_col
                        ,DueDateTo_col
                    });

                //RECEIVE NO
                ReceiveNo_col.HeaderText = "RECEIVE NO";
                ReceiveNo_col.DataPropertyName = "RECEIVE_NO";
                ReceiveNo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ReceiveNo_col.Name = "Receive_No";
                ReceiveNo_col.Width = 200;

                //ORDER DATE
                DueDateFrom_col.HeaderText = "DUE DATE FROM";
                DueDateFrom_col.DataPropertyName = "DUE_DATE_FROM";
                DueDateFrom_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DueDateFrom_col.Name = "Due_Date_From";
                DueDateFrom_col.Width = 120;

                //DUE DATE
                DueDateTo_col.HeaderText = "DUE DATE TO";
                DueDateTo_col.DataPropertyName = "DUE_DATE_TO";
                DueDateTo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DueDateTo_col.Name = "Due_Date_To";
                DueDateTo_col.Width = 120;

                //ORDER DATE
                this.GridView_Search.Columns["Due_Date_From"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //DUE DATE
                this.GridView_Search.Columns["Due_Date_To"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            else if (_caseSearch == "btnSearch_ShippingNo")
            {
                System.Windows.Forms.DataGridViewTextBoxColumn CustomerCode_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn ShipTo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn UnitCurrency_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn Amount_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn DateCreate_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn DateETD_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn ShippingNo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn InvoiceNo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn LockStatus_col = new System.Windows.Forms.DataGridViewTextBoxColumn();

                this.GridView_Search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
                    {
                          CustomerCode_col
                        , ShipTo_col
                        , ShippingNo_col
                        , InvoiceNo_col
                        , LockStatus_col
                        , UnitCurrency_col
                        , DateETD_col
                        , DateCreate_col
                        , Amount_col
                    });
                //CUSTOMER CODE
                CustomerCode_col.HeaderText = "ISSUED TO";
                CustomerCode_col.DataPropertyName = "ISSUEDTO_CUSTOMER_CODE";
                CustomerCode_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                CustomerCode_col.HeaderCell.Style.Font = new Font("Arial", 8.25F, FontStyle.Bold);
                CustomerCode_col.Name = "Customer_Code";
                CustomerCode_col.Width = 90;

                //SHIP TO
                ShipTo_col.HeaderText = "SHIP TO";
                ShipTo_col.DataPropertyName = "SHIPTO_CUSTOMER_CODE";
                ShipTo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ShipTo_col.HeaderCell.Style.Font = new Font("Arial", 8.25F, FontStyle.Bold);
                ShipTo_col.Name = "Ship_To";
                ShipTo_col.Width = 90;

                //SHIPPING NO
                ShippingNo_col.HeaderText = "SHIPPING NO";
                ShippingNo_col.DataPropertyName = "SHIPPING_NO";
                ShippingNo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ShippingNo_col.HeaderCell.Style.Font = new Font("Arial", 8.25F, FontStyle.Bold);
                ShippingNo_col.Name = "Shipping_No";
                ShippingNo_col.Width = 160;

                //INVOICE NO
                InvoiceNo_col.HeaderText = "INVOICE NO";
                InvoiceNo_col.DataPropertyName = "INVOICE_NO";
                InvoiceNo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InvoiceNo_col.HeaderCell.Style.Font = new Font("Arial", 8.25F, FontStyle.Bold);
                InvoiceNo_col.Name = "Invoice_No";
                InvoiceNo_col.Width = 140;

                //ETD
                DateETD_col.HeaderText = "ETD";
                DateETD_col.DataPropertyName = "ETD";
                DateETD_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DateETD_col.HeaderCell.Style.Font = new Font("Arial", 8.25F, FontStyle.Bold);
                DateETD_col.Name = "ETD";
                DateETD_col.Width = 100;

                //UNIT CURRENCY
                UnitCurrency_col.HeaderText = "UNIT CURRENCY";
                UnitCurrency_col.DataPropertyName = "UNIT_CURRENCY";
                UnitCurrency_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                UnitCurrency_col.HeaderCell.Style.Font = new Font("Arial", 8.25F, FontStyle.Bold);
                UnitCurrency_col.Name = "Unit_Currency";
                UnitCurrency_col.Width = 90;

                //AMOUNT
                Amount_col.HeaderText = "AMOUNT";
                Amount_col.DataPropertyName = "AMOUNT";
                Amount_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Amount_col.HeaderCell.Style.Font = new Font("Arial", 8.25F, FontStyle.Bold);
                Amount_col.Name = "Amount";
                Amount_col.Width = 100;

                //DATE CREATE
                DateCreate_col.HeaderText = "DATE CREATE";
                DateCreate_col.DataPropertyName = "DATE_CREATE";
                DateCreate_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DateCreate_col.HeaderCell.Style.Font = new Font("Arial", 8.25F, FontStyle.Bold);
                DateCreate_col.Name = "Date_Create";
                DateCreate_col.Width = 100;

                //LOCK STATUS
                LockStatus_col.HeaderText = "LOCK STATUS";
                LockStatus_col.DataPropertyName = "LOCK_STATUS";
                LockStatus_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                LockStatus_col.HeaderCell.Style.Font = new Font("Arial", 8.25F, FontStyle.Bold);
                LockStatus_col.Name = "Lock_Status";
                LockStatus_col.Width = 100;

                //CUSTOMER CODE
                this.GridView_Search.Columns["Customer_Code"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //SHIP TO
                this.GridView_Search.Columns["Ship_To"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //UNIT CURRENCY
                this.GridView_Search.Columns["Unit_Currency"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //DATE CREATE
                this.GridView_Search.Columns["Date_Create"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //ETD
                this.GridView_Search.Columns["ETD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //LOCK STATUS
                this.GridView_Search.Columns["Lock_Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //AMOUNT
                this.GridView_Search.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.GridView_Search.Columns["Amount"].DefaultCellStyle.Format = "#,##0.00##";
            }
            else if (_caseSearch == "btnSearch_Inv")
            {
                System.Windows.Forms.DataGridViewTextBoxColumn CustomerCode_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn ShipTo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn UnitCurrency_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn Amount_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn DateCreate_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn DateETD_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn ShippingNo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn InvoiceNo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn LockStatus_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn Revise_col = new System.Windows.Forms.DataGridViewTextBoxColumn();

                this.GridView_Search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] 
                    {
                          CustomerCode_col
                        , ShipTo_col
                        , UnitCurrency_col
                        , ShippingNo_col
                        , InvoiceNo_col
                        , DateETD_col
                        , Amount_col
                        , DateCreate_col
                        , LockStatus_col
                        , Revise_col
                    });
                //CUSTOMER CODE
                CustomerCode_col.HeaderText = "ISSUED TO";
                CustomerCode_col.DataPropertyName = "ISSUEDTO_CUSTOMER_CODE";
                CustomerCode_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                CustomerCode_col.HeaderCell.Style.Font = new Font("Arial", 8.25F, FontStyle.Bold);
                CustomerCode_col.Name = "Customer_Code";
                CustomerCode_col.Width = 90;

                //SHIP TO
                ShipTo_col.HeaderText = "SHIP TO";
                ShipTo_col.DataPropertyName = "SHIPTO_CUSTOMER_CODE";
                ShipTo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ShipTo_col.HeaderCell.Style.Font = new Font("Arial", 8.25F, FontStyle.Bold);
                ShipTo_col.Name = "Ship_To";
                ShipTo_col.Width = 90;

                //SHIPPING NO
                ShippingNo_col.HeaderText = "SHIPPING NO";
                ShippingNo_col.DataPropertyName = "SHIPPING_NO";
                ShippingNo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ShippingNo_col.HeaderCell.Style.Font = new Font("Arial", 8.25F, FontStyle.Bold);
                ShippingNo_col.Name = "Shipping_No";
                ShippingNo_col.Width = 160;

                //INVOICE NO
                InvoiceNo_col.HeaderText = "INVOICE NO";
                InvoiceNo_col.DataPropertyName = "INVOICE_NO";
                InvoiceNo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InvoiceNo_col.HeaderCell.Style.Font = new Font("Arial", 8.25F, FontStyle.Bold);
                InvoiceNo_col.Name = "Invoice_No";
                InvoiceNo_col.Width = 160;

                //ETD
                DateETD_col.HeaderText = "ETD";
                DateETD_col.DataPropertyName = "ETD";
                DateETD_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DateETD_col.HeaderCell.Style.Font = new Font("Arial", 8.25F, FontStyle.Bold);
                DateETD_col.Name = "ETD";
                DateETD_col.Width = 100;

                //UNIT CURRENCY
                UnitCurrency_col.HeaderText = "UNIT CURRENCY";
                UnitCurrency_col.DataPropertyName = "UNIT_CURRENCY";
                UnitCurrency_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                UnitCurrency_col.HeaderCell.Style.Font = new Font("Arial", 8.25F, FontStyle.Bold);
                UnitCurrency_col.Name = "Unit_Currency";
                UnitCurrency_col.Width = 90;

                //AMOUNT
                Amount_col.HeaderText = "AMOUNT";
                Amount_col.DataPropertyName = "AMOUNT";
                Amount_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Amount_col.HeaderCell.Style.Font = new Font("Arial", 8.25F, FontStyle.Bold);
                Amount_col.Name = "Amount";
                Amount_col.Width = 100;

                //DATE CREATE
                DateCreate_col.HeaderText = "DATE CREATE";
                DateCreate_col.DataPropertyName = "DATE_CREATE";
                DateCreate_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DateCreate_col.HeaderCell.Style.Font = new Font("Arial", 8.25F, FontStyle.Bold);
                DateCreate_col.Name = "Date_Create";
                DateCreate_col.Width = 100;

                //LOCK STATUS
                LockStatus_col.HeaderText = "LOCK STATUS";
                LockStatus_col.DataPropertyName = "LOCK_STATUS";
                LockStatus_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                LockStatus_col.HeaderCell.Style.Font = new Font("Arial", 8.25F, FontStyle.Bold);
                LockStatus_col.Name = "Lock_Status";
                LockStatus_col.Width = 100;

                //REVISE VERSION
                Revise_col.HeaderText = "REVISE VERSION";
                Revise_col.DataPropertyName = "REVISE_VERSION";
                Revise_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Revise_col.HeaderCell.Style.Font = new Font("Arial", 8.25F, FontStyle.Bold);
                Revise_col.Name = "Revise_Version";
                Revise_col.Width = 100;

                //CUSTOMER CODE
                this.GridView_Search.Columns["Customer_Code"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //SHIP TO
                this.GridView_Search.Columns["Ship_To"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //UNIT CURRENCY
                this.GridView_Search.Columns["Unit_Currency"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //DATE CREATE
                this.GridView_Search.Columns["Date_Create"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //ETD
                this.GridView_Search.Columns["ETD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //LOCK STATUS
                this.GridView_Search.Columns["Lock_Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //REVISE VERSION
                this.GridView_Search.Columns["Revise_Version"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                //AMOUNT
                this.GridView_Search.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.GridView_Search.Columns["Amount"].DefaultCellStyle.Format = "#,##0.00##";
            }
            else if (_caseSearch == "btnSearch_TradeCondition")
            {
                System.Windows.Forms.DataGridViewTextBoxColumn PriceCondition_col = new System.Windows.Forms.DataGridViewTextBoxColumn();

                this.GridView_Search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { PriceCondition_col });

                //PRICE CONDITION
                PriceCondition_col.HeaderText = "PRICE CONDITION";
                PriceCondition_col.DataPropertyName = "PRICE_COND";
                PriceCondition_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                PriceCondition_col.Name = "Price_Cond";
                PriceCondition_col.Width = 200;
            }
            else if (_caseSearch == "btnSearch_PaymentTerm")
            {
                System.Windows.Forms.DataGridViewTextBoxColumn PriceCondition_col = new System.Windows.Forms.DataGridViewTextBoxColumn();

                this.GridView_Search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { PriceCondition_col });

                //PAYMENT TERM
                PriceCondition_col.HeaderText = "PAYMENT TERM";
                PriceCondition_col.DataPropertyName = "PAYMENT_ID";
                PriceCondition_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                PriceCondition_col.Name = "Payment_Id";
                PriceCondition_col.Width = 450;
            }
            else if (_caseSearch == "btnSearch_PortLoading")
            {
                System.Windows.Forms.DataGridViewTextBoxColumn PortLoading_col = new System.Windows.Forms.DataGridViewTextBoxColumn();

                this.GridView_Search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { PortLoading_col });

                //PORT LOADING
                PortLoading_col.HeaderText = "PORT OF LOADING";
                PortLoading_col.DataPropertyName = "DESTINATION_ID";
                PortLoading_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                PortLoading_col.Name = "Loading_Id";
                PortLoading_col.Width = 250;
            }
            else if (_caseSearch == "btnSearch_PortDestination")
            {
                System.Windows.Forms.DataGridViewTextBoxColumn Destination_col = new System.Windows.Forms.DataGridViewTextBoxColumn();

                this.GridView_Search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { Destination_col });

                //PORT DESTINATION
                Destination_col.HeaderText = "PORT OF DESTINATION";
                Destination_col.DataPropertyName = "DESTINATION_ID";
                Destination_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                Destination_col.Name = "Destination_Id";
                Destination_col.Width = 250;
            }
            else if (_caseSearch == "btnSearch_IssuedTo" || _caseSearch == "btnSearch_ShipTo")
            {
                System.Windows.Forms.DataGridViewTextBoxColumn CustomerCode_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn CustomerName_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn Address_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn TelNo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn FaxNo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn InvoieFormat_col = new System.Windows.Forms.DataGridViewTextBoxColumn();

                this.GridView_Search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
                    {CustomerCode_col, CustomerName_col, Address_col, TelNo_col, FaxNo_col});

                //CUSTOMER
                CustomerCode_col.HeaderText = "CUSTOMER";
                CustomerCode_col.DataPropertyName = "CUSTOMER_CODE";
                CustomerCode_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                CustomerCode_col.Name = "Customer_Code";
                CustomerCode_col.Width = 90;

                //CUSTOMER NAME
                CustomerName_col.HeaderText = "CUSTOMER NAME";
                CustomerName_col.DataPropertyName = "CUSTOMER_NAME1";
                CustomerName_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                CustomerName_col.Name = "Customer_Name1";
                CustomerName_col.Width = 200;

                //ADDRESS
                Address_col.HeaderText = "ADDRESS";
                Address_col.DataPropertyName = "ADDRESS";
                Address_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                Address_col.Name = "Address";
                Address_col.Width = 150;

                //TEL_NO
                TelNo_col.HeaderText = "TELEPHONE NUMBER";
                TelNo_col.DataPropertyName = "TEL_NO";
                TelNo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                TelNo_col.Name = "Tel_No";
                TelNo_col.Width = 120;

                //FAX_NO
                FaxNo_col.HeaderText = "FAX NUMBER";
                FaxNo_col.DataPropertyName = "FAX_NO";
                FaxNo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                FaxNo_col.Name = "Fax_No";
                FaxNo_col.Width = 120;

                //INVOICE FORMAT
                FaxNo_col.HeaderText = "INVOICE FORMAT";
                FaxNo_col.DataPropertyName = "INVOICE_FORMAT";
                FaxNo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                FaxNo_col.Name = "Invoice_Format";
                FaxNo_col.Width = 140;
            }
            else if (_caseSearch == "btnSearch_CodeTVC")
            {
                System.Windows.Forms.DataGridViewTextBoxColumn CustomerCode_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn ItemCode_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn ItemName_col = new System.Windows.Forms.DataGridViewTextBoxColumn();

                this.GridView_Search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
                    {
                      CustomerCode_col
                    , ItemCode_col
                    , ItemName_col
                    });

                //CUSTOMER CODE
                CustomerCode_col.HeaderText = "CUSTOMER CODE";
                CustomerCode_col.DataPropertyName = "CUSTOMER_CODE";
                CustomerCode_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                CustomerCode_col.Name = "Customer_Code";
                CustomerCode_col.Width = 120;

                //TVC ITEM CODE
                ItemCode_col.HeaderText = "TVC ITEM CODE";
                ItemCode_col.DataPropertyName = "TVC_ITEM_CODE";
                ItemCode_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                ItemCode_col.Name = "TVC_Item_Code";
                ItemCode_col.Width = 150;

                //ITEM NAME
                ItemName_col.HeaderText = "TVC ITEM NAME";
                ItemName_col.DataPropertyName = "ITEM_NAME";
                ItemName_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                ItemName_col.Name = "TVC_Item_Name";
                ItemName_col.Width = 150;
            }
            else if (_caseSearch == "btnSearch_POCustomer")
            {
                System.Windows.Forms.DataGridViewTextBoxColumn CompanyCode_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn CustomerCode_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn ReceiveNo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn DueDate_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn CustomerPO_col = new System.Windows.Forms.DataGridViewTextBoxColumn();

                this.GridView_Search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
                    {
                      CompanyCode_col
                    , CustomerCode_col
                    , ReceiveNo_col
                    , CustomerPO_col
                    , DueDate_col
                    });

                //COMPANY CODE
                CompanyCode_col.HeaderText = "COMPANY CODE";
                CompanyCode_col.DataPropertyName = "COMPANY_CODE";
                CompanyCode_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                CompanyCode_col.Name = "Company_Code";
                CompanyCode_col.Width = 100;

                //CUSTOMER CODE
                CustomerCode_col.HeaderText = "CUSTOMER CODE";
                CustomerCode_col.DataPropertyName = "CUSTOMER_CODE";
                CustomerCode_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                CustomerCode_col.Name = "Customer_Code";
                CustomerCode_col.Width = 120;

                //RECEIVE NO
                ReceiveNo_col.HeaderText = "RECEIVE NO";
                ReceiveNo_col.DataPropertyName = "RECEIVE_NO";
                ReceiveNo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ReceiveNo_col.Name = "Receive_No";
                ReceiveNo_col.Width = 150;

                //DUE DATE
                DueDate_col.HeaderText = "DUE DATE";
                DueDate_col.DataPropertyName = "DUE_DATE";
                DueDate_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DueDate_col.Name = "Due_Date";
                DueDate_col.Width = 120;

                //CUSTOMER PO
                CustomerPO_col.HeaderText = "CUSTOMER PO";
                CustomerPO_col.DataPropertyName = "CUSTOMER_PO";
                CustomerPO_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                CustomerPO_col.Name = "Customer_PO";
                CustomerPO_col.Width = 124;

                //COMPANY CODE
                this.GridView_Search.Columns["Company_Code"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //DUE DATE
                this.GridView_Search.Columns["Due_Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            else if (_caseSearch == "btnSearch_InvoiceNo")
            {
                System.Windows.Forms.DataGridViewTextBoxColumn InvoiceNo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn DueDate_From_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn DueDate_To_col = new System.Windows.Forms.DataGridViewTextBoxColumn();

                this.GridView_Search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
                    {InvoiceNo_col, DueDate_From_col, DueDate_To_col});

                //INVOICE NO
                InvoiceNo_col.HeaderText = "INVOICE NO";
                InvoiceNo_col.DataPropertyName = "INVOICE_NO";
                InvoiceNo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InvoiceNo_col.Name = "Invoice_No";
                InvoiceNo_col.Width = 170;

                //DUE DATE FROM
                DueDate_From_col.HeaderText = "DUE DATE FROM";
                DueDate_From_col.DataPropertyName = "DUE_DATE_FROM";
                DueDate_From_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DueDate_From_col.Name = "Due_Date_From";
                DueDate_From_col.Width = 130;

                //DUE DATE TO
                DueDate_To_col.HeaderText = "DUE DATE TO";
                DueDate_To_col.DataPropertyName = "DUE_DATE_TO";
                DueDate_To_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DueDate_To_col.Name = "Due_Date_To";
                DueDate_To_col.Width = 130;

                this.GridView_Search.Columns["Invoice_No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.GridView_Search.Columns["Due_Date_From"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.GridView_Search.Columns["Due_Date_To"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        public void SettingInit(string _searchValue,string filter1)
        {
            try
            {
                if (_caseSearch == "btnSearch_Customer")
                {
                    GridView_Search.DataSource = _searchDAO.GetInfoCustomer(_searchValue);
                }
                else if (_caseSearch == "btnSearch_ReciveNo")
                {
                    GridView_Search.DataSource = _searchDAO.GetInfoPO(_searchValue);
                }
                else if (_caseSearch == "btnSearch_ShippingNo")
                {
                    GridView_Search.DataSource = _searchDAO.GetInfoShipping(_searchValue,filter1);
                    //
                    foreach (DataGridViewRow row in GridView_Search.Rows)
                    {
                        if (row.Cells["Lock_Status"].Value.ToString() == "0")
                        {
                            row.Cells["Lock_Status"].Value = "NORMAL";
                        }
                        else if (row.Cells["Lock_Status"].Value.ToString() == "1")
                        {
                            row.Cells["Lock_Status"].Value = "LOCK";
                        }
                        else if (row.Cells["Lock_Status"].Value.ToString() == "2")
                        {
                            row.Cells["Lock_Status"].Value = "REVISE";
                        }
                    }
                }
                else if (_caseSearch == "btnSearch_Inv")
                {
                    GridView_Search.DataSource = _searchDAO.GetInfoInvoice(_searchValue);
                    //
                    foreach (DataGridViewRow row in GridView_Search.Rows)
                    {
                        if(row.Cells["Lock_Status"].Value.ToString() == "0"
                            || row.Cells["Lock_Status"].Value.ToString() == "1")
                        {
                            row.Cells["Lock_Status"].Value = "NORMAL";
                        }
                        else if (row.Cells["Lock_Status"].Value.ToString() == "2")
                        {
                            row.Cells["Lock_Status"].Value = "REVISE";
                        }
                    }
                }
                else if (_caseSearch == "btnSearch_TradeCondition")
                {
                    GridView_Search.DataSource = _searchDAO.GetInfoPriceCondition(_searchValue);
                }
                else if (_caseSearch == "btnSearch_PaymentTerm")
                {
                    GridView_Search.DataSource = _searchDAO.GetInfoPaymentTerm(_searchValue);
                }
                else if (_caseSearch == "btnSearch_PortLoading" || _caseSearch == "btnSearch_PortDestination")
                {
                    GridView_Search.DataSource = _searchDAO.GetInfoDestination(_searchValue);
                }
                else if (_caseSearch == "btnSearch_IssuedTo")
                {
                    GridView_Search.DataSource = _searchDAO.GetCustomer_IssuedTo(_searchValue);
                }
                else if (_caseSearch == "btnSearch_ShipTo")
                {
                    GridView_Search.DataSource = _searchDAO.GetCustomer_ShipTo(_searchValue);
                }
                else if (_caseSearch == "btnSearch_CodeTVC")
                {
                    GridView_Search.DataSource = _searchDAO.GetInfoTVC_CodeItem(_searchValue);
                }
                else if (_caseSearch == "btnSearch_POCustomer")
                {
                    GridView_Search.DataSource = _searchDAO.GetInfoCustomerPO(_searchValue);
                }
                else if (_caseSearch == "btnSearch_InvoiceNo")
                {
                    GridView_Search.DataSource = _searchDAO.GetInfoInvoiceNo(_searchValue);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }

        private void GridView_Search_DoubleClick(object sender, EventArgs e)
        {
            if (_caseSearch == "btnSearch_Customer")
            {
                if(_formCall == "Form_PO")
                {
                    _companyInfo.CompanyCode = this.GridView_Search.CurrentRow.Cells[0].Value.ToString();
                    _companyInfo.CompanyName = this.GridView_Search.CurrentRow.Cells[1].Value.ToString();

                    this.Close();
                }
            }
            else if (_caseSearch == "btnSearch_ReciveNo")
            {
                _PoInfo.ReceiveNo = this.GridView_Search.CurrentRow.Cells[0].Value.ToString();
                _PoInfo.DueDate_From = Convert.ToDateTime(this.GridView_Search.CurrentRow.Cells[1].Value);
                _PoInfo.DueDate_To = Convert.ToDateTime(this.GridView_Search.CurrentRow.Cells[2].Value);

                this.Close();
            }
            else if (_caseSearch == "btnSearch_ShippingNo")
            {
                _shippingInfo.DateCreate = Convert.ToDateTime(this.GridView_Search.CurrentRow.Cells["Date_Create"].Value.ToString());
                _shippingInfo.ETD = Convert.ToDateTime(this.GridView_Search.CurrentRow.Cells["ETD"].Value.ToString());
                _shippingInfo.ShippingNo = this.GridView_Search.CurrentRow.Cells["Shipping_No"].Value.ToString();
                _shippingInfo.InvoiceNo = this.GridView_Search.CurrentRow.Cells["Invoice_No"].Value.ToString();
                _shippingInfo.Lock_Status = this.GridView_Search.CurrentRow.Cells["Lock_Status"].Value.ToString();

                this.Close();
            }
            else if (_caseSearch == "btnSearch_Inv")
            {
                _InvoiceInfo.DateCreate = Convert.ToDateTime(this.GridView_Search.CurrentRow.Cells["Date_Create"].Value.ToString());
                _InvoiceInfo.ETD = Convert.ToDateTime(this.GridView_Search.CurrentRow.Cells["ETD"].Value.ToString());
                _InvoiceInfo.Unit_Currency = this.GridView_Search.CurrentRow.Cells["Unit_Currency"].Value.ToString();
                _InvoiceInfo.Shipping_No = this.GridView_Search.CurrentRow.Cells["Shipping_No"].Value.ToString();
                _InvoiceInfo.InvoiceNo = this.GridView_Search.CurrentRow.Cells["Invoice_No"].Value.ToString();
                _InvoiceInfo.LockStatus = this.GridView_Search.CurrentRow.Cells["Lock_Status"].Value.ToString();

                this.Close();
            }
            else if (_caseSearch == "btnSearch_TradeCondition")
            {
                _priceCondition.PriceCondition = this.GridView_Search.CurrentRow.Cells[0].Value.ToString();

                this.Close();
            }
            else if (_caseSearch == "btnSearch_PaymentTerm")
            {
                _paymentTerm.PaymentID = this.GridView_Search.CurrentRow.Cells[0].Value.ToString();

                this.Close();
            }
            else if (_caseSearch == "btnSearch_PortLoading" || _caseSearch == "btnSearch_PortDestination")
            {
                _destinationInfo.DestinationID = this.GridView_Search.CurrentRow.Cells[0].Value.ToString();

                this.Close();
            }
            else if (_caseSearch == "btnSearch_IssuedTo" || _caseSearch == "btnSearch_ShipTo")
            {
                _companyInfo.CompanyCode = this.GridView_Search.CurrentRow.Cells[0].Value.ToString();
                _companyInfo.CompanyName = this.GridView_Search.CurrentRow.Cells[1].Value.ToString();
                _companyInfo.CompanyAddress = this.GridView_Search.CurrentRow.Cells[2].Value.ToString();
                _companyInfo.CompanyTelNo = this.GridView_Search.CurrentRow.Cells[3].Value.ToString();
                _companyInfo.CompanyFaxNo = this.GridView_Search.CurrentRow.Cells[4].Value.ToString();
                _companyInfo.InvoiceFormat = this.GridView_Search.CurrentRow.Cells[5].Value.ToString();

                this.Close();
            }
            else if (_caseSearch == "btnSearch_CodeTVC")
            {
                _itemCodeInfo.TVC_ItemCode = this.GridView_Search.CurrentRow.Cells[1].Value.ToString();

                this.Close();
            }
            else if (_caseSearch == "btnSearch_POCustomer")
            {
                _itemCodeInfo.CustomerPO = this.GridView_Search.CurrentRow.Cells["Customer_PO"].Value.ToString();
                _itemCodeInfo.DueDate_PO = Convert.ToDateTime(this.GridView_Search.CurrentRow.Cells["Due_Date"].Value);

                this.Close();
            }
            else if (_caseSearch == "btnSearch_InvoiceNo")
            {
                _InvoiceInfo.InvoiceNo = this.GridView_Search.CurrentRow.Cells["Invoice_No"].Value.ToString();
                _InvoiceInfo.Due_Date_From = Convert.ToDateTime(this.GridView_Search.CurrentRow.Cells["Due_Date_From"].Value);
                _InvoiceInfo.Due_Date_To = Convert.ToDateTime(this.GridView_Search.CurrentRow.Cells["Due_Date_To"].Value);

                this.Close();
            }
        }

        //Draw number order
        private void GridView_Search_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        #region ButtonTop
        private void picBox_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picBox_Min_MouseEnter(object sender, EventArgs e)
        {
            picBox_Min.Size = new Size(27, 27);
        }

        private void picBox_Max_MouseEnter(object sender, EventArgs e)
        {
            picBox_Max.Size = new Size(27, 27);
        }

        private void picBox_Close_MouseEnter(object sender, EventArgs e)
        {
            picBox_Close.Size = new Size(27, 27);
        }

        private void picBox_Min_MouseLeave(object sender, EventArgs e)
        {
            picBox_Min.Size = new Size(25, 25);
        }

        private void picBox_Max_MouseLeave(object sender, EventArgs e)
        {
            picBox_Max.Size = new Size(25, 25);
        }

        private void picBox_Close_MouseLeave(object sender, EventArgs e)
        {
            picBox_Close.Size = new Size(25, 25);
        }

        private void picBox_Max_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                picBox_Max.Image = Properties.Resources.Maximize_window;
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                picBox_Max.Image = Properties.Resources.Zoom_full;
            }
        }
        private void picBox_Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region Moveable
        bool mouseDown = false;
        Point startPoint = new Point(0, 0);

        private void panelTop_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                picBox_Max.Image = Properties.Resources.Zoom_full;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.CenterScreen;
                picBox_Max.Image = Properties.Resources.Maximize_window;
            }
        }

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
        #endregion

        private void GridView_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                GridView_Search_DoubleClick(sender, e);
            }
            if (e.KeyCode == Keys.Tab)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtKeySearch1_Validated(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtKeySearch1.Text) ||
                !String.IsNullOrEmpty(txtFilter1.Text)) { 
                SettingInit(txtKeySearch1.Text, txtFilter1.Text);
            }
        }

        private void txtKeySearch1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SettingInit(txtKeySearch1.Text, txtFilter1.Text);
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtFilter1_Validated(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtKeySearch1.Text) ||
                !String.IsNullOrEmpty(txtFilter1.Text)) {
                SettingInit(txtKeySearch1.Text, txtFilter1.Text);
            }
        }

        private void txtFilter1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!String.IsNullOrEmpty(txtKeySearch1.Text) ||
                !String.IsNullOrEmpty(txtFilter1.Text))
                {
                    SettingInit(txtKeySearch1.Text, txtFilter1.Text);
                    this.SelectNextControl((Control)sender, true, true, true, true);
                }
            }
        }
    }
}
