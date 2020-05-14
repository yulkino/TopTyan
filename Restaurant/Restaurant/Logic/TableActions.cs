using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Restaurant
{
    partial class MainWindow : Window
    {
        public void TakeDish(Point waiterPosition)
        {
            Waiter.DishInHand = (TableState)(Array.IndexOf(TableForFood, waiterPosition) + 1);
            AddInInventory();
        }

        public void ServedTable(Guest guestInf)
        {
            Tables[guestInf.NumberOfTable].Served = true;
            Tables[guestInf.NumberOfTable].FoodOnTable = Waiter.DishInHand;
        }

        public void AcceptOrderOrServe(Point waiterPosition)
        {
            var guestInf = Guests.GuestsList.FirstOrDefault(g => g.NumberOfTable == Array.IndexOf(Tables, Tables.FirstOrDefault(p => p.Position == waiterPosition)));
            if (Tables.FirstOrDefault(p => p.Position == waiterPosition).IsOccupated && guestInf.Order == TableState.EmptyTable)
            {
                guestInf.OrderFood();
                guestInf.AcceptOrder = true;
            }
            else  
            if (Tables.FirstOrDefault(p => p.Position == waiterPosition).IsOccupated && guestInf.Order != TableState.EmptyTable
                && Waiter.DishInHand != TableState.EmptyTable && !Tables[guestInf.NumberOfTable].Served)
            {
                ServedTable(guestInf);
                var dishForGuest = GetImage(Textures.FoodOnTable[(int)Waiter.DishInHand - 1]);
                Draw(dishForGuest, waiterPosition);
                guestInf.DishImage = dishForGuest;
                Rating.UpdateRating(Tables[guestInf.NumberOfTable], guestInf);
                OutputStars();
                ClearHand();
            }
        }

        public static void CleanTable(Table table)
        {
            table.FoodOnTable = TableState.EmptyTable;
            table.IsOccupated = false;
            table.Served = false;
        }
    }
}
