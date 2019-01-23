using System.Windows;

namespace VerkeerslichtOpdracht
{
    /// <summary>
    /// Interaction logic for Verkeerslicht.xaml
    /// </summary>
    public partial class Verkeerslicht : Window
    {
        public Verkeerslicht()
        {
            InitializeComponent();
        }

        private void ButtonGo_Click(object sender, RoutedEventArgs e)
        {
            OranjeLicht.Opacity = 0;
            GroenLicht.Opacity = 1;
            ButtonOpgelet.IsEnabled = true;
            ButtonGo.IsEnabled = false;
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            OranjeLicht.Opacity = 0;
            RoodLicht.Opacity = 1;
            ButtonOpgelet.IsEnabled = true;
            ButtonStop.IsEnabled = false;
        }

        private void ButtonOpgelet_Click(object sender, RoutedEventArgs e)
        {
            if (GroenLicht.Opacity == 1)
            {
                ButtonStop.IsEnabled = true;
                GroenLicht.Opacity = 0;
            }
            else
            {
                ButtonGo.IsEnabled = true;
                RoodLicht.Opacity = 0;
            }
            OranjeLicht.Opacity = 1;
            ButtonOpgelet.IsEnabled = false;
        }
    }
}
