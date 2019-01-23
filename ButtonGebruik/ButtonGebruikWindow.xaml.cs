using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ButtonGebruik
{
    /// <summary>
    /// Interaction logic for ButtonGebruikWindow.xaml
    /// </summary>
    public partial class ButtonGebruikWindow : Window
    {
        public ButtonGebruikWindow()
        {
            InitializeComponent();
        }

        private void ButtonBold_Checked(object sender, RoutedEventArgs e)
        {
            LabelTekst.FontWeight = FontWeights.Bold;
            CheckBoxBold.IsChecked = ButtonBold.IsChecked;
        }

        private void ButtonBold_Unchecked(object sender, RoutedEventArgs e)
        {
            LabelTekst.FontWeight = FontWeights.Normal;
            CheckBoxBold.IsChecked = ButtonBold.IsChecked;
        }

        private void ButtonItalic_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton knop = (ToggleButton)sender;

            if (knop.IsChecked == true)
                LabelTekst.FontStyle = FontStyles.Italic;
            else
                LabelTekst.FontStyle = FontStyles.Normal;
            CheckBoxItalic.IsChecked = knop.IsChecked;
            ButtonItalic.IsChecked = knop.IsChecked;
        }

        private void RepeatButtonGroter_Click(object sender, RoutedEventArgs e)
        {
            if (LabelTekst.FontSize < 25)
                LabelTekst.FontSize++;
        }

        private void RepeatButtonKleiner_Click(object sender, RoutedEventArgs e)
        {
            if (LabelTekst.FontSize > 1)
                LabelTekst.FontSize--;
        }

        private void Kleur_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton knop = (RadioButton)sender;
            LabelTekst.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(knop.Content.ToString());
        }

        private void CheckBoxBold_Click(object sender, RoutedEventArgs e)
        {
            ButtonBold.IsChecked = CheckBoxBold.IsChecked;
        }

        private void CheckBoxItalic_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton knop = (ToggleButton)sender;

            if (knop.IsChecked == true)
                LabelTekst.FontStyle = FontStyles.Italic;
            else
                LabelTekst.FontStyle = FontStyles.Normal;
            CheckBoxItalic.IsChecked = knop.IsChecked;
            ButtonItalic.IsChecked = knop.IsChecked;
        }
    }
}
