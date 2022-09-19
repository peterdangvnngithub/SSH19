using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TAKAKO_ERP_3LAYER.DAL;
using TAKAKO_ERP_3LAYER.DAO;
using System.ComponentModel;
using static TAKAKO_ERP_3LAYER.Common;

namespace TAKAKO_ERP_3LAYER
{
    public partial class Form_Shipping_Instruction : Form
    {

        public SHIPPING_DAO _shippingDAO;

        public SEARCH_DAO _searchDAO;

        public SYSTEM_DAL _systemDAL;

        public LOG_DAO _logDAO;

        DataGridViewRow lastSelected;

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

        public enum Number
        {
             Zero = 0
            ,One = 1
            ,Two = 2
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
            AddColumnGridView(GridView_Invoice);
            AddColumnGridView(GridView_PackingList);

            //Setting enable item
            SettingInitGridView();

            //Setting init date Renueve
            dtpRevenue.Value = DateTime.Now;

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
        }

        #region Setting Init
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

        public void GetSelectedItem(List<ItemCodeInfo> _listValue)
        {
            decimal quantity = 0;
            decimal price = 0;
            if (_listValue != null)
            {
                foreach (ItemCodeInfo item in _listValue)
                {
                    //Binding for Invoice
                    int n = GridView_Invoice.Rows.Add();
                    GridView_Invoice.Rows[n].Cells["Customer_Code"].Value = item.CustomerCode;
                    GridView_Invoice.Rows[n].Cells["Cus_ItemCode"].Value = item.Customer_ItemCode;
                    GridView_Invoice.Rows[n].Cells["Tvc_ItemCode"].Value = item.TVC_ItemCode;
                    GridView_Invoice.Rows[n].Cells["Part_Description"].Value = item.Item_Name;
                    GridView_Invoice.Rows[n].Cells["Customer_PO"].Value = item.CustomerPO;
                    GridView_Invoice.Rows[n].Cells["ThirdParty_PO"].Value = item.ThirdPartyPO;
                    GridView_Invoice.Rows[n].Cells["Order_Date"].Value = item.Order_Date.ToString("dd/MM/yyyy");
                    GridView_Invoice.Rows[n].Cells["DueDate_PO"].Value = item.DueDate_PO.ToString("dd/MM/yyyy");
                    GridView_Invoice.Rows[n].Cells["Quantity"].Value = item.Balance;
                    GridView_Invoice.Rows[n].Cells["Quantity_Revise"].Value = item.Balance;
                    GridView_Invoice.Rows[n].Cells["Balance"].Value = item.Balance;
                    GridView_Invoice.Rows[n].Cells["Unit_Currency"].Value = item.OrderUnitCurrency;
                    GridView_Invoice.Rows[n].Cells["USD_Rate"].Value = 0;
                    GridView_Invoice.Rows[n].Cells["Order_Price"].Value = item.OrderPrice;
                    GridView_Invoice.Rows[n].Cells["Order_Price_Revise"].Value = item.OrderPrice;
                    GridView_Invoice.Rows[n].Cells["Global_Price"].Value = item.CustomerPrice;
                    GridView_Invoice.Rows[n].Cells["Amount_JPY"].Value = 0;
                    ////
                    //if (item.OrderUnitCurrency == "USD")
                    //{
                    //    GridView_Invoice.Rows[n].Cells["USD_Rate"].ReadOnly = false;
                    //}

                    if (!item.OrderPrice.Equals(null) && !item.CustomerPrice.Equals(null))
                    {
                        if (Convert.ToDecimal(item.OrderPrice) != Convert.ToDecimal(item.CustomerPrice))
                        {
                            GridView_Invoice.Rows[n].Cells["Order_Price"].Style.BackColor = Color.Red;
                            GridView_Invoice.Rows[n].Cells["Global_Price"].Style.BackColor = Color.Red;
                        }
                    }
                    else if (item.OrderPrice.Equals(null))
                    {
                        GridView_Invoice.Rows[n].Cells["Order_Price"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        GridView_Invoice.Rows[n].Cells[6].Style.BackColor = Color.Red;
                    }

                    if (GridView_Invoice.Rows[n].Cells["Quantity"].Value != null
                        || GridView_Invoice.Rows[n].Cells["Order_Price"].Value != null)
                    {
                        if (decimal.TryParse(GridView_Invoice.Rows[n].Cells["Quantity"].Value.ToString(), out quantity)
                         && decimal.TryParse(GridView_Invoice.Rows[n].Cells["Order_Price"].Value.ToString(), out price))
                        {
                            decimal amount = quantity * price;
                            if (item.OrderUnitCurrency.ToUpper() == "JPY")
                            {
                                GridView_Invoice.Rows[n].Cells["Amount_Jpy"].Value = Math.Round(amount, 0, MidpointRounding.AwayFromZero);
                            }
                            else if (item.OrderUnitCurrency.ToUpper() == "USD")
                            {
                                GridView_Invoice.Rows[n].Cells["Amount_Jpy"].Value = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
                            }
                        }
                    }

                    if (Convert.ToDecimal(GridView_Invoice.Rows[n].Cells["Quantity"].Value) == 0)
                    {
                        GridView_Invoice.Rows[n].Cells["Quantity"].Style.BackColor = Color.Tomato;
                    }

                    //Binding for Packing List
                    int m = GridView_PackingList.Rows.Add();
                    GridView_PackingList.Rows[m].Cells["Customer_Code"].Value = item.CustomerCode;
                    GridView_PackingList.Rows[m].Cells["Packages_No"].Value = "1";
                    if (!String.IsNullOrEmpty(item.ThirdPartyItemCode))
                    { 
                        GridView_PackingList.Rows[m].Cells["Customer_ItemCode"].Value = item.ThirdPartyItemCode + "\n" + "(" + item.Customer_ItemCode + ")";
                    } else
                    {
                        GridView_PackingList.Rows[m].Cells["Customer_ItemCode"].Value = item.Customer_ItemCode;
                    }
                    GridView_PackingList.Rows[m].Cells["TVC_ItemCode"].Value = item.TVC_ItemCode;
                    GridView_PackingList.Rows[m].Cells["Customer_PO"].Value = item.CustomerPO;
                    GridView_PackingList.Rows[m].Cells["Qty_Carton"].Value = 0;
                    GridView_PackingList.Rows[m].Cells["Qty_Per_Carton"].Value = 0;
                    GridView_PackingList.Rows[m].Cells["Qty_Total"].Value = item.Balance;
                    GridView_PackingList.Rows[m].Cells["Qty_Total_Revise"].Value = item.Balance;
                    GridView_PackingList.Rows[m].Cells["Net_Weight"].Value = item.Weight;
                    GridView_PackingList.Rows[m].Cells["Net_Weight_Total"].Value = item.Weight * item.Balance;
                    GridView_PackingList.Rows[m].Cells["Gross_Weight"].Value = 0;
                    GridView_PackingList.Rows[m].Cells["Lot_No"].Value = "NONE-LOT-NO.";
                }
            }
        }

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

            txtTotalQuantity.Text = "0";
            txtTotalAmount.Text = "0";
            txtTotal_QtyCarton.Text = "0";
            txtTotal_Qty.Text = "0";
            txtTotal_NetWeight.Text = "0";
            txtTotal_GrossWeight.Text = "0";

            //
            txtShippingNo.Enabled = true;

            //Clear Gridview Invoice
            GridView_Invoice.Rows.Clear();

            //Clear Gridview PL
            GridView_PackingList.Rows.Clear();

            //
            tabControl.SelectedIndex = 0;

            //
            cb_CompanyCode.SelectedIndex = 0;

            //
            dtpRevenue.Value = DateTime.Now;

            //
            btnUnlockData.Enabled = false;
            btnRevise.Enabled = false;

            //
            cb_Freight.SelectedValue = 99;

            //
            txtShippingNo.Focus();
        }

        private string Sum_Total(DataGridView _datagridviewSum, string _cellName)
        {
            string Sum = "";
            Sum = (from
                        DataGridViewRow row in _datagridviewSum.Rows
                   where
                        row.Cells[_cellName].FormattedValue.ToString() != String.Empty
                   select
                        Convert.ToDecimal(row.Cells[_cellName].FormattedValue)).Sum().ToString();
            return Sum;
        }

        private void btnSumInvoice_Click(object sender, EventArgs e)
        {
            if (radNormal.Checked == true || radLock.Checked == true)
            { 
                //Sum quantity
                txtTotalQuantity.Text = FormatCommas(Convert.ToDecimal(Sum_Total(GridView_Invoice, "Quantity")));
            } else if (radRevise.Checked == true)
            {
                //Sum quantity
                txtTotalQuantity.Text = FormatCommas(Convert.ToDecimal(Sum_Total(GridView_Invoice, "Quantity_Revise")));
            }

            //Sum amount
            txtTotalAmount.Text = FormatCommas(Convert.ToDecimal(Sum_Total(GridView_Invoice, "Amount_Jpy")));
        }

        public string FormatCommas(decimal tempValue)
        {
            return string.Format("{0:#,##0.##}", tempValue);
        }

        private void btnSumPL_Click(object sender, EventArgs e)
        {
            //Sum total carton
            txtTotal_QtyCarton.Text = FormatCommas(Convert.ToDecimal(Sum_Total(GridView_PackingList, "Qty_Carton")));

            if (radNormal.Checked == true || radLock.Checked == true)
            {
                //Sum total quantity
                txtTotal_Qty.Text = FormatCommas(Convert.ToDecimal(Sum_Total(GridView_PackingList, "Qty_Total")));
            } else if(radRevise.Checked == true || radLock.Checked == true)
            {
                //Sum total quantity revise
                txtTotal_Qty.Text = FormatCommas(Convert.ToDecimal(Sum_Total(GridView_PackingList, "Qty_Total_Revise")));
            }

            //Sum total Net Weight
            txtTotal_NetWeight.Text = FormatCommas(Convert.ToDecimal(Sum_Total(GridView_PackingList, "Net_Weight_Total")));

            //Sum total Grosss Weight
            txtTotal_GrossWeight.Text = FormatCommas(Convert.ToDecimal(Sum_Total(GridView_PackingList, "Gross_Weight")));
        }

        #region Search
        private void btnSearch_ShippingNo_Click(object sender, EventArgs e)
        {
            Form_Search _formSearchShippingNo = new Form_Search("btnSearch_ShippingNo",this.Name);
            _formSearchShippingNo.StartPosition = FormStartPosition.CenterParent;
            _formSearchShippingNo.ShowDialog();

            if (!String.IsNullOrEmpty(_formSearchShippingNo._shippingInfo.ShippingNo))
            {
                ClearData();
                string Lock_Status = "";

                txtShippingNo.Text = _formSearchShippingNo._shippingInfo.ShippingNo;
                Lock_Status = _formSearchShippingNo._shippingInfo.Lock_Status;

                if (Lock_Status.ToLower() == "normal")
                {
                    radNormal.Checked = true;
                }
                else if (Lock_Status.ToLower() == "lock")
                {
                    radLock.Checked = true;
                }
                else if (Lock_Status.ToLower() == "revise")
                {
                    radRevise.Checked = true;
                }

                //Load Invoice
                btn_SearchShipping_Click(sender, e);

                //Setting enable item
                SettingInitGridView();
            }
            else
            {
                txtShippingNo.Focus();
            }
        }

