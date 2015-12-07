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
    /// Interaction logic for SwapCourseBlock.xaml
    /// </summary>
    public partial class SwapCourseBlock : UserControl
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
        private SwapCourseBlock[] S_courses;
        public SwapCourseBlock(int seats, int waitSeat, String prof, String course, String courseName, String[] days, int[] times, String type, String details, int courseNum, MainWindow screen, SwapCourseBlock[] S_courses)
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
            this.S_courses = S_courses;

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

            if (seats == 100)
            {
                square.Visibility = System.Windows.Visibility.Visible;
                border.BorderBrush = Brushes.Blue;
            }
            else
            {
                circle.Visibility = System.Windows.Visibility.Visible;
                border.BorderBrush = Brushes.Green;
            }
           /*if(isWaitlisted)
            {
                triangle.Visibility = System.Windows.Visibility.Visible;
                square.Visibility = System.Windows.Visibility.Hidden;
                WaitlistBtn.Visibility = System.Windows.Visibility.Hidden;
            }
           else if (seats == 100 && waitSeat == 10)
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
            }*/
        }
        

        private void SwapSelect(object sender, MouseButtonEventArgs e)
        {
            screen.uncheckSwap(S_courses);
            SwapSelector.Opacity = 0.5;
        }

        //RANDOM CRAP FROM COURSEBLOCK *************************************************************
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
    
    }
}
