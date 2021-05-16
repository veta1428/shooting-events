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
        public MainWindow()
        {
            InitializeComponent();     
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {          
                showmistake.Content = " ";
        }

        private void CheckBounds(int radius)
        {
            if (radius > 200 || radius < 20)
            {
                throw new Exception();
            }
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {
            Tuple<int, bool> info = TryToParse();
            InfoOrNewWindow(info);              
        }

        private void InfoOrNewWindow(Tuple<int, bool> info)
        {
            if (info.Item2 == false)
            {
                showmistake.Content = "Wrong input!";
            }
            else
            {
                Shooting SecondForm = new Shooting(info.Item1);                
                SecondForm.Show();
                MainForm.Close();
            }
        }

        private Tuple<int, bool> TryToParse()
        {
            int radius = 0;
            try
            {
                radius = Convert.ToInt32(textbox.Text);
                CheckBounds(radius);    
            }
            catch
            {
                return Tuple.Create(0, false);
            }

            return Tuple.Create(radius, true);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Tuple<int, bool> info = TryToParse();
                InfoOrNewWindow(info);
            }
        }


    }
}