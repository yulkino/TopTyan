using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Restaurant
{
    static class DefaultParams
    {
        public static Point[] TableForFood = new Point[7]
        {
            new Point(1, 0),
            new Point(2, 0),
            new Point(3, 0),
            new Point(4, 0),
            new Point(5, 0),
            new Point(6, 0),
            new Point(7, 0),
        };
        public static Point[] TablesPosition = new Point[6]
        {
            new Point(4, 3),
            new Point(2, 2),
            new Point(6, 2),
            new Point(1, 4),
            new Point(4, 5),
            new Point(7, 4)
        };
    }
}
