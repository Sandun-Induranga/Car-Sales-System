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
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.AdminDashboard_Load);
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
                dataGridView1.DataSource = dt;
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No data found.");
                }
                this.Load += new System.EventHandler(this.AdminDashboard_Load);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }
    }
}
