using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TAKAKO_ERP_3LAYER.DAL;
using TAKAKO_ERP_3LAYER.DAO;
using static TAKAKO_ERP_3LAYER.Common;

namespace TAKAKO_ERP_3LAYER
{
    public partial class Form_Search_PO : Form
    {
        public static string _caseSearch;

        public static string _customerCode;

        public static string _unitCurrency = "";
        public static DateTime _dateCreateInvoice;

        public POInfo _PoInfo;

        public SEARCH_DAO _searchDAO;

        public SYSTEM_DAL _systemDAL;

        public Form_Search_PO(SYSTEM_DAL _tempSystemDAL,string _valueTransmission,string _tempnameCompany, String _tempUnitCurrency,DateTime _tempDateCreateInvoice)
        {
            InitializeComponent();

            //Setting no title but have border
            this.ControlBox = false;
            this.Text = String.Empty;

            _searchDAO = new SEARCH_DAO();

            //
            _systemDAL = new SYSTEM_DAL();

            //
            _caseSearch = _valueTransmission;

            //
            _customerCode = _tempnameCompany;

            //
            _unitCurrency = _tempUnitCurrency;

            //
            _systemDAL = _tempSystemDAL;

            //
            _dateCreateInvoice = _tempDateCreateInvoice;
        }

        private void Form_Search_Load(object sender, EventArgs e)
        {
            string _searchValue = txtKeySearch.Text.Trim();
            string _customerPO = txtCustomerPO.Text.Trim();

            //
            AddColumnGridView();

            //Setting init radio button
            if (_unitCurrency.ToUpper() == "JPY")
            {
                radCurrencyJPY.Checked = true;
                radCurrencyUSD.Enabled = false;
            }
            else if (_unitCurrency.ToUpper() == "USD")
            {
                radCurrencyUSD.Checked = true;
                radCurrencyJPY.Enabled = false;
            }

            //
            SetInit_CboxCompany();

            //
            SetInit_CboxTypeSearch();
            cboxTypeSearch.SelectedValue = 0;

            if (_systemDAL.CompanyCode == "00001")
            {
                cboxCompany.SelectedValue = 1;
            }
            else if (_systemDAL.CompanyCode == "00002")
            {
                cboxCompany.SelectedValue = 2;
            }
            else
            {
                cboxCompany.SelectedValue = 0;
            }

            //Get data init
            SettingInit(_searchValue
                       ,_customerPO
                       ,_customerCode
                       ,IsCurrencyJapanese()
                       ,Convert.ToInt32(cboxCompany.SelectedValue)
                       ,Convert.ToInt32(cboxTypeSearch.SelectedValue));
        }

        /// <summary>
        /// Set Init combobox Freight
        /// </summary>
        public void SetInit_CboxCompany()
        {
            cboxCompany.DisplayMember = "Text";
            cboxCompany.ValueMember = "Value";

            var items = new[] {
                new { Text = "Whole Company", Value = 0 },
                new { Text = "TVC1", Value = 1 },
                new { Text = "TVC2", Value = 2 },
            };

            cboxCompany.DataSource = items;
        }

        /// <summary>
        /// Set Init combobox TypeSearch
        /// </summary>
        public void SetInit_CboxTypeSearch()
        {
            cboxTypeSearch.DisplayMember = "Text";
            cboxTypeSearch.ValueMember = "Value";

            var items = new[] {
                new { Text = "Single Search", Value = 0 },
                new { Text = "Multiple Search", Value = 1 },
            };

            cboxTypeSearch.DataSource = items;
        }

        public Boolean IsCurrencyJapanese()
        {
            if (radCurrencyJPY.Checked == true)
            {
                return true;
            }
            return false;
        }

