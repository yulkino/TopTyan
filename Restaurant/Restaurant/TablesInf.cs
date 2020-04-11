using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    class TablesInf
    {
        static public List<Tuple<int, bool, int>> Tables = new List<Tuple<int, bool, int>>();


        public static void CreateTables()
        {
            for (var i = 0; i < 7; i++)
            {
                Tables.Add(Tuple.Create(i, false, (int)TableState.EmptyTable));
            }
        }
    }
}
