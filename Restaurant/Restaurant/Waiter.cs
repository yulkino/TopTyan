using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    class Waiter
    {
        private static int[] NoteBook = new int[4];

        public Waiter(TableState tableState) 
        {
            var i = 0;
            while(i < 4)
            if (NoteBook[i] == 0)
                NoteBook[i] = (int)tableState;
            else
                i++;
        }
    }
}
