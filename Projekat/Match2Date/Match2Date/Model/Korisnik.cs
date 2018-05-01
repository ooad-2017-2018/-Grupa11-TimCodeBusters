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

        public Korisnik()
        {
            listaSlika = new List<byte[]>();
            listaLajkanihKorisnika = new List<Korisnik>();
            listaRazgovora = new List<Razgovor>();
        }

    }
}
