using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Restaurant
{
    public class Restaurant
    {
        public Size RestaurantSize = new Size(9, 7);
        public Table[] Tables = new Table[6];
        public Point[] TableForFood = new Point[7]
        {
            new Point(1, 0),
            new Point(2, 0),
            new Point(3, 0),
            new Point(4, 0),
            new Point(5, 0),
            new Point(6, 0),
            new Point(7, 0),
        };
        public List<Guest> GuestsList = new List<Guest>();
        //public TableState DishInHand = new TableState();
        public Waiter Waiter;
        DispatcherTimer timer;

        public Restaurant()
        {
            Waiter = new Waiter(this);
            StartTimer();
        }

        public void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (sender, args) =>
            {
                var g = new Guest(this);
                if (g.TryTakeTable())
                {
                    GuestsList.Add(g);
                    var rnd = new Random();
                    //var guest = GetImage(Textures.GuestImages[rnd.Next(0, 5)]);
                    //g.GuestImage = guest;
                    //guest.MouseMove += (sender1, args1) =>
                    //{
                    //    if (g.Order != TableState.EmptyTable)
                    //    {
                    //        OutputOrder(GetImage(Textures.Dish[(int)g.Order - 1]));
                    //        OutputLabel(Textures.DishName[(int)g.Order - 1]);
                    //        OutputCounter();
                    //    }
                    //};
                    //guest.MouseLeave += (sender2, args2) =>
                    //{
                    //    OrderImage = new Image();
                    //    DishName = new Label();
                    //};
                    //Draw(guest, Tables[g.NumberOfTable].Position);
                    Tables[g.NumberOfTable].IsOccupated = true;
                    g.SetTimer();
                }
            };
            timer.Start();
        }

        public void RemoveGuest(Guest guest)
        {
            guest.TimerForOrder.Stop();
            GuestsList.Remove(guest);
            Rating.UpdateRating(Tables[guest.NumberOfTable], guest);
            CleanTable(Tables[guest.NumberOfTable]);
            //CleanTableImage(guest);
            //OutputStars();
        }


        public static void CleanTable(Table table)
        {
            table.FoodOnTable = TableState.EmptyTable;
            table.IsOccupated = false;
            table.Served = false;
        }

    }
}
