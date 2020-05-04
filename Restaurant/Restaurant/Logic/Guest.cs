using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    class Guest
    {
        public TableState Order;
        public int NumberOfTable;

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
