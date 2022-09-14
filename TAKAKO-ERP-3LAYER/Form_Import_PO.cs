using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using System.Windows.Forms;
using TAKAKO_ERP_3LAYER.DAO;
using TAKAKO_ERP_3LAYER.DAL;

namespace TAKAKO_ERP_3LAYER
{
    public partial class Form_Import_PO : Form
    {
        private DBConnection conn;

        public PO_DAO _poDAO = new PO_DAO();

        public SYSTEM_DAL _systemPODAL;

        public DataTable _dtTemp = new DataTable();

        public Form_Import_PO(SYSTEM_DAL _systemDAL)
        {
            InitializeComponent();

            //Setting no title but have border
            this.ControlBox = false;
            this.Text = String.Empty;

            _systemPODAL = _systemDAL;

            conn = new DBConnection();
        }

        private void btn_ChoseFile_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open File Data";
            theDialog.Filter = "Files Excel|*.xlsx";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //read the Excel file as byte array
                    byte[] bin = File.ReadAllBytes(theDialog.FileName);

                    //create a new Excel package in a memorystream
                    using (MemoryStream stream = new MemoryStream(bin))
                    using (ExcelPackage excelPackage = new ExcelPackage(stream))
                    {
                        if ((myStream = theDialog.OpenFile()) != null)
                        {
                            _dtTemp = ExcelPackageToDataTable(excelPackage);
                            
                            //Check dupplicated row
                            var countDuplication = _dtTemp.AsEnumerable()
                                                          .GroupBy(x => new {TVC_ItemCode = x.Field<string>("TVC_Item_Code")
                                                                            ,Customer_PO = x.Field<string>("Customer_PO")
                                                                            ,Due_Date = x.Field<DateTime>("Due_Date")
                                                          })
                                                          .Where(x => x.Count() > 1);
                            if (countDuplication != null && countDuplication.Any())
                            {
                                foreach (var group in countDuplication)
                                {
                                    foreach (DataRow row in group)
                                    {
                                        MessageBox.Show("Trùng dữ liệu.\nTVC_Item_Code: " + row.Field<string>("TVC_Item_Code")
                                                            + "\nCustomer_PO: " + row.Field<string>("Customer_PO")
                                                            + "\nDue_Date: " + row.Field<DateTime>("Due_Date").ToShortDateString()
                                                            , "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    }
                                    break;
                                }
                            } else
                            {
                                GridView_ImportPO.DataSource = _dtTemp;
                                txtReceiveNo.Focus();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message,"Lỗi",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (myStream != null)
                        myStream.Close();
                }
            }
        }

        public static DataTable ExcelPackageToDataTable(ExcelPackage excelPackage)
        {
            DataTable dt = new DataTable();
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];

            //check if the worksheet is completely empty
            if (worksheet.Dimension == null)
            {
                return dt;
            }

            dt.Columns.Add("Company_Code");
            dt.Columns.Add("Customer_Code");
            dt.Columns.Add("TVC_Item_Code");
            dt.Columns.Add("Customer_Item_Code");
            dt.Columns.Add("Parts_Name");
            dt.Columns.Add("Item_Name");
            dt.Columns.Add("Quantity");
            dt.Columns["Quantity"].DataType = typeof(Decimal);
            dt.Columns.Add("Unit_Currency");
            dt.Columns.Add("Price_JPY");
            dt.Columns["Price_JPY"].DataType = typeof(Decimal);
            dt.Columns.Add("Price_USD");
            dt.Columns["Price_USD"].DataType = typeof(Decimal);
            dt.Columns.Add("Amount_JPY");
            dt.Columns["Amount_JPY"].DataType = typeof(Decimal);
            dt.Columns.Add("Amount_USD");
            dt.Columns["Amount_USD"].DataType = typeof(Decimal);
            dt.Columns.Add("Customer_PO");
            dt.Columns.Add("Order_Date");
            dt.Columns["Order_Date"].DataType = typeof(DateTime);
            dt.Columns.Add("Due_Date");
            dt.Columns["Due_Date"].DataType = typeof(DateTime);
            dt.Columns.Add("Third_Party_Item_Code");
            dt.Columns.Add("Third_Party_PO");
            dt.Columns.Add("Note");

            //start adding the contents of the excel file to the datatable
            for (int i = 3; i <= worksheet.Dimension.End.Row; i++)
            {
                var row = worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column];
                DataRow newRow = dt.NewRow();
                //loop all cells in the row
                int index = 1;
                foreach (var cell in row)
                {
                    switch (index)
                    {
                        case 1:
                            newRow["Company_Code"] = cell.Value;
                            index++;
                            break;
                        case 2:
                            newRow["Customer_Code"] = cell.Value;
                            index++;
                            break;
                        case 3:
                            newRow["TVC_Item_Code"] = cell.Value;
                            index++;
                            break;
                        case 4:
                            newRow["Customer_Item_Code"] = cell.Value;
                            index++;
                            break;
                        case 5:
                            newRow["Parts_Name"] = cell.Value;
                            index++;
                            break;
                        case 6:
                            newRow["Item_Name"] = cell.Value;
                            index++;
                            break;
                        case 7:
                            newRow["Quantity"] = cell.Value;
                            index++;
                            break;
                        case 8:
                            newRow["Unit_Currency"] = cell.Value;
                            index++;
                            break;
                        case 9:
                            newRow["Price_JPY"] = cell.Value;
                            index++;
                            break;
                        case 10:
                            newRow["Price_USD"] = cell.Value;
                            index++;
                            break;
                        case 11:
                            newRow["Amount_JPY"] = cell.Value;
                            index++;
                            break;
                        case 12:
                            newRow["Amount_USD"] = cell.Value;
                            index++;
                            break;
                        case 13:
                            newRow["Customer_PO"] = cell.Value;
                            index++;
                            break;
                        case 14:
                            newRow["Order_Date"] = cell.Value;
                            index++;
                            break;
                        case 15:
                            newRow["Due_Date"] = Convert.ToDateTime(cell.Value);
                            index++;
                            break;
                        case 16:
                            newRow["Third_Party_Item_Code"] = cell.Value;
                            index++;
                            break;
                        case 17:
                            newRow["Third_Party_PO"] = cell.Value;
                            index++;
                            break;
                        case 18:
                            newRow["Note"] = cell.Value;
                            index++;
                            break;
                    }
                }
                dt.Rows.Add(newRow);
            }
            return dt;
        }

