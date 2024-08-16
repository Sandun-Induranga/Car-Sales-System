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
    public partial class CustomerLogin : Form
    {
        public CustomerLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Connection string to your SQL Server database
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;

            // SQL query to check if the provided credentials are valid
            string query = "SELECT COUNT(1) FROM Customer WHERE email = @email AND password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", txtEmail.Text);
                command.Parameters.AddWithValue("@Password", txtPassword.Text);

                try
                {
                    connection.Open();
                    int result = (int)command.ExecuteScalar();

                    if (result > 0)
                    {
                        // Login successful
                        MessageBox.Show("Login successful!");
                        CustomerMenuBar customerDashboard = new CustomerMenuBar();
                        customerDashboard.Show();
                    }
                    else
                    {
                        // Login failed
                        MessageBox.Show("Invalid username or password.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
