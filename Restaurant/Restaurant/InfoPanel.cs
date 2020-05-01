using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Restaurant
{
    class InfoPanel
    {
        public Grid Panel;
        public void SetGrid()
        {
            Panel = new Grid();
            Panel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            Panel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(7, GridUnitType.Star) });
            Panel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            Panel.ShowGridLines = true;
        }
        public InfoPanel()
        {
            SetGrid();
        }
    }
}
