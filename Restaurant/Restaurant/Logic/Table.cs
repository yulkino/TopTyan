using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Restaurant
{
    public class Table
    {
        public Point Position = new Point();
        public bool Occupated;
        public bool Served;
        public TableState FoodOnTable;

        public Table(Point position, TableState foodOnTable)
        {
            Position = position;
            FoodOnTable = foodOnTable;
        }
    }
}
