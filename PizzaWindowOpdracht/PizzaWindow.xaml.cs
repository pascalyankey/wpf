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

namespace PizzaWindowOpdracht
{
    /// <summary>
    /// Interaction logic for PizzaWindow.xaml
    /// </summary>
    public partial class PizzaWindow : Window
    {
        public PizzaWindow()
        {
            InitializeComponent();
        }

        private void ButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            var hoeveelheid = Convert.ToInt32(LabelHoeveelheid.Content);
            if (hoeveelheid < 10)
            {
                hoeveelheid++;
                LabelHoeveelheid.Content = hoeveelheid;
            }
        }

        private void ButtonMin_Click(object sender, RoutedEventArgs e)
        {
            var hoeveelheid = Convert.ToInt32(LabelHoeveelheid.Content);
            if (hoeveelheid > 1)
            {
                hoeveelheid--;
                LabelHoeveelheid.Content = hoeveelheid;
            }
        }

        public string ToonIngredienten()
        {
            var ingredienten = new List<string>();
            var ingredientBuilder = new StringBuilder();
            var teller = 0;

            foreach (CheckBox kind in ingredientenPanel.Children)
            {
                if ((bool)kind.IsChecked)
                    ingredienten.Add(kind.Content.ToString());
            }

            foreach (var ingredient in ingredienten)
            {
                teller++;
       
                if (teller == ingredienten.Count)
                    ingredientBuilder.Append(" en " + ingredient);
                if (ingredienten.Count - teller >= 2)
                    ingredientBuilder.Append(ingredient + ", ");
                if (ingredienten.Count - teller == 1)
                    ingredientBuilder.Append(ingredient);
            }

            return ingredientBuilder.ToString();
        }

        public string ToonOvertrooid()
        {
            if ((bool)ButtonExtraDikkeKorst.IsChecked && (bool)ButtonExtraKaas.IsChecked)
            {
                return "overstrooid met extra dikke korst \noverstrooid met extra kaas";
            }
            else if ((bool)ButtonExtraDikkeKorst.IsChecked && !(bool)ButtonExtraKaas.IsChecked)
            {
                return "overstrooid met extra dikke korst";
            }
            else if (!(bool)ButtonExtraDikkeKorst.IsChecked && (bool)ButtonExtraKaas.IsChecked)
            {
                return "overstrooid met extra kaas";
            } 
            else
                return "";
        }

        private void ButtonBestellen_Click(object sender, RoutedEventArgs e)
        {
            LabelResultaat.Content = "";

            var hoeveelheid = Convert.ToInt32(LabelHoeveelheid.Content);
            string grootte = "";

            foreach(RadioButton kind in groottePanel.Children)
            {
                if ((bool)kind.IsChecked)
                    grootte = kind.Content.ToString();
            }

            LabelResultaat.Content = "U heeft " + hoeveelheid + " " + grootte + " pizza('s) besteld met: " + ToonIngredienten() + "\n" + ToonOvertrooid() ;
        }
    }
}
