using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projekat3
{
    /// <summary>
    /// Interaction logic for DodajLekWindow.xaml
    /// </summary>
    public partial class DodajLekWindow : Window
    {
        private MainWindow rootWindow; 
        private bool isIzmeni;

        public DodajLekWindow(MainWindow rootWindow, bool isIzmeni)
        {
            this.isIzmeni = isIzmeni;
            InitializeComponent();
            this.rootWindow = rootWindow;
            if (isIzmeni)
            {
                popupNazivTextBox.Text = rootWindow.nazivLekaTextBox.Text;
                popupDatumProizvodnjeTextBox.Text = rootWindow.datumProizvodnjeTextBox.Text;
                popupDatumVazenjaTextBox.Text = rootWindow.datumVazenjaTextBox.Text;
                popupKolicinaTextBox.Text = rootWindow.kolicinaTextBox.Text;
            }
        }

        private void potvrdiNoviLekBtn_Click(object sender, RoutedEventArgs e)
        {
            Lek novi = new Lek(popupNazivTextBox.Text, popupDatumProizvodnjeTextBox.Text, popupDatumVazenjaTextBox.Text, Int32.Parse(popupKolicinaTextBox.Text));

            if (isIzmeni)
            {
                rootWindow.Lekovi[rootWindow.lekoviListBox.SelectedIndex].Naziv = novi.Naziv;
                rootWindow.Lekovi[rootWindow.lekoviListBox.SelectedIndex].DatumProizvodnje = novi.DatumProizvodnje;
                rootWindow.Lekovi[rootWindow.lekoviListBox.SelectedIndex].DatumVazenja = novi.DatumVazenja;
                rootWindow.Lekovi[rootWindow.lekoviListBox.SelectedIndex].Kolicina = novi.Kolicina;
            }
            else
            {
                rootWindow.Apoteke[rootWindow.apotekeListBox.SelectedIndex].Lekovi.Add(novi);
            }
            rootWindow.Export("../Apoteke.txt", String.Join("*", rootWindow.Apoteke));
            Close();
            
        }

        private void popupNazivTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            check();
        }

        private void popupDatumProizvodnjeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            check();
        }

        private void popupDatumVazenjaTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            check();
        }

        private void popupKolicinaTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            check();
        }

        private void check()
        {
            potvrdiNoviLekBtn.IsEnabled = Int32.TryParse(popupKolicinaTextBox.Text, out int res)
                && String.IsNullOrEmpty(popupNazivTextBox.Text.Trim()) == false
                && String.IsNullOrEmpty(popupKolicinaTextBox.Text.Trim()) == false
                && String.IsNullOrEmpty(popupDatumVazenjaTextBox.Text.Trim()) == false
                && String.IsNullOrEmpty(popupDatumProizvodnjeTextBox.Text.Trim()) == false;
        }
    }
}