        private void Form_Import_PO_Load(object sender, EventArgs e)
        {
            //Format Gridview
            AddColumnGridView();
        }

        public void AddColumnGridView()
        {
            System.Windows.Forms.DataGridViewTextBoxColumn Company_Code_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Customer_Code_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Code_Of_TVC_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Cus_ItemCode_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn PartsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Quantity_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn UnitCurrency_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn PriceJPY_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn PriceUSD_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Amount_JPY_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Amount_USD_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Customer_PO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Order_date_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Due_Date_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Code_Of_Customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Po_Of_Customer_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Note_Col = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.GridView_ImportPO.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] 
                {
                 Company_Code_Col
                ,Customer_Code_Col
                ,Code_Of_TVC_Col
                ,Cus_ItemCode_Col
                ,PartsName
                ,ItemName
                ,Quantity_Col
                ,UnitCurrency_Col
                ,PriceJPY_Col
                ,PriceUSD_Col
                ,Amount_JPY_Col
                ,Amount_USD_Col
                ,Customer_PO
                ,Order_date_Col
                ,Due_Date_Col
                ,Code_Of_Customer
                ,Po_Of_Customer_Col
                ,Note_Col
                });

            //COMPANY CODE
            Company_Code_Col.HeaderText = "COMPANY CODE";
            Company_Code_Col.DataPropertyName = Company_Code_Col.HeaderText.ToString().Replace(" ", "_");
            Company_Code_Col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            Company_Code_Col.Name = "Company_Code";
            Company_Code_Col.Width = 100;

