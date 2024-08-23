using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AD_Coursework_01
{
    public partial class OrderForm : UserControl
    {
        private DataTable cartTable = new DataTable(); // DataTable to hold cart items

        public OrderForm()
        {
            InitializeComponent();
            InitializeCart(); // Initialize the cart on form load
            LoadAvailableCars(); // Load available cars only
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            dgvAvailableCars.SelectionChanged += dgvAvailableCars_SelectionChanged; // Event when row is clicked
            btnAddToCart.Click += btnAddToCart_Click;
            btnFinalizePurchase.Click += btnFinalizePurchase_Click;
        }

        private void InitializeCart()
        {
            // Define columns for the cart
            cartTable.Columns.Add("CarID", typeof(int));
            cartTable.Columns.Add("CarName", typeof(string));
            cartTable.Columns.Add("Quantity", typeof(int));
            cartTable.Columns.Add("Price", typeof(decimal));
            cartTable.Columns.Add("Total", typeof(decimal), "Quantity * Price");

            // Set the primary key for the cartTable
            cartTable.PrimaryKey = new DataColumn[] { cartTable.Columns["CarID"] };

            // Bind the cart DataTable to a DataGridView control
            dgvCart.DataSource = cartTable;
        }

        private void LoadAvailableCars()
        {
            // Load available cars into a DataGridView
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;
            string query = "SELECT CarID, CONCAT(Brand, ' ', Model) AS CarName, Price FROM Car";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable availableCarsTable = new DataTable();
                adapter.Fill(availableCarsTable);

                dgvAvailableCars.DataSource = availableCarsTable; // Bind to a DataGridView
            }
        }

        private void dgvAvailableCars_SelectionChanged(object sender, EventArgs e)
        {
            // Load data from the selected row into text fields
            if (dgvAvailableCars.SelectedRows.Count > 0)
            {
                var selectedRow = dgvAvailableCars.SelectedRows[0];
                txtCarID.Text = selectedRow.Cells["CarID"].Value.ToString();
                txtCarName.Text = selectedRow.Cells["CarName"].Value.ToString();
                txtPrice.Text = selectedRow.Cells["Price"].Value.ToString();
            }
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            // Add selected car to the cart
            int carID = Convert.ToInt32(txtCarID.Text);
            string carName = txtCarName.Text;
            decimal price = Convert.ToDecimal(txtPrice.Text);
            int quantity = Convert.ToInt32(nudQuantity.Value); // Assume nudQuantity is a NumericUpDown control for quantity

            // Check if the car is already in the cart
            DataRow existingRow = cartTable.Rows.Find(carID);
            if (existingRow != null)
            {
                // Update quantity and total if car is already in the cart
                existingRow["Quantity"] = (int)existingRow["Quantity"] + quantity;
            }
            else
            {
                // Add new car to the cart
                cartTable.Rows.Add(carID, carName, quantity, price);
            }
        }

        private void btnRemoveFromCart_Click(object sender, EventArgs e)
        {
            // Remove selected car from the cart
            if (dgvCart.SelectedRows.Count > 0)
            {
                dgvCart.Rows.RemoveAt(dgvCart.SelectedRows[0].Index);
            }
        }

        private void btnFinalizePurchase_Click(object sender, EventArgs e)
        {
            // Finalize the purchase and save the order to the database
            if (cartTable.Rows.Count > 0)
            {
                string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // Insert the order into the Order table
                        string insertOrderQuery = "INSERT INTO [Order] (CustomerID, OrderDate, TotalAmount, OrderStatus) " +
                                                  "VALUES (@CustomerID, GETDATE(), @TotalAmount, 'Pending'); " +
                                                  "SELECT SCOPE_IDENTITY();";
                        SqlCommand insertOrderCommand = new SqlCommand(insertOrderQuery, connection, transaction);
                        insertOrderCommand.Parameters.AddWithValue("@CustomerID", /* CustomerID */ 1); // Replace with actual customer ID
                        insertOrderCommand.Parameters.AddWithValue("@TotalAmount", CalculateTotalAmount());

                        int orderID = Convert.ToInt32(insertOrderCommand.ExecuteScalar());

                        // Insert each car in the cart into OrderDetails table
                        foreach (DataRow row in cartTable.Rows)
                        {
                            string insertOrderDetailsQuery = "INSERT INTO OrderDetails (OrderID, ItemType, ItemID, Quantity, Price) " +
                                                             "VALUES (@OrderID, 'Car', @CarID, @Quantity, @Price)";
                            SqlCommand insertOrderDetailsCommand = new SqlCommand(insertOrderDetailsQuery, connection, transaction);
                            insertOrderDetailsCommand.Parameters.AddWithValue("@OrderID", orderID);
                            insertOrderDetailsCommand.Parameters.AddWithValue("@CarID", row["CarID"]);
                            insertOrderDetailsCommand.Parameters.AddWithValue("@Quantity", row["Quantity"]);
                            insertOrderDetailsCommand.Parameters.AddWithValue("@Price", row["Price"]);

                            insertOrderDetailsCommand.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();
                        MessageBox.Show("Purchase completed successfully!");

                        // Clear the cart after purchase
                        cartTable.Clear();
                    }
                    catch (Exception ex)
                    {
                        // Rollback the transaction in case of error
                        transaction.Rollback();
                        MessageBox.Show($"An error occurred: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Your cart is empty.");
            }
        }

        private decimal CalculateTotalAmount()
        {
            // Calculate the total amount of all cars in the cart
            decimal total = 0;
            foreach (DataRow row in cartTable.Rows)
            {
                total += (decimal)row["Total"];
            }
            return total;
        }
    }
}
