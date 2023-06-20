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
    public partial class AdminDashboardForm : Form
    {
        public AdminDashboardForm()
        {
            InitializeComponent();
            AdminHomeControl ahc = new AdminHomeControl();
            ahc.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(ahc);
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            dgvRooms rc = new dgvRooms();
            rc.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(rc);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            AdminHomeControl ahc = new AdminHomeControl();
            ahc.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(ahc);
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
    }
}