        private void btnSearch_IssuedTo_Click(object sender, EventArgs e)
        {
            Form_Search _formSearch = new Form_Search("btnSearch_IssuedTo", this.Name);
            _formSearch.StartPosition = FormStartPosition.CenterParent;
            _formSearch.ShowDialog();

            if (!String.IsNullOrEmpty(_formSearch._companyInfo.CompanyCode))
            { 
                txtIssuedTo_CompanyCode.Text = _formSearch._companyInfo.CompanyCode;
                txtIssuedTo_CompanyName.Text = _formSearch._companyInfo.CompanyName;
                txtIssuedTo_CompanyAddress.Text = _formSearch._companyInfo.CompanyAddress;
                txtIssuedTo_TelNo.Text = _formSearch._companyInfo.CompanyTelNo;
                txtIssuedTo_FaxNo.Text = _formSearch._companyInfo.CompanyFaxNo;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void btnSearch_ShipTo_Click(object sender, EventArgs e)
        {
            Form_Search _formSearch = new Form_Search("btnSearch_ShipTo", this.Name);
            _formSearch.StartPosition = FormStartPosition.CenterParent;
            _formSearch.ShowDialog();

            if (!String.IsNullOrEmpty(_formSearch._companyInfo.CompanyCode))
            {
                txtShipTo_CompanyCode.Text = _formSearch._companyInfo.CompanyCode;
                txtShipTo_CompanyName.Text = _formSearch._companyInfo.CompanyName;
                txtShipTo_CompanyAddress.Text = _formSearch._companyInfo.CompanyAddress;
                txtShipTo_TelNo.Text = _formSearch._companyInfo.CompanyTelNo;
                txtShipTo_FaxNo.Text = _formSearch._companyInfo.CompanyFaxNo;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
            else
            {
                txtShipTo_CompanyCode.Focus();
            }
        }

        private void btnSearch_PortLoading_Click(object sender, EventArgs e)
        {
            Form_Search _formSearch = new Form_Search("btnSearch_PortLoading", this.Name);
            _formSearch.StartPosition = FormStartPosition.CenterParent;
            _formSearch.ShowDialog();

            if (!String.IsNullOrEmpty(_formSearch._destinationInfo.DestinationID))
            {
                txtPortLoading.Text = _formSearch._destinationInfo.DestinationID;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
            else
            {
                txtPortLoading.Focus();
            }
        }

        private void btnSearch_PortDestination_Click(object sender, EventArgs e)
        {
            Form_Search _formSearch = new Form_Search("btnSearch_PortDestination", this.Name);
            _formSearch.StartPosition = FormStartPosition.CenterParent;
            _formSearch.ShowDialog();

            if (!String.IsNullOrEmpty(_formSearch._destinationInfo.DestinationID))
            {
                txtPortDestination.Text = _formSearch._destinationInfo.DestinationID;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
            else
            {
                txtPortDestination.Focus();
            }
        }

        private void btnSearch_PriceCondition_Click(object sender, EventArgs e)
        {
            Form_Search _formSearch = new Form_Search("btnSearch_TradeCondition", this.Name);
            _formSearch.StartPosition = FormStartPosition.CenterParent;
            _formSearch.ShowDialog();
            if (!String.IsNullOrEmpty(_formSearch._priceCondition.PriceCondition))
            {
                txtPriceCondition.Text = _formSearch._priceCondition.PriceCondition;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
            else
            {
                txtPriceCondition.Focus();
            }
        }

        private void btnSearch_PaymentTerm_Click(object sender, EventArgs e)
        {
            Form_Search _formSearch = new Form_Search("btnSearch_PaymentTerm", this.Name);
            _formSearch.StartPosition = FormStartPosition.CenterParent;
            _formSearch.ShowDialog();
            if (!String.IsNullOrEmpty(_formSearch._paymentTerm.PaymentID))
            {
                txtPaymentTerm.Text = _formSearch._paymentTerm.PaymentID;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
            else
            {
                txtPaymentTerm.Focus();
            }
        }
        #endregion

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
        public void AddColumnGridView(DataGridView _dataGridView)
        {
            if (_dataGridView.Name == "GridView_Invoice")
            {
                System.Windows.Forms.DataGridViewTextBoxColumn InvCustomerCode_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn InvItem_Name_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn InvCus_ItemCode_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn InvTVC_ItemCode_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn InvCustomerPO_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn InvThirdPartyItemCode_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn InvThirdPartyPO_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn OrderDate_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn DueDatePO_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn InvQuantity_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn InvQuantityRevise_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn InvBalance_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn InvOrder_Price_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn InvOrder_Price_Revise_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn InvGlobal_Price_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn InvAmount_Price_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn InvEx_Rate_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn InvUnitCurrency_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewButtonColumn InvButtonSearch_col = new System.Windows.Forms.DataGridViewButtonColumn();

                this.GridView_Invoice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
                    {
                         InvCustomerCode_col
                        ,InvItem_Name_col
                        ,InvCus_ItemCode_col
                        ,InvTVC_ItemCode_col
                        ,InvButtonSearch_col
                        ,InvCustomerPO_col
                        ,InvThirdPartyItemCode_col
                        ,InvThirdPartyPO_col
                        ,OrderDate_col
                        ,DueDatePO_col
                        ,InvQuantity_col
                        ,InvQuantityRevise_col
                        ,InvBalance_col
                        ,InvUnitCurrency_col
                        ,InvEx_Rate_col
                        ,InvOrder_Price_col
                        ,InvOrder_Price_Revise_col
                        ,InvGlobal_Price_col
                        ,InvAmount_Price_col
                    });
                //CUSTOMER CODE
                InvCustomerCode_col.HeaderText = "CUSTOMER CODE";
                InvCustomerCode_col.DataPropertyName = "CUSTOMER_CODE";
                InvCustomerCode_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InvCustomerCode_col.Name = "Customer_Code";
                InvCustomerCode_col.Width = 90;

                //PART DESCRIPTION
                InvItem_Name_col.HeaderText = "PART DESCRIPTION";
                InvItem_Name_col.DataPropertyName = "ITEM_NAME";
                InvItem_Name_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InvItem_Name_col.Name = "Part_Description";
                InvItem_Name_col.Width = 133;

                //CUSTOMER ITEM CODE
                InvCus_ItemCode_col.HeaderText = "CUSTOMER ITEM CODE";
                InvCus_ItemCode_col.DataPropertyName = "INV_CUS_ITEM_CODE";
                InvCus_ItemCode_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InvCus_ItemCode_col.Name = "Cus_ItemCode";
                InvCus_ItemCode_col.Width = 110;

                //TVC ITEM CODE
                InvTVC_ItemCode_col.HeaderText = "TVC ITEM CODE";
                InvTVC_ItemCode_col.DataPropertyName = "TVC_ITEM_CODE";
                InvTVC_ItemCode_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InvTVC_ItemCode_col.Name = "Tvc_ItemCode";
                InvTVC_ItemCode_col.Width = 130;

                //BUTTON SEARCH ITEM CODE
                InvButtonSearch_col.HeaderText = "";
                InvButtonSearch_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InvButtonSearch_col.Name = "BtnSearch_ItemCode";
                InvButtonSearch_col.Width = 30;

                //CUSTOMER PO
                InvCustomerPO_col.HeaderText = "CUSTOMER PO";
                InvCustomerPO_col.DataPropertyName = "CUSTOMER_PO";
                InvCustomerPO_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InvCustomerPO_col.Name = "Customer_PO";
                InvCustomerPO_col.Width = 120;

                //THIRD PARTY ITEM CODE
                InvThirdPartyItemCode_col.HeaderText = "THIRD PARTY CODE";
                InvThirdPartyItemCode_col.DataPropertyName = "THIRD_PARTY_ITEM_CODE";
                InvThirdPartyItemCode_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InvThirdPartyItemCode_col.Name = "ThirdParty_ItemCode";
                InvThirdPartyItemCode_col.Width = 100;

                //THIRD PARTY PO
                InvThirdPartyPO_col.HeaderText = "THIRD PARTY PO";
                InvThirdPartyPO_col.DataPropertyName = "THIRD_PARTY_PO";
                InvThirdPartyPO_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InvThirdPartyPO_col.Name = "ThirdParty_PO";
                InvThirdPartyPO_col.Width = 100;

                //ORDER DATE
                OrderDate_col.HeaderText = "ORDER DATE PO";
                OrderDate_col.DataPropertyName = "ORDER_DATE";
                OrderDate_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                OrderDate_col.Name = "Order_Date";
                OrderDate_col.Width = 120;

                //DUE DATE PO
                DueDatePO_col.HeaderText = "DUE DATE PO";
                DueDatePO_col.DataPropertyName = "DUE_DATE_PO";
                DueDatePO_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DueDatePO_col.Name = "DueDate_PO";
                DueDatePO_col.Width = 120;

                //QUANTITY
                InvQuantity_col.HeaderText = "QUANTITY";
                InvQuantity_col.DataPropertyName = "QUANTITY";
                InvQuantity_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InvQuantity_col.Name = "Quantity";
                InvQuantity_col.Width = 90;

                //QUANTITY REVISE
                InvQuantityRevise_col.HeaderText = "QUANTITY REVISE";
                InvQuantityRevise_col.DataPropertyName = "QUANTITY_REVISE";
                InvQuantityRevise_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InvQuantityRevise_col.Name = "Quantity_Revise";
                InvQuantityRevise_col.Width = 90;

                //Balance
                InvBalance_col.HeaderText = "BALANCE";
                InvBalance_col.DataPropertyName = "INV_BALANCE";
                InvBalance_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InvBalance_col.Name = "Balance";
                InvBalance_col.Width = 90;

                //UNIT CURRENCY
                InvUnitCurrency_col.HeaderText = "UNIT CURRENCY";
                InvUnitCurrency_col.DataPropertyName = "UNIT_CURRENCY";
                InvUnitCurrency_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InvUnitCurrency_col.Name = "Unit_Currency";
                InvUnitCurrency_col.Width = 80;

                //EX RATE
                InvEx_Rate_col.HeaderText = "Ex. RATE";
                InvEx_Rate_col.DataPropertyName = "USD_RATE";
                InvEx_Rate_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InvEx_Rate_col.Name = "USD_Rate";
                InvEx_Rate_col.Width = 90;

                //ORDER PRICE
                InvOrder_Price_col.HeaderText = "ORDER PRICE";
                InvOrder_Price_col.DataPropertyName = "ORDER_PRICE";
                InvOrder_Price_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InvOrder_Price_col.Name = "Order_Price";
                InvOrder_Price_col.Width = 115;

                //ORDER PRICE REVISE
                InvOrder_Price_Revise_col.HeaderText = "ORDER PRICE REVISE";
                InvOrder_Price_Revise_col.DataPropertyName = "ORDER_PRICE_REVISE";
                InvOrder_Price_Revise_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InvOrder_Price_Revise_col.Name = "Order_Price_Revise";
                InvOrder_Price_Revise_col.Width = 130;

                //UNIT PRICE COMPARE
                InvGlobal_Price_col.HeaderText = "GLOBAL PRICE";
                InvGlobal_Price_col.DataPropertyName = "PRICE";
                InvGlobal_Price_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InvGlobal_Price_col.Name = "Global_Price";
                InvGlobal_Price_col.Width = 115;

                //AMOUNT (JPY)
                InvAmount_Price_col.HeaderText = "AMOUNT";
                InvAmount_Price_col.DataPropertyName = "AMOUNT_JPY";
                InvAmount_Price_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InvAmount_Price_col.Name = "Amount_Jpy";
                InvAmount_Price_col.Width = 120;

                //
                this.GridView_Invoice.DefaultCellStyle.Font = new Font("Arial", 10.25F, GraphicsUnit.Pixel);

                //CUSTOMER CODE
                this.GridView_Invoice.Columns["Customer_Code"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //DUE DATE PO
                this.GridView_Invoice.Columns["Order_Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //DUE DATE PO
                this.GridView_Invoice.Columns["DueDate_PO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //this.GridView_Invoice.Columns["DueDate_PO"].Visible = false;

                //THIRD PARTY CODE
                this.GridView_Invoice.Columns["ThirdParty_ItemCode"].Visible = false;

                //ORDER DATE
                this.GridView_Invoice.Columns["Order_Date"].Visible = true;

                //DUE DATE PO
                //this.GridView_Invoice.Columns["DueDate_PO"].Visible = false;
                this.GridView_Invoice.Columns["DueDate_PO"].Visible = true;

                ////BALANCE
                //this.GridView_Invoice.Columns["Balance"].Visible = false;
                this.GridView_Invoice.Columns["Balance"].DefaultCellStyle.Format = "#,##0.##";
                this.GridView_Invoice.Columns["Balance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                //QUANTITY
                this.GridView_Invoice.Columns["Quantity"].DefaultCellStyle.Format = "#,##0.##";
                this.GridView_Invoice.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.GridView_Invoice.Columns["Quantity"].CellTemplate.ValueType = typeof(Int32);
                
                //QUANTITY REVISE
                this.GridView_Invoice.Columns["Quantity_Revise"].DefaultCellStyle.Format = "#,##0.##";
                this.GridView_Invoice.Columns["Quantity_Revise"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.GridView_Invoice.Columns["Quantity_Revise"].CellTemplate.ValueType = typeof(Int32);

                //ORDER PRICE
                this.GridView_Invoice.Columns["Order_Price"].DefaultCellStyle.Format = "#,##0.####";
                this.GridView_Invoice.Columns["Order_Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //ORDER PRICE REVISE
                this.GridView_Invoice.Columns["Order_Price_Revise"].DefaultCellStyle.Format = "#,##0.####";
                this.GridView_Invoice.Columns["Order_Price_Revise"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //UNIT CURRENCY
                this.GridView_Invoice.Columns["Unit_Currency"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //USD RATE
                //this.GridView_Invoice.Columns["USD_Rate"].ReadOnly = true;
                this.GridView_Invoice.Columns["USD_Rate"].DefaultCellStyle.Format = "#,##0.##";
                this.GridView_Invoice.Columns["USD_Rate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //UNIT PRICE COMPARE
                this.GridView_Invoice.Columns["Global_Price"].DefaultCellStyle.Format = "#,##0.####";
                this.GridView_Invoice.Columns["Global_Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //AMOUNT JPY
                this.GridView_Invoice.Columns["Amount_Jpy"].DefaultCellStyle.Format = "#,##0.00##";
                this.GridView_Invoice.Columns["Amount_Jpy"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else
            {
                System.Windows.Forms.DataGridViewTextBoxColumn PlCustomer_Code_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn PlPackagesNo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn PlCusItemCode_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn PlTVCItemCode_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn PlCustomerPO_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn PlQtyCarton_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn PlQtyPerCarton_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn PlQtyTotal_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn PlQtyTotal_Revise_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn PlNetWeight_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn PlNetWeightTotal_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn PlGrossWeight_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn PlLotNo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();

                this.GridView_PackingList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
                    {
                         PlCustomer_Code_col
                        ,PlPackagesNo_col
                        ,PlCusItemCode_col
                        ,PlTVCItemCode_col
                        ,PlCustomerPO_col
                        ,PlQtyCarton_col
                        ,PlQtyPerCarton_col
                        ,PlQtyTotal_col
                        ,PlQtyTotal_Revise_col
                        ,PlNetWeight_col
                        ,PlNetWeightTotal_col
                        ,PlGrossWeight_col
                        ,PlLotNo_col
                    });

                //CUSTOMER CODE
                PlCustomer_Code_col.HeaderText = "CUSTOMER CODE";
                PlCustomer_Code_col.DataPropertyName = "CUSTOMER_CODE";
                PlCustomer_Code_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PlCustomer_Code_col.Name = "Customer_Code";
                PlCustomer_Code_col.Width = 95;

                //PACKAGES NO
                PlPackagesNo_col.HeaderText = "PACKAGES NO";
                PlPackagesNo_col.DataPropertyName = "PACKAGES_NO";
                PlPackagesNo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PlPackagesNo_col.Name = "Packages_No";
                PlPackagesNo_col.Width = 95;

                //CUSTOMER ITEM CODE
                PlCusItemCode_col.HeaderText = "CUSTOMER ITEM CODE";
                PlCusItemCode_col.DataPropertyName = "CUSTOMER_ITEM_CODE";
                PlCusItemCode_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PlCusItemCode_col.Name = "Customer_ItemCode";
                PlCusItemCode_col.Width = 150;

                //TVC ITEM CODE
                PlTVCItemCode_col.HeaderText = "TVC ITEM CODE";
                PlTVCItemCode_col.DataPropertyName = "TVC_ITEM_CODE";
                PlTVCItemCode_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PlTVCItemCode_col.Name = "TVC_ItemCode";
                PlTVCItemCode_col.Width = 120;

                //CUSTOMER PO
                PlCustomerPO_col.HeaderText = "CUSTOMER PO";
                PlCustomerPO_col.DataPropertyName = "PL_CUSTOMER_PO";
                PlCustomerPO_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PlCustomerPO_col.Name = "Customer_PO";
                PlCustomerPO_col.Width = 120;

                //QTY OF CARTON
                PlQtyCarton_col.HeaderText = "QUANTITY OF CARTON";
                PlQtyCarton_col.DataPropertyName = "QTY_CARTON";
                PlQtyCarton_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PlQtyCarton_col.Name = "Qty_Carton";
                PlQtyCarton_col.Width = 100;

                //QTY_PER_CARTON
                PlQtyPerCarton_col.HeaderText = "QUANTITY / CARTON (PCS)";
                PlQtyPerCarton_col.DataPropertyName = "BOX_QUANTITY";
                PlQtyPerCarton_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PlQtyPerCarton_col.Name = "Qty_Per_Carton";
                PlQtyPerCarton_col.Width = 120;

                //QTY TOTAL
                PlQtyTotal_col.HeaderText = "QUANTITY TOTAL(PCS)";
                PlQtyTotal_col.DataPropertyName = "QTY_TOTAL";
                PlQtyTotal_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PlQtyTotal_col.Name = "Qty_Total";
                PlQtyTotal_col.Width = 120;

                //QTY TOTAL REVISE
                PlQtyTotal_Revise_col.HeaderText = "QTY TOTAL REVISE(PCS)";
                PlQtyTotal_Revise_col.DataPropertyName = "QTY_TOTAL_REVISE";
                PlQtyTotal_Revise_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PlQtyTotal_Revise_col.Name = "Qty_Total_Revise";
                PlQtyTotal_Revise_col.Width = 120;

                //NET WEIGHT
                PlNetWeight_col.HeaderText = "N / W (KG)";
                PlNetWeight_col.DataPropertyName = "WEIGHT";
                PlNetWeight_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PlNetWeight_col.Name = "Net_Weight";
                PlNetWeight_col.Width = 90;

                //NET WEIGHT TOTAL
                PlNetWeightTotal_col.HeaderText = "N / W TOTAL";
                PlNetWeightTotal_col.DataPropertyName = "WEIGHT_TOTAL";
                PlNetWeightTotal_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PlNetWeightTotal_col.Name = "Net_Weight_Total";
                PlNetWeightTotal_col.Width = 110;

                //GROSS WEIGHT
                PlGrossWeight_col.HeaderText = "G / W (KG)";
                PlGrossWeight_col.DataPropertyName = "GROSS_WEIGHT";
                PlGrossWeight_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PlGrossWeight_col.Name = "Gross_Weight";
                PlGrossWeight_col.Width = 100;

                //LOT NO
                PlLotNo_col.HeaderText = "LOT NO";
                PlLotNo_col.DataPropertyName = "LOT_NO";
                PlLotNo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PlLotNo_col.Name = "Lot_No";
                PlLotNo_col.Width = 134;

                //Setting
                this.GridView_PackingList.DefaultCellStyle.Font = new Font("Arial", 10.25F, GraphicsUnit.Pixel);
                this.GridView_PackingList.Columns["Net_Weight"].Visible = false;
                //this.GridView_PackingList.Columns["Customer_PO"].Visible = false;

                //CUSTOMER CODE
                this.GridView_PackingList.Columns["Customer_Code"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //
                this.GridView_PackingList.Columns["Packages_No"].DefaultCellStyle.Format = "#,##0.##";
                this.GridView_PackingList.Columns["Packages_No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //
                this.GridView_PackingList.Columns["Qty_Carton"].DefaultCellStyle.Format = "#,##0.##";
                this.GridView_PackingList.Columns["Qty_Carton"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //
                this.GridView_PackingList.Columns["Qty_Per_Carton"].DefaultCellStyle.Format = "#,##0.##";
                this.GridView_PackingList.Columns["Qty_Per_Carton"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //
                this.GridView_PackingList.Columns["Qty_Total"].DefaultCellStyle.Format = "#,##0.##";
                this.GridView_PackingList.Columns["Qty_Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //QTY TOTAL REVISE
                this.GridView_PackingList.Columns["Qty_Total_Revise"].DefaultCellStyle.Format = "#,##0.##";
                this.GridView_PackingList.Columns["Qty_Total_Revise"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //
                this.GridView_PackingList.Columns["Net_Weight"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.GridView_PackingList.Columns["Net_Weight"].DefaultCellStyle.Format = "#,##0.###";
                // 
                this.GridView_PackingList.Columns["Net_Weight_Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.GridView_PackingList.Columns["Net_Weight_Total"].DefaultCellStyle.Format = "#,##0.##";
                //
                this.GridView_PackingList.Columns["Gross_Weight"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.GridView_PackingList.Columns["Gross_Weight"].DefaultCellStyle.Format = "#,##0.##";
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
                cb_CompanyCode.Enabled = true;
                dtpETA.Enabled = true;
                dtpETD.Enabled = true;
                dtpDateCreateShipping.Enabled = true;
                txtInvoiceNo.ReadOnly = false;
                txtIssuedTo_CompanyCode.ReadOnly = false;
                txtIssuedTo_CompanyName.ReadOnly = false;
                txtIssuedTo_CompanyAddress.ReadOnly = false;
                txtIssuedTo_TelNo.ReadOnly = false;
                txtIssuedTo_FaxNo.ReadOnly = false;

                txtShipTo_CompanyCode.ReadOnly = false;
                txtShipTo_CompanyName.ReadOnly = false;
                txtShipTo_CompanyAddress.ReadOnly = false;
                txtShipTo_TelNo.ReadOnly = false;
                txtShipTo_FaxNo.ReadOnly = false;

                txtShipVia.ReadOnly = false;
                dtpRevenue.Enabled = true;
                cb_Freight.Enabled = true;
                txtVessel.ReadOnly = false;
                txtPortLoading.ReadOnly = false;
                txtPortDestination.ReadOnly = false;

                txtPriceCondition.ReadOnly = false;
                txtPaymentTerm.ReadOnly = false;

                //Button Lock, Unlock & Revise Data
                btnLockData.Enabled = true;
                btnUnlockData.Enabled = false;
                btnRevise.Enabled = false;

                //
                btnSave_Data.Enabled = true;

                //Button search
                btnSearch_ShippingNo.Enabled = true;
                btnSearch_IssuedTo.Enabled = true;
                btnSearch_ShipTo.Enabled = true;
                btnSearch_PortLoading.Enabled = true;
                btnSearch_PortDestination.Enabled = true;
                btnSearch_TradeCondition.Enabled = true;
                btnSearch_PaymentTerm.Enabled = true;

                GridView_Invoice.AllowUserToDeleteRows = true;
                GridView_PackingList.AllowUserToDeleteRows = true;

                //---------------- Gridview Invoice ----------------//
                //------ Disable------//
                GridView_Invoice.Columns["Quantity_Revise"].Visible = false;
                GridView_Invoice.Columns["Quantity_Revise"].ReadOnly = true;
                GridView_Invoice.Columns["Quantity_Revise"].DefaultCellStyle.BackColor = Color.Gray;
                GridView_Invoice.Columns["Order_Price_Revise"].Visible = false;
                GridView_Invoice.Columns["Order_Price_Revise"].ReadOnly = true;
                GridView_Invoice.Columns["Order_Price_Revise"].DefaultCellStyle.BackColor = Color.Gray;

                //------------ Gridview PackingLisst ---------------//
                //------ Disable------//
                GridView_PackingList.Columns["Qty_Total_Revise"].Visible = false;
                GridView_PackingList.Columns["Qty_Total_Revise"].ReadOnly = true;
                GridView_PackingList.Columns["Qty_Total_Revise"].DefaultCellStyle.BackColor = Color.Gray;

                foreach (DataGridViewRow dtRow in GridView_Invoice.Rows)
                {
                    if (!dtRow.IsNewRow)
                    { 
                        //---- Enable ----//
                        dtRow.Cells["Customer_Code"].ReadOnly = true;
                        dtRow.Cells["Customer_Code"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Part_Description"].ReadOnly = false;
                        dtRow.Cells["Part_Description"].Style.BackColor = Color.White;

                        dtRow.Cells["Cus_ItemCode"].ReadOnly = true;
                        dtRow.Cells["Cus_ItemCode"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Tvc_ItemCode"].ReadOnly = true;
                        dtRow.Cells["Tvc_ItemCode"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Customer_PO"].ReadOnly = false;
                        dtRow.Cells["Customer_PO"].Style.BackColor = Color.White;

                        dtRow.Cells["ThirdParty_PO"].ReadOnly = true;
                        dtRow.Cells["ThirdParty_PO"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Order_Date"].ReadOnly = true;
                        dtRow.Cells["Order_Date"].Style.BackColor = Color.Gray;

                        dtRow.Cells["DueDate_PO"].ReadOnly = true;
                        dtRow.Cells["DueDate_PO"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Quantity"].ReadOnly = false;
                        dtRow.Cells["Quantity"].Style.BackColor = Color.White;

                        dtRow.Cells["Balance"].ReadOnly = true;
                        dtRow.Cells["Balance"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Unit_Currency"].ReadOnly = true;
                        dtRow.Cells["Unit_Currency"].Style.BackColor = Color.Gray;

                        dtRow.Cells["USD_Rate"].ReadOnly = false;
                        dtRow.Cells["USD_Rate"].Style.BackColor = Color.White;

                        dtRow.Cells["Order_Price"].ReadOnly = false;
                        dtRow.Cells["Order_Price"].Style.BackColor = Color.White;

                        dtRow.Cells["Global_Price"].ReadOnly = true;
                        dtRow.Cells["Global_Price"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Amount_Jpy"].ReadOnly = true;
                        dtRow.Cells["Amount_Jpy"].Style.BackColor = Color.Gray;
                    }
                }

                //---------------- Gridview PackingList ----------------//
                //---- Enable ----//
                foreach (DataGridViewRow dtRow in GridView_PackingList.Rows)
                {
                    if (!dtRow.IsNewRow)
                    { 
                        dtRow.Cells["Customer_Code"].ReadOnly = true;
                        dtRow.Cells["Customer_Code"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Packages_No"].ReadOnly = false;
                        dtRow.Cells["Packages_No"].Style.BackColor = Color.White;

                        dtRow.Cells["Customer_ItemCode"].ReadOnly = true;
                        dtRow.Cells["Customer_ItemCode"].Style.BackColor = Color.Gray;

                        dtRow.Cells["TVC_ItemCode"].ReadOnly = true;
                        dtRow.Cells["TVC_ItemCode"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Customer_PO"].ReadOnly = false;
                        dtRow.Cells["Customer_PO"].Style.BackColor = Color.White;

                        dtRow.Cells["Qty_Carton"].ReadOnly = false;
                        dtRow.Cells["Qty_Carton"].Style.BackColor = Color.White;

                        dtRow.Cells["Qty_Per_Carton"].ReadOnly = false;
                        dtRow.Cells["Qty_Per_Carton"].Style.BackColor = Color.White;

                        dtRow.Cells["Qty_Total"].ReadOnly = true;
                        dtRow.Cells["Qty_Total"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Net_Weight"].ReadOnly = false;
                        dtRow.Cells["Net_Weight"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Net_Weight_Total"].ReadOnly = false;
                        dtRow.Cells["Net_Weight_Total"].Style.BackColor = Color.White;

                        dtRow.Cells["Gross_Weight"].ReadOnly = false;
                        dtRow.Cells["Gross_Weight"].Style.BackColor = Color.White;

                        dtRow.Cells["Lot_No"].ReadOnly = false;
                        dtRow.Cells["Lot_No"].Style.BackColor = Color.White;
                    }
                }
            }
            else if (radLock.Checked == true)
            {
                //--------------------- Header ---------------------//
                //---- Disable ----//
                cb_CompanyCode.Enabled = false;
                dtpETA.Enabled = false;
                dtpETD.Enabled = false;
                dtpDateCreateShipping.Enabled = false;
                txtInvoiceNo.ReadOnly = true;
                txtIssuedTo_CompanyCode.ReadOnly = true;
                txtIssuedTo_CompanyName.ReadOnly = true;
                txtIssuedTo_CompanyAddress.ReadOnly = true;
                txtIssuedTo_TelNo.ReadOnly = true;
                txtIssuedTo_FaxNo.ReadOnly = true;

                txtShipTo_CompanyCode.ReadOnly = true;
                txtShipTo_CompanyName.ReadOnly = true;
                txtShipTo_CompanyAddress.ReadOnly = true;
                txtShipTo_TelNo.ReadOnly = true;
                txtShipTo_FaxNo.ReadOnly = true;

                txtShipVia.ReadOnly = true;
                dtpRevenue.Enabled = false;
                cb_Freight.Enabled = false;
                txtVessel.ReadOnly = true;
                txtPortLoading.ReadOnly = true;
                txtPortDestination.ReadOnly = true;

                txtPriceCondition.ReadOnly = true;
                txtPaymentTerm.ReadOnly = true;

                btnLockData.Enabled = false;
                if(String.Equals(createBy,_systemDAL.UserName) && String.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                {
                    btnUnlockData.Enabled = true;
                }
                else
                {
                    btnUnlockData.Enabled = false;
                }
                if (String.Equals(createBy, _systemDAL.UserName) && !String.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                {
                    btnRevise.Enabled = true;
                }
                else
                {
                    btnRevise.Enabled = false;
                }
                btnSave_Data.Enabled = false;
                btnSearch_ShippingNo.Enabled = true;
                btnSearch_IssuedTo.Enabled = false;
                btnSearch_ShipTo.Enabled = false;
                btnSearch_PortLoading.Enabled = false;
                btnSearch_PortDestination.Enabled = false;
                btnSearch_TradeCondition.Enabled = false;
                btnSearch_PaymentTerm.Enabled = false;

                GridView_Invoice.AllowUserToDeleteRows = false;
                GridView_PackingList.AllowUserToDeleteRows = false;

                //---------------- Gridview Invoice ----------------//
                //---- Disable ----//
                GridView_Invoice.Columns["Quantity_Revise"].Visible = false;
                GridView_Invoice.Columns["Quantity_Revise"].ReadOnly = true;
                GridView_Invoice.Columns["Quantity_Revise"].DefaultCellStyle.BackColor = Color.Gray;
                //
                GridView_Invoice.Columns["Order_Price_Revise"].Visible = false;
                GridView_Invoice.Columns["Order_Price_Revise"].ReadOnly = true;
                GridView_Invoice.Columns["Order_Price_Revise"].DefaultCellStyle.BackColor = Color.Gray;

                //---- Disable ----//
                foreach (DataGridViewRow dtRow in GridView_Invoice.Rows)
                {
                    if (!dtRow.IsNewRow)
                    {
                        dtRow.Cells["Customer_Code"].ReadOnly = true;
                        dtRow.Cells["Customer_Code"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Part_Description"].ReadOnly = true;
                        dtRow.Cells["Part_Description"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Cus_ItemCode"].ReadOnly = true;
                        dtRow.Cells["Cus_ItemCode"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Tvc_ItemCode"].ReadOnly = true;
                        dtRow.Cells["Tvc_ItemCode"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Customer_PO"].ReadOnly = true;
                        dtRow.Cells["Customer_PO"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Order_Date"].ReadOnly = true;
                        dtRow.Cells["Order_Date"].Style.BackColor = Color.Gray;

                        dtRow.Cells["ThirdParty_PO"].ReadOnly = true;
                        dtRow.Cells["ThirdParty_PO"].Style.BackColor = Color.Gray;

                        dtRow.Cells["DueDate_PO"].ReadOnly = true;
                        dtRow.Cells["DueDate_PO"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Quantity"].ReadOnly = true;
                        dtRow.Cells["Quantity"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Balance"].ReadOnly = true;
                        dtRow.Cells["Balance"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Unit_Currency"].ReadOnly = true;
                        dtRow.Cells["Unit_Currency"].Style.BackColor = Color.Gray;

                        dtRow.Cells["USD_Rate"].ReadOnly = true;
                        dtRow.Cells["USD_Rate"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Order_Price"].ReadOnly = true;
                        dtRow.Cells["Order_Price"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Global_Price"].ReadOnly = true;
                        dtRow.Cells["Global_Price"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Amount_Jpy"].ReadOnly = true;
                        dtRow.Cells["Amount_Jpy"].Style.BackColor = Color.Gray;
                    }
                }

                //---------------- Gridview PackingList ----------------//
                //---- Disable ----//
                foreach (DataGridViewRow dtRow in GridView_PackingList.Rows)
                {
                    if (!dtRow.IsNewRow)
                    { 
                        dtRow.Cells["Customer_Code"].ReadOnly = true;
                        dtRow.Cells["Customer_Code"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Packages_No"].ReadOnly = true;
                        dtRow.Cells["Packages_No"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Customer_ItemCode"].ReadOnly = true;
                        dtRow.Cells["Customer_ItemCode"].Style.BackColor = Color.Gray;

                        dtRow.Cells["TVC_ItemCode"].ReadOnly = true;
                        dtRow.Cells["TVC_ItemCode"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Customer_PO"].ReadOnly = true;
                        dtRow.Cells["Customer_PO"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Qty_Carton"].ReadOnly = true;
                        dtRow.Cells["Qty_Carton"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Qty_Per_Carton"].ReadOnly = true;
                        dtRow.Cells["Qty_Per_Carton"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Qty_Total"].ReadOnly = true;
                        dtRow.Cells["Qty_Total"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Net_Weight"].ReadOnly = true;
                        dtRow.Cells["Net_Weight"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Net_Weight_Total"].ReadOnly = true;
                        dtRow.Cells["Net_Weight_Total"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Gross_Weight"].ReadOnly = true;
                        dtRow.Cells["Gross_Weight"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Lot_No"].ReadOnly = true;
                        dtRow.Cells["Lot_No"].Style.BackColor = Color.Gray;
                    }
                }
            }
            else if(radRevise.Checked == true)
            {
                //--------------------- Header ---------------------//
                cb_CompanyCode.Enabled = false;
                dtpETA.Enabled = false;
                dtpETD.Enabled = false;
                dtpDateCreateShipping.Enabled = false;
                txtInvoiceNo.ReadOnly = true;
                txtIssuedTo_CompanyCode.ReadOnly = true;
                txtIssuedTo_CompanyName.ReadOnly = true;
                txtIssuedTo_CompanyAddress.ReadOnly = true;
                txtIssuedTo_TelNo.ReadOnly = true;
                txtIssuedTo_FaxNo.ReadOnly = true;

                txtShipTo_CompanyCode.ReadOnly = true;
                txtShipTo_CompanyName.ReadOnly = true;
                txtShipTo_CompanyAddress.ReadOnly = true;
                txtShipTo_TelNo.ReadOnly = true;
                txtShipTo_FaxNo.ReadOnly = true;

                txtShipVia.ReadOnly = true;
                dtpRevenue.Enabled = false;
                cb_Freight.Enabled = false;
                txtVessel.ReadOnly = true;
                txtPortLoading.ReadOnly = true;
                txtPortDestination.ReadOnly = true;

                txtPriceCondition.ReadOnly = true;
                txtPaymentTerm.ReadOnly = true;

                btnLockData.Enabled = false;
                btnUnlockData.Enabled = false;
                btnSave_Data.Enabled = true;

                //Button Search
                btnSearch_ShippingNo.Enabled = true;
                btnSearch_IssuedTo.Enabled = false;
                btnSearch_ShipTo.Enabled = false;
                btnSearch_PortLoading.Enabled = false;
                btnSearch_PortDestination.Enabled = false;
                btnSearch_TradeCondition.Enabled = false;
                btnSearch_PaymentTerm.Enabled = false;

                GridView_Invoice.AllowUserToDeleteRows = false;
                GridView_PackingList.AllowUserToDeleteRows = false;

                //---- Enable ----//
                //Gridview Invoice
                GridView_Invoice.Columns["Quantity_Revise"].Visible = true;
                GridView_Invoice.Columns["Quantity_Revise"].ReadOnly = false;
                GridView_Invoice.Columns["Quantity_Revise"].DefaultCellStyle.BackColor = Color.White;
                //
                GridView_Invoice.Columns["Order_Price_Revise"].Visible = true;
                GridView_Invoice.Columns["Order_Price_Revise"].ReadOnly = false;
                GridView_Invoice.Columns["Order_Price_Revise"].DefaultCellStyle.BackColor = Color.White;
                //Gridview PackingList
                GridView_PackingList.Columns["Qty_Total_Revise"].Visible = true;
                GridView_PackingList.Columns["Qty_Total_Revise"].ReadOnly = false;
                GridView_PackingList.Columns["Qty_Total_Revise"].DefaultCellStyle.BackColor = Color.White;

                //---- Disable ----//
                foreach(DataGridViewRow dtRow in GridView_Invoice.Rows)
                {
                    if (!dtRow.IsNewRow)
                    {
                        dtRow.Cells["Customer_Code"].ReadOnly = true;
                        dtRow.Cells["Customer_Code"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Part_Description"].ReadOnly = true;
                        dtRow.Cells["Part_Description"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Tvc_ItemCode"].ReadOnly = true;
                        dtRow.Cells["Tvc_ItemCode"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Customer_PO"].ReadOnly = true;
                        dtRow.Cells["Customer_PO"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Order_Date"].ReadOnly = true;
                        dtRow.Cells["Order_Date"].Style.BackColor = Color.Gray;

                        dtRow.Cells["ThirdParty_PO"].ReadOnly = true;
                        dtRow.Cells["ThirdParty_PO"].Style.BackColor = Color.Gray;

                        dtRow.Cells["DueDate_PO"].ReadOnly = true;
                        dtRow.Cells["DueDate_PO"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Quantity"].ReadOnly = true;
                        dtRow.Cells["Quantity"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Balance"].ReadOnly = true;
                        dtRow.Cells["Balance"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Unit_Currency"].ReadOnly = true;
                        dtRow.Cells["Unit_Currency"].Style.BackColor = Color.Gray;

                        dtRow.Cells["USD_Rate"].ReadOnly = true;
                        dtRow.Cells["USD_Rate"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Order_Price"].ReadOnly = true;
                        dtRow.Cells["Order_Price"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Global_Price"].ReadOnly = true;
                        dtRow.Cells["Global_Price"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Amount_Jpy"].ReadOnly = true;
                        dtRow.Cells["Amount_Jpy"].Style.BackColor = Color.Gray;
                    }
                }

                //---------------- Gridview PackingList ----------------//
                //---- Disable ----//
                foreach (DataGridViewRow dtRow in GridView_PackingList.Rows)
                {
                    if (!dtRow.IsNewRow)
                    { 
                        dtRow.Cells["Customer_Code"].ReadOnly = true;
                        dtRow.Cells["Customer_Code"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Packages_No"].ReadOnly = true;
                        dtRow.Cells["Packages_No"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Customer_ItemCode"].ReadOnly = true;
                        dtRow.Cells["Customer_ItemCode"].Style.BackColor = Color.Gray;

                        dtRow.Cells["TVC_ItemCode"].ReadOnly = true;
                        dtRow.Cells["TVC_ItemCode"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Customer_PO"].ReadOnly = true;
                        dtRow.Cells["Customer_PO"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Qty_Carton"].ReadOnly = true;
                        dtRow.Cells["Qty_Carton"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Qty_Per_Carton"].ReadOnly = true;
                        dtRow.Cells["Qty_Per_Carton"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Qty_Total"].ReadOnly = true;
                        dtRow.Cells["Qty_Total"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Qty_Total_Revise"].ReadOnly = true;
                        dtRow.Cells["Qty_Total_Revise"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Net_Weight"].ReadOnly = true;
                        dtRow.Cells["Net_Weight"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Net_Weight_Total"].ReadOnly = true;
                        dtRow.Cells["Net_Weight_Total"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Gross_Weight"].ReadOnly = true;
                        dtRow.Cells["Gross_Weight"].Style.BackColor = Color.Gray;

                        dtRow.Cells["Lot_No"].ReadOnly = true;
                        dtRow.Cells["Lot_No"].Style.BackColor = Color.Gray;
                    }
                }
            }
        }

        private void GridView_Invoice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int columnIndex = 0;
            int rowIndex = 0;
            String _unitCurrency = "";
            DateTime _dateCreateInvoice = dtpDateCreateShipping.Value;

            if (e.RowIndex >= 0)
            {
                rowIndex = e.RowIndex;
                columnIndex = e.ColumnIndex;

                if (GridView_Invoice.Columns[columnIndex] is DataGridViewButtonColumn)
                {
                    _listItemCodeInfo = null;
                    if(GridView_Invoice.Rows.Count > 1)
                    {
                        foreach (DataGridViewRow dr in GridView_Invoice.Rows)
                        {
                            if (dr.Cells["Unit_Currency"].Value != null)
                            {
                                _unitCurrency = dr.Cells["Unit_Currency"].Value.ToString();
                                break;
                            }
                        }
                    }
                    Form_Search_PO_New _formSearch = new Form_Search_PO_New(_systemDAL, txtIssuedTo_CompanyCode.Text, _unitCurrency, _dateCreateInvoice);
                    _formSearch.StartPosition = FormStartPosition.CenterParent;
                    _formSearch.ShowDialog();

                    //Move data from form search to current Form
                    GetSelectedItem(_listItemCodeInfo);

                    //Sum Invoice
                    btnSumInvoice_Click(sender, e);

                    //Sum packing List
                    btnSumPL_Click(sender, e);

                    GridView_Invoice.ClearSelection();
                    int nRowIndex = GridView_Invoice.Rows.Count - 1;

                    GridView_Invoice.Rows[nRowIndex].Selected = true;
                    GridView_Invoice.Rows[nRowIndex].Cells[8].Selected = true;
                }
            }
        }

        private void GridView_Invoice_KeyDown(object sender, KeyEventArgs e)
        {
            int _RowIndex = 0;
            int _ColumnIndex = 0;

            if (e.KeyCode == Keys.Tab)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }

            if (e.KeyCode == Keys.F5)
            {
                //Get row index
                int rowIndex = 0;
                rowIndex = GridView_Invoice.CurrentCell.RowIndex;
                //Get column index
                int columnIndex = 0;
                columnIndex = GridView_Invoice.CurrentCell.ColumnIndex;
                String _unitCurrency = "";
                DateTime _dateCreateInvoice = dtpDateCreateShipping.Value;

                if (rowIndex >= 0)
                {
                    _listItemCodeInfo = null;
                    if (GridView_Invoice.Rows.Count > 1)
                    {
                        foreach (DataGridViewRow dr in GridView_Invoice.Rows)
                        {
                            if (dr.Cells["Unit_Currency"].Value != null)
                            {
                                _unitCurrency = dr.Cells["Unit_Currency"].Value.ToString();
                                break;
                            }
                        }
                    }
                    Form_Search_PO _formSearch = new Form_Search_PO(_systemDAL,"btnSearch_ItemCode", txtIssuedTo_CompanyCode.Text, _unitCurrency, _dateCreateInvoice);
                    _formSearch.StartPosition = FormStartPosition.CenterParent;
                    _formSearch.ShowDialog();

                    //
                    GetSelectedItem(_listItemCodeInfo);

                    //Sum Invoice
                    btnSumInvoice_Click(sender, e);

                    //Sum packing List
                    btnSumPL_Click(sender, e);

                    GridView_Invoice.ClearSelection();
                    int nRowIndex = GridView_Invoice.Rows.Count - 1;

                    GridView_Invoice.Rows[nRowIndex].Selected = true;
                    GridView_Invoice.Rows[nRowIndex].Cells[8].Selected = true;
                }
            }

            if (e.Control && e.KeyCode == Keys.C)
            {
                if (GridView_Invoice.SelectedCells.Count == 1
                    && (GridView_Invoice.CurrentCell.Value != null))
                {
                    _RowIndex = GridView_Invoice.CurrentCell.RowIndex;
                    _ColumnIndex = GridView_Invoice.CurrentCell.ColumnIndex;
                     Clipboard.SetText(GridView_Invoice.Rows[_RowIndex].Cells[_ColumnIndex].Value.ToString());
                }
            }

            if (e.Control && e.KeyCode == Keys.V)
            {
                if (GridView_Invoice.SelectedCells.Count == 1)
                {
                    _RowIndex = GridView_Invoice.CurrentCell.RowIndex;
                    _ColumnIndex = GridView_Invoice.CurrentCell.ColumnIndex;
                    GridView_Invoice.Rows[_RowIndex].Cells[_ColumnIndex].Value = Clipboard.GetText();
                }
            }

            if (e.KeyCode == Keys.Delete)
            {
                string exitMessageText = "Do you want to remove this row?";
                string exitCaption = "Confirm";
                MessageBoxButtons button = MessageBoxButtons.YesNo;
                DialogResult res = MessageBox.Show(exitMessageText, exitCaption, button, MessageBoxIcon.Exclamation);
                if (res == DialogResult.Yes)
                {
                    //Get row index
                    int rowIndex = GridView_Invoice.CurrentRow.Index;
                    string Inv_Customer_PO = "";
                    string PL_Customer_PO = "";
                    int index = 0;
                    List<int> list = new List<int>();

                    if (GridView_Invoice.Rows[rowIndex].Cells["Customer_PO"].Value != null)
                    {
                        Inv_Customer_PO = GridView_Invoice.Rows[rowIndex].Cells["Customer_PO"].Value.ToString();
                    }

                    if (!String.IsNullOrEmpty(Inv_Customer_PO))
                    { 
                        foreach (DataGridViewRow row in GridView_PackingList.Rows)
                        {
                            PL_Customer_PO = Convert.ToString(row.Cells["Customer_PO"].Value);
                            if (PL_Customer_PO == Inv_Customer_PO)
                            {
                                tabControl.SelectedIndex = 1;
                                //
                                exitMessageText = "Do you want to remove row 「" + (index + 1) + "」.\nCustomer PO「" + Inv_Customer_PO + "」 of 「Packing List」 ?";
                                exitCaption = "Confirm";
                                res = MessageBox.Show(exitMessageText, exitCaption, button, MessageBoxIcon.Exclamation);
                                if (res == DialogResult.Yes)
                                {
                                    list.Add(index);
                                }
                            }
                            index++;
                        }
                        foreach (int indexRemove in list.AsEnumerable().Reverse())
                        {
                            GridView_PackingList.Rows.RemoveAt(indexRemove);
                        }
                    }
                } else
                {
                    e.Handled = true;
                }
            }

            if (e.Control && e.KeyCode == Keys.F)
            {
                txt_Search_Grid.Focus();
            }

        }

        private void GridView_Invoice_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            decimal quantity, quantity_revise, price, price_revise, price_compare, amount, USD_Rate, balance;
            string _customer_PO = "";

            if (e.RowIndex != GridView_Invoice.NewRowIndex)
            {
                //Normal data
                if (radNormal.Checked == true)
                {
                    if (GridView_Invoice.Rows[e.RowIndex].Cells["Quantity"].Value != null
                    && GridView_Invoice.Rows[e.RowIndex].Cells["Order_Price"].Value != null)
                    {
                        if (decimal.TryParse(GridView_Invoice.Rows[e.RowIndex].Cells["Quantity"].Value.ToString(), out quantity)
                        && decimal.TryParse(GridView_Invoice.Rows[e.RowIndex].Cells["Order_Price"].Value.ToString(), out price))
                        {
                            if (GridView_Invoice.Rows[e.RowIndex].Cells["Balance"].Value != null)
                            {
                                decimal.TryParse(GridView_Invoice.Rows[e.RowIndex].Cells["Balance"].Value.ToString(), out balance);
                                if (quantity > balance)
                                {
                                    MessageBox.Show("Số lượng còn lại: " + balance + ".\nSố lượng nhập: " + quantity, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                            if (GridView_Invoice.Rows[e.RowIndex].Cells["Customer_PO"].Value != null)
                            {
                                _customer_PO = GridView_Invoice.Rows[e.RowIndex].Cells["Customer_PO"].Value.ToString();
                            }

                            if (GridView_Invoice.Rows[e.RowIndex].Cells["Unit_Currency"].Value.ToString().ToUpper() == "JPY")
                            {
                                //Cal Amount
                                amount = quantity * price;
                                GridView_Invoice.Rows[e.RowIndex].Cells["Amount_Jpy"].Value = Math.Round(amount, 0, MidpointRounding.AwayFromZero);
                            }
                            else if (GridView_Invoice.Rows[e.RowIndex].Cells["Unit_Currency"].Value.ToString().ToUpper() == "USD")
                            {
                                //Cal Amount(USD)
                                amount = quantity * price;
                                GridView_Invoice.Rows[e.RowIndex].Cells["Amount_Jpy"].Value = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
                            }
                        }
                    }

                    //Background red when quantity = 0
                    if (GridView_Invoice.Rows[e.RowIndex].Cells["Quantity"].Value != null)
                    {
                        if (Convert.ToDecimal(GridView_Invoice.Rows[e.RowIndex].Cells["Quantity"].Value) == 0)
                        {
                            GridView_Invoice.Rows[e.RowIndex].Cells["Quantity"].Style.BackColor = Color.Red;
                        }
                        else
                        {
                            GridView_Invoice.Rows[e.RowIndex].Cells["Quantity"].Style.BackColor = Color.White;
                        }
                    }

                    //Background red when order different from global price
                    if (GridView_Invoice.Rows[e.RowIndex].Cells["Order_Price"].Value != null
                     && GridView_Invoice.Rows[e.RowIndex].Cells["Global_Price"].Value != null)
                    {
                        if (decimal.TryParse(GridView_Invoice.Rows[e.RowIndex].Cells["Order_Price"].Value.ToString(), out price)
                         && decimal.TryParse(GridView_Invoice.Rows[e.RowIndex].Cells["Global_Price"].Value.ToString(), out price_compare))
                        {
                            if (price == price_compare)
                            {
                                GridView_Invoice.Rows[e.RowIndex].Cells["Order_Price"].Style.BackColor = Color.White;
                                GridView_Invoice.Rows[e.RowIndex].Cells["Global_Price"].Style.BackColor = Color.White;
                            }
                            else
                            {
                                GridView_Invoice.Rows[e.RowIndex].Cells["Order_Price"].Style.BackColor = Color.Red;
                                GridView_Invoice.Rows[e.RowIndex].Cells["Global_Price"].Style.BackColor = Color.Red;
                            }
                        }
                    }
                    else
                    {
                        GridView_Invoice.Rows[e.RowIndex].Cells["Order_Price"].Style.BackColor = Color.Red;
                        GridView_Invoice.Rows[e.RowIndex].Cells["Global_Price"].Style.BackColor = Color.Red;
                    }

                    if (GridView_Invoice.Rows[e.RowIndex].Cells["USD_Rate"].Value != null)
                    {
                        USD_Rate = decimal.Parse(GridView_Invoice.Rows[e.RowIndex].Cells["USD_Rate"].Value.ToString());
                        foreach (DataGridViewRow dr in GridView_Invoice.Rows)
                        {
                            if (dr.Cells["TVC_ItemCode"].Value != null)
                            {
                                dr.Cells["USD_Rate"].Value = USD_Rate;
                            }
                        }
                    }
                }

                //Revise data
                if (radRevise.Checked == true)
                {
                    if (GridView_Invoice.Rows[e.RowIndex].Cells["Quantity_Revise"].Value != null
                     && GridView_Invoice.Rows[e.RowIndex].Cells["Order_Price_Revise"].Value != null)
                    {
                        if (decimal.TryParse(GridView_Invoice.Rows[e.RowIndex].Cells["Quantity_Revise"].Value.ToString(), out quantity_revise)
                         && decimal.TryParse(GridView_Invoice.Rows[e.RowIndex].Cells["Order_Price_Revise"].Value.ToString(), out price_revise))
                        {
                            if (GridView_Invoice.Rows[e.RowIndex].Cells["Unit_Currency"].Value.ToString().ToUpper() == "JPY")
                            {
                                //Cal Amount
                                amount = quantity_revise * price_revise;
                                GridView_Invoice.Rows[e.RowIndex].Cells["Amount_Jpy"].Value = Math.Round(amount, 0, MidpointRounding.AwayFromZero);
                            }
                            else if (GridView_Invoice.Rows[e.RowIndex].Cells["Unit_Currency"].Value.ToString().ToUpper() == "USD")
                            {
                                //Cal Amount(USD)
                                amount = quantity_revise * price_revise;
                                GridView_Invoice.Rows[e.RowIndex].Cells["Amount_Jpy"].Value = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
                            }
                        }
                    }
                }

                //Sum Invoice
                btnSumInvoice_Click(sender, e);

                //Sum packing List
                btnSumPL_Click(sender, e);
            }
        }

        private void GridView_PackingList_KeyDown(object sender, KeyEventArgs e)
        {
            int _RowIndex = 0;
            int _ColumnIndex = 0;

            if (e.KeyCode == Keys.Tab)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }

            if (e.Control && e.KeyCode == Keys.C)
            {
                if (GridView_PackingList.SelectedCells.Count == 1
                    && (GridView_PackingList.CurrentCell.Value != null))
                {
                    _RowIndex = GridView_PackingList.CurrentCell.RowIndex;
                    _ColumnIndex = GridView_PackingList.CurrentCell.ColumnIndex;
                    Clipboard.SetText(GridView_PackingList.Rows[_RowIndex].Cells[_ColumnIndex].Value.ToString());
                }
            }

            if (e.Control && e.KeyCode == Keys.V)
            {
                if (GridView_PackingList.SelectedCells.Count == 1)
                {
                    _RowIndex = GridView_PackingList.CurrentCell.RowIndex;
                    _ColumnIndex = GridView_PackingList.CurrentCell.ColumnIndex;
                    GridView_PackingList.Rows[_RowIndex].Cells[_ColumnIndex].Value = Clipboard.GetText();
                }
            }

            //if (e.KeyCode == Keys.Delete)
            //{
            //    //string exitMessageText = "Can't remove data in tab 「Packing List」";
            //    //string exitCaption = "Information";
            //    //MessageBoxButtons button = MessageBoxButtons.OK;
            //    //DialogResult res = MessageBox.Show(exitMessageText, exitCaption, button, MessageBoxIcon.Exclamation);
            //    //e.Handled = true;
            //}

            if (e.Control && e.KeyCode == Keys.F)
            {
                txt_Search_Grid.Focus();
            }
        }
        #endregion

        #region KeyDown_Event
        private void txtIssuedTo_CompanyCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnSearch_IssuedTo_Click(sender, e);
            }
            if (e.KeyCode == Keys.Enter)
            {
                //Upper Issued to company Code
                string IssuedTo_CompanyCode = txtIssuedTo_CompanyCode.Text.ToString().ToUpper();
                txtIssuedTo_CompanyCode.Text = IssuedTo_CompanyCode;

                //Search value company code
                DataTable _tempTable = new DataTable();
                _tempTable = _searchDAO.GetCustomer_IssuedTo(IssuedTo_CompanyCode);

                if (_tempTable.Rows.Count > 0)
                {
                    foreach (DataRow dtRow in _tempTable.Rows)
                    {
                        if (!String.IsNullOrEmpty(Convert.ToString(dtRow["CUSTOMER_CODE"])))
                        { 
                            txtIssuedTo_CompanyCode.Text = dtRow["CUSTOMER_CODE"].ToString();
                            txtIssuedTo_CompanyName.Text = dtRow["CUSTOMER_NAME1"].ToString();
                            txtIssuedTo_CompanyAddress.Text = dtRow["ADDRESS"].ToString();
                            txtIssuedTo_TelNo.Text = dtRow["TEL_NO"].ToString();
                            txtIssuedTo_FaxNo.Text = dtRow["FAX_NO"].ToString();
                        }
                    }
                    this.SelectNextControl((Control)sender,true, true, true, true);
                } else
                {
                    MessageBox.Show("Can't find info IssuedTo!");
                    txtIssuedTo_CompanyCode.Focus();
                }
            }
        }

        private void txtShipTo_CompanyCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnSearch_ShipTo_Click(sender, e);
            }
            if (e.KeyCode == Keys.Enter)
            {
                //Upper Issued to company Code
                string ShipTo_CompanyCode = txtShipTo_CompanyCode.Text.ToString().ToUpper();
                txtShipTo_CompanyCode.Text = ShipTo_CompanyCode;

                //Search value company code
                DataTable _tempTable = new DataTable();
                _tempTable = _searchDAO.GetCustomer_ShipTo(ShipTo_CompanyCode);

                if (_tempTable.Rows.Count > 0)
                {
                    foreach (DataRow dtRow in _tempTable.Rows)
                    {
                        if (!String.IsNullOrEmpty(Convert.ToString(dtRow["CUSTOMER_CODE"])))
                        {
                            txtShipTo_CompanyCode.Text = dtRow["CUSTOMER_CODE"].ToString();
                            txtShipTo_CompanyName.Text = dtRow["CUSTOMER_NAME1"].ToString();
                            txtShipTo_CompanyAddress.Text = dtRow["ADDRESS"].ToString();
                            txtShipTo_TelNo.Text = dtRow["TEL_NO"].ToString();
                            txtShipTo_FaxNo.Text = dtRow["FAX_NO"].ToString();
                        }
                    }
                    this.SelectNextControl((Control)sender, true, true, true, true);
                }
                else
                {
                    MessageBox.Show("Can't find info ShipTo!");
                    txtShipTo_CompanyCode.Focus();
                }
            }
        }

        private void txtPortLoading_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnSearch_PortLoading_Click(sender, e);
            }
        }

        private void txtPortDestination_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnSearch_PortDestination_Click(sender, e);
            }
        }

        private void txtPriceCondition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnSearch_PriceCondition_Click(sender, e);
            }
        }

        private void txtPaymentTerm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnSearch_PaymentTerm_Click(sender, e);
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
            int Version = 0;
            if ((MessageBox.Show("Xác nhận lưu dữ liệu?", "Xác Nhận"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Question
                , MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                if (CheckError() == true)
                {
                    string _invoiceNo = txtInvoiceNo.Text.Trim();
                    string _shippingNo = txtShippingNo.Text.Trim();
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
                    invoiceMS["DateCreate"] = dtpDateCreateShipping.Value;
                    invoiceMS["IssuedToCode"] = txtIssuedTo_CompanyCode.Text.Trim();
                    invoiceMS["IssuedToName"] = txtIssuedTo_CompanyName.Text.Trim();
                    invoiceMS["IssuedToAddress"] = txtIssuedTo_CompanyAddress.Text.Trim();
                    invoiceMS["IssuedTelNo"] = txtIssuedTo_TelNo.Text.Trim();
                    invoiceMS["IssuedFaxNo"] = txtIssuedTo_FaxNo.Text.Trim();
                    invoiceMS["ShipToCode"] = txtShipTo_CompanyCode.Text.Trim();
                    invoiceMS["ShipToName"] = txtShipTo_CompanyName.Text.Trim();
                    invoiceMS["ShipToAddress"] = txtShipTo_CompanyAddress.Text.Trim();
                    invoiceMS["ShipToTelNo"] = txtShipTo_TelNo.Text.Trim();
                    invoiceMS["ShipToFaxNo"] = txtShipTo_FaxNo.Text.Trim();
                    invoiceMS["Revenue"] = dtpRevenue.Value.ToString("MM/yyyy");
                    invoiceMS["ShipVia"] = txtShipVia.Text.Trim();
                    invoiceMS["Freight"] = Convert.ToInt32(cb_Freight.SelectedValue);
                    invoiceMS["Vessel"] = txtVessel.Text.Trim();
                    invoiceMS["PortLoading"] = txtPortLoading.Text.Trim();
                    invoiceMS["PortDestination"] = txtPortDestination.Text.Trim();
                    invoiceMS["ETD"] = dtpETD.Value;
                    invoiceMS["ETA"] = dtpETA.Value;
                    invoiceMS["TradeCondition"] = txtPriceCondition.Text.Trim();
                    invoiceMS["PaymentTerm"] = txtPaymentTerm.Text.Trim();
                    invoiceMS["CreateBy"] = _systemDAL.UserName;
                    invoiceMS["CreateAt"] = DateTime.Now;
                    invoiceMS["EditBy"] = _systemDAL.UserName;
                    invoiceMS["EditAt"] = DateTime.Now;
                    dtInvoiceMS.Rows.Add(invoiceMS);

                    //Grid Invoice
                    foreach (DataGridViewRow row in GridView_Invoice.Rows)
                    {
                        if (row.Cells["Tvc_ItemCode"].Value != null)
                        {
                            DataRow invoiceDetail = dtInvoiceDetail.NewRow();
                            invoiceDetail["CompanyCode"] = _systemDAL.CompanyCode;
                            invoiceDetail["CustomerCode"] = row.Cells["Customer_Code"].Value.ToString();
                            invoiceDetail["ShippingNo"] = _shippingNo;
                            invoiceDetail["InvoiceNo"] = _invoiceNo;
                            //Mode: Add new
                            if (radNormal.Checked == true)
                            {
                                invoiceDetail["ReviseNo"] = "";
                                invoiceDetail["ReviseDate"] = DateTime.Now;
                                invoiceDetail["Version"] = Number.Zero;                     //Init = 0
                            }
                            else if (radRevise.Checked == true)
                            {
                                invoiceDetail["ReviseNo"] = _shippingNo + "_Revise_" + Convert.ToInt32(Version + 1);
                                invoiceDetail["ReviseDate"] = DateTime.Now;
                                invoiceDetail["Version"] = Convert.ToInt32(Version + 1);    //Init = 0
                            }
                            invoiceDetail["ItemName"] = row.Cells["Part_Description"].Value.ToString();
                            invoiceDetail["Cus_ItemCode"] = row.Cells["Cus_ItemCode"].Value.ToString();
                            invoiceDetail["Tvc_ItemCode"] = row.Cells["Tvc_ItemCode"].Value.ToString();
                            invoiceDetail["Customer_PO"] = row.Cells["Customer_PO"].Value.ToString();
                            if (row.Cells["ThirdParty_ItemCode"].Value != null)
                            { 
                                invoiceDetail["ThirdParty_ItemCode"] = row.Cells["ThirdParty_ItemCode"].Value.ToString();
                            }
                            if (row.Cells["ThirdParty_PO"].Value != null) {
                                invoiceDetail["ThirdParty_PO"] = row.Cells["ThirdParty_PO"].Value.ToString();
                            }
                            if (row.Cells["Order_Date"].Value != null)
                            { 
                                invoiceDetail["Order_Date"] = Convert.ToDateTime(row.Cells["Order_Date"].Value);
                            }
                            if (row.Cells["DueDate_PO"].Value != null) { 
                                invoiceDetail["DueDate_PO"] = Convert.ToDateTime(row.Cells["DueDate_PO"].Value);
                            }
                            invoiceDetail["Quantity"] = Convert.ToDecimal(row.Cells["Quantity"].Value);
                            if (radRevise.Checked == true)
                            { 
                                invoiceDetail["QuantityRevise"] = Convert.ToDecimal(row.Cells["Quantity_Revise"].Value);
                            } else if (radNormal.Checked == true || radLock.Checked == true)
                            {
                                invoiceDetail["QuantityRevise"] = 0;
                            }
                            invoiceDetail["Balance"] = Convert.ToDecimal(row.Cells["Balance"].Value);
                            invoiceDetail["Unit_Currency"] = row.Cells["Unit_Currency"].Value.ToString();
                            invoiceDetail["USD_Rate"] = row.Cells["USD_Rate"].Value;
                            invoiceDetail["OrderPrice"] = Convert.ToDecimal(row.Cells["Order_Price"].Value);
                            if (radRevise.Checked == true) { 
                                invoiceDetail["OrderPriceRevise"] = Convert.ToDecimal(row.Cells["Order_Price_Revise"].Value);
                            }
                            else if (radNormal.Checked == true || radLock.Checked== true)
                            {
                                invoiceDetail["OrderPriceRevise"] = 0;
                            }
                            invoiceDetail["Global_Price"] = Convert.ToDecimal(row.Cells["Global_Price"].Value);
                            invoiceDetail["Amount"] = Convert.ToDecimal(row.Cells["Amount_Jpy"].Value);
                            dtInvoiceDetail.Rows.Add(invoiceDetail);
                        }
                    }

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

                    //Grid Packing List
                    foreach (DataGridViewRow row in GridView_PackingList.Rows)
                    {
                        if (row.Cells["Customer_ItemCode"].Value != null)
                        {
                            DataRow packingListDetail = dtPackingListDetail.NewRow();
                            packingListDetail["CompanyCode"] = _systemDAL.CompanyCode;
                            packingListDetail["CustomerCode"] = row.Cells["Customer_Code"].Value.ToString();
                            packingListDetail["ShippingNo"] = _shippingNo;
                            packingListDetail["InvoiceNo"] = _invoiceNo;
                            //Mode: Add new
                            if (radNormal.Checked == true)
                            {
                                packingListDetail["ReviseNo"] = _shippingNo;
                                packingListDetail["ReviseDate"] = DateTime.Now;
                                packingListDetail["Version"] = Number.Zero;                     //Init = 0
                            }
                            else if (radRevise.Checked == true)
                            {
                                packingListDetail["ReviseNo"] = _shippingNo + "_Revise_" + Convert.ToInt32(Version + 1);
                                packingListDetail["ReviseDate"] = DateTime.Now;
                                packingListDetail["Version"] = Convert.ToInt32(Version + 1);    //Init = 0
                            }
                            if (row.Cells["Packages_No"].Value != null)
                            { 
                                packingListDetail["PackagesNo"] = row.Cells["Packages_No"].Value.ToString();
                            }
                            packingListDetail["Customer_ItemCode"] = row.Cells["Customer_ItemCode"].Value.ToString();
                            packingListDetail["TVC_ItemCode"] = row.Cells["TVC_ItemCode"].Value.ToString();
                            packingListDetail["Customer_PO"] = row.Cells["Customer_PO"].Value.ToString();
                            packingListDetail["QtyCarton"] = Convert.ToDecimal(row.Cells["Qty_Carton"].Value);
                            packingListDetail["QtyPerCarton"] = row.Cells["Qty_Per_Carton"].Value.ToString();
                            packingListDetail["QuantityTotal"] = Convert.ToDecimal(row.Cells["Qty_Total"].Value);
                            if (radRevise.Checked == true)
                            {
                                packingListDetail["QuantityTotalRevise"] = Convert.ToDecimal(row.Cells["Qty_Total_Revise"].Value);
                            } else if (radNormal.Checked == true || radLock.Checked == true)
                            {
                                packingListDetail["QuantityTotalRevise"] = 0;
                            }
                            packingListDetail["NetWeight"] = Convert.ToDecimal(row.Cells["Net_Weight"].Value);
                            packingListDetail["NetWeight_Total"] = Convert.ToDecimal(row.Cells["Net_Weight_Total"].Value);
                            packingListDetail["GrossWeight"] = Convert.ToDecimal(row.Cells["Gross_Weight"].Value);
                            packingListDetail["LotNo"] = row.Cells["Lot_No"].Value.ToString();
                            dtPackingListDetail.Rows.Add(packingListDetail);
                        }
                    }

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
                        if (GridView_Invoice.Rows.Count == 0)
                        {

                        }

                        //Insert new data
                        if (_shippingDAO.insertShipping(dtInvoiceMS,Grid_Invoice,dtInvoiceDetail, dtPackingListDetail) == true)
                        {
                            string Message = "Lưu thành công shipping : \"" + txtShippingNo.Text + "\"!";
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
                            Message += " Computer: " + Environment.MachineName + ". Invoice " + (GridView_Invoice.Rows.Count - 1) + " rows";
                            Message += ". Packing List " + (GridView_PackingList.Rows.Count - 1) + " rows";
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
            string shippingNo = txtShippingNo.Text.Trim();

            Form_Shipping_Output_PackingList form_Shipping_Output_PackingList = new Form_Shipping_Output_PackingList(_systemDAL, shippingNo);
            form_Shipping_Output_PackingList.StartPosition = FormStartPosition.CenterParent;
            form_Shipping_Output_PackingList.ShowDialog();

            ClearData();

            txtShippingNo.Text = shippingNo;

            //Load Invoice
            btn_SearchShipping_Click(sender, e);

            //Setting enable item
            SettingInitGridView();
        }

        private void btnLockData_Click(object sender, EventArgs e)
        {
            string _shippingNo = txtShippingNo.Text.Trim();
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
            string _shippingNo = txtShippingNo.Text.Trim();
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
            string _shippingNo = txtShippingNo.Text.Trim();
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
                                    txtShippingNo.Text = _shippingNo;
                                    //Reload Shipping Instruction
                                    btn_SearchShipping_Click(sender, e);
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
            string _shippingNo = txtShippingNo.Text.Trim();
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
                        dtpDateCreateShipping.Value = Convert.ToDateTime(row["DATE_CREATE"]);

                        //SHIPPING NO
                        txtShippingNo.Text = row["SHIPPING_NO"].ToString();
                        txtShippingNo.Enabled = false;

                        //INVOICE NO
                        txtInvoiceNo.Text = row["INVOICE_NO"].ToString();

                        //ISSUEDTO
                        txtIssuedTo_CompanyCode.Text = row["ISSUEDTO_CUSTOMER_CODE"].ToString();
                        txtIssuedTo_CompanyName.Text = row["ISSUEDTO_CUSTOMER_NAME"].ToString();
                        txtIssuedTo_CompanyAddress.Text = row["ISSUEDTO_CUSTOMER_ADDRESS"].ToString();
                        txtIssuedTo_TelNo.Text = row["ISSUEDTO_CUSTOMER_TEL_NO"].ToString();
                        txtIssuedTo_FaxNo.Text = row["ISSUEDTO_CUSTOMER_FAX_NO"].ToString();

                        //if (row["ISSUEDTO_CUSTOMER_CODE"].Equals("TTC"))
                        //{
                        //    this.GridView_PackingList.Columns["Customer_PO"].Visible = true;
                        //} else
                        //{
                        //    this.GridView_PackingList.Columns["Customer_PO"].Visible = false;
                        //}

                        //SHIPTO
                        txtShipTo_CompanyCode.Text = row["SHIPTO_CUSTOMER_CODE"].ToString();
                        txtShipTo_CompanyName.Text = row["SHIPTO_CUSTOMER_NAME"].ToString();
                        txtShipTo_CompanyAddress.Text = row["SHIPTO_CUSTOMER_ADDRESS"].ToString();
                        txtShipTo_TelNo.Text = row["SHIPTO_CUSTOMER_TEL_NO"].ToString();
                        txtShipTo_FaxNo.Text = row["SHIPTO_CUSTOMER_FAX_NO"].ToString();

                        if (!String.IsNullOrEmpty(Convert.ToString(row["REVENUE"])))
                        {
                            //REVENUE
                            dtpRevenue.Value = Convert.ToDateTime(row["REVENUE"]);
                        }
                        else
                        {
                            //REVENUE
                            dtpRevenue.Value = DateTime.MinValue;
                        }

                        //SHIP VIA
                        txtShipVia.Text = row["SHIP_VIA"].ToString();

                        //FREIGHT
                        cb_Freight.SelectedValue = Convert.ToInt32(row["FREIGHT"]);

                        //VESSEL
                        txtVessel.Text = row["VESSEL"].ToString();

                        //PORT OF LOADING
                        txtPortLoading.Text = row["PORT_OF_LOADING"].ToString();

                        //PORT OF DESTINATION
                        txtPortDestination.Text = row["PORT_OF_DESTINATION"].ToString();

                        //ETD
                        dtpETD.Value = Convert.ToDateTime(row["ETD"]);

                        //ETA
                        dtpETA.Value = Convert.ToDateTime(row["ETA"]);

                        //TRADE CONDITION
                        txtPriceCondition.Text = row["TRADE_CONDITION"].ToString();

                        //PAYMENT TERM
                        txtPaymentTerm.Text = row["PAYMENT_TERM"].ToString();

                        //CREATE BY
                        createBy = row["CREATE_BY"].ToString();
                    }

                    //Grid data
                    Grid_Invoice = _shippingDAO.GetDetail_ShipInv(_shippingNo);
                    if (Grid_Invoice.Rows.Count == 0)
                    {
                        //MessageBox.Show("Không có dữ liệu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (Grid_Invoice.Rows.Count >= 1)
                    {
                        //Clear DataGridView
                        GridView_Invoice.Rows.Clear();
                        GridView_PackingList.Rows.Clear();
                        foreach (DataRow row in Grid_Invoice.Rows)
                        {
                            if (!String.IsNullOrEmpty(row["INV_CUSTOMER_CODE"].ToString()))
                            {
                                int indexInv = GridView_Invoice.Rows.Add();
                                //Binding for Invoice
                                GridView_Invoice.Rows[indexInv].Cells["Customer_Code"].Value = row["INV_CUSTOMER_CODE"].ToString();
                                GridView_Invoice.Rows[indexInv].Cells["Part_Description"].Value = row["INV_ITEM_NAME"].ToString();
                                GridView_Invoice.Rows[indexInv].Cells["Cus_ItemCode"].Value = row["INV_CUS_ITEM_CODE"].ToString();
                                GridView_Invoice.Rows[indexInv].Cells["Tvc_ItemCode"].Value = row["INV_ITEM_CODE"].ToString();
                                GridView_Invoice.Rows[indexInv].Cells["Customer_PO"].Value = row["INV_REF_PO_NO"].ToString();
                                GridView_Invoice.Rows[indexInv].Cells["ThirdParty_PO"].Value = row["THIRD_PARTY_PO"].ToString();
                                if (!String.IsNullOrEmpty(Convert.ToString(row["ORDER_DATE"])))
                                {
                                    GridView_Invoice.Rows[indexInv].Cells["Order_Date"].Value = Convert.ToDateTime(row["ORDER_DATE"]).ToString("dd/MM/yyyy");
                                }
                                GridView_Invoice.Rows[indexInv].Cells["DueDate_PO"].Value = Convert.ToDateTime(row["DUE_DATE_PO"]).ToString("dd/MM/yyyy");
                                GridView_Invoice.Rows[indexInv].Cells["Quantity"].Value = row["INV_QUANTITY"];
                                GridView_Invoice.Rows[indexInv].Cells["Quantity_Revise"].Value = row["INV_QUANTITY_REVISE"];
                                GridView_Invoice.Rows[indexInv].Cells["Balance"].Value = row["INV_BALANCE"];
                                GridView_Invoice.Rows[indexInv].Cells["Unit_Currency"].Value = row["INV_UNIT_CURRENCY"];
                                GridView_Invoice.Rows[indexInv].Cells["USD_Rate"].Value = row["INV_USD_RATE"];
                                GridView_Invoice.Rows[indexInv].Cells["Order_Price"].Value = row["INV_ORDER_PRICE"];
                                GridView_Invoice.Rows[indexInv].Cells["Order_Price_Revise"].Value = row["INV_ORDER_PRICE_REVISE"];
                                GridView_Invoice.Rows[indexInv].Cells["Global_Price"].Value = row["GLOBAL_PRICE"];
                                GridView_Invoice.Rows[indexInv].Cells["Amount_Jpy"].Value = row["INV_AMOUNT"];
                            }
                        }

                        //
                        Grid_PackingList = _shippingDAO.GetDetail_ShipPL(_shippingNo);
                        if (Grid_PackingList.Rows.Count >= 1)
                        {
                            foreach (DataRow row in Grid_PackingList.Rows)
                            {
                                if (row["PL_CUSTOMER_CODE"] != null)
                                {
                                    int indexPL = GridView_PackingList.Rows.Add();
                                    GridView_PackingList.Rows[indexPL].Cells["Customer_Code"].Value = row["PL_CUSTOMER_CODE"].ToString();
                                    GridView_PackingList.Rows[indexPL].Cells["Packages_No"].Value = row["PL_PACKAGES_NO"].ToString();
                                    GridView_PackingList.Rows[indexPL].Cells["Customer_ItemCode"].Value = row["PL_ITEM_CODE"].ToString();
                                    GridView_PackingList.Rows[indexPL].Cells["TVC_ItemCode"].Value = row["PL_TVC_ITEM_CODE"].ToString();
                                    GridView_PackingList.Rows[indexPL].Cells["Customer_PO"].Value = row["PL_CUSTOMER_PO"].ToString();
                                    GridView_PackingList.Rows[indexPL].Cells["Qty_Carton"].Value = Convert.ToDecimal(row["PL_QTY_CARTON"]);
                                    GridView_PackingList.Rows[indexPL].Cells["Qty_Per_Carton"].Value = Convert.ToDecimal(row["PL_QTY_PER_CARTON"]);
                                    GridView_PackingList.Rows[indexPL].Cells["Qty_Total"].Value = Convert.ToDecimal(row["PL_QTY_TOTAL"]);
                                    GridView_PackingList.Rows[indexPL].Cells["Qty_Total_Revise"].Value = row["PL_QTY_TOTAL_REVISE"];
                                    GridView_PackingList.Rows[indexPL].Cells["Net_Weight"].Value = Convert.ToDecimal(row["PL_NET_WEIGHT"]);
                                    GridView_PackingList.Rows[indexPL].Cells["Net_Weight_Total"].Value = Convert.ToDecimal(row["PL_NET_WEIGHT_TOTAL"]);
                                    GridView_PackingList.Rows[indexPL].Cells["Gross_Weight"].Value = Convert.ToDecimal(row["PL_GROSS_WEIGHT"]);
                                    GridView_PackingList.Rows[indexPL].Cells["Lot_No"].Value = row["PL_LOT_NO"].ToString();
                                }
                            }
                        }

                        //Sum Invoice
                        btnSumInvoice_Click(sender, e);
                        //Sum Packing List
                        btnSumPL_Click(sender, e);
                    }
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Binding_Grid_Invoice(DataTable _tempInvoiceTable)
        {
            foreach (DataRow row in _tempInvoiceTable.Rows)
            {
                if (!String.IsNullOrEmpty(row["INV_CUSTOMER_CODE"].ToString()))
                {
                    int indexInv = GridView_Invoice.Rows.Add();
                    //Binding for Invoice
                    GridView_Invoice.Rows[indexInv].Cells["Customer_Code"].Value = row["INV_CUSTOMER_CODE"].ToString();
                    GridView_Invoice.Rows[indexInv].Cells["Part_Description"].Value = row["INV_ITEM_NAME"].ToString();
                    GridView_Invoice.Rows[indexInv].Cells["Cus_ItemCode"].Value = row["INV_CUS_ITEM_CODE"].ToString();
                    GridView_Invoice.Rows[indexInv].Cells["Tvc_ItemCode"].Value = row["INV_ITEM_CODE"].ToString();
                    GridView_Invoice.Rows[indexInv].Cells["Customer_PO"].Value = row["INV_REF_PO_NO"].ToString();
                    GridView_Invoice.Rows[indexInv].Cells["ThirdParty_PO"].Value = row["THIRD_PARTY_PO"].ToString();
                    GridView_Invoice.Rows[indexInv].Cells["DueDate_PO"].Value = Convert.ToDateTime(row["DUE_DATE_PO"]).ToString("dd/MM/yyyy");
                    GridView_Invoice.Rows[indexInv].Cells["Quantity"].Value = row["INV_QUANTITY"];
                    GridView_Invoice.Rows[indexInv].Cells["Quantity_Revise"].Value = row["INV_QUANTITY_REVISE"];
                    GridView_Invoice.Rows[indexInv].Cells["Balance"].Value = row["INV_BALANCE"];
                    GridView_Invoice.Rows[indexInv].Cells["Unit_Currency"].Value = row["INV_UNIT_CURRENCY"];
                    GridView_Invoice.Rows[indexInv].Cells["USD_Rate"].Value = row["INV_USD_RATE"];
                    GridView_Invoice.Rows[indexInv].Cells["Order_Price"].Value = row["INV_ORDER_PRICE"];
                    GridView_Invoice.Rows[indexInv].Cells["Order_Price_Revise"].Value = row["INV_ORDER_PRICE_REVISE"];
                    GridView_Invoice.Rows[indexInv].Cells["Global_Price"].Value = row["GLOBAL_PRICE"];
                    GridView_Invoice.Rows[indexInv].Cells["Amount_Jpy"].Value = row["INV_AMOUNT"];
                }
            }
        }

        private void Binding_Grid_PackingList(DataTable _tempDataTable)
        {

        }
        #endregion

        private void btn_NewRow_Click(object sender, EventArgs e)
        {

        }

        private void btn_CoppyRow_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                if (GridView_Invoice.SelectedRows.Count > 0)
                {

                }
            }
            else if (tabControl.SelectedIndex == 1)
            {
                if(GridView_PackingList.Rows.Count > 1)
                {
                    if (GridView_PackingList.SelectedRows.Count == 1)
                    {
                        int selectedRow = GridView_PackingList.CurrentRow.Index;
                        int position = selectedRow + 1;
                        GridView_PackingList.Rows.Insert(position);
                        GridView_PackingList.Rows[position].Cells["Customer_Code"].Value = GridView_PackingList.Rows[selectedRow].Cells["Customer_Code"].Value;
                        GridView_PackingList.Rows[position].Cells["Packages_No"].Value = GridView_PackingList.Rows[selectedRow].Cells["Packages_No"].Value;
                        GridView_PackingList.Rows[position].Cells["Customer_ItemCode"].Value = GridView_PackingList.Rows[selectedRow].Cells["Customer_ItemCode"].Value;
                        GridView_PackingList.Rows[position].Cells["TVC_ItemCode"].Value = GridView_PackingList.Rows[selectedRow].Cells["TVC_ItemCode"].Value;
                        GridView_PackingList.Rows[position].Cells["Customer_PO"].Value = GridView_PackingList.Rows[selectedRow].Cells["Customer_PO"].Value;
                        GridView_PackingList.Rows[position].Cells["Qty_Carton"].Value = GridView_PackingList.Rows[selectedRow].Cells["Qty_Carton"].Value;
                        GridView_PackingList.Rows[position].Cells["Qty_Per_Carton"].Value = GridView_PackingList.Rows[selectedRow].Cells["Qty_Per_Carton"].Value;
                        GridView_PackingList.Rows[position].Cells["Qty_Total"].Value = GridView_PackingList.Rows[selectedRow].Cells["Qty_Total"].Value;
                        GridView_PackingList.Rows[position].Cells["Qty_Total_Revise"].Value = GridView_PackingList.Rows[selectedRow].Cells["Qty_Total_Revise"].Value;
                        GridView_PackingList.Rows[position].Cells["Net_Weight"].Value = GridView_PackingList.Rows[selectedRow].Cells["Net_Weight"].Value;
                        GridView_PackingList.Rows[position].Cells["Net_Weight_Total"].Value = GridView_PackingList.Rows[selectedRow].Cells["Net_Weight_Total"].Value;
                        GridView_PackingList.Rows[position].Cells["Gross_Weight"].Value = GridView_PackingList.Rows[selectedRow].Cells["Gross_Weight"].Value;
                        GridView_PackingList.Rows[position].Cells["Lot_No"].Value = GridView_PackingList.Rows[selectedRow].Cells["Lot_No"].Value;
                        GridView_PackingList.CurrentCell = GridView_PackingList.Rows[position].Cells["Packages_No"];
                        GridView_PackingList.Rows[position].Cells["Packages_No"].Selected = true;
                        GridView_PackingList.BeginEdit(true);
                    } else if (GridView_PackingList.SelectedRows.Count > 1)
                    {
                        MessageBox.Show("Xin chỉ chọn 1 dòng cần copy!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    } else
                    {
                        MessageBox.Show("Xin hãy chọn dòng cần copy!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                } else if (GridView_PackingList.Rows.Count == 1)
                {
                    MessageBox.Show("Packing List rỗng, không có dữ liệu copy!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            //Sum packing List
            btnSumPL_Click(sender, e);
        }

        /// <summary>
        /// Set focus to last row added
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView_Invoice_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (GridView_Invoice.CurrentCell != null)
                GridView_Invoice.CurrentCell.OwningRow.Selected = false;
            if (lastSelected != null)
                lastSelected.Selected = false;
            lastSelected = GridView_Invoice.Rows[e.RowIndex];
            lastSelected.Selected = true;
        }

        private void txtShippingNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Load_InvoiceSearch();
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
            else if (e.KeyCode == Keys.F5)
            {
                //btnSearch_Inv_Click(sender, e);
                btnSearch_ShippingNo_Click(sender, e);
            }
        }

        private void txtSearchValue_Gridview_Enter(object sender, EventArgs e)
        {           
            //Setting color text search
            if (txt_Search_Grid.Text == "Search Value")
            {
                txt_Search_Grid.Text = "";
                txt_Search_Grid.ForeColor = Color.Black;
            }
        }

        private void txtSearchValue_Gridview_Leave(object sender, EventArgs e)
        {
            string _valueSearch = txt_Search_Grid.Text.Trim();
            List<int> _listIndex = new List<int>();
            int _counter = 0;
            //Current Index of gridview
            int currentIndex = 0;

            //
            if (txt_Search_Grid.Text.Trim() == "")
            {
                txt_Search_Grid.Text = "Search Value";
                txt_Search_Grid.ForeColor = Color.Gray;
            }

            //
            if (!String.IsNullOrEmpty(_valueSearch) && _valueSearch != "Search Value")
            {
                _listIndex.Clear();

                //Grid Invoice
                if (tabControl.SelectedIndex == 0)
                {
                    foreach (DataGridViewRow row in GridView_Invoice.Rows)
                    {
                        if (row.Cells["Customer_PO"].Value != null)
                        {
                            if (row.Cells["Customer_PO"].Value.ToString().Contains(_valueSearch))
                            {
                                _listIndex.Add(row.Index);
                            }
                        }
                    }

                    _counter = _listIndex.Count;
                    if (_listIndex.Count > 0)
                    {
                        int indexList = 0;
                        int paraIndex = 0;
                        foreach (int rowIndex in _listIndex)
                        {
                            paraIndex = 0;
                            if (GridView_Invoice.SelectedRows.Count > 0)
                            {
                                currentIndex = GridView_Invoice.SelectedRows[0].Index;
                            }
                            //Index nhỏ hơn min của List index
                            if ((currentIndex < rowIndex))
                            {
                                GridView_Invoice.ClearSelection();
                                GridView_Invoice.Rows[rowIndex].Selected = true;
                                GridView_Invoice.Rows[rowIndex].Cells["Part_Description"].Selected = true;
                                GridView_Invoice.FirstDisplayedScrollingRowIndex = rowIndex;
                                txt_Search_Grid.Focus();
                                break;
                            }
                            //Index lớn hơn rowIndex của foreach
                            //Nhưng bé hơn max của List index
                            else if (((currentIndex > rowIndex) || (currentIndex == rowIndex)) && (currentIndex < Find_Bigger_Number(_listIndex, rowIndex, out paraIndex)))
                            {
                                GridView_Invoice.ClearSelection();
                                GridView_Invoice.Rows[paraIndex].Selected = true;
                                GridView_Invoice.Rows[paraIndex].Cells["Part_Description"].Selected = true;
                                GridView_Invoice.FirstDisplayedScrollingRowIndex = paraIndex;
                                txt_Search_Grid.Focus();
                                break;
                            }
                            //Last result
                            else if ((currentIndex == _listIndex[_counter - 1]) || (GridView_Invoice.SelectedRows[0].IsNewRow))
                            {
                                MessageBox.Show("Tìm kiếm hoàn thành!\nCó " + _counter + " kết quả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                GridView_Invoice.ClearSelection();
                                GridView_Invoice.Rows[_listIndex[0]].Selected = true;
                                GridView_Invoice.Rows[_listIndex[0]].Cells["Part_Description"].Selected = true;
                                GridView_Invoice.FirstDisplayedScrollingRowIndex = _listIndex[0];
                                break;
                            }
                            indexList++;

                        }
                    }
                }
                //Grid Packing List
                else if (tabControl.SelectedIndex == 1)
                {
                    foreach (DataGridViewRow row in GridView_PackingList.Rows)
                    {
                        if (row.Cells["Tvc_ItemCode"].Value != null)
                        {
                            if (row.Cells["Tvc_ItemCode"].Value.ToString().Contains(_valueSearch))
                            {
                                _listIndex.Add(row.Index);
                            }
                        }
                    }

                    _counter = _listIndex.Count;
                    if (_counter > 0)
                    {
                        int indexList = 0;
                        foreach (int rowIndex in _listIndex)
                        {
                            int paraIndex = 0;
                            if (GridView_PackingList.SelectedRows.Count > 0)
                            {
                                currentIndex = GridView_PackingList.SelectedRows[0].Index;
                            }
                            //Index nhỏ hơn min của List index
                            if ((currentIndex < rowIndex))
                            {
                                GridView_PackingList.ClearSelection();
                                GridView_PackingList.Rows[rowIndex].Selected = true;
                                GridView_PackingList.Rows[rowIndex].Cells["Packages_No"].Selected = true;
                                GridView_PackingList.FirstDisplayedScrollingRowIndex = rowIndex;
                                txt_Search_Grid.Focus();
                                break;
                            }
                            //Index lớn hơn rowIndex của foreach
                            //Nhưng bé hơn max của List index
                            else if (((currentIndex > rowIndex) || (currentIndex == rowIndex)) && (currentIndex < Find_Bigger_Number(_listIndex, rowIndex, out paraIndex)))
                            {
                                GridView_PackingList.ClearSelection();
                                GridView_PackingList.Rows[paraIndex].Selected = true;
                                GridView_PackingList.Rows[paraIndex].Cells["Packages_No"].Selected = true;
                                GridView_PackingList.FirstDisplayedScrollingRowIndex = paraIndex;
                                txt_Search_Grid.Focus();
                                break;
                            }
                            //Last result
                            else if ((currentIndex == _listIndex[_counter - 1]) || (GridView_PackingList.SelectedRows[0].IsNewRow))
                            {
                                MessageBox.Show("Tìm kiếm hoàn thành!\nCó " + _counter + " kết quả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                GridView_PackingList.ClearSelection();
                                GridView_PackingList.Rows[_listIndex[0]].Selected = true;
                                GridView_PackingList.Rows[_listIndex[0]].Cells["Packages_No"].Selected = true;
                                GridView_PackingList.FirstDisplayedScrollingRowIndex = _listIndex[0];
                                break;
                            }
                            indexList++;
                        }
                    }
                }
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            GridView_PackingList.Sort(GridView_PackingList.Columns["Packages_No"], ListSortDirection.Ascending);
        }

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

        private void btnSort_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btnSort, "Sort dữ liệu tăng dần theo Packages_No");
        }

        private void btn_CoppyRow_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btn_CoppyRow, "Coppy dòng hiện tại thành 1 dòng mới ngay phía dưới.");
        }

        private int Find_Bigger_Number(List<int> list,int number,out int index)
        {
            index = 0;
            foreach (int value in list)
            {
                if (value > number)
                    return index++;
                if (value == number)
                    return ++index;
                else index++;
            }
            return index;
        }

        private void GridView_PackingList_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            decimal _qtyCarton = 0, _qtyPerCarton = 0, _qtyTotal = 0;

            if (e.RowIndex != GridView_PackingList.NewRowIndex) { 
                if (GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Carton"].Value != null
                    && GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Per_Carton"].Value != null)
                {
                    if (decimal.TryParse(GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Carton"].Value.ToString(), out _qtyCarton)
                     && decimal.TryParse(GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Per_Carton"].Value.ToString(), out _qtyPerCarton))
                    {
                        if (_qtyCarton > 0 && _qtyPerCarton > 0)
                        { 
                            _qtyTotal = _qtyCarton * _qtyPerCarton;

                            //Normal data
                            if (radNormal.Checked == true)
                            {
                                GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Total"].Value = _qtyTotal;
                            }

                            //Revise data
                            if (radRevise.Checked == true)
                            {
                                GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Total_Revise"].Value = _qtyTotal;
                            }
                        }
                    }
                }

                //Sum packing List
                btnSumPL_Click(sender, e);
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

            if (String.IsNullOrEmpty(txtShippingNo.Text.Trim()))
            {
                MessageBox.Show("Xin hãy nhập 「Shipping No」!");
                txtShippingNo.Focus();
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

            if (dtpRevenue.Value == DateTime.MinValue)
            {
                MessageBox.Show("Xin hãy chọn「Revenue」!\nShipping thuộc doanh thu của tháng nào!");
                dtpRevenue.Focus();
                return false;
            }

            DataTable _tempTable = new DataTable();
            _tempTable = _shippingDAO.Check_Shipping(txtShippingNo.Text.Trim());

            if (_tempTable.Rows.Count > 0)
            {
                if (MessageBox.Show("Shipping:" + txtShippingNo.Text.Trim() + "đã tồn tại!\nBạn muốn CẬP NHẬT dữ liệu (Chọn OK)!\nBạn muốn THOÁT tiến trình lưu dữ liệu (Chọn Cancel)!", "Cảnh Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return false;
                }
            }

            if (GridView_Invoice.Rows.Count == 1)
            {
                //MessageBox.Show("Grid Invoice đang rỗng!");
                //GridView_Invoice.Focus();
                //return false;
            }
            else
            {
                int rowIndex = 1;
                foreach (DataGridViewRow row in GridView_Invoice.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if (String.IsNullOrEmpty(row.Cells["Tvc_ItemCode"].Value as string))
                        {
                            MessageBox.Show("Tab Invoice, \"TVC ITEM CODE\" dòng số " + rowIndex + " đang rỗng!");
                            tabControl.SelectedIndex = 0;
                            GridView_Invoice.Focus();
                            GridView_Invoice.ClearSelection();
                            GridView_Invoice.CurrentCell = row.Cells["Tvc_ItemCode"];
                            row.Cells["Tvc_ItemCode"].Selected = true;
                            return false;
                        }

                        if (String.IsNullOrEmpty(row.Cells["Customer_PO"].Value as string))
                        {
                            MessageBox.Show("Tab Invoice,\"Customer_PO\" dòng số " + rowIndex + " đang rỗng!");
                            tabControl.SelectedIndex = 0;
                            GridView_Invoice.Focus();
                            GridView_Invoice.ClearSelection();
                            GridView_Invoice.CurrentCell = row.Cells["Customer_PO"];
                            row.Cells["Customer_PO"].Selected = true;
                            return false;
                        }

                        if (Convert.ToDecimal(row.Cells["Quantity"].Value) == 0)
                        {
                            MessageBox.Show("Tab Invoice,\"QUANTITY\" dòng số " + rowIndex + " không thể rỗng hoặc bằng 0!");
                            tabControl.SelectedIndex = 0;
                            GridView_Invoice.Focus();
                            GridView_Invoice.ClearSelection();
                            GridView_Invoice.CurrentCell = row.Cells["Quantity"];
                            row.Cells["Quantity"].Selected = true;
                            return false;
                        }

                        if (Convert.ToDecimal(row.Cells["Quantity"].Value) > Convert.ToDecimal(row.Cells["Balance"].Value))
                        {
                            MessageBox.Show("Tab Invoice, \"QUANTITY\" dòng số " + rowIndex + " không thể lớn hơn \"BALANCE\"!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabControl.SelectedIndex = 0;
                            GridView_Invoice.Focus();
                            GridView_Invoice.ClearSelection();
                            GridView_Invoice.CurrentCell = row.Cells["Quantity"];
                            row.Cells["Quantity"].Selected = true;
                            return false;
                        }

                        DateTime _tempDate;
                        if (row.Cells["Order_Date"].Value != null)
                        {
                            if (!DateTime.TryParse(row.Cells["Order_Date"].Value.ToString(), out _tempDate))
                            {
                                MessageBox.Show("Tab Invoice, Định dạng \"Order_Date\" dòng số " + rowIndex + " không đúng!\nĐịnh dạng đúng \"01/01/2019\"", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                tabControl.SelectedIndex = 0;
                                GridView_Invoice.Focus();
                                GridView_Invoice.ClearSelection();
                                GridView_Invoice.CurrentCell = row.Cells["Order_Date"];
                                row.Cells["Order_Date"].Selected = true;
                                return false;
                            }
                        }

                        if (row.Cells["DueDate_PO"].Value != null)
                        {
                            if (!DateTime.TryParse(row.Cells["DueDate_PO"].Value.ToString(), out _tempDate))
                            {
                                MessageBox.Show("Tab Invoice, Định dạng \"DueDate_PO\" dòng số " + rowIndex + " không đúng!\nĐịnh dạng đúng \"01/01/2019\"", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                tabControl.SelectedIndex = 0;
                                GridView_Invoice.Focus();
                                GridView_Invoice.ClearSelection();
                                GridView_Invoice.CurrentCell = row.Cells["DueDate_PO"];
                                row.Cells["DueDate_PO"].Selected = true;
                                return false;
                            }
                        }

                        string Currency = row.Cells["Unit_Currency"].Value.ToString();

                        if (String.IsNullOrEmpty(Currency))
                        {
                            MessageBox.Show("Tab Invoice,\"UNIT CURRENCY\" dòng số " + rowIndex + " đang rỗng!");
                            tabControl.SelectedIndex = 0;
                            GridView_Invoice.Focus();
                            GridView_Invoice.ClearSelection();
                            GridView_Invoice.CurrentCell = row.Cells["Unit_Currency"];
                            row.Cells["Unit_Currency"].Selected = true;
                            return false;
                        }

                        if (String.IsNullOrEmpty(row.Cells["Customer_PO"].Value as string))
                        {
                            MessageBox.Show("Tab Invoice,\"Customer_PO\" dòng số " + rowIndex + " đang rỗng!");
                            tabControl.SelectedIndex = 0;
                            GridView_Invoice.Focus();
                            GridView_Invoice.ClearSelection();
                            GridView_Invoice.CurrentCell = row.Cells["Customer_PO"];
                            row.Cells["Customer_PO"].Selected = true;
                            return false;
                        }
                        rowIndex++;
                    }
                }
            }

            //Check Total Invocie
            if(txtTotalQuantity.Text != null && txtTotal_Qty.Text != null)
            {
                int _totalQtyInv = 0;
                int _totalQtyPL = 0;
                if (!String.IsNullOrEmpty(txtTotalQuantity.Text))
                {
                    _totalQtyInv = int.Parse(txtTotalQuantity.Text, System.Globalization.NumberStyles.AllowThousands);
                }
                if(!String.IsNullOrEmpty(txtTotal_Qty.Text))
                {
                    _totalQtyPL = int.Parse(txtTotal_Qty.Text, System.Globalization.NumberStyles.AllowThousands);
                }

                if (_totalQtyInv > 0 && _totalQtyPL > 0)
                {
                    if (!Equals(_totalQtyInv, _totalQtyPL))
                    {
                        MessageBox.Show("Số lượng(quantity) của Invoice đang khác với số lượng(quantity) của Packing List!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTotalQuantity.Focus();
                        //return false;
                    }
                }
            }

            return true;
        }

        public Boolean CheckError_Lock()
        {
            //Check Total Invocie
            int _totalQtyInv = 0;
            int _totalQtyPL = 0;
            if (!String.IsNullOrEmpty(txtTotalQuantity.Text))
            {
                _totalQtyInv = int.Parse(txtTotalQuantity.Text, System.Globalization.NumberStyles.AllowThousands);
            }
            if (!String.IsNullOrEmpty(txtTotal_Qty.Text))
            {
                _totalQtyPL = int.Parse(txtTotal_Qty.Text, System.Globalization.NumberStyles.AllowThousands);
            }

            if (_totalQtyInv > 0 && _totalQtyPL > 0)
            {
                if (!Equals(_totalQtyInv, _totalQtyPL))
                {
                    MessageBox.Show("Số lượng(quantity) của Invoice đang khác với số lượng(quantity) của Packing List!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTotalQuantity.Focus();
                    return false;
                }
            }

            return true;
        }

        private void TxtShipTo_CompanyCode_TextChanged(object sender, EventArgs e)
        {
            if (txtShipTo_CompanyCode.Text.Trim().ToUpper() == "TTC")
            {
                this.GridView_PackingList.Columns["Customer_PO"].Visible = true;
            }
            else
            {
                this.GridView_PackingList.Columns["Customer_PO"].Visible = false;
            }
        }

        private void GridView_Invoice_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            btnSumInvoice_Click(sender, e);
        }

        private void GridView_PackingList_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            btnSumPL_Click(sender, e);
        }
    }
}
