using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    class Guest
    {
        public int Order;
        public int NumberOfTable;
        public bool IsGuestServed;

        public bool TryTakeTable()
        { 
            for (var i = 0; i < TablesInf.Tables.Length; i++)
                if (!Table.IsTableOccupated(i))
                {
                    NumberOfTable = i;
                    TablesInf.Tables[NumberOfTable] = Tuple.Create(true, (int)TableState.EmptyTable);
                    //item 2 = TablesInf.Tables[NumberOfTable].Item2
                    return true;
                }
            return false;
        }

        public void OrderFood()
        {
            if (TablesInf.Tables[NumberOfTable].Item1)
            {
                var rndDish = new Random();
                Order = rndDish.Next(1, 7);
                TablesInf.Tables[NumberOfTable] = Tuple.Create(true, Order);
                IsGuestServed = true;
            }
        }
    }
}
