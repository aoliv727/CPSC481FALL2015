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
            grid.ShowGridLines = true;
            scheduledCourses = new CourseBlock[0];
            dropList = new CourseBlock[0];
        }

        public bool tryToSchedule(CourseBlock course)
        {
            bool success = true;

            // Check if Closed
            if (!course.getisClosed())
            {
                // Check for time Conflicts
                int[] courseTimes = course.getTimes();
                for (int i = 0; i < scheduledCourses.Length; i++)
                {
                    int[] currCourseTimes = scheduledCourses[i].getTimes();
                    if (scheduledCourses[i] == course)
                    {
                        success = false;
                    }
                    else if (courseTimes[0] < currCourseTimes[1] && courseTimes[0] >= currCourseTimes[0])
                    {
                        success = false;
                    }
                    else if (courseTimes[1] > currCourseTimes[0] && courseTimes[1] <= currCourseTimes[1])
                    {
                        success = false;
                    }
                }
            }
            else
            {
                success = false;
            }
            // If no time Conflict and not closed then add it to Schedule Array
            if (success)
            {
                Array.Resize(ref scheduledCourses, scheduledCourses.Length + 1);
                scheduledCourses[scheduledCourses.Length - 1] = course;
            }
            return success;
        }

        private void DropCourses()
        {
            screen.setCoursesToDrop(this.dropList);
        }

        private void AddToDroplist()
        {

        }

        public void Update()
        {
            // For every Course in schedule
            for (int i = 0; i < scheduledCourses.Length; i++)
            {

                // For every Day in that Course
                for (int j = 0; j < scheduledCourses[i].getDays().Length; j++)
                {
                    String[] days = scheduledCourses[i].getDays();
                    int startime = scheduledCourses[i].getTimes()[0];
                    int endtime = scheduledCourses[i].getTimes()[1];
                    int startIndex = startime - 6;
                    int timeDiff = endtime - startime;

                    // Color the schedule according to the day and times
                    switch (days[j])
                    {
                        case "M":
                            for (int k = 0; k < timeDiff; k++)
                            {
                               Rectangle currNode = GetGridElement(startIndex, 1);
                               currNode.Fill = new SolidColorBrush(Color.FromRgb(193, 191, 236));
                               currNode.ToolTip = scheduledCourses[i].getCourse() + scheduledCourses[i].getCourseNum() + scheduledCourses[i].getCourseName();
                               startIndex++;
                            }
                            break;
                        case "T":
                            for (int k = 0; k < timeDiff; k++)
                            {
                                Rectangle currNode = GetGridElement(startIndex, 2);
                                currNode.Fill = new SolidColorBrush(Color.FromRgb(193, 191, 236));
                                currNode.ToolTip = scheduledCourses[i].getCourse() + scheduledCourses[i].getCourseNum() + scheduledCourses[i].getCourseName();
                                startIndex++;
                            }
                            break;
                        case "W":
                            for (int k = 0; k < timeDiff; k++)
                            {
                                Rectangle currNode = GetGridElement(startIndex, 3);
                                currNode.Fill = new SolidColorBrush(Color.FromRgb(193, 191, 236));
                                currNode.ToolTip = scheduledCourses[i].getCourse() + scheduledCourses[i].getCourseNum() + scheduledCourses[i].getCourseName();
                                startIndex++;
                            }
                            break;
                        case "R":
                            for (int k = 0; k < timeDiff; k++)
                            {
                                Rectangle currNode = GetGridElement(startIndex, 4);
                                currNode.Fill = new SolidColorBrush(Color.FromRgb(193, 191, 236));
                                currNode.ToolTip = scheduledCourses[i].getCourse() + scheduledCourses[i].getCourseNum() + scheduledCourses[i].getCourseName();
                                startIndex++;
                            }
                            break;
                        case "F":
                            for (int k = 0; k < timeDiff; k++)
                            {
                                Rectangle currNode = GetGridElement(startIndex, 5);
                                currNode.Fill = new SolidColorBrush(Color.FromRgb(193, 191, 236));
                                currNode.ToolTip = scheduledCourses[i].getCourse() + scheduledCourses[i].getCourseNum() + scheduledCourses[i].getCourseName();
                                startIndex++;
                            }
                            break;
                    }
                }
            }
        }

       private Rectangle GetGridElement(int row, int column)
        {
            foreach (UIElement child in grid.Children)
            {
                if (Grid.GetRow(child) == row && Grid.GetColumn(child) == column)
                {
                    return (Rectangle)child;
                }
            }
            return null;
        }

    }
}
