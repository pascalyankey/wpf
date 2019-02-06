using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace MVVMHobby.ViewModel
{
    public class HobbyLijstVM : ViewModelBase
    {
        public HobbyLijstVM()
        {
            HobbyLijst.Add(new HobbyVM(new Model.Hobby("sport", "voetbal", new BitmapImage(new Uri("pack://application:,,,/Images/voetbal.jpg", UriKind.Absolute)))));
            HobbyLijst.Add(new HobbyVM(new Model.Hobby("sport", "atletiek", new BitmapImage(new Uri("pack://application:,,,/Images/atletiek.jpg", UriKind.Absolute)))));
            HobbyLijst.Add(new HobbyVM(new Model.Hobby("sport", "basketbal", new BitmapImage(new Uri("pack://application:,,,/Images/basketbal.jpg", UriKind.Absolute)))));
            HobbyLijst.Add(new HobbyVM(new Model.Hobby("sport", "tennis", new BitmapImage(new Uri("pack://application:,,,/Images/tennis.jpg", UriKind.Absolute)))));
            HobbyLijst.Add(new HobbyVM(new Model.Hobby("sport", "turnen", new BitmapImage(new Uri("pack://application:,,,/Images/turnen.jpg", UriKind.Absolute)))));
            HobbyLijst.Add(new HobbyVM(new Model.Hobby("muziek", "trompet", new BitmapImage(new Uri("pack://application:,,,/Images/trompet.jpg", UriKind.Absolute)))));
            HobbyLijst.Add(new HobbyVM(new Model.Hobby("muziek", "drum", new BitmapImage(new Uri("pack://application:,,,/Images/drum.jpg", UriKind.Absolute)))));
            HobbyLijst.Add(new HobbyVM(new Model.Hobby("muziek", "gitaar", new BitmapImage(new Uri("pack://application:,,,/Images/gitaar.jpg", UriKind.Absolute)))));
            HobbyLijst.Add(new HobbyVM(new Model.Hobby("muziek", "piano", new BitmapImage(new Uri("pack://application:,,,/Images/piano.jpg", UriKind.Absolute)))));
        }

        private ObservableCollection<HobbyVM> hobbyLijst = new ObservableCollection<HobbyVM>();
        public ObservableCollection<HobbyVM> HobbyLijst
        {
            get
            {
                return hobbyLijst;
            }
            set
            {
                hobbyLijst = value;
                RaisePropertyChanged("HobbyLijst");
            }
        }

        private HobbyVM selectedHobby;
        public HobbyVM SelectedHobby
        {
            get
            {
                return selectedHobby;
            }
            set
            {
                selectedHobby = value;
                RaisePropertyChanged("SelectedHobby");
            }
        }

        public RelayCommand<RoutedEventArgs> VerwijderCommand
        {
            get { return new RelayCommand<RoutedEventArgs>(Verwijder); }
        }

        private void Verwijder(RoutedEventArgs e)
        {
            HobbyLijst.Remove(SelectedHobby);
        }

        View.ImageView groteView;
        public RelayCommand<MouseEventArgs> MouseDownCommand
        {
            get { return new RelayCommand<MouseEventArgs>(MuisIn); }
        }

        private void MuisIn(MouseEventArgs e)
        {
            Image tg = (Image)e.OriginalSource;
            groteView = new View.ImageView();
            groteView.GroteImage.Source = tg.Source;
            groteView.Show();
        }

        public RelayCommand<MouseEventArgs> MouseUpCommand
        {
            get { return new RelayCommand<MouseEventArgs>(MuisUit); }
        }

        private void MuisUit(MouseEventArgs e)
        {
            if (groteView != null)
                groteView.Close();
            groteView = null;
        }
    }
}
