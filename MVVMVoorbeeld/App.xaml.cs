using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MVVMVoorbeeld
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Model.TekstMetOpmaak mijnTekst = new Model.TekstMetOpmaak();
            ViewModel.TekstMetOpmaakVM vm = new ViewModel.TekstMetOpmaakVM(mijnTekst);
            View.TextBoxView mijnTekstboxView = new View.TextBoxView();
            mijnTekstboxView.DataContext = vm;
            mijnTekstboxView.Show();
        }
    }
}
