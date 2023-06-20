using QRCoder;
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
    public partial class NotificationdetailsForms : Form
  
    {
       
        
      

        public NotificationdetailsForms()
        {
            InitializeComponent();
  
            
        }

     

        private void NotificationdetailsFormsForm_Load(object sender, EventArgs e)
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

        public void showNotificationdetailsForms(string reservationID, string name, string contactNumber, DateTime startTime, DateTime endTime, DateTime date, DateTime endDate, string roomType, string bedType, decimal price)
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

   
     
        private void btnComplete_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
