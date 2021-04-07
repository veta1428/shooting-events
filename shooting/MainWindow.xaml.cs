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
//using System.Windows.Shapes;

namespace shooting
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool firstTime = true;
        public MainWindow()
        {
            InitializeComponent();

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Activated(object sender, EventArgs e)
        {
           

        }

        private void MainForm_Loaded(object sender, RoutedEventArgs e)
        {
            Height = 700;
            Width = 1000;
            
        }

        private void RadEnter_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void RadEnter_TextInput(object sender, TextCompositionEventArgs e)
        {
            
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (firstTime == false)
            {
                ShowMistake.Content = "";
                
            }
            else
            {
                firstTime = false;
            }
           
        }

        private void CheckBounds(int radius)
        {
            if (radius > 445 || radius < 20)
            {
                throw new Exception();
            }
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {
            int radius = 0;
            try
            {
                radius = Convert.ToInt32(textBox.Text);

                CheckBounds(radius);

                MinHeight = radius * 2 + 40;
                MinWidth = radius * 2 + 40;
                Target11 tard = new Target11(new Point(ActualWidth / 2, ActualWidth / 2), radius);
                tard.Show(this, grid);
            }
            catch
            {
                ShowMistake.Content = "Input mistake!";
            }
       
        }

        private Point CoordTransforFromGlobal(Point global)
        {
            return new Point(global.X - ActualWidth / 2, global.Y - ActualHeight / 2);

        }


        private void MainForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point shot = e.GetPosition(MainForm);

            Ellipse circle = new Ellipse();
            circle.Height = 5;
            circle.Width = 5;
            Point coord = CoordTransforFromGlobal(shot);
            circle.Margin = new Thickness(2 * coord.X + 9, 2 * coord.Y + 34, 0, 0);
            circle.Fill = Brushes.Red;
            grid.Children.Add(circle);

        }    
    }
}
