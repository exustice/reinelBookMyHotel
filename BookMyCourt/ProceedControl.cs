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
    public partial class ProceedControl : UserControl
    {
        private string reservationID;

        public ProceedControl(string reservationID)
        {
            InitializeComponent();

            this.reservationID = reservationID;

        }

       
    }
}
