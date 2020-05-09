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
        string[] guestImages = new string[6]
            {
            "texture\\Guests\\guest1.png",
            "texture\\Guests\\guest2.png",
            "texture\\Guests\\guest3.png",
            "texture\\Guests\\guest4.png",
            "texture\\Guests\\guest5.png",
            "texture\\Guests\\guest6.png"
            };

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
                    Draw(GetImage(guestImages[rnd.Next(0, 5)]), Tables[g.NumberOfTable].Position);
                    Tables[g.NumberOfTable].IsOccupated = true;
                }
            };
            //timer.Interval = TimeSpan.FromMilliseconds(10);
            //timer.Tick += (sender, args) =>
            //{
                
            //};
            timer.Start();
        }


    }
}
