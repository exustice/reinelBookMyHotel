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
using System.Text.RegularExpressions;

namespace BookMyCourt
{
    public partial class LoginForm : Form
    {
        private RegistrationForm registrationForm;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            bool isValidEmail = Regex.IsMatch(email, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$");
            bool containsSpaces = email.Contains(" ");
            // Check if email and password are not empty
            if (!isValidEmail || containsSpaces)
            {
                MessageBox.Show(txtEmail, "Invalid email address");

            }

           else if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                // Connect to the database
                string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=true;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if the user exists in the database
                    string query = "SELECT COUNT(*), IsAdmin FROM Users WHERE email = @Email AND password = @Password GROUP BY IsAdmin";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            int count = reader.GetInt32(0);
                            bool isAdmin = reader.GetBoolean(1);

                            if (count > 0)
                            {
                                if (isAdmin)
                                {
                                    // User is an admin, open AdminDashboardForm
                                    MessageBox.Show("Login successful as admin.");
                                    this.Hide(); // Hide the login form
                                    AdminDashboardForm adminDashboard = new AdminDashboardForm();
                                    adminDashboard.Show();
                                }
                                else
                                {
                                    // User is not an admin, open DashboardForm
                                    MessageBox.Show("Login successful.");
                                    this.Hide(); // Hide the login form
                                    DashboardForm dashboard = new DashboardForm();
                                    dashboard.Show();
                                }
                            }


                        }
                        else
                        {
                            MessageBox.Show("Please try again!");
                        }
                    }
                }


            }
         

            else
            {
                MessageBox.Show("Please Enter your email and password.");
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Navigate to the registration form
            RegistrationForm regForm = new RegistrationForm();
            this.Hide();
            regForm.Show();
        }

        private void forgotPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (registrationForm == null)
            {
                MessageBox.Show("Registration form is not available.");
                return;
            }

            // Access the registrationForm's properties
            string secretQuestion = registrationForm.SelectedSecretQuestion;
            string secretAnswer = registrationForm.SecretAnswer;

            string email = txtEmail.Text;


            string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=true;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the email exists in the database
                string query = "SELECT secretQuestion, secretAnswer FROM Users WHERE email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string storedSecretQuestion = reader["secretQuestion"].ToString();
                    string storedSecretAnswer = reader["secretAnswer"].ToString();

                    if (secretQuestion.Equals(storedSecretQuestion) && secretAnswer.Equals(storedSecretAnswer))
                    {
                        // Secret question and answer are correct, allow password reset
                        string newPassword = GenerateRandomPassword(); // Replace this with your password reset logic

                        // Update the password in the database
                        query = "UPDATE Users SET password = @NewPassword WHERE email = @Email";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@NewPassword", newPassword);
                        command.Parameters.AddWithValue("@Email", email);

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Password reset successful. Your new password is: " + newPassword);
                        }
                        else
                        {
                            MessageBox.Show("Password reset failed. Please try again.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Incorrect secret question or answer. Please try again.");
                    }
                }
                else
                {
                    MessageBox.Show("Email not found. Please enter a valid email address.");
                }

                reader.Close();
            }
        }

        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var newPassword = new string(
                Enumerable.Repeat(chars, 10)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return newPassword;
        }

        // Event handler for the "Forgot Password" link label
        private void lnkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }


    }
}
