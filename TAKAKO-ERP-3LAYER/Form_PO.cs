using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TAKAKO_ERP_3LAYER.DAL;
using TAKAKO_ERP_3LAYER.DAO;

namespace TAKAKO_ERP_3LAYER
{
    public partial class Form_PO : Form
    {
        public PO_DAO _PoDAO;

        public SYSTEM_DAL _systemDAL;

        /// <summary>
        /// Gets the Receive Date.
        /// </summary>
        public DateTime _receiveDate
        {
            get { return dtp_DueDateFrom.Value; }
        }

        /// <summary>
        /// Gets the Customer Code.
        /// </summary>
        public string _customerCode
        {
            get { return txt_CustomerCode.Text.Trim(); }
        }

        /// <summary>
        /// Gets the Receive No.
        /// </summary>
        public string _receiveNo
        {
            get { return txtReceiveNo.Text.Trim(); }
        }

        public Form_PO(SYSTEM_DAL _formMaiSystemDAL)
        {
            InitializeComponent();

            //Setting no title but have border
            this.ControlBox = false;
            this.Text = String.Empty;

            _systemDAL = _formMaiSystemDAL;
        }

        private void Form_PO_Load(object sender, EventArgs e)
        {
            _PoDAO = new PO_DAO();

            //Format Gridview
            SettingInitGridView();

            //Set data gridview
            GetInfoPO(dtp_DueDateFrom.Value
                     ,dtp_DueDateTo.Value
                     ,txt_CustomerCode.Text
                     ,txtReceiveNo.Text
                     ,txtCodeTVC.Text
                     ,txtPOCustomer.Text
                     ,txtShippingNo.Text);
        }

        public void SettingInit()
        {
            dtp_DueDateFrom.Value = DateTime.Now;
        }

        public void SettingInitGridView()
        {
            System.Windows.Forms.DataGridViewTextBoxColumn CompanyCode_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn CustomerCode_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn ReceiveNo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn CodeTVC_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn CodeTSP_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Name1_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Name2_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Quantity_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn PriceUnit_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn PriceJPY_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn PriceUSD_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn AmountJPY_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn AmountUSD_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn PoTSP_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn OrderDate_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn DueDate_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn CodeCustomer_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn PoCustomer_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Note_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn ShippingInstruction_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn SailingDate_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn InvoiceNo_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn InvoiceDate_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn QuantityOutput_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn QuantityInv_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Balance_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.GridView_PO.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] 
                {
                     CompanyCode_col
                    ,CustomerCode_col
                    ,ReceiveNo_col
                    ,CodeTVC_col
                    ,CodeTSP_col
                    ,Name1_col
                    ,Name2_col
                    ,Quantity_col
                    ,PriceUnit_col
                    ,PriceJPY_col
                    ,PriceUSD_col
                    ,AmountJPY_col
                    ,AmountUSD_col
                    ,PoTSP_col
                    ,OrderDate_col
                    ,DueDate_col
                    ,CodeCustomer_col
                    ,PoCustomer_col
                    ,Note_col
                    ,ShippingInstruction_col
                    ,SailingDate_Col
                    ,InvoiceNo_Col
                    ,InvoiceDate_Col
                    ,QuantityOutput_Col
                    ,QuantityInv_Col
                    ,Balance_Col
                });

            //COMPANY CODE
            CompanyCode_col.HeaderText = "COMPANY CODE";
            CompanyCode_col.DataPropertyName = "COMPANY_CODE";
            CompanyCode_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CompanyCode_col.Name = "Company_Code";
            CompanyCode_col.Width = 100;

            //CUSTOMER CODE
            CustomerCode_col.HeaderText = "CUSTOMER";
            CustomerCode_col.DataPropertyName = "CUSTOMER_CODE";
            CustomerCode_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CustomerCode_col.Name = "Customer_Code";
            CustomerCode_col.Width = 100;

            //RECEIVE NO
            ReceiveNo_col.HeaderText = "RECEIVE NO";
            ReceiveNo_col.DataPropertyName = "RECEIVE_NO";
            ReceiveNo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ReceiveNo_col.Name = "Receive_No";
            ReceiveNo_col.Width = 130;

