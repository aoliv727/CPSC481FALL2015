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
    /// Interaction logic for CourseBlock1.xaml
    /// </summary>
    public partial class CourseBlock1 : UserControl
    {
        private int seats;
        private int waitSeat;
        private String prof;
        private String course;
        private String courseName;
        private String days;
        private String[] times;
        private String type;

        public CourseBlock1(int seats, int waitSeat, String prof, String course, String courseName, String days, String[] times, String type)
        {
            this.seats = seats;
            this.waitSeat = waitSeat;
            this.prof = prof;
            this.course = course;
            this.courseName = courseName;
            this.days = days;
            this.times = times;
            this.type = type;

            courselbl.Content = course;
            courseNamelbl.Content = courseName;
            typelbl.Content = type;
            proflbl.Content = prof;
            timeslbl.Content = days + " ";
            //If there's less than 100 in seats then put green circle in statuslbl and make btn not visible
            //Else if seats = 100 put blue square in statuslbl and if waitlist < 10 then make btn visible else not visible

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
