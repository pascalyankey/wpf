using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;

namespace Wenskaarten.ViewModel
{
    public class WenskaartVM : ViewModelBase
    {
        private Model.Wenskaart wenskaart;
        private Canvas CanvasKaart;

        public WenskaartVM(Model.Wenskaart wenskaart)
        {
            this.wenskaart = wenskaart;
            this.wenskaart.Lettergrootte = 12;
            this.wenskaart.Wens = "Wens intikken";

            EnableMenu = false;
            KaartZichtbaarheid = 0;
            Statusbalk = "Nieuw";

            foreach (PropertyInfo info in typeof(Colors).GetProperties())
            {
                var bc = new BrushConverter();
                var deKleur = (SolidColorBrush)bc.ConvertFromString(info.Name);
                var kleurke = new Model.Kleur();
                kleurke.Borstel = deKleur;
                kleurke.Naam = info.Name;
                kleurke.Hex = deKleur.ToString();
                kleurke.Rood = deKleur.Color.R;
                kleurke.Groen = deKleur.Color.G;
                kleurke.Blauw = deKleur.Color.B;
                Kleuren.Add(kleurke);
            }
        }

        private int kaartzichtbaarheid;
        public int KaartZichtbaarheid
        {
            get
            {
                return kaartzichtbaarheid;
            }
            set
            {
                kaartzichtbaarheid = value;
                RaisePropertyChanged("KaartZichtbaarheid");
            }
        }
        public string TypeKaart
        {
            get
            {
                return wenskaart.TypeKaart;
            }
            set
            {
                wenskaart.TypeKaart = value;
                RaisePropertyChanged("TypeKaart");
            }
        }

        private string statusbalk;
        public string Statusbalk
        {
            get
            {
                return statusbalk;
            }
            set
            {
                statusbalk = value;
                RaisePropertyChanged("Statusbalk");
            }
        }
        public string Wens
        {
            get
            {
                return wenskaart.Wens;
            }
            set
            {
                wenskaart.Wens = value;
                RaisePropertyChanged("Wens");
            }
        }
        public Model.Kleur BalKleur
        {
            get
            {
                return wenskaart.BalKleur;
            }
            set
            {
                wenskaart.BalKleur = value;
                RaisePropertyChanged("BalKleur");
            }
        }

        private ObservableCollection<Model.Kleur> kleuren = new ObservableCollection<Model.Kleur>();
        public ObservableCollection<Model.Kleur> Kleuren
        {
            get
            {
                return kleuren;
            }
            set
            {
                kleuren = value;
                RaisePropertyChanged("Kleuren");
            }
        }

        public string Lettertype
        {
            get
            {
                return wenskaart.Lettertype;
            }
            set
            {
                wenskaart.Lettertype = value;
                RaisePropertyChanged("Lettertype");
            }
        }

        public int Lettergrootte
        {
            get
            {
                return wenskaart.Lettergrootte;
            }
            set
            {
                wenskaart.Lettergrootte = value;
                RaisePropertyChanged("Lettergrootte");
            }
        }

        private Boolean enableMenu;
        public Boolean EnableMenu
        {
            get
            {
                return enableMenu;
            }
            set
            {
                enableMenu = value;
                RaisePropertyChanged("EnableMenu");
            }
        }

        private ObservableCollection<Bal> ballen = new ObservableCollection<Bal>();
        public ObservableCollection<Bal> Ballen
        {
            get
            {
                return ballen;
            }
            set
            {
                ballen = value;
                RaisePropertyChanged("Ballen");
            }
        }

        public RelayCommand<MouseEventArgs> BalKleur_MouseMove
        {
            get { return new RelayCommand<MouseEventArgs>(Ellipse_MouseMove); }
        }

        private Ellipse sleepEllipse = new Ellipse();
        private void Ellipse_MouseMove(MouseEventArgs e)
        {
            sleepEllipse = (Ellipse)e.OriginalSource;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject sleepKleur = new DataObject("balKleur", sleepEllipse.Fill);
                DragDrop.DoDragDrop(sleepEllipse, sleepKleur, DragDropEffects.Move);
            }
        }

        public RelayCommand<DragEventArgs> BalKleur_Drop
        {
            get { return new RelayCommand<DragEventArgs>(Ellipse_Drop); }
        }

        private void Ellipse_Drop(DragEventArgs e)
        {
            if (e.Data.GetDataPresent("balKleur"))
            {
                var bal = new Bal();
                CanvasKaart = (Canvas)e.OriginalSource;
                bal.Kleur = (SolidColorBrush)sleepEllipse.Fill;
                var pos = e.GetPosition(CanvasKaart);
                double posX = pos.X;
                bal.X = posX;
                double posY = pos.Y;
                bal.Y = posY;
                Ballen.Add(bal);
                EnableMenu = true;
            }
        }