            //CODE OF TVC
            CodeTVC_col.HeaderText = "ITEM CODE OF TVC";
            CodeTVC_col.DataPropertyName = "TVC_ITEM_CODE";
            CodeTVC_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CodeTVC_col.Name = "TVCItemCode_Column";
            CodeTVC_col.Width = 120;

            //ITEM CODE OF CUSTOMER
            CodeTSP_col.HeaderText = "ITEM CODE OF CUSTOMER";
            CodeTSP_col.DataPropertyName = "CUSTOMER_ITEM_CODE";
            CodeTSP_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CodeTSP_col.Name = "Cus_ItemCode_Col";
            CodeTSP_col.Width = 120;

            //PARTS NAME
            Name1_col.HeaderText = "PARTS NAME";
            Name1_col.DataPropertyName = "PARTS_NAME";
            Name1_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Name1_col.Name = "PartsName_Col";
            Name1_col.Width = 110;

            //ITEM NAME
            Name2_col.HeaderText = "ITEM NAME";
            Name2_col.DataPropertyName = "ITEM_NAME";
            Name2_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Name2_col.Name = "ItemName_Col";
            Name2_col.Width = 110;

            //QTY (PCS)
            Quantity_col.HeaderText = "QTY (PCS)";
            Quantity_col.DataPropertyName = "QUANTITY";
            Quantity_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            Quantity_col.Name = "Qty_Column";
            Quantity_col.Width = 100;

            //Unit Price
            PriceUnit_col.HeaderText = "PRICE CURRENCY";
            PriceUnit_col.DataPropertyName = "UNIT_CURRENCY";
            PriceUnit_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PriceUnit_col.Name = "UnitCurrency_Column";
            PriceUnit_col.Width = 80;

            //Price JPY
            PriceJPY_col.HeaderText = "PRICE JPY";
            PriceJPY_col.DataPropertyName = "PRICE_JPY";
            PriceJPY_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            PriceJPY_col.Name = "PriceJPY_Column";
            PriceJPY_col.Width = 100;

            //Price USD
            PriceUSD_col.HeaderText = "PRICE USD";
            PriceUSD_col.DataPropertyName = "PRICE_USD";
            PriceUSD_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            PriceUSD_col.Name = "PriceUSD_Column";
            PriceUSD_col.Width = 100;

            //Amount JPY
            AmountJPY_col.HeaderText = "AMOUNT JPY";
            AmountJPY_col.DataPropertyName = "Amount";
            AmountJPY_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AmountJPY_col.Name = "AmountJPY_Column";
            AmountJPY_col.Width = 100;

            //Amount USD
            AmountUSD_col.HeaderText = "AMOUNT USD";
            AmountUSD_col.DataPropertyName = "AMOUNT_USD";
            AmountUSD_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AmountUSD_col.Name = "AmountUSD_Column";
            AmountUSD_col.Width = 100;

            //PO OF CUSTOMER
            PoTSP_col.HeaderText = "PO OF CUSTOMER";
            PoTSP_col.DataPropertyName = "CUSTOMER_PO";
            PoTSP_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PoTSP_col.Name = "Cus_PO_col";
            PoTSP_col.Width = 120;

            //Order Date (dd/mm/yy)
            OrderDate_col.HeaderText = "ORDER DATE\n(dd/MM/yyyy)";
            OrderDate_col.DataPropertyName = "ORDER_DATE";
            OrderDate_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OrderDate_col.Name = "OrderDate_Column";
            OrderDate_col.Width = 120;

            //Due Date (dd/mm/ yy)
            DueDate_col.HeaderText = "DUE DATE\n(dd/MM/yyyy)";
            DueDate_col.DataPropertyName = "DUE_DATE";
            DueDate_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DueDate_col.Name = "DueDate_Column";
            DueDate_col.Width = 120;

            //CODE OF CUSTOMER
            CodeCustomer_col.HeaderText = "THIRD PARTY\nITEM CODE";
            CodeCustomer_col.DataPropertyName = "THIRD_PARTY_ITEM_CODE";
            CodeCustomer_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CodeCustomer_col.Name = "Third_Itemcode_Col";
            CodeCustomer_col.Width = 120;

            //PO No. of Customer
            PoCustomer_col.HeaderText = "THIRD PARTY PO";
            PoCustomer_col.DataPropertyName = "THIRD_PARTY_PO";
            PoCustomer_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PoCustomer_col.Name = "Third_Party_PO";
            PoCustomer_col.Width = 120;

