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
                if (a.Sifra.Equals(Hash.IzracunajMD5Hash(pass)) && a.Email.Equals(mail))
                {
                    Korisnik korisnik = new Korisnik(a.Ime, a.Prezime, a.Grad, a.Email, a.Sifra, a.DatumRodjenja, a.Spol, a.Opis, new List<String> { a.Slika1, a.Slika2, a.Slika3});
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

        public static async Task<bool> PostojiMail(string mail)
        {
            IMobileServiceTable<korisnici> Korisnici = App.MobileService.GetTable<korisnici>();
            bool found = false;
            
            IEnumerable<korisnici> x = await Korisnici.ReadAsync();
            foreach (var a in x)
            {
                if (a.Email.Equals(mail))
                {
                    return true;
                }
            }
            return false;
           
        }

        public static async void DodajKorisnika(korisnici obj, Kontakti obj2)
        {
            IMobileServiceTable<korisnici> Korisnik = App.MobileService.GetTable<korisnici>();
            IMobileServiceTable<Kontakti> Kontakt = App.MobileService.GetTable<Kontakti>();

            try
            {
                await Korisnik.InsertAsync(obj);
                await Kontakt.InsertAsync(obj2);
               
            }
            catch(Exception e)
            {
                throw;
            }
        }

        public static async Task<int> DajIduciIDAsync()
        {
            IMobileServiceTable<korisnici> Korisnici = App.MobileService.GetTable<korisnici>();
            
            IEnumerable<korisnici> x = await Korisnici.ReadAsync();
            return x.Count();
        }

    }
}
