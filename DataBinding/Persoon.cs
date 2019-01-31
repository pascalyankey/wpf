using System;
using System.ComponentModel;

namespace DataBinding
{
    public class Persoon : INotifyPropertyChanged
    {
        public Persoon(string nNaam, decimal nWedde, DateTime nInDienst)
        {
            Naam = nNaam;
            Wedde = nWedde;
            InDienst = nInDienst;
        }

        private string naamValue;
        public string Naam
        {
            get
            {
                return naamValue;
            }
            set
            {
                naamValue = value;
                RaisePropertyChanged("Naam");
            }
        }
        private decimal weddeValue;
        public decimal Wedde
        {
            get { return weddeValue; }
            set
            {
                weddeValue = value;
                RaisePropertyChanged("Wedde");
            }
        }
        private DateTime inDienstValue;
        public DateTime InDienst
        {
            get { return inDienstValue; }
            set
            {
                inDienstValue = value;
                RaisePropertyChanged("InDienst");
            }
        }

        private void RaisePropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
