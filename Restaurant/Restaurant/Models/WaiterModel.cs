using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Restaurant
{
    public class WaiterModel
    {
        public TableState DishInHand;
        public Point Position;

        public WaiterModel(TableState dishInHand, Point position)
        {
            DishInHand = dishInHand;
            Position = position;
        }
    }
}