            //NOTE
            Note_col.HeaderText = "NOTE";
            Note_col.DataPropertyName = "NOTE";
            Note_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Note_col.Name = "Note_Column";
            Note_col.Width = 140;

            //SHIPPING INSTRUCTION No.
            ShippingInstruction_col.HeaderText = "SHIPPING INSTRUCTION No.";
            ShippingInstruction_col.DataPropertyName = "Shipping_No";
            ShippingInstruction_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ShippingInstruction_col.Name = "Shipping_No";
            ShippingInstruction_col.Width = 150;

            //SAILING DATE
            SailingDate_Col.HeaderText = "SAILING DATE";
            SailingDate_Col.DataPropertyName = "Sailing_Date";
            SailingDate_Col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            SailingDate_Col.Name = "Sailing_Date";
            SailingDate_Col.Width = 120;

            //INVOICE NO
            InvoiceNo_Col.HeaderText = "INVOICE NO";
            InvoiceNo_Col.DataPropertyName = "Invoice_No";
            InvoiceNo_Col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            InvoiceNo_Col.Name = "Invoice_No";
            InvoiceNo_Col.Width = 120;

            //INVOICE DATE
            InvoiceDate_Col.HeaderText = "INVOICE DATE";
            InvoiceDate_Col.DataPropertyName = "Invoice_Date";
            InvoiceDate_Col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            InvoiceDate_Col.Name = "Invoice_Date";
            InvoiceDate_Col.Width = 120;

            //QUANTITY OUTPUT
            QuantityOutput_Col.HeaderText = "QTY ORDER(PCS)";
            QuantityOutput_Col.DataPropertyName = "QUANTITY";
            QuantityOutput_Col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            QuantityOutput_Col.Name = "Quantity_Output";
            QuantityOutput_Col.Width = 120;

            //QUANTITY INVOICE
            QuantityInv_Col.HeaderText = "QTY INVOICE(PCS)";
            QuantityInv_Col.DataPropertyName = "QUANTITY_ORDER";
            QuantityInv_Col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            QuantityInv_Col.Name = "Quantity_Invoice";
            QuantityInv_Col.Width = 120;

            //BALANCE
            Balance_Col.HeaderText = "BALANCE";
            Balance_Col.DataPropertyName = "Balance";
            Balance_Col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Balance_Col.Name = "Balance";
            Balance_Col.Width = 120;

            this.GridView_PO.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            this.GridView_PO.AllowUserToAddRows = false;

