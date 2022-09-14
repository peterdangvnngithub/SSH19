using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TAKAKO_ERP_3LAYER.DAL;
using TAKAKO_ERP_3LAYER.DAO;
using System.IO;
using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;
using static TAKAKO_ERP_3LAYER.Common;

namespace TAKAKO_ERP_3LAYER
{
    public partial class Form_Invoice : Form
    {

        public INV_DAO _invDAO;

        public SEARCH_DAO _searchDAO;

        public SYSTEM_DAL _systemDAL;

        public LOG_DAO _logDAO;

        public string _createBy = "";

        public enum EnumRevise
        {
             Normal = 0
            ,Revise = 1
        };

        public enum Number
        {
            Zero = 0
        };

        public Form_Invoice(SYSTEM_DAL _formMainSystemDAL)
        {
            InitializeComponent();

            //Setting no title but have border
            this.ControlBox = false;
            this.Text = String.Empty;

            //Receive common data
            _systemDAL = _formMainSystemDAL;
        }

        private void Form_Invoice_PL_Load(object sender, EventArgs e)
        {
            //Define Init
            _invDAO = new INV_DAO();
            _searchDAO = new SEARCH_DAO();
            _logDAO = new LOG_DAO();
            _listShippingInfo = new List<ShippingInfo>();

            //Setting Init combo box
            SetInit_CboxFreight();
            SetInit_Cbox_Round();

            //Setting Init gridview
            AddColumnGridView(GridView_Invoice);
            AddColumnGridView(GridView_PackingList);

            //Control Enable, disable item
            SettingInit();

            //Control Enable, disable item in grid
            SettingInitGridView();

            //Setting Init date Revenue
            dtpRevenue.Value = DateTime.Now;

            //Setting Init company code
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

        }

        public void SettingInit()
        {
            //Field total Gridview Invoice
            txtTotalQuantity.Enabled = false;
            txtTotalAmount.Enabled = false;

            //Field total Gridview PackingList
            txtTotal_QtyCarton.Enabled = false;
            txtTotal_Qty.Enabled = false;
            txtTotal_NetWeight.Enabled = false;
            txtTotal_GrossWeight.Enabled = false;
        }

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
                    GridView_PackingList.Rows[m].Cells["Tvc_ItemCode"].Value = item.TVC_ItemCode;
                    GridView_PackingList.Rows[m].Cells["Customer_PO"].Value = item.CustomerPO;
                    if (item.BoxQuantity > 0)
                    {
                        GridView_PackingList.Rows[m].Cells["Qty_Carton"].Value = (int)Math.Ceiling(item.Balance / item.BoxQuantity);
                    }
                    GridView_PackingList.Rows[m].Cells["Qty_Per_Carton"].Value = item.BoxQuantity;
                    GridView_PackingList.Rows[m].Cells["Qty_Total"].Value = item.Balance;
                    GridView_PackingList.Rows[m].Cells["Qty_Total_Revise"].Value = item.Balance;
                    GridView_PackingList.Rows[m].Cells["Net_Weight"].Value = item.Weight;
                    GridView_PackingList.Rows[m].Cells["Net_Weight_Total"].Value = item.Weight * item.Balance;
                    GridView_PackingList.Rows[m].Cells["Gross_Weight"].Value = 0;
                    GridView_PackingList.Rows[m].Cells["Lot_No"].Value = "NONE-LOT-NO.";
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
        /// Setting Init combobox Round
        /// </summary>
        public void SetInit_Cbox_Round()
        {
            cbBox_Round.DisplayMember = "Text";
            cbBox_Round.ValueMember = "Value";

            var items = new[] {
                new { Text = "Do nothing", Value = 0 },
                new { Text = "Round Up", Value = 1 },
                new { Text = "Round Down", Value = 2 },
            };

            cbBox_Round.DataSource = items;
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
                    else if (control is ListBox)
                        (control as ListBox).Items.Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);

            //
            cb_Freight.SelectedIndex = 0;
            //
            cbBox_Round.Enabled = true;
            cbBox_Round.SelectedIndex = 0;
            //
            listBox_ShippingNo.Enabled = true;
            //
            txtInvoiceNo.Enabled = true;

            //Clear Gridview Invoice
            GridView_Invoice.Rows.Clear();

            //Clear Gridview PL
            GridView_PackingList.Rows.Clear();

            dtpDateCreateInv.Focus();

            tabControl.SelectedIndex = 0;

            txtTotalQuantity.Text = "0";
            txtTotalAmount.Text = "0";
            txtTotal_QtyCarton.Text = "0";
            txtTotal_Qty.Text = "0";
            txtTotal_NetWeight.Text = "0";
            txtTotal_GrossWeight.Text = "0";
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
            if (radNormal.Checked == true)
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

            if (radNormal.Checked == true)
            {
                //Sum total quantity
                txtTotal_Qty.Text = FormatCommas(Convert.ToDecimal(Sum_Total(GridView_PackingList, "Qty_Total")));
            } else if(radRevise.Checked == true)
            {
                //Sum total quantity revise
                txtTotal_Qty.Text = FormatCommas(Convert.ToDecimal(Sum_Total(GridView_PackingList, "Qty_Total_Revise")));
            }

            //SUm total Net Weight
            txtTotal_NetWeight.Text = FormatCommas(Convert.ToDecimal(Sum_Total(GridView_PackingList, "Net_Weight_Total")));

            //Sum total Grosss Weight
            txtTotal_GrossWeight.Text = FormatCommas(Convert.ToDecimal(Sum_Total(GridView_PackingList, "Gross_Weight")));
        }

        #region Search
        private void btnSearch_ShippingNo_Click(object sender, EventArgs e)
        {
            _listShippingInfo.Clear();

            Form_Search_ShippingNo _formSearchShipping = new Form_Search_ShippingNo("btnSearchSingle_ShippingNo");
            _formSearchShipping.StartPosition = FormStartPosition.CenterParent;
            _formSearchShipping.ShowDialog();

            if (_listShippingInfo.Count > 0)
            {
                listBox_ShippingNo.Items.Clear();
                foreach(ShippingInfo shipping in _listShippingInfo)
                {
                    listBox_ShippingNo.Items.Add(shipping.ShippingNo);
                }
                txtInvoiceNo.Focus();

                //
                GetList_Data_Shipping();

                //Sum Grid Invoice
                btnSumInvoice_Click(sender, e);

                //Sum Grid Packing List
                btnSumPL_Click(sender, e);
            } else
            {
                listBox_ShippingNo.Focus();
            }
        }

