using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AD_Coursework_01
{
    public partial class CarPartsForm : UserControl
    {
        public CarPartsForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadAllCarParts();
            tblCarParts.CellClick += tblCarParts_CellClick;
        }

        private void LoadAllCarParts()
        {
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM CarPart", con))
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
                    tblCarParts.AutoGenerateColumns = true;
                    tblCarParts.DataSource = dt;
                    tblCarParts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    tblCarParts.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO CarPart (PartId, Name, UnitPrice, QtyOnHand) VALUES (@PartId, @Name, @UnitPrice, @QtyOnHand)", con))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@PartId", txtId.Text);
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@UnitPrice", txtUnitPrice.Text);
                        cmd.Parameters.AddWithValue("@QtyOnHand", txtQtyOnHand.Text);

                        cmd.ExecuteNonQuery();
                    }
                }
                LoadAllCarParts();

                MessageBox.Show("Car saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving car: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;

            try
            {
                // Confirm if the user wants to delete the car
                if (MessageBox.Show("Are you sure you want to delete this car part?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM CarPart WHERE PartId = @PartId", con))
                        {
                            con.Open();
                            cmd.Parameters.AddWithValue("@PartId", txtId.Text); // Assuming PartId is stored in txtCarId
                            cmd.ExecuteNonQuery();
                        }
                    }
                    LoadAllCarParts(); // Reload the data in the DataGridView
                    MessageBox.Show("Car deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting car: " + ex.Message);
            }
        }

        private void tblCarParts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked row index is validif (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow row = tblCarParts.Rows[e.RowIndex];

                // Populate text fields with data from the selected row
                txtId.Text = row.Cells["PartId"].Value.ToString().Trim();
                txtName.Text = row.Cells["Name"].Value.ToString().Trim();
                txtUnitPrice.Text = row.Cells["UnitPrice"].Value.ToString().Trim();
                txtQtyOnHand.Text = row.Cells["QtyOnHand"].Value.ToString().Trim();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE CarPart SET Name = @Name, UnitPrice = @UnitPrice, QtyOnHand = @QtyOnHand WHERE PartId = @PartId", con))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@PartId", txtId.Text);
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@UnitPrice", txtUnitPrice.Text);
                        cmd.Parameters.AddWithValue("@QtyOnHand", txtQtyOnHand.Text);
                        
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadAllCarParts(); // Reload the data in the DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating car: " + ex.Message);
            }
        }
    }
}
