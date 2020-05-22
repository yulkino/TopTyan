using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Restaurant
{
    public class Game
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

        public Game()
        {
            CreateTables();
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
                    Tables[g.NumberOfTable].IsOccupated = true;
                    g.SetTimer();
                }
            };
            timer.Start();
        }

        public void CreateTables()
        {
            Point[] defaultTablesPosition = new Point[6]
    {
                new Point(4, 3),
                new Point(2, 2),
                new Point(6, 2),
                new Point(1, 4),
                new Point(4, 5),
                new Point(7, 4)
    };
            for(var i = 0; i < defaultTablesPosition.Length; i++)
                Tables[i] = new Table(defaultTablesPosition[i], TableState.EmptyTable);
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
