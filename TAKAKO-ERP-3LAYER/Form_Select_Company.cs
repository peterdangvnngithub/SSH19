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

namespace TAKAKO_ERP_3LAYER
{
    public partial class Form_Select_Company : Form
    {
        SYSTEM_DAL _systemDAL;

        public Form_Select_Company(SYSTEM_DAL _formLogin)
        {
            InitializeComponent();

            ////Setting no title but have border
            this.ControlBox = false;
            this.Text = String.Empty;

            _systemDAL = new SYSTEM_DAL();
            _systemDAL = _formLogin;
        }
        private void Form_Select_Company_Load(object sender, EventArgs e)
        {
            //
            SettingInitComboBox();
        }

        public void SettingInitComboBox()
        {
            cbxCompany.DisplayMember = "Text";
            cbxCompany.ValueMember = "Value";

            var items = new[] {
                new { Text = "00000 Whole Company", Value = "00000" },
                new { Text = "00001 TVC1", Value = "00001" },
                new { Text = "00002 TVC2", Value = "00002" }
            };

            cbxCompany.DataSource = items;
        }

        private void btnSelectCompany_Click(object sender, EventArgs e)
        {
            try
            {
                _systemDAL.CompanyCode = cbxCompany.SelectedValue.ToString();

                //Setting company name
                if (_systemDAL.CompanyCode == "00000")
                {
                    _systemDAL.CompanyName = "TVC";
                }
                else if (_systemDAL.CompanyCode == "00001")
                {
                    _systemDAL.CompanyName = "TVC1";
                }
                else if (_systemDAL.CompanyCode == "00002")
                {
                    _systemDAL.CompanyName = "TVC2";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }

            this.Close();

            Form_Main _formMain = new Form_Main(_systemDAL);
            _formMain.StartPosition = FormStartPosition.CenterScreen;
            _formMain.Show();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
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

        private void cbxCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnSelectCompany_Click(sender, e);
            }    
        }
    }
}
