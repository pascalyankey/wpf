using System.Windows;
using System.Windows.Documents;

namespace ParkingBon
{
    /// <summary>
    /// Interaction logic for Afdrukvoorbeeld.xaml
    /// </summary>
    public partial class Afdrukvoorbeeld : Window
    {
        public Afdrukvoorbeeld()
        {
            InitializeComponent();
        }

        public IDocumentPaginatorSource AfdrukDocument
        {
            get { return printpreview.Document; }
            set { printpreview.Document = value; }
        }
    }
}
