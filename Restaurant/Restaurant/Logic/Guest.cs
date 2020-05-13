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
        //DispatcherTimer TimerForOrder = new DispatcherTimer();

        //public void GuestTimer()
        //{
        //    var stages = 1;
        //    TimerForOrder.Interval = TimeSpan.FromSeconds(10);
        //    TimerForOrder.Tick += (sender, args) =>
        //    {
        //        if(stages == 1)
        //        {
        //            if(Table)
        //            TimerForOrder = new DispatcherTimer();
        //            stages++;
        //        }
        //        if (stages == 2)
        //        {
        //            TimerForOrder.Stop();
        //        }
        //    };
        //    TimerForOrder.Start();
        //}

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
