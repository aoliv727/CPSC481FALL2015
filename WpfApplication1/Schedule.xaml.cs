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
    /// Interaction logic for Schedule.xaml
    /// </summary>
    public partial class Schedule : UserControl
    {
        private MainWindow screen;

        public Schedule(MainWindow screen)
        {
            InitializeComponent();
            this.screen = screen;
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            /*
            base.OnDrop(e);

            // Add Check if course is valid first
            // when dropped do things
            if (e.KeyStates.HasFlag(DragDropKeyStates.ControlKey))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.Move;
            }
            e.Handled = true;
             */
           CourseBlock toDrop = (CourseBlock)sender;
            //CourseBlock _element = (CourseBlock)e.Data.GetData("Object");
            UIElement _element = (UIElement)e.Data.GetData("Object");
            StackPanel _parent = (StackPanel)VisualTreeHelper.GetParent(_element);

            if (e.AllowedEffects.HasFlag(DragDropEffects.Move))
            {
                _parent.Children.Remove(_element);
                //_panel.Children.Add(_element);
                // set the value to return to the DoDragDrop call
                e.Effects = DragDropEffects.Move;
            }
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            /*
            base.OnDragOver(e);
            e.Effects = DragDropEffects.None;

                    // Set Effects to notify the drag source what effect
                    // the drag-and-drop operation will have. These values are 
                    // used by the drag source's GiveFeedback event handler.
                    // (Copy if CTRL is pressed; otherwise, move.)
                    if (e.KeyStates.HasFlag(DragDropKeyStates.ControlKey))
                    {
                        e.Effects = DragDropEffects.Copy;
                    }
                    else
                    {
                        e.Effects = DragDropEffects.Move;
                    }           
            e.Handled = true;
            */
            if (e.Data.GetDataPresent("Object"))
            {
                // These Effects values are used in the drag source's
                // GiveFeedback event handler to determine which cursor to display.
                if (e.KeyStates == DragDropKeyStates.ControlKey)
                {
                    e.Effects = DragDropEffects.Copy;
                }
                else
                {
                    e.Effects = DragDropEffects.Move;
                }
            }
        }
    }
}
