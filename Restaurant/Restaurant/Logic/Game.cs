using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace Restaurant
{
    public class Game : IInterpretatable
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
        public Waiter Waiter;
        DispatcherTimer timer;

        public Queue<EventData> EventQueue { get; } = new Queue<EventData>();
        public Dictionary<Event, Action<EventData>> Actions { get; }

        public Game()
        {
            CreateTables();
            Waiter = new Waiter(this);
            Actions = new Dictionary<Event, Action<EventData>>
            {
                { Event.PressedE, (e)=> Waiter.InteractWithTables() },
                { Event.PressedW, (e)=> Waiter.MakeSteps(0, -1) },
                { Event.PressedA, (e)=> Waiter.MakeSteps(-1, 0) },
                { Event.PressedD, (e)=> Waiter.MakeSteps(1, 0) },
                { Event.PressedS, (e)=> Waiter.MakeSteps(0, 1) }
            };
            StartTimer();
        }

        public void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(8);
            timer.Tick += (sender, args) =>
            {
                var g = new Guest(this);
                if (g.TryTakeTable())
                {
                    GuestsList.Add(g);
                    Tables[g.NumberOfTable].IsOccupated = true;
                    EventQueue.Enqueue(new EventData(Event.GuestArrived, new List<object> {Tables[g.NumberOfTable].Position}));
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
            for (var i = 0; i < defaultTablesPosition.Length; i++)
            {
                Tables[i] = new Table(defaultTablesPosition[i], TableState.EmptyTable);
                EventQueue.Enqueue(new EventData(Event.CreatedTable, new List<object> { defaultTablesPosition[i] }));
            }
        }

        public void RemoveGuest(Guest guest)
        {
            guest.TimerForOrder.Stop();
            EventQueue.Enqueue(new EventData(Event.GuestGone, new List<object> { Tables[guest.NumberOfTable].Position }));
            GuestsList.Remove(guest);
            Rating.UpdateRating(Tables[guest.NumberOfTable], guest);
            CleanTable(Tables[guest.NumberOfTable]);
            EventQueue.Enqueue(new EventData(Event.RatingUpdated, new List<object> { Rating.Grade, Rating.CountRating }));
            if (Rating.Grade == 0) EventQueue.Enqueue(new EventData(Event.FinishGame));
        }


        public static void CleanTable(Table table)
        {
            table.FoodOnTable = TableState.EmptyTable;
            table.IsOccupated = false;
            table.Served = false;
        }
    }
}
