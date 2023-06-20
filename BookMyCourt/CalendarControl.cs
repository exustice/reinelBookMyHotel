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
using System.Xml;

namespace BookMyCourt
{
    public partial class CalendarControl : UserControl
    {
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CalendarControl_Load_1(object sender, EventArgs e)
        {
        }
            public event EventHandler<DateTime> DateSelected;
    
        private void OnDateSelected(DateTime selectedDate)
        {
            DateSelected?.Invoke(this, selectedDate);
        }

        private DateTime _selectedDate;

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                UpdateCalendar();
            }
        }


        public CalendarControl()
        {
            InitializeComponent();
            SelectedDate = DateTime.Today;
            CreateReservationPanel();
        }

        private void CreateTimeSlots()
        {
            reservationPanel.Controls.Clear();
            for (int i = 0; i < 17; i++)
            {
                Label timeLabel = new Label();
                int hour = 6 + i / 1;
                string amPm = (hour < 12 ? "AM" : "PM");
                hour = (hour % 12 == 0) ? 12 : hour % 12;
                timeLabel.Text = hour.ToString() + ":" + (00).ToString("00") + " " + amPm;


                timeLabel.Dock = DockStyle.Fill;
                reservationPanel.Controls.Add(timeLabel, 0, i);

                Panel reservationSlotPanel = new Panel();
                reservationSlotPanel.BorderStyle = BorderStyle.FixedSingle;
                reservationSlotPanel.Margin = new Padding(0);
                reservationSlotPanel.Dock = DockStyle.Fill;
                reservationSlotPanel.Click += ReservationSlotPanel_Click;
                reservationPanel.Controls.Add(reservationSlotPanel, 1, i);

            }
        }
        private void ReservationSlotPanel_Click(object sender, EventArgs e)
        {
          
        }
        private void CreateReservationPanel()
        {
            // Clear the current content of the reservationPanel
            reservationPanel.Controls.Clear();
            reservationPanel.ColumnStyles.Clear();
            reservationPanel.RowStyles.Clear();

            // Set the number of rows and columns in the reservationPanel
            reservationPanel.ColumnCount = 2;
            reservationPanel.RowCount = 17;

            // Set the width of the first column
            reservationPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            reservationPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            // Set the height of each row
            for (int i = 0; i < 17; i++)
            {
                reservationPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            }

            // Add the time slots to the first column
            CreateTimeSlots();

            // Add the details of the user who reserved in the second column
            // ...
        }
        private void UpdateCalendar()
        {
            // Clear the current calendar
            calendarPanel.Controls.Clear();

            // Add the month and year labels
            Label monthLabel = new Label();
            monthLabel.Text = SelectedDate.ToString("MMMM");
            monthLabel.Dock = DockStyle.Fill;
            monthLabel.TextAlign = ContentAlignment.MiddleCenter;
            calendarPanel.Controls.Add(monthLabel, 0, 0);

            Label yearLabel = new Label();
            yearLabel.Text = SelectedDate.Year.ToString();
            yearLabel.Dock = DockStyle.Fill;
            yearLabel.TextAlign = ContentAlignment.MiddleCenter;
            calendarPanel.Controls.Add(yearLabel, 1, 0);

            // Create the header row
            string[] daysOfWeek = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
            for (int i = 0; i < 7; i++)
            {
                Label dayLabel = new Label();
                dayLabel.Text = daysOfWeek[i];
                dayLabel.Dock = DockStyle.Fill;
                dayLabel.TextAlign = ContentAlignment.MiddleCenter;
                calendarPanel.Controls.Add(dayLabel, i, 1);
            }

            // Calculate the first day of the month
            DateTime firstDayOfMonth = new DateTime(SelectedDate.Year, SelectedDate.Month, 1);
            int daysInMonth = DateTime.DaysInMonth(SelectedDate.Year, SelectedDate.Month);

            // Create the calendar cells
            int row = 2;
            int col = (int)firstDayOfMonth.DayOfWeek;
            for (int i = 1; i <= daysInMonth; i++)
            {
                Label dayLabel = new Label();
                dayLabel.Text = i.ToString();
                dayLabel.Dock = DockStyle.Fill;
                dayLabel.TextAlign = ContentAlignment.MiddleCenter;
                dayLabel.Click += DayLabel_Click;

                calendarPanel.Controls.Add(dayLabel, col, row);

                col++;
                if (col > 6)
                {
                    col = 0;
                    row++;
                }
            }
        }


        private void DayLabel_Click(object sender, EventArgs e)
        {
            Label dayLabel = (Label)sender;
            int day = int.Parse(dayLabel.Text);
            SelectedDate = new DateTime(SelectedDate.Year, SelectedDate.Month, day);
            DateTime clickedDate = new DateTime(SelectedDate.Year, SelectedDate.Month, day);
            // Show the reservation panel and position it next to the clicked day label
            reservationPanel.Visible = true;
            reservationPanel.Location = new Point(dayLabel.Location.X + dayLabel.Width, dayLabel.Location.Y);

          

        }

    

        private void prevMonthButton_Click_1(object sender, EventArgs e)
        {
            SelectedDate = SelectedDate.AddMonths(-1);
            UpdateCalendar();
        }

       

        private void reservationPanel_Click(object sender, EventArgs e)
        {
            reservationPanel.Visible = false;
        }

        private void nextMonthButton_Click(object sender, EventArgs e)
        {
            SelectedDate = SelectedDate.AddMonths(1);
            SelectedDate = new DateTime(SelectedDate.Year, SelectedDate.Month, 1);
            UpdateCalendar();
        }
    }




}


       
    

   
        