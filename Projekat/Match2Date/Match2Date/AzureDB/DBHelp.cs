﻿using System;
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
       
        public static async void DodajKorisnika(korisnici obj)
        {
            IMobileServiceTable<korisnici> Korisnik = App.MobileService.GetTable<korisnici>();
            try
            {
                await Korisnik.InsertAsync(obj);
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
            return x.Count() + 1;
        }

    }
}
