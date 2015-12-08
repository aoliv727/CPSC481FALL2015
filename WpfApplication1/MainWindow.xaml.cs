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
        private CourseBlock[] WLCourses = new CourseBlock[0];
        private SwapCourseBlock[] S_courses = new SwapCourseBlock[0]; //Courses you searched up
        private string swapValueSelected; //course name of the courses you're going to swap out of from SCourses
        private string switchValueSelected;
        private int swapIndex; //index of the selected course you're going to swap out of from SCourses
        private CourseBlock[] SCourses = new CourseBlock[0];
        private CourseBlock[] SelectedCourses = new CourseBlock[0];
        private CourseBlock toSwitch = null;
        private CourseBlock currSelected;
        private byte clear = 255;
        private byte stColor1 = 193;
        private byte stColor2 = 191;
        private byte stColor3 = 236;


        public MainWindow()
        {
            InitializeComponent();
            schedule = new Schedule(this);
          
            scheduleCanvas.Children.Add(schedule);
            Canvas.SetLeft(schedule, 10);
            Canvas.SetTop(schedule, 40);

            fileReader();
            populateSwap();
            populateSwitch();
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

        //populate the switch tutorial combo box
        private void populateSwitch()
        {
            if (SCourses == null) { return; };
            switch_combo.Items.Clear();
            for (int i = 0; i < SCourses.Length; i++)
            {
                if (SCourses[i].getType() != "Lec")
                {
                    switch_combo.Items.Add(SCourses[i].getCourse() + SCourses[i].getCourseNum() + " " + SCourses[i].getCourseName());
                }
            }
        }

        //Populate the swap combobox dropdown thing
        private void populateSwap()
        {
            if (SCourses == null) { return; };
            Swap_combo.Items.Clear();
            for (int i = 0; i < SCourses.Length; i++)
            {
                if(SCourses[i].getType() == "Lec")
                {
                Swap_combo.Items.Add(SCourses[i].getCourse() + SCourses[i].getCourseNum() + " " + SCourses[i].getCourseName());
                }
            }
        }

        //Swap Combo Box
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            swapValueSelected = Swap_combo.Text;

            if (swapValueSelected.Length >= 8)
            {
                swapValueSelected = swapValueSelected.Substring(8);//grabs from the 8th spot to the end (inclusive) (starts from 0)
                int k = 0;
                while (k < SCourses.Length)
                {
                    if (SCourses[k].getCourseName() == swapValueSelected)
                    {
                        swapIndex = k;
                        k = SCourses.Length;
                    }
                    k++;
                }
            }
        }

        //SWITCH BUTTON
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Find the course you want to switch into
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

            if(somethingSelected)
            {
            CourseBlock[] temp = new CourseBlock[0];
            for (int k = 0; k < SCourses.Length; k++)
            {
                if(toSwitch != SCourses[k])
                {
                    Array.Resize(ref temp, temp.Length + 1);
                    temp[temp.Length - 1] = SCourses[k];
                }
            }
            this.SCourses = temp;

            Array.Resize(ref SCourses, SCourses.Length + 1);
            SCourses[SCourses.Length - 1] = new CourseBlock(S_courses[i].getSeats(), S_courses[i].getWaitSeat(), S_courses[i].getProf(),
                                                            S_courses[i].getCourse(), S_courses[i].getCourseName(), S_courses[i].getDays(),
                                                            S_courses[i].getTimes(), S_courses[i].getType(), S_courses[i].getDetails(),
                                                            S_courses[i].getCourseNum(), this);
            }
            schedule.Update(clear,clear,clear);
            schedule.setScheduledCourses(SCourses);
            schedule.Update(stColor1,stColor2,stColor3);
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
                    if ((allCourses[i].getCourseNum()== courseNum || allCourses[i].getCourseNum() / 100 == courseNum 
                        || allCourses[i].getCourseNum() / 10 == courseNum) && !(allCourses[i].getisWaitlised()))
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
                        //if (allCourses[i].getisWaitlised())
                        //{
                        //    temp.triangle.Visibility = System.Windows.Visibility.Visible;
                        //    temp.square.Visibility = System.Windows.Visibility.Hidden;
                        //    temp.WaitlistBtn.Visibility = System.Windows.Visibility.Hidden;
                        //}
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
                         && allCourses[i].getType() == "Lec" && !(allCourses[i].getisWaitlised()))
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
                   
                    if ((allCourses[i].getCourseNum() == courseNum || allCourses[i].getCourseNum() / 100 == courseNum || allCourses[i].getCourseNum() / 10 == courseNum) //He's checking for 500, 50, 5
                        && allCourses[i].getType() == "Lec" && !(allCourses[i].getisWaitlised())) 
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
            //Find the course you want to swap into
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

                // Check if full first
                if (S_courses[i].getSeats() == 100)
                {
                    // Full error message
                    conflict = true;
                    MessageBox.Show("Sorry, the class you are trying to Swap into is already full.");
                }
                else
                {
                    while (k < SCourses.Length)
                    {
                            // Check for time Conflicts
                            int[] courseTimes = SCourses[k].getTimes();
                            int[] currCourseTimes = S_courses[i].getTimes();

                                if (courseTimes[0] < currCourseTimes[1] && courseTimes[0] >= currCourseTimes[0] && SCourses[k].getCourseName() != swapValueSelected)
                                {
                                    conflict = true;
                                    MessageBox.Show("Error. Time Conflict detected with " + SCourses[k].getCourse() + " " + SCourses[k].getCourseNum() + " " +SCourses[k].getCourseName());
                                }
                                else if (courseTimes[1] > currCourseTimes[0] && courseTimes[1] <= currCourseTimes[1] && SCourses[k].getCourseName() != swapValueSelected)
                                {
                                    conflict = true;
                                    MessageBox.Show("Error. Time Conflict detected with " + SCourses[k].getCourse() + " " + SCourses[k].getCourseNum() + " " + SCourses[k].getCourseName());
                                }                  
                        k++;
                    }
                }

                if (conflict == false)
                {
                    //SCourses[swapIndex] = S_courses[i];
                    k = 0;
                    while (k < allCourses.Length)
                    {
                        if (allCourses[k].getCourseName() == S_courses[i].getCourseName())
                        {
                            SCourses[swapIndex] = allCourses[k];
                            schedule.Update(clear, clear, clear);
                            populateSwap();
                            //populateSwitch();
                            schedule.setScheduledCourses(SCourses);
                            schedule.Update(stColor1, stColor2, stColor3);
                            k = allCourses.Length;
                        }
                        k++;
                    }
                }
            }
            SwapCourses.Children.Clear();
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            bool success;
            if (e.GetPosition(mainGrid).Y > 50 && e.GetPosition(mainGrid).Y < 731  && e.GetPosition(mainGrid).X > 386 && e.GetPosition(mainGrid).X < 1160 && toDrag != null)
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
                    schedule.Update(stColor1,stColor2,stColor3);
                    populateSwap();
                    populateSwitch();

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

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.Handled == true) { return; }
        }

        public void setCurrSelected(CourseBlock currSelected)
        {
            this.currSelected = currSelected;
        }

        // DROP BUTTON
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            schedule.Update(clear, clear, clear);
            schedule.ReColorSelected(clear, clear, clear);
            schedule.setSelected(null);
            this.SCourses = schedule.getScheduledCourses();
            populateSwap();
            populateSwitch();
            schedule.Update(stColor1, stColor2, stColor3);
        }

        //ENROLL BUTTON
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            enrolledCanvas.Visibility = Visibility.Visible;
            enrolledTextBox.Text = "You Have Successfully Enrolled!";
        }

        //For the Error Casses
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.F))
            {
                errorCanvas.Visibility = Visibility.Visible;
                errorTextBox.Text = SCourses[0].getCourse() + " " + SCourses[0].getCourseNum() + " " + SCourses[0].getCourseName() + " has reached it's capacity, Please drop it from your schedule and re-enroll.";
                schedule.makeError(SCourses[0]);
            }
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.P)) 
            {
                errorCanvas.Visibility = Visibility.Visible;
                schedule.makeError(SCourses[0]);
                errorTextBox.Text = "You do not have the required prerequisites to enroll in " + SCourses[0].getCourse() + " " + SCourses[0].getCourseNum() + " " + SCourses[0].getCourseName() +", Please drop this course and re-enroll.";
            }
        }

        // X button on the Error Handler
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            errorCanvas.Visibility = Visibility.Hidden;
            enrolledCanvas.Visibility = Visibility.Hidden;
        }

        // Switch tutorial Combo box
        private void ComboBox_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;

            switchValueSelected = (sender as ComboBox).SelectedItem as string;
            for (int i = 0; i < SCourses.Length; i++)
            {
                if (switchValueSelected == (SCourses[i].getCourse() + SCourses[i].getCourseNum() + " " + SCourses[i].getCourseName()))
                {
                    toSwitch = SCourses[i];
                    i = SCourses.Length;
                }
            }

            if (toSwitch != null)
            {
                int numToSearch = toSwitch.getCourseNum();
                for (int i = 0; i < allCourses.Length; i++)
                {
                    if (numToSearch == allCourses[i].getCourseNum() && toSwitch != allCourses[i] && allCourses[i].getType() != "Lec")
                    {
                        SwapCourseBlock temp = new SwapCourseBlock(allCourses[i].getSeats(), allCourses[i].getWaitSeat(), allCourses[i].getProf(),
                                                            allCourses[i].getCourse(), allCourses[i].getCourseName(), allCourses[i].getDays(),
                                                            allCourses[i].getTimes(), allCourses[i].getType(), allCourses[i].getDetails(),
                                                            allCourses[i].getCourseNum(), this, S_courses);
                        switchStack.Children.Add(temp);
                        Array.Resize(ref S_courses, S_courses.Length + 1);
                        S_courses[S_courses.Length - 1] = temp;
                        
                    }
                }
            }

        }
    }
}
