using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace shooting
{
    public class MyTarget : Shape
    {        
        public PathGeometry my_figure = new PathGeometry();

        public MyTarget() : base() { }
     
        public MyTarget(Point centre, int radius) : base()
        {         
            Radius = radius;
            Centre = centre;
        }

        public Int32 Radius
        {
            get { return (Int32)this.GetValue(RadiusProperty); }
            set { this.SetValue(RadiusProperty, value);}          
        }

        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
            "Radius", typeof(Int32), typeof(MyTarget), new PropertyMetadata(100));

        public Point Centre
        {
            get { return (Point)this.GetValue(CentreProperty); }
            set { this.SetValue(CentreProperty, value); }
        }

        public static readonly DependencyProperty CentreProperty = DependencyProperty.Register(
            "Centre", typeof(Point), typeof(MyTarget), new PropertyMetadata(new Point(100, 100)));

        private void MakeGeometry()
        {
            List<PathFigure> figures = new List<PathFigure>();
            List<PathSegment> segments = new List<PathSegment>();
            segments.Add(new LineSegment(new Point(Centre.X, Centre.Y + Radius), true));
            segments.Add(new LineSegment(new Point(Centre.X - Radius, Centre.Y), true));
            segments.Add(new LineSegment(new Point(Centre.X + Radius, Centre.Y), true));
            segments.Add(new ArcSegment(new Point(Centre.X, Centre.Y - Radius), new Size(Radius, Radius), 0, false, System.Windows.Media.SweepDirection.Counterclockwise, true));

            PathFigure figure = new PathFigure(new Point(Centre.X, Centre.Y - Radius), segments, true);
            figures.Add(figure);
            my_figure = new PathGeometry(figures);
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                MakeGeometry();
                return my_figure;
            }
        }

        private static Point TransformToLocal(Point absoluteCoord, MyTarget target)
        {
            return new Point(absoluteCoord.X - target.Centre.X, absoluteCoord.Y - target.Centre.Y);
        }

        private static double DestanceToCentre(Point locals)
        {
            return Math.Sqrt(Math.Pow(locals.X, 2) + Math.Pow(locals.Y, 2));
        }       

        public bool PointOnTarget(Point parent)
        {
            Point locals = new Point(parent.X - Centre.X, parent.Y - Centre.X);
            if ((locals.X < 0 && locals.Y < 0) || (locals.X > 0 && locals.Y > 0))
            {
                return false;
            }
            else if (locals.X > 0 && locals.Y < 0)
            {
                if (DestanceToCentre(locals) > Radius)
                {
                    return false;
                }
            }
            else if (locals.Y > locals.X + Radius)
            {
                return false;
            }

            return true;
        }
    }
}