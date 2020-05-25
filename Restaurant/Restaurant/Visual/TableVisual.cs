using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Restaurant
{
    class TableVisual
    {
        MainWindow Window;
        public string DishName = "";
        public Image Dish;
        Image guest;
        public Image Guest
        { get => guest;
            set
            {
                guest = value;
                if (guest == null) return;
                guest.MouseMove += (sender1, args1) =>
                {
                    if (Dish != null)
                    {
                        Window.OutputOrder(Dish);
                        Window.OutputLabel(DishName);
                    }
                };
                guest.MouseLeave += (sender2, args2) =>
                {
                    Window.OrderImage = new Image();
                    Window.DishName = new Label();
                };
            }
        }
        public Point Position;

        public TableVisual(MainWindow win)
        {
            Window = win;
        }
        
        public void InitializeOrder(TableState order)
        {
            DishName = Textures.DishName[(int)order - 1];
            Dish = MainWindow.GetImage(Textures.Dish[(int)order - 1]);
        }
    }
}
