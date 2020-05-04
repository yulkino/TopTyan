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
        List<Uri> guestImages = new List<Uri> { new Uri("https://github.com/yulkino/TopTyan/blob/master/Restaurant/Restaurant/texture/Guests/guest1.png?raw=true"),
                                                new Uri("https://github.com/yulkino/TopTyan/blob/master/Restaurant/Restaurant/texture/Guests/guest2.png?raw=true"),
                                                new Uri("https://github.com/yulkino/TopTyan/blob/master/Restaurant/Restaurant/texture/Guests/guest3.png?raw=true"),
                                                new Uri("https://github.com/yulkino/TopTyan/blob/master/Restaurant/Restaurant/texture/Guests/guest4.png?raw=true"),
                                                new Uri("https://github.com/yulkino/TopTyan/blob/master/Restaurant/Restaurant/texture/Guests/guest5.png?raw=true"),
                                                new Uri("https://github.com/yulkino/TopTyan/blob/master/Restaurant/Restaurant/texture/Guests/guest6.png?raw=true")};

        public void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += (sender, args) => 
            {
                var g = new Guest();
                if (g.TryTakeTable())
                {
                    Guests.GuestsList.Add(g);
                    var rnd = new Random();
                    Draw(guestImages[rnd.Next(0, 5)], Tables[g.NumberOfTable].Position);
                }
            };
            timer.Start();
        }


    }
}
