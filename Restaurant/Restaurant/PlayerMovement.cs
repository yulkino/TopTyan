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
    public partial class PlayerMovement : Window
    {
        public Point waiterPosition;
        public Image waiter;
        MainWindow Window;

        public PlayerMovement(MainWindow window)
        {
            Window = window;
        }

        public void StartPlayerMovement()
        {
            waiterPosition = new Point();

            BitmapImage wait = new BitmapImage();
            wait.BeginInit();
            wait.UriSource = new Uri("https://github.com/yulkino/TopTyan/blob/master/Restaurant/Restaurant/texture/Waiter/вниз.png?raw=true");
            wait.EndInit();
            waiter = new Image { Source = wait };
            Window.floor.Children.Add(waiter);
            MakeSteps(0, 1);
        }

        public void KeyDetected(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    MakeSteps(0, -1);
                    break;
                case Key.A:
                    MakeSteps(-1, 0);
                    break;
                case Key.S:
                    MakeSteps(0, 1);
                    break;
                case Key.D:
                    MakeSteps(1, 0);
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

        public bool InMap(int dx, int dy)
        {
            return (0 <= dx + waiterPosition.X && 1 <= dy + waiterPosition.Y
                && dx + waiterPosition.X < Window.floor.ColumnDefinitions.Count && Window.floor.RowDefinitions.Count > dy + waiterPosition.Y);
        }

        public bool IsSituationForWorkaround(int dx, int dy)
        {
            return dx == 0 && dy == 1 && Window.defaultTablesPosition.Contains(new Point(waiterPosition.X + dx, waiterPosition.Y + dy)) ?
                true : dx == 0 && dy == -1 && Window.defaultTablesPosition.Contains(new Point(waiterPosition.X, waiterPosition.Y));
        }

    }
}
