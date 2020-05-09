using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Restaurant
{
    public partial class MainWindow : Window
    {
        public Point waiterPosition;
        public Image waiter;
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
                if(foodInHandImage != null)
                    panelDown.Panel.Children.Remove(foodInHandImage);
                foodInHandImage = value;
                panelDown.Panel.Children.Add(foodInHandImage);
                Grid.SetColumn(foodInHandImage, 2);
            }
        }
        Image foodInHandImage;


        public void StartPlayerMovement()
        {
            waiterPosition = new Point();
            waiter = GetImage("texture\\Waiter\\down.png");
            Grid.SetZIndex(waiter, 1);
            floor.Children.Add(waiter);
            MakeSteps(0, 0);
        }

        public void KeyDetected(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W: MakeSteps(0, -1);
                    break;
                case Key.A: MakeSteps(-1, 0);
                    break;
                case Key.S: MakeSteps(0, 1);
                    break;
                case Key.D: MakeSteps(1, 0);
                    break;
                case Key.E: InteractionWithTables(waiterPosition);
                    break;
            }
        }

        public void MakeSteps(int dx, int dy)
        {
            if (InMap(dx, dy) && !IsSituationForWorkaround(dx, dy))
            {
                waiterPosition.X += dx;
                waiterPosition.Y += dy;
                Grid.SetColumn(waiter, (int)waiterPosition.X);
                Grid.SetRow(waiter, (int)waiterPosition.Y);
            }
        }

        public void InteractionWithTables(Point waiterPosition)
        {
            if (Tables.Any(p => p.Position == waiterPosition))
                AcceptOrderOrServe(waiterPosition);
            else if (TableForFood.Any(p => p == waiterPosition))
                TakeDish(waiterPosition);
        }

        public void TakeDish(Point waiterPosition)
        {
            Waiter.DishInHand = (TableState)(Array.IndexOf(TableForFood, waiterPosition) + 1);
            FoodInHandImage = GetImage(Dish[(int)Waiter.DishInHand - 1]);
        }

        public void AcceptOrderOrServe(Point waiterPosition)
        {
            if (IsSituationForAccaptOrder(waiterPosition))
            {
                Tables.FirstOrDefault(p => p.Position == waiterPosition).Served = true;
                Guests.GuestsList
                    .FirstOrDefault(g => g.NumberOfTable == Array.IndexOf(Tables, Tables.FirstOrDefault(p => p.Position == waiterPosition)))
                    .OrderFood();
            }
        }

        public bool IsSituationForAccaptOrder(Point waiterPosition) => Tables.Select(p => p.Position).Contains(waiterPosition);

        public bool InMap(int dx, int dy) => (0 <= dx + waiterPosition.X && 0 <= dy + waiterPosition.Y
                && dx + waiterPosition.X < floor.ColumnDefinitions.Count && floor.RowDefinitions.Count > dy + waiterPosition.Y);

        public bool IsSituationForWorkaround(int dx, int dy) => dx == 0 && dy == 1 &&
            Tables.Select(p => p.Position).Contains(new Point(waiterPosition.X + dx, waiterPosition.Y + dy)) ?
                true : dx == 0 && dy == -1 && Tables.Select(p => p.Position).Contains(new Point(waiterPosition.X, waiterPosition.Y));
    }
}
