using System.Data.SqlClient;
using System.Windows.Forms;
using System;
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


        private void submitBtn_Click_1(object sender, EventArgs e)
        {
            string email = txtBox.Text.Trim();

            // Connect to the database
            string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=true;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the email exists in the database
                string query = "SELECT secretQuestions, secretAnswers, Password FROM Users WHERE email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string secretQuestion = reader["secretQuestions"].ToString();
                        string storedSecretAnswer = reader["secretAnswers"].ToString();
                        string currentPassword = reader["Password"].ToString();

                        label1.Text = secretQuestion;
                        txtBox.Visible = false;
                        secretAnswerTextBox.Visible = true;

                        // Compare the entered secret answer with the stored secret answer when the user clicks the submit button
                        submitBtn.Click += (s, evt) =>
                        {
                            string enteredSecretAnswer = secretAnswerTextBox.Text.Trim();

                            if (storedSecretAnswer.Equals(enteredSecretAnswer))
                            {
                                // Secret answer is correct, allow password reset
                                MessageBox.Show("Secret answer is correct. You can now reset your password.");
                                label1.Visible = false;
                                secretAnswerTextBox.Visible = false;
                                submitBtn.Visible = false;
                                // Display password reset controls (assuming you have appropriate controls on your form)
                                newPasswordLabel.Visible = true;
                                newPasswordTextBox.Visible = true;
                                confirmPasswordLabel.Visible = true;
                                confirmPasswordTextBox.Visible = true;
                                resetPasswordButton.Visible = true;
                            }
                            else
                            {
                                // Secret answer is incorrect
                                MessageBox.Show("Secret answer is incorrect. Please try again.");
                            }
                        };
                    }
                    else
                    {
                        // Email does not exist in the database
                        MessageBox.Show("Email not found in the database. Please enter a valid email address.");
                    }
                }
            }
        }

        private void resetPasswordButton_Click(object sender, EventArgs e)
        {
            string email = txtBox.Text.Trim();
            string newPassword = newPasswordTextBox.Text.Trim();
            string confirmedPassword = confirmPasswordTextBox.Text.Trim();

            if (string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Please enter a new password.");
                return; // Exit the event handler without updating the password
            }

            if (newPassword.Equals(confirmedPassword))
            {
                // Connect to the database
                string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=true;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Update the password in the database
                    string query = "UPDATE Users SET Password = @Password WHERE email = @Email";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Password", newPassword);
                    command.Parameters.AddWithValue("@Email", email);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Password updated successfully
                        MessageBox.Show("Password updated successfully.");
                        this.Close(); // Close the forgot password form
                        LoginForm loginForm = new LoginForm();
                        loginForm.Show();
                    }
                    else
                    {
                        // Password update failed
                        MessageBox.Show("Failed to update the password. Please try again.");
                    }
                }
            }
            else
            {
                // New password and confirmed password do not match
                MessageBox.Show("New password and confirmed password do not match. Please try again.");
                return;
            }
        }

    }
}