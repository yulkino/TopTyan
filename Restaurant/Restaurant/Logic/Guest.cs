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
        public Game Environment; 
        public TableState Order;
        public int NumberOfTable;
        public bool IsOrderAccepted;
        public DispatcherTimer TimerForOrder = new DispatcherTimer();

        public Guest(Game rest)
        {
            Environment = rest;
        }

        public void SetTimer()
        {
            TimerForOrder.Interval = TimeSpan.FromSeconds(10);
            TimerForOrder.Tick += (sender, args) =>
            {
                if (IsOrderAccepted)
                {
                    TimerForOrder.Stop();
                    TimerForOrder = new DispatcherTimer();
                    TimerForOrder.Interval = TimeSpan.FromSeconds(10);
                    TimerForOrder.Tick += (sender1, args1) => Environment.RemoveGuest(this);
                    TimerForOrder.Start();
                }
                else
                    Environment.RemoveGuest(this);
            };
            TimerForOrder.Start();
        }

        public bool TryTakeTable()
        {
            for (var i = 0; i < Environment.Tables.Length; i++)
                if (!Environment.Tables[i].IsOccupated)
                {
                    NumberOfTable = i;
                    Environment.Tables[NumberOfTable].IsOccupated = true;
                    return true;
                }
            return false;
        }

        public void OrderFood()
        {
            Order = (TableState)new Random().Next(1, 7);
            Environment.EventQueue.Enqueue(new EventData(Event.OrderAccepted, new List<object> { Environment.Tables[NumberOfTable].Position, Order }));
        }
    }
}
