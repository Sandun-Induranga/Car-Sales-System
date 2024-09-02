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

            // SQL query to check the credentials and retrieve the user role and CustomerID
            string query = "SELECT Role, (SELECT CustomerID FROM Customer WHERE Username = @Username) AS CustomerID FROM [User] WHERE Username = @Username AND Password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", txtUsername.Text); // Assuming txtEmail.Text holds the username
                command.Parameters.AddWithValue("@Password", HashPassword(txtPassword.Text)); // Hash the password before checking

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string role = reader["Role"].ToString();
                            string customerId = reader["CustomerID"].ToString(); // Null if the user is not a customer

                            if (role != null)
                            {
                                // Login successful, check role
                                if (role == "Admin")
                                {
                                    // Redirect to Admin form
                                    AdminMenuBar adminMenuBar = new AdminMenuBar(txtUsername.Text);
                                    adminMenuBar.Show();
                                }
                                else if (role == "Customer")
                                {
                                    // Redirect to Customer form and pass the CustomerID
                                    CustomerMenuBar customerDashboard = new CustomerMenuBar(customerId, txtUsername.Text); // Pass CustomerID
                                    customerDashboard.Show();
                                }

                                this.Hide(); // Hide the login form
                            }
                        }
                        else
                        {
                            // Login failed
                            MessageBox.Show("Invalid username or password.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

            private void BtnSignUp_Click(object sender, EventArgs e)
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
