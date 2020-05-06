using System;
using System.Collections.Generic;
using System.IO;
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
        public Point[] TableForFood = new Point[7] { new Point(1, 1), new Point(2, 1), new Point(3, 1), new Point(4, 1), new Point(5, 1), new Point(6, 1), new Point(7, 1), };
        public static Table[] Tables = new Table[6];
        InfoPanel panel;
        string[] foodImages = new string[7]
        {
            "texture\\TableForFood\\Ratatouille.png",
            "texture\\TableForFood\\Guacamole.png",
            "texture\\TableForFood\\CreamSoup.png",
            "texture\\TableForFood\\HotChili.png",
            "texture\\TableForFood\\Lobster.png",
            "texture\\TableForFood\\HoneyNuggets.png",
            "texture\\TableForFood\\IceCream.png"
        };

        public MainWindow()
        {
            InitializeComponent();
            GetContur();
            panel = new InfoPanel();
            CreateTable();
            StartPlayerMovement();
            StartTimer();
            Grid1.Children.Add(panel.Panel);
            Grid.SetRow(panel.Panel, 1);
        }

        public void CreateTable()
        {
            Point[] defaultTablesPosition = new Point[6] { new Point(4, 3), new Point(2, 4), new Point(6, 4), new Point(1, 6), new Point(4, 6), new Point(7, 6) };
            for(var forg = 0; forg < defaultTablesPosition.Length; forg++)
            {
                Draw(GetImage("texture\\Guests\\DefaultTable.png"), defaultTablesPosition[forg]);
                Tables[forg] = new Table(defaultTablesPosition[forg], TableState.EmptyTable);
            }
            for (var forf = 0; forf <= 6; forf++)
            {
                Draw(GetImage("texture\\TableForFood\\TableForFood.png"), TableForFood[forf]);
                Draw(GetImage(foodImages[forf]), TableForFood[forf]);
            }
        }

        public void Draw(Image image, Point position)
        {
            image.Stretch = Stretch.Fill;
            floor.Children.Add(image);
            Grid.SetColumn(image, (int)position.X);
            Grid.SetRow(image, (int)position.Y);
        }

        public void GetContur()
        {

            BitmapImage poiu = new BitmapImage();
            poiu.BeginInit();
            poiu.UriSource = new Uri("https://sochi.crystile.ru/upload/iblock/71e/71eec539e9a70145944887420fb3ac1f.jpg");
            poiu.EndInit();

            Image textureFloor = new Image { Source = poiu };
            textureFloor.Stretch = Stretch.Fill;
            floor.Children.Add(textureFloor);
            Grid.SetColumnSpan(textureFloor, floor.ColumnDefinitions.Count);
            Grid.SetColumnSpan(textureFloor, floor.RowDefinitions.Count);
        }

        public static Image GetImage(string path)
        {
            Stream imageStreamSource = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            BitmapDecoder decoder = BitmapDecoder.Create(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BitmapSource bitmapSource = decoder.Frames[0];
            Image myImage = new Image();
            myImage.Source = bitmapSource;
            return myImage;
        }
    }
}
