using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TAKAKO_ERP_3LAYER
{
    public partial class Form_Flash : Form
    {
        public Form_Flash()
        {
            InitializeComponent();

            //Setting no title but have border
            this.ControlBox = false;
            this.Text = String.Empty;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Visible = true;

            this.progressBar1.Value = this.progressBar1.Value + 2;
            if (this.progressBar1.Value == 10)
            {
                label3.Text = "Reading modules..";
            }
            else if (this.progressBar1.Value == 20)
            {
                label3.Text = "Turning on modules.";
            }
            else if (this.progressBar1.Value == 40)
            {
                label3.Text = "Starting modules..";
            }
            else if (this.progressBar1.Value == 60)
            {
                label3.Text = "Loading modules..";
            }
            else if (this.progressBar1.Value == 80)
            {
                label3.Text = "Done Loading modules..";
            }
            else if (this.progressBar1.Value == 100)
            {
                timer1.Enabled = false;
                this.Hide();
                Form_Login _fonmLogin = new Form_Login();
                _fonmLogin.StartPosition = FormStartPosition.CenterScreen;
                _fonmLogin.Show();
            }
        }
    }
}
