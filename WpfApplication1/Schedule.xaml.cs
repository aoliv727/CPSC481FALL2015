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
        private CourseBlock[] selected;
        private byte stColor1 = 193;
        private byte stColor2 = 191;
        private byte stColor3 = 236;
        private byte ctColor1 = 255;
        private byte ctColor2 = 127;
        private byte ctColor3 = 80;

        public Schedule(MainWindow screen)
        {
            InitializeComponent();
            this.screen = screen;
            grid.ShowGridLines = true;
            scheduledCourses = new CourseBlock[0];
            selected = new CourseBlock[0];
        }

        public CourseBlock[] getScheduledCourses()
        {
            return this.scheduledCourses;
        }

        public void setSelected(CourseBlock[] selected)
        {
            if (selected == null) { this.selected = new CourseBlock[0]; }
            else
            {
                for (int i = 0; i < selected.Length; i++)
                {
                    this.selected[i] = selected[i];
                }
            }
        }

        public void setScheduledCourses(CourseBlock[] scheduledCourses)
        {
            //this.scheduledCourses = scheduledCourses;
            for (int i = 0; i < scheduledCourses.Length; i++)
            {
                this.scheduledCourses[i] = scheduledCourses[i];
            }
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

        public void Update(byte c1, byte c2, byte c3)
        {
            if (scheduledCourses == null) { return; }
        
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
                               currNode.Fill = new SolidColorBrush(Color.FromRgb(c1, c2, c3));
                               currNode.ToolTip = scheduledCourses[i].getCourse() + scheduledCourses[i].getCourseNum() + scheduledCourses[i].getCourseName();
                               startIndex++;
                            }
                            break;
                        case "T":
                            for (int k = 0; k < timeDiff; k++)
                            {
                                Rectangle currNode = GetGridElement(startIndex, 2);
                                currNode.Fill = new SolidColorBrush(Color.FromRgb(c1, c2, c3));
                                currNode.ToolTip = scheduledCourses[i].getCourse() + scheduledCourses[i].getCourseNum() + scheduledCourses[i].getCourseName();
                                startIndex++;
                            }
                            break;
                        case "W":
                            for (int k = 0; k < timeDiff; k++)
                            {
                                Rectangle currNode = GetGridElement(startIndex, 3);
                                currNode.Fill = new SolidColorBrush(Color.FromRgb(c1, c2, c3));
                                currNode.ToolTip = scheduledCourses[i].getCourse() + scheduledCourses[i].getCourseNum() + scheduledCourses[i].getCourseName();
                                startIndex++;
                            }
                            break;
                        case "R":
                            for (int k = 0; k < timeDiff; k++)
                            {
                                Rectangle currNode = GetGridElement(startIndex, 4);
                                currNode.Fill = new SolidColorBrush(Color.FromRgb(c1, c2, c3));
                                currNode.ToolTip = scheduledCourses[i].getCourse() + scheduledCourses[i].getCourseNum() + scheduledCourses[i].getCourseName();
                                startIndex++;
                            }
                            break;
                        case "F":
                            for (int k = 0; k < timeDiff; k++)
                            {
                                Rectangle currNode = GetGridElement(startIndex, 5);
                                currNode.Fill = new SolidColorBrush(Color.FromRgb(c1, c2, c3));
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

       private void OnMouseUp(object sender, MouseButtonEventArgs e)
       { 
           //MessageBox.Show(e.GetPosition(grid).Y.ToString());
       }

       private void OnMouseDown(object sender, MouseButtonEventArgs e)
       {
           Rectangle currRec = (Rectangle)sender;

           if (currRec.Fill.ToString() != "#FFFFFFFF" && currRec.Fill.ToString() != "#FFFF7F50")
           {
               CourseBlock[] temp = new CourseBlock[0];
               for (int i = 0; i < scheduledCourses.Length; i++)
               {
                   String currCourse = scheduledCourses[i].getCourse() + scheduledCourses[i].getCourseNum() + scheduledCourses[i].getCourseName();
                   if (currCourse == currRec.ToolTip.ToString())
                   {
                       // Add to selected list
                       Array.Resize(ref selected, selected.Length + 1);
                       selected[selected.Length - 1] = new CourseBlock(scheduledCourses[i]);
                       ReColorSelected(ctColor1,ctColor2,ctColor3);
                   }
                   else
                   {
                       //Resize and update temp to create new schedule list -- selected is removed from schedule list
                       Array.Resize(ref temp, temp.Length + 1);
                       temp[temp.Length - 1] = new CourseBlock(scheduledCourses[i]);
                   }
               }
               this.scheduledCourses = temp;
           }

           else if (currRec.Fill.ToString() == "#FFFF7F50")
           {
               //remove from selected list and put back in schedule list and call update()
               CourseBlock[] temp = new CourseBlock[0];
               for (int i = 0; i < selected.Length; i++)
               {
                   String currCourse = selected[i].getCourse() + selected[i].getCourseNum() + selected[i].getCourseName();
                   if (currCourse == currRec.ToolTip.ToString())
                   {
                       // Add back to schedule list
                       Array.Resize(ref scheduledCourses, scheduledCourses.Length + 1);
                       scheduledCourses[scheduledCourses.Length - 1] = new CourseBlock(selected[i]);
                       Update(stColor1,stColor2,stColor3);
                   }
                   else
                   {
                       Array.Resize(ref temp, temp.Length + 1);
                       temp[temp.Length - 1] = new CourseBlock(selected[i]);
                   }
               }
               this.selected = temp;
           }
          // DropCourses();
       }

       public void ReColorSelected(byte c1, byte c2, byte c3)
       {
           for (int i = 0; i < selected.Length; i++)
           {
               // For every Day in that Course
               for (int j = 0; j < selected[i].getDays().Length; j++)
               {
                   String[] days = selected[i].getDays();
                   int startime = selected[i].getTimes()[0];
                   int endtime = selected[i].getTimes()[1];
                   int startIndex = startime - 6;
                   int timeDiff = endtime - startime;

                   // Color the schedule according to the day and times
                   switch (days[j])
                   {
                       case "M":
                           for (int k = 0; k < timeDiff; k++)
                           {
                               Rectangle currNode = GetGridElement(startIndex, 1);
                               currNode.Fill = new SolidColorBrush(Color.FromRgb(c1, c2, c3));
                               startIndex++;
                           }
                           break;
                       case "T":
                           for (int k = 0; k < timeDiff; k++)
                           {
                               Rectangle currNode = GetGridElement(startIndex, 2);
                               currNode.Fill = new SolidColorBrush(Color.FromRgb(c1, c2, c3));
                               startIndex++;
                           }
                           break;
                       case "W":
                           for (int k = 0; k < timeDiff; k++)
                           {
                               Rectangle currNode = GetGridElement(startIndex, 3);
                               currNode.Fill = new SolidColorBrush(Color.FromRgb(c1, c2, c3));
                               startIndex++;
                           }
                           break;
                       case "R":
                           for (int k = 0; k < timeDiff; k++)
                           {
                               Rectangle currNode = GetGridElement(startIndex, 4);
                               currNode.Fill = new SolidColorBrush(Color.FromRgb(c1, c2, c3));
                               startIndex++;
                           }
                           break;
                       case "F":
                           for (int k = 0; k < timeDiff; k++)
                           {
                               Rectangle currNode = GetGridElement(startIndex, 5);
                               currNode.Fill = new SolidColorBrush(Color.FromRgb(c1, c2, c3));
                               startIndex++;
                           }
                           break;
                   }
               }
           }
       }

    }
}
