using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            btnGenerateIncomeReport.Click += btnGenerateIncomeReport_Click;
            btnGenerateMonthlyReport.Click += btnGenerateMonthlyReport_Click;
            btnGenerateAnnualReport.Click += btnGenerateAnnualReport_Click;
        }

        private void btnGenerateIncomeReport_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;

            string query = "SELECT OrderID, CustomerID, OrderDate, TotalAmount, OrderStatus " +
                           "FROM [Order] WHERE OrderDate BETWEEN @StartDate AND @EndDate";

            GenerateReport(query, startDate, endDate, "IncomeReport.xlsx");
        }

        private void btnGenerateMonthlyReport_Click(object sender, EventArgs e)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1); // End of the current month

            string query = "SELECT OrderID, CustomerID, OrderDate, TotalAmount, OrderStatus " +
                           "FROM [Order] WHERE OrderDate BETWEEN @StartDate AND @EndDate";

            GenerateReport(query, startDate, endDate, "MonthlyReport.xlsx");
        }

        private void btnGenerateAnnualReport_Click(object sender, EventArgs e)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime endDate = new DateTime(DateTime.Now.Year, 12, 31);

            string query = "SELECT OrderID, CustomerID, OrderDate, TotalAmount, OrderStatus " +
                           "FROM [Order] WHERE OrderDate BETWEEN @StartDate AND @EndDate";

            GenerateReport(query, startDate, endDate, "AnnualReport.xlsx");
        }

        private void GenerateReport(string query, DateTime startDate, DateTime endDate, string reportFileName)
        {
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;
            DataTable orderTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@StartDate", startDate);
                    adapter.SelectCommand.Parameters.AddWithValue("@EndDate", endDate);
                    adapter.Fill(orderTable);
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

                // Add headers
                worksheet.Cell(1, 1).Value = "Order ID";
                worksheet.Cell(1, 2).Value = "Customer ID";
                worksheet.Cell(1, 3).Value = "Order Date";
                worksheet.Cell(1, 4).Value = "Total Amount";
                worksheet.Cell(1, 5).Value = "Order Status";

                // Add data rows
                for (int i = 0; i < orderTable.Rows.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = (string)orderTable.Rows[i]["OrderID"];
                    worksheet.Cell(i + 2, 2).Value = (int)orderTable.Rows[i]["CustomerID"];
                    worksheet.Cell(i + 2, 3).Value = (DateTime)orderTable.Rows[i]["OrderDate"];
                    worksheet.Cell(i + 2, 4).Value = (decimal)orderTable.Rows[i]["TotalAmount"];
                    worksheet.Cell(i + 2, 5).Value = (string)orderTable.Rows[i]["OrderStatus"];
                }

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
