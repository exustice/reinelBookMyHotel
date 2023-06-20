using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyCourt
{
    using System;

    public class DateSelectedEventArgs : EventArgs
    {
        public DateTime SelectedDate { get; set; }

        public DateSelectedEventArgs(DateTime selectedDate)
        {
            SelectedDate = selectedDate;
        }
    }
}
