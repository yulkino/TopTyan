using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class InformationAboutTables
    {
        static public List<Tuple<int, bool, int>> Tables = new List<Tuple<int, bool, int>>();

        public enum TableState
        {
            AsparagusSoup,
            FishSteak,
            Guacamole,
            HoneyRoll,
            HotChili,
            Lobster,
            Meatballs,
            Vareniki,
            EmptyTable
        }

        public static void CreateTables()
        {
            for (var i = 0; i < 7; i++)
            {
                Tables.Add(Tuple.Create(i, false, (int)TableState.EmptyTable));
            }
        }

        public bool IsTableServed(TableState tableState)
        {
            return !(tableState == TableState.EmptyTable);
        }

        public static int TakeTableAndOrderDish(int numberOfTable)
        {
            if (IsTableBusy(numberOfTable))
            {
                var rndDish = new Random();
                Tables[numberOfTable] = Tuple.Create(numberOfTable, true, rndDish.Next(0, 8));
                return Tables[numberOfTable].Item3;
            }
            return default;
        }

        public static bool IsTableBusy(int numberOfTable)
        {
            return (Tables[numberOfTable].Item2 == false);
        }

        public Tuple<int, bool, int> this[int index]
        {
            get
            {
                return Tables[index];
            }
        }
    }
}