            //CUSTOMER CODE
            Customer_Code_Col.HeaderText = "CUSTOMER CODE";
            Customer_Code_Col.DataPropertyName = Customer_Code_Col.HeaderText.ToString().Replace(" ", "_");
            Customer_Code_Col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            Customer_Code_Col.Name = "Customer_Code";
            Customer_Code_Col.Width = 100;

            //TVC ITEM CODE
            Code_Of_TVC_Col.HeaderText = "TVC ITEM CODE";
            Code_Of_TVC_Col.DataPropertyName = Code_Of_TVC_Col.HeaderText.ToString().Replace(" ", "_");
            Code_Of_TVC_Col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Code_Of_TVC_Col.Name = "TVC_ItemCode_Col";
            Code_Of_TVC_Col.Width = 100;

            //CUSTOMER ITEM CODE
            Cus_ItemCode_Col.HeaderText = "CUSTOMER ITEM CODE";
            Cus_ItemCode_Col.DataPropertyName = Cus_ItemCode_Col.HeaderText.ToString().Replace(" ", "_");
            Cus_ItemCode_Col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Cus_ItemCode_Col.Name = "Cus_ItemCode_Col";
            Cus_ItemCode_Col.Width = 100;

            //PARTS NAME
            PartsName.HeaderText = "PARTS NAME";
            PartsName.DataPropertyName = PartsName.HeaderText.ToString().Replace(" ", "_");
            PartsName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PartsName.Name = "PartName";
            PartsName.Width = 150;

            //ITEM NAME
            ItemName.HeaderText = "ITEM NAME";
            ItemName.DataPropertyName = ItemName.HeaderText.ToString().Replace(" ", "_");
            ItemName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ItemName.Name = "ItemName";
            ItemName.Width = 150;

            //QTY (PCS)
            Quantity_Col.HeaderText = "QUANTITY (PCS)";
            Quantity_Col.DataPropertyName = "Quantity";
            Quantity_Col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            Quantity_Col.Name = "Qty_Column";
            Quantity_Col.Width = 100;

            //UNIT CURRENCY
            UnitCurrency_Col.HeaderText = "UNIT CURRENCY";
            UnitCurrency_Col.DataPropertyName = UnitCurrency_Col.HeaderText.ToString().Replace(" ", "_");
            UnitCurrency_Col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            UnitCurrency_Col.Name = "UnitCurrency_Column";
            UnitCurrency_Col.Width = 100;

            //Price JPY
            PriceJPY_Col.HeaderText = "PRICE JPY";
            PriceJPY_Col.DataPropertyName = PriceJPY_Col.HeaderText.ToString().Replace(" ", "_");
            PriceJPY_Col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PriceJPY_Col.Name = "PriceJPY_Column";
            PriceJPY_Col.Width = 100;

            //Price USD
            PriceUSD_Col.HeaderText = "PRICE USD";
            PriceUSD_Col.DataPropertyName = PriceUSD_Col.HeaderText.ToString().Replace(" ", "_");
            PriceUSD_Col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PriceUSD_Col.Name = "PriceUSD_Column";
            PriceUSD_Col.Width = 100;

            //Amount JPY
            Amount_JPY_Col.HeaderText = "AMOUNT JPY";
            Amount_JPY_Col.DataPropertyName = Amount_JPY_Col.HeaderText.ToString().Replace(" ", "_");
            Amount_JPY_Col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Amount_JPY_Col.Name = "AmountJPY_Column";
            Amount_JPY_Col.Width = 100;

            //Amount USD
            Amount_USD_Col.HeaderText = "AMOUNT USD";
            Amount_USD_Col.DataPropertyName = Amount_USD_Col.HeaderText.ToString().Replace(" ", "_");
            Amount_USD_Col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Amount_USD_Col.Name = "AmountUSD_Column";
            Amount_USD_Col.Width = 100;

            //CUSTOMER PO
            Customer_PO.HeaderText = "CUSTOMER PO";
            Customer_PO.DataPropertyName = Customer_PO.HeaderText.ToString().Replace(" ", "_");
            Customer_PO.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Customer_PO.Name = "CustomerPO_Column";
            Customer_PO.Width = 120;

