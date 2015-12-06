using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Schedule.xaml
    /// </summary>
    public partial class Schedule : UserControl
    {
        private MainWindow screen;
        private CourseBlock[] scheduledCourses;
        private CourseBlock[] dropList;

        public Schedule(MainWindow screen)
        {
            InitializeComponent();
            this.screen = screen;
            scheduledCourses = new CourseBlock[0];
            dropList = new CourseBlock[0];
        }

        public bool tryToSchedule(CourseBlock course)
        {
            bool success = true;
            int[] courseTimes = course.getTimes();         

            // Check for time Conflicts
            for(int i = 0; i < scheduledCourses.Length; i++)
            {
                int[] currCouseTimes = scheduledCourses[i].getTimes();
                if (scheduledCourses[i] == course)
                {
                    success = false;
                }
                else if(courseTimes[0] < currCouseTimes[1] && courseTimes[0] >= currCouseTimes[0])
                {
                    success = false;
                }
                else if(courseTimes[1] > currCouseTimes[0] && courseTimes[1] <= currCouseTimes[1])
                {
                    success = false;
                } 
            }
            // If no time Conflict then add it to Schedule Array & Update Schedule
            if(success)
            {
                Array.Resize(ref scheduledCourses, scheduledCourses.Length + 1);
                scheduledCourses[scheduledCourses.Length] = course;
                Update();
            }       
            return success;
        }

        private void DropCourses()
        {
            screen.setCoursesToDrop(this.dropList);
        }

        public void Update()
        {
            //Read from the schedule and display it's courses
        }
    }
}
