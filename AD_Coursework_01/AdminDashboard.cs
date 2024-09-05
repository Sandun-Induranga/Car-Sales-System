using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace AD_Coursework_01
{
    public partial class AdminDashboard : UserControl
    {
        public AdminDashboard()
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
                    lblAvailableCars.Text = carCount.ToString(); // Set the label to display car count

                    // Load the number of available car parts
                    string carPartCountQuery = "SELECT COUNT(*) FROM CarPart";
                    SqlCommand carPartCountCommand = new SqlCommand(carPartCountQuery, connection);
                    int carPartCount = (int)carPartCountCommand.ExecuteScalar();
                    lblAvailableCarParts.Text = carPartCount.ToString(); // Set the label to display car part count

                    // Load the number of orders
                    string orderCountQuery = "SELECT COUNT(*) FROM [Order]";
                    SqlCommand orderCountCommand = new SqlCommand(orderCountQuery, connection);
                    int orderCount = (int)orderCountCommand.ExecuteScalar();
                    lblTotalSalesToday.Text = orderCount.ToString(); // Set the label to display order count

                    // Load total sales today
                    LoadTotalSalesToday(connection);

                    // Load data for the chart
                    LoadChartData(connection);

                    // Set the date label to today's date
                    lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

        private void LoadTotalSalesToday(SqlConnection connection)
        {
            try
            {
                // Query to calculate the total sales for today
                string totalSalesQuery = @"
                    SELECT ISNULL(SUM(TotalAmount), 0) 
                    FROM [Order]
                    WHERE CAST(OrderDate AS DATE) = CAST(GETDATE() AS DATE)";

                SqlCommand totalSalesCommand = new SqlCommand(totalSalesQuery, connection);
                decimal totalSalesToday = (decimal)totalSalesCommand.ExecuteScalar();

                // Set the label to display the total sales today
                lblTodaySales.Text = $"Today's Sales: {totalSalesToday:C}"; // Format as currency
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading total sales: {ex.Message}");
            }
        }

        private void LoadChartData(SqlConnection connection)
        {
            try
            {
                // Example query to get data for the chart (e.g., number of orders per month)
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
                chartSales.Series["Series1"].XValueMember = "Month";
                chartSales.Series["Series1"].YValueMembers = "Orders";
                chartSales.DataSource = chartDataTable;
                chartSales.DataBind();
                chartSales.Series["Series1"].LegendText = "Orders"; // Name the chart series

                // Name the chart axes
                chartSales.ChartAreas[0].AxisX.Title = "Month";
                chartSales.ChartAreas[0].AxisY.Title = "Number of Orders";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading chart data: {ex.Message}");
            }
        }
    }
}
