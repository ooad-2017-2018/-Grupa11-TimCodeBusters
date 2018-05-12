﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Match2Date.Enumeration;
using Windows.UI.Xaml.Media.Imaging;

namespace Match2Date.AzureDB
{
    public class korisnici
    {
        String id;
        String ime;
        String prezime;
        String grad;
        String email;
        String sifra;
        DateTime datumRodjenja;
        Spol spol;
        String opis;
        Double ocjena;
        bool aktivan;
        private String slika;

        public String Id { get => id; set => id = value; }
        public String Ime { get => ime; set => ime = value; }
        public String Prezime { get => prezime; set => prezime = value; }
        public String Grad { get => grad; set => grad = value; }
        public String Email { get => email; set => email = value; }
        public String Sifra { get => sifra; set => sifra = value; }
        public DateTime DatumRodjenja { get => datumRodjenja; set => datumRodjenja = value; }
        public Spol Spol { get => spol; set => spol = value; }
        public String Opis { get => opis; set => opis = value; }
        public Double Ocjena { get => ocjena; set => ocjena = value; }
        public global::System.Boolean Aktivan { get => aktivan; set => aktivan = value; }
        public String Slika { get => slika; set => slika = value; }
    }
}
