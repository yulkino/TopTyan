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
        public static int SumRating = 5;
        public static int CountRating = 1;

        public static  void UpdateRating(Table table, Guest guest)
        {
            var rnd = new Random();
            CountRating++;
            if (table.Served && table.FoodOnTable == guest.Order) SumRating += rnd.Next(4, 5);
            else if (!table.Served)
                SumRating += 0;
            else SumRating += rnd.Next(1, 3);
        }
    }
}
