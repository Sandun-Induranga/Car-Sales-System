using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AD_Coursework_01
{
    public partial class OrderForm : UserControl
    {
        private DataTable cartTable = new DataTable(); // DataTable to hold cart items

        public OrderForm()
        {
            InitializeComponent();
            InitializeCart(); // Initialize the cart on form load
            LoadAvailableItems(); // Load available cars and car parts
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            dgvAvailableCars.SelectionChanged += dgvAvailableCars_SelectionChanged; // Event when a car row is clicked
            dgvAvailableParts.SelectionChanged += dgvAvailableParts_SelectionChanged; // Event when a part row is clicked
            btnAddToCart.Click += btnAddToCart_Click;
            btnRemoveFromCart.Click += btnRemoveFromCart_Click;
            btnFinalizePurchase.Click += btnFinalizePurchase_Click;
            txtCarSearch.TextChanged += TxtCarSearch_TextChanged;
            txtCarPartSearch.TextChanged += TxtCarPartsSearch_TextChanged;
        }

        private void InitializeCart()
        {
            // Define columns for the cart
            cartTable.Columns.Add("ItemID", typeof(string));
            cartTable.Columns.Add("ItemName", typeof(string));
            cartTable.Columns.Add("ItemType", typeof(string)); // "Car" or "Part"
            cartTable.Columns.Add("Quantity", typeof(int));
            cartTable.Columns.Add("Price", typeof(decimal));
            cartTable.Columns.Add("Total", typeof(decimal), "Quantity * Price");

            // Set the primary key for the cartTable
            cartTable.PrimaryKey = new DataColumn[] { cartTable.Columns["ItemID"] };

            // Bind the cart DataTable to a DataGridView control
            dgvCart.DataSource = cartTable;
        }

        private void LoadAvailableItems()
        {
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;

            // Get the search terms from the search fields
            string carSearchTerm = txtCarSearch.Text.Trim();
            string partSearchTerm = txtCarPartSearch.Text.Trim();

            // Define the queries with search conditions
            string carQuery = "SELECT CarID, CONCAT(Brand, ' ', Model) AS CarName, Price FROM Car " +
            "WHERE CarID LIKE @CarSearch OR Brand + ' ' + Model LIKE @CarSearch";

            string partQuery = "SELECT PartID, PartName, Price FROM CarPart " +
    "WHERE PartID LIKE @PartSearch OR PartName LIKE @PartSearch";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Load Cars
                SqlDataAdapter carAdapter = new SqlDataAdapter(carQuery, connection);
                carAdapter.SelectCommand.Parameters.AddWithValue("@CarSearch", $"%{carSearchTerm}%");
                DataTable availableCarsTable = new DataTable();
                carAdapter.Fill(availableCarsTable);
                dgvAvailableCars.DataSource = availableCarsTable;

                // Load Car Parts
                SqlDataAdapter partAdapter = new SqlDataAdapter(partQuery, connection);
                partAdapter.SelectCommand.Parameters.AddWithValue("@PartSearch", $"%{partSearchTerm}%");
                DataTable availablePartsTable = new DataTable();
                partAdapter.Fill(availablePartsTable);
                dgvAvailableParts.DataSource = availablePartsTable;
            }
        }

        private void TxtCarSearch_TextChanged(object sender, EventArgs e)
        {
            LoadAvailableItems(); // Reload the items based on the search term
        }

        private void TxtCarPartsSearch_TextChanged(object sender, EventArgs e)
        {
            LoadAvailableItems(); // Reload the items based on the search term
        }

        private void dgvAvailableCars_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAvailableCars.SelectedRows.Count > 0)
            {
                lblItemType.Text = "Car"; // Indicate the item type
            }
        }

        private void dgvAvailableParts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAvailableParts.SelectedRows.Count > 0)
            {
                lblItemType.Text = "Part"; // Indicate the item type
            }
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (lblItemType.Text == "Car" && dgvAvailableCars.SelectedRows.Count > 0)
            {
                var selectedRow = dgvAvailableCars.SelectedRows[0];
                AddToCart(selectedRow.Cells["CarID"].Value.ToString(), selectedRow.Cells["CarName"].Value.ToString(), lblItemType.Text, Convert.ToDecimal(selectedRow.Cells["Price"].Value));
            }
            else if (lblItemType.Text == "Part" && dgvAvailableParts.SelectedRows.Count > 0)
            {
                var selectedRow = dgvAvailableParts.SelectedRows[0];
                AddToCart(selectedRow.Cells["PartID"].Value.ToString(), selectedRow.Cells["PartName"].Value.ToString(), lblItemType.Text, Convert.ToDecimal(selectedRow.Cells["Price"].Value));
            }
            else
            {
                MessageBox.Show("Please select an item before adding to the cart.");
            }
        }

        private void AddToCart(string itemID, string itemName, string itemType, decimal price)
        {
            int quantity = Convert.ToInt32(nudQuantity.Value);

            if (quantity <= 0)
            {
                MessageBox.Show("Quantity must be greater than 0.");
                return;
            }

            DataRow existingRow = cartTable.Rows.Find(itemID);
            if (existingRow != null)
            {
                existingRow["Quantity"] = (int)existingRow["Quantity"] + quantity;
            }
            else
            {
                cartTable.Rows.Add(itemID, itemName, itemType, quantity, price);
            }
        }

        private void btnRemoveFromCart_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                dgvCart.Rows.RemoveAt(dgvCart.SelectedRows[0].Index);
            }
        }

        private void btnFinalizePurchase_Click(object sender, EventArgs e)
        {
            if (cartTable.Rows.Count > 0)
            {
                string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // Generate the next OrderID
                        string newOrderId = GenerateOrderId(connection, transaction);

                        string insertOrderQuery = "INSERT INTO [Order] (OrderID, CustomerID, OrderDate, TotalAmount, OrderStatus) " +
                                                  "VALUES (@OrderID, @CustomerID, GETDATE(), @TotalAmount, 'Pending');";
                        SqlCommand insertOrderCommand = new SqlCommand(insertOrderQuery, connection, transaction);
                        insertOrderCommand.Parameters.AddWithValue("@OrderID", newOrderId);
                        insertOrderCommand.Parameters.AddWithValue("@CustomerID", /* CustomerID */ 1); // Replace with actual customer ID
                        insertOrderCommand.Parameters.AddWithValue("@TotalAmount", CalculateTotalAmount());

                        insertOrderCommand.ExecuteNonQuery();

                        foreach (DataRow row in cartTable.Rows)
                        {
                            string insertOrderDetailsQuery = "INSERT INTO OrderDetails (OrderID, ItemType, ItemID, Quantity, Price) " +
                                                             "VALUES (@OrderID, @ItemType, @ItemID, @Quantity, @Price)";
                            SqlCommand insertOrderDetailsCommand = new SqlCommand(insertOrderDetailsQuery, connection, transaction);
                            insertOrderDetailsCommand.Parameters.AddWithValue("@OrderID", newOrderId);
                            insertOrderDetailsCommand.Parameters.AddWithValue("@ItemType", row["ItemType"]);
                            insertOrderDetailsCommand.Parameters.AddWithValue("@ItemID", row["ItemID"]);
                            insertOrderDetailsCommand.Parameters.AddWithValue("@Quantity", row["Quantity"]);
                            insertOrderDetailsCommand.Parameters.AddWithValue("@Price", row["Price"]);

                            insertOrderDetailsCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show($"Purchase completed successfully! Order ID: {newOrderId}");

                        GenerateBill(newOrderId);
                        cartTable.Clear();
                    }
                    catch (Exception ex)
                    {
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

        private string GenerateOrderId(SqlConnection connection, SqlTransaction transaction)
        {
            string query = "SELECT TOP 1 OrderID FROM [Order] ORDER BY OrderID DESC";
            SqlCommand cmd = new SqlCommand(query, connection, transaction);
            var lastOrderId = cmd.ExecuteScalar() as string;

            if (lastOrderId != null)
            {
                int newOrderIdNumber = int.Parse(lastOrderId.Substring(4)) + 1;
                return "ORD-" + newOrderIdNumber.ToString("D3"); // Format with leading zeros
            }
            else
            {
                return "ORD-001"; // Start with ORD-001 if no records are found
            }
        }

            private decimal CalculateTotalAmount()
        {
            decimal total = 0;
            foreach (DataRow row in cartTable.Rows)
            {
                total += (decimal)row["Total"];
            }
            return total;
        }

        private void GenerateBill(string orderID)
        {
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            string fileName = $"Bill_{orderID}.pdf";
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));

            document.Open();

            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            Paragraph title = new Paragraph("Invoice", titleFont)
            {
                Alignment = Element.ALIGN_CENTER
            };
            document.Add(title);

            document.Add(new Paragraph($"Order ID: {orderID}"));
            document.Add(new Paragraph("--------------------------------------"));

            PdfPTable table = new PdfPTable(5);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 40f, 20f, 10f, 15f, 15f });

            table.AddCell("Item Name");
            table.AddCell("Item Type");
            table.AddCell("Quantity");
            table.AddCell("Price");
            table.AddCell("Total");

            foreach (DataRow row in cartTable.Rows)
            {
                table.AddCell(row["ItemName"].ToString());
                table.AddCell(row["ItemType"].ToString());
                table.AddCell(row["Quantity"].ToString());
                table.AddCell(row["Price"].ToString());
                table.AddCell(row["Total"].ToString());
            }

            document.Add(table);

            document.Add(new Paragraph("--------------------------------------"));
            document.Add(new Paragraph($"Total Amount: {CalculateTotalAmount()}"));

            document.Close();
            writer.Close();

            MessageBox.Show("Bill generated as PDF!", "Success");

            System.Diagnostics.Process.Start(fileName);
        }
    }
}
