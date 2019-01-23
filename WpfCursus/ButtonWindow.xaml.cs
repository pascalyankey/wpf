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

namespace WpfCursus
{
    /// <summary>
    /// Interaction logic for ButtonWindow.xaml
    /// </summary>
    public partial class ButtonWindow : Window
    {
        public ButtonWindow()
        {
            InitializeComponent();
        }

        private void ButtonKleur_Click(object sender, RoutedEventArgs e)
        {
            Button knop = (Button)sender;
            SolidColorBrush kleur = (SolidColorBrush)new BrushConverter().ConvertFromString(knop.Tag.ToString());
            this.Background = kleur;
        }
    }
}
