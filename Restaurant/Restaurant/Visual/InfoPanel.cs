using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Restaurant
{
    class InfoPanel
    {
        public Grid Panel;
        public void SetGrid()
        {
            Panel = new Grid();
            Panel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            Panel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(7, GridUnitType.Star) });
            Panel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            Panel.ShowGridLines = true;
        }
        public InfoPanel()
        {
            SetGrid();
        }
    }

    partial class MainWindow : Window
    {
        public string[] Dish = new string[7]
        {
            "texture\\DishInHand\\Ratatouille.png",
            "texture\\DishInHand\\Guacamole.png",
            "texture\\DishInHand\\CreamSoup.png",
            "texture\\DishInHand\\HotChili.png",
            "texture\\DishInHand\\Lobster.png",
            "texture\\DishInHand\\HoneyNuggets.png",
            "texture\\DishInHand\\IceCream.png"
        };

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
        Image foodInHandImage;

        public void AddInInventory()
        {
            FoodInHandImage = GetImage(Dish[(int)Waiter.DishInHand - 1]);
        }

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
        Image orderImage;

        public void OutputOrder(Image orderImage)
        {
            if (!panelDown.Panel.Children.Contains(orderImage))
                OrderImage = orderImage;
        }
    }
}
