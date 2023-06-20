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
    public partial class BookingListControl : UserControl
    {
        public BookingListControl()
        {
            InitializeComponent();
            PopulateBookingDataGridView();

            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.HeaderText = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(deleteButtonColumn);

            DataGridViewButtonColumn proceedButtonColumn = new DataGridViewButtonColumn();
            proceedButtonColumn.HeaderText = "Proceed";
            proceedButtonColumn.Text = "Proceed";
            proceedButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(proceedButtonColumn);
        }

        private void PopulateBookingDataGridView()
        {
            string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=True";
            string query = "SELECT ReservationID, Name, Contact#, CONVERT(VARCHAR(20), StartTime, 108) + ' - ' + CONVERT(VARCHAR(20), EndTime, 108)" +
                " AS Time, CONVERT(VARCHAR(10), Date, 101) + ' - ' + CONVERT(VARCHAR(10), EndDate, 101) AS Date, RoomType, BedType, Price FROM Bookings WHERE Status = 'Not Completed'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string columnHeaderText = dataGridView1.Columns[e.ColumnIndex].HeaderText;

                if (columnHeaderText == "Delete")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this reservation?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string reservationID = dataGridView1.Rows[e.RowIndex].Cells["ReservationID"].Value.ToString();
                        DeleteReservation(reservationID);
                        LoadData();
                    }
                }
                else if (columnHeaderText == "Proceed")
                {
                    string reservationID = dataGridView1.Rows[e.RowIndex].Cells["ReservationID"].Value.ToString();
                    DisplayReservationDetails(reservationID);
                    LoadData();

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

        private void LoadData()
        {
            string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=True";
            string query = "SELECT ReservationID, Name, Contact#, CONVERT(VARCHAR(20), StartTime, 108) + ' - ' + CONVERT(VARCHAR(20), EndTime, 108)" +
                " AS Time, CONVERT(VARCHAR(10), Date, 101) + ' - ' + CONVERT(VARCHAR(10), EndDate, 101) AS Date, RoomType, BedType, Price FROM Bookings WHERE Status = 'Not Completed'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void DisplayReservationDetails(string reservationID)
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
            ReservationDetailsForm detailsForm = new ReservationDetailsForm();
            detailsForm.PopulateReservationDetails(reservationID, name, contactNumber, startTime, endTime, date, endDate, roomType, bedType, price);
            detailsForm.ShowDialog();
           
           
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
