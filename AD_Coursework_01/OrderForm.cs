using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AD_Coursework_01
{
    public partial class OrderForm : UserControl
    {
        private DataTable cartTable = new DataTable(); // DataTable to hold cart items
        private string CustomerID; // Customer ID of the logged in user

        public OrderForm(string CustomerID)
        {
            InitializeComponent();
            InitializeCart(); // Initialize the cart on form load
            LoadAvailableItems(); // Load available cars and car parts
            this.CustomerID = CustomerID;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            dgvAvailableCars.SelectionChanged += dgvAvailableCars_SelectionChanged; // Event when a car row is clicked
            dgvAvailableParts.SelectionChanged += dgvAvailableParts_SelectionChanged; // Event when a part row is clicked
            btnAddToCart.Click += btnAddToCart_Click; // Add item to cart
            btnRemoveFromCart.Click += btnRemoveFromCart_Click; // Remove item from cart
            btnFinalizePurchase.Click += btnFinalizePurchase_Click; // Finalize the purchase
            txtCarSearch.TextChanged += TxtCarSearch_TextChanged; // Search for cars
            txtCarPartSearch.TextChanged += TxtCarPartsSearch_TextChanged; // Search for car parts
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
            string carQuery = "SELECT CarID, CONCAT(Brand, ' ', Model) AS CarName, Price, QtyOnHand FROM Car WHERE CarID LIKE @CarSearch OR Brand + ' ' + Model LIKE @CarSearch";

            string partQuery = "SELECT PartID, PartName, Price, QtyOnHand FROM CarPart WHERE PartID LIKE @PartSearch OR PartName LIKE @PartSearch";

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
                txtCarID.Text = dgvAvailableCars.SelectedRows[0].Cells["CarID"].Value.ToString();
                txtCarName.Text = dgvAvailableCars.SelectedRows[0].Cells["CarName"].Value.ToString();
                txtPrice.Text = dgvAvailableCars.SelectedRows[0].Cells["Price"].Value.ToString();
                nudQuantity.Maximum = Convert.ToInt32(dgvAvailableCars.SelectedRows[0].Cells["QtyOnHand"].Value); // Set max quantity
            }
        }

        private void dgvAvailableParts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAvailableParts.SelectedRows.Count > 0)
            {
                lblItemType.Text = "Part"; // Indicate the item type
                txtCarID.Text = dgvAvailableParts.SelectedRows[0].Cells["PartID"].Value.ToString();
                txtCarName.Text = dgvAvailableParts.SelectedRows[0].Cells["PartName"].Value.ToString();
                txtPrice.Text = dgvAvailableParts.SelectedRows[0].Cells["Price"].Value.ToString();
                nudQuantity.Maximum = Convert.ToInt32(dgvAvailableParts.SelectedRows[0].Cells["QtyOnHand"].Value); // Set max quantity
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

            // Check stock availability
            if (!IsStockAvailable(itemID, itemType, quantity))
            {
                MessageBox.Show("Insufficient stock available.");
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

            CalculateTotalAmount();
        }

        private bool IsStockAvailable(string itemID, string itemType, int requestedQuantity)
        {
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;
            string query = itemType == "Car" ? "SELECT QtyOnHand FROM Car WHERE CarID = @ItemID"
                                             : "SELECT QtyOnHand FROM CarPart WHERE PartID = @ItemID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ItemID", itemID);

                connection.Open();
                int qtyOnHand = (int)command.ExecuteScalar();

                return qtyOnHand >= requestedQuantity;
            }
        }

        private void btnRemoveFromCart_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                dgvCart.Rows.RemoveAt(dgvCart.SelectedRows[0].Index);
                CalculateTotalAmount(); // Update total after removing an item
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
                        insertOrderCommand.Parameters.AddWithValue("@CustomerID", CustomerID);
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

                            // Update stock quantity in the database
                            UpdateStockQuantity(row["ItemID"].ToString(), row["ItemType"].ToString(), (int)row["Quantity"], connection, transaction);
                        }

                        transaction.Commit();
                        MessageBox.Show($"Purchase completed successfully! Order ID: {newOrderId}");

                        GenerateBill(newOrderId);
                        cartTable.Clear();
                        CalculateTotalAmount();
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

        private void UpdateStockQuantity(string itemID, string itemType, int quantityPurchased, SqlConnection connection, SqlTransaction transaction)
        {
            string updateQuery = itemType == "Car" ? "UPDATE Car SET QtyOnHand = QtyOnHand - @Quantity WHERE CarID = @ItemID"
                                                   : "UPDATE CarPart SET QtyOnHand = QtyOnHand - @Quantity WHERE PartID = @ItemID";

            SqlCommand updateCommand = new SqlCommand(updateQuery, connection, transaction);
            updateCommand.Parameters.AddWithValue("@Quantity", quantityPurchased);
            updateCommand.Parameters.AddWithValue("@ItemID", itemID);

            updateCommand.ExecuteNonQuery();
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

            // Update the lblTotal label with the formatted total amount
            lblTotal.Text = $"{total:C}"; // C formats the number as currency

            return total;
        }

        private void GenerateBill(string orderID)
        {
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            string fileName = $"Bill_{orderID}.pdf";
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));

            document.Open();

            // Fonts and Styles
            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLACK);
            Font subTitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.GRAY);
            Font textFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);
            Font tableHeaderFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);
            Font tableCellFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);

            // Header with Company Info
            Paragraph companyName = new Paragraph("ABC Car Traders", titleFont);
            companyName.Alignment = Element.ALIGN_LEFT;
            document.Add(companyName);

            Paragraph companyDetails = new Paragraph("1234 Street Name, City, Country\nPhone: +1 234 567 890\nEmail: info@abccartraders.com", textFont);
            companyDetails.Alignment = Element.ALIGN_LEFT;
            companyDetails.SpacingAfter = 20f;
            document.Add(companyDetails);

            // Invoice Title
            Paragraph title = new Paragraph("INVOICE", titleFont)
            {
                Alignment = Element.ALIGN_CENTER
            };
            title.SpacingAfter = 20f;
            document.Add(title);

            // Order Information
            Paragraph orderInfo = new Paragraph($"Order ID: {orderID}\nDate: {DateTime.Now.ToString("dd/MM/yyyy")}", textFont);
            orderInfo.Alignment = Element.ALIGN_RIGHT;
            orderInfo.SpacingAfter = 20f;
            document.Add(orderInfo);

            document.Add(new Paragraph("------------------------------------------------------------------------------------------", subTitleFont));

            // Table with Items
            PdfPTable table = new PdfPTable(5);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 40f, 20f, 10f, 15f, 15f });

            // Table Header
            PdfPCell cell = new PdfPCell(new Phrase("Item Name", tableHeaderFont)) { BackgroundColor = BaseColor.DARK_GRAY, Padding = 5 };
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Item Type", tableHeaderFont)) { BackgroundColor = BaseColor.DARK_GRAY, Padding = 5 };
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Quantity", tableHeaderFont)) { BackgroundColor = BaseColor.DARK_GRAY, Padding = 5 };
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Price", tableHeaderFont)) { BackgroundColor = BaseColor.DARK_GRAY, Padding = 5 };
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Total", tableHeaderFont)) { BackgroundColor = BaseColor.DARK_GRAY, Padding = 5 };
            table.AddCell(cell);

            // Table Data
            foreach (DataRow row in cartTable.Rows)
            {
                table.AddCell(new PdfPCell(new Phrase(row["ItemName"].ToString(), tableCellFont)) { Padding = 5 });
                table.AddCell(new PdfPCell(new Phrase(row["ItemType"].ToString(), tableCellFont)) { Padding = 5 });
                table.AddCell(new PdfPCell(new Phrase(row["Quantity"].ToString(), tableCellFont)) { Padding = 5 });
                table.AddCell(new PdfPCell(new Phrase(Convert.ToDecimal(row["Price"]).ToString("C"), tableCellFont)) { Padding = 5 });
                table.AddCell(new PdfPCell(new Phrase(Convert.ToDecimal(row["Total"]).ToString("C"), tableCellFont)) { Padding = 5 });
            }

            document.Add(table);

            document.Add(new Paragraph("------------------------------------------------------------------------------------------", subTitleFont));

            // Total Amount
            Paragraph totalAmount = new Paragraph($"Total Amount: {CalculateTotalAmount().ToString("C")}", titleFont);
            totalAmount.Alignment = Element.ALIGN_RIGHT;
            totalAmount.SpacingBefore = 10f;
            totalAmount.SpacingAfter = 20f;
            document.Add(totalAmount);

            // Footer
            Paragraph footer = new Paragraph("Thank you for your purchase!\nWe hope to see you again soon.", subTitleFont);
            footer.Alignment = Element.ALIGN_CENTER;
            footer.SpacingBefore = 30f;
            document.Add(footer);

            document.Close();
            writer.Close();

            MessageBox.Show("Bill generated as PDF!", "Success");

            System.Diagnostics.Process.Start(fileName);
        }
    }
}
