using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace Restaurant
{
    public class Game : IInterpretatable
    {
        public Size RestaurantSize { get; } = new Size(9, 7);
        public Table[] Tables { get; set; } = new Table[6];
        public List<Guest> GuestsList { get; set; }  = new List<Guest>();
        Waiter Waiter;
        DispatcherTimer timer;

        public Queue<EventData> EventQueue { get; } = new Queue<EventData>();
        public Dictionary<Event, Action<EventData>> Actions { get; }

        public Game()
        {
            EventQueue.Enqueue(new EventData(Event.CreatedTablesForFood, new List<object> { DefaultParams.TableForFood }));
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

        void StartTimer()
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
                }
            };
            timer.Start();
        }

        void CreateTables()
        {
            for (var i = 0; i < DefaultParams.TablesPosition.Length; i++)
            {
                Tables[i] = new Table(DefaultParams.TablesPosition[i], TableState.EmptyTable);
                EventQueue.Enqueue(new EventData(Event.CreatedTable, new List<object> {DefaultParams.TablesPosition[i] }));
            }
        }

        public void RemoveGuest(Guest guest)
        {
            EventQueue.Enqueue(new EventData(Event.GuestGone, new List<object> { Tables[guest.NumberOfTable].Position }));
            GuestsList.Remove(guest);
            Rating.UpdateRating(Tables[guest.NumberOfTable], guest);
            Tables[guest.NumberOfTable].Clean();
            EventQueue.Enqueue(new EventData(Event.RatingUpdated, new List<object> { Rating.Grade, Rating.CountRating }));
            if (Rating.Grade == 0) EventQueue.Enqueue(new EventData(Event.FinishGame));
        }
    }
}
