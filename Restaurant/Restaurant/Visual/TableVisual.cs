﻿using System.Windows;
using System.Windows.Controls;

namespace Restaurant
{
    class TableVisual
    {
        MainWindow Window;
        string DishName = "";
        public Image Dish { get; set; }
        public Image DishPanel { get; set; }
        public Point Position { get; set; }
        Image guest;

        public Image Guest
        {
            get => guest;
            set
            {
                guest = value;
                if (guest == null) return;
                guest.MouseMove += (sender1, args1) =>
                {
                    if (DishPanel == null) return;
                    Window.OutputOrder(DishPanel);
                    Window.OutputLabel(DishName);
                };
                guest.MouseLeave += (sender2, args2) =>
                {
                    Window.LowerPanel.Left = new Image();
                    Window.LowerPanel.Middle = new Label();
                };
            }
        }

        public TableVisual(MainWindow win) => Window = win;
        
        public void InitializeOrder(int order)
        {
            DishName = Textures.DishName[order - 1];
            DishPanel = MainWindow.GetImage(Textures.Dish[order - 1]);
        }
    }
}
