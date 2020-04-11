using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Table
    {
        public static bool IsTableServed(int numberOfTable)
        {
            return TablesInf.Tables[numberOfTable].Item2 != (int)TableState.EmptyTable;
        }

        public static void TakeTable(int numberOfTable)
        {
            TablesInf.Tables[numberOfTable] = Tuple.Create(true, TablesInf.Tables[numberOfTable].Item2);
        }

        public static int OrderDish(int numberOfTable)
        {

        }

        public static int TakeTableAndOrderDish(int numberOfTable)
        {
            if (!IsTableBusy(numberOfTable))
            {
                var rndDish = new Random();
                TablesInf.Tables[numberOfTable] = Tuple.Create(numberOfTable, true, rndDish.Next(0, 8));
                return TablesInf.Tables[numberOfTable].Item3;
            }
            return default;
        }

        public static bool IsTableBusy(int numberOfTable)
        {
            return TablesInf.Tables[numberOfTable].Item1;
        }

    }
}
