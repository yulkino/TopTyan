using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Restaurant
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BitmapImage poiu = new BitmapImage();
            poiu.BeginInit();
            //poiu.UriSource = new Uri("D:\\GitKraken\\TopTyan\\Restaurant\\Restaurant\\texture\\1.jpg");
            poiu.UriSource = new Uri("https://github.com/yulkino/TopTyan/blob/master/Restaurant/Restaurant/texture/пол.png?raw=true");
            poiu.EndInit();
          
            for(var x = 0; x < floor.ColumnDefinitions.Count; x++)
             for(var y = 1; y < floor.RowDefinitions.Count - 1 ; y++)
             {
                    Image textureFloor = new Image { Source = poiu };
                    floor.Children.Add(textureFloor);
                    Grid.SetColumn(textureFloor, x);
                    Grid.SetRow(textureFloor, y);
             }
            
        }
    }
}
