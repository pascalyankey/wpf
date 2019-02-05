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

namespace WPFCursus
{
    /// <summary>
    /// Interaction logic for OpleidingenWindow.xaml
    /// </summary>
    public partial class OpleidingenWindow : Window
    {
        public OpleidingenWindow()
        {
            InitializeComponent();
            ListBoxProgrammas.Items.Add(new OfficeProgramma("Access", new BitmapImage(new Uri(@"images\access.png", UriKind.Relative))));
            ListBoxProgrammas.Items.Add(new OfficeProgramma("Excel", new BitmapImage(new Uri(@"images\excel.png", UriKind.Relative))));
            ListBoxProgrammas.Items.Add(new OfficeProgramma("Groove", new BitmapImage(new Uri(@"images\groove.png", UriKind.Relative))));
            ListBoxProgrammas.Items.Add(new OfficeProgramma("InfoPath", new BitmapImage(new Uri(@"images\infopath.png", UriKind.Relative))));
            ListBoxProgrammas.Items.Add(new OfficeProgramma("OneNote", new BitmapImage(new Uri(@"images\onenote.png", UriKind.Relative))));
            ListBoxProgrammas.Items.Add(new OfficeProgramma("Outlook", new BitmapImage(new Uri(@"images\outlook.png", UriKind.Relative))));
            ListBoxProgrammas.Items.Add(new OfficeProgramma("Powerpoint", new BitmapImage(new Uri(@"images\powerpoint.png", UriKind.Relative))));
            ListBoxProgrammas.Items.Add(new OfficeProgramma("Publisher", new BitmapImage(new Uri(@"images\publisher.png", UriKind.Relative))));
            ListBoxProgrammas.Items.Add(new OfficeProgramma("Word", new BitmapImage(new Uri(@"images\word.png", UriKind.Relative))));
            ListBoxProgrammas.SelectedIndex = 0;
        }

        private ListBoxItem VindListBoxItem(Object sleepitem)
        {
            DependencyObject keuze = (DependencyObject)sleepitem;
            while (keuze != null)
            {
                if (keuze is ListBoxItem)
                    return (ListBoxItem)keuze;
                keuze = VisualTreeHelper.GetParent(keuze);
            }
            return null;
        }

        ListBox draglijst;
        private void DragListBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                draglijst = (ListBox)sender;
                ListBoxItem programmaitem = VindListBoxItem(e.OriginalSource);
                if (draglijst.SelectedIndex >= 0 && programmaitem != null)
                {
                    DataObject sleepdata = new DataObject("mijnprogramma", programmaitem.Content);
                    DragDrop.DoDragDrop(programmaitem, sleepdata, DragDropEffects.Move);
                }
            }
        }

        private void DropListBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("mijnprogramma"))
            {
                OfficeProgramma sleepprogramma = (OfficeProgramma)e.Data.GetData("mijnprogramma");
                ListBox droplijst = (ListBox)sender;
                if (draglijst != droplijst)
                {
                    droplijst.Items.Add(sleepprogramma);
                    draglijst.Items.Remove(draglijst.SelectedItem);
                }
            }
        }

        private void ButtonDoorgeven_Click(object sender, RoutedEventArgs e)
        {
            String tekst = "Volgende programma's zijn:\n";
            if (ListBoxProgrammas.Items.Count > 0)
            {
                tekst += "\nNiet toegewezen: ";
                foreach (OfficeProgramma prog in ListBoxProgrammas.Items)
                {
                    tekst += prog.naam + ", ";
                }
                tekst = tekst.Substring(0, tekst.Length - 2);
            }

            if (ListBoxGekend.Items.Count > 0)
            {
                tekst += "\nGekend: ";
                foreach (OfficeProgramma prog in ListBoxGekend.Items)
                {
                    tekst += prog.naam + ", ";
                }
                tekst = tekst.Substring(0, tekst.Length - 2);
            }
            if (ListBoxTeVolgen.Items.Count > 0)
            {
                tekst += "\nTe volgen: ";
                foreach (OfficeProgramma prog in ListBoxTeVolgen.Items)
                {
                    tekst += prog.naam + ", ";
                }
                tekst = tekst.Substring(0, tekst.Length - 2);
            }

            MessageBox.Show(tekst, "Overzicht");
        }
    }
}
