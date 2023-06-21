using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookMyCourt
{
    public partial class HomePageControl : UserControl
    {

        private Button btnShowDetails;
        private string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=True";
        public HomePageControl()
        {
            InitializeComponent();
            LoadHotels();

        }


        private Image ConvertByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }


        private void btnShowDetails_Click(object sender, EventArgs e)
        {


            btnShowDetails = (Button)sender;
            // Get the clicked button
            string hotelName = btnShowDetails.Tag.ToString(); // Retrieve the hotel name from the button's tag

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT HotelAddress, HotelStar, HotelOffers, HotelImage FROM Hotels WHERE HotelName = @HotelName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@HotelName", hotelName);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string hotelAddress = reader.GetString(0);
                            int hotelStar = reader.GetInt32(1);
                            string hotelOffers = reader.GetString(2);
                            byte[] hotelImageBytes = (byte[])reader.GetValue(3);
                            Image hotelImage = ConvertByteArrayToImage(hotelImageBytes);

                            //create new form upon clicking show details
                            Form hotelDetailsForm = new Form();
                            hotelDetailsForm.Text = hotelName + " Details";
                            hotelDetailsForm.AutoSize = true; // Set AutoSize property to true
                            hotelDetailsForm.AutoSizeMode = AutoSizeMode.GrowAndShrink; // Set AutoSizeMode property to GrowAndShrink
                            hotelDetailsForm.StartPosition = FormStartPosition.CenterScreen; // Set the form's position to the center of the screen
                            hotelDetailsForm.FormBorderStyle = FormBorderStyle.FixedSingle; // Set the form's border style to FixedSingle (cannot be resized)
                            hotelDetailsForm.MaximizeBox = false; // Disable maximize button



                            // Create labels to display the full details of the hotel
                            PictureBox pbHotelImage = new PictureBox();
                            pbHotelImage.Image = hotelImage;
                            pbHotelImage.SizeMode = PictureBoxSizeMode.StretchImage;
                            pbHotelImage.Width = 200;
                            pbHotelImage.Height = 150;
                            pbHotelImage.Location = new Point(100, 10);

                            // Create labels to display the full details of the hotel
                            Label lblHotelNameTitle = new Label();
                            lblHotelNameTitle.Text = "HOTEL NAME";
                            lblHotelNameTitle.Location = new Point(10, 170);
                            lblHotelNameTitle.AutoSize = true;
                            lblHotelNameTitle.Font = new Font(lblHotelNameTitle.Font, FontStyle.Bold);
                            lblHotelNameTitle.TextAlign = ContentAlignment.MiddleCenter; // Align text at the center
                            hotelDetailsForm.Controls.Add(lblHotelNameTitle);

                            Label lblHotelName = new Label();
                            lblHotelName.Text = hotelName;
                            lblHotelName.Location = new Point(10, lblHotelNameTitle.Bottom + 5);
                            lblHotelName.AutoSize = true;
                            lblHotelName.Font = new Font(lblHotelName.Font.FontFamily, lblHotelName.Font.Size);
                            lblHotelName.TextAlign = ContentAlignment.MiddleCenter; // Align text at the center
                            hotelDetailsForm.Controls.Add(lblHotelName);


                            Label lblHotelAddressTitle = new Label();
                            lblHotelAddressTitle.Text = "HOTEL ADDRESS";
                            lblHotelAddressTitle.Location = new Point(10, lblHotelName.Bottom + 10);
                            lblHotelAddressTitle.AutoSize = true;
                            lblHotelAddressTitle.Font = new Font(lblHotelAddressTitle.Font, FontStyle.Bold);
                            lblHotelAddressTitle.TextAlign = ContentAlignment.MiddleCenter; // Align text at the center
                            hotelDetailsForm.Controls.Add(lblHotelAddressTitle);

                            Label lblHotelAddress = new Label();
                            lblHotelAddress.Text = hotelAddress;
                            lblHotelAddress.Location = new Point(10, lblHotelAddressTitle.Bottom + 5);
                            lblHotelAddress.AutoSize = true;
                            lblHotelAddress.TextAlign = ContentAlignment.MiddleCenter; // Align text at the center
                            hotelDetailsForm.Controls.Add(lblHotelAddress);

                            Label lblHotelStarTitle = new Label();
                            lblHotelStarTitle.Text = "HOTEL RATINGS";
                            lblHotelStarTitle.Location = new Point(10, lblHotelAddress.Bottom + 10);
                            lblHotelStarTitle.AutoSize = true;
                            lblHotelStarTitle.Font = new Font(lblHotelStarTitle.Font, FontStyle.Bold);
                            lblHotelStarTitle.TextAlign = ContentAlignment.MiddleCenter; // Align text at the center
                            hotelDetailsForm.Controls.Add(lblHotelStarTitle);

                            Label lblHotelStar = new Label();
                            lblHotelStar.Text = hotelStar.ToString() + " STARS";
                            lblHotelStar.Location = new Point(10, lblHotelStarTitle.Bottom + 5);
                            lblHotelStar.AutoSize = true;
                            lblHotelStar.TextAlign = ContentAlignment.MiddleCenter; // Align text at the center
                            hotelDetailsForm.Controls.Add(lblHotelStar);

                            Label lblHotelOffersTitle = new Label();
                            lblHotelOffersTitle.Text = "HOTEL OFFERS";
                            lblHotelOffersTitle.Location = new Point(10, lblHotelStar.Bottom + 10);
                            lblHotelOffersTitle.AutoSize = true;
                            lblHotelOffersTitle.Font = new Font(lblHotelOffersTitle.Font, FontStyle.Bold);
                            lblHotelOffersTitle.TextAlign = ContentAlignment.MiddleCenter; // Align text at the center
                            hotelDetailsForm.Controls.Add(lblHotelOffersTitle);

                            Label lblHotelOffers = new Label();
                            lblHotelOffers.Text = "- " + hotelOffers;
                            lblHotelOffers.Location = new Point(10, lblHotelOffersTitle.Bottom + 5);
                            lblHotelOffers.AutoSize = true;
                            //lblHotelOffers.TextAlign = ContentAlignment.MiddleCenter; // Align text at the center
                            hotelDetailsForm.Controls.Add(lblHotelOffers);

                            // Add the picture box and labels to the form
                            hotelDetailsForm.Controls.Add(pbHotelImage);
                            hotelDetailsForm.Controls.Add(lblHotelName);
                            hotelDetailsForm.Controls.Add(lblHotelAddress);
                            hotelDetailsForm.Controls.Add(lblHotelStar);
                            hotelDetailsForm.Controls.Add(lblHotelOffers);

                            
                            // ...

                            // Show the hotel details form
                            hotelDetailsForm.ShowDialog();
                        }
                    }
                }
            }
        }






        private void LoadHotels()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT HotelName, HotelAddress, HotelStar, HotelOffers, HotelImage FROM Hotels";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string hotelName = reader.GetString(0);
                            string hotelAddress = reader.GetString(1);
                            int hotelStar = reader.GetInt32(2);
                            string hotelOffers = reader.GetString(3);
                            byte[] hotelImageBytes = (byte[])reader.GetValue(4);
                            Image hotelImage = ConvertByteArrayToImage(hotelImageBytes);

                            // Create a panel to hold hotel details
                            Panel hotelPanel = new Panel();
                            hotelPanel.BorderStyle = BorderStyle.FixedSingle;
                            hotelPanel.Padding = new Padding(10);
                            hotelPanel.Size = new Size(745, 180);
                            hotelPanel.BackColor = Color.White;

                            int maxTextLength = 12;
                            // Create labels for hotel details
                            Label lblHotelName = new Label();
                            lblHotelName.Text = "Hotel Name: " + hotelName;
                            lblHotelName.AutoSize = true;
                            lblHotelName.Location = new Point(10, 10);
                            lblHotelName.Text = "Hotel Name: " + (hotelName.Length <= maxTextLength ? hotelName : hotelName.Substring(0, maxTextLength) + "...");
                            lblHotelName.Font = new Font(lblHotelName.Font.FontFamily, 15);

                            Label lblHotelOffers = new Label();
                            lblHotelOffers.Text = "Offers: " + hotelOffers;
                            lblHotelOffers.AutoSize = true;
                            lblHotelOffers.Location = new Point(10, 130);
                            lblHotelOffers.Font = new Font(lblHotelOffers.Font.FontFamily, 15);
                            lblHotelOffers.Text = "Hotel Offers: " + (hotelOffers.Length <= maxTextLength ? hotelOffers : hotelOffers.Substring(0, maxTextLength) + "...");


                            // Create a button to show more details
                            btnShowDetails = new Button();
                            btnShowDetails.Text = "Show Details";
                            btnShowDetails.Tag = hotelName; // Store the hotel name as the button's tag
                            btnShowDetails.Click += btnShowDetails_Click;
                            btnShowDetails.AutoSize = true;
                            btnShowDetails.Location = new Point(572, 120);
                            btnShowDetails.Font = new Font(lblHotelOffers.Font.FontFamily, 12);



                            Label lblHotelAddress = new Label();
                            lblHotelAddress.Text = "Address: " + hotelAddress;
                            lblHotelAddress.AutoSize = true;
                            lblHotelAddress.Location = new Point(10, 65);
                            lblHotelAddress.Font = new Font(lblHotelAddress.Font.FontFamily, 15);
                            lblHotelAddress.Text = "Hotel Address: " + (hotelAddress.Length <= maxTextLength ? hotelAddress : hotelAddress.Substring(0, maxTextLength) + "...");

                            Label lblHotelStar = new Label();
                            lblHotelStar.Text = "Star: " + hotelStar.ToString();
                            lblHotelStar.AutoSize = true;
                            lblHotelStar.Location = new Point(10, 90);
                            lblHotelStar.Font = new Font(lblHotelStar.Font.FontFamily, 15);


                            // Create a PictureBox for hotel image
                            PictureBox pbHotelImage = new PictureBox();
                            pbHotelImage.Image = hotelImage;
                            pbHotelImage.SizeMode = PictureBoxSizeMode.StretchImage;
                            pbHotelImage.Width = 200;
                            pbHotelImage.Height = 150;
                            pbHotelImage.Location = new Point(300, 10);

                            // Create a button to check the selected hotel
                            Button btnCheckHotel = new Button();
                            btnCheckHotel.Text = "Book Hotel";
                            btnCheckHotel.Tag = hotelName; // Store the hotel name as the button's tag
                            btnCheckHotel.Click += btnBooking_Click;
                            btnCheckHotel.AutoSize = true;
                            btnCheckHotel.Location = new Point(570, 80);
                            btnCheckHotel.Font = new Font(lblHotelOffers.Font.FontFamily, 15);

                            

                            // Adjust the label's location to avoid overlapping with the button
                            lblHotelName.Location = new Point(10, 10 + btnShowDetails.Height + 10);

                            // Add labels and picture box to the panel
                            hotelPanel.Controls.Add(lblHotelName);
                            hotelPanel.Controls.Add(lblHotelAddress);
                            hotelPanel.Controls.Add(lblHotelStar);
                            hotelPanel.Controls.Add(lblHotelOffers);
                            hotelPanel.Controls.Add(pbHotelImage);
                            hotelPanel.Controls.Add(btnCheckHotel);
                            hotelPanel.Controls.Add(btnShowDetails);


                            // Add the panel to the FlowLayoutPanel
                            flowLayoutPanelHotels.Controls.Add(hotelPanel);
                        }
                    }
                }
            }
        }






        private void btnBooking_Click(object sender, EventArgs e)
        {
            Button btnCheckHotel = (Button)sender; // Get the clicked button
            string hotelName = btnCheckHotel.Tag.ToString(); // Retrieve the hotel name from the button's tag

            // Close the HomePageControl
            this.Hide();

            // Create an instance of the BookingPageControl and pass the hotelName
            BookingPageControl bookingPageControl = new BookingPageControl();
            bookingPageControl.SetHotelName(hotelName);

            // Add the bookingPageControl to the controls of the main form
            Parent.Controls.Add(bookingPageControl);

            // Set the docking style of the bookingPageControl to fill the entire form
            bookingPageControl.Dock = DockStyle.Fill;

            // Show the bookingPageControl
            bookingPageControl.Show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT HotelName, HotelAddress, HotelStar, HotelOffers, HotelImage FROM Hotels WHERE HotelName LIKE @SearchText OR HotelAddress LIKE @SearchText OR HotelOffers LIKE @SearchText";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Clear existing hotel panels
                        flowLayoutPanelHotels.Controls.Clear();

                        while (reader.Read())
                        {
                            string hotelName = reader.GetString(0);
                            string hotelAddress = reader.GetString(1);
                            int hotelStar = reader.GetInt32(2);
                            string hotelOffers = reader.GetString(3);
                            byte[] hotelImageBytes = (byte[])reader.GetValue(4);
                            Image hotelImage = ConvertByteArrayToImage(hotelImageBytes);

                            // Create a panel to hold hotel details
                            Panel hotelPanel = new Panel();
                            hotelPanel.BorderStyle = BorderStyle.FixedSingle;
                            hotelPanel.Padding = new Padding(10);
                            hotelPanel.Size = new Size(745, 180);
                            hotelPanel.BackColor = Color.White;

                            int maxTextLength = 12;
                            // Create labels for hotel details
                            Label lblHotelName = new Label();
                            lblHotelName.Text = "Hotel Name: " + hotelName;
                            lblHotelName.AutoSize = true;
                            lblHotelName.Location = new Point(10, 10);
                            lblHotelName.Text = "Hotel Name: " + (hotelName.Length <= maxTextLength ? hotelName : hotelName.Substring(0, maxTextLength) + "...");
                            lblHotelName.Font = new Font(lblHotelName.Font.FontFamily, 15);

                            Label lblHotelAddress = new Label();
                            lblHotelAddress.Text = "Address: " + hotelAddress;
                            lblHotelAddress.AutoSize = true;
                            lblHotelAddress.Location = new Point(10, 50);
                            lblHotelAddress.Font = new Font(lblHotelAddress.Font.FontFamily, 15);
                            lblHotelAddress.Text = "Hotel Address: " + (hotelAddress.Length <= maxTextLength ? hotelAddress : hotelAddress.Substring(0, maxTextLength) + "...");

                            Label lblHotelStar = new Label();
                            lblHotelStar.Text = "Star: " + hotelStar.ToString();
                            lblHotelStar.AutoSize = true;
                            lblHotelStar.Location = new Point(10, 90);
                            lblHotelStar.Font = new Font(lblHotelStar.Font.FontFamily, 15);

                            Label lblHotelOffers = new Label();
                            lblHotelOffers.Text = "Offers: " + hotelOffers;
                            lblHotelOffers.AutoSize = true;
                            lblHotelOffers.Location = new Point(10, 130);
                            lblHotelOffers.Font = new Font(lblHotelOffers.Font.FontFamily, 15);
                            lblHotelOffers.Text = "Hotel Offers: " + (hotelOffers.Length <= maxTextLength ? hotelOffers : hotelOffers.Substring(0, maxTextLength) + "...");

                            // Create a PictureBox for hotel image
                            PictureBox pbHotelImage = new PictureBox();
                            pbHotelImage.Image = hotelImage;
                            pbHotelImage.SizeMode = PictureBoxSizeMode.StretchImage;
                            pbHotelImage.Width = 200;
                            pbHotelImage.Height = 150;
                            pbHotelImage.Location = new Point(300, 10);

                            // Create a button to check the selected hotel
                            Button btnCheckHotel = new Button();
                            btnCheckHotel.Text = "Book Hotel";
                            btnCheckHotel.Tag = hotelName; // Store the hotel name as the button's tag
                            btnCheckHotel.Click += btnBooking_Click;
                            btnCheckHotel.AutoSize = true;
                            btnCheckHotel.Location = new Point(570, 80);
                            btnCheckHotel.Font = new Font(lblHotelOffers.Font.FontFamily, 15);

                            // Add labels and picture box to the panel
                            hotelPanel.Controls.Add(lblHotelName);
                            hotelPanel.Controls.Add(lblHotelAddress);
                            hotelPanel.Controls.Add(lblHotelStar);
                            hotelPanel.Controls.Add(lblHotelOffers);
                            hotelPanel.Controls.Add(pbHotelImage);
                            hotelPanel.Controls.Add(btnCheckHotel);

                            // Add the panel to the FlowLayoutPanel
                            flowLayoutPanelHotels.Controls.Add(hotelPanel);
                        }
                    }
                }
            }
        }
    }
    }