            //Order Date (dd/mm/yy)
            Order_date_Col.HeaderText = "ORDER DATE\n(dd/MM/yyyy)";
            Order_date_Col.DataPropertyName = "Order_Date";
            Order_date_Col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Order_date_Col.Name = "OrderDate_Column";
            Order_date_Col.Width = 120;

            //Due Date (dd / mm / yy)
            Due_Date_Col.HeaderText = "DUE DATE\n(dd/MM/yyyy)";
            Due_Date_Col.DataPropertyName = "Due_Date";
            Due_Date_Col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Due_Date_Col.Name = "DueDate_Column";
            Due_Date_Col.Width = 120;

            //THIRD PARTY ITEM CODE
            Code_Of_Customer.HeaderText = "THIRD PARTY ITEM CODE";
            Code_Of_Customer.DataPropertyName = Code_Of_Customer.HeaderText.ToString().Replace(" ", "_");
            Code_Of_Customer.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Code_Of_Customer.Name = "Third_ItemCode_Col";
            Code_Of_Customer.Width = 120;

            //THIRD PARTY PO
            Po_Of_Customer_Col.HeaderText = "THIRD PARTY PO";
            Po_Of_Customer_Col.DataPropertyName = Po_Of_Customer_Col.HeaderText.ToString().Replace(" ", "_");
            Po_Of_Customer_Col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Po_Of_Customer_Col.Name = "ThirdParty_PO_Col";
            Po_Of_Customer_Col.Width = 120;

            //NOTE
            Note_Col.HeaderText = "NOTE";
            Note_Col.DataPropertyName = Note_Col.HeaderText.ToString().Replace(" ", "_");
            Note_Col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Note_Col.Name = "Note_Column";
            Note_Col.Width = 100;

