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
       // private Boolean captured;

<<<<<<< HEAD
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
            this.screen = c.screen;
           // this.captured = c.captured;
            //this.Width = c.Width;
           // this.Height = c.Height;
           // this.Margin = c.Margin;
        }

=======
>>>>>>> master
        public CourseBlock(int seats, int waitSeat, String prof, String course, String courseName, String[] days, int[] times, String type, String details, int courseNum, MainWindow screen)
        {
            String printDays = null;
            InitializeComponent();
            // Set all the private Variables
            this.screen = screen;
<<<<<<< HEAD
            //this.captured = false;
=======
>>>>>>> master
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
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            triangle.Visibility = System.Windows.Visibility.Visible;
            square.Visibility = System.Windows.Visibility.Hidden;
            WaitlistBtn.Visibility = System.Windows.Visibility.Hidden;
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

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Package the data.
                DataObject data = new DataObject();
                data.SetData("Object", this);

                // Inititate the drag-and-drop operation.
                DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
            }
        }

        private void OnGiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            base.OnGiveFeedback(e);
            // These Effects values are set in the drop target's
            // DragOver event handler.
           if (e.Effects.HasFlag(DragDropEffects.Move))
            {
                Mouse.SetCursor(Cursors.Hand);
            }
            else
            {
                Mouse.SetCursor(Cursors.No);
            }
            e.Handled = true;
        }

        /*
        public void setCaptured(Boolean captured)
        {
            this.captured = captured;
        }

        public Boolean getCaptured()
        {
            return this.captured;
        }
        
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            captured = true;
            screen.setToDrag(this);
        }
        */


    }
}
