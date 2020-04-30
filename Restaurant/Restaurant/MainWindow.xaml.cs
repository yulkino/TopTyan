using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Restaurant
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[,] map;

        public MainWindow()
        {
            InitializeComponent();
            map = new int[floor.ColumnDefinitions.Count, floor.RowDefinitions.Count];
            GetFloor();
            GetTableForFood();
            BitmapImage wait = new BitmapImage();
            wait.BeginInit();
            wait.UriSource = new Uri("https://github.com/yulkino/TopTyan/blob/master/Restaurant/Restaurant/texture/Waiter/вниз.png?raw=true");
            wait.EndInit();
            Image waiter = new Image { Source = wait };
            floor.Children.Add(waiter);
            Grid.SetColumn(waiter, 1);
            Grid.SetRow(waiter, 3);
        }

        public void GetFloor()
        {

            BitmapImage poiu = new BitmapImage();
            poiu.BeginInit();
            poiu.UriSource = new Uri("https://sochi.crystile.ru/upload/iblock/71e/71eec539e9a70145944887420fb3ac1f.jpg");
            poiu.EndInit();

            Image textureFloor = new Image { Source = poiu };
            textureFloor.Stretch = Stretch.Fill;
            floor.Children.Add(textureFloor);
            Grid.SetColumn(textureFloor, 0);
            Grid.SetRow(textureFloor, 0);
            Grid.SetColumnSpan(textureFloor, map.GetLength(0));
            
            //for (var x = 0; x < map.GetLength(0); x++)
            //    {
            //        Image textureFloor = new Image { Source = poiu };
            //        floor.Children.Add(textureFloor);
            //        Grid.SetColumn(textureFloor, 0);
            //        Grid.SetRow(textureFloor, 0);
            //    }
        }

        public void GetTableForFood()
        {
            BitmapImage tab = new BitmapImage();
            tab.BeginInit();
            tab.UriSource = new Uri("https://github.com/yulkino/TopTyan/blob/master/Restaurant/Restaurant/texture/стол%20с%20едой/лобстер.png?raw=true");
            tab.EndInit();
            for (var x = 1; x < floor.ColumnDefinitions.Count - 1; x++)
            {
                Image textureTableWihthFood = new Image { Source = tab };
                floor.Children.Add(textureTableWihthFood);
                Grid.SetColumn(textureTableWihthFood, x);
                Grid.SetRow(textureTableWihthFood, 1);
            }
        }
    }
}
