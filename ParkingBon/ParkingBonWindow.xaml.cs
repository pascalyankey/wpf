using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ParkingBon
{
    /// <summary>
    /// Interaction logic for ParkingBonWindow.xaml
    /// </summary>
    public partial class ParkingBonWindow : Window
    {
        private double breedte = 16.93 / 2.54 * 96;
        private double hoogte = 8.46 / 2.54 * 96;
        private double vertPositie;
        public static RoutedCommand mijnRouteCtrlF2 = new RoutedCommand();
        public ParkingBonWindow()
        {
            InitializeComponent();

            CommandBinding mijnCtrlF2 = new CommandBinding(mijnRouteCtrlF2, PrintExecuted);
            this.CommandBindings.Add(mijnCtrlF2);
            KeyGesture toetsCtrlF2 = new KeyGesture(Key.F2, ModifierKeys.Control);
            KeyBinding mijnKeyCtrlF2 = new KeyBinding(mijnRouteCtrlF2, toetsCtrlF2);
            this.InputBindings.Add(mijnKeyCtrlF2);

            Nieuw();
        }

        private FixedDocument StelAfdrukSamen()
        {
            FixedDocument document = new FixedDocument();
            document.DocumentPaginator.PageSize = new Size(breedte, hoogte);

            PageContent inhoud = new PageContent();
            document.Pages.Add(inhoud);

            FixedPage page = new FixedPage();
            inhoud.Child = page;

            page.Width = breedte;
            page.Height = hoogte;
            vertPositie = 96;

            page.Children.Add(Foto(logoImage));
            page.Children.Add(Regel("datum : " + DatumBon.SelectedDate.Value.ToLongDateString()));
            page.Children.Add(Regel("starttijd : " + AankomstLabelTijd.Content.ToString()));
            page.Children.Add(Regel("eindtijd : " + VertrekLabelTijd.Content.ToString()));
            page.Children.Add(Regel("bedrag betaald : " + TeBetalenLabel.Content.ToString()));

            return document;
        }

        private TextBlock Regel(string tekst)
        {
            var deRegel = new TextBlock();
            
            deRegel.Text = tekst;
            deRegel.FontSize = 18;
            deRegel.Margin = new Thickness(300, vertPositie, 96, 96);
            vertPositie += 36;

            return deRegel;
        }

        private Image Foto(Image afbeelding)
        {
            var image = new Image();

            image.Source = afbeelding.Source;
            image.Margin = new Thickness(96);

            return image;
        }

        private void NewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Nieuw();
        }

        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.FileName = "Document";
                dlg.DefaultExt = ".bon";
                dlg.Filter = "Parkingbonnen |*.bon";

                if (dlg.ShowDialog() == true)
                {
                    using (StreamReader bestand = new StreamReader(dlg.FileName))
                    {
                        DatumBon.SelectedDate = Convert.ToDateTime(bestand.ReadLine());
                        AankomstLabelTijd.Content = bestand.ReadLine();
                        TeBetalenLabel.Content = bestand.ReadLine();
                        VertrekLabelTijd.Content = bestand.ReadLine();
                    }

                    StatusBon.Content = dlg.FileName;
                    AfdrukItem.IsEnabled = true;
                    AfdrukButton.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("openen mislukt : " + ex.Message);
            }
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();

                dlg.FileName = DatumBon.SelectedDate.Value.Date.ToString("dd-MM-yyyy") + "om" + AankomstLabelTijd.Content.ToString().Replace(":", "-") + ".bon";
                dlg.DefaultExt = ".bon";
                dlg.Filter = "Parkingbonnen |*.bon";

                if (dlg.ShowDialog() == true)
                {
                    using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                    {
                        bestand.WriteLine(DatumBon.SelectedDate.Value.Date.ToString("dd/MM/yyyy"));
                        bestand.WriteLine(AankomstLabelTijd.Content);
                        bestand.WriteLine(TeBetalenLabel.Content);
                        bestand.WriteLine(VertrekLabelTijd.Content);
                    }

                    StatusBon.Content = dlg.FileName.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("opslaan mislukt: " + ex.Message);
            }
        }

        private void PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Afdrukvoorbeeld preview = new Afdrukvoorbeeld();
            preview.Owner = this;
            preview.AfdrukDocument = StelAfdrukSamen();
            preview.ShowDialog();
        }

        private void PrintExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            PrintDialog afdrukken = new PrintDialog();
            if (afdrukken.ShowDialog() == true)
            {
                afdrukken.PrintDocument(StelAfdrukSamen().DocumentPaginator, "Parkingbon");
            }
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
            StatusBon.Content = "nieuwe bon";
            OpslaanItem.IsEnabled = false;
            OpslaanButton.IsEnabled = false;
            AfdrukItem.IsEnabled = false;
            AfdrukButton.IsEnabled = false;
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Programma afsluiten?", "Afsluiten", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                e.Cancel = true;
        }

        private void Minder_Click(object sender, RoutedEventArgs e)
        {
            int bedrag = Convert.ToInt32(TeBetalenLabel.Content.ToString().Replace(" €", ""));

            if (bedrag > 0)
                bedrag -= 1;

            if (bedrag > 0)
            {
                OpslaanItem.IsEnabled = true;
                OpslaanButton.IsEnabled = true;
                AfdrukItem.IsEnabled = true;
                AfdrukButton.IsEnabled = true;
            }
            else
            {
                OpslaanItem.IsEnabled = false;
                OpslaanButton.IsEnabled = false;
                AfdrukItem.IsEnabled = false;
                AfdrukButton.IsEnabled = false;
            }

            TeBetalenLabel.Content = bedrag.ToString() + " €";
            VertrekLabelTijd.Content = Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 * bedrag).ToLongTimeString();
        }

        private void Meer_Click(object sender, RoutedEventArgs e)
        {
            int bedrag = Convert.ToInt32(TeBetalenLabel.Content.ToString().Replace(" €", ""));
            DateTime vertrekuur = Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 * bedrag);

            if (vertrekuur.Hour < 22)
                bedrag += 1;

            if (bedrag > 0)
            {
                OpslaanItem.IsEnabled = true;
                OpslaanButton.IsEnabled = true;
                AfdrukItem.IsEnabled = true;
                AfdrukButton.IsEnabled = true;
            }
            else
            {
                OpslaanItem.IsEnabled = false;
                OpslaanButton.IsEnabled = false;
                AfdrukItem.IsEnabled = false;
                AfdrukButton.IsEnabled = false;
            }

            TeBetalenLabel.Content = bedrag.ToString() + " €";
            VertrekLabelTijd.Content = Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 * bedrag).ToLongTimeString();
        }
    }
}
