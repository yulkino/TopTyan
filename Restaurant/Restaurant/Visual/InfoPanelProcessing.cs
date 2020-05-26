using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Restaurant
{
    partial class MainWindow : Window
    {
        void AddInInventory(int dish) => LowerPanel.Right = GetImage(Textures.Dish[dish - 1]);

        public void OutputOrder(Image orderImage) => LowerPanel.Left = orderImage;

        public void OutputLabel(string text) => LowerPanel.Middle = new Label
        {
            Content = text,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = 24,
            FontWeight = FontWeights.SemiBold,
            FontFamily = new FontFamily("Courier New"),
            Foreground = new SolidColorBrush(Colors.DarkSlateGray)
        };

        void OutputStars(int starsCount, int ratesCount)
        {
            UpperPanel.Middle = GetImage(Textures.Stars[starsCount]);
            OutputCounter(ratesCount);
        }

        void OutputCounter(int ratesCount) => UpperPanel.Right = new Label
        {
            Content = "Rates\nCount:\n  " + ratesCount.ToString(),
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = 15,
            FontWeight = FontWeights.SemiBold,
            FontFamily = new FontFamily("Courier New"),
            Foreground = new SolidColorBrush(Colors.Brown)
        };

        void FreeHand() => LowerPanel.Right = new Image();
    }
}