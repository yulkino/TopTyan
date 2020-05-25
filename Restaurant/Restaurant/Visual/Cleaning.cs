using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Restaurant
{
    partial class MainWindow : Window
    {

        //public void RemoveThisGuest(Guest guest)
        //{
        //    guest.TimerForOrder.Stop();
        //    Guests.GuestsList.Remove(guest);
        //    Rating.UpdateRating(Tables[guest.NumberOfTable], guest);
        //    CleanTable(Tables[guest.NumberOfTable]);
        //    CleanTableImage(guest);
        //    OutputStars();
        //}  

        public void CleanTableImage(GuestModel guest)
        {
            var tableVisual = TablesVisual.FirstOrDefault(p => p.Position == guest.Position);
            if (tableVisual.Dish != null)
            {
                floor.Children.Remove(tableVisual.Dish);
                tableVisual.Dish = null;
            }
            if (tableVisual.Guest != null)
            {
                floor.Children.Remove(tableVisual.Guest);
                tableVisual.Guest = null;
            }
        }
    }
}
