using System.Windows.Controls;
using System.Windows;

namespace Restaurant
{
    public class InfoPanel
    {
        public Grid Panel;
        public void SetGrid()
        {
            Panel = new Grid();
            Panel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            Panel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(7, GridUnitType.Star) });
            Panel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }
        public InfoPanel()
        {
            SetGrid();
        }
    }
}
