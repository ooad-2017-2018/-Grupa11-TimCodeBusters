using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Match2Date.Model;
using Match2Date.Enumeration;
using System.ComponentModel;
using System.Windows.Input;
using Windows.UI.Popups;
using System.Text.RegularExpressions;
using Match2Date.AzureDB;

namespace Match2Date.ViewModel
{
    public class RegistracijaKorisnikaViewModel : INotifyPropertyChanged
    {
        public static int id = 0;
        public event PropertyChangedEventHandler PropertyChanged;
        public MessageDialog Poruka { get; set; }

        public string VIme { get; set; }
        public string VPrezime { get; set; }
        public string VGrad { get; set; }
        public string VEmail { get; set; }
        public string VSifra { get; set; }
        public string VPotSifra { get; set; }
        public DateTime VDatumRodjenja { get; set; }
        public Spol VSpol { get; set; }
        public string VOpis { get; set; }
        List<Byte[]> VListaSlika { get; set; }
        public ICommand RegistrujSe;
        public static Korisnik korisnik;

        public RegistracijaKorisnikaViewModel()
        {
            RegistrujSe = new RelayCommand<object>(registracijaKorisnika);
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private async void registracijaKorisnika(object parametar)
        {
            if (!validirajMail())
            {
                Poruka = new MessageDialog("Neispravna mail adresa.");
                await Poruka.ShowAsync();
                return;
            }
            if (VPotSifra != VSifra)
            {
                Poruka = new MessageDialog("Šifre se ne podudaraju.");
                await Poruka.ShowAsync();
                return;
            }
            korisnici obj = new korisnici();
            obj.Id = id.ToString(); id++;
            obj.Ime = VIme;
            obj.Prezime = VPrezime;
            obj.Grad = VGrad;
            obj.Email = VEmail;
            obj.Sifra = VSifra;
            obj.DatumRodjenja = VDatumRodjenja;
            obj.Spol = VSpol;
            obj.Ocjena = -1;
            obj.Aktivan = true;
            DBHelp.DodajKorisnika(obj);

            Korisnik korisnik = new Korisnik(VIme, VPrezime, VGrad, VEmail, VSifra, VDatumRodjenja, VSpol, VOpis);
            Poruka = new MessageDialog("Uspješno kreiran račun.");
            await Poruka.ShowAsync();
        }

        private bool validirajMail()
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(VEmail);
            if (match.Success)
            {
                return true;
            }
            return false;
        }
    }
}
