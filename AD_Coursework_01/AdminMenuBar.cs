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
    public partial class AdminMenuBar : Form
    {
        public AdminMenuBar()
        {
            InitializeComponent();
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
            LoadContentIntoPanel(new OrderDetailsForm());
        }
    }
}
