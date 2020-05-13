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
            MainWindow.CleanTable(MainWindow.Tables[NumberOfTable]);
            MainWindow.CleanTableImage(window, GuestImage, DishImage);
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
