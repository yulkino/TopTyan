using System.Linq;
using System.Windows;

namespace Restaurant
{
    partial class MainWindow : Window
    {
        public void CleanTableImage(Point position)
        {
            var tableVisual = TablesVisual.FirstOrDefault(p => p.Position == position);
            if (tableVisual.Dish != null)
            {
                floor.Children.Remove(tableVisual.Dish);
                tableVisual.Dish = null;
            }
            if (tableVisual.Guest != null)
            {
                floor.Children.Remove(tableVisual.Guest);
                tableVisual.Guest = null;
            }
        }
    }
}
