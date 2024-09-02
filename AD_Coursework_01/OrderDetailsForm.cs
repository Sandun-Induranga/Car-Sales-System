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

        // Property to hold CustomerID (nullable)
        private string CustomerID { get; set; }

        public OrderDetailsForm(string customerId = null)
        {
            InitializeComponent();
            this.CustomerID = customerId; // Set CustomerID
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadOrders(); // Load all orders or orders for a specific customer on form load
            dgvOrders.SelectionChanged += dgvOrders_SelectionChanged; // Load order details when an order is selected

            if (string.IsNullOrEmpty(CustomerID))
            {
                AddUpdateStatusButtonColumn(); // Add "Update Status" button if CustomerID is null
            }
            else
            {
                AddCancelOrderButtonColumn(); // Add "Cancel Order" button if CustomerID is provided
            }
        }

        private void LoadOrders()
        {
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;
            string query = "SELECT OrderID, CustomerID, OrderDate, TotalAmount, OrderStatus FROM [Order]";

            if (!string.IsNullOrEmpty(CustomerID))
            {
                query += " WHERE CustomerID = @CustomerID";
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    orderTable.Clear();

                    if (!string.IsNullOrEmpty(CustomerID))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@CustomerID", CustomerID);
                    }

                    adapter.Fill(orderTable);

                    dgvOrders.DataSource = orderTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }

        private void AddUpdateStatusButtonColumn()
        {
            DataGridViewButtonColumn updateButtonColumn = new DataGridViewButtonColumn();
            updateButtonColumn.Name = "UpdateStatus";
            updateButtonColumn.HeaderText = "Update Status";
            updateButtonColumn.Text = "Update";
            updateButtonColumn.UseColumnTextForButtonValue = true;

            dgvOrders.Columns.Add(updateButtonColumn);
            dgvOrders.CellClick += dgvOrders_CellClick; // Handle button click events
        }

        private void AddCancelOrderButtonColumn()
        {
            DataGridViewButtonColumn cancelButtonColumn = new DataGridViewButtonColumn();
            cancelButtonColumn.Name = "CancelOrder";
            cancelButtonColumn.HeaderText = "Cancel Order";
            cancelButtonColumn.Text = "Cancel";
            cancelButtonColumn.UseColumnTextForButtonValue = true;

            dgvOrders.Columns.Add(cancelButtonColumn);
            dgvOrders.CellClick += dgvOrders_CellClick; // Handle button click events
        }

        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string orderID = dgvOrders.Rows[e.RowIndex].Cells["OrderID"].Value.ToString();

                if (string.IsNullOrEmpty(CustomerID) && e.ColumnIndex == dgvOrders.Columns["UpdateStatus"].Index)
                {
                    // Admin updating status
                    UpdateOrderStatus(orderID);
                }
                else if (!string.IsNullOrEmpty(CustomerID) && e.ColumnIndex == dgvOrders.Columns["CancelOrder"].Index)
                {
                    // Customer cancelling order
                    CancelOrder(orderID);
                }
            }
        }

        private void UpdateOrderStatus(string orderID)
        {
            string[] statuses = { "Pending", "Shipped", "Delivered", "Cancelled" };
            string newStatus = PromptForStatus(statuses);

            if (!string.IsNullOrEmpty(newStatus))
            {
                string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;
                string query = "UPDATE [Order] SET OrderStatus = @OrderStatus WHERE OrderID = @OrderID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@OrderStatus", newStatus);
                        command.Parameters.AddWithValue("@OrderID", orderID);

                        connection.Open();
                        command.ExecuteNonQuery();

                        MessageBox.Show("Order status updated successfully.");

                        // Refresh the orders grid
                        LoadOrders();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating order status: " + ex.Message);
                    }
                }
            }
        }

        private string PromptForStatus(string[] statuses)
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 500;
                prompt.Height = 150;
                prompt.Text = "Select New Order Status";

                Label textLabel = new Label() { Left = 50, Top = 20, Text = "Order Status:" };
                ComboBox comboBox = new ComboBox() { Left = 150, Top = 20, Width = 200 };
                comboBox.Items.AddRange(statuses);
                comboBox.SelectedIndex = 0;

                Button confirmation = new Button() { Text = "OK", Left = 150, Width = 100, Top = 70 };
                confirmation.Click += (sender, e) => { prompt.DialogResult = DialogResult.OK; prompt.Close(); };

                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(comboBox);
                prompt.Controls.Add(confirmation);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? comboBox.SelectedItem.ToString() : null;
            }
        }

        private void CancelOrder(string orderID)
        {
            var result = MessageBox.Show("Are you sure you want to cancel this order?", "Cancel Order", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;
                string query = "UPDATE [Order] SET OrderStatus = 'Cancelled' WHERE OrderID = @OrderID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@OrderID", orderID);

                        connection.Open();
                        command.ExecuteNonQuery();

                        MessageBox.Show("Order cancelled successfully.");

                        // Refresh the orders grid
                        LoadOrders();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error cancelling order: " + ex.Message);
                    }
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

                LoadOrderDetails(orderID);
                LoadCustomerDetails(customerID);
            }
        }

        private void LoadOrderDetails(string orderID)
        {
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;
            string query = "SELECT ItemID, ItemType, Quantity, Price FROM OrderDetails WHERE OrderID = @OrderID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@OrderID", orderID);

                    orderDetailsTable.Clear();
                    adapter.Fill(orderDetailsTable);

                    dgvOrderDetails.DataSource = orderDetailsTable;
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
