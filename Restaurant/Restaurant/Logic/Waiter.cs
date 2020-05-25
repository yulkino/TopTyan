﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Restaurant
{
    public class Waiter
    {
        public Game Environment;
        public Point Position = new Point();
        public TableState DishInHand = new TableState();

        public Waiter(Game rest)
        {
            Environment = rest;
        }

        public void TakeDish()
        {
            DishInHand = (TableState)(Array.IndexOf(Environment.TableForFood, Position) + 1);
            Environment.EventQueue.Enqueue(new EventData(Event.DishTaken, new List<object> { DishInHand }));
        }

        public void MakeSteps(int dx, int dy)
        {
            if (InMap(dx, dy) && !IsSituationForWorkaround(dx, dy))
            {
                Position.X += dx;
                Position.Y += dy;
                Environment.EventQueue.Enqueue(new EventData(Event.WaiterMoved, new List<object> { dx, dy, Position }));
            }
        }

        public void AcceptOrder(Guest guest)
        {
            guest.OrderFood();
            guest.IsOrderAccepted = true;
        }

        public void ServeTable(Guest guest)
        {
            Environment.Tables[guest.NumberOfTable].Serve(DishInHand);
            
        }

        public void InteractWithTables()
        {
            if (Environment.Tables.Any(p => p.Position == Position))
            {
                var guestInf = Environment.GuestsList
                    .FirstOrDefault(g => g.NumberOfTable == Array.IndexOf(Environment.Tables, Environment.Tables
                        .FirstOrDefault(p => p.Position == Position)));
                if (IsSituationForAccepting(guestInf)) AcceptOrder(guestInf);
                else if (IsSituationForServing(guestInf)) ServeTable(guestInf);
            }
            if (Environment.TableForFood.Any(p => p == Position)) TakeDish();
        }

        public bool IsSituationForAccepting(Guest guest) => Environment.Tables.FirstOrDefault(p => p.Position == Position).IsOccupated && guest.Order == TableState.EmptyTable;

        public bool IsSituationForServing(Guest guest) => Environment.Tables.FirstOrDefault(p => p.Position == Position).IsOccupated && 
            guest.Order != TableState.EmptyTable && DishInHand != TableState.EmptyTable && !Environment.Tables[guest.NumberOfTable].Served;

        public bool InMap(int dx, int dy) => (0 <= dx + Position.X && 0 <= dy + Position.Y
                && dx + Position.X < Environment.RestaurantSize.Width && Environment.RestaurantSize.Height > dy + Position.Y);

        public bool IsSituationForWorkaround(int dx, int dy) => dx == 0 && dy == 1 &&
            Environment.Tables.Select(p => p.Position).Contains(new Point(Position.X + dx, Position.Y + dy)) ?
                true : dx == 0 && dy == -1 && Environment.Tables.Select(p => p.Position).Contains(new Point(Position.X, Position.Y));
    }
}
