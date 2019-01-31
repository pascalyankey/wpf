using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using System;

namespace DataBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DataBindingWindow : Window
    {
        public Persoon persoon = new Persoon("jan", 3500m, new DateTime(2011, 2, 13));
        public DataBindingWindow()
        {
            InitializeComponent();

            SortDescription sd = new SortDescription("Source", ListSortDirection.Ascending);
            lettertypeComboBox.Items.SortDescriptions.Add(sd);
            lettertypeComboBox.SelectedItem = new FontFamily("Arial");

            veranderPanel.DataContext = persoon;
        }

        private void ToonNaamButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(persoon.Naam);
        }

        private void VeranderButton_Click(object sender, RoutedEventArgs e)
        {
            persoon.Naam = "piet";
            persoon.Wedde = 4125.5m;
            persoon.InDienst = new DateTime(2010, 12, 21);
        }
    }
}
