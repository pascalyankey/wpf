using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public double Bedrag
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

        public RelayCommand ButtonMeerCommand
        {
            get { return new RelayCommand(MeerCommand); }
        }

        private void MeerCommand()
        {
            DateTime vertrekuur = Convert.ToDateTime(Vertrektijd).AddHours(0.5 * Bedrag);

            if (vertrekuur.Hour < 22)
                Bedrag += 1;
        }

        public RelayCommand ButtonMinderCommand
        {
            get { return new RelayCommand(MinderCommand); }
        }

        private void MinderCommand()
        {
            if (Bedrag > 0)
                Bedrag -= 1;
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
