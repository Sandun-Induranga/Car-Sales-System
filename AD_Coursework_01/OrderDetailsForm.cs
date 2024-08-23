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
            LoadOrders(); // Load all orders on form load
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            dgvOrders.SelectionChanged += dgvOrders_SelectionChanged;
            btnUpdateStatus.Click += btnUpdateStatus_Click;
        }

        private void LoadOrders()
        {
            // Load orders into a DataGridView
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;
            string query = "SELECT OrderID, CustomerID, OrderDate, TotalAmount, OrderStatus FROM [Order]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(orderTable);

                dgvOrders.DataSource = orderTable; // Bind to a DataGridView
            }
        }

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            // Load order details when an order is selected
            if (dgvOrders.SelectedRows.Count > 0)
            {
                var selectedRow = dgvOrders.SelectedRows[0];
                int orderID = Convert.ToInt32(selectedRow.Cells["OrderID"].Value);

                LoadOrderDetails(orderID);
                cmbStatus.SelectedItem = selectedRow.Cells["OrderStatus"].Value.ToString();
            }
        }

        private void LoadOrderDetails(int orderID)
        {
            // Load order details into a DataGridView
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;
            string query = "SELECT ItemID, ItemType, Quantity, Price FROM OrderDetails WHERE OrderID = @OrderID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@OrderID", orderID);

                orderDetailsTable.Clear(); // Clear the previous details
                adapter.Fill(orderDetailsTable);

                dgvOrderDetails.DataSource = orderDetailsTable; // Bind to a DataGridView
            }
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            // Update the status of the selected order
            if (dgvOrders.SelectedRows.Count > 0)
            {
                var selectedRow = dgvOrders.SelectedRows[0];
                int orderID = Convert.ToInt32(selectedRow.Cells["OrderID"].Value);
                string newStatus = cmbStatus.SelectedItem.ToString();

                string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;
                string query = "UPDATE [Order] SET OrderStatus = @OrderStatus WHERE OrderID = @OrderID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@OrderStatus", newStatus);
                    command.Parameters.AddWithValue("@OrderID", orderID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Order status updated successfully!");

                // Refresh the order table to reflect the changes
                LoadOrders();
            }
            else
            {
                MessageBox.Show("Please select an order to update.");
            }
        }
    }
}
