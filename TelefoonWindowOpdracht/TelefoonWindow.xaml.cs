using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Media;

namespace TelefoonWindowOpdracht
{
    /// <summary>
    /// Interaction logic for TelefoonWindow.xaml
    /// </summary>
    public partial class TelefoonWindow : Window
    {
        public TelefoonWindow()
        {
            InitializeComponent();
        }

        public List<Persoon> personen = new List<Persoon>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            personen.Add(new Persoon("grote zus", "111111111", "Familie", new BitmapImage(new Uri(@"Images\grotezus.jpg", UriKind.Relative))));
            personen.Add(new Persoon("kleine zus", "222222222", "Familie", new BitmapImage(new Uri(@"Images\kleinezus.jpg", UriKind.Relative))));
            personen.Add(new Persoon("tante non", "333333333", "Familie", new BitmapImage(new Uri(@"Images\tantenon.jpg", UriKind.Relative))));
            personen.Add(new Persoon("vader", "444444444", "Familie", new BitmapImage(new Uri(@"Images\vader.jpg", UriKind.Relative))));
            personen.Add(new Persoon("anne", "555555555", "Vrienden", new BitmapImage(new Uri(@"Images\anne.jpg", UriKind.Relative))));
            personen.Add(new Persoon("bob", "666666666", "Vrienden", new BitmapImage(new Uri(@"Images\bob.jpg", UriKind.Relative))));
            personen.Add(new Persoon("ed", "777777777", "Vrienden", new BitmapImage(new Uri(@"Images\ed.jpg", UriKind.Relative))));
            personen.Add(new Persoon("collega 1", "888888888", "Werk", new BitmapImage(new Uri(@"Images\collega1.jpg", UriKind.Relative))));
            personen.Add(new Persoon("collega 2", "999999999", "Werk", new BitmapImage(new Uri(@"Images\collega2.jpg", UriKind.Relative))));
            personen.Add(new Persoon("collega 3", "101010101", "Werk", new BitmapImage(new Uri(@"Images\collega3.jpg", UriKind.Relative))));

            ComboBoxGroep.Items.Add("Iedereen");
            ComboBoxGroep.Items.Add("Familie");
            ComboBoxGroep.Items.Add("Vrienden");
            ComboBoxGroep.Items.Add("Werk");
            ComboBoxGroep.SelectedIndex = 0;
        }

        private void ComboBoxGroep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxPersonen.Items.Clear();

            foreach (var persoon in personen)
            {
                if (persoon.Groep == ComboBoxGroep.SelectedItem.ToString() || ComboBoxGroep.SelectedIndex == 0)
                    ListBoxPersonen.Items.Add(persoon);
            }

            ListBoxPersonen.Items.SortDescriptions.Add(new SortDescription("Naam", ListSortDirection.Ascending));
        }

        private void ButtonBellen_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxPersonen.SelectedItem == null)
            {
                MessageBox.Show("Je moet eerst iemand selecteren", "Niemand gekozen", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                var persoon = (Persoon)ListBoxPersonen.SelectedValue;

                if (MessageBox.Show($"Wil je {persoon.Naam} bellen \nop het nummer: {persoon.Telefoonnr}?", "Telefoon", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    var speler = new SoundPlayer(TelefoonWindowOpdracht.Properties.Resources.PHONE);
                    speler.Play();
                }
            }
        }
    }
}
