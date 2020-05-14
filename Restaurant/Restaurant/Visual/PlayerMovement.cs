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
            waiter = GetImage(Textures.PlayerMovement[3]);
            Grid.SetZIndex(waiter, 3);
            floor.Children.Add(waiter);
            MakeSteps(4, 6);
        }

        public void KeyDetected(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    //ReplaceImage(waiter, Textures.PlayerMovement[0]);
                    MakeSteps(0, -1);
                    break;
                case Key.A:
                    //ReplaceImage(waiter, Textures.PlayerMovement[2]);
                    MakeSteps(-1, 0);
                        break;
                case Key.S:
                    //ReplaceImage(waiter, Textures.PlayerMovement[3]);
                    MakeSteps(0, 1);
                    break;
                case Key.D:
                    //ReplaceImage(waiter, Textures.PlayerMovement[1]);
                    MakeSteps(1, 0);
                    break;
                case Key.E: InteractionWithTables(waiterPosition);
                    break;
            }
        }

        public void MakeSteps(int dx, int dy)
        {
            if (InMap(dx, dy) && !IsSituationForWorkaround(dx, dy))
            {
                int index = dx == 1 ? 1 : dx == -1 ? 2 : dy == 1 ? 3 : 0;
                ReplaceImage(waiter, Textures.PlayerMovement[index]);
                waiterPosition.X += dx;
                waiterPosition.Y += dy;
                Grid.SetColumn(waiter, (int)waiterPosition.X);
                Grid.SetRow(waiter, (int)waiterPosition.Y);
                //Draw(waiter, waiterPosition);
            }
        }

        public void InteractionWithTables(Point waiterPosition)
        {
            if (Tables.Any(p => p.Position == waiterPosition))
                AcceptOrderOrServe(waiterPosition);
            if (TableForFood.Any(p => p == waiterPosition))
                TakeDish(waiterPosition);
        }

        public bool InMap(int dx, int dy) => (0 <= dx + waiterPosition.X && 0 <= dy + waiterPosition.Y
                && dx + waiterPosition.X < floor.ColumnDefinitions.Count && floor.RowDefinitions.Count > dy + waiterPosition.Y);

        public bool IsSituationForWorkaround(int dx, int dy) => dx == 0 && dy == 1 &&
            Tables.Select(p => p.Position).Contains(new Point(waiterPosition.X + dx, waiterPosition.Y + dy)) ?
                true : dx == 0 && dy == -1 && Tables.Select(p => p.Position).Contains(new Point(waiterPosition.X, waiterPosition.Y));
    }
}
