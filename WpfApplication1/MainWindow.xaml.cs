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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CourseBlock[] allCourses;
        private CourseBlock toDrag;
        private Schedule schedule;
        private bool mouseOnSchedule = true;
        private CourseBlock[] WLCourses = new CourseBlock[0];
        private SwapCourseBlock[] S_courses;
        private string swapValueSelected; //course name of the courses you're going to swap out of from SCourses
        private int swapIndex; //index of the selected course you're going to swap out of from SCourses
        private CourseBlock[] SCourses = new CourseBlock[0];
        private CourseBlock[] CoursesToDrop = new CourseBlock[0];
        private CourseBlock currSelected;


        public MainWindow()
        {
            InitializeComponent();
            schedule = new Schedule(this);
            scheduleCanvas.Children.Add(schedule);
            Canvas.SetLeft(schedule, 10);
            Canvas.SetTop(schedule, 40);
            fileReader();

            for (int i = 0; i < allCourses.Length; i++)
            {
                this.courses.Children.Add(allCourses[i]);
            }
            populateSwap();
        }

        private void fileReader()
        {
            int seats, waitSeat, courseNum;
            String prof, course, courseName, type, details;
            String[] days;
            int[] times;

            System.IO.StreamReader file = new System.IO.StreamReader("../../res/CourseDatabase.txt");
            int numCourses = int.Parse(file.ReadLine());
            allCourses = new CourseBlock [numCourses];

            for (int i = 0; i < numCourses; i++)
            {
                seats = int.Parse(file.ReadLine());
                waitSeat = int.Parse(file.ReadLine());
                course = file.ReadLine();
                courseNum = int.Parse(file.ReadLine());
                courseName = file.ReadLine();
                type = file.ReadLine();
                prof = file.ReadLine();
                days = file.ReadLine().Split(',');
                String[] arr = file.ReadLine().Split(',');
                times = new int[arr.Length];
                for (int j = 0; j < arr.Length; j++)
                {
                    times[j] = int.Parse(arr[j]);
                }
                details = file.ReadLine();
                allCourses[i] = new CourseBlock(seats, waitSeat, prof, course, courseName, days, times, type, details, courseNum, this);
            }
        }

        //Populate the swap combobox dropdown thing
        private void populateSwap()
        {
            int seats, waitSeat, courseNum;
            String prof, course, courseName, type, details;
            String[] days;
            char[] splitter = { ',' };
            int[] times;

            System.IO.StreamReader file = new System.IO.StreamReader("../../res/CourseDatabase.txt");
            int numCourses = int.Parse(file.ReadLine());
            allCourses = new CourseBlock[numCourses];

            for (int i = 0; i < numCourses; i++)
            {
                seats = int.Parse(file.ReadLine());
                waitSeat = int.Parse(file.ReadLine());
                course = file.ReadLine();
                courseNum = int.Parse(file.ReadLine());
                courseName = file.ReadLine();
                //put it in the combobox
                Swap_combo.Items.Add(course + courseNum.ToString() + " " + courseName);
                type = file.ReadLine();
                prof = file.ReadLine();
                days = file.ReadLine().Split(splitter);
                String[] arr = file.ReadLine().Split(splitter);
                times = new int[arr.Length];
                for (int j = 0; j < arr.Length; j++)
                {
                    times[j] = int.Parse(arr[j]);
                }
                details = file.ReadLine();
                allCourses[i] = new CourseBlock(seats, waitSeat, prof, course, courseName, days, times, type, details, courseNum, this);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }


        //Swap selection thing
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        /*    swapValueSelected = Swap_combo.Text;
            swapValueSelected = swapValueSelected.Substring(8);//grabs from the 8th spot to the end (inclusive) (starts from 0)
            int k = 0;
            while (k < SCourses.Length)
            {
                if (SCourses[k].getCourseName() == swapValueSelected)
                {
                    swapIndex = k;
                    k = SCourses.Length;
                }
            }*/
        }
        //Swap Combo box
        /*
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SCourses == null) { return; }
            for (int i = 0; i < SCourses.Length; i++)
            {
                Swap_combo.Items.Add(SCourses[i].getCourse()+" "+SCourses[i].getCourseNum()+" "+SCourses[i].getCourseName());
            }
        }
        */

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        //Switch Tutorial Button
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (toDrag == null) { return; }
            Thickness margin = (toDrag.Margin);
            toDrag.Visibility = Visibility.Hidden;
            fakeDragObj.Visibility = Visibility.Visible;
           
            if (toDrag.getCaptured())
            {
                fakeDragObj.Margin = margin;
                margin.Left = e.GetPosition(mainGrid).X - (fakeDragObj.Width / 2);
                margin.Top = e.GetPosition(mainGrid).Y - (fakeDragObj.Height / 2);
                fakeDragObj.Margin = margin;
            }
        }

        public void setToDrag(CourseBlock toDrag)
        {
            this.toDrag = toDrag;
        }

        //Search button on Search tab
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.courses.Children.Clear();
            int j = 0;
            int k = 0;
            string course = S_Subject.Text;
            int courseNum;
            int.TryParse(S_courseNum.Text, out courseNum); 
            CourseBlock[] S_courses;
            for(int i = 0; i < allCourses.Length; i++)
            {
                if (allCourses[i].getCourse() == course)
                {
                    if (allCourses[i].getCourseNum()== courseNum || allCourses[i].getCourseNum() / 100 == courseNum 
                        || allCourses[i].getCourseNum() / 10 == courseNum)
                    {
                        j++;
                    }
                }
            }
            S_courses = new CourseBlock[j];

            for (int i = 0; i < allCourses.Length; i++)
            {
                if (allCourses[i].getCourse() == course)
                {
                    if (allCourses[i].getCourseNum() == courseNum || allCourses[i].getCourseNum() / 100 == courseNum || allCourses[i].getCourseNum() / 10 == courseNum)
                    {
                        CourseBlock temp = new CourseBlock(allCourses[i].getSeats(), allCourses[i].getWaitSeat(), allCourses[i].getProf(),
                                                            allCourses[i].getCourse(), allCourses[i].getCourseName(), allCourses[i].getDays(),
                                                            allCourses[i].getTimes(), allCourses[i].getType(), allCourses[i].getDetails(),
                                                            allCourses[i].getCourseNum(), this);
                        if (allCourses[i].getisWaitlised())
                        {
                            temp.triangle.Visibility = System.Windows.Visibility.Visible;
                            temp.square.Visibility = System.Windows.Visibility.Hidden;
                            temp.WaitlistBtn.Visibility = System.Windows.Visibility.Hidden;
                        }
                        S_courses[k] = temp;
                        k++;
                    }
                }
            }
            
            for (int i = 0; i < S_courses.Length; i++)
            {
                courses.Children.Add(S_courses[i]);
            }
            
        }

        public void AddtoWaitList(int seats, int waitSeat, string prof, string course, string courseName, string[] days, int[] times, string type, string details, int courseNum)
        {
           
            CourseBlock temp = new CourseBlock(seats, waitSeat, prof, course, courseName, days, times, type, details, courseNum, this);
            temp.WaitlistBtn.Visibility= System.Windows.Visibility.Hidden;
            temp.triangle.Visibility = System.Windows.Visibility.Visible;
            temp.square.Visibility = System.Windows.Visibility.Hidden;
            Array.Resize<CourseBlock>(ref WLCourses, WLCourses.Length +1);
            WLCourses[WLCourses.Length - 1] = temp;
            this.WL_List.Children.Clear();

            for (int i = 0; i < WLCourses.Length; i++)
            {
                this.WL_List.Children.Add(WLCourses[i]);
            }


            for (int i = 0; i < allCourses.Length; i++)
            {
                if (allCourses[i] == temp)
                {
                    
                        allCourses[i].setisWaitlisted(true); 
                    
                }
            }

        }

        // Search Combo box in search tab
        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            
        }


        public void uncheckSwap(SwapCourseBlock[] S_courses)
        {
            int i = 0;
            while (i < S_courses.Length)
            {
                S_courses[i].SwapSelector.Opacity = 0;
                i++;
            }
        }

        //Search Button on Swap tab
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.SwapCourses.Children.Clear();
            int j = 0;
            int k = 0;
            string course = SubjectSwap.Text;
            int courseNum;
            int.TryParse(courseNumSwap.Text, out courseNum);
            for (int i = 0; i < allCourses.Length; i++)
            {
                if (allCourses[i].getCourse() == course)
                {
                    if ((allCourses[i].getCourseNum() == courseNum || allCourses[i].getCourseNum() / 100 == courseNum || allCourses[i].getCourseNum() / 10 == courseNum)
                         && allCourses[i].getType() == "Lec")
                    {
                        j++;
                    }
                }
            }
            S_courses = new SwapCourseBlock[j];

            for (int i = 0; i < allCourses.Length; i++)
            {
                if (allCourses[i].getCourse() == course)
                {
                   
                    if ((allCourses[i].getCourseNum() == courseNum || allCourses[i].getCourseNum() / 100 == courseNum || allCourses[i].getCourseNum() / 10 == courseNum) 
                        && allCourses[i].getType() == "Lec") 
                    {
                        SwapCourseBlock temp = new SwapCourseBlock(allCourses[i].getSeats(), allCourses[i].getWaitSeat(), allCourses[i].getProf(),
                                                            allCourses[i].getCourse(), allCourses[i].getCourseName(), allCourses[i].getDays(),
                                                            allCourses[i].getTimes(), allCourses[i].getType(), allCourses[i].getDetails(), 
                                                            allCourses[i].getCourseNum(), this, S_courses);
                        /*
                        if (allCourses[i].getisWaitlised())
                        {
                            temp.triangle.Visibility = System.Windows.Visibility.Visible;
                            temp.square.Visibility = System.Windows.Visibility.Hidden;
                            temp.WaitlistBtn.Visibility = System.Windows.Visibility.Hidden;
                        }
                        */
                        S_courses[k] = temp;
                        k++;
                    }
                    
                }
            }

            for (int i = 0; i < S_courses.Length; i++)
            {
                SwapCourses.Children.Add(S_courses[i]);
            }

        }


        //SWAP BUTTON
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
         /*   //Find the course you want to swap into
            int i = -1;
            int j = 0;
            bool somethingSelected = false;
            while (j < S_courses.Length)
            {
                i++;
                if (S_courses[i].SwapSelector.Opacity > 0)
                {
                    j = S_courses.Length;
                    somethingSelected = true;
                }
                j++;
            }

            //Compare the selected course with current schedule and identify any conflicts 
            if (somethingSelected == true)
            {
                int k = 0;
                bool conflict = false;
                while (k < SCourses.Length)
                {
                    if (SCourses[k].getTimes() == S_courses[i].getTimes() && SCourses[k].getCourseName() != swapValueSelected)
                    {
                        conflict = true;
                        errors message;
                    }
                }
                if (conflict == false)
                {
                    SCourses[swapIndex] = S_courses[i];
                }
            }*/
        }


        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            bool success;
            if (e.GetPosition(mainGrid).Y > 50 && e.GetPosition(mainGrid).Y < 731  && e.GetPosition(mainGrid).X > 386 && e.GetPosition(mainGrid).X < 1160)
            {           
                success = schedule.tryToSchedule(toDrag);
                if (success)
                {
                    // Remove course from stack and all Courses
                    courses.Children.Remove(toDrag);
                    fakeDragObj.Visibility = Visibility.Hidden;

                    // Add course to Schedule
                    Array.Resize(ref SCourses, SCourses.Length + 1);
                    SCourses[SCourses.Length - 1] = toDrag;
                    schedule.Update();

                    // Set toDrag to null 
                    this.toDrag = null;
                }
                else
                {
                    if (e.Handled == false)
                    {
                        toDrag.Visibility = Visibility.Visible;
                        fakeDragObj.Visibility = Visibility.Hidden;
                        this.toDrag = null;
                    }
                }
            }
            else
            {
                if (e.Handled == false && toDrag != null)
                {
                    toDrag.Visibility = Visibility.Visible;
                    fakeDragObj.Visibility = Visibility.Hidden;
                    this.toDrag = null;
                }
            }
            
        }

        public void setOnSchedule(bool onSchedule)
        {
            this.mouseOnSchedule = onSchedule;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.Handled == true) { return; }
        }

        public void setCurrSelected(CourseBlock currSelected)
        {
            this.currSelected = currSelected;
        }

        // Drop Button
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            CourseBlock[] temp = new CourseBlock[0];

            for (int i = 0; i < SCourses.Length; i++)
            {
                for (int j = 0; j < CoursesToDrop.Length; j++)
                {
                    if(SCourses[i] != CoursesToDrop[j])
                    {
                        Array.Resize(ref temp, temp.Length + 1);
                        temp[i] = SCourses[i];
                    }
                }

            }

            this.SCourses = temp;
            schedule.Update();
        }

        public void setCoursesToDrop(CourseBlock[] coursesToDrop)
        {
            this.CoursesToDrop = coursesToDrop;
        }

        //Enroll Button
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {

        }
    }
}
