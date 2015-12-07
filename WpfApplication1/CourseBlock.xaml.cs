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
        private MainWindow screen;
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
        private Boolean captured;
        private Boolean isClosed;


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
            this.isClosed = c.isClosed;
            this.screen = c.screen;
            this.captured = c.captured;

            this.Width = c.Width;
            this.Height = c.Height;
            this.Margin = c.Margin;
        }


        public CourseBlock(int seats, int waitSeat, String prof, String course, String courseName, String[] days, int[] times, String type, String details, int courseNum, MainWindow screen)
        {
            String printDays = null;
            InitializeComponent();

            // Set all the private Variables
            this.screen = screen;
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
            this.isWaitlisted = false;

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

           if(isWaitlisted)
            {
                triangle.Visibility = Visibility.Visible;
                square.Visibility = Visibility.Hidden;
                WaitlistBtn.Visibility = Visibility.Hidden;
            }
           else if (seats == 100 && waitSeat == 10)
            {
                isClosed = true;
                square.Visibility = Visibility.Visible;
                border.BorderBrush = Brushes.Blue;
            }
            else if (seats == 100 && waitSeat != 10)
            {
                isClosed = true;
                square.Visibility = Visibility.Visible;
                WaitlistBtn.Visibility = Visibility.Visible;
                border.BorderBrush = Brushes.Blue;
            }
            else
            {
                circle.Visibility = Visibility.Visible;
                border.BorderBrush = Brushes.Green;
            }
        }

        // WaitList Button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            triangle.Visibility = Visibility.Visible;
            square.Visibility = Visibility.Hidden;
            WaitlistBtn.Visibility = Visibility.Hidden;
            isWaitlisted = true;
            screen.AddtoWaitList(this.seats, this.waitSeat, this.prof, this.course, this.courseName, this.days, this.times, this.type, this.details, this.courseNum);
        }

        public string getCourse()
        {
            return this.course;
        }

        public int getCourseNum()
        {
            return this.courseNum;
        }
        public int getSeats()
        {
            return this.seats;
        } 

        public int getWaitSeat ()
        {
            return this.waitSeat;
        }

        public string getProf()
        {
            return this.prof;
        }

        public string getCourseName()
        {
            return this.courseName;
        }

        public string[] getDays()
        {
            return this.days;
        }

        public int[] getTimes()
        {
            return this.times;
        }

        public string getType()
        {
            return this.type;
        }

       public string getDetails()
        {
            return this.details;
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

        public void setisClosed(Boolean isClosed)
        {
            this.isClosed = isClosed;
        }

        public Boolean getisClosed()
        {
            return this.isClosed;
        }

        public void setCaptured(Boolean captured)
        {
            this.captured = captured;
        }

        public Boolean getCaptured()
        {
            return this.captured;
        }
        

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.captured = false;
            screen.setToDrag(null);
            e.Handled = true;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.captured = true;
            screen.setToDrag(this);
            e.Handled = true;
        }

    }
}
