using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BookMyCourt
{
    public partial class RegistrationForm : Form
    {
        string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=True";

        public RegistrationForm()
        {
            InitializeComponent();
            btnRegistration.Enabled = false;
        }

        public ComboBox SecretQuestionComboBox
        {
            get { return secretQuestions; }
        }
        private void btnRegistration_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string password = txtPassword.Text;
            string secretQuestion = secretQuestions.SelectedItem?.ToString();
            string secretAnswer = secretAnswers.Text;

            bool isValidEmail = Regex.IsMatch(email, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$");
            bool containsSpaces = email.Contains(" ");

            // Check if email, first name, last name, and password are not empty
            if (!isValidEmail || containsSpaces)
            {
                MessageBox.Show(txtEmail, "Invalid email address");
            }
            else if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(password))
            {
                // Email, first name, last name, or password is empty
                MessageBox.Show("Please enter your email, first name, last name, and password.");
            }
            else if (string.IsNullOrEmpty(secretQuestion) || string.IsNullOrEmpty(secretAnswer))
            {
                // Secret question or secret answer is not filled
                MessageBox.Show("Please select a secret question and provide an answer.");
            }
            else
            {
                // Connect to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if the email already exists in the database
                    string query = "SELECT COUNT(*) FROM Users WHERE email = @Email";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Email", email);

                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        // Email already exists in the database
                        MessageBox.Show("Email already exists. Please try again with a different email.");
                    }
                    else
                    {
                        // Email does not exist in the database, create a new user
                        query = "INSERT INTO Users (email, firstName, lastName, password, secretQuestions, secretAnswers, isAdmin) " +
                                "VALUES (@Email, @FirstName, @LastName, @Password, @SecretQuestions, @SecretAnswers, @IsAdmin)";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@SecretQuestions", secretQuestion);
                        command.Parameters.AddWithValue("@SecretAnswers", secretAnswer);
                        command.Parameters.AddWithValue("@IsAdmin", false);

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            // User created successfully
                            MessageBox.Show("Registration successful.");
                            this.Hide(); // Hide the registration form

                            LoginForm loginForm = new LoginForm();
                            loginForm.Show();
                        }
                        else
                        {
                            // User creation failed
                            MessageBox.Show("Registration failed. Please try again.");
                        }
                    }
                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            btnRegistration.Enabled = termsCheckBox.Checked;

            // Update the appearance of the label based on the checkbox state
            if (termsCheckBox.Checked)
            {
                // Add a strikethrough effect to the label text
                termsLabel.Font = new Font(termsLabel.Font, FontStyle.Strikeout);
                termsLabel.ForeColor = Color.Gray;
            }
            else
            {
                // Remove the strikethrough effect from the label text
                termsLabel.Font = new Font(termsLabel.Font, FontStyle.Regular);
                termsLabel.ForeColor = SystemColors.ControlText;
            }
        }

        public string SelectedSecretQuestion
        {
            get { return secretQuestions.SelectedItem.ToString(); }
        }

        public string SecretAnswer
        {
            get { return secretAnswers.Text; }
        }
    }
}