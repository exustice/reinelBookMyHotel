using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QRCoder;

namespace BookMyCourt
{
    public partial class ShowReservationDetailsForm : Form
    {
        private NotificationControl notificationControl; // Add this field
      

        public ShowReservationDetailsForm(NotificationControl notificationControl) // Modify the constructor
        {
            InitializeComponent();
            this.notificationControl = notificationControl; // Assign the passed NotificationControl instance
            
             

        }

        // Rest of the code...

        private void ShowReservationDetailsForm_Load(object sender, EventArgs e)
        {
            GenerateAndDisplayQRCode();
           



        }

      

        private void GenerateAndDisplayQRCode()
        {
            // Combine the reservation details into a single string
            string reservationDetails = $"{labelReservationID.Text},{labelName.Text},{labelContactNumber.Text},{labelStartTime.Text},{labelEndTime.Text},{labelDate.Text},{labelEndDate.Text},{labelRoomType.Text},{labelBedType.Text},{labelPrice.Text}";

            // Create a QR code generator
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(reservationDetails, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            // Convert the QR code to an image
            Bitmap qrCodeImage = qrCode.GetGraphic(10); // Set the size of the QR code image

            // Display the QR code in the PictureBox control
            pictureBoxQRCode.Image = qrCodeImage;

           

        }
        
    
        
        public void ShowPopulateReservationDetails(string reservationID, string name, string contactNumber, DateTime startTime, DateTime endTime, DateTime date, DateTime endDate, string roomType, string bedType, decimal price)
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

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }



        private void btnComplete_Click(object sender, EventArgs e)
        {
            


            string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=True";
            string query = "UPDATE Bookings SET Status = 'Finished' WHERE ReservationID = @ReservationID";

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
                            ShowReservationDetailsForm reservationDetailsForm = new ShowReservationDetailsForm(notificationControl);
                         
                            this.Close();
                            reservationDetailsForm.Hide();
                            

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
