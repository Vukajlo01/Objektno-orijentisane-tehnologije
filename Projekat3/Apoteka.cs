using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Projekat3
{
    public class Apoteka : INotifyPropertyChanged
    {
        private int godinaOsnivanja;
        private string nazivApoteke, adresa;
        private string pathSlika;

        private ObservableCollection<Lek> lekovi;
        private ObservableCollection<Radnik> radnici;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Apoteka(int godinaOsnivanja, string nazivApoteke, string adresa, string slika)
        {
            this.godinaOsnivanja = godinaOsnivanja;
            this.nazivApoteke = nazivApoteke;
            this.adresa = adresa;
            this.PathSlika = slika;
            Lekovi = new ObservableCollection<Lek>();
            Radnici = new ObservableCollection<Radnik>();
        }


        public string NazivApoteke
        {
            get { return this.nazivApoteke; }
            set
            {
                if (this.nazivApoteke != value)
                {
                    this.nazivApoteke = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int GodinaOsnivanja
        {
            get { return this.godinaOsnivanja; }
            set
            {
                if (this.godinaOsnivanja != value)
                {
                    this.godinaOsnivanja = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Adresa
        {
            get { return this.adresa; }
            set
            {
                if (this.adresa != value)
                {
                    this.adresa = value;
                    NotifyPropertyChanged();
                }
            }
        }

        internal ObservableCollection<Lek> Lekovi { get => lekovi; set => lekovi = value; }
        internal ObservableCollection<Radnik> Radnici { get => radnici; set => radnici = value; }
        public string PathSlika { get => pathSlika; set => pathSlika = value; }

        public override string ToString()
        {
            string retVal = "";

            retVal += nazivApoteke + "," + godinaOsnivanja.ToString() + "," + adresa + "," + pathSlika + "\n";

            retVal += String.Join("|", lekovi);

            retVal += "\n";

            retVal += String.Join("|", radnici);

            retVal += "\n";

            return retVal;
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
