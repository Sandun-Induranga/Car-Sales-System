using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //tblCars.CellClick += tblCars_CellClick;
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
    }
}
