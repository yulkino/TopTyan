﻿using System;
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

        public bool TryTakeTable()
        {
            for (var i = 0; i < TablesInf.Tables.Length; i++)
            {
                if (TablesInf.Tables[i].Item1 == false)
                {
                    NumberOfTable = i;
                    TablesInf.Tables[i] = Tuple.Create(true, TablesInf.Tables[i].Item2);
                    return true;
                }                
            }
            return false;
        }

        public void OrderDish()
        {
            if (Table.IsTableBusy(NumberOfTable))
            {
                var rndDish = new Random();
                TablesInf.Tables[NumberOfTable] = Tuple.Create(true, rndDish.Next(1, 7));
            }
        }
    }
}
