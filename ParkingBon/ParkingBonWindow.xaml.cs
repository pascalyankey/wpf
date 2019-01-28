using System;
using System.Windows;
using System.Windows.Input;

namespace ParkingBon
{
    /// <summary>
    /// Interaction logic for ParkingBonWindow.xaml
    /// </summary>
    public partial class ParkingBonWindow : Window
    {
        public static RoutedCommand mijnRouteCtrlF2 = new RoutedCommand();
        public ParkingBonWindow()
        {
            InitializeComponent();

            CommandBinding mijnCtrlF2 = new CommandBinding(mijnRouteCtrlF2, CtrlF2Executed);
            this.CommandBindings.Add(mijnCtrlF2);
            KeyGesture toetsCtrlF2 = new KeyGesture(Key.F2, ModifierKeys.Control);
            KeyBinding mijnKeyCtrlF2 = new KeyBinding(mijnRouteCtrlF2, toetsCtrlF2);
            this.InputBindings.Add(mijnKeyCtrlF2);

            Nieuw();
        }

        private void NewExecuted(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        private void CtrlF2Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void CloseExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void Nieuw()
        { 
            DatumBon.SelectedDate = DateTime.Now;
            AankomstLabelTijd.Content = DateTime.Now.ToLongTimeString();
            TeBetalenLabel.Content = "0 €";
            VertrekLabelTijd.Content = AankomstLabelTijd.Content;        
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Programma afsluiten ?", "Afsluiten", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                e.Cancel = true;
        }

        private void Minder_Click(object sender, RoutedEventArgs e)
        {
            int bedrag = Convert.ToInt32(TeBetalenLabel.Content.ToString().Replace(" €", ""));
            if (bedrag > 0)
                bedrag -= 1;
            TeBetalenLabel.Content = bedrag.ToString() + " €";
            VertrekLabelTijd.Content = Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 * bedrag).ToLongTimeString();
        }

        private void Meer_Click(object sender, RoutedEventArgs e)
        {
            int bedrag = Convert.ToInt32(TeBetalenLabel.Content.ToString().Replace(" €", ""));
            DateTime vertrekuur = Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 * bedrag);
            if (vertrekuur.Hour < 22)
                bedrag += 1;
            TeBetalenLabel.Content = bedrag.ToString() + " €";
            VertrekLabelTijd.Content = Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 * bedrag).ToLongTimeString();
        }
    }
}
