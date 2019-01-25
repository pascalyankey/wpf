using System.Windows.Media.Imaging;

namespace HobbyLijst
{
    public class Hobby
    {
        public string Categorie { get; set; }
        public string Activiteit { get; set; }
        public BitmapImage Symbool { get; set; }

        public Hobby(string nCategorie, string nActiviteit, BitmapImage nSymbool)
        {
            Categorie = nCategorie;
            Activiteit = nActiviteit;
            Symbool = nSymbool;
        }
    }
}
