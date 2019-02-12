using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Wenskaarten.Model
{
    public class Wenskaart
    {
        public string TypeKaart { get; set; }
        public string Wens { get; set; }
        public Kleur BalKleur { get; set; }
        public string Lettertype { get; set; }
        public int Lettergrootte { get; set; }
    }
}
