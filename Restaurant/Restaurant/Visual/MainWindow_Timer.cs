using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Restaurant
{
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;

        public void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += (sender, args) => 
            {
                var g = new Guest();
                if (g.TryTakeTable())
                {
                    Guests.GuestsList.Add(g);
                    var rnd = new Random();
                    var guest = GetImage(Textures.guestImages[rnd.Next(0, 5)]);
                    guest.MouseMove += (sender1, args1) =>
                    {
                        if (g.Order != TableState.EmptyTable)
                        {
                            OutputOrder(GetImage(Textures.Dish[(int)g.Order]));
                            OutputLabel(Textures.DishName[(int)g.Order]);
                        }
                    };
                    guest.MouseLeave += (sender2, args2) =>
                    {
                        OrderImage = new Image();
                        DishName = new Label();
                    };
                    Draw(guest, Tables[g.NumberOfTable].Position);
                    Tables[g.NumberOfTable].IsOccupated = true;
                }
            };   
            timer.Start();
        }


    }
}
