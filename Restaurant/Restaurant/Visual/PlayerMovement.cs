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

        public void StartPlayerMovement()
        {
            waiterPosition = new Point();
            waiter = GetImage("texture\\Waiter\\down.png");
            Grid.SetZIndex(waiter, 1);
            floor.Children.Add(waiter);
            MakeSteps(0, 1);
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
                case Key.E: InteractionWithTablesept(waiterPosition);
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

        public void InteractionWithTablesept(Point waiterPosition)
        {
            if (Tables.Any(p => p.Position == waiterPosition))
                AcceptOrder(waiterPosition);
            else if (TableForFood.Any(p => p == waiterPosition))
                TakeDish(waiterPosition);
        }

        public void TakeDish(Point waiterPosition)
        {
            var e = (TableState)(Array.IndexOf(TableForFood, waiterPosition) + 1);
            Waiter.DishInHand = (TableState)(Array.IndexOf(TableForFood, waiterPosition) + 1);
        }

        public void AcceptOrder(Point waiterPosition)
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

        public bool InMap(int dx, int dy) => (0 <= dx + waiterPosition.X && 1 <= dy + waiterPosition.Y
                && dx + waiterPosition.X < floor.ColumnDefinitions.Count && floor.RowDefinitions.Count > dy + waiterPosition.Y);

        public bool IsSituationForWorkaround(int dx, int dy) => dx == 0 && dy == 1 &&
            Tables.Select(p => p.Position).Contains(new Point(waiterPosition.X + dx, waiterPosition.Y + dy)) ?
                true : dx == 0 && dy == -1 && Tables.Select(p => p.Position).Contains(new Point(waiterPosition.X, waiterPosition.Y));
    }
}
