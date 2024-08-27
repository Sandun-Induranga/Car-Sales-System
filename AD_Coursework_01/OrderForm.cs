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
            btnFinalizePurchase.Click += btnFinalizePurchase_Click;
        }

        private void InitializeCart()
        {
            // Define columns for the cart
            cartTable.Columns.Add("ItemID", typeof(int));
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
            // Load available cars and parts into DataGridView controls
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;
            string carQuery = "SELECT CarID, CONCAT(Brand, ' ', Model) AS CarName, Price FROM Car";
            string partQuery = "SELECT PartID, PartName, Price FROM CarPart";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Load Cars
                SqlDataAdapter carAdapter = new SqlDataAdapter(carQuery, connection);
                DataTable availableCarsTable = new DataTable();
                carAdapter.Fill(availableCarsTable);
                dgvAvailableCars.DataSource = availableCarsTable;

                // Load Car Parts
                SqlDataAdapter partAdapter = new SqlDataAdapter(partQuery, connection);
                DataTable availablePartsTable = new DataTable();
                partAdapter.Fill(availablePartsTable);
                dgvAvailableParts.DataSource = availablePartsTable;
            }
        }

        private void dgvAvailableCars_SelectionChanged(object sender, EventArgs e)
        {
            // Load data from the selected car row into text fields
            if (dgvAvailableCars.SelectedRows.Count > 0)
            {
                var selectedRow = dgvAvailableCars.SelectedRows[0];
                txtCarID.Text = selectedRow.Cells["CarID"].Value.ToString();
                txtCarName.Text = selectedRow.Cells["CarName"].Value.ToString();
                txtPrice.Text = selectedRow.Cells["Price"].Value.ToString();
                lblItemType.Text = "Car"; // Indicate the item type
            }
        }

        private void dgvAvailableParts_SelectionChanged(object sender, EventArgs e)
        {
            // Load data from the selected part row into text fields
            if (dgvAvailableParts.SelectedRows.Count > 0)
            {
                var selectedRow = dgvAvailableParts.SelectedRows[0];
                txtCarID.Text = selectedRow.Cells["PartID"].Value.ToString();
                txtCarName.Text = selectedRow.Cells["PartName"].Value.ToString();
                txtPrice.Text = selectedRow.Cells["Price"].Value.ToString();
                lblItemType.Text = "Part"; // Ensure the item type is set to "Part"
            }
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            // Add selected car or part to the cart
            if (!string.IsNullOrEmpty(txtCarID.Text) && !string.IsNullOrEmpty(txtCarName.Text))
            {
                int itemID = Convert.ToInt32(txtCarID.Text);
                string itemName = txtCarName.Text;
                decimal price = Convert.ToDecimal(txtPrice.Text);
                int quantity = Convert.ToInt32(nudQuantity.Value); // Ensure quantity is greater than 0
                string itemType = lblItemType.Text; // Either "Car" or "Part"

                if (quantity <= 0)
                {
                    MessageBox.Show("Quantity must be greater than 0.");
                    return;
                }

                // Check if the item is already in the cart
                DataRow existingRow = cartTable.Rows.Find(itemID);
                if (existingRow != null)
                {
                    // Update quantity and total if the item is already in the cart
                    existingRow["Quantity"] = (int)existingRow["Quantity"] + quantity;
                }
                else
                {
                    // Add new item to the cart
                    cartTable.Rows.Add(itemID, itemName, itemType, quantity, price);
                }
            }
            else
            {
                MessageBox.Show("Please select an item before adding to the cart.");
            }
        }

        private void btnRemoveFromCart_Click(object sender, EventArgs e)
        {
            // Remove selected item from the cart
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

                        // Insert each item in the cart into OrderDetails table
                        foreach (DataRow row in cartTable.Rows)
                        {
                            string insertOrderDetailsQuery = "INSERT INTO OrderDetails (OrderID, ItemType, ItemID, Quantity, Price) " +
                                                             "VALUES (@OrderID, @ItemType, @ItemID, @Quantity, @Price)";
                            SqlCommand insertOrderDetailsCommand = new SqlCommand(insertOrderDetailsQuery, connection, transaction);
                            insertOrderDetailsCommand.Parameters.AddWithValue("@OrderID", orderID);
                            insertOrderDetailsCommand.Parameters.AddWithValue("@ItemType", row["ItemType"]);
                            insertOrderDetailsCommand.Parameters.AddWithValue("@ItemID", row["ItemID"]);
                            insertOrderDetailsCommand.Parameters.AddWithValue("@Quantity", row["Quantity"]);
                            insertOrderDetailsCommand.Parameters.AddWithValue("@Price", row["Price"]);

                            insertOrderDetailsCommand.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();
                        MessageBox.Show("Purchase completed successfully!");

                        // Generate a bill (optional)
                        GenerateBill(orderID);

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
            // Calculate the total amount of all items in the cart
            decimal total = 0;
            foreach (DataRow row in cartTable.Rows)
            {
                total += (decimal)row["Total"];
            }
            return total;
        }

        private void GenerateBill(int orderID)
        {
            // Create a PDF document
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            string fileName = $"Bill_{orderID}.pdf";
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));

            document.Open();

            // Add title
            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            Paragraph title = new Paragraph("Invoice", titleFont)
            {
                Alignment = Element.ALIGN_CENTER
            };
            document.Add(title);

            // Add order details
            document.Add(new Paragraph($"Order ID: {orderID}"));
            document.Add(new Paragraph("--------------------------------------"));

            // Add table with bill details
            PdfPTable table = new PdfPTable(5); // 5 columns: ItemName, ItemType, Quantity, Price, Total
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 40f, 20f, 10f, 15f, 15f });

            // Add table headers
            table.AddCell("Item Name");
            table.AddCell("Item Type");
            table.AddCell("Quantity");
            table.AddCell("Price");
            table.AddCell("Total");

            // Add rows from cartTable
            foreach (DataRow row in cartTable.Rows)
            {
                table.AddCell(row["ItemName"].ToString());
                table.AddCell(row["ItemType"].ToString());
                table.AddCell(row["Quantity"].ToString());
                table.AddCell(row["Price"].ToString());
                table.AddCell(row["Total"].ToString());
            }

            document.Add(table);

            // Add total amount
            document.Add(new Paragraph("--------------------------------------"));
            document.Add(new Paragraph($"Total Amount: {CalculateTotalAmount()}"));

            // Close the document
            document.Close();
            writer.Close();

            MessageBox.Show("Bill generated as PDF!", "Success");

            // Optionally, open the PDF after creating it
            System.Diagnostics.Process.Start(fileName);

        }
    }
}
