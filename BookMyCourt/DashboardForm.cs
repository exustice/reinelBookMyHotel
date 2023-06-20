using BookMyCourt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookMyCourt
{
    public partial class DashboardForm : Form
    {
        static DashboardForm _obj;

        public static DashboardForm Instance
        {
            get
            {
                if (_obj == null)
                {
                    _obj = new DashboardForm();
                }
                return _obj;
            }
        }
        public Panel PnlContainer
        {
            get { return panelContainer; }
            set { panelContainer = value; }
        }

       
        public DashboardForm()
        {
            InitializeComponent();
        }


     


        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                LoginForm loginForm = new LoginForm();
                this.Hide();
                loginForm.Show();
            }

        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
               
              

                HomePageControl hpc = new HomePageControl();
                hpc.Dock = DockStyle.Fill;
                panelContainer.Controls.Add(hpc);
            
        }


        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            HomePageControl hpc = new HomePageControl();
            hpc.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(hpc);
        }

        private void btnCalendar_Click(object sender, EventArgs e)
        {
            BookingPageControl BC = new BookingPageControl();
            BC.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(BC);
            
           
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            BookingListControl blc = new BookingListControl();
            blc.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(blc);
        }


     

        private void btnNotif_Click_1(object sender, EventArgs e)
        {
            NotificationControl upc = new NotificationControl();
            upc.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(upc);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string url = "https://www.instagram.com/";

            System.Diagnostics.Process.Start(url);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string url = "https://twitter.com/";

            System.Diagnostics.Process.Start(url);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string url = "https://www.facebook.com/";

            System.Diagnostics.Process.Start(url);
        

        }
    }
}