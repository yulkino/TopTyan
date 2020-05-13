using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Restaurant
{
    public class Guest
    {
        public TableState Order;
        public int NumberOfTable;
        public bool AcceptOrder;
        DispatcherTimer TimerForOrder = new DispatcherTimer();

        public void GuestTimer()
        {
            TimerForOrder.Interval = TimeSpan.FromSeconds(10);
            TimerForOrder.Tick += (sender, args) =>
            {
                if (AcceptOrder)
                {
                    TimerForOrder = new DispatcherTimer();
                    TimerForOrder.Interval = TimeSpan.FromSeconds(10);
                    TimerForOrder.Tick += (sender1, args1) => RemoveThisGuest();
                    TimerForOrder.Start();
                }
                else
                    RemoveThisGuest();
            };
            TimerForOrder.Start();
        }

        public void RemoveThisGuest()
        {
            TimerForOrder.Stop();
            Guests.GuestsList.Remove(this);
            Rating.UpdateRating(MainWindow.Tables[NumberOfTable], this);
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
