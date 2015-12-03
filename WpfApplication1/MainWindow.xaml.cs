﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
            fileReader();
            for (int i = 0; i < allCourses.Length; i++)
            {
                this.courses.Children.Add(allCourses[i]);
            }
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
                allCourses[i] = new CourseBlock(seats, waitSeat, prof, course, courseName, days, times, type, details, courseNum);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
                    if (allCourses[i].getCourseNum()== courseNum || allCourses[i].getCourseNum() / 100 == courseNum || allCourses[i].getCourseNum() / 10 == courseNum)
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
                                                            allCourses[i].getCourseNum());
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

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.SwapCourses.Children.Clear();
            int j = 0;
            int k = 0;
            string course = SubjectSwap.Text;
            int courseNum;
            int.TryParse(courseNumSwap.Text, out courseNum);
            CourseBlock[] S_courses;
            for (int i = 0; i < allCourses.Length; i++)
            {
                if (allCourses[i].getCourse() == course)
                {
                    if (allCourses[i].getCourseNum() == courseNum || allCourses[i].getCourseNum() / 100 == courseNum || allCourses[i].getCourseNum() / 10 == courseNum)
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
                                                            allCourses[i].getCourseNum());
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
    }
}
