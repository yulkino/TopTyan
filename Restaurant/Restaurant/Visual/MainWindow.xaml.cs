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
        public InfoPanel UpperPanel { get; } = new InfoPanel();
        public InfoPanel LowerPanel { get; } = new InfoPanel();
        List<TableVisual> TablesVisual = new List<TableVisual>();
        public Queue<EventData> EventQueue { get; } = new Queue<EventData>();
        public Dictionary<Event, Action<EventData>> Actions { get; private set; }

        public MainWindow()
        {
            new Interpretator(new Game(), this);
            InitializeComponent();
            DrawFloor();
            FillDictionary();
            SetInfoPanels();
            StartPlayerMovement();
            GetBackgroundInfoPanels();
            OutputStars(5, 1);
        }

        void FillDictionary() => Actions = new Dictionary<Event, Action<EventData>>
        {
            { Event.CreatedTable, eventData => DrawTable((Point)eventData.Data[0])},
            { Event.GuestArrived, eventData => DrawGuest((Point)eventData.Data[0])},
            { Event.GuestGone, eventData => CleanTableImage((Point)eventData.Data[0])},
            { Event.CreatedTablesForFood, eventData => DrawTablesForFood((Point[])eventData.Data[0])},
            { Event.RatingUpdated, eventData => OutputStars((int)eventData.Data[0], (int)eventData.Data[1])},
            { Event.DishTaken, eventData => AddInInventory((int)eventData.Data[0])},
            { Event.WaiterMoved, eventData => MakeStepsWithAnimation((int)eventData.Data[0], (int)eventData.Data[1], (Point)eventData.Data[2])},
            { Event.OrderAccepted, eventData => TablesVisual.FirstOrDefault(p => p.Position == (Point)eventData.Data[0]).InitializeOrder((int)eventData.Data[1])},
            { Event.ServedTable, eventData => OutputDishOnTable((Point)eventData.Data[0], (int)eventData.Data[1])},
            { Event.FinishGame, eventData => FinishGame() }
        };

        void GetBackgroundInfoPanels()
        {
            SetBackgroundInfoPanel(UpperPanel, "texture\\back0.png");
            SetBackgroundInfoPanel(LowerPanel,"texture\\back.png");
            var contourImage = GetImage("texture\\DishInHand\\Contour.png");
            contourImage.Stretch = Stretch.Fill;
            LowerPanel.Panel.Children.Add(contourImage);
            Grid.SetColumn(contourImage, 2);
        }

        void SetBackgroundInfoPanel(InfoPanel infoPanel, string img)
        {
            var backInfo = new ImageBrush(GetImage(img).Source);
            infoPanel.Panel.Background = backInfo;
            backInfo.TileMode = TileMode.Tile;
        }

        void SetInfoPanels()
        {
            Grid1.Children.Add(UpperPanel.Panel);
            Grid.SetRow(UpperPanel.Panel, 0);
            Grid1.Children.Add(LowerPanel.Panel);
            Grid.SetRow(LowerPanel.Panel, 2);
        }

        void DrawTable(Point position)
        {
            TablesVisual.Add(new TableVisual(this) { Position = position });
            Draw(GetImage("texture\\Guests\\DefaultTable.png"), position);
        }

        void DrawTablesForFood(Point[] tableForFood)
        {
            for (var i = 0; i <= 6; i++)
            {
                Draw(GetImage("texture\\TableForFood\\TableForFood.png"), tableForFood[i]);
                Draw(GetImage(Textures.FoodImages[i]),tableForFood[i]);
            }
        }

        void DrawGuest(Point position)
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
            Image image = new Image();
            image.Source = bitmapSource;
            return image;
        }

        void ChangeWaiterImage(Image image, string path)
        {
            Stream imageStreamSource = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            BitmapDecoder decoder = BitmapDecoder.Create(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BitmapSource bitmapSource = decoder.Frames[0];
            image.Source = bitmapSource;
        }

        void DrawFloor()
        {
            ImageBrush brush = new ImageBrush(GetImage("texture\\floor.jpg").Source);
            floor.Background = brush;
            brush.TileMode = TileMode.Tile;
            brush.Viewport = new Rect(0, 0, 0.1, 0.1);
        }

        void FinishGame()
        {
            if (MessageBox.Show("                 YOU LOSE\n     (вы делали это без души)") == MessageBoxResult.OK)
                Environment.Exit(0);
        }
    }
}