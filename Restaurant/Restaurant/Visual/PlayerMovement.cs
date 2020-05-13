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
            if (TableForFood.Any(p => p == waiterPosition))
                TakeDish(waiterPosition);
        }

        public void AcceptOrderOrServe(Point waiterPosition)
        {
            var guestInf = Guests.GuestsList.FirstOrDefault(g => g.NumberOfTable == Array.IndexOf(Tables, Tables.FirstOrDefault(p => p.Position == waiterPosition)));
            if (Tables.FirstOrDefault(p => p.Position == waiterPosition).IsOccupated && guestInf.Order == TableState.EmptyTable)
            {
                guestInf.OrderFood();
                guestInf.AcceptOrder = true;
            }
            else
            if (Tables.FirstOrDefault(p => p.Position == waiterPosition).IsOccupated && guestInf.Order != TableState.EmptyTable
                && Waiter.DishInHand != TableState.EmptyTable && !Tables[guestInf.NumberOfTable].Served)
            {
                ServedTable(guestInf);
                Draw(GetImage(Textures.FoodOnTable[(int)Waiter.DishInHand - 1]), waiterPosition);
                Rating.UpdateRating(Tables[guestInf.NumberOfTable], guestInf);
                OutputStars();
                ClearHand();
            }
        }

        public bool InMap(int dx, int dy) => (0 <= dx + waiterPosition.X && 0 <= dy + waiterPosition.Y
                && dx + waiterPosition.X < floor.ColumnDefinitions.Count && floor.RowDefinitions.Count > dy + waiterPosition.Y);

        public bool IsSituationForWorkaround(int dx, int dy) => dx == 0 && dy == 1 &&
            Tables.Select(p => p.Position).Contains(new Point(waiterPosition.X + dx, waiterPosition.Y + dy)) ?
                true : dx == 0 && dy == -1 && Tables.Select(p => p.Position).Contains(new Point(waiterPosition.X, waiterPosition.Y));
    }
}
