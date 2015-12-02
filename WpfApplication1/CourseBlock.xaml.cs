using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for CourseBlock.xaml
    /// </summary>
    public partial class CourseBlock : UserControl
    {
        private int seats;
        private int waitSeat;
        private String prof;
        private String course;
        private int courseNum;
        private String courseName;
        private String[] days;
        private int[] times;
        private String type;
        private String details;
        private Boolean isErolled;
        private Boolean isWaitlisted;

        public CourseBlock(CourseBlock c)
        {
            this.seats = c.seats;
            this.waitSeat = c.waitSeat;
            this.prof = c.prof;
            this.course = c.course;
            this.courseNum = c.courseNum;
            this.courseName = c.courseName;
            this.days = c.days;
            this.times = c.times;
            this.type = c.type;
            this.details = c.details;
            this.isErolled = c.isErolled;
            this.isWaitlisted = c.isWaitlisted;
        }

        public CourseBlock(int seats, int waitSeat, String prof, String course, String courseName, String[] days, int[] times, String type, String details, int courseNum)
        {
            String printDays = null;
            InitializeComponent();
            // Set all the private Variables
            this.seats = seats;
            this.waitSeat = waitSeat;
            this.prof = prof;
            this.course = course;
            this.courseName = courseName;
            this.days = days;
            this.times = times;
            this.type = type;
            this.details = details;
            this.courseNum = courseNum;

            // Input all relavent values into the GUI
            courselbl.Content = this.course;
            courseNamelbl.Content = courseName;
            typelbl.Content = type;
            proflbl.Content = prof;
            for (int i = 0; i < days.Length; i++)
            {
                printDays = printDays + days[i] + " ";
            }
            timeslbl.Content = printDays + "  " + times[0].ToString() + " - " + times[1].ToString();
            detailslbl.ToolTip = details;
            courseNumlbl.Content = courseNum;

            //NOT YET IMPLEMENTED A TRIANGLE FOR WAITLIST FF2379CF ("#FF4CD62D")


            if (seats == 100 && waitSeat == 10)
            {
                square.Visibility = System.Windows.Visibility.Visible;
                border.BorderBrush = Brushes.Blue;
            }
            else if (seats == 100 && waitSeat != 10)
            {
                square.Visibility = System.Windows.Visibility.Visible;
                WaitlistBtn.Visibility = System.Windows.Visibility.Visible;
                border.BorderBrush = Brushes.Blue;
            }
            else
            {
                circle.Visibility = System.Windows.Visibility.Visible;
                border.BorderBrush = Brushes.Green;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        public void setEnrolled(Boolean isEnrolled)
        {
            this.isErolled = isEnrolled;
        }

        public Boolean getisEnrolled()
        {
            return this.isErolled;
        }

        public void setisWaitlisted(Boolean isWaitlisted)
        {
            this.isWaitlisted = isWaitlisted;
        }

        public Boolean getisWaitlised()
        {
            return this.isWaitlisted;
        }
    }
}
