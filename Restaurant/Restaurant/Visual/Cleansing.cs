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
        public static void CleanTableImage(MainWindow window,Image tableImage, Image dishForGuest)
        {
            if(dishForGuest != null)
                window.floor.Children.Remove(dishForGuest);
            if (tableImage != null)
                window.floor.Children.Remove(tableImage);
        }
    }
}
