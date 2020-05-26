using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    static class Rating
    {
        public static int Grade { get => CountRating == 0 ? 0 : SumRating / CountRating; }
        public static int SumRating { get; private set; } = 5;
        public static int CountRating { get; private set; } = 1;

        public static  void UpdateRating(Table table, Guest guest)
        {
            if (Grade < 0) return;
            CountRating++;
            if (table.Served && table.FoodOnTable == guest.Order) SumRating += new Random().Next(4, 5);
            else if (!table.Served) SumRating += 0;
            else SumRating += new Random().Next(1, 3);
        }
    }
}
