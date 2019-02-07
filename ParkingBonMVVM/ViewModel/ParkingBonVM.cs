using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ParkingBonMVVM.ViewModel
{
    public class ParkingBonVM : ViewModelBase
    {
        private Model.ParkingBon parkingbon;
        public ParkingBonVM(Model.ParkingBon parkingbon)
        {
            this.parkingbon = parkingbon;
            this.parkingbon.DatumBon = DateTime.Now;
            this.parkingbon.Aankomsttijd = DateTime.Now.ToString("HH:mm:ss");
            this.parkingbon.Bedrag = "€ 0,00";
            this.parkingbon.Vertrektijd = DateTime.Now.ToString("HH:mm:ss");
            this.parkingbon.EnableOpslaan = false;
        }

        public DateTime DatumBon
        {
            get
            {
                return parkingbon.DatumBon;
            }
            set
            {
                parkingbon.DatumBon = value;
                RaisePropertyChanged("DatumBon");
            }
        }

        public string Aankomsttijd
        {
            get
            {
                return parkingbon.Aankomsttijd;
            }
            set
            {
                parkingbon.Aankomsttijd = value;
                RaisePropertyChanged("Aankomsttijd");
            }
        }

        public string Bedrag
        {
            get
            {
                return parkingbon.Bedrag;
            }
            set
            {
                parkingbon.Bedrag = value;
                RaisePropertyChanged("Bedrag");
            }
        }

        public string Vertrektijd
        {
            get
            {
                return parkingbon.Vertrektijd;
            }
            set
            {
                parkingbon.Vertrektijd = value;
                RaisePropertyChanged("Vertrektijd");
            }
        }

        public Boolean EnableOpslaan
        {
            get { return parkingbon.EnableOpslaan; }
            set
            {
                parkingbon.EnableOpslaan = value;
                RaisePropertyChanged("EnableOpslaan");
            }
        }

        public RelayCommand NieuwCommand
        {
            get { return new RelayCommand(Nieuw); }
        }

        private void Nieuw()
        {
            DatumBon = DateTime.Now;
            Aankomsttijd = DateTime.Now.ToString("HH:mm:ss");
            Bedrag = "€ 0,00";
            Vertrektijd = DateTime.Now.ToString("HH:mm:ss");
            EnableOpslaan = false;
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
                dlg.DefaultExt = ".bon";
                dlg.Filter = "Parkingbonnen |*.bon";
                if (dlg.ShowDialog() == true)
                {
                    using (StreamReader bestand = new StreamReader(dlg.FileName))
                    {
                        DatumBon = Convert.ToDateTime(bestand.ReadLine());
                        Aankomsttijd = Convert.ToDateTime(bestand.ReadLine()).ToString("HH:mm:ss");
                        Bedrag = "€ " + string.Format("{0:N2}", Convert.ToDecimal(bestand.ReadLine()));
                        Vertrektijd = Convert.ToDateTime(bestand.ReadLine()).ToString("HH:mm:ss");
                    }
                }
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

                dlg.FileName = DatumBon.ToString("dd-MM-yyyy") + "om" + Convert.ToDateTime(Aankomsttijd).ToString("HH-mm") + ".bon";
                dlg.DefaultExt = ".bon";
                dlg.Filter = "Parkingbonnen |*.bon";

                if (dlg.ShowDialog() == true)
                {
                    using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                    {
                        bestand.WriteLine(DatumBon.ToString("dd/MM/yyyy"));
                        bestand.WriteLine(Convert.ToDateTime(Aankomsttijd).ToString("HH:mm"));
                        Bedrag = Bedrag.Replace(",00", "");
                        bestand.WriteLine(Bedrag.Replace("€ ", ""));
                        bestand.WriteLine(Convert.ToDateTime(Vertrektijd).ToString("HH:mm"));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("opslaan mislukt: " + ex.Message);
            }
        }

        public RelayCommand ButtonMeerCommand
        {
            get { return new RelayCommand(MeerCommand); }
        }

        private void MeerCommand()
        {
            Bedrag = Bedrag.Replace(",00", "");
            int bedrag = Convert.ToInt32(Bedrag.Replace("€ ", ""));
            DateTime vertrekuur = Convert.ToDateTime(Aankomsttijd).AddHours(0.5 * bedrag);

            if (vertrekuur.Hour < 22)
                bedrag += 1;

            if (bedrag > 0)
            {
                EnableOpslaan = true;
            }
            else
            {
                EnableOpslaan = false;
            }

            Bedrag = "€ " + string.Format("{0:N2}", Convert.ToDecimal(bedrag));
            Vertrektijd = Convert.ToDateTime(Aankomsttijd).AddHours(0.5 * bedrag).ToLongTimeString();
        }

        public RelayCommand ButtonMinderCommand
        {
            get { return new RelayCommand(MinderCommand); }
        }

        private void MinderCommand()
        {
            Bedrag = Bedrag.Replace(",00", "");
            int bedrag = Convert.ToInt32(Bedrag.Replace("€ ", ""));

            if (bedrag > 0)
                bedrag -= 1;

            if (bedrag > 0)
            {
                EnableOpslaan = true;
            }
            else
            {
                EnableOpslaan = false;
            }

            Bedrag = "€ " + string.Format("{0:N2}", Convert.ToDecimal(bedrag));
            Vertrektijd = Convert.ToDateTime(Aankomsttijd).AddHours(0.5 * bedrag).ToLongTimeString();
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
            if (MessageBox.Show("Afsluiten", "Wilt u het programma sluiten ?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                e.Cancel = true;
        }
    }
}
