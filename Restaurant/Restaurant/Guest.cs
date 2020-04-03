using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    class Guest
    {
        public static void TryToOrderDish()
        {
            int dish;
            for(var i = 0; i < 7; i++)
            {
                dish = InformationAboutTables.TakeTableAndOrderDish(i);
            }
        }
    }
}
