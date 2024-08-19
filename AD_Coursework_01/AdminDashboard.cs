using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AD_Coursework_01
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void AdminDashboard_Load(object sender, EventArgs e)

        {

            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;

            DataTable dt = new DataTable();

            try

            {

                using (SqlConnection con = new SqlConnection(connectionString))

                {

                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Admin", con))

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

                    dataGridView1.AutoGenerateColumns = true;

                    dataGridView1.DataSource = dt;

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    dataGridView1.Refresh();

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show("Error loading data: " + ex.Message);

            }

        }


    }
}
