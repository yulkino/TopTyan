using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Restaurant
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IInterpretatable
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
        public InfoPanel panelUp = new InfoPanel();
        public InfoPanel panelDown = new InfoPanel();
        Interpretator e;
        List<TableVisual> TablesVisual = new List<TableVisual>();

        public Queue<EventData> EventQueue { get; } = new Queue<EventData>();

        public Dictionary<Event, Action<EventData>> Actions { get; private set; }

        public void FillDictionary()
        {
            Actions = new Dictionary<Event, Action<EventData>>
            {
                { Event.CreatedTable, eventData =>  DrawTable((Point)eventData.Data[0])},
                { Event.GuestArrived, eventData => DrawGuest((Point)eventData.Data[0])},
                { Event.GuestGone, eventData => CleanTableImage((Point)eventData.Data[0])},
                { Event.RatingUpdated, eventData => OutputStars((int)eventData.Data[0], (int)eventData.Data[1])},
                { Event.DishTaken, eventData => AddInInventory((TableState)eventData.Data[0])},
                { Event.WaiterMoved, eventData => MakeStepsWithAnimation((int)eventData.Data[0], (int)eventData.Data[1], (Point)eventData.Data[2])},
                { Event.OrderAccepted, eventData => TablesVisual.FirstOrDefault(p => p.Position == (Point)eventData.Data[0]).InitializeOrder((TableState)eventData.Data[1])},
                { Event.ServedTable, eventData => OutputDishOnTable((Point)eventData.Data[0], (TableState)eventData.Data[1])},
                { Event.FinishGame, eventData => FinishGame() }
            };
        }

        public MainWindow()
        {
            e = new Interpretator(new Game(), this);
            InitializeComponent();
            DrawFloor();
            FillDictionary();
            SetInfoPanel();
            DrawTablesForFood();
            StartPlayerMovement();
            GetContourInventory();
            OutputStars(5, 1);
        }

        public void GetContourInventory()
        {
            var contourImage = GetImage("texture\\DishInHand\\Contour.png");
            contourImage.Stretch = Stretch.Fill;
            panelDown.Panel.Children.Add(contourImage);
            Grid.SetColumn(contourImage, 2);
            ImageBrush brush = new ImageBrush(GetImage("texture\\back.png").Source);
            panelDown.Panel.Background = brush;
            brush.TileMode = TileMode.Tile;
        }

        public void SetInfoPanel()
        {
            Grid1.Children.Add(panelUp.Panel);
            Grid.SetRow(panelUp.Panel, 0);
            Grid1.Children.Add(panelDown.Panel);
            Grid.SetRow(panelDown.Panel, 2);
        }

        public void DrawTable(Point position)
        {
            TablesVisual.Add(new TableVisual(this) { Position = position });
            Draw(GetImage("texture\\Guests\\DefaultTable.png"), position);
        }

        public void DrawTablesForFood()
        {

            for (var forf = 0; forf <= 6; forf++)
            {
                Draw(GetImage("texture\\TableForFood\\TableForFood.png"), TableForFood[forf]);
                Draw(GetImage(Textures.FoodImages[forf]), TableForFood[forf]);
            }
        }

        public void DrawGuest(Point position)
        {
            var image = GetImage(Textures.GuestImages[new Random().Next(0, 5)]);
            TablesVisual.FirstOrDefault(p => p.Position == position).Guest = image;
            Draw(image, position);

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

        public static void ReplaceImage(Image image, string path)
        {
            Stream imageStreamSource = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            BitmapDecoder decoder = BitmapDecoder.Create(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BitmapSource bitmapSource = decoder.Frames[0];
            image.Source = bitmapSource;
        }

        public void DrawFloor()
        {
            ImageBrush brush = new ImageBrush(GetImage("texture\\floor.jpg").Source);
            floor.Background = brush;
            brush.TileMode = TileMode.Tile;
            brush.Viewport = new Rect(0, 0, 0.1, 0.1);
        }

        public void FinishGame()
        {
            if (MessageBox.Show("you lose (вы делали это без души)") == MessageBoxResult.OK)
                Environment.Exit(0);
        }
    }
}