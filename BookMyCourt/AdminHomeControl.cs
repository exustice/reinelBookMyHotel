using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BookMyCourt
{
    public partial class AdminHomeControl : UserControl
    {

        private NotificationControl notificationControl;

        public AdminHomeControl()
        {
            InitializeComponent();
            LoadCompletedBookings();
           
            DataGridViewButtonColumn processButtonColumn = new DataGridViewButtonColumn();
            processButtonColumn.Name = "Process";
            processButtonColumn.HeaderText = "Process";
            processButtonColumn.Text = "Process";
            processButtonColumn.UseColumnTextForButtonValue = true;

            // Create the DataGridViewButtonColumn for the Delete button
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.HeaderText = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true;

            // Add the button columns to the DataGridView
           
            dataGridViewCompletedBookings.Columns.Add(deleteButtonColumn);
            dataGridViewCompletedBookings.Columns.Add(processButtonColumn);

        }

        private void LoadCompletedBookings()
        {
         
            string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=True";
            string query = "SELECT B.ReservationID, B.Name, B.Contact#, CONVERT(VARCHAR(20), B.StartTime, 108) + ' - ' + CONVERT(VARCHAR(20), B.EndTime, 108)" +
                " AS Time, CONVERT(VARCHAR(10), B.Date, 101) + ' - ' + CONVERT(VARCHAR(10), B.EndDate, 101) AS Date, B.RoomType, B.BedType, B.Price" +
                " FROM Bookings AS B" +
                " WHERE B.Status = 'Complete'";



            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {


                        connection.Open();


                        // Create a SqlDataAdapter to retrieve the data
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();

                        // Fill the DataTable with the retrieved data
                        adapter.Fill(dataTable);


                        // Create the DataGridViewButtonColumn for the Process button
               

                  
           
                        // Add the DataGridViewComboBoxColumn to the DataGridView
                   

                        // Bind the DataTable to the DataGridView
                        dataGridViewCompletedBookings.DataSource = dataTable;

                        // Optional: Customize the column headers or formatting if needed
                        dataGridViewCompletedBookings.Columns["ReservationID"].HeaderText = "Reservation ID";
                        // Add more column customization as needed

                        // Refresh the DataGridView to update the view
                        dataGridViewCompletedBookings.Refresh();
                    }
                    catch (Exception ex)
                    {
                        // Handle any potential errors
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }
        private void dataGridViewCompletedBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string columnHeaderText = dataGridViewCompletedBookings.Columns[e.ColumnIndex].HeaderText;

                if (columnHeaderText == "Delete")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this reservation?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string reservationID = dataGridViewCompletedBookings.Rows[e.RowIndex].Cells["ReservationID"].Value.ToString();
                        DeleteReservation(reservationID);
                       
                    }
                }
                else if (columnHeaderText == "Process") // Update the condition to check for "Process" instead of "Proceed"
                {
                    


                    // Get the reservation ID from the DataGridView
                  
                    string reservationID = dataGridViewCompletedBookings.Rows[e.RowIndex].Cells["ReservationID"].Value.ToString();
                    ShowReservationDetailsForm(reservationID);
                    LoadCompletedBookings();


                }
            }
        }
        private DataTable GetAvailableRoomNumbers(SqlConnection connection)
        {
            string query = "SELECT R.RoomNumber FROM RoomInfo AS R INNER JOIN Bookings AS B ON B.RoomType = R.RoomType AND B.BedType = R.BedType GROUP BY R.RoomNumber";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                
                
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }



        private void DeleteReservation(string reservationID)
        {
            string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=True";
            string query = "DELETE FROM Bookings WHERE ReservationID = @ReservationID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReservationID", reservationID);

                    try
                    {
                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Reservation deleted successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete reservation.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }


        private void ShowReservationDetailsForm(string reservationID)
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
           






            ShowReservationDetailsForm showdetailsForm = new ShowReservationDetailsForm(notificationControl);
            showdetailsForm.ShowPopulateReservationDetails(reservationID, name, contactNumber, startTime, endTime, date, endDate, roomType, bedType, price );
            showdetailsForm.ShowDialog();
            dataGridViewCompletedBookings.Refresh();
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

        private string GetReservationIDFromDataGridView(string roomNumber)
        {
            string reservationID = "";

            // Search for the room number in the DataGridView
            foreach (DataGridViewRow row in dataGridViewCompletedBookings.Rows)
            {
                string roomNumberCellValue = row.Cells["RoomNumber"].Value.ToString();

                if (roomNumberCellValue == roomNumber)
                {
                    // Room number found, retrieve the corresponding reservation ID
                    reservationID = row.Cells["ReservationID"].Value.ToString();
                    break;
                }
            }

            return reservationID;
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


    

