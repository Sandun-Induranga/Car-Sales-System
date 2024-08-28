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
            GeneratePartId(); // Generate Part ID when the form loads
            tblCarParts.CellClick += tblCarParts_CellClick;
        }

        private void GeneratePartId()
        {
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 PartId FROM CarPart ORDER BY PartId DESC", con))
                    {
                        var lastPartId = cmd.ExecuteScalar() as string;
                        if (lastPartId != null)
                        {
                            // Extract the numeric part of the Part ID and increment it
                            int newIdNumber = int.Parse(lastPartId.Substring(5)) + 1;
                            txtId.Text = "PART-" + newIdNumber.ToString("D3"); // Format with leading zeros
                        }
                        else
                        {
                            // If no records are found, start with PART-001
                            txtId.Text = "PART-001";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating Part ID: " + ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear all the text fields
            txtName.Clear();
            txtUnitPrice.Clear();
            txtQtyOnHand.Clear();

            // Generate a new Part ID
            GeneratePartId();
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

        private void tblCarParts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = tblCarParts.Rows[e.RowIndex];
                txtId.Text = row.Cells["PartId"].Value.ToString().Trim();
                txtName.Text = row.Cells["PartName"].Value.ToString().Trim();
                txtUnitPrice.Text = row.Cells["Price"].Value.ToString().Trim();
                txtQtyOnHand.Text = row.Cells["QtyOnHand"].Value.ToString().Trim();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO CarPart (PartId, Name, UnitPrice, QtyOnHand) VALUES (@PartId, @Name, @Price, @QtyOnHand)", con))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@PartId", txtId.Text);
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Price", txtUnitPrice.Text);
                        cmd.Parameters.AddWithValue("@QtyOnHand", txtQtyOnHand.Text);

                        cmd.ExecuteNonQuery();
                    }
                }
                LoadAllCarParts();

                MessageBox.Show("Car part saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving car part: " + ex.Message);
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
                MessageBox.Show("Error updating car part: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;

            try
            {
                if (MessageBox.Show("Are you sure you want to delete this car part?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM CarPart WHERE PartId = @PartId", con))
                        {
                            con.Open();
                            cmd.Parameters.AddWithValue("@PartId", txtId.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    LoadAllCarParts(); // Reload the data in the DataGridView
                    MessageBox.Show("Car part deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting car part: " + ex.Message);
            }
        }
    }
}
