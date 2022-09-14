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
    public partial class Form_Search_ShippingNo : Form
    {
        public static string _caseSearch;

        public static string _customerCode;

        public static string _unitCurrency = "";
        public static DateTime _dateCreateInvoice;
                                
        public ShippingInfo _shippingInfo;

        public SEARCH_DAO _searchDAO;

        public Form_Search_ShippingNo(string _keySearch)
        {
            InitializeComponent();

            //Setting no title but have border
            this.ControlBox = false;
            this.Text = String.Empty;

            //
            _caseSearch = _keySearch;
        }

        private void Form_Search_Load(object sender, EventArgs e)
        {
            string _searchValue = txtKeySearch.Text.Trim();

            _searchDAO = new SEARCH_DAO();

            _shippingInfo = new ShippingInfo();

            //
            AddColumnGridView();

            //Get data init
            SettingInit(_searchValue);

        }

        public void AddColumnGridView()
        {
            if (_caseSearch == "btnSearchSingle_ShippingNo")
            {
                System.Windows.Forms.DataGridViewCheckBoxColumn CheckBox_col = new System.Windows.Forms.DataGridViewCheckBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn IssuedTo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn ShipTo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn ShippingNo_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn UnitCurrency_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn ETD_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
                System.Windows.Forms.DataGridViewTextBoxColumn LockStatus_col = new System.Windows.Forms.DataGridViewTextBoxColumn();

                this.GridView_Search.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
                    {
                         CheckBox_col
                        ,IssuedTo_col
                        ,ShipTo_col
                        ,ShippingNo_col
                        ,UnitCurrency_col
                        ,ETD_col
                        ,LockStatus_col
                    });
                this.GridView_Search.AutoGenerateColumns = false;

                //CHECKBOX
                CheckBox_col.HeaderText = "SELECT";
                CheckBox_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                CheckBox_col.Name = "Check_All";
                CheckBox_col.Width = 60;

                //ISSUED TO
                IssuedTo_col.HeaderText = "ISSUED TO";
                IssuedTo_col.DataPropertyName = "ISSUEDTO_CUSTOMER_CODE";
                IssuedTo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                IssuedTo_col.Name = "Issued_To";
                IssuedTo_col.Width = 120;

                //SHIP TO
                ShipTo_col.HeaderText = "SHIP TO";
                ShipTo_col.DataPropertyName = "SHIPTO_CUSTOMER_CODE";
                ShipTo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ShipTo_col.Name = "Ship_To";
                ShipTo_col.Width = 120;

                //SHIPPING NO
                ShippingNo_col.HeaderText = "SHIPPING NO";
                ShippingNo_col.DataPropertyName = "SHIPPING_NO";
                ShippingNo_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ShippingNo_col.Name = "Shipping_No";
                ShippingNo_col.Width = 140;

                //UNIT CURRENCY
                UnitCurrency_col.HeaderText = "UNIT CURRENCY";
                UnitCurrency_col.DataPropertyName = "UNIT_CURRENCY";
                UnitCurrency_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                UnitCurrency_col.Name = "Unit_Currency";
                UnitCurrency_col.Width = 120;

                //ETD
                ETD_col.HeaderText = "ETD";
                ETD_col.DataPropertyName = "ETD";
                ETD_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ETD_col.Name = "ETD";
                ETD_col.Width = 100;

                //LOCK STATUS
                LockStatus_col.HeaderText = "LOCK STATUS";
                LockStatus_col.DataPropertyName = "LOCK_STATUS";
                LockStatus_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                LockStatus_col.Name = "Lock_Status";
                LockStatus_col.Width = 100;

                //Setting
                this.GridView_Search.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 8.25F);

                this.GridView_Search.Columns["Issued_To"].ReadOnly = true;
                this.GridView_Search.Columns["Ship_To"].ReadOnly = true;
                this.GridView_Search.Columns["Shipping_No"].ReadOnly = true;
                this.GridView_Search.Columns["ETD"].ReadOnly = true;
                this.GridView_Search.Columns["Lock_Status"].ReadOnly = true;

                //ISSUED TO
                this.GridView_Search.Columns["Issued_To"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //SHIP TO
                this.GridView_Search.Columns["Ship_To"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //SHIPPING NO
                this.GridView_Search.Columns["Shipping_No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //UNIT CURRENCY
                this.GridView_Search.Columns["Unit_Currency"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //ETD
                this.GridView_Search.Columns["ETD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //LOCK STATUS
                this.GridView_Search.Columns["Lock_Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.GridView_Search.Columns["Lock_Status"].ValueType = typeof(string);
            }
        }

        public void SettingInit(string _searchValue)
        {
            if (_caseSearch == "btnSearchSingle_ShippingNo")
            {
                GridView_Search.DataSource = _searchDAO.GetInfoShippingNo(_searchValue);
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
            if (_listShippingInfo == null)
            {
                _listShippingInfo = new List<ShippingInfo>();
            }
            else
            {
                _listShippingInfo.Clear();
            }

            foreach (DataGridViewRow row in GridView_Search.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Check_All"].Value))
                {
                    _listShippingInfo.Add(new ShippingInfo
                    {
                        ShippingNo = row.Cells["Shipping_No"].Value.ToString()
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

        private void Form_Invoice_PL_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
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

        private void txtKeySearch_Validated(object sender, EventArgs e)
        {
            GridView_Search.DataSource = _searchDAO.GetInfoShippingNo(txtKeySearch.Text);
        }
    }
}
