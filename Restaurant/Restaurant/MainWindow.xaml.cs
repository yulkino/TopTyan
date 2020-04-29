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
        public MainWindow()
        {
            InitializeComponent();
            GetFloor();
            GetTableForFood();
            BitmapImage wait = new BitmapImage();
            wait.BeginInit();
            wait.UriSource = new Uri("https://github.com/yulkino/TopTyan/blob/master/Restaurant/Restaurant/texture/Waiter/вправо.png?raw=true");
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
            poiu.UriSource = new Uri("https://github.com/yulkino/TopTyan/blob/master/Restaurant/Restaurant/texture/пол.png?raw=true");
            poiu.EndInit();

            for (var y = 0; y < floor.ColumnDefinitions.Count; y++)
                for (var x = 1; x < floor.RowDefinitions.Count - 1; x++)
                {
                    Image textureFloor = new Image { Source = poiu };
                    floor.Children.Add(textureFloor);
                    Grid.SetColumn(textureFloor, y);
                    Grid.SetRow(textureFloor, x);
                }
        }

        public void GetTableForFood()
        {
            BitmapImage tab = new BitmapImage();
            tab.BeginInit();
            tab.UriSource = new Uri("https://github.com/yulkino/TopTyan/blob/master/Restaurant/Restaurant/texture/стол%20с%20едой/крем-суп.png?raw=true");
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
