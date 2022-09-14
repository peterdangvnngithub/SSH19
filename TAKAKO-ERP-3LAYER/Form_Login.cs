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
using TAKAKO_ERP_3LAYER.BUS;

namespace TAKAKO_ERP_3LAYER
{
    public partial class Form_Login : Form
    {
        public USER_BUS _userBUS;

        public USER_DAL _userDAL;

        public USER_DAO _userDAO;

        public SYSTEM_DAL _systemDAL;

        public Form_Login()
        {
            InitializeComponent();

            //Setting no title but have border
            this.ControlBox = false;
            this.Text = String.Empty;

            //
            _userBUS = new USER_BUS();

            //
            _userDAO = new USER_DAO();

            //
            _systemDAL = new SYSTEM_DAL();
        }

        private void Form_Login_Load(object sender, EventArgs e)
        {
            _userDAL = new USER_DAL();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string _userName = txtUserName.Text.Trim();
            string _passWord = txtPassWord.Text.Trim();
            if (String.IsNullOrEmpty(_userName))
            {
                MessageBox.Show("Please enter Username", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserName.Focus();
                return;
            }
            if (String.IsNullOrEmpty(_passWord))
            {
                MessageBox.Show("Please enter Password", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassWord.Focus();
                return;
            }
            try
            {
                _userDAL = _userBUS.getUser("00001", _userName , _passWord);
                _systemDAL.UserName = _userDAL.UserName;
                _systemDAL.Department = _userDAL.Department;
                if ((_userDAL.CompanyCode == null) 
                    && (_userDAL.UserName == null)
                    && (_userDAL.PassWord == null))
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUserName.Focus();
                }
                else
                {
                    //Merge Data
                    bool result = _userDAO.Merge_Data();
                    if (!result)
                    {
                        MessageBox.Show("Merge Data lỗi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    this.Close();

                    Form_Select_Company _formSelectCompany = new Form_Select_Company(_systemDAL);
                    _formSelectCompany.StartPosition = FormStartPosition.CenterScreen;
                    _formSelectCompany.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #region Moveable
        bool mouseDown = false;
        Point startPoint = new Point(0, 0);
        private void panelTopLeft_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void panelTopRight_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void panelTopLeft_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void panelTopRight_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void panelTopLeft_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - startPoint.X, p.Y - startPoint.Y);
            }
        }

        private void panelTopRight_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - startPoint.X, p.Y - startPoint.Y);
            }
        }
        #endregion

        private void btn_ChoseDB_Click(object sender, EventArgs e)
        {

        }

        private void picShowPass_MouseDown(object sender, MouseEventArgs e)
        {
            picShowPass.Image = Properties.Resources.eye_gray;
            txtPassWord.PasswordChar = '\0';
        }

        private void picShowPass_MouseUp(object sender, MouseEventArgs e)
        {
            picShowPass.Image = Properties.Resources.icon_eye;
            txtPassWord.PasswordChar = '*';
        }

        private void picShowPass_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.picShowPass, "Show Password");
        }

        private void txtUserName_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true,true);
            }    
        }

        private void txtPassWord_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }    
        }
    }
}
