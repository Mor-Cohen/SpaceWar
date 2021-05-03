using System;
using System.Windows.Threading;

namespace SpaceWar.Classes
{
    public abstract class Update
    {
        public DispatcherTimer dispatcherTimer;

        public Update()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(20);
            dispatcherTimer.Start();
        }
        abstract public void dispatcherTimer_Tick(object sender, EventArgs e);

    }
}
