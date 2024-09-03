using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace AD_Coursework_01
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            // Connection string to your SQL Server database
            string connectionString = Properties.Settings.Default.abcCarTradersConnectionString;

            // Collect data from the form fields
            string username = txtUsername.Text;
            string password = HashPassword(txtPassword.Text);
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string address = txtAddress.Text;

            // Insert data into User and Customer tables
            string userQuery = "INSERT INTO [User] (Username, Password, Role) VALUES (@Username, @Password, 'Customer')";
            string customerQuery = "INSERT INTO Customer (Username, FirstName, LastName, Email, Phone, Address) VALUES (@Username, @FirstName, @LastName, @Email, @Phone, @Address)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand userCommand = new SqlCommand(userQuery, connection);
                SqlCommand customerCommand = new SqlCommand(customerQuery, connection);

                // Add parameters for User table
                userCommand.Parameters.AddWithValue("@Username", username);
                userCommand.Parameters.AddWithValue("@Password", password);

                // Add parameters for Customer table
                customerCommand.Parameters.AddWithValue("@Username", username);
                customerCommand.Parameters.AddWithValue("@FirstName", firstName);
                customerCommand.Parameters.AddWithValue("@LastName", lastName);
                customerCommand.Parameters.AddWithValue("@Email", email);
                customerCommand.Parameters.AddWithValue("@Phone", phone);
                customerCommand.Parameters.AddWithValue("@Address", address);

                try
                {
                    connection.Open();

                    // Start a transaction to ensure both inserts succeed
                    SqlTransaction transaction = connection.BeginTransaction();
                    userCommand.Transaction = transaction;
                    customerCommand.Transaction = transaction;

                    try
                    {
                        // Execute the insert commands
                        userCommand.ExecuteNonQuery();
                        customerCommand.ExecuteNonQuery();

                        // Commit the transaction
                        transaction.Commit();

                        MessageBox.Show("Registration successful!");
                        // Transition to the Login form
                        LoginForm loginForm = new LoginForm();
                        loginForm.Show();
                        this.Hide();

                    }
                    catch (Exception ex)
                    {
                        // Rollback the transaction if something goes wrong
                        transaction.Rollback();
                        MessageBox.Show($"An error occurred during registration: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Transition to the Login form
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }
    }
}
