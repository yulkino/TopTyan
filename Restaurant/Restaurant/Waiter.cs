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

        public Waiter(Table.TableState tableState) 
        {
            if(NoteBook.Count < 5)
                NoteBook.Add((int)tableState);
        }

        public int this[int index]
        {
            get
            {
                return NoteBook[index];
            }
        }
    }
}
