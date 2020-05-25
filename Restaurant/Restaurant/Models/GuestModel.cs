using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Restaurant
{
    public class GuestModel
    {
        public TableState Dish;
        public Point Position;


        public GuestModel(Point pos)
        {
            Position = pos;
        }
    }
}
