using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AD_Coursework_01
{
    public partial class PurchaseCarsForm : UserControl
    {
        public PurchaseCarsForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadAllCars();
            tblCars.CellClick += tblCars_CellClick;
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
            // Check if the clicked row index is validif (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow row = tblCars.Rows[e.RowIndex];

                // Populate text fields with data from the selected row
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
                    using (SqlCommand cmd = new SqlCommand("UPDATE Car SET brand = @brand, model = @model, year = @year, color = @color, price = @price, qtyOnHand = @qtyOnHand WHERE CarId = @CarId", con))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@CarId", txtId.Text); // Assuming CarId is stored in txtCarId
                        cmd.Parameters.AddWithValue("@brand", txtBrand.Text);
                        cmd.Parameters.AddWithValue("@model", txtModel.Text);
                        cmd.Parameters.AddWithValue("@year", txtYear.Text);
                        cmd.Parameters.AddWithValue("@color", txtColor.Text);
                        cmd.Parameters.AddWithValue("@price", txtPrice.Text);
                        cmd.Parameters.AddWithValue("@qtyOnHand", txtQtyOnHand.Text);

                        cmd.ExecuteNonQuery();
                    }
                }
                LoadAllCars(); // Reload the data in the DataGridView
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
                // Confirm if the user wants to delete the car
                if (MessageBox.Show("Are you sure you want to delete this car?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Car WHERE CarId = @CarId", con))
                        {
                            con.Open();
                            cmd.Parameters.AddWithValue("@CarId", txtId.Text); // Assuming CarId is stored in txtCarId
                            cmd.ExecuteNonQuery();
                        }
                    }
                    LoadAllCars(); // Reload the data in the DataGridView
                    MessageBox.Show("Car deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting car: " + ex.Message);
            }
        }
    }
}
