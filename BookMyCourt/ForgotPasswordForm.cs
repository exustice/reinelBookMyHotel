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

            // Connect to the database
            string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=true;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the email exists in the database
                string query = "SELECT secretQuestions FROM Users WHERE email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    // Email exists in the database, retrieve the secret question
                    string secretQuestion = result.ToString();

                    // Hide the controls and show the secret question label
                    emailLabel.Visible = false;
                    secretQuestionLabel.Visible = true;
                    secretQuestionLabel.Text = secretQuestion;
                    inputBox.Clear();

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