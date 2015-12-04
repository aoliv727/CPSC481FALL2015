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
       // private CourseBlock toDrag;
        private Schedule schedule;


        public MainWindow()
        {
            InitializeComponent();
            schedule = new Schedule(this);
            fileReader();
            for (int i = 0; i < allCourses.Length; i++)
            {
                this.courselist.Children.Add(allCourses[i]);
            }
            scheduleCanvas.Children.Add(schedule);
            Canvas.SetLeft(schedule, 10);
            Canvas.SetTop(schedule, 40);
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

        /*

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (toDrag == null) { return; }

            courselist.Children.Remove(toDrag);
            int gridChildren = mainGrid.Children.Count;

            CourseBlock newToDrag = new CourseBlock(toDrag);
            //mainGrid.Children.Add(newToDrag);
            mainGrid.Children.Insert(gridChildren, newToDrag);

            if (newToDrag.getCaptured())
            {
                Thickness margin = (newToDrag.Margin);
                margin.Left = e.GetPosition(mainGrid).X - (newToDrag.Width / 2);
                margin.Top = e.GetPosition(mainGrid).Y - (newToDrag.Height / 2);
                newToDrag.Margin = margin;
            }
        }

        public void setToDrag(CourseBlock c)
        {
            this.toDrag = c;
        }
        */


    }
}
