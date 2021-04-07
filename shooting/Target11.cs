using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace shooting
{
    public sealed class Target11 : Target
    {
        private int radius;
        public Target11(Point centre, int radius): base(centre)
        {
            this.radius = radius;
        }

        public override bool Hit(Point click)
        {
            throw new NotImplementedException();
        }

        public override void Show(Window window, Grid grid)
        {
            grid.Height = window.ActualHeight;
            grid.Width = window.ActualWidth;
            grid.Margin = new Thickness(0, 0, 0, 0);

            Ellipse circle = new Ellipse();
            circle.Height = radius * 2;
            circle.Width = circle.Height;
            circle.HorizontalAlignment = HorizontalAlignment.Center;
            circle.VerticalAlignment = VerticalAlignment.Center;
            circle.Fill = Brushes.Gray;
            grid.Children.Add(circle);

            Rectangle square2 = new Rectangle();
            square2.Height = radius;
            square2.Width = radius;
            square2.Margin = new Thickness(-radius, -radius, 0, 0);
            square2.Fill = Brushes.White;           
            grid.Children.Add(square2);

            Rectangle square4 = new Rectangle();
            square4.Height = radius;
            square4.Width = radius;
            square4.Margin = new Thickness(radius, radius, 0, 0);
            square4.Fill = Brushes.White;
            grid.Children.Add(square4);

            Polygon triangle = new Polygon();
            triangle.HorizontalAlignment = HorizontalAlignment.Center;
            triangle.VerticalAlignment = VerticalAlignment.Center;
            PointCollection pc = new PointCollection() { new Point(- radius, 2 * radius), new Point(-radius, radius ), new Point(0, 2* radius) };
            triangle.Points = pc;
            triangle.Fill = Brushes.White;
            grid.Children.Add(triangle);
        }
    }
}
