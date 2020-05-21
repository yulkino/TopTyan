using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace Restaurant
{
    public class InfoPanel
    {
        public Grid Panel;
        public void SetGrid()
        {
            Panel = new Grid();
            Panel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            Panel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(7, GridUnitType.Star) });
            Panel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }
        public InfoPanel()
        {
            SetGrid();
        }
    }

    partial class MainWindow : Window 
    {
        Image foodInHandImage;
        public Image FoodInHandImage
        {
            get => foodInHandImage;
            set
            {
                if (foodInHandImage != null)
                    panelDown.Panel.Children.Remove(foodInHandImage);
                foodInHandImage = value;
                panelDown.Panel.Children.Add(foodInHandImage);
                Grid.SetColumn(foodInHandImage, 2);
            }
        }

        public void AddInInventory(WaiterModel waiter)
        {
            FoodInHandImage = GetImage(Textures.Dish[(int)waiter.DishInHand - 1]);
        }

        Image orderImage;
        public Image OrderImage
        {
            get => orderImage;
            set
            {
                if (orderImage != null)
                    panelDown.Panel.Children.Remove(orderImage);
                orderImage = value;
                panelDown.Panel.Children.Add(orderImage);
                Grid.SetColumn(orderImage, 0);
            }
        }

        public void OutputOrder(Image orderImage)
        {
            if (!panelDown.Panel.Children.Contains(orderImage))
                OrderImage = orderImage;
        }

        Label name;
        public Label DishName
        {
            get => name;
            set
            {
                if (name != null)
                    panelDown.Panel.Children.Remove(name);
                name = value;
                panelDown.Panel.Children.Add(name);
                Grid.SetColumn(name, 1);
            }
        }

        public void OutputLabel(string text) 
        {
            DishName = new Label
            {
                Content = text,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 24,
                FontWeight = FontWeights.SemiBold,
                FontFamily = new FontFamily("Courier New"),
                Foreground = new SolidColorBrush(Colors.DarkSlateGray)
            };
        }

        public void OutputStars()
        {
            var image = new Image();
            panelUp.Panel.Children.Add(image);
            Grid.SetColumn(image, 1);
            var stars = GetImage(Textures.Stars[Rating.Grade]);
            panelUp.Panel.Children.Add(stars);
            Grid.SetColumn(stars, 1);
            OutputCounter();
            if (Rating.Grade == 0) FinishGame();
        }

        Label Counter = new Label()
        {
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = 15,
            FontFamily = new FontFamily("Courier New")
        };

        public void OutputCounter()
        {
            if (!panelUp.Panel.Children.Contains(Counter))
            {
                Counter.Content = "Total:" + Rating.CountRating.ToString();
                panelUp.Panel.Children.Add(Counter);
            }
            Counter.Content = "Total:" + Rating.CountRating.ToString();
            Grid.SetColumn(Counter, 2);
        }

        public void ClearHand(WaiterModel waiter)
        {
            FoodInHandImage = new Image();
            waiter.DishInHand = TableState.EmptyTable;
        }
    }
}
