using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Restaurant
{
    public partial class MainWindow : Window
    {
        Image waiter;
        bool movingLocked;

        void StartPlayerMovement()
        {
            waiter = GetImage(Textures.PlayerMovement[3]);
            Grid.SetZIndex(waiter, 3);
            floor.Children.Add(waiter);
        }

        void OutputDishOnTable(Point position, int dishInHand)
        {
            var dishForGuest = GetImage(Textures.FoodOnTable[dishInHand - 1]);
            Draw(dishForGuest, position);
            FreeHand();
        }

        public void KeyDetected(object sender, KeyEventArgs e)
        {
            if (movingLocked) return;
            switch (e.Key)
            {
                case Key.W:
                    EventQueue.Enqueue(new EventData(Event.PressedW));
                    break;
                case Key.A:
                    EventQueue.Enqueue(new EventData(Event.PressedA));
                    break;
                case Key.S:
                    EventQueue.Enqueue(new EventData(Event.PressedS));
                    break;
                case Key.D:
                    EventQueue.Enqueue(new EventData(Event.PressedD));
                    break;
                case Key.E:
                    EventQueue.Enqueue(new EventData(Event.PressedE));
                    break;
            }
        }

        void MoveWaiter(Point position)
        {
            Grid.SetColumn(waiter, (int)position.X);
            Grid.SetRow(waiter, (int)position.Y);
        }

        void MakeStepsWithAnimation(int dx, int dy, Point position)
        {
            int index = dx == 1 ? 1 : dx == -1 ? 2 : dy == 1 ? 3 : 0;
            ChangeWaiterImage(waiter, Textures.PlayerMovement[index]);
            movingLocked = true;
            ThicknessAnimation animation = new ThicknessAnimation()
            {
                From = new Thickness(0, 0, 0, 0),
                To = new Thickness(82 * dx, 82 * dy, -82 * dx, -82 * dy),
                AccelerationRatio = 0.2,
                FillBehavior = FillBehavior.Stop,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(140)
            };
            animation.Completed += (sender, args) =>
            {
                MoveWaiter(position);
                movingLocked = false;
            };
            Storyboard.SetTarget(animation, waiter);
            Storyboard.SetTargetProperty(animation, new PropertyPath(MarginProperty));
            var sb = new Storyboard();
            sb.Children.Add(animation);
            sb.Begin();
        }
    }
}
