using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AD_Coursework_01
{
    public partial class CustomerDashboard : UserControl
    {
        private string customerId;
        public CustomerDashboard(string customerId)
        {
            InitializeComponent();
            this.customerId = customerId;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadDashboardData(); // Load data from the database when the dashboard is loaded
        }

        private void LoadDashboardData()
        {
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Load the number of available cars
                    string carCountQuery = "SELECT COUNT(*) FROM Car";
                    SqlCommand carCountCommand = new SqlCommand(carCountQuery, connection);
                    int carCount = (int)carCountCommand.ExecuteScalar();
                    lblAvailableCars.Text = carCount.ToString(); // Set the label to display car count

                    // Load the number of available car parts
                    string carPartCountQuery = "SELECT COUNT(*) FROM CarPart";
                    SqlCommand carPartCountCommand = new SqlCommand(carPartCountQuery, connection);
                    int carPartCount = (int)carPartCountCommand.ExecuteScalar();
                    lblAvailableCarParts.Text = carPartCount.ToString(); // Set the label to display car part count

                    // Load the number of orders
                    string orderCountQuery = "SELECT COUNT(*) FROM [Order] WHERE CustomerId=@CustomerId";
                    SqlCommand orderCountCommand = new SqlCommand(orderCountQuery, connection);
                    orderCountCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    int orderCount = (int)orderCountCommand.ExecuteScalar();
                    lblTotalOrders.Text = orderCount.ToString(); // Set the label to display order count

                    // Set the date label to today's date
                    lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
