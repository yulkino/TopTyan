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
        public Point[] TableForFood = new Point[7] 
        {
            new Point(1, 0),
            new Point(2, 0),
            new Point(3, 0),
            new Point(4, 0),
            new Point(5, 0),
            new Point(6, 0),
            new Point(7, 0),
        };
        public static Table[] Tables = new Table[6];
        public InfoPanel panelUp = new InfoPanel();
        public InfoPanel panelDown = new InfoPanel();

        public MainWindow()
        {
            InitializeComponent();
            GetFloor();
            GetInfoPanel();
            CreateTable();
            StartPlayerMovement();
            GetContourInventory();
            StartTimer();
        }

        public void GetContourInventory()
        {
            var contourImage = GetImage("texture\\DishInHand\\Contour.png");
            contourImage.Stretch = Stretch.Fill;
            panelDown.Panel.Children.Add(contourImage);
            Grid.SetColumn(contourImage, 2);
        }

        public void GetInfoPanel()
        {
            Grid1.Children.Add(panelUp.Panel);
            Grid.SetRow(panelUp.Panel, 0);
            Grid1.Children.Add(panelDown.Panel);
            Grid.SetRow(panelDown.Panel, 2);
        }

        public void CreateTable()
        {
            Point[] defaultTablesPosition = new Point[6] 
            {
                new Point(4, 3),
                new Point(2, 2),
                new Point(6, 2),
                new Point(1, 4),
                new Point(4, 5),
                new Point(7, 4)
            };
            for(var forg = 0; forg < defaultTablesPosition.Length; forg++)
            {
                Draw(GetImage("texture\\Guests\\DefaultTable.png"), defaultTablesPosition[forg]);
                Tables[forg] = new Table(defaultTablesPosition[forg], TableState.EmptyTable);
            }
            for (var forf = 0; forf <= 6; forf++)
            {
                Draw(GetImage("texture\\TableForFood\\TableForFood.png"), TableForFood[forf]);
                Draw(GetImage(Textures.foodImages[forf]), TableForFood[forf]);
            }
        }

        public void Draw(Image image, Point position)
        {
            if (image != null)
            {
                image.Stretch = Stretch.Fill;
                floor.Children.Add(image);
                Grid.SetColumn(image, (int)position.X);
                Grid.SetRow(image, (int)position.Y);
            }
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

        public void GetFloor()
        {
            ImageBrush brush = new ImageBrush(GetImage("texture\\floor.jpg").Source);
            floor.Background = brush;
            brush.TileMode = TileMode.Tile;
            brush.Viewport = new Rect(0, 0, 0.1, 0.1);
        }
    }
}
