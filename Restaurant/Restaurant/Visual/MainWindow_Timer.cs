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
                    
                }
            };
            timer.Start();
        }

        public void Obrabotchik()
        {
            BitmapImage guest = new BitmapImage();
            guest.BeginInit();
            guest.UriSource = new Uri("https://sochi.crystile.ru/upload/iblock/71e/71eec539e9a70145944887420fb3ac1f.jpg");
            guest.EndInit();
            Image textureFloor = new Image { Source = guest };
            floor.Children.Add(textureFloor);
        }

        List<Uri> guestImages = new List<Uri> { new Uri("https://github.com/yulkino/TopTyan/blob/master/Restaurant/Restaurant/texture/Guests/guest1.png?raw=true"),
                                                new Uri("https://github.com/yulkino/TopTyan/blob/master/Restaurant/Restaurant/texture/Guests/guest2.png?raw=true"),
                                                new Uri("https://github.com/yulkino/TopTyan/blob/master/Restaurant/Restaurant/texture/Guests/guest3.png?raw=true"),
                                                new Uri("https://github.com/yulkino/TopTyan/blob/master/Restaurant/Restaurant/texture/Guests/guest4.png?raw=true"),
                                                new Uri("https://github.com/yulkino/TopTyan/blob/master/Restaurant/Restaurant/texture/Guests/guest5.png?raw=true"),
                                                new Uri("https://github.com/yulkino/TopTyan/blob/master/Restaurant/Restaurant/texture/Guests/guest6.png?raw=true")};
    }
}
