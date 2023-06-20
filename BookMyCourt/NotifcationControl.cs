using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BookMyCourt
{
    public partial class NotificationControl : UserControl
    {
        private Bitmap qrCodeImage;


        public NotificationControl()
        {
            InitializeComponent();

        }

        private void NotificationControl_Load(object sender, EventArgs e)
        {
            LoadFinishedBookings();
        }

      

      

        private void LoadFinishedBookings()
        {
            string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=True";
            string query = "SELECT ReservationID, Name ,Price FROM Bookings WHERE Status = 'Finished'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string reservationID = reader["ReservationID"].ToString();
                        string name = reader["Name"].ToString();    
                        decimal price = Convert.ToDecimal(reader["Price"]);


                        CreateNotificationItem(reservationID, name, price, qrCodeImage);
                    }

                    reader.Close();
                }
            }
        }

        private void CreateNotificationItem(string reservationID, string name, decimal price, Bitmap qrCodeImage)
        {
            Panel notificationPanel = new Panel();
            notificationPanel.BackColor = Color.White;
            notificationPanel.BorderStyle = BorderStyle.FixedSingle;
            notificationPanel.Size = new Size(200, 150);

            Label reservationIDLabel = new Label();
            reservationIDLabel.Text =  reservationID;
            reservationIDLabel.Location = new Point(10, 10);

            Label nameLabel = new Label();
            nameLabel.Text =  name;
            nameLabel.Location = new Point(10, 40);

            Label priceLabel = new Label();
            priceLabel.Text = "Php" + price.ToString();
            priceLabel.Location = new Point(10, 70);


            Button deleteButton = new Button();
            deleteButton.Text = "delete";
            deleteButton.Location = new Point(5, 100);
            deleteButton.Click += (sender, e) => DeleteNotification(notificationPanel,reservationID); // Call the DeleteNotification method when clicked

            Button checkButton = new Button();
            checkButton.Text = "check";
            checkButton.Location = new Point(120, 100);
            checkButton.Click += (sender, e) => showNotificationdetailsForms(reservationID); // Call the ShowReservationDetails method when clicked

            notificationPanel.Controls.Add(reservationIDLabel);
            notificationPanel.Controls.Add(nameLabel);
            notificationPanel.Controls.Add(priceLabel);
            notificationPanel.Controls.Add(deleteButton);
            notificationPanel.Controls.Add(checkButton);
            // Add the notification panel to the flow layout panel
            flowLayoutPanelNotifications.Controls.Add(notificationPanel);
        }

        private void DeleteNotification(Panel notificationPanel, string reservationID)
        {

            // Update the status of the booking to "Not Completed"
            string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Bookings WHERE ReservationID = @ReservationID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReservationID", reservationID);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            // Remove the notification panel from the flow layout panel
            flowLayoutPanelNotifications.Controls.Remove(notificationPanel);


        }

        private void showNotificationdetailsForms(string reservationID)
        {
            string name = GetReservationName(reservationID);
            string contactNumber = GetReservationContactNumber(reservationID);
            DateTime startTime = GetReservationStartTime(reservationID);
            DateTime endTime = GetReservationEndTime(reservationID);
            DateTime date = GetReservationDate(reservationID);
            DateTime endDate = GetReservationEndDate(reservationID);
            string roomType = GetReservationRoomType(reservationID);
            string bedType = GetReservationBedType(reservationID);
            decimal price = GetReservationPrice(reservationID);

            NotificationdetailsForms showdnotifdetailsForm = new NotificationdetailsForms();
            showdnotifdetailsForm.showNotificationdetailsForms(reservationID, name, contactNumber, startTime, endTime, date, endDate, roomType, bedType, price);
            showdnotifdetailsForm.ShowDialog();
            

        }

        private string GetReservationName(string reservationID)
        {
            string name = string.Empty;

            string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Name FROM Bookings WHERE ReservationID = @ReservationID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReservationID", reservationID);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        name = result.ToString();
                    }
                }
            }

            return name;
        }

        private string GetReservationContactNumber(string reservationID)
        {
            string contactNumber = string.Empty;

            string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT [Contact#] FROM Bookings WHERE ReservationID = @ReservationID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReservationID", reservationID);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        contactNumber = result.ToString();
                    }
                }
            }

            return contactNumber;
        }

        private DateTime GetReservationStartTime(string reservationID)
        {
            DateTime startTime = DateTime.MinValue;

            string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT StartTime FROM Bookings WHERE ReservationID = @ReservationID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReservationID", reservationID);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && DateTime.TryParse(result.ToString(), out DateTime startTimeValue))
                    {
                        startTime = startTimeValue;
                    }
                }
            }

            return startTime;
        }

        private DateTime GetReservationEndTime(string reservationID)
        {
            DateTime endTime = DateTime.MinValue;

            string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT EndTime FROM Bookings WHERE ReservationID = @ReservationID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReservationID", reservationID);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && DateTime.TryParse(result.ToString(), out DateTime endTimeValue))
                    {
                        endTime = endTimeValue;
                    }
                }
            }

            return endTime;
        }

        private DateTime GetReservationDate(string reservationID)
        {
            DateTime date = DateTime.MinValue;

            string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Date FROM Bookings WHERE ReservationID = @ReservationID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReservationID", reservationID);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && DateTime.TryParse(result.ToString(), out DateTime dateValue))
                    {
                        date = dateValue;
                    }
                }
            }

            return date;
        }
        private DateTime GetReservationEndDate(string reservationID)
        {
            DateTime endDate = DateTime.MinValue;

            string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT EndDate FROM Bookings WHERE ReservationID = @ReservationID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReservationID", reservationID);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && DateTime.TryParse(result.ToString(), out DateTime endDateValue))
                    {
                        endDate = endDateValue;
                    }
                }
            }

            return endDate;
        }

        private string GetReservationRoomType(string reservationID)
        {
            string roomType = string.Empty;

            string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT RoomType FROM Bookings WHERE ReservationID = @ReservationID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReservationID", reservationID);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        roomType = result.ToString();
                    }
                }
            }

            return roomType;
        }

        private string GetReservationBedType(string reservationID)
        {
            string bedType = string.Empty;

            string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT BedType FROM Bookings WHERE ReservationID = @ReservationID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReservationID", reservationID);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        bedType = result.ToString();
                    }
                }
            }

            return bedType;
        }

        private decimal GetReservationPrice(string reservationID)
        {
            decimal price = 0;

            string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Price FROM Bookings WHERE ReservationID = @ReservationID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReservationID", reservationID);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && decimal.TryParse(result.ToString(), out decimal priceValue))
                    {
                        price = priceValue;
                    }
                }
            }

            return price;
        }

    }

}