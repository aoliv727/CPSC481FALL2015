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
        private CourseBlock[] WLCourses = new CourseBlock[0];
        private SwapCourseBlock[] S_courses;
        private string swapValueSelected; //course name of the courses you're going to swap out of from SCourses
        private int swapIndex; //index of the selected course you're going to swap out of from SCourses

        public MainWindow()
        {
            InitializeComponent();
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
            char[] splitter = { ',' };
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

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
                    if ((allCourses[i].getCourseNum()== courseNum || allCourses[i].getCourseNum() / 100 == courseNum || allCourses[i].getCourseNum() / 10 == courseNum) && !(allCourses[i].getisWaitlised()))
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
                    if ((allCourses[i].getCourseNum() == courseNum || allCourses[i].getCourseNum() / 100 == courseNum || allCourses[i].getCourseNum() / 10 == courseNum) && !(allCourses[i].getisWaitlised()))
                    {
                        CourseBlock temp = new CourseBlock(allCourses[i].getSeats(), allCourses[i].getWaitSeat(), allCourses[i].getProf(),
                                                            allCourses[i].getCourse(), allCourses[i].getCourseName(), allCourses[i].getDays(),
                                                            allCourses[i].getTimes(), allCourses[i].getType(), allCourses[i].getDetails(),
                                                            allCourses[i].getCourseNum(), this);
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

        //Swap search button
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
                    if ((allCourses[i].getCourseNum() == courseNum || allCourses[i].getCourseNum() / 100 == courseNum || allCourses[i].getCourseNum() / 10 == courseNum) && !(allCourses[i].getisWaitlised()))
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
                   
                    if ((allCourses[i].getCourseNum() == courseNum || allCourses[i].getCourseNum() / 100 == courseNum || allCourses[i].getCourseNum() / 10 == courseNum) && !(allCourses[i].getisWaitlised())) 
                    {
                        SwapCourseBlock temp = new SwapCourseBlock(allCourses[i].getSeats(), allCourses[i].getWaitSeat(), allCourses[i].getProf(),
                                                            allCourses[i].getCourse(), allCourses[i].getCourseName(), allCourses[i].getDays(),
                                                            allCourses[i].getTimes(), allCourses[i].getType(), allCourses[i].getDetails(), 
                                                            allCourses[i].getCourseNum(), this, S_courses);
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

    }
}
