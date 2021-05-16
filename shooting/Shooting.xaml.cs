using events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static events.Publisher;

namespace shooting
{
    /// <summary>
    /// Логика взаимодействия для Shooting.xaml
    /// </summary>
    public partial class Shooting : Window
    {
        private int Radius;
        private int succeededhits = 0;
        private int allHits = 0;

        Subscriber sub;
        Publisher publisher;
        Thread calculating;
        Thread shooting;

        public Shooting(int radius)
        {            
            InitializeComponent();
            Radius = radius;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.MinHeight = this.Height;
            this.MinWidth = this.MinHeight;

            gridforshooting.Height = 2 * Radius;
            gridforshooting.Width = 2 * Radius;           

            targety.Radius = Radius;
            targety.Centre = new Point(gridforshooting.Width / 2, gridforshooting.Height / 2);

            sub = new Subscriber(PrintInfo, ResizeControl, this, Radius, 900, new Point(0, 0), new Point(gridforshooting.Width, gridforshooting.Height), targety, Shot);
            publisher = new Publisher(1000);
            publisher.DelannoyIsReady += new ActionsToDelannoy(sub.Print);
            sub.EndCalculating += new Subscriber.EndWork(publisher.ShoulStopWorking);
            sub.EndCalculating += new Subscriber.EndWork(sub.ShouldStopWorking);
        }

        private void Shot(Point coord)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.BeginInvoke(new Shot(Shot), coord);
                return;
            }
            Ellipse circle = new Ellipse();
            circle.Height = 5;
            circle.Width = 5;
            circle.Fill = Brushes.Black;
            circle.VerticalAlignment = VerticalAlignment.Top;
            circle.HorizontalAlignment = HorizontalAlignment.Left;
            circle.Margin = new Thickness(coord.X - 2.5, coord.Y - 2.5, 0, 0);
            gridforshooting.Children.Add(circle);
            allhitsnumber.Content = ++allHits;
            if (targety.PointOnTarget(coord))
            {
                hitsnumber.Content = ++succeededhits;
            }
        }

        private void PrintInfo(int n, int m, int delannoy)
        {
            infoN.Content = infoN.Content.ToString() + n.ToString() + '\n';
            infoM.Content = infoM.Content.ToString() + m.ToString() + '\n';
            infoD.Content = infoD.Content.ToString() + delannoy.ToString() + '\n';
        }

        private void ResizeControl()
        {
            gridforlabels.Height += 22;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            calculating = new Thread(new ThreadStart(publisher.StartWorking));
            shooting = new Thread(new ThreadStart(sub.Shooting));
            calculating.Start();
            shooting.Start();
            ButtonsOnStart();
        }

        private void ButtonsOnStart()
        {
            start.IsEnabled = false;
            stop.IsEnabled = true;
            stopShooting.IsEnabled = true;
        }

        private void ButtonsOnStop()
        {
            start.IsEnabled = true;
            stop.IsEnabled = false;
            stopShooting.IsEnabled = false;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {         
            sub.EndWorking();
            ButtonsOnStop();
        }

        private void stopShooting_Click(object sender, RoutedEventArgs e)
        {
            Stop_Click(sender, e);
        }
    }
}