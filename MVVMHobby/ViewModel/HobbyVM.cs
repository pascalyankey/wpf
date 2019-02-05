using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;

namespace MVVMHobby.ViewModel
{
    public class HobbyVM : ViewModelBase
    {
        private Model.Hobby hobby;
        public HobbyVM(Model.Hobby nhobby)
        {
            this.hobby = nhobby;
        }

        public string Categorie
        {
            get
            {
                return hobby.Categorie;
            }
            set
            {
                hobby.Categorie = value;
                RaisePropertyChanged("Categorie");
            }
        }

        public string Activiteit
        {
            get
            {
                return hobby.Activiteit;
            }
            set
            {
                hobby.Activiteit = value;
                RaisePropertyChanged("Activiteit");
            }
        }

        public BitmapImage Symbool
        {
            get
            {
                return hobby.Symbool;
            }
            set
            {
                hobby.Symbool = value;
                RaisePropertyChanged("Symbool");
            }
        }
    }
}
