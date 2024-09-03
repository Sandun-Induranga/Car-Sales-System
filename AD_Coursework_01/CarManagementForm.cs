using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AD_Coursework_01
{
    public partial class CarManagementForm : UserControl
    {
        public CarManagementForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e); // Call the base class's OnLoad method
            LoadAllCars(); // Load all cars when the form loads
            GenerateCarId(); // Generate Car ID when the form loads
            tblCars.CellClick += tblCars_CellClick; // Handle cell click event
        }

        private void GenerateCarId()
        {
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 carId FROM Car ORDER BY carId DESC", con)) // Get the last Car ID
                    {
                        var lastCarId = cmd.ExecuteScalar() as string;
                        if (lastCarId != null)
                        {
                            // Extract the numeric part of the Car ID and increment it
                            int newIdNumber = int.Parse(lastCarId.Substring(4)) + 1;
                            txtId.Text = "CAR-" + newIdNumber.ToString("D3"); // Format with leading zeros
                        }
                        else
                        {
                            // If no records are found, start with CAR-001
                            txtId.Text = "CAR-001";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating Car ID: " + ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear all the text fields
            txtBrand.Clear();
            txtModel.Clear();
            txtYear.Clear();
            txtColor.Clear();
            txtPrice.Clear();
            txtQtyOnHand.Clear();

            // Generate a new Car ID
            GenerateCarId();
        }

        private void LoadAllCars()
        {
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Car", con))
                    {
                        con.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }

                // Check if there are any rows in the DataTable
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No data found.");
                }
                else
                {
                    tblCars.AutoGenerateColumns = true;
                    tblCars.DataSource = dt;
                    tblCars.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    tblCars.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void tblCars_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = tblCars.Rows[e.RowIndex];
                txtId.Text = row.Cells["carId"].Value.ToString().Trim();
                txtBrand.Text = row.Cells["brand"].Value.ToString().Trim();
                txtModel.Text = row.Cells["model"].Value.ToString().Trim();
                txtYear.Text = row.Cells["year"].Value.ToString().Trim();
                txtColor.Text = row.Cells["color"].Value.ToString().Trim();
                txtPrice.Text = row.Cells["price"].Value.ToString().Trim();
                txtQtyOnHand.Text = row.Cells["qtyOnHand"].Value.ToString().Trim();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Car (carId, brand, model, year, color, price, qtyOnHand) VALUES (@carId, @brand, @model, @year, @color, @price, @qtyOnHand)", con))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@carId", txtId.Text);
                        cmd.Parameters.AddWithValue("@brand", txtBrand.Text);
                        cmd.Parameters.AddWithValue("@model", txtModel.Text);
                        cmd.Parameters.AddWithValue("@year", txtYear.Text);
                        cmd.Parameters.AddWithValue("@color", txtColor.Text);
                        cmd.Parameters.AddWithValue("@price", txtPrice.Text);
                        cmd.Parameters.AddWithValue("@qtyOnHand", txtQtyOnHand.Text);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Car saved successfully.");
                LoadAllCars();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving car: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Car SET brand = @brand, model = @model, year = @year, color = @color, price = @price, qtyOnHand = @qtyOnHand WHERE carId = @carId", con))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@carId", txtId.Text);
                        cmd.Parameters.AddWithValue("@brand", txtBrand.Text);
                        cmd.Parameters.AddWithValue("@model", txtModel.Text);
                        cmd.Parameters.AddWithValue("@year", txtYear.Text);
                        cmd.Parameters.AddWithValue("@color", txtColor.Text);
                        cmd.Parameters.AddWithValue("@price", txtPrice.Text);
                        cmd.Parameters.AddWithValue("@qtyOnHand", txtQtyOnHand.Text);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Car updated successfully.");
                LoadAllCars();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating car: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;

            try
            {
                if (MessageBox.Show("Are you sure you want to delete this car?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Car WHERE carId = @carId", con))
                        {
                            con.Open();
                            cmd.Parameters.AddWithValue("@carId", txtId.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Car deleted successfully.");
                    LoadAllCars();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting car: " + ex.Message);
            }
        }
    }
}