        public void AddColumnGridView()
        {
            if (_caseSearch == "btnSearch_ItemCode")
            {
                System.Windows.Forms.DataGridViewCheckBoxColumn CheckBox_col = new System.Windows.Forms.DataGridViewCheckBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn CustomerCode_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn ReceiveNo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn CustomerItemCode_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn TVCItemCode_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn ItemName_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn CustomerPO_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn ThirdPartyItemCode_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn ThirdPartyPO_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn DueDate_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn UnitCurrency = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn Balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn OrderPrice_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn GlobalPrice_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn BoxQuantity_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn BoxWeight_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn OrderDate_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn Note_col = new System.Windows.Forms.DataGridViewTextBoxColumn();

                this.GridView_Search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
                    {
                         CheckBox_col
                        ,CustomerCode_col
                        ,CustomerPO_col
                        ,CustomerItemCode_col
                        ,TVCItemCode_col
                        ,ItemName_col
                        ,ReceiveNo_col
                        ,ThirdPartyItemCode_col
                        ,ThirdPartyPO_col
                        ,DueDate_col
                        ,OrderDate_col
                        ,UnitCurrency
                        ,Balance
                        ,OrderPrice_col
                        ,GlobalPrice_col
                        ,BoxQuantity_col
                        ,BoxWeight_col
                        ,Note_col
                    });
                this.GridView_Search.AutoGenerateColumns = false;

                //CHECKBOX
                CheckBox_col.HeaderText = "SELECT";
                CheckBox_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                CheckBox_col.Name = "Check_All";
                CheckBox_col.Width = 60;

                //CUSTOMER CODE
                CustomerCode_col.HeaderText = "CUSTOMER CODE";
                CustomerCode_col.DataPropertyName = "CUSTOMER_CODE";
                CustomerCode_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                CustomerCode_col.Name = "Customer_Code";
                CustomerCode_col.Width = 80;

                //RECEIVE NO
                ReceiveNo_col.HeaderText = "RECEIVE NO";
                ReceiveNo_col.DataPropertyName = "RECEIVE_NO";
                ReceiveNo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ReceiveNo_col.Name = "Receive_No";
                ReceiveNo_col.Width = 120;

                //CUSTOMER ITEM CODE
                CustomerItemCode_col.HeaderText = "CUSTOMER ITEM CODE";
                CustomerItemCode_col.DataPropertyName = "CUS_ITEM_CODE";
                CustomerItemCode_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                CustomerItemCode_col.Name = "Cus_Item_Code";
                CustomerItemCode_col.Width = 110;

                //TVC ITEM CODE
                TVCItemCode_col.HeaderText = "TVC ITEM CODE";
                TVCItemCode_col.DataPropertyName = "ITEM_CODE";
                TVCItemCode_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                TVCItemCode_col.Name = "Item_Code";
                TVCItemCode_col.Width = 110;

                //ITEM NAME
                ItemName_col.HeaderText = "ITEM NAME";
                ItemName_col.DataPropertyName = "ITEM_NAME";
                ItemName_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ItemName_col.Name = "Item_Name";
                ItemName_col.Width = 150;

                //CUSTOMER PO
                CustomerPO_col.HeaderText = "CUSTOMER PO";
                CustomerPO_col.DataPropertyName = "CUSTOMER_PO";
                CustomerPO_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                CustomerPO_col.Name = "Customer_PO";
                CustomerPO_col.Width = 140;

                //THIRD PARTY ITEM CODE
                ThirdPartyItemCode_col.HeaderText = "THIRD PARTY CODE";
                ThirdPartyItemCode_col.DataPropertyName = "THIRD_PARTY_ITEM_CODE";
                ThirdPartyItemCode_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ThirdPartyItemCode_col.Name = "ThirdParty_ItemCode";
                ThirdPartyItemCode_col.Width = 140;

                //THIRD PARTY PO
                ThirdPartyPO_col.HeaderText = "THIRD PARTY PO";
                ThirdPartyPO_col.DataPropertyName = "THIRD_PARTY_PO";
                ThirdPartyPO_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ThirdPartyPO_col.Name = "ThirdParty_PO";
                ThirdPartyPO_col.Width = 140;

                //ORDER DATE
                OrderDate_col.HeaderText = "ORDER DATE";
                OrderDate_col.DataPropertyName = "ORDER_DATE";
                OrderDate_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                OrderDate_col.Name = "Order_Date";
                OrderDate_col.Width = 100;

                //DUE DATE
                DueDate_col.HeaderText = "DUE DATE";
                DueDate_col.DataPropertyName = "DUE_DATE";
                DueDate_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DueDate_col.Name = "DueDate_PO";
                DueDate_col.Width = 100;

                //UNIT CURRENCY
                UnitCurrency.HeaderText = "UNIT CURRENCY";
                UnitCurrency.DataPropertyName = "UNIT_CURRENCY";
                UnitCurrency.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                UnitCurrency.Name = "Order_UnitCurrency";
                UnitCurrency.Width = 80;

                //BALANCE
                Balance.HeaderText = "QUANTITY BALANCE";
                Balance.DataPropertyName = "BALANCE";
                Balance.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Balance.Name = "Balance";
                Balance.Width = 110;

                //ORDER PRICE
                OrderPrice_col.HeaderText = "ORDER PRICE";
                OrderPrice_col.DataPropertyName = "ORDER_PRICE";
                OrderPrice_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                OrderPrice_col.Name = "Order_Price";
                OrderPrice_col.Width = 105;

                //GLOBAL PRICE
                GlobalPrice_col.HeaderText = "GLOBAL PRICE";
                GlobalPrice_col.DataPropertyName = "PRICE";
                GlobalPrice_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GlobalPrice_col.Name = "Global_Price";
                GlobalPrice_col.Width = 105;

                //BOX QUANTITY
                BoxQuantity_col.HeaderText = "QUANTITY PER BOX";
                BoxQuantity_col.DataPropertyName = "BOX_QUANTITY";
                BoxQuantity_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                BoxQuantity_col.Name = "Box_Quantity";
                BoxQuantity_col.Width = 100;

                //BOX WEIGHT
                BoxWeight_col.HeaderText = "BOX WEIGHT";
                BoxWeight_col.DataPropertyName = "WEIGHT";
                BoxWeight_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                BoxWeight_col.Name = "Box_Weight";
                BoxWeight_col.Width = 100;

                //NOTE
                Note_col.HeaderText = "NOTE";
                Note_col.DataPropertyName = "NOTE";
                Note_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Note_col.Name = "Note";
                Note_col.Width = 120;

                //Setting
                this.GridView_Search.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 8.25F, FontStyle.Bold);

