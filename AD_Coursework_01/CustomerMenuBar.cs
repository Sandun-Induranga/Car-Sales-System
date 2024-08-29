using System;
using System.Windows.Forms;

namespace AD_Coursework_01
{
    public partial class CustomerMenuBar : Form
    {
        private string customerId;
        public CustomerMenuBar(string customerId)
        {
            InitializeComponent();
            LoadContentIntoPanel(new CustomerDashboard());
            this.customerId = customerId;
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
        }

        private void btnCars_Click(object sender, EventArgs e)
        {
            LoadContentIntoPanel(new PurchaseCarsForm());
        }

        private void btnCarParts_Click(object sender, EventArgs e)
        {
            LoadContentIntoPanel(new CarPartsForm());
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            LoadContentIntoPanel(new OrderForm(customerId));
        }

        private void btnOrderDetails_Click(object sender, EventArgs e)
        {
            LoadContentIntoPanel(new OrderDetailsForm());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }
}
