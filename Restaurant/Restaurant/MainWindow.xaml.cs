﻿using System;
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

        public Point[] defaultTablesPosition = new Point[6] { new Point(4,3), new Point(2, 4), new Point(6,4), new Point(1,6) , new Point(4, 6) , new Point(7,6) };
        PlayerMovement playerMove;

        public MainWindow()
        {
            InitializeComponent();
            GetFloor();
            GetTableForFood();
            SetTables();
            playerMove = new PlayerMovement(this);
            
        }

        public void PassAction()
        {
            //playerMove.KeyDetected();
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
            Grid.SetColumnSpan(textureFloor, floor.ColumnDefinitions.Count);
        }

        public void GetTableForFood()
        {
            BitmapImage tab = new BitmapImage();
            tab.BeginInit();
            tab.UriSource = new Uri("https://github.com/yulkino/TopTyan/blob/master/Restaurant/Restaurant/texture/стол%20с%20едой/стол1.jpg?raw=true");
            tab.EndInit();
            for (var x = 1; x < floor.ColumnDefinitions.Count - 1; x++)
            {
                Image textureTableWihthFood = new Image { Source = tab };
                textureTableWihthFood.Stretch = Stretch.Fill;
                floor.Children.Add(textureTableWihthFood);
                Grid.SetColumn(textureTableWihthFood, x);
                Grid.SetRow(textureTableWihthFood, 1);
            }
        }

        public void SetTables()
        {
            BitmapImage table = new BitmapImage();
            table.BeginInit();
            table.UriSource = new Uri("https://github.com/yulkino/TopTyan/blob/master/Restaurant/Restaurant/texture/Гости/дефолтный%20стол.png?raw=true");
            table.EndInit();
            foreach (var tablePos in defaultTablesPosition)
            {
                Image textureTableWihthFood = new Image { Source = table };
                textureTableWihthFood.Stretch = Stretch.Fill;
                floor.Children.Add(textureTableWihthFood);
                Grid.SetColumn(textureTableWihthFood, (int)tablePos.X);
                Grid.SetRow(textureTableWihthFood, (int)tablePos.Y);
            }
        }


    }
}