            //COMPANY CODE
            this.GridView_PO.Columns["Company_Code"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //QTY (PCS)
            this.GridView_PO.Columns["Qty_Column"].DefaultCellStyle.Format = "#,##0.##";
            this.GridView_PO.Columns["Qty_Column"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //UNIT PRICE
            this.GridView_PO.Columns["UnitCurrency_Column"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //PRICE JPY
            this.GridView_PO.Columns["PriceJPY_Column"].DefaultCellStyle.Format = "#,##0.00";
            this.GridView_PO.Columns["PriceJPY_Column"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.GridView_PO.Columns["PriceJPY_Column"].SortMode = DataGridViewColumnSortMode.NotSortable;
            //Price USD
            this.GridView_PO.Columns["PriceUSD_Column"].DefaultCellStyle.Format = "#,##0.00";
            this.GridView_PO.Columns["PriceUSD_Column"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.GridView_PO.Columns["PriceUSD_Column"].SortMode = DataGridViewColumnSortMode.NotSortable;
            //Amount JPY
            this.GridView_PO.Columns["AmountJPY_Column"].DefaultCellStyle.Format = "#,##0.00";
            this.GridView_PO.Columns["AmountJPY_Column"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.GridView_PO.Columns["AmountJPY_Column"].SortMode = DataGridViewColumnSortMode.NotSortable;
            //Amount USD
            this.GridView_PO.Columns["AmountUSD_Column"].DefaultCellStyle.Format = "#,##0.00";
            this.GridView_PO.Columns["AmountUSD_Column"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.GridView_PO.Columns["AmountUSD_Column"].SortMode = DataGridViewColumnSortMode.NotSortable;
            //Order Date (dd/mm/yy)
            this.GridView_PO.Columns["OrderDate_Column"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Due Date (dd/mm/yy)
            this.GridView_PO.Columns["DueDate_Column"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //SAILING DATE
            this.GridView_PO.Columns["Sailing_Date"].DefaultCellStyle.Format = "dd/MM/yyyy";
            this.GridView_PO.Columns["Sailing_Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //INVOCE DATE
            this.GridView_PO.Columns["Invoice_Date"].DefaultCellStyle.Format = "dd/MM/yyyy";
            this.GridView_PO.Columns["Invoice_Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //QTY OUTPUT(PCS)
            this.GridView_PO.Columns["Quantity_Output"].DefaultCellStyle.Format = "#,##0.##";
            this.GridView_PO.Columns["Quantity_Output"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //QTY INVOICE(PCS)
            this.GridView_PO.Columns["Quantity_Invoice"].DefaultCellStyle.Format = "#,##0.##";
            this.GridView_PO.Columns["Quantity_Invoice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //BALANCE
            this.GridView_PO.Columns["Balance"].DefaultCellStyle.Format = "#,##0.##";
            this.GridView_PO.Columns["Balance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void GetInfoPO(DateTime DueDateFrom, DateTime DueDateTo, string customerCode, string receiveNo, string codeTVC, string POCustomer, string ShippingNo)
        {
            DataTable _tempTable = new DataTable();

            try
            {
                _tempTable = _PoDAO.GetInfoPO(DueDateFrom, DueDateTo, customerCode, receiveNo, codeTVC, POCustomer, ShippingNo);

                if (_tempTable.Rows.Count > 0)
                {
                    GridView_PO.Rows.Clear();
                    foreach (DataRow dr in _tempTable.Rows)
                    {
                        if (!String.IsNullOrEmpty(Convert.ToString(dr["TVC_ITEM_CODE"])))
                        {
                            //if (Convert.ToString)
                            int indexInv = GridView_PO.Rows.Add();

                            GridView_PO.Rows[indexInv].Cells["Company_Code"].Value = dr ["COMPANY_CODE"].ToString();
                            GridView_PO.Rows[indexInv].Cells["Customer_Code"].Value = dr["CUSTOMER_CODE"].ToString();
                            GridView_PO.Rows[indexInv].Cells["Receive_No"].Value = dr["RECEIVE_NO"].ToString();
                            GridView_PO.Rows[indexInv].Cells["TVCItemCode_Column"].Value = dr["TVC_ITEM_CODE"].ToString();
                            GridView_PO.Rows[indexInv].Cells["Cus_ItemCode_Col"].Value = dr["CUSTOMER_ITEM_CODE"].ToString();
                            GridView_PO.Rows[indexInv].Cells["PartsName_Col"].Value = dr["PARTS_NAME"].ToString();
                            GridView_PO.Rows[indexInv].Cells["ItemName_Col"].Value = dr["ITEM_NAME"].ToString();
                            GridView_PO.Rows[indexInv].Cells["Qty_Column"].Value = dr["QUANTITY"];
                            GridView_PO.Rows[indexInv].Cells["UnitCurrency_Column"].Value = dr["UNIT_CURRENCY"].ToString();
                            if(dr["UNIT_CURRENCY"].ToString().ToUpper() == "JPY")
                            {
                                GridView_PO.Rows[indexInv].Cells["PriceJPY_Column"].Value = Convert.ToDecimal(dr["ORDER_PRICE"]);
                                GridView_PO.Rows[indexInv].Cells["PriceUSD_Column"].Value = 0;
                                GridView_PO.Rows[indexInv].Cells["AmountJPY_Column"].Value = Convert.ToDecimal(dr["ORDER_PRICE"]) * Convert.ToDecimal(dr["QUANTITY"]);
                                GridView_PO.Rows[indexInv].Cells["AmountUSD_Column"].Value = 0;
                            }
                            else if(dr["UNIT_CURRENCY"].ToString().ToUpper() == "USD")
                            {
                                GridView_PO.Rows[indexInv].Cells["PriceUSD_Column"].Value = Convert.ToDecimal(dr["ORDER_PRICE"]);
                                GridView_PO.Rows[indexInv].Cells["PriceJPY_Column"].Value = 0;
                                GridView_PO.Rows[indexInv].Cells["AmountUSD_Column"].Value = Convert.ToDecimal(dr["ORDER_PRICE"]) * Convert.ToDecimal(dr["QUANTITY"]);
                                GridView_PO.Rows[indexInv].Cells["AmountJPY_Column"].Value = 0;
                            }
                            GridView_PO.Rows[indexInv].Cells["Cus_PO_col"].Value = dr["CUSTOMER_PO"].ToString();
                            GridView_PO.Rows[indexInv].Cells["OrderDate_Column"].Value = Convert.ToDateTime(dr["ORDER_DATE"]).ToString("dd/MM/yyyy");
                            GridView_PO.Rows[indexInv].Cells["DueDate_Column"].Value = Convert.ToDateTime(dr["DUE_DATE"]).ToString("dd/MM/yyyy");
                            GridView_PO.Rows[indexInv].Cells["Third_Itemcode_Col"].Value = dr["THIRD_PARTY_ITEM_CODE"].ToString();
                            GridView_PO.Rows[indexInv].Cells["Third_Party_PO"].Value = dr["THIRD_PARTY_PO"].ToString();
                            GridView_PO.Rows[indexInv].Cells["Note_Column"].Value = dr["NOTE"].ToString();
                            GridView_PO.Rows[indexInv].Cells["Shipping_No"].Value = dr["SHIPPING_NO"].ToString();
                            if (dr["SAILING_DATE"] != DBNull.Value)
                            { 
                                GridView_PO.Rows[indexInv].Cells["Sailing_Date"].Value = Convert.ToDateTime(dr["SAILING_DATE"]).ToString("dd/MM/yyyy");
                            }
                            //INVOICE NO
                            string _tempInvoiceNo = Convert.ToString(dr["INVOICE_NO"]);
                            if (!String.IsNullOrEmpty(_tempInvoiceNo))
                            { 
                                GridView_PO.Rows[indexInv].Cells["Invoice_No"].Value = _tempInvoiceNo;
                            }
                            //DATE CREATE
                            string _tempDateCreate = Convert.ToString(dr["DATE_CREATE"]);
                            if (!String.IsNullOrEmpty(_tempDateCreate))
                            {
                                GridView_PO.Rows[indexInv].Cells["Invoice_Date"].Value = Convert.ToDateTime(_tempDateCreate).ToString("dd/MM/yyyy");
                            }
                            GridView_PO.Rows[indexInv].Cells["Quantity_Output"].Value = Convert.ToDecimal(dr["QUANTITY"]);
                            //QUANTITY INVOICE
                            string _tempQuantityInv = Convert.ToString(dr["QUANTITY_INVOICE"]);
                            if(!String.IsNullOrEmpty(_tempQuantityInv))
                            { 
                                GridView_PO.Rows[indexInv].Cells["Quantity_Invoice"].Value = Convert.ToDecimal(_tempQuantityInv);
                            }
                            //BALANCE
                            string _tempBalance = Convert.ToString(dr["BALANCE"]);
                            if (!String.IsNullOrEmpty(_tempBalance))
                            {
                                GridView_PO.Rows[indexInv].Cells["Balance"].Value = Convert.ToDecimal(_tempBalance);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No data PO from " + dtp_DueDateFrom.Value.ToString("dd/MM/yyyy") 
                                              + " to " + dtp_DueDateTo.Value.ToString("dd/MM/yyyy")
                                              , "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //GridView_PO.DataSource = _tempTable;
                    dtp_DueDateFrom.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }

        private void Form_PO_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button_Import_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form_Import_PO _formImportPO = new Form_Import_PO(_systemDAL);
            _formImportPO.StartPosition = FormStartPosition.CenterParent;
            _formImportPO.Closed += (s, args) => this.Show();
            _formImportPO.ShowDialog();
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            dtp_DueDateFrom.Value = DateTime.Now;
            dtp_DueDateTo.Value = DateTime.Now;
            txt_CustomerCode.Clear();
            txt_CustomerName.Clear();
            txtReceiveNo.Clear();
            txtCodeTVC.Clear();
            txtPOCustomer.Clear();
            txtShippingNo.Clear();

            //Clear and setting init GridviewPO
            GridView_PO.Rows.Clear();
            SettingInitGridView();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //Set data gridview
            GetInfoPO(dtp_DueDateFrom.Value
                     ,dtp_DueDateTo.Value
                     ,txt_CustomerCode.Text
                     ,txtReceiveNo.Text
                     ,txtCodeTVC.Text
                     ,txtPOCustomer.Text
                     ,txtShippingNo.Text);
        }

        private void btnSearch_ReciveNo_Click(object sender, EventArgs e)
        {
            Form_Search _formSearch = new Form_Search("btnSearch_ReciveNo", this.Name);
            _formSearch.StartPosition = FormStartPosition.CenterParent;
            _formSearch.ShowDialog();

            if (_formSearch._PoInfo.ReceiveNo != null 
                && _formSearch._PoInfo.DueDate_From != null)
            { 
                txtReceiveNo.Text = _formSearch._PoInfo.ReceiveNo;
                dtp_DueDateFrom.Value = _formSearch._PoInfo.DueDate_From;
                dtp_DueDateTo.Value = _formSearch._PoInfo.DueDate_To;
            }
        }

        private void btnSearch_Customer_Click(object sender, EventArgs e)
        {
            Form_Search _formSearch = new Form_Search("btnSearch_Customer", this.Name);
            _formSearch.StartPosition = FormStartPosition.CenterParent;
            _formSearch.ShowDialog();

            if (_formSearch._companyInfo.CompanyCode != null)
            { 
                txt_CustomerCode.Text = _formSearch._companyInfo.CompanyCode;
                txt_CustomerName.Text = _formSearch._companyInfo.CompanyName;
                this.SelectNextControl((Control)sender, true, true, true, true);
            } else {
                txt_CustomerCode.Focus();
            }
        }

        private void btnSearch_CodeTVC_Click(object sender, EventArgs e)
        {
            Form_Search _formSearch = new Form_Search("btnSearch_CodeTVC", this.Name);
            _formSearch.StartPosition = FormStartPosition.CenterParent;
            _formSearch.ShowDialog();

            if (_formSearch._itemCodeInfo.TVC_ItemCode != null)
            {
                txtCodeTVC.Text = _formSearch._itemCodeInfo.TVC_ItemCode;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
            else
            {
                txtCodeTVC.Focus();
            }
        }

        private void btnSearch_POCustomer_Click(object sender, EventArgs e)
        {
            Form_Search _formSearch = new Form_Search("btnSearch_POCustomer", this.Name);
            _formSearch.StartPosition = FormStartPosition.CenterParent;
            _formSearch.ShowDialog();

            if (_formSearch._itemCodeInfo.CustomerPO != null)
            {
                txtPOCustomer.Text = _formSearch._itemCodeInfo.CustomerPO;
                dtp_DueDateFrom.Value = _formSearch._itemCodeInfo.DueDate_PO;
                dtp_DueDateTo.Value = _formSearch._itemCodeInfo.DueDate_PO;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
            else
            {
                txtCodeTVC.Focus();
            }
        }

        private void btnSearch_InvoiceNo_Click(object sender, EventArgs e)
        {
            Form_Search _formSearch = new Form_Search("btnSearch_ShippingNo", this.Name);
            _formSearch.StartPosition = FormStartPosition.CenterParent;
            _formSearch.ShowDialog();

            if (_formSearch._shippingInfo.ShippingNo != null)
            {
                txtShippingNo.Text = _formSearch._shippingInfo.ShippingNo;
                //dtp_DueDateFrom.Value = _formSearch._shippingInfo.DateCreate;
                //dtp_DueDateTo.Value = _formSearch._shippingInfo.DateCreate;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
            else
            {
                txtShippingNo.Focus();
            }
        }

        //Draw number order
        private void GridView_PO_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        /// <summary>
        /// btn_Cancel_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
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

        private void picBox_BackToMain_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.picBox_BackToMain, "Back to Main Screen");
        }

        private void picBox_BackToMain_Click(object sender, EventArgs e)
        {
            string exitMessageText = "Are you sure you want to return to the main app?";
            string exitCaption = "Confirm";
            MessageBoxButtons button = MessageBoxButtons.YesNo;
            DialogResult res = MessageBox.Show(exitMessageText, exitCaption, button, MessageBoxIcon.Exclamation);
            if (res == DialogResult.Yes)
            {
                this.Hide();
                Form_Main _formMain = new Form_Main(_systemDAL);
                _formMain.Show();
            }
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

        private void panelTop_MouseDoubleClick(object sender, MouseEventArgs e)
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Chose Template Detail PO";
            theDialog.Filter = "Files Excel|*.xlsx";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string _filePath = theDialog.FileName;

                    //Create fileinfo object of an excel file
                    FileInfo _fileInfo = new FileInfo(_filePath);

                    //Create a new Excel package from the file
                    using (ExcelPackage _excelPackage = new ExcelPackage(_fileInfo))
                    {
                        int rowCount = 0;
                        DataRow drLocal = null;

                        #region Export Detail PO
                        ExcelWorksheet _PODetailSheet = _excelPackage.Workbook.Worksheets[1];

                        //
                        DataTable _tempPODetail = new DataTable();
                        _tempPODetail.Columns.Add("No");
                        _tempPODetail.Columns["No"].DataType = typeof(Int32);
                        _tempPODetail.Columns.Add("Company_Code");
                        _tempPODetail.Columns.Add("Customer_Code");
                        _tempPODetail.Columns.Add("Receive_No");
                        _tempPODetail.Columns.Add("Code_TVC");
                        _tempPODetail.Columns.Add("Code_TSP");
                        _tempPODetail.Columns.Add("Name1");
                        _tempPODetail.Columns.Add("Name2");
                        _tempPODetail.Columns.Add("Qty_PCS");
                        _tempPODetail.Columns["Qty_PCS"].DataType = typeof(Decimal);
                        _tempPODetail.Columns.Add("UNIT_CURRENCY");
                        _tempPODetail.Columns.Add("Price_JPY");
                        _tempPODetail.Columns["Price_JPY"].DataType = typeof(Decimal);
                        _tempPODetail.Columns.Add("Price_USD");
                        _tempPODetail.Columns["Price_USD"].DataType = typeof(Decimal);
                        _tempPODetail.Columns.Add("Amount_JPY");
                        _tempPODetail.Columns["Amount_JPY"].DataType = typeof(Decimal);
                        _tempPODetail.Columns.Add("Amount_USD");
                        _tempPODetail.Columns["Amount_USD"].DataType = typeof(Decimal);
                        _tempPODetail.Columns.Add("PO_TSP");
                        _tempPODetail.Columns.Add("Order_Date");
                        _tempPODetail.Columns["Order_Date"].DataType = typeof(DateTime);
                        _tempPODetail.Columns.Add("Due_Date");
                        _tempPODetail.Columns["Due_Date"].DataType = typeof(DateTime);
                        _tempPODetail.Columns.Add("Code_Customer");
                        _tempPODetail.Columns.Add("PO_No_Customer");
                        _tempPODetail.Columns.Add("Note");
                        _tempPODetail.Columns.Add("Shipping");
                        _tempPODetail.Columns.Add("Sailing_Date");
                        _tempPODetail.Columns["Sailing_Date"].DataType = typeof(DateTime);
                        _tempPODetail.Columns.Add("InvoiceNo");
                        _tempPODetail.Columns.Add("Invoice_Date");
                        _tempPODetail.Columns["Invoice_Date"].DataType = typeof(DateTime);
                        _tempPODetail.Columns.Add("Quantity_Order");
                        _tempPODetail.Columns["Quantity_Order"].DataType = typeof(Decimal);
                        _tempPODetail.Columns.Add("Quantity_Invoice");
                        _tempPODetail.Columns["Quantity_Invoice"].DataType = typeof(Decimal);
                        _tempPODetail.Columns.Add("Balance");
                        _tempPODetail.Columns["Balance"].DataType = typeof(Decimal);

                        int _noIndex = 1;
                        rowCount = GridView_PO.Rows.Count;
                        foreach (DataGridViewRow dr in GridView_PO.Rows)
                        {
                            if (!dr.IsNewRow)
                            {
                                drLocal = _tempPODetail.NewRow();
                                drLocal["No"] = _noIndex;
                                drLocal["Customer_Code"] = dr.Cells["Customer_Code"].Value;
                                drLocal["Company_Code"] = dr.Cells["Company_Code"].Value;
                                drLocal["Receive_No"] = dr.Cells["Receive_No"].Value;
                                drLocal["Code_TVC"] = dr.Cells["TVCItemCode_Column"].Value;
                                drLocal["Code_TSP"] = dr.Cells["Cus_ItemCode_Col"].Value;
                                drLocal["Name1"] = dr.Cells["PartsName_Col"].Value;
                                drLocal["Name2"] = dr.Cells["ItemName_Col"].Value;
                                drLocal["Qty_PCS"] = Convert.ToDecimal(dr.Cells["Qty_Column"].Value);
                                drLocal["UNIT_CURRENCY"] = dr.Cells["UnitCurrency_Column"].Value;
                                drLocal["Price_JPY"] = dr.Cells["PriceJPY_Column"].Value;
                                drLocal["Price_USD"] = dr.Cells["PriceUSD_Column"].Value;
                                drLocal["Amount_JPY"] = dr.Cells["AmountJPY_Column"].Value;
                                drLocal["Amount_USD"] = dr.Cells["AmountUSD_Column"].Value;
                                drLocal["PO_TSP"] = dr.Cells["Cus_PO_col"].Value;
                                drLocal["Order_Date"] = dr.Cells["OrderDate_Column"].Value;
                                drLocal["Due_Date"] = dr.Cells["DueDate_Column"].Value;
                                drLocal["Code_Customer"] = dr.Cells["Third_Itemcode_Col"].Value;
                                drLocal["PO_No_Customer"] = dr.Cells["Third_Party_PO"].Value;
                                drLocal["Note"] = dr.Cells["Note_Column"].Value;
                                drLocal["Shipping"] = dr.Cells["Shipping_No"].Value;
                                if (dr.Cells["Sailing_Date"].Value != null)
                                { 
                                    drLocal["Sailing_Date"] = dr.Cells["Sailing_Date"].Value;
                                }
                                if (dr.Cells["Invoice_No"].Value != null)
                                { 
                                    drLocal["InvoiceNo"] = dr.Cells["Invoice_No"].Value;
                                }
                                if (dr.Cells["Invoice_Date"].Value != null)
                                { 
                                    drLocal["Invoice_Date"] = dr.Cells["Invoice_Date"].Value;
                                }
                                drLocal["Quantity_Order"] = dr.Cells["Quantity_Output"].Value;
                                if (dr.Cells["Quantity_Invoice"].Value != null)
                                { 
                                    drLocal["Quantity_Invoice"] = dr.Cells["Quantity_Invoice"].Value;
                                }
                                if (dr.Cells["Balance"].Value != null)
                                { 
                                    drLocal["Balance"] = dr.Cells["Balance"].Value;
                                }
                                _noIndex++;
                                _tempPODetail.Rows.Add(drLocal);
                            }
                        }


                        if (rowCount > 0)
                        {
                            _PODetailSheet.InsertRow(4, rowCount - 1);

                            for (int i = 1; i < rowCount; i++)
                            {
                                _PODetailSheet.Cells[3, 1, 3, 27].Copy(_PODetailSheet.Cells[3 + i, 1, 3 + i, 27]);
                            }
                            //Load data from datatable to excel
                            _PODetailSheet.Cells["A3"].LoadFromDataTable(_tempPODetail, false);
                        }
                        #endregion

                        //Focus A1, sheet Invoice
                        _PODetailSheet.Select("A1");

                        byte[] bin = _excelPackage.GetAsByteArray();

                        //Create a SaveFileDialog instance with some properties
                        SaveFileDialog _saveFileDialog = new SaveFileDialog();
                        _saveFileDialog.Title = "Save file Detail PO";
                        _saveFileDialog.Filter = "Excel files|*.xlxs|All files|*.*";
                        _saveFileDialog.FileName = "DetailPO_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";

                        //Check if user clicked the save button
                        if (_saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            //write the file to the disk
                            File.WriteAllBytes(_saveFileDialog.FileName, bin);
                            MessageBox.Show("    Export File Detail PO complete !!!    ", "Hoàn Thành", MessageBoxButtons.OK);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Can't read file. Original error: " + ex.Message);
                }
            }
        }

        private void txt_CustomerCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnSearch_Customer_Click(sender, e);
            }
        }

        private void txtReceiveNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnSearch_ReciveNo_Click(sender, e);
            }
        }

        private void txtCodeTVC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnSearch_CodeTVC_Click(sender, e);
            }
        }

        private void txtPOCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnSearch_POCustomer_Click(sender, e);
            }
        }

        private void txtInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnSearch_InvoiceNo_Click(sender, e);
            }
        }

        private void button_Cancel_PO_Click(object sender, EventArgs e)
        {

        }
    }
}
