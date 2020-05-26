using System.Windows.Controls;
using System.Windows;

namespace Restaurant
{
    public class InfoPanel
    {
        public Grid Panel;
        public UIElement Left { get => left; set => UpdateItem(value, 0); }
        public UIElement Middle { get => middle; set => UpdateItem(value, 1); }
        public UIElement Right { get => right; set => UpdateItem(value, 2); }

        UIElement left;
        UIElement middle;
        UIElement right;

        public InfoPanel()
        {
            Panel = new Grid();
            SetGrid();
        }

        public void SetGrid()
        {
            Panel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            Panel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(7, GridUnitType.Star) });
            Panel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }

        void UpdateItem(UIElement value, int index)
        {
            var item = index == 0 ? left : index == 1 ? middle : right;
            if (item != null) Panel.Children.Remove(item);
            switch(index)
            {
                case 0:
                    left = value;
                    Panel.Children.Add(left);
                    Grid.SetColumn(left, index);
                    break;
                case 1:
                    middle = value;
                    Panel.Children.Add(middle);
                    Grid.SetColumn(middle, index);
                    break;
                case 2:
                    right = value;
                    Panel.Children.Add(right);
                    Grid.SetColumn(right, index);
                    break;
            }
        }
    }
}
