using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class InformationAboutTables
    {
        static public int[] Tables = new int[6];

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
            for (var i = 0; i < 9; i++)
            {
                Tables[i] = 8;
            }
        }

        public bool IsTableServed(TableState tableState)
        {
            return !(tableState == TableState.EmptyTable);
        }
    }


}
