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

namespace RegenboogDragDrop
{
    /// <summary>
    /// Interaction logic for WindowRegenboog.xaml
    /// </summary>
    public partial class WindowRegenboog : Window
    {
        public WindowRegenboog()
        {
            InitializeComponent();
        }

        private Rectangle sleeprechthoek = new Rectangle();

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            sleeprechthoek = (Rectangle)sender;
            if ((e.LeftButton == MouseButtonState.Pressed) && (sleeprechthoek.Fill != Brushes.White))
            {
                DataObject sleepKleur = new DataObject("deKleur", sleeprechthoek.Fill);
                DragDrop.DoDragDrop(sleeprechthoek, sleepKleur, DragDropEffects.Move);
            }
        }

        private void Rectangle_DragEnter(object sender, DragEventArgs e)
        {
            Rectangle kader = (Rectangle)sender;
            kader.StrokeThickness = 5;
        }

        private void Rectangle_DragLeave(object sender, DragEventArgs e)
        {
            Rectangle kader = (Rectangle)sender;
            kader.StrokeThickness = 3;
        }

        private void Rectangle_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("deKleur"))
            {
                Brush gesleepteKleur = (Brush)e.Data.GetData("deKleur");
                Rectangle rechthoek = (Rectangle)sender;
                if (rechthoek.Fill == Brushes.White)
                {
                    rechthoek.Fill = gesleepteKleur;
                    sleeprechthoek.Fill = Brushes.White;
                }
                rechthoek.StrokeThickness = 3;
            }
        }

        private void ButtonCheck_Click(object sender, RoutedEventArgs e)
        {
            foreach (Rectangle rechthoek in DropZone.Children)
            {
                string naam = rechthoek.Name.Substring(4);
                Brush naamkleur = (Brush)new BrushConverter().ConvertFromString(naam);
                Brush kleur = rechthoek.Fill;
                if (naamkleur == kleur)
                {
                    rechthoek.Stroke = Brushes.Green;
                }
                else
                {
                    rechthoek.Stroke = Brushes.Red;
                }
            }
        }
    }
}

