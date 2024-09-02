using System;
using System.Windows.Forms;

namespace AD_Coursework_01
{
    public partial class AdminMenuBar : Form
    {
        public AdminMenuBar(string username)
        {
            InitializeComponent();
            LoadContentIntoPanel(new CustomerDashboard());
            lblUsername.Text = username;
        }

        private void LoadContentIntoPanel(UserControl userControl)
        {
            // Clear the existing controls from the panel
            panelContent.Controls.Clear();

            // Set the dock style to fill the panel
            userControl.Dock = DockStyle.Fill;

            // Add the UserControl to the panel
            panelContent.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            LoadContentIntoPanel(new CustomerDashboard());
            lblTitle.Text = "Admin/Dashboard";
        }

        private void btnCars_Click(object sender, EventArgs e)
        {
            LoadContentIntoPanel(new PurchaseCarsForm());
            lblTitle.Text = "Admin/Manage Cars";
        }

        private void btnCarParts_Click(object sender, EventArgs e)
        {
            LoadContentIntoPanel(new CarPartsForm());
            lblTitle.Text = "Admin/Manage Car Parts";
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            LoadContentIntoPanel(new OrderDetailsForm(null));
            lblTitle.Text = "Admin/Manage Orders";
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            LoadContentIntoPanel(new GenerateReportForm());
            lblTitle.Text = "Admin/Generate Reports";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }
}