        private void btnSearch_Inv_Click(object sender, EventArgs e)
        {
            Form_Search _formSearch = new Form_Search("btnSearch_Inv", this.Name);
            _formSearch.StartPosition = FormStartPosition.CenterParent;
            _formSearch.ShowDialog();

            if(!String.IsNullOrEmpty(_formSearch._InvoiceInfo.Shipping_No))
            {
                ClearData();
                txtInvoiceNo.Text = _formSearch._InvoiceInfo.InvoiceNo;
                string[] listShippingNo = _formSearch._InvoiceInfo.Shipping_No.Split(',').ToArray();

                //Load Invoice
                btn_SearchInvoice_Click(sender, e);

                listBox_ShippingNo.Items.Clear();
                foreach (var shippingNo in listShippingNo)
                {
                    listBox_ShippingNo.Items.Add(shippingNo);
                }
                dtpDateCreateInv.Value = _formSearch._InvoiceInfo.DateCreate;
                if (_formSearch._InvoiceInfo.LockStatus.ToUpper() == "NORMAL")
                {
                    radNormal.Checked = true;
                } else if (_formSearch._InvoiceInfo.LockStatus.ToUpper() == "REVISE")
                {
                    radRevise.Checked = true;
                }

                //Setting Init
                SettingInitGridView();
            } else
            {
                txtInvoiceNo.Focus();
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
                if (String.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                {
                    txtInvoiceNo.Text = _formSearch._companyInfo.InvoiceFormat.Replace("yyMMdd", dtpDateCreateInv.Value.ToString("yyMMdd"));
                    txtInvoiceNo.Focus();
                }
                else
                {
                    txtShipTo_CompanyCode.Focus();
                }
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

        private void picBox_BackToMain_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.picBox_BackToMain, "Trở về màn hình chính");
        }

        private void Form_Invoice_PL_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void picBox_Close_Click(object sender, EventArgs e)
        {
            string exitMessageText = "Bạn muốn thoát chương trình?";
            string exitCaption = "Xác nhận";
            MessageBoxButtons button = MessageBoxButtons.YesNo;
            DialogResult res = MessageBox.Show(exitMessageText, exitCaption, button, MessageBoxIcon.Question);
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
                System.Windows.Forms.DataGridViewTextBoxColumn InvShippingNo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
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

                this.GridView_Invoice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
                    {
                         InvShippingNo_col
                        ,InvCustomerCode_col
                        ,InvItem_Name_col
                        ,InvCus_ItemCode_col
                        ,InvTVC_ItemCode_col
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

                //SHIPPING NO
                InvShippingNo_col.HeaderText = "SHIPPING NO";
                InvShippingNo_col.DataPropertyName = "SHIPPING_NO";
                InvShippingNo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InvShippingNo_col.Name = "Shipping_No";
                InvShippingNo_col.Width = 120;

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

                //ORDER DATE
                this.GridView_Invoice.Columns["DueDate_PO"].Visible = false;

                ////BALANCE
                //this.GridView_Invoice.Columns["Balance"].Visible = false;
                this.GridView_Invoice.Columns["Balance"].DefaultCellStyle.Format = "#,##0.##";
                this.GridView_Invoice.Columns["Balance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                //QUANTITY
                this.GridView_Invoice.Columns["Quantity"].DefaultCellStyle.Format = "#,##0.##";
                this.GridView_Invoice.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //QUANTITY REVISE
                this.GridView_Invoice.Columns["Quantity_Revise"].DefaultCellStyle.Format = "#,##0.##";
                this.GridView_Invoice.Columns["Quantity_Revise"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
                System.Windows.Forms.DataGridViewTextBoxColumn PlShipping_No_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn PlCustomer_Code_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn PlPackagesNo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn PlItemCode_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
                         PlShipping_No_col
                        ,PlCustomer_Code_col
                        ,PlPackagesNo_col
                        ,PlItemCode_col
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

                //SHIPPING NO
                PlShipping_No_col.HeaderText = "SHIPPING NO";
                PlShipping_No_col.DataPropertyName = "SHIPPING_NO";
                PlShipping_No_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PlShipping_No_col.Name = "Shipping_No";
                PlShipping_No_col.Width = 120;

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

                //ITEM CODE
                PlItemCode_col.HeaderText = "CUSTOMER ITEM CODE";
                PlItemCode_col.DataPropertyName = "TVC_ITEM_CODE";
                PlItemCode_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PlItemCode_col.Name = "TVC_ItemCode";
                PlItemCode_col.Width = 150;

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
                PlNetWeightTotal_col.Width = 90;

                //Price JPY
                PlGrossWeight_col.HeaderText = "G / W (KG)";
                PlGrossWeight_col.DataPropertyName = "GROSS_WEIGHT";
                PlGrossWeight_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PlGrossWeight_col.Name = "Gross_Weight";
                PlGrossWeight_col.Width = 90;

                //LOT NO
                PlLotNo_col.HeaderText = "LOT NO";
                PlLotNo_col.DataPropertyName = "LOT_NO";
                PlLotNo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PlLotNo_col.Name = "Lot_No";
                PlLotNo_col.Width = 134;

                //Setting
                this.GridView_PackingList.DefaultCellStyle.Font = new Font("Arial", 10.25F, GraphicsUnit.Pixel);

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
                dtpDateCreateInv.Enabled = true;
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
                cb_Freight.Enabled = true;
                txtVessel.ReadOnly = false;
                txtPortLoading.ReadOnly = false;
                txtPortDestination.ReadOnly = false;

                txtPriceCondition.ReadOnly = false;
                txtPaymentTerm.ReadOnly = false;

                //
                if (String.Equals(_createBy, _systemDAL.UserName) && !String.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                {
                    btnDelete.Enabled = true;
                } else
                {
                    btnDelete.Enabled = false;
                }

                btnSearch_Inv.Enabled = true;
                btnSearch_IssuedTo.Enabled = true;
                btnSearch_ShipTo.Enabled = true;
                btnSearch_PortLoading.Enabled = true;
                btnSearch_PortDestination.Enabled = true;
                btnSearch_TradeCondition.Enabled = true;
                btnSearch_PaymentTerm.Enabled = true;
                btnSearch_Inv.Enabled = true;

                //---------------- Gridview Invoice ----------------//
                GridView_Invoice.Columns["Quantity_Revise"].Visible = false;
                GridView_Invoice.Columns["Quantity_Revise"].DefaultCellStyle.BackColor = Color.Gray;
                //
                GridView_Invoice.Columns["Order_Price_Revise"].Visible = false;
                GridView_Invoice.Columns["Order_Price_Revise"].DefaultCellStyle.BackColor = Color.Gray;
                //Gridview PackingList
                GridView_PackingList.Columns["Qty_Total_Revise"].Visible = false;
                GridView_PackingList.Columns["Qty_Total_Revise"].DefaultCellStyle.BackColor = Color.Gray;

                //---- Enable ----//
                GridView_Invoice.Columns["Shipping_No"].ReadOnly = true;
                GridView_Invoice.Columns["Shipping_No"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_Invoice.Columns["Customer_Code"].ReadOnly = true;
                GridView_Invoice.Columns["Customer_Code"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_Invoice.Columns["Part_Description"].ReadOnly = true;
                GridView_Invoice.Columns["Part_Description"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_Invoice.Columns["Cus_ItemCode"].ReadOnly = true;
                GridView_Invoice.Columns["Cus_ItemCode"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_Invoice.Columns["Tvc_ItemCode"].ReadOnly = true;
                GridView_Invoice.Columns["Tvc_ItemCode"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_Invoice.Columns["Customer_PO"].ReadOnly = true;
                GridView_Invoice.Columns["Customer_PO"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_Invoice.Columns["ThirdParty_PO"].ReadOnly = true;
                GridView_Invoice.Columns["ThirdParty_PO"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_Invoice.Columns["Order_Date"].ReadOnly = true;
                GridView_Invoice.Columns["Order_Date"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_Invoice.Columns["Quantity"].ReadOnly = true;
                GridView_Invoice.Columns["Quantity"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_Invoice.Columns["Balance"].ReadOnly = true;
                GridView_Invoice.Columns["Balance"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_Invoice.Columns["Unit_Currency"].ReadOnly = true;
                GridView_Invoice.Columns["Unit_Currency"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_Invoice.Columns["Order_Price"].ReadOnly = true;
                GridView_Invoice.Columns["Order_Price"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_Invoice.Columns["Global_Price"].ReadOnly = true;
                GridView_Invoice.Columns["Global_Price"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_Invoice.Columns["Amount_Jpy"].ReadOnly = true;
                GridView_Invoice.Columns["Amount_Jpy"].DefaultCellStyle.BackColor = Color.Gray;

                //---------------- Gridview PackingList ----------------//
                //---- Enable ----//
                GridView_PackingList.Columns["Shipping_No"].ReadOnly = true;
                GridView_PackingList.Columns["Shipping_No"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_PackingList.Columns["Customer_Code"].ReadOnly = true;
                GridView_PackingList.Columns["Customer_Code"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_PackingList.Columns["Packages_No"].ReadOnly = false;
                GridView_PackingList.Columns["Packages_No"].DefaultCellStyle.BackColor = Color.White;

                GridView_PackingList.Columns["TVC_ItemCode"].ReadOnly = true;
                GridView_PackingList.Columns["TVC_ItemCode"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_PackingList.Columns["Customer_PO"].ReadOnly = true;
                GridView_PackingList.Columns["Customer_PO"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_PackingList.Columns["Qty_Carton"].ReadOnly = true;
                GridView_PackingList.Columns["Qty_Carton"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_PackingList.Columns["Qty_Per_Carton"].ReadOnly = false;
                GridView_PackingList.Columns["Qty_Per_Carton"].DefaultCellStyle.BackColor = Color.White;

                GridView_PackingList.Columns["Qty_Total"].ReadOnly = false;
                GridView_PackingList.Columns["Qty_Total"].DefaultCellStyle.BackColor = Color.White;

                GridView_PackingList.Columns["Net_Weight_Total"].ReadOnly = true;
                GridView_PackingList.Columns["Net_Weight_Total"].DefaultCellStyle.BackColor = Color.Gray;
            }
            else if(radRevise.Checked == true)
            {
                //--------------------- Header ---------------------//
                dtpDateCreateInv.Enabled = false;
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
                cb_Freight.Enabled = false;
                txtVessel.ReadOnly = true;
                txtPortLoading.ReadOnly = true;
                txtPortDestination.ReadOnly = true;

                txtPriceCondition.ReadOnly = true;
                txtPaymentTerm.ReadOnly = true;

                if (String.Equals(_createBy, _systemDAL.UserName) && String.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                {
                    btnDelete.Enabled = true;
                }
                else
                {
                    btnDelete.Enabled = false;
                }

                btnSearch_Inv.Enabled = false;
                btnSearch_IssuedTo.Enabled = false;
                btnSearch_ShipTo.Enabled = false;
                btnSearch_PortLoading.Enabled = false;
                btnSearch_PortDestination.Enabled = false;
                btnSearch_TradeCondition.Enabled = false;
                btnSearch_PaymentTerm.Enabled = false;
                btnSearch_ShippingNo.Enabled = false;
                btnSearch_Inv.Enabled = true;

                //---------------- Gridview Invoice ----------------//
                //---- Enable ----//
                GridView_Invoice.Columns["Quantity_Revise"].Visible = true;
                GridView_Invoice.Columns["Quantity_Revise"].DefaultCellStyle.BackColor = Color.White;
                GridView_Invoice.Columns["Order_Price_Revise"].Visible = true;
                GridView_Invoice.Columns["Order_Price_Revise"].DefaultCellStyle.BackColor = Color.White;
                //Gridview PackingList
                GridView_PackingList.Columns["Qty_Total_Revise"].Visible = true;
                GridView_PackingList.Columns["Qty_Total_Revise"].DefaultCellStyle.BackColor = Color.White;

                //---- Disable ----//
                GridView_Invoice.Columns["Customer_Code"].ReadOnly = true;
                GridView_Invoice.Columns["Customer_Code"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_Invoice.Columns["Part_Description"].ReadOnly = true;
                GridView_Invoice.Columns["Part_Description"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_Invoice.Columns["Tvc_ItemCode"].ReadOnly = true;
                GridView_Invoice.Columns["Tvc_ItemCode"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_Invoice.Columns["Customer_PO"].ReadOnly = true;
                GridView_Invoice.Columns["Customer_PO"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_Invoice.Columns["Order_Date"].ReadOnly = true;
                GridView_Invoice.Columns["Order_Date"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_Invoice.Columns["Quantity"].ReadOnly = true;
                GridView_Invoice.Columns["Quantity"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_Invoice.Columns["Unit_Currency"].ReadOnly = true;
                GridView_Invoice.Columns["Unit_Currency"].DefaultCellStyle.BackColor = Color.Gray;

                //GridView_Invoice.Columns["USD_Rate"].ReadOnly = true;
                //GridView_Invoice.Columns["USD_Rate"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_Invoice.Columns["Order_Price"].ReadOnly = true;
                GridView_Invoice.Columns["Order_Price"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_Invoice.Columns["Global_Price"].ReadOnly = true;
                GridView_Invoice.Columns["Global_Price"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_Invoice.Columns["Amount_Jpy"].ReadOnly = true;
                GridView_Invoice.Columns["Amount_Jpy"].DefaultCellStyle.BackColor = Color.Gray;

                //---------------- Gridview PackingList ----------------//
                GridView_PackingList.Columns["Customer_Code"].ReadOnly = true;
                GridView_PackingList.Columns["Customer_Code"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_PackingList.Columns["Packages_No"].ReadOnly = true;
                GridView_PackingList.Columns["Packages_No"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_PackingList.Columns["TVC_ItemCode"].ReadOnly = true;
                GridView_PackingList.Columns["TVC_ItemCode"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_PackingList.Columns["Customer_PO"].ReadOnly = true;
                GridView_PackingList.Columns["Customer_PO"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_PackingList.Columns["Qty_Carton"].ReadOnly = true;
                GridView_PackingList.Columns["Qty_Carton"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_PackingList.Columns["Qty_Per_Carton"].ReadOnly = true;
                GridView_PackingList.Columns["Qty_Per_Carton"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_PackingList.Columns["Qty_Total"].ReadOnly = true;
                GridView_PackingList.Columns["Qty_Total"].DefaultCellStyle.BackColor = Color.Gray;

                GridView_PackingList.Columns["Net_Weight_Total"].ReadOnly = true;
                GridView_PackingList.Columns["Net_Weight_Total"].DefaultCellStyle.BackColor = Color.Gray;
            }
        }
        private void GridView_Invoice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int columnIndex = 0;
            int rowIndex = 0;
            String _unitCurrency = "";
            DateTime _dateCreateInvoice = dtpDateCreateInv.Value;

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
                    Form_Search_PO _formSearch = new Form_Search_PO(_systemDAL,"btnSearch_ItemCode", txtIssuedTo_CompanyCode.Text, _unitCurrency, _dateCreateInvoice);
                    _formSearch.StartPosition = FormStartPosition.CenterParent;
                    _formSearch.ShowDialog();

                    //
                    GetSelectedItem(_listItemCodeInfo);

                    //Sum Invoice
                    btnSumInvoice_Click(sender, e);

                    //Sum packing List
                    btnSumPL_Click(sender, e);
                }
            }
        }
        private void GridView_Invoice_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            decimal quantity, quantity_revise, price, price_revise, price_compare, amount, USD_Rate, balance;
            decimal _quantityTotal, _quantityTotal_revise, _quantityPerCarton, _quantityCarton;

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
                            if(quantity > balance)
                            {
                                MessageBox.Show("Số lượng còn lại: " + balance + ".\nSố lượng nhập: " + quantity,"Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            }
                        }

                        if (GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Total"].Value != null
                         && GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Per_Carton"].Value != null)
                        {
                            if (decimal.TryParse(GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Total"].Value.ToString(), out _quantityTotal)
                             && decimal.TryParse(GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Per_Carton"].Value.ToString(), out _quantityPerCarton))
                            {
                                if (_quantityPerCarton != 0)
                                {
                                    _quantityCarton = _quantityTotal / _quantityPerCarton;
                                    GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Carton"].Value = (int)Math.Ceiling(_quantityCarton);
                                }
                            }
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
                if (Convert.ToDecimal(GridView_Invoice.Rows[e.RowIndex].Cells["Quantity"].Value) == 0)
                {
                    GridView_Invoice.Rows[e.RowIndex].Cells["Quantity"].Style.BackColor = Color.Red;
                }
                else
                {
                    GridView_Invoice.Rows[e.RowIndex].Cells["Quantity"].Style.BackColor = Color.White;
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
                        if (GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Total_Revise"].Value != null
                         && GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Per_Carton"].Value != null)
                        {
                            if (decimal.TryParse(GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Total_Revise"].Value.ToString(), out _quantityTotal_revise)
                             && decimal.TryParse(GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Per_Carton"].Value.ToString(), out _quantityPerCarton))
                            {
                                if (_quantityPerCarton != 0)
                                {
                                    _quantityCarton = _quantityTotal_revise / _quantityPerCarton;
                                    GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Carton"].Value = (int)Math.Ceiling(_quantityCarton);
                                }
                            }
                        }

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
        private void GridView_Invoice_KeyDown(object sender, KeyEventArgs e)
        {
            int _RowIndex = 0;
            int _ColumnIndex = 0;

            if (e.KeyCode == Keys.Tab)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }

            if(e.Control && e.KeyCode == Keys.C)
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

        }
        private void GridView_PackingList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            decimal _quantityTotal, _quantityTotal_revise, _quantityPerCarton, _quantityCarton,_netWeight;

            //Normal data
            if (radNormal.Checked == true)
            {
                if (GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Total"].Value != null
                    && GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Per_Carton"].Value != null)
                {
                    if (decimal.TryParse(GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Total"].Value.ToString(), out _quantityTotal)
                     && decimal.TryParse(GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Per_Carton"].Value.ToString(), out _quantityPerCarton))
                    {
                        //Calculator quantity carton
                        if (_quantityPerCarton != 0)
                        {
                            _quantityCarton = _quantityTotal / _quantityPerCarton;
                            GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Carton"].Value = (int)Math.Ceiling(_quantityCarton);
                        }
                        //Set new weight total
                        if (GridView_PackingList.Rows[e.RowIndex].Cells["Net_Weight"].Value != null) { 
                            decimal.TryParse(GridView_PackingList.Rows[e.RowIndex].Cells["Net_Weight"].Value.ToString(), out _netWeight);
                            GridView_PackingList.Rows[e.RowIndex].Cells["Net_Weight_Total"].Value = _quantityTotal* _netWeight;
                        }
                    }
                }
            }

            //Revise data
            if (radRevise.Checked == true)
            {
                if (GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Total_Revise"].Value != null
                 && GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Per_Carton"].Value != null)
                {
                    if (decimal.TryParse(GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Total_Revise"].Value.ToString(), out _quantityTotal_revise)
                     && decimal.TryParse(GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Per_Carton"].Value.ToString(), out _quantityPerCarton))
                    {
                        if (_quantityPerCarton != 0)
                        {
                            _quantityCarton = _quantityTotal_revise / _quantityPerCarton;
                            GridView_PackingList.Rows[e.RowIndex].Cells["Qty_Carton"].Value = (int)Math.Ceiling(_quantityCarton);
                        }
                        //Set new weight total
                        if (GridView_PackingList.Rows[e.RowIndex].Cells["Net_Weight"].Value != null)
                        {
                            decimal.TryParse(GridView_PackingList.Rows[e.RowIndex].Cells["Net_Weight"].Value.ToString(), out _netWeight);
                            GridView_PackingList.Rows[e.RowIndex].Cells["Net_Weight_Total"].Value = _quantityTotal_revise * _netWeight;
                        }
                    }
                }
            }

            //Sum packing List
            btnSumPL_Click(sender, e);
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
        }
        #endregion

        #region KeyDown_Event
        private void txtInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Load_InvoiceSearch();
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
            else if (e.KeyCode == Keys.F5)
            {
                btnSearch_Inv_Click(sender, e);
            }
        }

        private void txtIssuedTo_CompanyCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnSearch_IssuedTo_Click(sender, e);
            }
        }

        private void txtShipTo_CompanyCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnSearch_ShipTo_Click(sender, e);
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

        public Boolean CheckError()
        {
            if (listBox_ShippingNo.Items.Count == 0)
            {
                MessageBox.Show("Xin hãy nhập 「Shipping No」!","Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
                listBox_ShippingNo.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtInvoiceNo.Text))
            {
                MessageBox.Show("Xin hãy nhập 「Invoice No」!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtInvoiceNo.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtIssuedTo_CompanyName.Text))
            {
                MessageBox.Show("Xin hãy nhập 「Issued To」!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtIssuedTo_CompanyName.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtShipTo_CompanyName.Text))
            {
                MessageBox.Show("Xin hãy nhập 「Ship To」!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtShipTo_CompanyName.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtShipVia.Text))
            {
                MessageBox.Show("Xin hãy nhập 「Ship Via」!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtShipVia.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtPortLoading.Text))
            {
                MessageBox.Show("Xin hãy nhập 「Port Loading」!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPortLoading.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtPortDestination.Text))
            {
                MessageBox.Show("Xin hãy nhập 「Port Destination」!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPortDestination.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtPriceCondition.Text))
            {
                MessageBox.Show("Xin hãy nhập 「Trade Condition」!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPriceCondition.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtPaymentTerm.Text))
            {
                MessageBox.Show("Xin hãy nhập 「Payment Term」!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                txtPaymentTerm.Focus();
                return false;
            }

            DataTable _tempTable = new DataTable();
            _tempTable = _invDAO.CheckInv(txtInvoiceNo.Text);

            if (_tempTable.Rows.Count > 0)
            {
                if (MessageBox.Show("Invoice đã tồn tại!\nBạn muốn CẬP NHẬT dữ liệu (Chọn OK)!\nBạn muốn THOÁT tiến trình (Chọn Cancel)!", "Cảnh Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Cancel)
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
                            MessageBox.Show("Tab Invoice, \"TVC ITEM CODE\" dòng số " + rowIndex + " đang rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabControl.SelectedIndex = 0;
                            GridView_Invoice.Focus();
                            GridView_Invoice.ClearSelection();
                            GridView_Invoice.CurrentCell = row.Cells["Tvc_ItemCode"];
                            row.Cells["Tvc_ItemCode"].Selected = true;
                            return false;
                        }

                        if (String.IsNullOrEmpty(row.Cells["Customer_PO"].Value as string))
                        {
                            MessageBox.Show("Tab Invoice, \"Customer_PO\" dòng số " + rowIndex + " đang rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabControl.SelectedIndex = 0;
                            GridView_Invoice.Focus();
                            GridView_Invoice.ClearSelection();
                            GridView_Invoice.CurrentCell = row.Cells["Customer_PO"];
                            row.Cells["Customer_PO"].Selected = true;
                            return false;
                        }

                        if (Convert.ToDecimal(row.Cells["Quantity"].Value) == 0)
                        {
                            MessageBox.Show("Tab Invoice, \"QUANTITY\" dòng số " + rowIndex + " không thể rỗng hoặc bằng 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabControl.SelectedIndex = 0;
                            GridView_Invoice.Focus();
                            GridView_Invoice.ClearSelection();
                            GridView_Invoice.CurrentCell = row.Cells["Quantity"];
                            row.Cells["Quantity"].Selected = true;
                            return false;
                        }

                        if (Convert.ToDecimal(row.Cells["Quantity"].Value) > Convert.ToDecimal(row.Cells["Balance"].Value))
                        {
                            MessageBox.Show("Tab Invoice, \"QUANTITY\" dòng số " + rowIndex + " không thể lớn hơn \"BALANCE\"!","Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            tabControl.SelectedIndex = 0;
                            GridView_Invoice.Focus();
                            GridView_Invoice.ClearSelection();
                            GridView_Invoice.CurrentCell = row.Cells["Quantity"];
                            row.Cells["Quantity"].Selected = true;
                            return false;
                        }

                        string Currency = row.Cells["Unit_Currency"].Value.ToString();

                        if (String.IsNullOrEmpty(Currency))
                        {
                            MessageBox.Show("Tab Invoice, \"UNIT CURRENCY\" dòng số " + rowIndex + " đang rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabControl.SelectedIndex = 0;
                            GridView_Invoice.Focus();
                            GridView_Invoice.ClearSelection();
                            GridView_Invoice.CurrentCell = row.Cells["Unit_Currency"];
                            row.Cells["Unit_Currency"].Selected = true;
                            return false;
                        }

                        if (!Equals(Currency.ToUpper(),"JPY") && (Convert.ToDecimal(row.Cells["USD_Rate"].Value) <= 0))
                        {
                            MessageBox.Show("Tab Invoice, \"Ex Rate\" dòng số " + rowIndex + " không thể bằng 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabControl.SelectedIndex = 0;
                            GridView_Invoice.Focus();
                            GridView_Invoice.ClearSelection();
                            GridView_Invoice.CurrentCell = row.Cells["USD_Rate"];
                            row.Cells["USD_Rate"].Selected = true;
                            return false;
                        }

                        if (String.IsNullOrEmpty(row.Cells["Customer_PO"].Value as string))
                        {
                            MessageBox.Show("Tab Invoice, \"Customer_PO\" dòng số " + rowIndex + " đang rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabControl.SelectedIndex = 0;
                            GridView_Invoice.Focus();
                            GridView_Invoice.ClearSelection();
                            GridView_Invoice.CurrentCell = row.Cells["Customer_PO"];
                            row.Cells["Customer_PO"].Selected = true;
                            return false;
                        }

                        //if (radNormal.Checked == true && Convert.ToDecimal(row.Cells["Amount_Jpy"].Value) == 0)
                        //{
                        //    MessageBox.Show("Tab Invoice,\"AMOUNT (JPY)\" Picked " + rowIndex + " không thể rỗng hoặc bằng 0!","Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        //    tabControl.SelectedIndex = 0;
                        //    GridView_Invoice.Focus();
                        //    GridView_Invoice.ClearSelection();
                        //    GridView_Invoice.CurrentCell = row.Cells["Amount_Jpy"];
                        //    row.Cells["Amount_Jpy"].Selected = true;
                        //    return false;
                        //}
                        rowIndex++;
                    }
                }
            }

            if (GridView_PackingList.Rows.Count == 1)
            {
                //MessageBox.Show("Grid Packing List đang rỗng!");
                //tabControl.SelectedIndex = 1;
                //GridView_PackingList.Focus();
                //return false;
            }
            else
            {
                int rowIndex = 1;
                foreach (DataGridViewRow row in GridView_PackingList.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if (row.Cells["Packages_No"].Value != null)
                        {
                            if (row.Cells["Packages_No"].Value.ToString() == "0")
                            {
                                MessageBox.Show("Gridview Packing List,\"PACKAGES NO\" dòng số " + rowIndex + " không thể rỗng hoặc bằng 0!");
                                tabControl.SelectedIndex = 1;
                                GridView_PackingList.Focus();
                                GridView_PackingList.ClearSelection();
                                GridView_PackingList.CurrentCell = row.Cells["Packages_No"];
                                row.Cells["Packages_No"].Selected = true;
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Gridview Packing List,\"PACKAGES NO\" dòng số " + rowIndex + " đang rỗng!");
                            tabControl.SelectedIndex = 1;
                            GridView_PackingList.Focus();
                            GridView_PackingList.ClearSelection();
                            GridView_PackingList.CurrentCell = row.Cells["Packages_No"];
                            row.Cells["Packages_No"].Selected = true;
                            return false;
                        }

                        if (String.IsNullOrEmpty(row.Cells["Tvc_ItemCode"].Value as string))
                        {
                            MessageBox.Show("Gridview Packing List,\"TVC ITEM CODE\" dòng số " + rowIndex + " không thể rỗng!");
                            row.Cells["Tvc_ItemCode"].Selected = true;
                            GridView_PackingList.Focus();
                            GridView_PackingList.ClearSelection();
                            GridView_PackingList.CurrentCell = row.Cells[""];
                            row.Cells["Tvc_ItemCode"].Selected = true;
                            return false;
                        }

                        if (radNormal.Checked == true && Convert.ToDecimal(row.Cells["Qty_Carton"].Value) == 0)
                        {
                            MessageBox.Show("Gridview Packing List,\"QUANTITY OF CARTON\" dòng số " + rowIndex + " không thể rỗng hoặc bằng 0!");
                            tabControl.SelectedIndex = 1;
                            GridView_PackingList.Focus();
                            GridView_PackingList.ClearSelection();
                            GridView_PackingList.CurrentCell = row.Cells["Qty_Carton"];
                            row.Cells["Qty_Carton"].Selected = true;
                            return false;
                        }

                        rowIndex++;
                    }
                }

                //Check Total Invocie
                int _totalQtyInv = int.Parse(txtTotalQuantity.Text, System.Globalization.NumberStyles.AllowThousands);
                if (String.IsNullOrEmpty(txtTotalQuantity.Text) || _totalQtyInv == 0)
                {
                    MessageBox.Show("Xin hãy nhập 「Total Quantity of Invoice」!");
                    txtTotalQuantity.Focus();
                    return false;
                }

                decimal _totalAmount = decimal.Parse(txtTotalAmount.Text, System.Globalization.NumberStyles.Currency);
                if (String.IsNullOrEmpty(txtTotalAmount.Text) || _totalAmount == 0)
                {
                    MessageBox.Show("Xin hãy nhập 「Total Amount of Invoice」!");
                    txtTotalAmount.Focus();
                    return false;
                }

                int _totalQtyPL = int.Parse(txtTotal_Qty.Text, System.Globalization.NumberStyles.AllowThousands);
                if (String.IsNullOrEmpty(txtTotal_Qty.Text) || _totalQtyPL == 0)
                {
                    MessageBox.Show("Xin hãy nhập 「Total Quantity of Packing List」!");
                    txtTotal_Qty.Focus();
                    return false;
                }

                if (!Equals(_totalQtyInv, _totalQtyPL))
                {
                    MessageBox.Show("Tổng quantity của Invoice đang khác với tổng quantity của Packing List!");
                    txtTotalQuantity.Focus();
                    return false;
                }
            }

            return true;
        }

        private void IsNewData(string _invoiceNo, out Boolean IsNewData, out Boolean LockStatus, out int Version)
        {
            IsNewData = true;
            LockStatus = false;
            Version = 0;
            try
            {
                DataTable _tempTable = new DataTable();
                _tempTable = _searchDAO.GetInfoInvoice(_invoiceNo);

                if (_tempTable.Rows.Count > 0)
                {
                    IsNewData = false;

                    foreach (DataRow dr in _tempTable.Rows)
                    {
                        if (dr["LOCK_STATUS"].ToString() == "0")
                        { 
                            LockStatus = true;
                        } else if (dr["LOCK_STATUS"].ToString() == "1")
                        {
                            LockStatus = false;
                        }
                        Version = Convert.ToInt32(dr["REVISE_VERSION"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }

        #region Event
        private void radNormal_CheckedChanged(object sender, EventArgs e)
        {
            SettingInitGridView();
        }

        private void radRevise_CheckedChanged(object sender, EventArgs e)
        {
            SettingInitGridView();
        }

        private void btnSave_Data_Click(object sender, EventArgs e)
        {
            Boolean IsNew;
            Boolean IsLock;
            int Version = 0;
            if ((MessageBox.Show("Bạn muốn lưu dữ liệu?", "Xác nhận"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Question
                , MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                if (CheckError() == true)
                {
                    string _invoiceNo = txtInvoiceNo.Text.Trim();
                    string _shippingNo = "";
                    DataTable dtInvoiceMS = new DataTable();
                    DataTable dtInvoiceDetail = new DataTable();
                    DataTable dtPackingListDetail = new DataTable();
                    DataTable dtShippingNo = new DataTable();

                    #region define dtInvoiceMS
                    //Define dtInvoiceMS 
                    //LOCK STATUS (0 : NORMAL, 1 : REVISE)
                    dtInvoiceMS.Columns.Add("LockStatus");
                    dtInvoiceMS.Columns["LockStatus"].DataType = typeof(Int32);
                    //COMPANY CODE
                    dtInvoiceMS.Columns.Add("CompanyCode");
                    //SHIPPING NO
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
                    //TVC ITEM CODE
                    dtPackingListDetail.Columns.Add("Tvc_ItemCode");
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

                    #region dtShippingNo
                    dtShippingNo.Columns.Add("ShippingNo");
                    #endregion

                    //Get Info Invoice
                    IsNewData(_invoiceNo, out IsNew, out IsLock, out Version);

                    //Mode: Add new
                    if (IsNew)
                    {
                        if (radRevise.Checked == true)
                        {
                            MessageBox.Show("Invoice lưu xảy ra lỗi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtInvoiceNo.Focus();
                            return;
                        }
                    }

                    //Check listbox is not null
                    if (listBox_ShippingNo.Items.Count > 0)
                    {
                        //Add shipping No from listbox to datatable pass to Stored Procedure
                        foreach (var ListBoxItem in listBox_ShippingNo.Items)
                        {
                            _shippingNo += ListBoxItem.ToString() + ",";
                        }

                        _shippingNo = _shippingNo.Substring(0, _shippingNo.Length - 1);
                    }

                    //Add data to dtInvoiceMS
                    DataRow invoiceMS = dtInvoiceMS.NewRow();
                    if (radNormal.Checked == true)
                    {
                        invoiceMS["LockStatus"] = EnumRevise.Normal;
                    }
                    else if (radRevise.Checked == true)
                    {
                        invoiceMS["LockStatus"] = EnumRevise.Revise;
                    }
                    invoiceMS["CompanyCode"] = _systemDAL.CompanyCode;
                    invoiceMS["ShippingNo"] = _shippingNo;
                    invoiceMS["InvoiceNo"] = _invoiceNo;
                    invoiceMS["DateCreate"] = dtpDateCreateInv.Value;
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

                    //Add data to dtShippingNo
                    foreach (var item in listBox_ShippingNo.Items)
                    {
                        DataRow dtrow = dtShippingNo.NewRow();
                        dtrow["ShippingNo"] = item.ToString();
                        dtShippingNo.Rows.Add(dtrow);
                    }


                    //Grid Invoice
                    foreach (DataGridViewRow row in GridView_Invoice.Rows)
                    {
                        if (row.Cells["Tvc_ItemCode"].Value != null)
                        {
                            DataRow invoiceDetail = dtInvoiceDetail.NewRow();
                            invoiceDetail["CompanyCode"] = _systemDAL.CompanyCode;
                            invoiceDetail["CustomerCode"] = row.Cells["Customer_Code"].Value.ToString();
                            invoiceDetail["ShippingNo"] = row.Cells["Shipping_No"].Value.ToString();
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
                                invoiceDetail["ReviseNo"] = _invoiceNo + "_Revise_" + Convert.ToInt32(Version + 1);
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
                            } else if (radNormal.Checked == true)
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
                            else if (radNormal.Checked == true)
                            {
                                invoiceDetail["OrderPriceRevise"] = 0;
                            }
                            invoiceDetail["Global_Price"] = Convert.ToDecimal(row.Cells["Global_Price"].Value);
                            invoiceDetail["Amount"] = Convert.ToDecimal(row.Cells["Amount_Jpy"].Value);
                            dtInvoiceDetail.Rows.Add(invoiceDetail);
                        }
                    }

                    //Grid Packing List
                    foreach (DataGridViewRow row in GridView_PackingList.Rows)
                    {
                        if (row.Cells["Tvc_ItemCode"].Value != null)
                        {
                            DataRow packingListDetail = dtPackingListDetail.NewRow();
                            packingListDetail["CompanyCode"] = _systemDAL.CompanyCode;
                            packingListDetail["CustomerCode"] = row.Cells["Customer_Code"].Value.ToString();
                            packingListDetail["ShippingNo"] = row.Cells["Shipping_No"].Value.ToString();
                            packingListDetail["InvoiceNo"] = _invoiceNo;
                            //Mode: Add new
                            if (radNormal.Checked == true)
                            {
                                packingListDetail["ReviseNo"] = _invoiceNo;
                                packingListDetail["ReviseDate"] = DateTime.Now;
                                packingListDetail["Version"] = Number.Zero;                     //Init = 0
                            }
                            else if (radRevise.Checked == true)
                            {
                                packingListDetail["ReviseNo"] = _invoiceNo + "_Revise_" + Convert.ToInt32(Version + 1);
                                packingListDetail["ReviseDate"] = DateTime.Now;
                                packingListDetail["Version"] = Convert.ToInt32(Version + 1);    //Init = 0
                            }
                            packingListDetail["PackagesNo"] = row.Cells["Packages_No"].Value.ToString();
                            packingListDetail["Tvc_ItemCode"] = row.Cells["Tvc_ItemCode"].Value.ToString();
                            packingListDetail["Customer_PO"] = row.Cells["Customer_PO"].Value.ToString();
                            packingListDetail["QtyCarton"] = Convert.ToDecimal(row.Cells["Qty_Carton"].Value);
                            packingListDetail["QtyPerCarton"] = row.Cells["Qty_Per_Carton"].Value.ToString();
                            packingListDetail["QuantityTotal"] = Convert.ToDecimal(row.Cells["Qty_Total"].Value);
                            if (radRevise.Checked == true)
                            {
                                packingListDetail["QuantityTotalRevise"] = Convert.ToDecimal(row.Cells["Qty_Total_Revise"].Value);
                            } else if (radNormal.Checked == true)
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
                                                                ,
                                                                ShippingNo = x.Field<string>("ShippingNo")
                                                                ,
                                                                PackagesNo = x.Field<string>("PackagesNo")
                                                                ,
                                                                Customer_ItemCode = x.Field<string>("Tvc_ItemCode")
                                                                ,
                                                                Customer_PO = x.Field<string>("Customer_PO")
                                                                ,
                                                                QtyPerCarton = x.Field<string>("QtyPerCarton")
                                                                ,
                                                                QuantityTotal = x.Field<string>("QuantityTotal")
                                                                ,
                                                                NetWeight = x.Field<decimal>("NetWeight")
                                                                ,
                                                                LotNo = x.Field<string>("LotNo")
                                                            }).Where(x => x.Count() > 1);

                    //Nếu có dữ liệu trùng thì xuất ra thông báo lỗi
                    if (count_Duplication_PL != null && count_Duplication_PL.Any())
                    {
                        foreach (var group in count_Duplication_PL)
                        {
                            foreach (DataRow row in group)
                            {
                                MessageBox.Show("Trùng dữ liệu Packing List." + "\nCustomer Item Code: " + row.Field<string>("Tvc_ItemCode")
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
                        if (_invDAO.insertInvoice(dtInvoiceMS, dtInvoiceDetail, dtPackingListDetail, dtShippingNo) == true)
                        {
                            string Message = "";
                            if (!String.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                            {
                                Message = "Lưu thành công Invoice: \"" + txtInvoiceNo.Text + "\"!";

                                //Write Lock
                                string _typeLock = "";
                                DataTable _tempTable = new DataTable();
                                _tempTable = _invDAO.CheckInv(txtInvoiceNo.Text);
                                if (_tempTable.Rows.Count > 0)
                                {
                                    _typeLock = "EDIT";
                                }
                                else if (_tempTable.Rows.Count == 0)
                                {
                                    _typeLock = "NEW";
                                }
                                _logDAO.InsertLog(_systemDAL.CompanyCode, _systemDAL.UserName, _typeLock, Message);
                            }
                            MessageBox.Show(Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearData();
                            txtInvoiceNo.Focus();
                        }
                    }
                    catch (ApplicationException ex)
                    {
                        MessageBox.Show(ex.Message, "Lỗi!");
                    }
                }
            }
            else
            {
                return;
            }
        }

        private void btnExport_Data_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Chọn Template Invoice & Packing List";
            theDialog.Filter = "Files Excel|*.xlsx";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string _filePath = theDialog.FileName;

                    //Create fileinfo object of an excel file
                    FileInfo _fileInfo = new FileInfo(_filePath);

                    // According to the Polyform Noncommercial license:
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    //Create a new Excel package from the file
                    using (ExcelPackage _excelPackage = new ExcelPackage(_fileInfo))
                    {
                        int rowCount = 0;
                        DataRow drLocal = null;
                        int _valueCbFreight = Convert.ToInt32(cb_Freight.SelectedValue);

                        #region Export Invoice
                        ExcelWorksheet _invoiceSheet = _excelPackage.Workbook.Worksheets[1];
                        _invoiceSheet.Cells["D9"].Value = dtpDateCreateInv.Value.ToString("MMMM dd,yyyy");

                        _invoiceSheet.Cells["D10"].Value = txtInvoiceNo.Text.Trim();

                        _invoiceSheet.Cells["D11"].Value = txtIssuedTo_CompanyName.Text.Trim();
                        _invoiceSheet.Cells["D12"].Value = txtIssuedTo_CompanyAddress.Text.Trim();
                        _invoiceSheet.Cells["D14"].Value = "TEL: " + txtIssuedTo_TelNo.Text.Trim();
                        _invoiceSheet.Cells["E14"].Value = "FAX: " + txtIssuedTo_FaxNo.Text.Trim();

                        _invoiceSheet.Cells["D15"].Value = txtShipTo_CompanyName.Text.Trim();
                        _invoiceSheet.Cells["D16"].Value = txtShipTo_CompanyAddress.Text.Trim();
                        _invoiceSheet.Cells["D18"].Value = "TEL: " + txtShipTo_TelNo.Text.Trim();
                        _invoiceSheet.Cells["E18"].Value = "FAX: " + txtShipTo_FaxNo.Text.Trim();

                        _invoiceSheet.Cells["G9"].Value = txtShipVia.Text.Trim();

                        if (_valueCbFreight == 0)
                        {
                            _invoiceSheet.Cells["G10"].Value = "COLLECT";
                        }
                        else
                        {
                            _invoiceSheet.Cells["G10"].Value = "PREPAID";
                        }

                        _invoiceSheet.Cells["G11"].Value = txtVessel.Text.Trim();

                        _invoiceSheet.Cells["G12"].Value = txtPortLoading.Text.Trim();

                        _invoiceSheet.Cells["G13"].Value = txtPortDestination.Text.Trim();

                        _invoiceSheet.Cells["G14"].Value = dtpETD.Value.ToString("MMMM dd,yyyy");

                        _invoiceSheet.Cells["G15"].Value = dtpETA.Value.ToString("MMMM dd,yyyy");

                        _invoiceSheet.Cells["G16"].Value = txtPriceCondition.Text.Trim();

                        _invoiceSheet.Cells["G17"].Value = txtPaymentTerm.Text.Trim();
                        //
                        DataTable _tempInvoice = new DataTable();
                        _tempInvoice.Columns.Add("No");
                        _tempInvoice.Columns["No"].DataType = typeof(Int32);
                        _tempInvoice.Columns.Add("Item_Name");
                        _tempInvoice.Columns.Add("Cus_ItemCode");
                        _tempInvoice.Columns.Add("Customer_PO");
                        _tempInvoice.Columns.Add("Quantity");
                        _tempInvoice.Columns["Quantity"].DataType = typeof(Decimal);
                        _tempInvoice.Columns.Add("Order_Price");
                        _tempInvoice.Columns["Order_Price"].DataType = typeof(Decimal);
                        _tempInvoice.Columns.Add("Amount");
                        _tempInvoice.Columns["Amount"].DataType = typeof(Decimal);
                        _tempInvoice.Columns.Add("Tvc_ItemCode");

                        int _noIndex = 1;
                        string _unitCurrency = "";
                        foreach (DataGridViewRow dr in GridView_Invoice.Rows)
                        {
                            if (!dr.IsNewRow)
                            {
                                if (radNormal.Checked== true || (radRevise.Checked == true && Convert.ToInt32(dr.Cells["Quantity_Revise"].Value) != 0))
                                { 
                                    drLocal = _tempInvoice.NewRow();
                                    drLocal["No"] = _noIndex;
                                    drLocal["Item_Name"] = dr.Cells["Part_Description"].Value;
                                    drLocal["Cus_ItemCode"] = dr.Cells["Cus_ItemCode"].Value;
                                    if(dr.Cells["ThirdParty_PO"].Value != null && !string.IsNullOrEmpty(Convert.ToString(dr.Cells["ThirdParty_PO"].Value)))
                                    { 
                                        drLocal["Customer_PO"] = dr.Cells["Customer_PO"].Value + "-" + dr.Cells["ThirdParty_PO"].Value;
                                    }
                                    else
                                    {
                                        drLocal["Customer_PO"] = dr.Cells["Customer_PO"].Value;
                                    }
                                    if (radNormal.Checked == true)
                                    { 
                                        drLocal["Quantity"] = dr.Cells["Quantity"].Value;
                                    } else if (radRevise.Checked == true)
                                    {
                                        drLocal["Quantity"] = dr.Cells["Quantity_Revise"].Value;
                                    }
                                    if (String.IsNullOrEmpty(_unitCurrency))
                                    {
                                        _unitCurrency = dr.Cells["Unit_Currency"].Value.ToString();
                                    }
                                    if (radNormal.Checked == true)
                                    { 
                                        drLocal["Order_Price"] = dr.Cells["Order_Price"].Value;
                                    }
                                    else if (radRevise.Checked == true)
                                    {
                                        drLocal["Order_Price"] = dr.Cells["Order_Price_Revise"].Value;
                                    }
                                    drLocal["Amount"] = dr.Cells["Amount_Jpy"].Value;
                                    drLocal["Tvc_ItemCode"] = dr.Cells["Tvc_ItemCode"].Value;
                                    _noIndex++;
                                    _tempInvoice.Rows.Add(drLocal);
                                }
                            }
                        }

                        rowCount = _tempInvoice.Rows.Count;

                        if (rowCount > 0)
                        {
                            if (!String.IsNullOrEmpty(_unitCurrency))
                            {
                                if (_unitCurrency.ToLower() == "usd")
                                {
                                    _invoiceSheet.Cells["G20"].Value = "UNIT PRICE\n(USD)";
                                    _invoiceSheet.Cells["H20"].Value = "AMOUNT\n(USD)";
                                }
                                else if (_unitCurrency.ToLower() == "jpy")
                                {
                                    _invoiceSheet.Cells["G20"].Value = "UNIT PRICE\n(JPY)";
                                    _invoiceSheet.Cells["H20"].Value = "AMOUNT\n(JPY)";
                                }
                            }

                            _invoiceSheet.InsertRow(22, rowCount - 1, 21);
                            for (int i = 0; i <= rowCount; i++)
                            {
                                _invoiceSheet.Row(21 + i).Height = 30;
                                if (_unitCurrency.ToLower() == "jpy")
                                {
                                    _invoiceSheet.Cells["G" + (21 + i).ToString()].Style.Numberformat.Format = "#,##0.00_)";
                                    _invoiceSheet.Cells["H" + (21 + i).ToString()].Style.Numberformat.Format = "#,##0_)";
                                } else if (_unitCurrency.ToLower() == "usd")
                                {
                                    _invoiceSheet.Cells["G" + (21 + i).ToString()].Style.Numberformat.Format = "#,##0.00_)";
                                    _invoiceSheet.Cells["H" + (21 + i).ToString()].Style.Numberformat.Format = "#,##0.00_)";
                                }
                            }
                            _invoiceSheet.Cells["B21"].LoadFromDataTable(_tempInvoice, false);
                            _invoiceSheet.Cells["F" + (21 + rowCount).ToString()].Formula = "SUM(F21:F" + (20 + rowCount).ToString() + ")";
                            _invoiceSheet.Cells["H" + (21 + rowCount).ToString()].Formula = "SUM(H21:H" + (20 + rowCount).ToString() + ")";
                        }
                        #endregion

                        #region Export PackingList
                        ExcelWorksheet _packingListSheet = _excelPackage.Workbook.Worksheets[2];
                        _packingListSheet.Cells["B8"].Value = dtpDateCreateInv.Value.ToString("MMMM dd,yyyy");
                        _packingListSheet.Cells["B9"].Value = txtInvoiceNo.Text.Trim();
                        _packingListSheet.Cells["B10"].Value = txtIssuedTo_CompanyName.Text.Trim();
                        _packingListSheet.Cells["B11"].Value = txtIssuedTo_CompanyAddress.Text.Trim();
                        _packingListSheet.Cells["B13"].Value = "TEL: " + txtIssuedTo_TelNo.Text.Trim();
                        _packingListSheet.Cells["C13"].Value = "FAX: " + txtIssuedTo_FaxNo.Text.Trim();

                        _packingListSheet.Cells["B14"].Value = txtShipTo_CompanyName.Text.Trim();
                        _packingListSheet.Cells["B15"].Value = txtShipTo_CompanyAddress.Text.Trim();
                        _packingListSheet.Cells["B17"].Value = "TEL: " + txtShipTo_TelNo.Text.Trim();
                        _packingListSheet.Cells["C17"].Value = "FAX: " + txtShipTo_FaxNo.Text.Trim();

                        _packingListSheet.Cells["G8"].Value = txtShipVia.Text.Trim();

                        if (_valueCbFreight == 0)
                        {
                            _packingListSheet.Cells["G9"].Value = "COLLECT";
                        }
                        else
                        {
                            _packingListSheet.Cells["G9"].Value = "PREPAID";
                        }

                        _packingListSheet.Cells["G10"].Value = txtVessel.Text.Trim();

                        _packingListSheet.Cells["G11"].Value = txtPortLoading.Text.Trim();

                        _packingListSheet.Cells["G12"].Value = txtPortDestination.Text.Trim();

                        _packingListSheet.Cells["G13"].Value = dtpETD.Value.ToString("MMMM dd,yyyy");

                        _packingListSheet.Cells["G14"].Value = dtpETA.Value.ToString("MMMM dd,yyyy");

                        _packingListSheet.Cells["G15"].Value = txtPriceCondition.Text.Trim();

                        _packingListSheet.Cells["G16"].Value = txtPaymentTerm.Text.Trim();

                        _packingListSheet.Cells["B25"].Value = txtInvoiceNo.Text.Trim();

                        _packingListSheet.Cells["B29"].Value = txtPortDestination.Text.Trim();

                        _packingListSheet.Cells["G25"].Value = txtTotal_NetWeight.Text.Trim();

                        _packingListSheet.Cells["G26"].Value = txtTotal_GrossWeight.Text.Trim();

                        //
                        DataTable _tempPackingList = new DataTable();
                        _tempPackingList.Columns.Add("Pakages_No");
                        _tempPackingList.Columns.Add("Tvc_ItemCode");
                        _tempPackingList.Columns.Add("Qty_Of_Box");
                        _tempPackingList.Columns["Qty_Of_Box"].DataType = typeof(Int32);
                        _tempPackingList.Columns.Add("Qty_Per_Box");
                        _tempPackingList.Columns["Qty_Per_Box"].DataType = typeof(Int32);
                        _tempPackingList.Columns.Add("Quantity_Total");
                        _tempPackingList.Columns["Quantity_Total"].DataType = typeof(Int32);
                        _tempPackingList.Columns.Add("Net_Weight");
                        _tempPackingList.Columns["Net_Weight"].DataType = typeof(Decimal);
                        _tempPackingList.Columns.Add("Gross_Weight");
                        _tempPackingList.Columns["Gross_Weight"].DataType = typeof(Decimal);
                        if (txtShipTo_CompanyCode.Text.ToUpper() == "TTC")
                        {
                            _tempPackingList.Columns.Add("Customer_PO");
                        }
                        _tempPackingList.Columns.Add("Lot_No");

                        drLocal = null;
                        foreach (DataGridViewRow dr in GridView_PackingList.Rows)
                        {
                            if (!dr.IsNewRow)
                            {
                                if (radNormal.Checked == true || (radRevise.Checked == true && Convert.ToInt32(dr.Cells["Qty_Total_Revise"].Value) != 0))
                                {
                                    drLocal = _tempPackingList.NewRow();
                                    drLocal["Pakages_No"] = dr.Cells["Packages_No"].Value;
                                    drLocal["Tvc_ItemCode"] = dr.Cells["Tvc_ItemCode"].Value;
                                    drLocal["Qty_Of_Box"] = dr.Cells["Qty_Carton"].Value;
                                    drLocal["Qty_Per_Box"] = dr.Cells["Qty_Per_Carton"].Value;
                                    if (radNormal.Checked == true)
                                    {
                                        drLocal["Quantity_Total"] = dr.Cells["Qty_Total"].Value;
                                    }
                                    else if (radRevise.Checked == true)
                                    {
                                        drLocal["Quantity_Total"] = dr.Cells["Qty_Total_Revise"].Value;
                                    }
                                    drLocal["Net_Weight"] = dr.Cells["Net_Weight_Total"].Value;
                                    drLocal["Gross_Weight"] = dr.Cells["Gross_Weight"].Value;
                                    string _lotNO = dr.Cells["Lot_No"].Value.ToString();
                                    if (txtShipTo_CompanyCode.Text.ToUpper() == "TTC")
                                    {
                                        drLocal["Customer_PO"] = dr.Cells["Customer_PO"].Value; ;
                                    }
                                    if (_lotNO.ToLower().Contains("none-lot-no"))
                                    {
                                        drLocal["Lot_No"] = "";
                                    }
                                    else
                                    {
                                        drLocal["Lot_No"] = dr.Cells["Lot_No"].Value;
                                    }
                                    _tempPackingList.Rows.Add(drLocal);
                                }
                            }
                        }

                        rowCount = _tempPackingList.Rows.Count;

                        //if (rowCount > 0)
                        //{
                        //    _packingListSheet.InsertRow(22, rowCount - 1, 21);
                        //    for (int i = 0; i <= rowCount; i++)
                        //    {
                        //        _packingListSheet.Row(21 + i).Height = 30;
                        //    }
                        //    _packingListSheet.Cells["A21"].LoadFromDataTable(_tempPackingList, false);
                        //    _packingListSheet.Cells["C" + (21 + rowCount).ToString()].Formula = "SUM(C21:C" + (20 + rowCount).ToString() + ")";
                        //    _packingListSheet.Cells["E" + (21 + rowCount).ToString()].Formula = "SUM(E21:E" + (20 + rowCount).ToString() + ")";
                        //    _packingListSheet.Cells["F" + (21 + rowCount).ToString()].Formula = "SUM(F21:F" + (20 + rowCount).ToString() + ")";
                        //    _packingListSheet.Cells["G" + (21 + rowCount).ToString()].Formula = "SUM(G21:G" + (20 + rowCount).ToString() + ")";
                        //}
                        #endregion

                        //Focus A1, sheet Invoice
                        _invoiceSheet.Select("A1");

                        byte[] bin = _excelPackage.GetAsByteArray();

                        //Create a SaveFileDialog instance with some properties
                        SaveFileDialog _saveFileDialog = new SaveFileDialog();
                        _saveFileDialog.Title = "Save file Invoice & Packing List";
                        _saveFileDialog.Filter = "Excel files|*.xlxs|All files|*.*";
                        _saveFileDialog.FileName = txtInvoiceNo.Text.Trim().Replace("/",".") + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";

                        //Check if user clicked the save button
                        if (_saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            //write the file to the disk
                            File.WriteAllBytes(_saveFileDialog.FileName, bin);
                            MessageBox.Show("Tạo file Invoice và Packing List thành công!", "Hoàn Thành", MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnExport_Data_For_ACC_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Chọn Template Invoice & Packing List";
            theDialog.Filter = "Files Excel|*.xlsx";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string _filePath = theDialog.FileName;

                    //Create fileinfo object of an excel file
                    FileInfo _fileInfo = new FileInfo(_filePath);

                    // According to the Polyform Noncommercial license:
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    //Create a new Excel package from the file
                    using (ExcelPackage _excelPackage = new ExcelPackage(_fileInfo))
                    {
                        int rowCount = 0;
                        DataRow drLocal = null;
                        int _valueCbFreight = Convert.ToInt32(cb_Freight.SelectedValue);

                        #region Export Invoice
                        ExcelWorksheet _invoiceSheet = _excelPackage.Workbook.Worksheets[0];
                        _invoiceSheet.Cells["D9"].Value = dtpDateCreateInv.Value.ToString("MMMM dd,yyyy");

                        _invoiceSheet.Cells["D10"].Value = txtInvoiceNo.Text.Trim();

                        _invoiceSheet.Cells["D11"].Value = txtIssuedTo_CompanyName.Text.Trim();
                        _invoiceSheet.Cells["D12"].Value = txtIssuedTo_CompanyAddress.Text.Trim();
                        _invoiceSheet.Cells["D14"].Value = "TEL: " + txtIssuedTo_TelNo.Text.Trim();
                        _invoiceSheet.Cells["E14"].Value = "FAX: " + txtIssuedTo_FaxNo.Text.Trim();

                        _invoiceSheet.Cells["D15"].Value = txtShipTo_CompanyName.Text.Trim();
                        _invoiceSheet.Cells["D16"].Value = txtShipTo_CompanyAddress.Text.Trim();
                        _invoiceSheet.Cells["D18"].Value = "TEL: " + txtShipTo_TelNo.Text.Trim();
                        _invoiceSheet.Cells["E18"].Value = "FAX: " + txtShipTo_FaxNo.Text.Trim();

                        _invoiceSheet.Cells["G9"].Value = txtShipVia.Text.Trim();

                        if (_valueCbFreight == 0)
                        {
                            _invoiceSheet.Cells["G10"].Value = "COLLECT";
                        }
                        else
                        {
                            _invoiceSheet.Cells["G10"].Value = "PREPAID";
                        }

                        _invoiceSheet.Cells["G11"].Value = txtVessel.Text.Trim();

                        _invoiceSheet.Cells["G12"].Value = txtPortLoading.Text.Trim();

                        _invoiceSheet.Cells["G13"].Value = txtPortDestination.Text.Trim();

                        _invoiceSheet.Cells["G14"].Value = dtpETD.Value.ToString("MMMM dd,yyyy");

                        _invoiceSheet.Cells["G15"].Value = dtpETA.Value.ToString("MMMM dd,yyyy");

                        _invoiceSheet.Cells["G16"].Value = txtPriceCondition.Text.Trim();

                        _invoiceSheet.Cells["G17"].Value = txtPaymentTerm.Text.Trim();
                        //
                        DataTable _tempInvoice = new DataTable();
                        _tempInvoice.Columns.Add("No");
                        _tempInvoice.Columns["No"].DataType = typeof(Int32);
                        _tempInvoice.Columns.Add("Item_Name");
                        _tempInvoice.Columns.Add("Cus_ItemCode");
                        _tempInvoice.Columns.Add("Customer_PO");
                        _tempInvoice.Columns.Add("Quantity");
                        _tempInvoice.Columns["Quantity"].DataType = typeof(Decimal);
                        _tempInvoice.Columns.Add("Order_Price");
                        _tempInvoice.Columns["Order_Price"].DataType = typeof(Decimal);
                        _tempInvoice.Columns.Add("Amount");
                        _tempInvoice.Columns["Amount"].DataType = typeof(Decimal);
                        _tempInvoice.Columns.Add("Tvc_ItemCode");

                        int _noIndex = 1;
                        string _unitCurrency = "";
                        foreach (DataGridViewRow dr in GridView_Invoice.Rows)
                        {
                            if (!dr.IsNewRow)
                            {
                                if (radNormal.Checked == true || (radRevise.Checked == true && Convert.ToInt32(dr.Cells["Quantity_Revise"].Value) != 0))
                                {
                                    drLocal = _tempInvoice.NewRow();
                                    drLocal["No"] = _noIndex;
                                    drLocal["Item_Name"] = dr.Cells["Part_Description"].Value;
                                    drLocal["Cus_ItemCode"] = dr.Cells["Cus_ItemCode"].Value;
                                    if (dr.Cells["ThirdParty_PO"].Value != null && !string.IsNullOrEmpty(Convert.ToString(dr.Cells["ThirdParty_PO"].Value)))
                                    {
                                        drLocal["Customer_PO"] = dr.Cells["Customer_PO"].Value + "-" + dr.Cells["ThirdParty_PO"].Value;
                                    }
                                    else
                                    {
                                        drLocal["Customer_PO"] = dr.Cells["Customer_PO"].Value;
                                    }
                                    if (radNormal.Checked == true)
                                    {
                                        drLocal["Quantity"] = dr.Cells["Quantity"].Value;
                                    }
                                    else if (radRevise.Checked == true)
                                    {
                                        drLocal["Quantity"] = dr.Cells["Quantity_Revise"].Value;
                                    }
                                    if (String.IsNullOrEmpty(_unitCurrency))
                                    {
                                        _unitCurrency = dr.Cells["Unit_Currency"].Value.ToString();
                                    }
                                    if (radNormal.Checked == true)
                                    {
                                        drLocal["Order_Price"] = dr.Cells["Order_Price"].Value;
                                    }
                                    else if (radRevise.Checked == true)
                                    {
                                        drLocal["Order_Price"] = dr.Cells["Order_Price_Revise"].Value;
                                    }
                                    drLocal["Amount"] = dr.Cells["Amount_Jpy"].Value;
                                    drLocal["Tvc_ItemCode"] = dr.Cells["Tvc_ItemCode"].Value + "-" + dr.Cells["Part_Description"].Value;
                                    _noIndex++;
                                    _tempInvoice.Rows.Add(drLocal);
                                }
                            }
                        }

                        rowCount = _tempInvoice.Rows.Count;

                        if (rowCount > 0)
                        {
                            if (!String.IsNullOrEmpty(_unitCurrency))
                            {
                                if (_unitCurrency.ToLower() == "usd")
                                {
                                    _invoiceSheet.Cells["G20"].Value = "UNIT PRICE\n(USD)";
                                    _invoiceSheet.Cells["H20"].Value = "AMOUNT\n(USD)";
                                }
                                else if (_unitCurrency.ToLower() == "jpy")
                                {
                                    _invoiceSheet.Cells["G20"].Value = "UNIT PRICE\n(JPY)";
                                    _invoiceSheet.Cells["H20"].Value = "AMOUNT\n(JPY)";
                                }
                            }

                            _invoiceSheet.InsertRow(22, rowCount - 1, 21);
                            for (int i = 0; i <= rowCount; i++)
                            {
                                _invoiceSheet.Row(21 + i).Height = 30;
                                if (_unitCurrency.ToLower() == "jpy")
                                {
                                    _invoiceSheet.Cells["G" + (21 + i).ToString()].Style.Numberformat.Format = "#,##0.00_)";
                                    _invoiceSheet.Cells["H" + (21 + i).ToString()].Style.Numberformat.Format = "#,##0_)";
                                }
                                else if (_unitCurrency.ToLower() == "usd")
                                {
                                    _invoiceSheet.Cells["G" + (21 + i).ToString()].Style.Numberformat.Format = "#,##0.00_)";
                                    _invoiceSheet.Cells["H" + (21 + i).ToString()].Style.Numberformat.Format = "#,##0.00_)";
                                }
                            }
                            _invoiceSheet.Cells["B21"].LoadFromDataTable(_tempInvoice, false);
                            _invoiceSheet.Cells["F" + (21 + rowCount).ToString()].Formula = "SUM(F21:F" + (20 + rowCount).ToString() + ")";
                            _invoiceSheet.Cells["H" + (21 + rowCount).ToString()].Formula = "SUM(H21:H" + (20 + rowCount).ToString() + ")";
                        }
                        #endregion

                        //Focus A1, sheet Invoice
                        _invoiceSheet.Select("A1");

                        byte[] bin = _excelPackage.GetAsByteArray();

                        //Create a SaveFileDialog instance with some properties
                        SaveFileDialog _saveFileDialog = new SaveFileDialog();
                        _saveFileDialog.Title = "Save file Invoice & Packing List";
                        _saveFileDialog.Filter = "Excel files|*.xlxs|All files|*.*";
                        _saveFileDialog.FileName = txtInvoiceNo.Text.Trim().Replace("/", ".") + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";

                        //Check if user clicked the save button
                        if (_saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            //write the file to the disk
                            File.WriteAllBytes(_saveFileDialog.FileName, bin);
                            MessageBox.Show("Tạo file Invoice và Packing List thành công!", "Hoàn Thành", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void btn_SearchInvoice_Click(object sender, EventArgs e)
        {
            string _invoiceNo = txtInvoiceNo.Text.Trim();
            try
            {
                DataTable _tempInvTable = new DataTable();
                //
                _tempInvTable = _invDAO.GetInfo_Invoice(_invoiceNo);
                //
                if (_tempInvTable.Rows.Count == 0)
                {
                    MessageBox.Show("Invoice \"" + "\" không tồn tại!");
                }
                else if (_tempInvTable.Rows.Count > 0)
                {
                    //Clear DataGridView
                    GridView_Invoice.Rows.Clear();
                    GridView_PackingList.Rows.Clear();
                    //-----------------------HUNGTEST-----------------------//
                    ClearData();
                    //-----------------------HUNGTEST-----------------------//
                    foreach (DataRow row in _tempInvTable.Rows)
                    {
                        int indexInv = 0;
                        if (indexInv == 0)
                        {
                            string Shipping_No = Convert.ToString(row["SHIPPING_NO"]);
                            //SHIPPING NO
                            if (!String.IsNullOrEmpty(Shipping_No))
                            {
                                listBox_ShippingNo.Items.Clear();
                                List<String> _list_shipping_No = Shipping_No.Split(',').ToList();
                                foreach (var shipping in _list_shipping_No)
                                {
                                    listBox_ShippingNo.Items.Add(shipping);
                                }
                            }
                            
                            //INVOICE NO
                            txtInvoiceNo.Text = row["INVOICE_NO"].ToString();

                            //ISSUEDTO
                            txtIssuedTo_CompanyCode.Text = row["ISSUEDTO_CUSTOMER_CODE"].ToString();
                            txtIssuedTo_CompanyName.Text = row["ISSUEDTO_CUSTOMER_NAME"].ToString();
                            txtIssuedTo_CompanyAddress.Text = row["ISSUEDTO_CUSTOMER_ADDRESS"].ToString();
                            txtIssuedTo_TelNo.Text = row["ISSUEDTO_CUSTOMER_TEL_NO"].ToString();
                            txtIssuedTo_FaxNo.Text = row["ISSUEDTO_CUSTOMER_FAX_NO"].ToString();

                            //SHIPTO
                            txtShipTo_CompanyCode.Text = row["SHIPTO_CUSTOMER_CODE"].ToString();
                            txtShipTo_CompanyName.Text = row["SHIPTO_CUSTOMER_NAME"].ToString();
                            txtShipTo_CompanyAddress.Text = row["SHIPTO_CUSTOMER_ADDRESS"].ToString();
                            txtShipTo_TelNo.Text = row["SHIPTO_CUSTOMER_TEL_NO"].ToString();
                            txtShipTo_FaxNo.Text = row["SHIPTO_CUSTOMER_FAX_NO"].ToString();

                            //REVENUE
                            if (!String.IsNullOrEmpty(Convert.ToString(row["REVENUE"])))
                            {
                                dtpRevenue.Value = Convert.ToDateTime(row["REVENUE"]);
                            }
                            else
                            {
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
                            _createBy = row["CREATE_BY"].ToString();
                        }

                        if (!String.IsNullOrEmpty(row["INV_CUSTOMER_CODE"].ToString()))
                        {
                            indexInv = GridView_Invoice.Rows.Add();
                            //Binding for Invoice
                            GridView_Invoice.Rows[indexInv].Cells["Shipping_No"].Value = row["INV_SHIPPING_NO"].ToString();
                            GridView_Invoice.Rows[indexInv].Cells["Customer_Code"].Value = row["INV_CUSTOMER_CODE"].ToString();
                            GridView_Invoice.Rows[indexInv].Cells["Part_Description"].Value = row["INV_ITEM_NAME"].ToString();
                            GridView_Invoice.Rows[indexInv].Cells["Cus_ItemCode"].Value = row["INV_CUS_ITEM_CODE"].ToString();
                            GridView_Invoice.Rows[indexInv].Cells["Tvc_ItemCode"].Value = row["INV_ITEM_CODE"].ToString();
                            GridView_Invoice.Rows[indexInv].Cells["Customer_PO"].Value = row["INV_REF_PO_NO"].ToString();
                            GridView_Invoice.Rows[indexInv].Cells["ThirdParty_PO"].Value = row["THIRD_PARTY_PO"].ToString();
                            if (!String.IsNullOrEmpty(Convert.ToString(row["Order_Date"])))
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
                    DataTable _tempPLTable = new DataTable();
                    _tempPLTable = _invDAO.GetInfo_PL(_invoiceNo);
                    if (_tempPLTable.Rows.Count >= 1) {
                        foreach (DataRow row in _tempPLTable.Rows)
                        {
                            if(row["PL_CUSTOMER_CODE"] != null) { 
                                int indexPL = GridView_PackingList.Rows.Add();
                                GridView_PackingList.Rows[indexPL].Cells["Shipping_No"].Value = row["PL_SHIPPING_NO"].ToString();
                                GridView_PackingList.Rows[indexPL].Cells["Customer_Code"].Value = row["PL_CUSTOMER_CODE"].ToString();
                                GridView_PackingList.Rows[indexPL].Cells["Packages_No"].Value = row["PL_PACKAGES_NO"].ToString();
                                GridView_PackingList.Rows[indexPL].Cells["Tvc_ItemCode"].Value = row["PL_ITEM_CODE"].ToString();
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

                    GridView_PackingList.Sort(GridView_PackingList.Columns["Packages_No"], System.ComponentModel.ListSortDirection.Ascending);

                    //Sum Invoice
                    btnSumInvoice_Click(sender, e);
                    //Sum Packing List
                    btnSumPL_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Lỗi");
            }
        }

        private bool GetList_Data_Shipping()
        {
            DataTable _listBoxTable = new DataTable();
            _listBoxTable.Columns.Add("Shipping_No");

            DataTable _tempShippingTable = new DataTable();

            try
            {
                //Check listbox is not null
                if (listBox_ShippingNo.Items.Count > 0)
                {
                    //Add shipping No from listbox to datatable pass to Stored Procedure
                    foreach (var ListBoxItem in listBox_ShippingNo.Items)
                    {
                        DataRow rowListBox = _listBoxTable.NewRow();
                        rowListBox["Shipping_No"] = ListBoxItem.ToString();
                        _listBoxTable.Rows.Add(rowListBox);
                    }

                    //Execute Store Procedure [SP_TVC_SELECT_INV]
                    _tempShippingTable = _invDAO.GetInvoice_ByShippingNo(_listBoxTable);

                    //Check if have query data
                    if (_tempShippingTable.Rows.Count > 0)
                    {
                        //Clear DataGridView
                        GridView_Invoice.Rows.Clear();
                        GridView_PackingList.Rows.Clear();

                        //Pass data from Datatable to Screen
                        foreach (DataRow row in _tempShippingTable.Rows)
                        {
                            int indexInv = 0;
                            if (indexInv == 0)
                            {
                                //ISSUEDTO
                                txtIssuedTo_CompanyCode.Text = row["ISSUEDTO_CUSTOMER_CODE"].ToString();
                                txtIssuedTo_CompanyName.Text = row["ISSUEDTO_CUSTOMER_NAME"].ToString();
                                txtIssuedTo_CompanyAddress.Text = row["ISSUEDTO_CUSTOMER_ADDRESS"].ToString();
                                txtIssuedTo_TelNo.Text = row["ISSUEDTO_CUSTOMER_TEL_NO"].ToString();
                                txtIssuedTo_FaxNo.Text = row["ISSUEDTO_CUSTOMER_FAX_NO"].ToString();

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
                            }

                            if (!String.IsNullOrEmpty(row["INV_SHIPPING_NO"].ToString()))
                            {
                                indexInv = GridView_Invoice.Rows.Add();
                                //Binding for Invoice
                                GridView_Invoice.Rows[indexInv].Cells["Shipping_No"].Value = row["INV_SHIPPING_NO"].ToString();
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
                                if (!String.IsNullOrEmpty(Convert.ToString(row["DUE_DATE_PO"])))
                                { 
                                    GridView_Invoice.Rows[indexInv].Cells["DueDate_PO"].Value = Convert.ToDateTime(row["DUE_DATE_PO"]).ToString("dd/MM/yyyy");
                                }
                                if (Convert.ToInt32(row["LOCK_STATUS"]) == 2 && radNormal.Checked == true)
                                {
                                    GridView_Invoice.Rows[indexInv].Cells["Quantity"].Value = row["INV_QUANTITY_REVISE"];
                                    GridView_Invoice.Rows[indexInv].Cells["Order_Price"].Value = row["INV_ORDER_PRICE_REVISE"];
                                } else
                                {
                                    GridView_Invoice.Rows[indexInv].Cells["Quantity"].Value = row["INV_QUANTITY"];
                                    GridView_Invoice.Rows[indexInv].Cells["Order_Price"].Value = row["INV_ORDER_PRICE"];
                                }
                                GridView_Invoice.Rows[indexInv].Cells["Quantity_Revise"].Value = row["INV_QUANTITY_REVISE"];
                                GridView_Invoice.Rows[indexInv].Cells["Balance"].Value = row["INV_BALANCE"];
                                GridView_Invoice.Rows[indexInv].Cells["Unit_Currency"].Value = row["INV_UNIT_CURRENCY"];
                                GridView_Invoice.Rows[indexInv].Cells["USD_Rate"].Value = row["INV_USD_RATE"];
                                GridView_Invoice.Rows[indexInv].Cells["Order_Price_Revise"].Value = row["INV_ORDER_PRICE_REVISE"];
                                GridView_Invoice.Rows[indexInv].Cells["Global_Price"].Value = row["GLOBAL_PRICE"];
                                GridView_Invoice.Rows[indexInv].Cells["Amount_Jpy"].Value = row["INV_AMOUNT"];
                            }
                        }

                        DataTable _tempShipping_PLTable = new DataTable();
                        _tempShipping_PLTable = _invDAO.GetPL_ByShippingNo(_listBoxTable);
                        if (_tempShipping_PLTable.Rows.Count >= 1)
                        {
                            foreach (DataRow row in _tempShipping_PLTable.Rows)
                            {
                                if (row["PL_SHIPPING_NO"] != null)
                                {
                                    int indexPL = GridView_PackingList.Rows.Add();
                                    GridView_PackingList.Rows[indexPL].Cells["Shipping_No"].Value = row["PL_SHIPPING_NO"].ToString();
                                    GridView_PackingList.Rows[indexPL].Cells["Customer_Code"].Value = row["PL_CUSTOMER_CODE"].ToString();
                                    GridView_PackingList.Rows[indexPL].Cells["Packages_No"].Value = row["PL_PACKAGES_NO"].ToString();
                                    GridView_PackingList.Rows[indexPL].Cells["Tvc_ItemCode"].Value = row["PL_ITEM_CODE"].ToString();
                                    GridView_PackingList.Rows[indexPL].Cells["Customer_PO"].Value = row["PL_CUSTOMER_PO"].ToString();
                                    GridView_PackingList.Rows[indexPL].Cells["Qty_Carton"].Value = Convert.ToDecimal(row["PL_QTY_CARTON"]);
                                    GridView_PackingList.Rows[indexPL].Cells["Qty_Per_Carton"].Value = Convert.ToDecimal(row["PL_QTY_PER_CARTON"]);
                                    if (Convert.ToInt32(row["LOCK_STATUS"]) == 2 && radNormal.Checked == true)
                                    {
                                        GridView_PackingList.Rows[indexPL].Cells["Qty_Total"].Value = Convert.ToDecimal(row["PL_QTY_TOTAL_REVISE"]);
                                    }
                                    else
                                    {
                                        GridView_PackingList.Rows[indexPL].Cells["Qty_Total"].Value = Convert.ToDecimal(row["PL_QTY_TOTAL"]);
                                    }
                                    GridView_PackingList.Rows[indexPL].Cells["Qty_Total_Revise"].Value = row["PL_QTY_TOTAL_REVISE"];
                                    GridView_PackingList.Rows[indexPL].Cells["Net_Weight"].Value = Convert.ToDecimal(row["PL_NET_WEIGHT"]);
                                    GridView_PackingList.Rows[indexPL].Cells["Net_Weight_Total"].Value = Convert.ToDecimal(row["PL_NET_WEIGHT_TOTAL"]);
                                    GridView_PackingList.Rows[indexPL].Cells["Gross_Weight"].Value = Convert.ToDecimal(row["PL_GROSS_WEIGHT"]);
                                    GridView_PackingList.Rows[indexPL].Cells["Lot_No"].Value = row["PL_LOT_NO"].ToString();
                                }
                            }
                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Lỗi");
            }
            return true;
        }

        private void btn_NewRow_Click(object sender, EventArgs e)
        {
            int index = 0;
            if (tabControl.SelectedIndex == 0)
            {
                if (GridView_Invoice.SelectedRows.Count > 0)
                {
                    index = GridView_Invoice.CurrentCell.RowIndex;
                    GridView_Invoice.Rows.Insert(index, 1);
                    GridView_Invoice.CurrentCell = GridView_Invoice.Rows[index].Cells[0];
                    GridView_Invoice.BeginEdit(true);

                    GridView_PackingList.Rows.Insert(index, 1);
                    GridView_PackingList.CurrentCell = GridView_PackingList.Rows[index].Cells[0];
                }
                else
                {
                    MessageBox.Show("Please select location insert!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (tabControl.SelectedIndex == 1)
            {
                if (GridView_PackingList.SelectedRows.Count > 0)
                {
                    index = GridView_PackingList.CurrentCell.RowIndex;
                    GridView_PackingList.Rows.Insert(index, 1);
                    GridView_PackingList.CurrentCell = GridView_PackingList.Rows[index].Cells[0];
                    GridView_PackingList.BeginEdit(true);
                }
                else
                {
                    MessageBox.Show("Please select location insert!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// Coppy Packing List row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                if (GridView_PackingList.SelectedRows.Count == 1)
                {
                    int selectedRow = GridView_PackingList.CurrentRow.Index;
                    int index = GridView_PackingList.Rows.Add();
                    GridView_PackingList.Rows[index].Cells["Shipping_No"].Value = GridView_PackingList.Rows[selectedRow].Cells["Shipping_No"].Value;
                    GridView_PackingList.Rows[index].Cells["Customer_Code"].Value = GridView_PackingList.Rows[selectedRow].Cells["Customer_Code"].Value;
                    GridView_PackingList.Rows[index].Cells["Packages_No"].Value = GridView_PackingList.Rows[selectedRow].Cells["Packages_No"].Value;
                    GridView_PackingList.Rows[index].Cells["TVC_ItemCode"].Value = GridView_PackingList.Rows[selectedRow].Cells["TVC_ItemCode"].Value;
                    GridView_PackingList.Rows[index].Cells["Customer_PO"].Value = GridView_PackingList.Rows[selectedRow].Cells["Customer_PO"].Value;
                    GridView_PackingList.Rows[index].Cells["Qty_Carton"].Value = GridView_PackingList.Rows[selectedRow].Cells["Qty_Carton"].Value;
                    GridView_PackingList.Rows[index].Cells["Qty_Per_Carton"].Value = GridView_PackingList.Rows[selectedRow].Cells["Qty_Per_Carton"].Value;
                    GridView_PackingList.Rows[index].Cells["Qty_Total"].Value = GridView_PackingList.Rows[selectedRow].Cells["Qty_Total"].Value;
                    GridView_PackingList.Rows[index].Cells["Qty_Total_Revise"].Value = GridView_PackingList.Rows[selectedRow].Cells["Qty_Total_Revise"].Value;
                    GridView_PackingList.Rows[index].Cells["Net_Weight"].Value = GridView_PackingList.Rows[selectedRow].Cells["Net_Weight"].Value;
                    GridView_PackingList.Rows[index].Cells["Net_Weight_Total"].Value = GridView_PackingList.Rows[selectedRow].Cells["Net_Weight_Total"].Value;
                    GridView_PackingList.Rows[index].Cells["Gross_Weight"].Value = GridView_PackingList.Rows[selectedRow].Cells["Gross_Weight"].Value;
                    GridView_PackingList.Rows[index].Cells["Lot_No"].Value = GridView_PackingList.Rows[selectedRow].Cells["Lot_No"].Value;
                    GridView_PackingList.CurrentCell = GridView_PackingList.Rows[index].Cells["Packages_No"];
                    GridView_PackingList.Rows[index].Cells["Packages_No"].Selected = true;
                    GridView_PackingList.BeginEdit(true);
                }
                else if (GridView_PackingList.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Chỉ được chọn 1 row!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Chưa chọn 1 dòng cần coppy!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion

        DataGridViewRow lastSelected;
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

        private void btnRevise_Click(object sender, EventArgs e)
        {
            string _invoiceNo = txtInvoiceNo.Text.Trim();
            if (radNormal.Checked == true)
            {
                if ((MessageBox.Show("Do you want to Revise Invoice?", "Confirm"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    , MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    try
                    {
                        if (String.IsNullOrEmpty(_invoiceNo))
                        {
                            MessageBox.Show("Invoice No đang rỗng!");
                        }
                        else
                        {
                            DataTable _tempTable = new DataTable();
                            _tempTable = _invDAO.CheckInv(_invoiceNo);

                            if (_tempTable.Rows.Count == 0)
                            {
                                MessageBox.Show("Invoice không tồn tại trong hệ thống!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else if (_tempTable.Rows.Count == 1)
                            {
                                bool _result = _invDAO.ReviseInvoice(_invoiceNo);
                                if (_result)
                                {
                                    MessageBox.Show("Khóa Invoice \"" + _invoiceNo + "\" thành công!", "Hoàn Thành", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    radRevise.Checked = true;
                                }
                            }
                            else if (_tempTable.Rows.Count > 1)
                            {
                                MessageBox.Show("Invoice duplicate!");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else if (radRevise.Checked == true)
            {
                MessageBox.Show("Invoice đã được revise!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.btnDelete, "Xóa Invoice");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string _invoiceNo = txtInvoiceNo.Text.Trim();

            if (radNormal.Checked == true)
            {
                if (String.IsNullOrEmpty(_invoiceNo))
                {
                    MessageBox.Show("Không có thông tin Invoice cần xóa!");
                }
                else if ((MessageBox.Show("Bạn có muốn xóa Invoice \"" + _invoiceNo + "\"?", "Xác nhận"
                        , MessageBoxButtons.YesNo, MessageBoxIcon.Question
                        , MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    try
                    {
                        DataTable _listBoxTable = new DataTable();
                        _listBoxTable.Columns.Add("Shipping_No");

                        //Check listbox is not null
                        if (listBox_ShippingNo.Items.Count > 0)
                        {
                            //Add shipping No from listbox to datatable pass to Stored Procedure
                            foreach (var ListBoxItem in listBox_ShippingNo.Items)
                            {
                                DataRow rowListBox = _listBoxTable.NewRow();
                                rowListBox["Shipping_No"] = ListBoxItem.ToString();
                                _listBoxTable.Rows.Add(rowListBox);
                            }
                        }

                        DataTable _tempTable = new DataTable();
                        _tempTable = _invDAO.CheckInv(_invoiceNo);
                        
                        if (_tempTable.Rows.Count == 0)
                        {
                            MessageBox.Show("Invoice \"" + _invoiceNo + "\" không tồn tại trong hệ thống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (_tempTable.Rows.Count == 1)
                        {
                            bool _result = _invDAO.DeleteInvoice(_invoiceNo, _listBoxTable);
                            if (_result)
                            {
                                MessageBox.Show("Xóa Invoice \"" + _invoiceNo + "\" thành công!", "Hoàn thành", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void cbBox_Round_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Round up
            if (cbBox_Round.SelectedIndex == 1)
            {
                if (GridView_Invoice.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in GridView_Invoice.Rows)
                    {
                        if (!row.IsNewRow)
                        { 
                            row.Cells["Amount_Jpy"].Value = Math.Ceiling(Convert.ToDecimal(row.Cells["Amount_Jpy"].Value));
                        }
                    }
                }

                cbBox_Round.Enabled = false;
            }

            //Round down
            else if (cbBox_Round.SelectedIndex == 2)
            {
                if (GridView_Invoice.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in GridView_Invoice.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            row.Cells["Amount_Jpy"].Value = Math.Floor(Convert.ToDecimal(row.Cells["Amount_Jpy"].Value));
                        }
                    }
                }

                cbBox_Round.Enabled = false;
            }

            //Sum Grid Invoice
            btnSumInvoice_Click(sender, e);
        }

        private void btn_SortData_Click(object sender, EventArgs e)
        {
            GridView_PackingList.Sort(GridView_PackingList.Columns["Packages_No"], System.ComponentModel.ListSortDirection.Ascending);
        }
    }
}