                this.GridView_Search.Columns["Receive_No"].ReadOnly = true;                
                this.GridView_Search.Columns["Cus_Item_Code"].ReadOnly = true;
                this.GridView_Search.Columns["Item_Code"].ReadOnly = true;
                this.GridView_Search.Columns["Item_Name"].ReadOnly = true;
                this.GridView_Search.Columns["Customer_PO"].ReadOnly = true;
                this.GridView_Search.Columns["ThirdParty_ItemCode"].ReadOnly = true;
                this.GridView_Search.Columns["ThirdParty_ItemCode"].Visible = false;
                this.GridView_Search.Columns["ThirdParty_PO"].ReadOnly = true;
                this.GridView_Search.Columns["ThirdParty_PO"].Visible = false;
                this.GridView_Search.Columns["DueDate_PO"].ReadOnly = true;
                this.GridView_Search.Columns["Order_UnitCurrency"].ReadOnly = true;
                this.GridView_Search.Columns["Balance"].ReadOnly = true;
                this.GridView_Search.Columns["Order_Price"].ReadOnly = true;
                this.GridView_Search.Columns["Global_Price"].ReadOnly = true;
                this.GridView_Search.Columns["Global_Price"].Visible = false;
                this.GridView_Search.Columns["Box_Quantity"].ReadOnly = true;
                this.GridView_Search.Columns["Box_Quantity"].Visible = false;
                this.GridView_Search.Columns["Box_Weight"].ReadOnly = true;
                this.GridView_Search.Columns["Box_Weight"].Visible = false;
                this.GridView_Search.Columns["Note"].ReadOnly = true;

