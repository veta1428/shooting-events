using shooting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using static events.Publisher;

namespace events
{
    public delegate void Shot(Point coord);
    class Subscriber
    {
        

        private ActionsToDelannoy PrintToControl;
        private Action Resize;
        private Window window;
        private int sleepingTime;
        private Point topRight;
        private Point bottomLeft;
        private MyTarget target;
        private bool shouldWork = true;
        private Shot Shot;

        public Subscriber(ActionsToDelannoy print, Action resize, Window window, int Radius, int sleepingTime, Point topRight, Point bottomLeft, MyTarget target, Shot shot)
        {
            this.PrintToControl = print;
            this.Resize = resize;
            this.window = window;
            this.target = target;
            this.target.Radius = Radius;
            this.topRight = topRight;
            this.bottomLeft = bottomLeft;
            this.sleepingTime = sleepingTime;
            this.Shot = shot;
        }

        public void Shooting()
        {
            int x;
            int y;
            Random rnd = new Random();
            shouldWork = true;
            while (shouldWork)
            {
                
                x = rnd.Next((int)topRight.X, (int)bottomLeft.X);
                y = rnd.Next((int)topRight.Y, (int)bottomLeft.Y);
                Shot(new Point(x, y));
                Thread.Sleep(sleepingTime);
            }
        }

        public void ShouldStopWorking()
        {
            shouldWork = false;
        }

        public delegate void EndWork();

        public event EndWork EndCalculating;

        public void Print(int n, int m, int delannoy)
        {
            if (!window.Dispatcher.CheckAccess())
            {
                window.Dispatcher.BeginInvoke(new Publisher.ActionsToDelannoy(Print), n, m, delannoy);
                return;
            }
            PrintToControl(n, m, delannoy);
            Resize();
        }

        public void EndWorking()
        {
            EndCalculating();
        }
    }
}
