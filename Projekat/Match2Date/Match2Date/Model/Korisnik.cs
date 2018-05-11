using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Match2Date.Enumeration;

namespace Match2Date.Model
{
    public class Korisnik
    {
        int id;
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
        Kontakt kontakt;
        Filter filter;
        List<Byte[]> listaSlika;
        List<Korisnik> listaLajkanihKorisnika;
        List<Razgovor> listaRazgovora;

        public int Id { get => id; set => id = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public string Grad { get => grad; set => grad = value; }
        public string Email { get => email; set => email = value; }
        public string Sifra { get => sifra; set => sifra = value; }
        public DateTime DatumRodjenja { get => datumRodjenja; set => datumRodjenja = value; }
        public Spol Spol { get => spol; set => spol = value; }
        public string Opis { get => opis; set => opis = value; }
        public double Ocjena { get => ocjena; set => ocjena = value; }
        public bool Aktivan { get => aktivan; set => aktivan = value; }
        public Kontakt Kontakt { get => kontakt; set => kontakt = value; }
        public Filter Filter { get => filter; set => filter = value; }
        public List<byte[]> ListaSlika { get => listaSlika; set => listaSlika = value; }
        public List<Korisnik> ListaLajkanihKorisnika { get => listaLajkanihKorisnika; set => listaLajkanihKorisnika = value; }
        public List<Razgovor> ListaRazgovora { get => listaRazgovora; set => listaRazgovora = value; }
        public string Ime { get => ime; set => ime = value; }

        public Korisnik()
        {
            listaSlika = new List<byte[]>();
            listaLajkanihKorisnika = new List<Korisnik>();
            listaRazgovora = new List<Razgovor>();
        }

        public Korisnik(string ime, string prezime, string grad, string email, string sifra, DateTime datumRodjenja, Spol spol, string opis, Kontakt kontakt)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.grad = grad;
            this.email = email;
            this.sifra = sifra;
            this.datumRodjenja = datumRodjenja;
            this.spol = spol;
            this.opis = opis;
            this.aktivan = true;
            this.ocjena = -1;
            this.Filter = null;
            this.listaLajkanihKorisnika = null;
            this.listaRazgovora = null;
            this.kontakt = kontakt;
        }

        public Korisnik(string ime, string prezime, string grad, string email, string sifra, DateTime datumRodjenja, Spol spol, string opis)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.grad = grad;
            this.email = email;
            this.sifra = sifra;
            this.datumRodjenja = datumRodjenja;
            this.spol = spol;
            this.opis = opis;
            this.aktivan = true;
            this.ocjena = -1;
            this.Filter = null;
            this.listaLajkanihKorisnika = null;
            this.listaRazgovora = null;
        }
    }
}
