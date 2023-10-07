using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Projekat3
{
    public class Lek : INotifyPropertyChanged
    {
        private string naziv, datumProizvodnje, datumVazenja;
        private int kolicina;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Lek(string naziv, string datumProizvodnje, string datumVazenja, int kolicina)
        {
            this.naziv = naziv;
            this.datumProizvodnje = datumProizvodnje;
            this.datumVazenja = datumVazenja;
            this.kolicina = kolicina;
        }

        public string Naziv
        {
            get { return this.naziv; }
            set
            {
                if (this.naziv != value)
                {
                    this.naziv = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string DatumProizvodnje
        {
            get { return this.datumProizvodnje; }
            set
            {
                if (this.datumProizvodnje != value)
                {
                    this.datumProizvodnje = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string DatumVazenja
        {
            get { return this.datumVazenja; }
            set
            {
                if (this.datumVazenja != value)
                {
                    this.datumVazenja = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Kolicina
        {
            get { return this.kolicina; }
            set
            {
                if (this.kolicina != value)
                {
                    this.kolicina = value;
                    NotifyPropertyChanged();
                }
            }
        }        

        public override string ToString()
        {
            return naziv + "," + datumProizvodnje + "," + datumVazenja + "," + kolicina;
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
