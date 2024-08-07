using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AD_Coursework_01
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Connection string to your SQL Server database
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;

            // SQL query to check if the provided credentials are valid
            string query = "SELECT COUNT(1) FROM Admin WHERE email = @email AND password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", txtEmail.Text);
                command.Parameters.AddWithValue("@Password", txtPassword.Text); // Consider using hashed passwords in real applications

                try
                {
                    connection.Open();
                    int result = (int)command.ExecuteScalar();

                    if (result > 0)
                    {
                        AdminDashboard adminDashboard = new AdminDashboard();
                        adminDashboard.Show();
                        // Login successful
                        MessageBox.Show("Login successful!");
                        // Proceed to the next form or functionality
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
