using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    class TablesInf
    {
        static public Tuple<bool, int>[] Tables = new Tuple<bool, int>[6];
        // Tuple<занят ли стол\ что на столе> 

        public static void CreateTables()
        {
            for(var i = 0; i < Tables.Length; i++)
            {
                Tables[i] = Tuple.Create(false, (int)TableState.EmptyTable);
            }
        }
    }
}
