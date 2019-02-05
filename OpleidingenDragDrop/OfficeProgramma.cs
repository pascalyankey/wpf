using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace WPFCursus
{
    public class OfficeProgramma
    {
        public string naam { get; set; }
        public BitmapImage symbool { get; set; }

        public OfficeProgramma(string nnaam, BitmapImage nsymbool)
        {
            naam = nnaam;
            symbool = nsymbool;
        }
    }
}
