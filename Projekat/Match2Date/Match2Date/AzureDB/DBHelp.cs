using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Match2Date.Model;

namespace Match2Date.AzureDB
{
    public class DBHelp
    {
        public static async Task<Korisnik> DajKorisnika(string mail, string pass)
        {
            IMobileServiceTable<korisnici> Korisnici = App.MobileService.GetTable<korisnici>();
            bool found = false;
            bool mailCorrect = false;
            IEnumerable<korisnici> x = await Korisnici.ReadAsync();
            foreach (var a in x)
            {
                if (a.Sifra.Equals(pass) && a.Email.Equals(mail))
                {
                    Korisnik korisnik = new Korisnik(a.Ime, a.Prezime, a.Grad, a.Email, a.Sifra, a.DatumRodjenja, a.Spol, a.Opis);
                    found = true;
                    return korisnik;
                }
                else if (a.Email.Equals(mail))
                {
                    mailCorrect = true;
                }
            }
            if (!found)
            {
                if (mailCorrect)
                {
                    throw new IzuzetakNetacnaSifra();
                }
                else
                {
                    throw new IzuzetakKorisnikNePostoji();
                }
            }
            return null;
        }
       

    }
}
