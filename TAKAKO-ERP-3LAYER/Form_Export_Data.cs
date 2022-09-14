using OfficeOpenXml;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TAKAKO_ERP_3LAYER.DAO;
using static TAKAKO_ERP_3LAYER.Common;
using TAKAKO_ERP_3LAYER.DAL;
using System.Diagnostics;

namespace TAKAKO_ERP_3LAYER
{
    public partial class Form_Export_Data : Form
    {
        public EXPORT_DAO _exportDAO;
        public SYSTEM_DAL _systemDAL;
        public Form_Export_Data(SYSTEM_DAL _tempSystem)
        {
            InitializeComponent();

            //Setting no title but have border
            this.ControlBox = false;
            this.Text = String.Empty;

            _systemDAL = _tempSystem;
        }

        private void Form_Export_Data_Load(object sender, EventArgs e)
        {
            //
            _exportDAO = new EXPORT_DAO();

            //
            SetInit_CbTemplate();
            cbTemplate.SelectedIndex = 1;

            //Setting init date Renueve
            dtpRevenue.Value = DateTime.Now;
        }

        private void SetInit_CbTemplate()
        {
            cbTemplate.DisplayMember = "Text";
            cbTemplate.ValueMember = "Value";

            var items = new[] {
                new { Text = "*------------PU-----------*",     Value = "" },
                new { Text = "1. Invoice & Packing List",       Value = "1" },
                new { Text = "2. Revenue",                      Value = "2" },
                new { Text = "*------------ACC----------*",     Value = "" },
                new { Text = "3. Internal Accounting Report",   Value = "3" },
                new { Text = "4. Invoice Detail",               Value = "4" },
                new { Text = "*------------PC-----------*",     Value = "" },
                new { Text = "5. PO Detail",                    Value = "5" },
                new { Text = "6. Technical support exp",        Value = "6" }
            };

            cbTemplate.DataSource = items;
        }

        private void Form_ExportData_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
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
        private void picBox_Close_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn thoát khỏi chương trình?", "Xác nhận", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
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
            tt.SetToolTip(picBox_BackToMain, "Trở về màn hình chính");
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

        //private void Export_Revenue(string path)
        //{
        //    DateTime _createDateFrom = dtpDueDateFrom.Value;
        //    DateTime _createDateTo = dtpDueDateTo.Value;
        //    DataTable _tempTable = new DataTable();
        //    try
        //    {
        //        //Create fileinfo object of an excel file
        //        FileInfo _fileInfo = new FileInfo(path);

        //        //Create a new Excel package from the file
        //        using (ExcelPackage _excelPackage = new ExcelPackage(_fileInfo))
        //        {
        //            ExcelWorksheet _revenue = _excelPackage.Workbook.Worksheets[1];

        //            //Rename sheet
        //            _revenue.Name = _createDateFrom.ToString("dd/MM/yyyy") + "-"
        //                          + _createDateTo.ToString("dd/MM/yyyy");

        //            _revenue.Cells["A1"].Value = "List exported Invoice \n" +
        //                                          _createDateFrom.ToString("dd/MM/yyyy") + "-"
        //                                        + _createDateTo.ToString("dd/MM/yyyy");

        //            _tempTable = _exportDAO.GetTotalData_Revenue(_revenue);

        //            //int index = 1;
        //            foreach (DataRow row in _tempTable.Rows)
        //            {
                        
        //            }


        //            //Focus A1, sheet Revenue
        //            _revenue.Select("A1");

        //            byte[] bin = _excelPackage.GetAsByteArray();

        //            //Create a SaveFileDialog instance with some properties
        //            SaveFileDialog _saveFileDialog = new SaveFileDialog();
        //            _saveFileDialog.Title = "Save file Revenue";
        //            _saveFileDialog.Filter = "Excel files|*.xlxs|All files|*.*";
        //            _saveFileDialog.FileName = "Revenue_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";

