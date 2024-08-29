using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AD_Coursework_01
{
    public partial class OrderDetailsForm : UserControl
    {
        private DataTable orderTable = new DataTable(); // DataTable to hold orders
        private DataTable orderDetailsTable = new DataTable(); // DataTable to hold order details

        public OrderDetailsForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadOrders(); // Load all orders on form load
            dgvOrders.SelectionChanged += dgvOrders_SelectionChanged; // Load order details when an order is selected
        }

        private void LoadOrders()
        {
            // Load orders into a DataGridView
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;
            string query = "SELECT OrderID, CustomerID, OrderDate, TotalAmount, OrderStatus FROM [Order]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    orderTable.Clear(); // Clear the previous orders
                    adapter.Fill(orderTable); // Fill the DataTable with data

                    dgvOrders.DataSource = orderTable; // Bind the DataGridView to the DataTable
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count > 0)
            {
                var selectedRow = dgvOrders.SelectedRows[0];
                string orderID = selectedRow.Cells["OrderID"].Value.ToString();
                string customerID = selectedRow.Cells["CustomerID"].Value.ToString();

                LoadOrderDetails(orderID); // Load order details based on selected OrderID
                LoadCustomerDetails(customerID); // Load customer details based on CustomerID
            }
        }

        private void LoadOrderDetails(string orderID)
        {
            // Load order details into a DataGridView
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;
            string query = "SELECT ItemID, ItemType, Quantity, Price FROM OrderDetails WHERE OrderID = @OrderID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@OrderID", orderID);

                    orderDetailsTable.Clear(); // Clear the previous details
                    adapter.Fill(orderDetailsTable); // Fill the DataTable with data

                    dgvOrderDetails.DataSource = orderDetailsTable; // Bind the DataGridView to the DataTable
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }

        private void LoadCustomerDetails(string customerID)
        {
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;
            string query = "SELECT FirstName, LastName, Address, Phone, Email FROM Customer WHERE CustomerID = @CustomerID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CustomerID", customerID);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        lblCusName.Text = $"Customer Name: {reader["FirstName"]} {reader["LastName"]}";
                        lblAddress.Text = $"Address: {reader["Address"]}";
                        lblMobile.Text = $"Mobile: {reader["Phone"]}";
                        lblEmail.Text = $"Email: {reader["Email"]}";
                    }
                    else
                    {
                        // Clear the labels if no customer data is found
                        lblCusName.Text = string.Empty;
                        lblAddress.Text = string.Empty;
                        lblMobile.Text = string.Empty;
                        lblEmail.Text = string.Empty;
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading customer details: " + ex.Message);
                }
            }
        }
    }
}
