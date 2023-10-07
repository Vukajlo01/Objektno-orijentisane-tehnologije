using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Projekat3
{
    public class Radnik : INotifyPropertyChanged
    {
        private string ime, prezime, datum;
        private string jmbg;
        private string pathSlika;


        public event PropertyChangedEventHandler? PropertyChanged;

        public Radnik(string ime, string prezime, string datum, string jmbg, string pathSlika)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.datum = datum;
            this.jmbg = jmbg;
            this.pathSlika = pathSlika;
        }

        public string Ime
        {
            get { return this.ime; }
            set
            {
                if (this.ime != value)
                {
                    this.ime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Prezime
        {
            get { return this.prezime; }
            set
            {
                if (this.prezime != value)
                {
                    this.prezime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Datum
        {
            get { return this.datum; }
            set
            {
                if (this.datum != value)
                {
                    this.datum = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Jmbg
        {
            get { return this.jmbg; }
            set
            {
                if (this.jmbg != value)
                {
                    this.jmbg = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string PathSlika
        {
            get { return this.pathSlika; }
            set
            {
                if (this.pathSlika != value)
                {
                    this.pathSlika = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public override string ToString()
        {
            return ime + "," + prezime + "," + datum + "," + jmbg + "," + pathSlika;
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Console.WriteLine(propertyName);
        }
    }
}