        //            //Check if user clicked the save button
        //            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
        //            {
        //                //write the file to the disk
        //                File.WriteAllBytes(_saveFileDialog.FileName, bin);
        //                MessageBox.Show("Xuất báo cáo doanh thu thành công!", "Hoàn thành", MessageBoxButtons.OK,MessageBoxIcon.Information);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi: " + ex.Message,"Lỗi",MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private Boolean Checkerror()
        {
            if (cbTemplate.SelectedIndex == 3)
            {
            }

            return true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dtpDueDateFrom.Value = DateTime.Now;
            dtpDueDateTo.Value = DateTime.Now;
            txtInvoiceNo.Text = String.Empty;
            cbTemplate.SelectedIndex = 1;
        }

        private void btnExport_Test_Click(object sender, EventArgs e)
        {
             DateTime _ETDFrom = dtpDueDateFrom.Value;
            DateTime _ETDTo = dtpDueDateTo.Value;
            string _revenue = dtpRevenue.Value.ToString("MM/yyyy");
            string _invoiceNo = txtInvoiceNo.Text;
            string _fileName = "";

            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Chose Template Export";
            theDialog.Filter = "Files Excel|*.xlsx";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                string _filePath = theDialog.FileName;

                //Create fileinfo object of an excel file
                FileInfo _fileInfo = new FileInfo(_filePath);

                //Create a new Excel package from the file
                using (ExcelPackage _excelPackage = new ExcelPackage(_fileInfo))
                {
                    //Renueve
                    #region Export Renueve
                    if (Convert.ToInt32(cbTemplate.SelectedValue) == 2)
                    {
                        int totalRowCount = 0;
                        ExcelWorksheet _renueveSheet = _excelPackage.Workbook.Worksheets[1];

                        //Total Revenue
                        DataTable _totalData = _exportDAO.GetTotalData_Revenue(_revenue);
                        DataTable _totalTable = new DataTable();
                        totalRowCount = _totalData.Rows.Count;

                        //Detail Revenue
                        DataTable _detailData = _exportDAO.GetDetailData_Revenue(_revenue);
                        DataTable _detailTable = new DataTable();
                        int detailRowCount = _detailData.Rows.Count;

                        //Setting Table Total Revenue
                        _totalTable.Columns.Add("CUSTOMER_CODE");
                        _totalTable.Columns.Add("CUSTOMER_NAME");
                        _totalTable.Columns.Add("COLUMN_D");
                        _totalTable.Columns.Add("QUANTITY");
                        _totalTable.Columns.Add("SUM_AMOUNT_TVC1");
                        _totalTable.Columns["SUM_AMOUNT_TVC1"].DataType = typeof(Decimal);
                        _totalTable.Columns.Add("SUM_AMOUNT_TVC2");
                        _totalTable.Columns["SUM_AMOUNT_TVC2"].DataType = typeof(Decimal);

                        //Setting Table Detail Revenue
                        _detailTable.Columns.Add("REVENUE");
                        _detailTable.Columns.Add("CUSTOMER_CODE");
                        _detailTable.Columns.Add("SAILING_DATE");
                        _detailTable.Columns["SAILING_DATE"].DataType = typeof(DateTime);
                        _detailTable.Columns.Add("INVOICE_NO");
                        _detailTable.Columns.Add("SHIPPING_NO");
                        _detailTable.Columns.Add("INVOICE_DATE");
                        _detailTable.Columns["INVOICE_DATE"].DataType = typeof(DateTime);
                        _detailTable.Columns.Add("AMOUNT_JPY");
                        _detailTable.Columns["AMOUNT_JPY"].DataType = typeof(Decimal);
                        _detailTable.Columns.Add("AMOUNT_USD");
                        _detailTable.Columns["AMOUNT_USD"].DataType = typeof(Decimal);
                        _detailTable.Columns.Add("USD_RATE");
                        _detailTable.Columns["USD_RATE"].DataType = typeof(Decimal);
                        _detailTable.Columns.Add("COMPANY_NAME");
                        _detailTable.Columns.Add("NOTE");

                        //Setting Detail Data Revenue
                        if (detailRowCount > 0)
                        {
                            DataRow drLocal = null;
                            foreach (DataRow dr in _detailData.Rows)
                            {
                                drLocal = _detailTable.NewRow();
                                //REVENUE
                                if (dr["REVENUE"] != null)
                                {
                                    drLocal["REVENUE"] = Convert.ToString(dr["REVENUE"]);
                                } else
                                {
                                    drLocal["REVENUE"] = String.Empty;
                                }
                                //CUSTOMER CODE
                                drLocal["CUSTOMER_CODE"] = Convert.ToString(dr["CUSTOMER_CODE"]);
                                //SAILING_DATE
                                if (dr["ETD"] != null)
                                {
                                    drLocal["SAILING_DATE"] = Convert.ToDateTime(dr["ETD"]);
                                } else
                                {
                                    drLocal["SAILING_DATE"] = String.Empty;
                                }
                                //SHIPPING_NO
                                drLocal["SHIPPING_NO"] = Convert.ToString(dr["SHIPPING_NO"]);
                                //INVOICE_NO
                                drLocal["INVOICE_NO"] = dr["INVOICE_NO"].ToString();
                                //INVOICE_DATE
                                drLocal["INVOICE_DATE"] = dr["DATE_CREATE"].ToString();
                                if (Convert.ToString(dr["UNIT_CURRENCY"]) == "JPY")
                                {
                                    //AMOUNT_JPY
                                    drLocal["AMOUNT_JPY"] = Convert.ToDecimal(dr["AMOUNT"]);
                                    //AMOUNT_USD
                                    drLocal["AMOUNT_USD"] = 0;
                                } else if (Convert.ToString(dr["UNIT_CURRENCY"]) == "USD")
                                {
                                    decimal Amount = Convert.ToDecimal(dr["AMOUNT"]);
                                    decimal USD_Rate = Convert.ToDecimal(dr["USD_RATE"]);
                                    //AMOUNT_JPY
                                    drLocal["AMOUNT_JPY"] = Amount* USD_Rate;
                                    //AMOUNT_USD
                                    drLocal["AMOUNT_USD"] = Amount;
                                }
                                //USD_RATE
                                drLocal["USD_RATE"] = Convert.ToDecimal(dr["USD_RATE"]);
                                //COMPANY_NAME
                                if (Convert.ToString(dr["COMPANY_CODE"]) == "00001")
                                { 
                                    drLocal["COMPANY_NAME"] = "TVC1";
                                } else if (Convert.ToString(dr["COMPANY_CODE"]) == "00002")
                                {
                                    drLocal["COMPANY_NAME"] = "TVC2";
                                } else
                                {
                                    drLocal["COMPANY_NAME"] = String.Empty;
                                }
                                    
                                //NOTE
                                    drLocal["NOTE"] = Convert.ToString(dr["NOTE"]);

                                _detailTable.Rows.Add(drLocal);
                            }

                            if (detailRowCount > 2)
                            {
                                _renueveSheet.InsertRow(12, detailRowCount - 2, 11);
                                //Coppy Style
                                for (int i = 1; i < detailRowCount; i++)
                                {
                                    _renueveSheet.Cells[11, 1, 11, 10].Copy(_renueveSheet.Cells[11 + i, 1, 11 + i, 10]);
                                }
                            }

                            //Load data from datatable to excel
                            _renueveSheet.Cells["A11"].LoadFromDataTable(_detailTable, false);
                        }

                        //Setting Total Data Revenue
                        if (totalRowCount > 0)
                        {
                            DataRow drLocal = null;
                            foreach (DataRow dr in _totalData.Rows)
                            {
                                drLocal = _totalTable.NewRow();
                                //CUSTOMER_CODE
                                drLocal["CUSTOMER_CODE"] = Convert.ToString(dr["CUSTOMER_CODE"]);
                                //CUSTOMER_NAME
                                drLocal["CUSTOMER_NAME"] = Convert.ToString(dr["CUSTOMER_NAME1"]);
                                //COLUMN_D
                                drLocal["COLUMN_D"] = String.Empty;
                                //COLUMN_E
                                drLocal["QUANTITY"] = Convert.ToDecimal(dr["SUM_QUANTITY"]);
                                //SUM_AMOUNT_TVC1
                                drLocal["SUM_AMOUNT_TVC1"] = Convert.ToDecimal(dr["SUM_JPY_TVC1"]);
                                //SUM_AMOUNT_TVC2
                                drLocal["SUM_AMOUNT_TVC2"] = Convert.ToDecimal(dr["SUM_JPY_TVC2"]);

                                _totalTable.Rows.Add(drLocal);
                            }

                            if (totalRowCount > 2)
                            {
                                _renueveSheet.InsertRow(6, totalRowCount - 2, 5);
                            }

                            //Load data from datatable to excel
                            _renueveSheet.Cells["B5"].LoadFromDataTable(_totalTable, false);
                        }

                        //Setting Sheet name
                        _renueveSheet.Name = dtpRevenue.Value.ToString("MM.yyyy");
                        //Setting Header
                        _renueveSheet.Cells["A1"].Value = "REVENUE " + _revenue;
                        //Focus A1, sheet Invoice
                        _renueveSheet.Select("A1");
                        //Set filename output
                        _fileName = "Revenue_" + dtpRevenue.Value.ToString("MM.yyyy") + "_";
                    }
                    #endregion

                    //Internal Accounting Report
                    #region Export Internal Accounting Report
                    else if (Convert.ToInt32(cbTemplate.SelectedValue) == 3)
                    {
                        ExcelWorksheet _internalAccReport = _excelPackage.Workbook.Worksheets[1];

                        DataTable _tempTable = _exportDAO.GetData_InternalACC(_revenue);

                        DataTable _tableOutput = new DataTable();
                        _tableOutput.Columns.Add("COMPANY_CODE");
                        _tableOutput.Columns.Add("CUSTOMER_CODE");
                        _tableOutput.Columns.Add("INVOICE_NO");
                        _tableOutput.Columns.Add("DATE_CREATE");
                        _tableOutput.Columns["DATE_CREATE"].DataType = typeof(DateTime);
                        _tableOutput.Columns.Add("ETD");
                        _tableOutput.Columns["ETD"].DataType = typeof(DateTime);
                        _tableOutput.Columns.Add("DOCUMENT_NO");
                        _tableOutput.Columns.Add("DESCRIPTION");
                        _tableOutput.Columns.Add("TOTAL_AMOUNT_USD");
                        _tableOutput.Columns["TOTAL_AMOUNT_USD"].DataType = typeof(Decimal);
                        _tableOutput.Columns.Add("TOTAL_AMOUNT_JPY");
                        _tableOutput.Columns["TOTAL_AMOUNT_JPY"].DataType = typeof(Decimal);
                        _tableOutput.Columns.Add("TEMP_AMOUNT_JPY");
                        _tableOutput.Columns["TEMP_AMOUNT_JPY"].DataType = typeof(Decimal);
                        _tableOutput.Columns.Add("EX.RATE");
                        _tableOutput.Columns["EX.RATE"].DataType = typeof(Decimal);

                        //Count querry SQL
                        int _tempTableCount = _tempTable.Rows.Count;

                        if (_tempTableCount > 0)
                        {
                            foreach (DataRow dr in _tempTable.Rows)
                            {
                                DataRow drLocal = _tableOutput.NewRow();
                                if (Convert.ToString(dr["COMPANY_CODE"]) == "00001")
                                { 
                                    drLocal["COMPANY_CODE"] = "TVC1";
                                } else if (Convert.ToString(dr["COMPANY_CODE"]) == "00002")
                                {
                                    drLocal["COMPANY_CODE"] = "TVC2";
                                }
                                drLocal["CUSTOMER_CODE"] = dr["CUSTOMER_CODE"];
                                drLocal["INVOICE_NO"] = dr["INVOICE_NO"];
                                drLocal["DATE_CREATE"] = dr["DATE_CREATE"];
                                drLocal["ETD"] = dr["ETD"];
                                drLocal["DOCUMENT_NO"] = dr["SHIPPING_NO"];
                                drLocal["DESCRIPTION"] = "TVC Export to " + dr["CUSTOMER_CODE"];
                                if (dr["UNIT_CURRENCY"].ToString().ToUpper() == "USD")
                                { 
                                    drLocal["EX.RATE"] = dr["USD_RATE"];
                                    drLocal["TOTAL_AMOUNT_USD"] = dr["AMOUNT"];
                                } else if (dr["UNIT_CURRENCY"].ToString().ToUpper() == "JPY")
                                {
                                    drLocal["EX.RATE"] = 0;
                                    drLocal["TEMP_AMOUNT_JPY"] = dr["AMOUNT"];
                                }
                                _tableOutput.Rows.Add(drLocal);
                            }

                            _internalAccReport.InsertRow(3, _tempTableCount - 1, 2);

                            //Load data from datatable to excel
                            _internalAccReport.Cells["A2"].LoadFromDataTable(_tableOutput, false);
                            for (int i = 2; i < _tempTableCount + 2; i++)
                            {
                                _internalAccReport.Cells["I" + i].Formula = "=IF(K" + i + ">0,ROUND(K" + i + "*H" + i + ",0),J" + i + ")";
                            }
                        }

                        //Focus A1
                        _internalAccReport.Select("A1");
                        //Set filename output
                        _fileName = "Internal ACC_" + dtpRevenue.Value.ToString("MM.yyyy") + "_";
                    }
                    #endregion
                    //Invoice Detail
                    else if (Convert.ToInt32(cbTemplate.SelectedValue) == 4)
                    {
                        #region Export Invoice Detail
                        ExcelWorksheet _invoiceDetail = _excelPackage.Workbook.Worksheets[0];

                        DataTable _tempTable = _exportDAO.GetData_DetailInvoice(_revenue);

                        DataTable _tableOutput = new DataTable();
                        _tableOutput.Columns.Add("CUSTOMER_CODE");
                        _tableOutput.Columns.Add("CUSTOMER_ITEM_CODE");
                        _tableOutput.Columns.Add("EF_INVENTORY_ID");
                        _tableOutput.Columns.Add("SOLOMON_CODE");
                        _tableOutput.Columns.Add("CODE_AND_NAME");
                        _tableOutput.Columns.Add("E_COLUMN");
                        _tableOutput.Columns.Add("QUANTITY");
                        _tableOutput.Columns["QUANTITY"].DataType = typeof(Decimal);
                        _tableOutput.Columns.Add("AMOUNT");
                        _tableOutput.Columns["AMOUNT"].DataType = typeof(Decimal);
                        _tableOutput.Columns.Add("PRICE");
                        _tableOutput.Columns["PRICE"].DataType = typeof(Decimal);
                        _tableOutput.Columns.Add("I_COLUMN");
                        _tableOutput.Columns.Add("J_COLUMN");
                        _tableOutput.Columns.Add("K_COLUMN");
                        _tableOutput.Columns.Add("L_COLUMN");
                        _tableOutput.Columns.Add("REASON_CODE");
                        _tableOutput.Columns.Add("SITE_ID");
                        _tableOutput.Columns.Add("O_COLUMN");
                        _tableOutput.Columns.Add("P_COLUMN");
                        _tableOutput.Columns.Add("Q_COLUMN");
                        _tableOutput.Columns.Add("R_COLUMN");
                        _tableOutput.Columns.Add("S_COLUMN");
                        _tableOutput.Columns.Add("T_COLUMN");
                        _tableOutput.Columns.Add("U_COLUMN");
                        _tableOutput.Columns.Add("V_COLUMN");
                        _tableOutput.Columns.Add("W_COLUMN");
                        _tableOutput.Columns.Add("X_COLUMN");
                        _tableOutput.Columns.Add("INVOICE_NO");
                        _tableOutput.Columns.Add("TVC_NUMBER");
                        _tableOutput.Columns.Add("AA_COLUMN");
                        _tableOutput.Columns.Add("INVOICE_DATE");
                        _tableOutput.Columns["INVOICE_DATE"].DataType = typeof(DateTime);
                        _tableOutput.Columns.Add("DUE_DATE");
                        _tableOutput.Columns["DUE_DATE"].DataType = typeof(DateTime);
                        _tableOutput.Columns.Add("EX.RATE");
                        _tableOutput.Columns["EX.RATE"].DataType = typeof(Decimal);
                        _tableOutput.Columns.Add("SHIP_TO");
                        _tableOutput.Columns.Add("SHIP_VIA");
                        _tableOutput.Columns.Add("PORT_OF_DESTINATION");

                        //Count querry SQL
                        int _tempTableCount = _tempTable.Rows.Count;

                        if (_tempTableCount > 0)
                        {
                            foreach (DataRow dr in _tempTable.Rows)
                            {
                                DataRow drLocal = _tableOutput.NewRow();
                                drLocal["CUSTOMER_CODE"] = dr["CUSTOMER_CODE"];
                                drLocal["CUSTOMER_ITEM_CODE"] = dr["CUS_ITEM_CODE"];
                                drLocal["EF_INVENTORY_ID"] = dr["TVC_ITEM_CODE"];
                                drLocal["SOLOMON_CODE"] = dr["ACC_ITEM_CODE"];
                                drLocal["CODE_AND_NAME"] = dr["CODE_AND_NAME"];
                                drLocal["QUANTITY"] = dr["QUANTITY"];
                                drLocal["AMOUNT"] = dr["AMOUNT"];
                                drLocal["PRICE"] = dr["PRICE"];
                                string _reasonCode = dr["REASON_CODE"].ToString().ToUpper();
                                drLocal["REASON_CODE"] = _reasonCode;
                                if (_reasonCode == "XBTP1")
                                { 
                                    drLocal["SITE_ID"] = "TVC1";
                                    drLocal["TVC_NUMBER"] = "1";
                                } else if (_reasonCode == "XBTP2")
                                {
                                    drLocal["SITE_ID"] = "TVC2";
                                    drLocal["TVC_NUMBER"] = "2";
                                }
                                drLocal["INVOICE_NO"] = dr["INVOICE_NO"];
                                drLocal["INVOICE_DATE"] = dr["DATE_CREATE"];
                                drLocal["DUE_DATE"] = dr["ETD"];
                                drLocal["CUSTOMER_CODE"] = dr["CUSTOMER_CODE"];
                                drLocal["EX.RATE"] = dr["USD_RATE"];
                                drLocal["SHIP_TO"] = dr["SHIPTO_CUSTOMER_CODE"];
                                drLocal["SHIP_VIA"] = dr["SHIP_VIA"];
                                drLocal["PORT_OF_DESTINATION"] = dr["PORT_OF_DESTINATION"];
                                _tableOutput.Rows.Add(drLocal);
                            }

                            _invoiceDetail.InsertRow(3, _tempTableCount - 1, 2);

                            //Load data from datatable to excel
                            _invoiceDetail.Cells["A2"].LoadFromDataTable(_tableOutput, false);
                        }

                        //Focus A1
                        _invoiceDetail.Select("A1");
                        //Set filename output
                        _fileName = "Detail_" + dtpRevenue.Value.ToString("MM.yyyy") + "_";
                        #endregion
                    }
                    //PO Detail
                    else if (Convert.ToInt32(cbTemplate.SelectedValue) == 5)
                    {
                        #region Export PO Detail
                        ExcelWorksheet _PODetail = _excelPackage.Workbook.Worksheets[1];

                        //_tempTable = _exportDAO.GetData_Revenue(_ETDFrom, _ETDTo);

                        //Focus A1, sheet Invoice
                        _PODetail.Select("A1");
                        #endregion
                    }
                    //Technical support exp
                    else if (Convert.ToInt32(cbTemplate.SelectedValue) == 6)
                    {
                        #region Export Technical Support Exp
                        ExcelWorksheet _technicalSupportExp = _excelPackage.Workbook.Worksheets[1];

                        DataTable _tempTable = _exportDAO.GetData_TechnicalSupport(_ETDFrom, _ETDTo);

                        DataTable _tableOutput = new DataTable();
                        _tableOutput.Columns.Add("DATE_CREATE");
                        _tableOutput.Columns["DATE_CREATE"].DataType = typeof(DateTime);
                        _tableOutput.Columns.Add("INVOICE_NO");
                        _tableOutput.Columns.Add("TVC_ITEM_CODE");
                        _tableOutput.Columns.Add("PART_DESCRIPTION");
                        _tableOutput.Columns.Add("QUANTITY");
                        _tableOutput.Columns["QUANTITY"].DataType = typeof(Decimal);
                        _tableOutput.Columns.Add("ORDER_PRICE");
                        _tableOutput.Columns["ORDER_PRICE"].DataType = typeof(Decimal);
                        _tableOutput.Columns.Add("MATERIALS_COST");
                        _tableOutput.Columns["MATERIALS_COST"].DataType = typeof(Decimal);
                        _tableOutput.Columns.Add("VARIABLE_COST");
                        _tableOutput.Columns["VARIABLE_COST"].DataType = typeof(Decimal);

                        //Count querry SQL
                        int _tempTableCount = _tempTable.Rows.Count;

                        if (_tempTableCount > 0)
                        {
                            foreach (DataRow dr in _tempTable.Rows)
                            {
                                DataRow drLocal = _tableOutput.NewRow();
                                drLocal["DATE_CREATE"] = dr["DATE_CREATE"];
                                drLocal["INVOICE_NO"] = dr["INVOICE_NO"];
                                drLocal["TVC_ITEM_CODE"] = dr["TVC_ITEM_CODE"];
                                drLocal["PART_DESCRIPTION"] = dr["PART_DESCRIPTION"];
                                drLocal["QUANTITY"] = dr["QUANTITY"];
                                drLocal["ORDER_PRICE"] = dr["ORDER_PRICE"];
                                drLocal["MATERIALS_COST"] = 0;
                                drLocal["VARIABLE_COST"] = 0;
                                _tableOutput.Rows.Add(drLocal);
                            }

                            _technicalSupportExp.InsertRow(12, _tempTableCount - 1, 11);

                            //Load data from datatable to excel
                            _technicalSupportExp.Cells["A11"].LoadFromDataTable(_tableOutput, false);
                        }

                        //Focus A1, sheet Invoice
                        _technicalSupportExp.Select("A1");
                        #endregion
                    }

                    byte[] bin = _excelPackage.GetAsByteArray();

                    //Create a SaveFileDialog instance with some properties
                    SaveFileDialog _saveFileDialog = new SaveFileDialog();
                    _saveFileDialog.Title = "Save file ";
                    _saveFileDialog.Filter = "Excel files|*.xlxs|All files|*.*";
                    _saveFileDialog.FileName = _fileName + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";

                    //Check if user clicked the save button
                    if (_saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //write the file to the disk
                        File.WriteAllBytes(_saveFileDialog.FileName, bin);

                        if(MessageBox.Show("Xuất thành công!", "Hoàn Thành", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            Process.Start(_saveFileDialog.FileName);
                        }
                            
                    }
                }
            }
        }