                this.GridView_Search.Columns["Order_UnitCurrency"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //CUSTOMER CODE
                this.GridView_Search.Columns["Customer_Code"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //ORDER DATE
                this.GridView_Search.Columns["Order_Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //DUE DATE
                this.GridView_Search.Columns["DueDate_PO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //QUANTITY ORDER
                this.GridView_Search.Columns["Balance"].DefaultCellStyle.Format = "#,##0.####";
                this.GridView_Search.Columns["Balance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                this.GridView_Search.Columns["Order_Price"].DefaultCellStyle.Format = "#,##0.####";
                this.GridView_Search.Columns["Order_Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                this.GridView_Search.Columns["Global_Price"].DefaultCellStyle.Format = "#,##0.####";
                this.GridView_Search.Columns["Global_Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                this.GridView_Search.Columns["Box_Quantity"].DefaultCellStyle.Format = "#,##0.####";
                this.GridView_Search.Columns["Box_Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                this.GridView_Search.Columns["Box_Weight"].DefaultCellStyle.Format = "#,##0.####";
                this.GridView_Search.Columns["Box_Weight"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        public void SettingInit(string _searchValue, string _customerPO,string _customerCode, Boolean _isCurrencyJPY, int _companyValue, int _typeSearchValue)
        {
            if (_caseSearch == "btnSearch_ItemCode")
            {
                GridView_Search.DataSource = _searchDAO.GetInfoItemWithCurrency(_searchValue
                                                                              , _customerPO
                                                                              , _customerCode
                                                                              , _isCurrencyJPY
                                                                              , _companyValue
                                                                              , _typeSearchValue);
            }
        }

        //Draw number order
        private void GridView_Search_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            int firstDisplayedCellIndex = GridView_Search.FirstDisplayedCell.RowIndex;
            int lastDisplayedCellIndex = firstDisplayedCellIndex + GridView_Search.DisplayedRowCount(true);

            var centerFormat = new StringFormat()
            {
                //Right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);

            Graphics Graphics = GridView_Search.CreateGraphics();
            int measureFirstDisplayed = (int)(Graphics.MeasureString(firstDisplayedCellIndex.ToString(), GridView_Search.Font).Width);
            int measureLastDisplayed = (int)(Graphics.MeasureString(lastDisplayedCellIndex.ToString(), GridView_Search.Font).Width);

            int rowHeaderWitdh = System.Math.Max(measureFirstDisplayed, measureLastDisplayed);
            GridView_Search.RowHeadersWidth = rowHeaderWitdh + 25;
        }

        private void GridView_Search_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int columnIndex = 0;
            int rowIndex = 0;
            var senderGrid = (DataGridView)sender;

            if (e.RowIndex >= 0)
            {
                rowIndex = e.RowIndex;
                columnIndex = e.ColumnIndex;

                if (GridView_Search.Columns[columnIndex] is DataGridViewCheckBoxColumn)
                {
                    //DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)GridView_Search.Rows[rowIndex].Cells[0];
                    //if (chk.Value == chk.FalseValue || chk.Value == null)
                    //{
                    //    chk.Value = chk.TrueValue;
                    //}
                    //else
                    //{
                    //    chk.Value = chk.FalseValue;
                    //}
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if(_listItemCodeInfo == null)
            {
                _listItemCodeInfo = new List<ItemCodeInfo>();
            }
            else
            {
                _listItemCodeInfo.Clear();
            }

            foreach (DataGridViewRow row in GridView_Search.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Check_All"].Value))
                {
                    _listItemCodeInfo.Add(new ItemCodeInfo {
                        CustomerCode = row.Cells["Customer_Code"].Value.ToString(),
                        ReceiveNo = row.Cells["Receive_No"].Value.ToString(),
                        Customer_ItemCode = row.Cells["Cus_Item_Code"].Value.ToString(),
                        TVC_ItemCode = row.Cells["Item_Code"].Value.ToString(),
                        Item_Name = row.Cells["Item_Name"].Value.ToString(),
                        CustomerPO = row.Cells["Customer_PO"].Value.ToString(),
                        ThirdPartyItemCode = row.Cells["ThirdParty_ItemCode"].Value.ToString(),
                        ThirdPartyPO = row.Cells["ThirdParty_PO"].Value.ToString(),
                        Order_Date = Convert.ToDateTime(row.Cells["Order_Date"].Value),
                        DueDate_PO = Convert.ToDateTime(row.Cells["DueDate_PO"].Value),
                        Balance = Convert.ToDecimal(row.Cells["Balance"].Value),
                        OrderUnitCurrency = row.Cells["Order_UnitCurrency"].Value.ToString(),
                        OrderPrice = Convert.ToDecimal(row.Cells["Order_Price"].Value),
                        CustomerPrice = Convert.ToDecimal(row.Cells["Global_Price"].Value),
                        BoxQuantity = Convert.ToDecimal(row.Cells["Box_Quantity"].Value),
                        Weight = Convert.ToDecimal(row.Cells["Box_Weight"].Value)
                    });
                }
            }
            this.Close();
        }

        private void GridView_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

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

        private void picBox_Close_Click(object sender, EventArgs e)
        {
            //string exitMessageText = "Are you sure you want to exit?";
            //string exitCaption = "Confirm";
            //MessageBoxButtons button = MessageBoxButtons.YesNo;
            //DialogResult res = MessageBox.Show(exitMessageText, exitCaption, button, MessageBoxIcon.Exclamation);
            //if (res == DialogResult.Yes)
            //{
            //    this.Close();
            //    Application.Exit();
            //}
            this.Close();
        }

        private void picBox_Max_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.CenterScreen;
                picBox_Max.Image = Properties.Resources.Maximize_window;
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                picBox_Max.Image = Properties.Resources.Zoom_full;
            }

        }

        private void panelTop_DoubleClick(object sender, EventArgs e)
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

        private void radCurrencyJPY_CheckedChanged(object sender, EventArgs e)
        {
            if (radCurrencyJPY.Checked == true)
            {
                txtKeySearch_Validated(sender, e);
            }
        }

        private void radCurrencyUSD_CheckedChanged(object sender, EventArgs e)
        {
            if (radCurrencyUSD.Checked == true)
            {
                txtKeySearch_Validated(sender, e);
            }
        }

        private void cbxCompany_TextChanged(object sender, EventArgs e)
        {
            if (cboxCompany.Items.Count == 3)
            { 
            GridView_Search.DataSource = _searchDAO.GetInfoItemWithCurrency(txtKeySearch.Text.Trim()
                                                                           , txtCustomerPO.Text.Trim()
                                                                           , _customerCode, IsCurrencyJapanese()
                                                                           , Convert.ToInt32(cboxCompany.SelectedValue)
                                                                           , Convert.ToInt32(cboxTypeSearch.SelectedValue));
            }
        }

        private void CboxTypeSearch_TextChanged(object sender, EventArgs e)
        {
            if (cboxCompany.Items.Count == 2)
            {
                GridView_Search.DataSource = _searchDAO.GetInfoItemWithCurrency(txtKeySearch.Text.Trim()
                                                                           , txtCustomerPO.Text.Trim()
                                                                           , _customerCode, IsCurrencyJapanese()
                                                                           , Convert.ToInt32(cboxCompany.SelectedValue)
                                                                           , Convert.ToInt32(cboxTypeSearch.SelectedValue));
            }
        }

        private void txtKeySearch_Validated(object sender, EventArgs e)
        {
            GridView_Search.DataSource = _searchDAO.GetInfoItemWithCurrency(txtKeySearch.Text.Trim()
                                                                               , txtCustomerPO.Text.Trim()
                                                                               , _customerCode, IsCurrencyJapanese()
                                                                               , Convert.ToInt32(cboxCompany.SelectedValue)
                                                                               , Convert.ToInt32(cboxTypeSearch.SelectedValue));
        }

        private void txtCustomerPO_Validated(object sender, EventArgs e)
        {
            GridView_Search.DataSource = _searchDAO.GetInfoItemWithCurrency(txtKeySearch.Text.Trim()
                                                                           , txtCustomerPO.Text.Trim()
                                                                           , _customerCode, IsCurrencyJapanese()
                                                                           , Convert.ToInt32(cboxCompany.SelectedValue)
                                                                           , Convert.ToInt32(cboxTypeSearch.SelectedValue));
        }

        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rowData in GridView_Search.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)rowData.Cells["Check_All"];
                chk.Value = !(chk.Value == null ? false : (bool)chk.Value);
            }
        }
    }
}
