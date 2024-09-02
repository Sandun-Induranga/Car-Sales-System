using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AD_Coursework_01
{
    public partial class CustomerDashboard : UserControl
    {
        public CustomerDashboard()
        {
            InitializeComponent();
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
                    label1.Text = carCount.ToString(); // Set the label to display car count

                    // Load the number of available car parts
                    string carPartCountQuery = "SELECT COUNT(*) FROM CarPart";
                    SqlCommand carPartCountCommand = new SqlCommand(carPartCountQuery, connection);
                    int carPartCount = (int)carPartCountCommand.ExecuteScalar();
                    label3.Text = carPartCount.ToString(); // Set the label to display car part count

                    // Load the number of orders
                    string orderCountQuery = "SELECT COUNT(*) FROM [Order]";
                    SqlCommand orderCountCommand = new SqlCommand(orderCountQuery, connection);
                    int orderCount = (int)orderCountCommand.ExecuteScalar();
                    label5.Text = orderCount.ToString(); // Set the label to display order count

                    // Load data for the chart
                    LoadChartData(connection);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

        private void LoadChartData(SqlConnection connection)
        {
            try
            {
                // Example query to get data for the chart (e.g., number of cars and car parts per month)
                string chartDataQuery = @"
                    SELECT
                        MONTH(OrderDate) AS [Month],
                        COUNT(*) AS [Orders]
                    FROM [Order]
                    GROUP BY MONTH(OrderDate)
                    ORDER BY [Month]";

                SqlDataAdapter chartDataAdapter = new SqlDataAdapter(chartDataQuery, connection);
                DataTable chartDataTable = new DataTable();
                chartDataAdapter.Fill(chartDataTable);

                // Bind data to chart
                chart1.Series["Series1"].XValueMember = "Month";
                chart1.Series["Series1"].YValueMembers = "Orders";
                chart1.DataSource = chartDataTable;
                chart1.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading chart data: {ex.Message}");
            }
        }
    }
}
