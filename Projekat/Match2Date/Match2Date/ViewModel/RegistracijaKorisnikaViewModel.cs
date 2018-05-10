using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Match2Date.Model;
using Match2Date.Enumeration;
using System.Windows.Input;
using Windows.UI.Popups;
using System.Text.RegularExpressions;
using Match2Date.AzureDB;
using Match2Date.View;
using System.Collections.ObjectModel;
using System.IO;

namespace Match2Date.ViewModel
{
    public class RegistracijaKorisnikaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public MessageDialog Poruka { get; set; }

        private string _vIme;
        private string _vPrezime;
        private string _vGrad;
        private string _vEmail;
        private string _vSifra;
        private string _vPotSifra;
        private DateTime _vDatumRodjenja;
        private Spol _vSpol;
        private string _vOpis;

        public string VIme { get => _vIme; set { _vIme = value; NotifyPropertyChanged(nameof(VIme)); } }
        public string VPrezime { get => _vPrezime; set { _vPrezime = value; NotifyPropertyChanged(nameof(VPrezime)); } }
        public string VGrad { get => _vGrad; set { _vGrad = value; NotifyPropertyChanged(nameof(VGrad)); } }
        public string VEmail { get => _vEmail; set { _vEmail = value; NotifyPropertyChanged(nameof(VEmail)); } }
        public string VSifra { get => _vSifra; set { _vSifra = value; NotifyPropertyChanged(nameof(VSifra)); } }
        public string VPotSifra { get => _vPotSifra; set { _vPotSifra = value; NotifyPropertyChanged(nameof(VPotSifra)); } }
        public DateTime VDatumRodjenja { get => _vDatumRodjenja; set { _vDatumRodjenja = value; NotifyPropertyChanged(nameof(VDatumRodjenja)); } }
        public DateTimeOffset VDatumRodjenjaOffset { get; set; }
        public Spol VSpol { get => _vSpol; set { _vSpol = value; NotifyPropertyChanged(nameof(VSpol)); } }
        public string VOpis { get => _vOpis; set { _vOpis = value; NotifyPropertyChanged(nameof(VOpis)); } }
        //List<Byte[]> VListaSlika { get; set; }
        public List<string> gradovi { get; set; }
        public int indexGrad { get; set; }

        public ICommand RegistrujSe { get; set; }
        public ICommand Musko { get; set; }
        public ICommand Zensko { get; set; }

        public RegistracijaKorisnikaViewModel()
        {
            VDatumRodjenjaOffset = DateTimeOffset.Now;
            gradovi = File.ReadAllLines(@"Assets\Gradovi.txt").ToList();
            indexGrad = 0;
            RegistrujSe = new RelayCommand<object>(registracijaKorisnika);
            Musko = new RelayCommand<object>(postaviMusko);
            Zensko = new RelayCommand<object>(postaviZensko);
            VIme = "";
            VPrezime = "";
            VGrad = "";
            VEmail = "";
            VSifra = "";
            VOpis = "";
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void postaviMusko(object parameter)
        {
            VSpol = Spol.musko;
        }

        private void postaviZensko(object parameter)
        {
            VSpol = Spol.zensko;
        }

        private async void registracijaKorisnika(object parametar)
        {
            if (VIme == "" || VEmail == "" || VPrezime == "" || VSifra == "" )
            {
                Poruka = new MessageDialog("Popunite prazna mjesta.");
                await Poruka.ShowAsync();
                return;
            }
            VDatumRodjenja = VDatumRodjenjaOffset.Date;

            if (!validirajDatum())
            {
                Poruka = new MessageDialog("Morate imati preko 18 godina!");
                await Poruka.ShowAsync();
                return;
            }

            if (await (DBHelp.PostojiMail(VEmail)) == true)
            {
                Poruka = new MessageDialog("Korisnik sa unesenim mailom već postoji.");
                await Poruka.ShowAsync();
                return;
            }
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

            int k = await DBHelp.DajIduciIDAsync();

            obj.Id = k.ToString();
            obj.Ime = VIme;
            obj.Prezime = VPrezime;
            obj.Grad = gradovi.ElementAt(indexGrad);
            obj.Opis = VOpis;
            obj.Email = VEmail;
            obj.Sifra = VSifra;
            obj.DatumRodjenja = VDatumRodjenja;
            obj.Spol = VSpol;
            obj.Ocjena = -1;
            obj.Aktivan = true;

            try
            {
                DBHelp.DodajKorisnika(obj);
            }
            catch(Exception ex)
            {
                await new MessageDialog(ex.ToString()).ShowAsync();
            }

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
        private bool validirajDatum()
        {
            if(VDatumRodjenja.AddYears(18) >= DateTime.Now)
                return true;
            return false;
        }
        
    }
}
