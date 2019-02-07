using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingBonMVVM.Model
{
    public class ParkingBon
    {
        public DateTime DatumBon { get; set; }
        public string Aankomsttijd { get; set; }
        public string Bedrag { get; set; }
        public string Vertrektijd { get; set; }
        public Boolean EnableOpslaan { get; set; }
    }
}
