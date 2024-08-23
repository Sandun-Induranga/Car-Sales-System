using System;
using System.Windows.Forms;

namespace AD_Coursework_01
{
    public partial class CustomerMenuBar : Form
    {
        public CustomerMenuBar()
        {
            InitializeComponent();
            LoadContentIntoPanel(new CustomerDashboard());
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
            LoadContentIntoPanel(new OrderForm());
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {

        }
    }
}
