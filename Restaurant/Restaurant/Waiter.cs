using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    class Waiter
    {
        private static List<int> NoteBook = new List<int>();

        public void Dish(TableState tableState) 
        {
            if(NoteBook.Count <= 4)
            {
                NoteBook.Add((int)tableState);
            }
        }
    }
}
