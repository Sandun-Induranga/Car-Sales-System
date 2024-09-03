using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace AD_Coursework_01
{
    public partial class GenerateReportForm : UserControl
    {
        public GenerateReportForm()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            btnGenerateIncomeReport.Click += btnGenerateIncomeReport_Click; // Handle button click event
            btnGenerateMonthlyReport.Click += btnGenerateMonthlyReport_Click; // Handle button click event
            btnGenerateAnnualReport.Click += btnGenerateAnnualReport_Click; // Handle button click event
        }

        private void btnGenerateIncomeReport_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtpStartDate.Value.Date; // Get the selected start date
            DateTime endDate = dtpEndDate.Value.Date; // Get the selected end date

            string query = "SELECT OrderID, CustomerID, OrderDate, TotalAmount, OrderStatus " +
                           "FROM [Order] WHERE OrderDate BETWEEN @StartDate AND @EndDate"; // SQL query to get orders within the date range

            GenerateReport(query, startDate, endDate, "IncomeReport.xlsx"); // Generate the report
        }

        private void btnGenerateMonthlyReport_Click(object sender, EventArgs e)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); // Start of the current month
            DateTime endDate = startDate.AddMonths(1).AddDays(-1); // End of the current month

            string query = "SELECT OrderID, CustomerID, OrderDate, TotalAmount, OrderStatus " +
                           "FROM [Order] WHERE OrderDate BETWEEN @StartDate AND @EndDate"; // SQL query to get orders within the month

            GenerateReport(query, startDate, endDate, "MonthlyReport.xlsx"); // Generate the report
        }

        private void btnGenerateAnnualReport_Click(object sender, EventArgs e)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, 1, 1); // Start of the current year
            DateTime endDate = new DateTime(DateTime.Now.Year, 12, 31); // End of the current year

            string query = "SELECT OrderID, CustomerID, OrderDate, TotalAmount, OrderStatus " +
                           "FROM [Order] WHERE OrderDate BETWEEN @StartDate AND @EndDate"; // SQL query to get orders within the year

            GenerateReport(query, startDate, endDate, "AnnualReport.xlsx"); // Generate the report
        }

        private void GenerateReport(string query, DateTime startDate, DateTime endDate, string reportFileName)
        {
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;
            DataTable orderTable = new DataTable();
            decimal totalDeliveredAmount = 0;
            int orderCount = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection); // Create a data adapter
                    adapter.SelectCommand.Parameters.AddWithValue("@StartDate", startDate); // Add the start date parameter
                    adapter.SelectCommand.Parameters.AddWithValue("@EndDate", endDate); // Add the end date parameter
                    adapter.Fill(orderTable);

                    // Calculate total amount for delivered orders and count of orders
                    foreach (DataRow row in orderTable.Rows)
                    {
                        orderCount++;
                        if (row["OrderStatus"].ToString().Equals("Delivered", StringComparison.OrdinalIgnoreCase)) // Check if the order is delivered
                        {
                            totalDeliveredAmount += (decimal)row["TotalAmount"]; // Add the total amount
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                    return;
                }
            }

            // Generate Excel report
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Report");

                // Add a title
                var titleCell = worksheet.Cell(1, 1);
                titleCell.Value = "Order Report";
                titleCell.Style.Font.Bold = true;
                titleCell.Style.Font.FontSize = 18;
                titleCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Range(1, 1, 1, 5).Merge(); // Merge the title across the top row

                // Add summary information
                worksheet.Cell(3, 1).Value = "Total Orders:";
                worksheet.Cell(3, 2).Value = orderCount;
                worksheet.Cell(4, 1).Value = "Total Delivered Order Amount:";
                worksheet.Cell(4, 2).Value = totalDeliveredAmount.ToString("C");

                // Add headers
                var headers = new[] { "Order ID", "Customer ID", "Order Date", "Total Amount", "Order Status" };
                for (int col = 0; col < headers.Length; col++)
                {
                    var headerCell = worksheet.Cell(6, col + 1);
                    headerCell.Value = headers[col];
                    headerCell.Style.Font.Bold = true;
                    headerCell.Style.Fill.BackgroundColor = XLColor.LightGray;
                    headerCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }

                // Add data rows
                for (int i = 0; i < orderTable.Rows.Count; i++)
                {
                    worksheet.Cell(i + 7, 1).Value = (string)orderTable.Rows[i]["OrderID"];
                    worksheet.Cell(i + 7, 2).Value = (int)orderTable.Rows[i]["CustomerID"];
                    worksheet.Cell(i + 7, 3).Value = ((DateTime)orderTable.Rows[i]["OrderDate"]).ToString("dd/MM/yyyy");
                    worksheet.Cell(i + 7, 4).Value = ((decimal)orderTable.Rows[i]["TotalAmount"]).ToString("C");
                    worksheet.Cell(i + 7, 5).Value = (string)orderTable.Rows[i]["OrderStatus"];
                }

                // Adjust column widths to fit the content
                worksheet.Columns().AdjustToContents();

                // Apply borders to the data table
                var dataRange = worksheet.Range(6, 1, orderTable.Rows.Count + 6, 5);
                dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                // Save the Excel file
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                    saveFileDialog.FileName = reportFileName;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        workbook.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Report generated successfully!");

                        // Open the generated report
                        if (MessageBox.Show("Do you want to open the generated report?", "Open Report", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(saveFileDialog.FileName);
                        }
                    }
                }
            }
        }
    }
}