            this.GridView_ImportPO.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8.25F, FontStyle.Bold);
            this.GridView_ImportPO.DefaultCellStyle.Font = new Font("Arial", 10.25F, GraphicsUnit.Pixel);

            //QTY (PCS)
            this.GridView_ImportPO.Columns["Qty_Column"].DefaultCellStyle.Format = "#,##0.##";
            this.GridView_ImportPO.Columns["Qty_Column"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //PRICE UNIT
            this.GridView_ImportPO.Columns["UnitCurrency_Column"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //PRICE JPY
            this.GridView_ImportPO.Columns["PriceJPY_Column"].DefaultCellStyle.Format = "#,##0.00";
            this.GridView_ImportPO.Columns["PriceJPY_Column"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //Price USD
            this.GridView_ImportPO.Columns["PriceUSD_Column"].DefaultCellStyle.Format = "#,##0.00";
            this.GridView_ImportPO.Columns["PriceUSD_Column"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //Amount JPY
            this.GridView_ImportPO.Columns["AmountJPY_Column"].DefaultCellStyle.Format = "#,##0.00";
            this.GridView_ImportPO.Columns["AmountJPY_Column"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //Amount USD
            this.GridView_ImportPO.Columns["AmountUSD_Column"].DefaultCellStyle.Format = "#,##0.00";
            this.GridView_ImportPO.Columns["AmountUSD_Column"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //Order Date (dd/mm/yy)
            this.GridView_ImportPO.Columns["OrderDate_Column"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Due Date (dd/mm/yy)
            this.GridView_ImportPO.Columns["DueDate_Column"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void Form_Import_PO_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn trở về màn hình PO?", "Xác nhận", MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                this.Dispose();
                this.Close();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private Boolean CheckError()
        {
            if (String.IsNullOrEmpty(txtReceiveNo.Text.Trim()))
            {
                MessageBox.Show("「Receive No」 rỗng!\nXin hãy nhập Receive No.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtReceiveNo.Focus();
                return false;
            }

            if(GridView_ImportPO.Rows.Count > 0)
            {
                int index = 1;
                foreach (DataGridViewRow dgRow in GridView_ImportPO.Rows)
                {
                    //Cell CompanyCode
                    var cell = dgRow.Cells[0];
                    if (string.IsNullOrEmpty(cell.Value.ToString())) {
                        cell.Style.BackColor = Color.Red;
                        cell.Selected = true;
                        this.GridView_ImportPO.CurrentCell = cell;
                        MessageBox.Show("Company Code dòng số \"" + index + "\" không được rỗng!\nXin hãy bổ sung.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        index++;
                        return false;
                    }

                    //Cell CustomerCode
                    cell = dgRow.Cells[1];
                    if (string.IsNullOrEmpty(cell.Value.ToString()))
                    {
                        cell.Style.BackColor = Color.Red;
                        cell.Selected = true;
                        this.GridView_ImportPO.CurrentCell = cell;
                        MessageBox.Show("Customer Code dòng số \"" + index + "\" không được rỗng!\nXin hãy nhập bổ sung.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        index++;
                        return false;
                    }

                    //Cell CustomerItemCode
                    cell = dgRow.Cells[3];
                    if (string.IsNullOrEmpty(cell.Value.ToString()))
                    {
                        cell.Style.BackColor = Color.Red;
                        cell.Selected = true;
                        this.GridView_ImportPO.CurrentCell = cell;
                        MessageBox.Show("Customer Item Code dòng \"" + index + "\" không được rỗng!\nXin hãy nhập bổ sung.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        index++;
                        return false;
                    }

                    index++;
                }
            } else
            {
                MessageBox.Show("Không có dữ liệu cần import!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Bạn có muốn lưu dữ liệu?", "Xác nhận",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question
                , MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                if (CheckError() == true)
                {
                    try
                    {
                        string _receiveNo = txtReceiveNo.Text.Trim();

                        DataTable _dttempPO = new DataTable();
                        _dttempPO.Columns.Add("Company_Code");
                        _dttempPO.Columns.Add("Customer_Code");
                        _dttempPO.Columns.Add("Receive_No");
                        _dttempPO.Columns.Add("Invoice_No");
                        _dttempPO.Columns.Add("Shipping_No");
                        _dttempPO.Columns.Add("TVC_ItemCode");
                        _dttempPO.Columns.Add("Customer_ItemCode");
                        _dttempPO.Columns.Add("Parts_Name");
                        _dttempPO.Columns.Add("Item_Name");
                        _dttempPO.Columns.Add("Quantity");
                        _dttempPO.Columns["Quantity"].DataType = typeof(Decimal);
                        _dttempPO.Columns.Add("Quantity_Order");
                        _dttempPO.Columns["Quantity_Order"].DataType = typeof(Decimal);
                        _dttempPO.Columns.Add("Unit_Currency");
                        _dttempPO.Columns.Add("Price_JPY");
                        _dttempPO.Columns["Price_JPY"].DataType = typeof(Decimal);
                        _dttempPO.Columns.Add("Price_USD");
                        _dttempPO.Columns["Price_USD"].DataType = typeof(Decimal);
                        _dttempPO.Columns.Add("Customer_PO");
                        _dttempPO.Columns.Add("Order_Date");
                        _dttempPO.Columns["Order_Date"].DataType = typeof(DateTime);
                        _dttempPO.Columns.Add("Due_Date");
                        _dttempPO.Columns["Due_Date"].DataType = typeof(DateTime);
                        _dttempPO.Columns.Add("Third_Party_Item_Code");
                        _dttempPO.Columns.Add("Third_Party_PO");
                        _dttempPO.Columns.Add("Note");
                        _dttempPO.Columns.Add("Create_By");
                        _dttempPO.Columns.Add("Create_At");
                        _dttempPO.Columns["Create_At"].DataType = typeof(DateTime);
                        _dttempPO.Columns.Add("Edit_By");
                        _dttempPO.Columns.Add("Edit_At");
                        _dttempPO.Columns["Edit_At"].DataType = typeof(DateTime);

                        //_dtTemp = ConvertGridviewToDataTable(GridView_ImportPO);

                        foreach (DataGridViewRow row in GridView_ImportPO.Rows)
                        {
                            if (row.Cells["TVC_ItemCode_Col"].Value != null)
                            {
                                DataRow PODetail = _dttempPO.NewRow();
                                PODetail["Company_Code"] = row.Cells["Company_Code"].Value.ToString();
                                PODetail["Customer_Code"] = row.Cells["Customer_Code"].Value.ToString();
                                PODetail["Receive_No"] = txtReceiveNo.Text.Trim();
                                PODetail["Invoice_No"] = "";
                                PODetail["Shipping_No"] = "";
                                PODetail["TVC_ItemCode"] = row.Cells["TVC_ItemCode_Col"].Value.ToString();
                                PODetail["Customer_ItemCode"] = row.Cells["Cus_ItemCode_Col"].Value.ToString();
                                PODetail["Parts_Name"] = row.Cells["PartName"].Value.ToString();
                                PODetail["Item_Name"] = row.Cells["ItemName"].Value.ToString();
                                PODetail["Quantity"] = Convert.ToInt32(row.Cells["Qty_Column"].Value);
                                PODetail["Quantity_Order"] = 0;
                                PODetail["Unit_Currency"] = row.Cells["UnitCurrency_Column"].Value.ToString();
                                PODetail["Price_JPY"] = Convert.ToDecimal(row.Cells["PriceJPY_Column"].Value);
                                PODetail["Price_USD"] = Convert.ToDecimal(row.Cells["PriceUSD_Column"].Value);
                                PODetail["Customer_PO"] = row.Cells["CustomerPO_Column"].Value.ToString();
                                PODetail["Order_Date"] = Convert.ToDateTime(row.Cells["OrderDate_Column"].Value);
                                PODetail["Due_Date"] = Convert.ToDateTime(row.Cells["DueDate_Column"].Value);
                                PODetail["Third_Party_Item_Code"] = row.Cells["Third_ItemCode_Col"].Value.ToString();
                                PODetail["Third_Party_PO"] = row.Cells["ThirdParty_PO_Col"].Value.ToString();
                                PODetail["Note"] = row.Cells["Note_Column"].Value.ToString();
                                PODetail["Create_By"] = _systemPODAL.UserName;
                                PODetail["Create_At"] = DateTime.Now;
                                PODetail["Edit_By"] = _systemPODAL.UserName;
                                PODetail["Edit_At"] = DateTime.Now;
                                _dttempPO.Rows.Add(PODetail);
                            }
                        }

                        if (_poDAO.insertPO(_dttempPO))
                        {
                            MessageBox.Show("Lưu thành công!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                    }
                    catch (ApplicationException ex)
                    {
                        MessageBox.Show(ex.Message, "Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
            }
        }

        private DataTable ConvertGridviewToDataTable(DataGridView _tempDataGridView)
        {
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn dataGridViewColumn in _tempDataGridView.Columns)
            {
                if (dataGridViewColumn.Visible)
                {
                    dt.Columns.Add();
                }
            }
            var cell = new object[_tempDataGridView.Columns.Count];
            foreach (DataGridViewRow dataGridViewRow in _tempDataGridView.Rows)
            {
                for (int i = 0; i < dataGridViewRow.Cells.Count; i++)
                {
                    cell[i] = dataGridViewRow.Cells[i].Value;
                }
                dt.Rows.Add(cell);
            }
            return dt;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            GridView_ImportPO.DataSource = null;
            AddColumnGridView();
            txtReceiveNo.Text = "";
        }

        private void GridView_ImportPO_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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
            this.Close();
        }

        private void picBox_Close_MouseEnter(object sender, EventArgs e)
        {
            picBox_Close.Size = new Size(27, 27);
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
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.CenterScreen;
                picBox_Max.Image = Properties.Resources.Maximize_window;
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

        private void btnSearch_ReciveNo_Click(object sender, EventArgs e)
        {
            Form_Search _formSearch = new Form_Search("btnSearch_ReciveNo", this.Name);
            _formSearch.StartPosition = FormStartPosition.CenterParent;
            _formSearch.ShowDialog();

            if (_formSearch._PoInfo.ReceiveNo != null)
            {
                txtReceiveNo.Text = _formSearch._PoInfo.ReceiveNo;
            }
        }
    }
}
