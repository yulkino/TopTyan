using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Restaurant
{
    public partial class MainWindow : Window
    {
        public Point waiterPosition;
        public Image waiter;
        private bool MovingLocked;

        public void StartPlayerMovement()
        {
            waiterPosition = new Point();
            waiter = GetImage(Textures.PlayerMovement[3]);
            Grid.SetZIndex(waiter, 3);
            floor.Children.Add(waiter);
            //MakeSteps(0, 0);
        }

        public void KeyDetected(object sender, KeyEventArgs e)
        {
            if (MovingLocked) return;
            switch (e.Key)
            {
                case Key.W:
                    MakeStepsWithAnimation(waiter, 0, -1);
                    break;
                case Key.A:
                    MakeStepsWithAnimation(waiter, -1, 0);
                    break;
                case Key.S:
                    MakeStepsWithAnimation(waiter, 0, 1);
                    break;
                case Key.D:
                    MakeStepsWithAnimation(waiter, 1, 0);
                    break;
                case Key.E:
                    InteractionWithTables(waiterPosition);
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
                //Draw(waiter, waiterPosition);
            }
        }

        private void MakeStepsWithAnimation(UIElement element, int dx, int dy)
        {
            if (!InMap(dx, dy) || IsSituationForWorkaround(dx, dy)) return;
            int index = dx == 1 ? 1 : dx == -1 ? 2 : dy == 1 ? 3 : 0;
            ReplaceImage(waiter, Textures.PlayerMovement[index]);
            MovingLocked = true;
            ThicknessAnimation animation = new ThicknessAnimation()
            {
                From = new Thickness(0, 0, 0, 0),
                To = new Thickness(82 * dx, 82 * dy, -82 * dx, -82 * dy),
                AccelerationRatio = 0.2,
                FillBehavior = FillBehavior.Stop,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(180)
            };
            animation.Completed += (sender, args) =>
            {
                MakeSteps(dx, dy);
                MovingLocked = false;
            };
            Storyboard.SetTarget(animation, element);
            Storyboard.SetTargetProperty(animation, new PropertyPath(MarginProperty));
            var sb = new Storyboard();
            sb.Children.Add(animation);
            sb.Begin();
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
