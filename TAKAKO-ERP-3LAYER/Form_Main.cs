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
    public partial class Form_Main : Form
    {
        public SYSTEM_DAL _systemDAL;

        public LOG_DAO _logDAO;

        public Form_Main(SYSTEM_DAL _formSelectSystemDAL)
        {
            InitializeComponent();

            //Setting no title but have border
            this.ControlBox = false;
            this.Text = String.Empty;

            //
            _systemDAL = new SYSTEM_DAL();
            
            //
            _systemDAL = _formSelectSystemDAL;
        }

        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        #region Event Draw Underline, Click Menu
        /// <summary>
        /// lblPO_MouseEnter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_PO_MouseEnter(object sender, EventArgs e)
        {
            lbl_PO.Font = new Font(lbl_PO.Font.Name, lbl_PO.Font.SizeInPoints, FontStyle.Underline);
        }

        /// <summary>
        /// lbl_PO_MouseLeave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_PO_MouseLeave(object sender, EventArgs e)
        {
            lbl_PO.Font = new Font(lbl_PO.Font.Name, lbl_PO.Font.SizeInPoints, FontStyle.Regular);
        }

        /// <summary>
        /// lbl_PO_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_PO_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form_PO _formPO = new Form_PO(_systemDAL);
            _formPO.StartPosition = FormStartPosition.CenterScreen;
            _formPO.Show();
        }

        /// <summary>
        /// lbl_Invoice_MouseEnter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_ShippingNo_MouseEnter(object sender, EventArgs e)
        {
            lbl_Shipping.Font = new Font(lbl_Shipping.Font.Name, lbl_Shipping.Font.SizeInPoints, FontStyle.Underline);
        }

        /// <summary>
        /// lbl_Invoice_MouseLeave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_ShippingNo_MouseLeave(object sender, EventArgs e)
        {
            lbl_Shipping.Font = new Font(lbl_Shipping.Font.Name, lbl_Shipping.Font.SizeInPoints, FontStyle.Regular);
        }

        /// <summary>
        /// lbl_Invoice_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_ShippingNo_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form_Shipping_Instruction _formInv_PL = new Form_Shipping_Instruction(_systemDAL);
            _formInv_PL.StartPosition = FormStartPosition.CenterScreen;
            _formInv_PL.Show();
        }

        /// <summary>
        /// lbl_Invoice_MouseEnter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_Invoice_MouseEnter(object sender, EventArgs e)
        {
            lbl_Invoice.Font = new Font(lbl_Invoice.Font.Name, lbl_Invoice.Font.SizeInPoints, FontStyle.Underline);
        }

        /// <summary>
        /// lbl_Invoice_MouseLeave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_Invoice_MouseLeave(object sender, EventArgs e)
        {
            lbl_Invoice.Font = new Font(lbl_Invoice.Font.Name, lbl_Invoice.Font.SizeInPoints, FontStyle.Regular);
        }

        /// <summary>
        /// lbl_Invoice_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_Invoice_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form_Invoice _formInv_PL = new Form_Invoice(_systemDAL);
            _formInv_PL.StartPosition = FormStartPosition.CenterScreen;
            _formInv_PL.Show();
        }

        /// <summary>
        /// lbl_PL_MouseEnter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_Excel_MouseEnter(object sender, EventArgs e)
        {
            lbl_Excel.Font = new Font(lbl_Excel.Font.Name, lbl_Excel.Font.SizeInPoints, FontStyle.Underline);
        }

        /// <summary>
        /// lbl_PL_MouseLeave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_Excel_MouseLeave(object sender, EventArgs e)
        {
            lbl_Excel.Font = new Font(lbl_Excel.Font.Name, lbl_Excel.Font.SizeInPoints, FontStyle.Regular);
        }

        /// <summary>
        /// lbl_Excel_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_Excel_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form_Export_Data _formExportData = new Form_Export_Data(_systemDAL);
            _formExportData.StartPosition = FormStartPosition.CenterScreen;
            _formExportData.Show();
        }

        private void lbl_NewLog_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form_Log _formExportData = new Form_Log(_systemDAL, "New");
            _formExportData.StartPosition = FormStartPosition.CenterScreen;
            _formExportData.Show();
        }

        private void lbl_EditLog_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form_Log _formExportData = new Form_Log(_systemDAL, "Edit");
            _formExportData.StartPosition = FormStartPosition.CenterScreen;
            _formExportData.Show();
        }

        private void lbl_DeleteLog_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form_Log _formExportData = new Form_Log(_systemDAL, "Delete");
            _formExportData.StartPosition = FormStartPosition.CenterScreen;
            _formExportData.Show();
        }

        private void lbl_ErrorLog_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form_Log _formExportData = new Form_Log(_systemDAL, "Error");
            _formExportData.StartPosition = FormStartPosition.CenterScreen;
            _formExportData.Show();
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

        private void picBoxLogOut_MouseEnter(object sender, EventArgs e)
        {
            picBoxLogOut.Size = new Size(24, 24);
        }

        private void picBoxLogOut_MouseLeave(object sender, EventArgs e)
        {
            picBoxLogOut.Size = new Size(22, 22);
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

        private void picBoxLogOut_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Do you want back to login Screen?", "Confirm"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Question
                , MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                System.Diagnostics.Process.Start(Application.ExecutablePath);
                Application.Exit();
            }
        }

        private void picBox_Max_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
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

        private void picBox_Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void picBoxLogOut_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.picBoxLogOut, "Back to Log-In Screen");
        }
        #endregion

        private void Form_Main_Load(object sender, EventArgs e)
        {
            //
            lblUserName.Text = _systemDAL.UserName;

            //
            lblCompanyCode.Text = _systemDAL.CompanyCode;

            //
            lblCompanyName.Text = _systemDAL.CompanyName;

            //
            lblTeam.Text = _systemDAL.Department;

            //
            tab_MainMenu.SelectedIndex = 1;

            //
            _logDAO = new LOG_DAO();

            //Setting Log
            SettingInitLog();
        }

        private void SettingInitLog()
        {
            try
            {
                DataTable _tempTable = new DataTable();
                _tempTable = _logDAO.Count_Log();

                lbl_NewLog.Text = "0";
                lbl_EditLog.Text = "0";
                lbl_DeleteLog.Text = "0";
                lbl_ErrorLog.Text = "0";

                if (_tempTable.Rows.Count > 0)
                {
                    foreach (DataRow row in _tempTable.Rows)
                    {
                        if (row["TYPE"].ToString().ToUpper() == "NEW")
                        {
                            lbl_NewLog.Text = row["COUNT_BY_TYPE"].ToString();
                        } else if (row["TYPE"].ToString().ToUpper() == "EDIT")
                        {
                            lbl_EditLog.Text = row["COUNT_BY_TYPE"].ToString();
                        }
                        else if (row["TYPE"].ToString().ToUpper() == "DELETE")
                        {
                            lbl_DeleteLog.Text = row["COUNT_BY_TYPE"].ToString();
                        }
                        else if (row["TYPE"].ToString().ToUpper() == "ERROR")
                        {
                            lbl_ErrorLog.Text = row["COUNT_BY_TYPE"].ToString();
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
}
