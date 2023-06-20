using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookMyCourt
{
    public partial class dgvRooms : UserControl
    {
        private string connectionString = @"Data Source=DESKTOP-JJULN80\SQLEXPRESS;Initial Catalog=DBbooking;Integrated Security=True";



        public dgvRooms()
        {
            InitializeComponent();


            LoadHotelInfo();

        }
      

      

        private void btnDeleteRoom_Click(object sender, EventArgs e)
        {
            // Check if a row is selected in the dataGridViewHotels
            if (dataGridViewHotels.SelectedRows.Count > 0)
            {
                // Get the selected hotel name
                string hotelName = dataGridViewHotels.SelectedRows[0].Cells["HotelName"].Value.ToString();

                // Prompt the user for confirmation before deleting the hotel
                DialogResult result = MessageBox.Show("Are you sure you want to delete the hotel '" + hotelName + "'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Delete the hotel from the Hotels table in the database
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string deleteQuery = "DELETE FROM Hotels WHERE HotelName = @HotelName";

                        using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                        {
                            command.Parameters.AddWithValue("@HotelName", hotelName);

                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }

                    // Refresh the dataGridViewHotels to reflect the changes
                    LoadHotelInfo();
                }
            }
            else
            {
                MessageBox.Show("Please select a hotel to delete.", "No Hotel Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void LoadHotelInfo()
        {
            string selectQuery = "SELECT HotelName, HotelAddress, HotelStar, HotelOffers, HotelImage FROM Hotels";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable hotelInfoTable = new DataTable();
                        adapter.Fill(hotelInfoTable);

                        dataGridViewHotels.DataSource = hotelInfoTable;
                    }
                }
            }
        }


        private void txtHotelOffers_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pbHotelImage.Image = Image.FromFile(openFileDialog.FileName);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Retrieve the input values from the TextBoxes
            string hotelName = txtHotelName.Text;
            string hotelAddress = txtHotelAddress.Text;
            int hotelStar = int.Parse(txtHotelStar.Text);
            string hotelOffers = txtHotelOffers.Text;

            // Retrieve the hotel image as a byte array
            byte[] hotelImage = null;
            if (pbHotelImage.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    pbHotelImage.Image.Save(ms, ImageFormat.Jpeg); // Assuming the image format is JPEG
                    hotelImage = ms.ToArray();
                }
            }

            // Insert the data into the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string insertQuery = "INSERT INTO Hotels (HotelName, HotelAddress, HotelStar, HotelOffers, HotelImage) " +
                                     "VALUES (@HotelName, @HotelAddress, @HotelStar, @HotelOffers, @HotelImage)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@HotelName", hotelName);
                    command.Parameters.AddWithValue("@HotelAddress", hotelAddress);
                    command.Parameters.AddWithValue("@HotelStar", hotelStar);
                    command.Parameters.AddWithValue("@HotelOffers", hotelOffers);
                    command.Parameters.AddWithValue("@HotelImage", hotelImage);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Hotel data inserted successfully!");
                        // Clear the input fields after successful insertion
                        txtHotelName.Text = string.Empty;
                        txtHotelAddress.Text = string.Empty;
                        txtHotelStar.Text = string.Empty;
                        txtHotelOffers.Text = string.Empty;
                        pbHotelImage.Image = null;
                    }
                    else
                    {
                        MessageBox.Show("Failed to insert hotel data.");
                    }
                }
            }
         
            // Clear the input fields after successful insertion
            // ...
        }
    }
}
