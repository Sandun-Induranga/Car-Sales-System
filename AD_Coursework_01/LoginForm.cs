using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

namespace AD_Coursework_01
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Connection string to your SQL Server database
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;

            // SQL query to check if the provided credentials are valid
            string query = "SELECT COUNT(1) FROM [User] WHERE Username = @Username AND Password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", txtEmail.Text); // Assuming txtEmail.Text holds the username
                command.Parameters.AddWithValue("@Password", HashPassword(txtPassword.Text)); // Hash the password before checking

                try
                {
                    connection.Open();
                    int result = (int)command.ExecuteScalar();

                    if (result > 0)
                    {
                        // Login successful
                        MessageBox.Show("Login successful!");
                        CustomerMenuBar customerDashboard = new CustomerMenuBar();
                        // Load Dashboard or transition to the next form
                        customerDashboard.Show();
                        this.Hide();
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

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            // Transition to the Sign Up form
            RegistrationForm registrationForm = new RegistrationForm();
            registrationForm.Show();
            this.Hide();
        }

        // Function to hash the password using SHA-256
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
