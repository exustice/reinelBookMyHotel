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

namespace BookMyCourt
{
    public partial class ForgotPasswordForm : Form
    {
        private RegistrationForm registrationForm;

        public ForgotPasswordForm(RegistrationForm registrationForm)
        {
            InitializeComponent();
            this.registrationForm = registrationForm;
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            string email = inputBox.Text.Trim();
            string secretAnswer = secretAnswerBox.Text.Trim();

            // Check if the input box is empty
            if (string.IsNullOrEmpty(secretAnswer))
            {
                MessageBox.Show("Please enter the secret answer.");
                return;
            }

            // Connect to the database
            string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=true;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the email and secret answer are correct
                string query = "SELECT secretAnswer FROM Users WHERE email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    string storedSecretAnswer = result.ToString();

                    if (storedSecretAnswer.Equals(secretAnswer))
                    {
                        // Secret answer is correct, allow password reset
                        MessageBox.Show("Secret answer is correct. You can now reset your password.");

                        // Display password reset controls (assuming you have appropriate controls on your form)
                        secretQuestionLabel.Visible = false;
                        secretAnswerBox.Visible = false;
                        newPasswordlbl.Visible = true;
                        newPasswordBox.Visible = true;
                        confirmPasswordlbl.Visible = true;
                        confirmPasswordBox.Visible = true;
                        resetPasswordButton.Visible = true;
                    }
                    else
                    {
                        // Secret answer is incorrect
                        MessageBox.Show("Secret answer is incorrect. Please try again.");
                    }
                }
                else
                {
                    // Email does not exist in the database
                    MessageBox.Show("Email not found. Please enter a valid email address.");
                }
            }
        }
    }
}