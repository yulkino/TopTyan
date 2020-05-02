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

        public static bool IsTableOccupated(int numberOfTable)
        {
            return TablesInf.Tables[numberOfTable].Item1;
        }

    }
}
