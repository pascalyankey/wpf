using System.Windows;
using System.Windows.Media;

namespace KleurenKiezer
{
    /// <summary>
    /// Interaction logic for BrushesWindow.xaml
    /// </summary>
    public partial class BrushesWindow : Window
    {
        public BrushesWindow()
        {
            InitializeComponent();
        }

        private void VergrootButton_Checked(object sender, RoutedEventArgs e)
        {
            if (VergrootButton.IsChecked == true)
            {
                VergrootButton.Content = "Zonder vergroting";
                var zicht = new VisualBrush();
                zicht.TileMode = TileMode.FlipY;
                zicht.Viewport = new Rect(0, 0, 1, 0.5);
                zicht.Visual = PanelMetKnop;
                Vergroting.Fill = zicht;
            }
            else
            {
                VergrootButton.Content = "Vergroot";
                Vergroting.Fill = null;
            }
        }
    }
}
