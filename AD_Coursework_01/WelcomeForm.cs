using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AD_Coursework_01
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            AdminLogin adminLogin = new AdminLogin();
            adminLogin.Location = this.Location;
            adminLogin.StartPosition = FormStartPosition.CenterScreen;
            adminLogin.Show();
            this.Hide();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            CustomerLogin customerLogin = new CustomerLogin();
            customerLogin.Location = this.Location;
            customerLogin.StartPosition = FormStartPosition.CenterScreen;
            customerLogin.Show();
            this.Hide();
        }
    }
}