        public RelayCommand KerstkaartCommand
        {
            get { return new RelayCommand(Kerstkaart); }
        }

        private void Kerstkaart()
        {
            TypeKaart = @"../images/kerstkaart.jpg";
            Wens = "Wens intikken";
            BalKleur = null;
            Lettertype = null;
            Lettergrootte = 12;
            KaartZichtbaarheid = 1;
            EnableMenu = false;
            Ballen.Clear();
            Statusbalk = "Nieuw";
        }

        public RelayCommand GeboortekaartCommand
        {
            get { return new RelayCommand(Geboortekaart); }
        }

        private void Geboortekaart()
        {
            TypeKaart = @"../images/geboortekaart.jpg";
            Wens = "Wens intikken";
            BalKleur = null;
            Lettertype = null;
            Lettergrootte = 12;
            KaartZichtbaarheid = 1;
            EnableMenu = false;
            Ballen.Clear();
            Statusbalk = "Nieuw";
        }

        public RelayCommand MeerCommand
        { get { return new RelayCommand(MeerFontSize); } }
        private void MeerFontSize()
        {
            if (Lettergrootte < 40)
                Lettergrootte++;
            
        }

        public RelayCommand MinderCommand
        { get { return new RelayCommand(MinderFontSize); } }
        private void MinderFontSize()
        {
            if (Lettergrootte > 10)
                Lettergrootte--;

        }

        public RelayCommand NieuwCommand
        {
            get { return new RelayCommand(Nieuw); }
        }

        private void Nieuw()
        {
            Wens = "Wens intikken";
            BalKleur = null;
            Lettertype = null;
            Lettergrootte = 12;
            EnableMenu = false;
            Statusbalk = "Nieuw";
            Ballen.Clear();
        }

        public RelayCommand OpenenCommand
        {
            get { return new RelayCommand(Openen); }
        }

        private void Openen()
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.FileName = "";
                dlg.DefaultExt = ".txt";
                dlg.Filter = "Wenskaarten |*.txt";
                if (dlg.ShowDialog() == true)
                {
                    using (StreamReader bestand = new StreamReader(dlg.FileName))
                    {
                        TypeKaart = bestand.ReadLine();
                        var aantalBallen = Convert.ToInt32(bestand.ReadLine());
                        Ballen.Clear();
                        for(var i = 0; i < aantalBallen; i++)
                        {
                            string[] prop = bestand.ReadLine().Split('*');
                            var bal = new Bal();
                            bal.Kleur = (SolidColorBrush)(new BrushConverter().ConvertFrom(prop[0]));
                            bal.X = Convert.ToDouble(prop[1]);
                            bal.Y = Convert.ToDouble(prop[2]);
                            Ballen.Add(bal);
                        }
                        Wens = bestand.ReadLine();
                        Lettertype = bestand.ReadLine();
                        Lettergrootte = Convert.ToInt32(bestand.ReadLine());
                    }
                }
                Statusbalk = dlg.FileName.ToString();
                KaartZichtbaarheid = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("openen mislukt : " + ex.Message);
            }
        }

        public RelayCommand OpslaanCommand
        {
            get { return new RelayCommand(Opslaan); }
        }

        private void Opslaan()
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();

                dlg.FileName = ".txt";
                dlg.DefaultExt = ".txt";
                dlg.Filter = "Wenskaarten |*.txt";

                if (dlg.ShowDialog() == true)
                {
                    using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                    {
                        bestand.WriteLine(TypeKaart);
                        bestand.WriteLine(Ballen.Count);
                        if (Ballen.Count > 1)
                        {
                            foreach(var bal in Ballen)
                            {
                                bestand.WriteLine(bal.Kleur.ToString() + "*" + bal.X.ToString() + "*" + bal.Y.ToString());
                            }
                        }
                        bestand.WriteLine(Wens);
                        bestand.WriteLine(Lettertype.ToString());
                        bestand.WriteLine(Lettergrootte);
                    }
                }
                Statusbalk = dlg.FileName.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("opslaan mislukt: " + ex.Message);
            }
        }

        public RelayCommand AfsluitenCommand
        {
            get { return new RelayCommand(AfsluitenApp); }
        }

        private void AfsluitenApp()
        {
            Application.Current.MainWindow.Close();
        }

        public RelayCommand<CancelEventArgs> ClosingCommand
        {
            get { return new RelayCommand<CancelEventArgs>(OnWindowClosing); }
        }

        public void OnWindowClosing(CancelEventArgs e)
        {
            if (MessageBox.Show("Wilt u het programma sluiten ?", "Afsluiten", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                e.Cancel = true;
        }
    }
}
