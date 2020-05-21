using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Restaurant
{
    public class Guest
    {
        public MainWindow window;
        public TableState Order;
        public int NumberOfTable;
        public bool AcceptOrder;
        public Image GuestImage;
        public Image DishImage;
        public DispatcherTimer TimerForOrder = new DispatcherTimer();

        public Guest(MainWindow mainWindow)
        {
            window = mainWindow;
        }

        public void GuestTimer()
        {
            TimerForOrder.Interval = TimeSpan.FromSeconds(5);
            TimerForOrder.Tick += (sender, args) =>
            {
                if (AcceptOrder)
                {
                    TimerForOrder.Stop();
                    TimerForOrder = new DispatcherTimer();
                    TimerForOrder.Interval = TimeSpan.FromSeconds(15);
                    TimerForOrder.Tick += (sender1, args1) => window.RemoveThisGuest(this);
                    TimerForOrder.Start();
                }
                else
                    window.RemoveThisGuest(this);
            };
            TimerForOrder.Start();
        }

        public bool TryTakeTable()
        {
            for (var i = 0; i < MainWindow.Tables.Length; i++)
                if (!MainWindow.Tables[i].IsOccupated)
                {
                    NumberOfTable = i;
                    MainWindow.Tables[NumberOfTable].IsOccupated = true;
                    return true;
                }
            return false;
        }

        public void OrderFood()
        {
            var rndDish = new Random();
            Order = (TableState)rndDish.Next(1, 7);
        }
    }
}
