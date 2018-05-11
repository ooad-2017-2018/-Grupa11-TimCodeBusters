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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage.Pickers;
using Windows.Media.Capture;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Media;

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
        private string _vFacebook;
        private string _vinstagram;
        private string _vBrojTelefona;

        public string VFacebook { get => _vFacebook; set { _vFacebook = value; NotifyPropertyChanged(nameof(VFacebook)); } }
        public string VInstagram { get => _vinstagram; set { _vinstagram = value; NotifyPropertyChanged(nameof(VInstagram)); } }
        public string VBrojTelefona { get => _vBrojTelefona; set { _vBrojTelefona = value; NotifyPropertyChanged(nameof(VBrojTelefona)); } }

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
        public ICommand DodajSLike { get; set; }
        public ICommand Kamera { get; set; }
        public BitmapSource Slika { get; set; }

        public RegistracijaKorisnikaViewModel()
        {

            VDatumRodjenjaOffset = DateTimeOffset.Now;
            gradovi = File.ReadAllLines(@"Assets\Gradovi.txt").ToList();
            indexGrad = 0;
            RegistrujSe = new RelayCommand<object>(RegistracijaKorisnika);
            Musko = new RelayCommand<object>(PostaviMusko);
            Zensko = new RelayCommand<object>(PostaviZensko);
            DodajSLike = new RelayCommand<object>(UploadSLike);
            Kamera = new RelayCommand<object>(UploadKamera);
            VIme = "";
            VPrezime = "";
            VGrad = "";
            VEmail = "";
            VSifra = "";
            VOpis = "";
            VFacebook = "";
            VInstagram = "";
            VBrojTelefona = "";
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private async void UploadSLike(object parameter)
        {
            /*var picker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
                FileTypeFilter = { ".jpg", ".jpeg", ".png", ".gif" }
            };
            var file = await picker.PickSingleFileAsync();*/

            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.ViewMode = PickerViewMode.Thumbnail;

            // Filter to include a sample subset of file types.
            openPicker.FileTypeFilter.Clear();
            openPicker.FileTypeFilter.Add(".bmp");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".jpg");

            // Open the file picker.
            Windows.Storage.StorageFile file = await openPicker.PickSingleFileAsync();

            // 'file' is null if user cancels the file picker.
            if (file != null)
            {
                // Open a stream for the selected file.
                // The 'using' block ensures the stream is disposed
                // after the image is loaded.
                using (Windows.Storage.Streams.IRandomAccessStream fileStream =
                    await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    // Set the image source to the selected bitmap.
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.SetSource(fileStream);
                    Slika = bitmapImage;
                }
            }
        }

        private async void UploadKamera(object parameter)
        {
            var capture = new CameraCaptureUI
            {
                PhotoSettings =
                {
                    Format = CameraCaptureUIPhotoFormat.Jpeg
                }
            };
            var file = await capture.CaptureFileAsync(CameraCaptureUIMode.Photo);
        }

        private void PostaviMusko(object parameter)
        {
            VSpol = Spol.musko;
        }

        private void PostaviZensko(object parameter)
        {
            VSpol = Spol.zensko;
        }

        private async void RegistracijaKorisnika(object parametar)
        {
            if (VIme == "" || VEmail == "" || VPrezime == "" || VSifra == "" || VOpis == "")
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
            Kontakti obj2 = new Kontakti();

            int k = await DBHelp.DajIduciIDAsync();

            obj.Id = k.ToString();
            obj.Ime = VIme;
            obj.Prezime = VPrezime;
            obj.Grad = gradovi.ElementAt(indexGrad);
            obj.Opis = VOpis;
            obj.Email = VEmail;
            obj.Sifra = Hash.IzracunajMD5Hash(VSifra);
            obj.DatumRodjenja = VDatumRodjenja;
            obj.Spol = VSpol;
            obj.Ocjena = -1;
            obj.Aktivan = true;

            obj2.Id = k.ToString();
            obj2.Facebook = VFacebook;
            obj2.Instagram = VInstagram;
            obj2.BrojTelefona = VBrojTelefona;
           // obj2.Korisnici_id = k.ToString();

            try
            {
                DBHelp.DodajKorisnika(obj, obj2);
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.ToString()).ShowAsync();
            }
            Kontakt kontakt = new Kontakt(VFacebook, VInstagram, VBrojTelefona);
            Korisnik korisnik = new Korisnik(VIme, VPrezime, VGrad, VEmail, VSifra, VDatumRodjenja, VSpol, VOpis, kontakt);
            Poruka = new MessageDialog("Uspješno kreiran račun.");
            await Poruka.ShowAsync();

            ((Frame)Window.Current.Content).Navigate(typeof(View.Prijava));
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
            if (VDatumRodjenja.AddYears(18) < DateTime.Now)
                return true;
            return false;
        }

    }
}
