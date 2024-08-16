using System;
using System.Windows.Forms;

namespace AD_Coursework_01
{
    public partial class WelcomeForm : Form
    {
        private Timer progressBarTimer;
        private bool increasing = true;

        public WelcomeForm()
        {
            InitializeComponent();

            // Initialize the Timer
            progressBarTimer = new Timer();
            progressBarTimer.Interval = 1000; // Set the interval (100ms)
            progressBarTimer.Tick += ProgressBarTimer_Tick;
        }

      

        private void WelcomeForm_Load(object sender, EventArgs e)
        {
            // Start animating the ProgressBar when the form loads
            progressBar.Value = 0;
            progressBarTimer.Start();
        }
        private
void
ProgressBarTimer_Tick
(object sender, EventArgs e)
        {
            // Check if we are increasing or decreasing the ProgressBar value
            if
            (increasing)
            {
                if
                (progressBar.Value < progressBar.Maximum)
                {
                    progressBar.Value += 100;
                }
                else
                {
                    // Start decreasing when the maximum is reached
                    increasing = false;
                    System.Threading.Thread.Sleep(5);
                    // Add delay before decreasing
                }
            }
            else
            {
                if
                (progressBar.Value > 0)
                {
                    progressBar.Value -= 100;
                }
                else
                {
                    // Start increasing again when the minimum is reached
                    increasing = true;
                    System.Threading.Thread.Sleep(5);
                    // Add delay before increasing again
                }
            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            // Start animating the ProgressBar
            progressBar.Value = 0;
            progressBarTimer.Start();

            // Open the AdminLogin form
            AdminLogin adminLogin = new AdminLogin();
            adminLogin.Location = this.Location;
            adminLogin.StartPosition = FormStartPosition.CenterScreen;
            adminLogin.Show();
            this.Hide();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            // Start animating the ProgressBar
            progressBar.Value = 0;
            progressBarTimer.Start();

            // Open the CustomerLogin form
            CustomerLogin customerLogin = new CustomerLogin();
            customerLogin.Location = this.Location;
            customerLogin.StartPosition = FormStartPosition.CenterScreen;
            customerLogin.Show();
            this.Hide();
        }
    }
}
