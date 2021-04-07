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

namespace shooting
{
    public abstract class Target: IShootable
    {
        protected Point centre;

        public Target(Point centre)
        {
            this.centre = centre;
        }

        public abstract bool Hit(Point click);
        public abstract void Show(Window window, Grid grid);
    }
}
