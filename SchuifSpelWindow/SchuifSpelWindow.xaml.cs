using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchuifSpel
{
    /// <summary>
    /// Interaction logic for SchuifSpelWindow.xaml
    /// </summary>
    public partial class SchuifSpelWindow : Window
    {
        private int sleeprij, sleepkolom, legerij, legekolom;
        public SchuifSpelWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Shuffle();
        }

        private void Check()
        {
            int irij, ikolom, grij, gkolom;
            int aantalfout = 0;
            foreach (Image stukje in puzzelGrid.Children)
            {
                irij = Convert.ToInt16(stukje.Name.Substring(4, 1));
                ikolom = Convert.ToInt16(stukje.Name.Substring(5, 1));
                grij = Grid.GetRow(stukje);
                gkolom = Grid.GetColumn(stukje);
                if ((irij != grij) || (ikolom != gkolom))
                {
                    aantalfout++;
                }
            }
            if (aantalfout == 0)
                Oplossing();
        }

        private void OplossingButton_Click(object sender, RoutedEventArgs e)
        {
            Oplossing();
        }

        private void Oplossing()
        {
            puzzelGrid.Children.Clear();
            for (int r = 0; r <= 3; r++)
            {
                for (int k = 0; k <= 3; k++)
                {
                    Image stuk = new Image();
                    BitmapImage bi = new BitmapImage(new Uri(@"images/vdab" + r + k + ".jpg", UriKind.Relative));
                    stuk.Name = "stuk" + r + k;
                    stuk.Source = bi;
                    ZetImage(r, k, stuk);
                }
            }
        }

        private void ZetImage(int rij, int kolom, Image zetstuk)
        {
            Image stuk = new Image();
            stuk = zetstuk;
            Grid.SetColumn(stuk, kolom);
            Grid.SetRow(stuk, rij);
            if (stuk.Name == "stuk33")
            {
                stuk.Drop += PuzzelGrid_Drop;
                stuk.AllowDrop = true;
            }
            else
            {
                stuk.MouseMove += Stuk_MouseMove;
                stuk.AllowDrop = false;
            }
            puzzelGrid.Children.Add(stuk);
        }

        private void Shuffle()
        {
            puzzelGrid.Children.Clear();
            int[,] checken = new int[4, 4];
            for (int r = 0; r <= 3; r++)
            {
                for (int k = 0; k <= 3; k++)
                {
                    checken[r, k] = 0;
                }
            }
            checken[3, 3] = 1;

            Random rnd = new Random();
            int rij, kolom;
            for (int r = 0; r <= 3; r++)
            {
                for (int k = 0; k <= 3; k++)
                {
                    if (k < 3 || r < 3)
                    {
                        do
                        {
                            rij = rnd.Next(0, 4);
                            kolom = rnd.Next(0, 4);
                        } while (checken[rij, kolom] == 1);

                        checken[rij, kolom] = 1;
                        Image stuk = new Image();
                        BitmapImage bi = new BitmapImage(new Uri(@"images/vdab" + r + k + ".jpg", UriKind.Relative));
                        stuk.Name = "stuk" + r + k;
                        stuk.Source = bi;
                        ZetImage(rij, kolom, stuk);
                    }
                }
            }

            Image leegstuk = new Image();
            BitmapImage bl = new BitmapImage(new Uri(@"images/leeg33.jpg", UriKind.Relative));
            leegstuk.Name = "stuk33";
            leegstuk.Source = bl;
            ZetImage(3, 3, leegstuk);

            legerij = 3;
            legekolom = 3;
        }

        private void ShuffleButton_Click(object sender, RoutedEventArgs e)
        {
            Shuffle();
        }

        private void Stuk_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Image stuk = (Image)sender;
                sleeprij = Grid.GetRow(stuk);
                sleepkolom = Grid.GetColumn(stuk);
                DataObject sleepstuk = new DataObject("sleepstuk", stuk);
                DragDrop.DoDragDrop(stuk, sleepstuk, DragDropEffects.Move);
            }
        }

        private void PuzzelGrid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("sleepstuk"))
            {
                if (Geldig())
                {
                    Image gesleeptstuk = (Image)e.Data.GetData("sleepstuk");
                    Image dropstuk = (Image)sender;
                    legerij = Grid.GetRow(dropstuk);
                    legekolom = Grid.GetColumn(dropstuk);
                    puzzelGrid.Children.Remove(gesleeptstuk);
                    puzzelGrid.Children.Remove(dropstuk);
                    ZetImage(legerij, legekolom, gesleeptstuk);
                    ZetImage(sleeprij, sleepkolom, dropstuk);
                    legerij = sleeprij;
                    legekolom = sleepkolom;
                    Check();
                }
            }
        }

        private Boolean Geldig()
        {
            if (((sleeprij + 1 == legerij) || (sleeprij - 1 == legerij)) && (sleepkolom == legekolom))
            {
                return true;
            }
            else
            {
                if (((sleepkolom + 1 == legekolom) || (sleepkolom - 1 == legekolom)) && (sleeprij == legerij))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