        private void btnSearch_Inv_Click(object sender, EventArgs e)
        {
            Form_Search _formSearch = new Form_Search("btnSearch_Inv", this.Name);
            _formSearch.StartPosition = FormStartPosition.CenterParent;
            _formSearch.ShowDialog();

            if (!String.IsNullOrEmpty(_formSearch._InvoiceInfo.InvoiceNo))
            {
                txtInvoiceNo.Text = _formSearch._InvoiceInfo.InvoiceNo;
                dtpDueDateFrom.Value = _formSearch._InvoiceInfo.ETD;
                dtpDueDateTo.Value = _formSearch._InvoiceInfo.ETD;
                this.SelectNextControl((Control)sender, true, true, true, true);
            } else
            {
                txtInvoiceNo.Focus();
            }
        }

        private void cbTemplate_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(cbTemplate.SelectedValue) == "2" 
                || Convert.ToString(cbTemplate.SelectedValue) == "3" 
                || Convert.ToString(cbTemplate.SelectedValue) == "4")
            {
                dtpRevenue.Enabled = true;
                dtpDueDateFrom.Enabled = false;
                dtpDueDateTo.Enabled = false;
            }
            else
            {
                dtpRevenue.Enabled = false;
                dtpDueDateFrom.Enabled = true;
                dtpDueDateTo.Enabled = true;
            }
        }
    }
}