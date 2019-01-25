using System.Windows.Media.Imaging;

namespace TelefoonWindowOpdracht
{
    public class Persoon
    {
        public string Naam { get; set; }
        public string Telefoonnr { get; set; }
        public string Groep { get; set; }
        public BitmapImage Foto { get; set; }

        public Persoon(string nNaam, string nTelefoonnr, string nGroep, BitmapImage nFoto)
        {
            Naam = nNaam;
            Telefoonnr = nTelefoonnr;
            Groep = nGroep;
            Foto = nFoto;
        }
    }
}
