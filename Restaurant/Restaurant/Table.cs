using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Table
    {
        public static bool IsTableServed(TableState tableState)
        {
            return tableState != TableState.EmptyTable;
        }

        public int TakeTableAndOrderDish(int numberOfTable)
        {
            if (!IsTableBusy(numberOfTable))
            {
                var rndDish = new Random();
                TablesInf.Tables[numberOfTable] = Tuple.Create(numberOfTable, true, rndDish.Next(0, 8));
                return TablesInf.Tables[numberOfTable].Item3;
            }
            return default;
        }

        public bool IsTableBusy(int numberOfTable)
        {
            return TablesInf.Tables[numberOfTable].Item2;
        }

        public Tuple<int, bool, int> this[int index]
        {
            get
            {
                return TablesInf.Tables[index];
            }
        }
    }
}
