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
    public partial class ReservationDetailsForm : Form


    {
        

        public ReservationDetailsForm()
        {
            InitializeComponent();

           
        }

        public void PopulateReservationDetails(string reservationID, string name, string contactNumber, DateTime startTime, DateTime endTime, DateTime date, DateTime endDate, string roomType, string bedType, decimal price)
        {
            // Set the values of the labels or text boxes to display the reservation details
            labelReservationID.Text = reservationID;
            labelName.Text = name;
            labelContactNumber.Text = contactNumber;
            labelStartTime.Text = startTime.ToString("hh:mm tt");
            labelEndTime.Text = endTime.ToString("hh:mm tt");
            labelDate.Text = date.ToShortDateString();
            labelEndDate.Text = endDate.ToShortDateString();
            labelRoomType.Text = roomType;
            labelBedType.Text = bedType;
            labelPrice.Text = price.ToString();
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void btnComplete_Click_1(object sender, EventArgs e)
        {

            
                string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=True";
                string query = "UPDATE Bookings SET Status = 'Complete' WHERE ReservationID = @ReservationID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ReservationID", labelReservationID.Text);

                        try
                        {
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Reservation marked as complete.");
                                this.Close();
                            
                        }
                        else
                        {
                            MessageBox.Show("Failed to update reservation status.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            this.Close();

        }
    }
}
