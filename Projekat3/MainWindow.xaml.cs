using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekat3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Lek> lekovi;
        private ObservableCollection<Radnik> radnici;

        public ObservableCollection<Apoteka> Apoteke { get; set; }

        public ObservableCollection<Lek> Lekovi {
            get { return this.lekovi; }
            set
            {
                if (this.lekovi != value)
                {
                    this.lekovi = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public ObservableCollection<Radnik> Radnici
        {
            get { return this.radnici; }
            set
            {
                if (this.radnici != value)
                {
                    this.radnici = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public MainWindow()
        {
            Lekovi = new ObservableCollection<Lek>();
            Radnici = new ObservableCollection<Radnik>();
            Apoteke = new ObservableCollection<Apoteka>();


            InitializeComponent();
            DataContext = this;

            try {
                
                Import("../Apoteke.txt");

            }
            catch
            {
                MessageBox.Show("File not found");
            }

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Import(string file)
        {
            Apoteke.Clear();

            string readText = File.ReadAllText(file);

            string[] apotekePodeljene = readText.Split("*");

            for (int i = 0; i < apotekePodeljene.Length; i++)
            {

                string[] linije;
                string[] deloviApoteke;
                string[] deloviLeka;
                string[] deloviRadnika;
                string apotekaLinija;
                string radniciLinija;
                string lekoviLinija;
                string[] lekovi;
                string[] radnici;

                linije = apotekePodeljene[i].Split("\n");

                apotekaLinija = linije[0];
                lekoviLinija = linije[1];
                radniciLinija = linije[2];

                deloviApoteke = apotekaLinija.Split(",");


                Apoteka a = new Apoteka(Int32.Parse(deloviApoteke[1]), deloviApoteke[0], deloviApoteke[2], deloviApoteke[3]);

                lekovi = lekoviLinija.Split("|");
                for (int j = 0; j < lekovi.Length; j++)
                {
                    deloviLeka = lekovi[j].Split(",");
                    Lek l = new Lek(deloviLeka[0], deloviLeka[1], deloviLeka[2], Int32.Parse(deloviLeka[3]));
                    a.Lekovi.Add(l);
                }
                radnici = radniciLinija.Split("|");
                for (int k = 0; k < radnici.Length; k++)
                {
                    deloviRadnika = radnici[k].Split(",");
                    Radnik r = new Radnik(deloviRadnika[0], deloviRadnika[1], deloviRadnika[2], deloviRadnika[3], deloviRadnika[4]);
                    a.Radnici.Add(r);
                }

                Apoteke.Add(a);
            }
        }
        public void Export(string file, string txt)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(file);
                sw.Write(txt);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace);
                MessageBox.Show("ERROR");
            }
            finally
            {
                try { sw.Close(); } catch (Exception e) { }

            }
        }

        private void apotekeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            obrisiLekBtn.IsEnabled = false;
            obirisiRadnikBtn.IsEnabled = false;
            izmeniLekBtn.IsEnabled = false;
            izmeniRadnikBtn.IsEnabled = false;
            dodajLekBtn.IsEnabled = true;
            dodajRadnikBtn.IsEnabled = true;

            pretragaPoImenuIPrezimenuTextBox.Text = "";
            pretragaPoNazivuTextBox.Text = "";

            Lekovi = Apoteke[apotekeListBox.SelectedIndex].Lekovi;
            Radnici = Apoteke[apotekeListBox.SelectedIndex].Radnici;
        }

        private void lekoviListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lekoviListBox.SelectedIndex != -1)
            {
                obrisiLekBtn.IsEnabled = true;
                izmeniLekBtn.IsEnabled = true;
            }
        }

        private void radniciListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (radniciListBox.SelectedIndex != -1)
            {
                obirisiRadnikBtn.IsEnabled = true;
                izmeniRadnikBtn.IsEnabled = true;       
            }
        }

        private void pretragaPoNazivuTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (apotekeListBox.SelectedIndex != -1)
            {
                ObservableCollection<Lek> pretrazenaLista = new ObservableCollection<Lek>();
               
                if (pretragaPoNazivuTextBox.Text.Equals(""))
                {
                    pretrazenaLista = Apoteke[apotekeListBox.SelectedIndex].Lekovi;
                }
                else
                {
                    foreach (Lek lek in Apoteke[apotekeListBox.SelectedIndex].Lekovi)
                    {

                        if (lek.Naziv.ToLower().Contains(pretragaPoNazivuTextBox.Text.ToLower()))
                        {
                            pretrazenaLista.Add(lek);

                        }
                    }
                }

                Lekovi = pretrazenaLista;

            }
        }

        private void pretragaPoImenuIPrezimenuTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (apotekeListBox.SelectedIndex != -1)
            {
                ObservableCollection<Radnik> pretrazenaLista = new ObservableCollection<Radnik>();

                if (pretragaPoImenuIPrezimenuTextBox.Text.Equals(""))
                {
                    pretrazenaLista = Apoteke[apotekeListBox.SelectedIndex].Radnici;
                }
                else
                {
                    foreach (Radnik r in Apoteke[apotekeListBox.SelectedIndex].Radnici)
                    {

                        if (r.Ime.ToLower().Contains(pretragaPoImenuIPrezimenuTextBox.Text.ToLower()) ||
                                r.Prezime.ToLower().Contains(pretragaPoImenuIPrezimenuTextBox.Text.ToLower()))
                        {
                            pretrazenaLista.Add(r);

                        }
                    }
                }

                Radnici = pretrazenaLista;

            }
        }

        private void dodajLekBtn_Click(object sender, RoutedEventArgs e)
        {
            DodajLekWindow popup = new DodajLekWindow(this, false);
            popup.Show();
        }

        private void izmeniLekBtn_Click(object sender, RoutedEventArgs e)
        {
            DodajLekWindow popup = new DodajLekWindow(this, true);
            popup.Show();
        }

        private void izmeniRadnikBtn_Click(object sender, RoutedEventArgs e)
        {
            DodajRadnikWindow popup = new DodajRadnikWindow(this, true);
            popup.Show();
        }

        private void dodajRadnikBtn_Click(object sender, RoutedEventArgs e)
        {
            DodajRadnikWindow popup = new DodajRadnikWindow(this, false);
            popup.Show();
        }

        private void obirisiRadnikBtn_Click(object sender, RoutedEventArgs e)
        {
            Apoteke[apotekeListBox.SelectedIndex].Radnici.Remove(Radnici[radniciListBox.SelectedIndex]);
            Export("../Apoteke.txt", String.Join("*", Apoteke));

            pretragaPoImenuIPrezimenuTextBox.Text = "";

        }


        private void obrisiLekBtn_Click(object sender, RoutedEventArgs e)
        {
            Apoteke[apotekeListBox.SelectedIndex].Lekovi.Remove(Lekovi[lekoviListBox.SelectedIndex]);
            Export("../Apoteke.txt", String.Join("*", Apoteke));

            pretragaPoNazivuTextBox.Text = "";
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
