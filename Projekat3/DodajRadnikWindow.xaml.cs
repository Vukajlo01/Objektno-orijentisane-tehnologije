using Microsoft.Win32;
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
    /// Interaction logic for DodajRadnikWindow.xaml
    /// </summary>
    public partial class DodajRadnikWindow : Window
    {
        private MainWindow rootWindow;
        private bool isIzmeni;
        private string filePath = "";

        public DodajRadnikWindow(MainWindow rootWindow, bool isIzmeni)
        {
            this.isIzmeni = isIzmeni;
            InitializeComponent();
            this.rootWindow = rootWindow;
            if (isIzmeni)
            {
                filePath = rootWindow.Image2.Source.ToString();
                popupImeTextBox.Text = rootWindow.imeTextBox.Text;
                popupPrezimeTextBox.Text = rootWindow.prezimeTextBox.Text;
                popupDatumTextBox.Text = rootWindow.datumPocetkaRadaTextBox.Text;
                popupJmbgTextBox.Text = rootWindow.jmbgTextBox.Text;
            }
        }

        private void potvrdiNoviRadnikBtn_Click(object sender, RoutedEventArgs e)
        {
            Radnik r = new Radnik(popupImeTextBox.Text, popupPrezimeTextBox.Text, popupDatumTextBox.Text, popupJmbgTextBox.Text, filePath);

            if (isIzmeni)
            {
                rootWindow.Radnici[rootWindow.radniciListBox.SelectedIndex].Ime = r.Ime;
                rootWindow.Radnici[rootWindow.radniciListBox.SelectedIndex].Prezime = r.Prezime;
                rootWindow.Radnici[rootWindow.radniciListBox.SelectedIndex].Datum = r.Datum;
                rootWindow.Radnici[rootWindow.radniciListBox.SelectedIndex].Jmbg = r.Jmbg;
                rootWindow.Radnici[rootWindow.radniciListBox.SelectedIndex].PathSlika = r.PathSlika;
            }
            else
            {
                rootWindow.Apoteke[rootWindow.apotekeListBox.SelectedIndex].Radnici.Add(r);
            }

            rootWindow.Export("../Apoteke.txt", String.Join("*", rootWindow.Apoteke));
            Close();
        }

        private void browse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new OpenFileDialog();

            bool? response = openFileDialog.ShowDialog();

            if (response == true)
            {

                string dialogString = openFileDialog.SafeFileName;

                string relPath = "../../../SlikeRadnici/" + dialogString;

                filePath = relPath;
                
                check();
            }
        }

        private void popupImeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            check();
        }

        private void popupPrezimeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            check();
        }

        private void popupDatumTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            check();
        }

        private void popupJmbgTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            check();
        }

        private void check()
        {
            potvrdiNoviRadnikBtn.IsEnabled = Int32.TryParse(popupJmbgTextBox.Text, out int res)
                && String.IsNullOrEmpty(popupImeTextBox.Text.Trim()) == false
                && String.IsNullOrEmpty(popupPrezimeTextBox.Text.Trim()) == false
                && String.IsNullOrEmpty(popupDatumTextBox.Text.Trim()) == false
                && String.IsNullOrEmpty(popupJmbgTextBox.Text.Trim()) == false
                && String.IsNullOrEmpty(filePath.Trim()) == false;
        }
    }
}
