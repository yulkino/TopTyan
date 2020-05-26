using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

namespace Restaurant
{
    public class Guest
    {
        Game Environment; 
        public TableState Order { get; set; }
        public int NumberOfTable { get; set; }
        public bool IsOrderAccepted { get; set; }
        DispatcherTimer TimerForOrder = new DispatcherTimer();

        public Guest(Game rest)
        {
            Environment = rest;
            SetTimer();
        }

        void SetTimer()
        {
            TimerForOrder.Interval = TimeSpan.FromSeconds(20);
            TimerForOrder.Tick += (sender, args) =>
            {
                if (IsOrderAccepted)
                {
                    TimerForOrder.Stop();
                    TimerForOrder = new DispatcherTimer();
                    TimerForOrder.Interval = TimeSpan.FromSeconds(20);
                    TimerForOrder.Tick += (sender1, args1) =>
                    {
                        Environment.RemoveGuest(this);
                        TimerForOrder.Stop();
                    };
                    TimerForOrder.Start();
                }
                else
                {
                    Environment.RemoveGuest(this);
                    TimerForOrder.Stop();
                }
            };
            TimerForOrder.Start();
        }

        public bool TryTakeTable()
        {
            var freeTables = Environment.Tables.Where(f => !f.IsOccupated).ToArray();
            if (freeTables.Length == 0) return false;
            var table = freeTables[new Random().Next(0, freeTables.Length)];
            NumberOfTable = Array.IndexOf(Environment.Tables, Environment.Tables.FirstOrDefault(p => p.Position == table.Position));
            Environment.Tables[NumberOfTable].IsOccupated = true;
            return true;
        }

        public void OrderFood()
        {
            Order = (TableState)new Random().Next(1, 8);
            Environment.EventQueue.Enqueue(new EventData(Event.OrderAccepted, new List<object> { Environment.Tables[NumberOfTable].Position, Order }));
        }
    }
}
