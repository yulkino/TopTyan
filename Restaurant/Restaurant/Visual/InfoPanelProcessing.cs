using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Restaurant
{
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

        public void AddInInventory(TableState dish) => FoodInHandImage = GetImage(Textures.Dish[(int)dish - 1]);

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

        public void OutputStars(int starsCount, int ratesCount)
        {
            var image = new Image();
            panelUp.Panel.Children.Add(image);
            Grid.SetColumn(image, 1);
            var stars = GetImage(Textures.Stars[starsCount]);
            panelUp.Panel.Children.Add(stars);
            Grid.SetColumn(stars, 1);
            OutputCounter(ratesCount);
            //if (Rating.Grade == 0) FinishGame();
        }

        Label Counter = new Label()
        {
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = 15,
            FontFamily = new FontFamily("Courier New")
        };

        public void OutputCounter(int ratesCount)
        {
            if (!panelUp.Panel.Children.Contains(Counter))
            {
                Counter.Content = "Rates\nCount:\n  " + ratesCount.ToString();
                panelUp.Panel.Children.Add(Counter);
            }
            Counter.Content = "Rates\nCount:\n  " + ratesCount.ToString();
            Grid.SetColumn(Counter, 2);
        }

        public void FreeHand() => FoodInHandImage = new Image();
    }
}
