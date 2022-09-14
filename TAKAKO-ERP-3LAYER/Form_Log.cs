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

namespace TAKAKO_ERP_3LAYER
{
    public partial class Form_Log : Form
    {
        public SYSTEM_DAL _systemDAL;
        public LOG_DAO _logDAO;
        String _searchCondition;

        public Form_Log(SYSTEM_DAL _tempSystem,string _tempSearchCondition)
        {
            InitializeComponent();
            //
            _logDAO = new LOG_DAO();
            //Setting no title but have border
            this.ControlBox = false;
            this.Text = String.Empty;
            //
            _systemDAL = _tempSystem;
            //
            _searchCondition = _tempSearchCondition;
        }

        private void Form_Log_Load(object sender, EventArgs e)
        {
            //
            SettingInit_cbxTypeSearch();
            //
            cbxTypeSearch.Text = _searchCondition;
            //
            AddColumnGridView();
            //
            GetInfoLog(dtpCreateDateFrom.Value, dtpCreateDateTo.Value, _searchCondition);
        }

        public void SettingInit_cbxTypeSearch()
        {
            cbxTypeSearch.DisplayMember = "Text";
            cbxTypeSearch.ValueMember = "Value";

            var items = new[] {
                new { Text = "New", Value = 0 },
                new { Text = "Edit", Value = 1 },
                new { Text = "Delete", Value = 2 },
                new { Text = "Error", Value = 3 },
            };

            cbxTypeSearch.DataSource = items;
        }

        public void AddColumnGridView()
        {
            System.Windows.Forms.DataGridViewTextBoxColumn CompanyCode_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn UserName_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn DateCreateLog_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Type_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Content_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            
            this.GridViewLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
                {
                     CompanyCode_col
                    ,UserName_col
                    ,DateCreateLog_col
                    ,Type_col
                    ,Content_col
                });
            
            //COMPANY CODE
            CompanyCode_col.HeaderText = "COMPANY CODE";
            CompanyCode_col.DataPropertyName = "COMPANY_CODE";
            CompanyCode_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CompanyCode_col.Name = "Company_Code";
            CompanyCode_col.Width = 100;
            
            //USERNAME
            UserName_col.HeaderText = "USER NAME";
            UserName_col.DataPropertyName = "USER_NAME";
            UserName_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            UserName_col.Name = "User_Name";
            UserName_col.Width = 100;
            
            //DATE CREATE
            DateCreateLog_col.HeaderText = "DATE CREATE";
            DateCreateLog_col.DataPropertyName = "DATE_CREATE";
            DateCreateLog_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DateCreateLog_col.Name = "Date_Create";
            DateCreateLog_col.Width = 100;
            
            //TYPE LOG
            Type_col.HeaderText = "TYPE LOG";
            Type_col.DataPropertyName = "TYPE_LOCK";
            Type_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Type_col.Name = "Type_Lock";
            Type_col.Width = 100;
            
            //CONTENT
            Content_col.HeaderText = "CONTENT";
            Content_col.DataPropertyName = "CONTENT_LOG";
            Content_col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Content_col.Name = "Content_Log";
            Content_col.Width = 347;
            
            //Setting Font
            this.GridViewLog.DefaultCellStyle.Font = new Font("Arial", 8.25F, GraphicsUnit.Pixel);
            this.GridViewLog.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8.25F, FontStyle.Bold);

            //COMPANY CODE
            this.GridViewLog.Columns["Company_Code"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            //USER NAME
            this.GridViewLog.Columns["User_Name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            //DATE CREATE
            this.GridViewLog.Columns["Date_Create"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            //TYPE LOCK
            this.GridViewLog.Columns["Type_Lock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public void GetInfoLog(DateTime DateCreateFrom
                              ,DateTime DateCreateTo
                              ,string TypeSearch)
        {
            DataTable _tempTable = new DataTable();

            try
            {
                _tempTable = _logDAO.GetInfoLog(DateCreateFrom, DateCreateTo, TypeSearch);

                if (_tempTable.Rows.Count > 0)
                {
                    try
                    {
                        //
                        if (_tempTable.Rows.Count == 0)
                        {
                            MessageBox.Show("Can't find data!");
                        }
                        else if (_tempTable.Rows.Count > 0)
                        {
                            //Clear DataGridView
                            GridViewLog.Rows.Clear();
                            foreach (DataRow row in _tempTable.Rows)
                            {
                                if (!String.IsNullOrEmpty(row["CONTENT_LOG"].ToString()))
                                {
                                    int indexInv = GridViewLog.Rows.Add();
                                    //Binding for Invoice
                                    GridViewLog.Rows[indexInv].Cells["Company_Code"].Value = row["COMPANY_CODE"].ToString();
                                    GridViewLog.Rows[indexInv].Cells["User_Name"].Value = row["USER_NAME"].ToString();
                                    GridViewLog.Rows[indexInv].Cells["Date_Create"].Value = Convert.ToDateTime(row["DATE_CREATE"]).ToString("dd/MM/yyyy");
                                    GridViewLog.Rows[indexInv].Cells["Type_Lock"].Value = row["TYPE"];
                                    GridViewLog.Rows[indexInv].Cells["Content_Log"].Value = row["CONTENT_LOG"];
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                                  MessageBox.Show("No Log \"" + TypeSearch + "\" from day " + dtpCreateDateFrom.Value.ToString("dd/MM/yyyy") + " to day " + dtpCreateDateTo.Value.ToString("dd/MM/yyyy") + "!", "   Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpCreateDateFrom.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }

        private void btnSearchLog_Click(object sender, EventArgs e)
        {
            string _typeSearch = cbxTypeSearch.Text ;
            GetInfoLog(dtpCreateDateFrom.Value, dtpCreateDateTo.Value, _typeSearch);
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
                _formMain.StartPosition = FormStartPosition.CenterScreen;
                _formMain.Show();
            }
        }
        #endregion
        private void Form_Log_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
