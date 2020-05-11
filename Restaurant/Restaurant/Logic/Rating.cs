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
        public static int SumRating = 0;
        public static int CountRating = 0;

        public static  void UpdateRating(Table table, Guest guest)
        {
            var rnd = new Random();
            CountRating++;
            if (table.FoodOnTable == guest.Order)
                SumRating += rnd.Next(3, 5);
            else
                SumRating += rnd.Next(0, 2);
        }
    }
}
