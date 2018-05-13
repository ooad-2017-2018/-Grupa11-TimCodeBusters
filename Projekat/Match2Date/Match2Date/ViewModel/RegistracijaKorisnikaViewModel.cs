using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using Match2Date.Model;
using Match2Date.Enumeration;
using System.Windows.Input;
using Windows.UI.Popups;
using System.Text.RegularExpressions;
using Match2Date.AzureDB;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage.Pickers;
using Windows.Media.Capture;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using Microsoft.WindowsAzure.Storage;
using Windows.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Windows.Graphics.Imaging;
using Windows.Foundation;

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
        private BitmapSource _slika1;
        private BitmapSource _slika2;
        private BitmapSource _slika3;
        private int aktivnaSlika;
        private StorageFile sf1;
        private StorageFile sf2;
        private StorageFile sf3;

        private static string pathAzure = "https://korisnicislike.blob.core.windows.net/korisnicislike/";


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
        public BitmapSource Slika1 { get => _slika1; set { _slika1 = value; NotifyPropertyChanged(nameof(Slika1)); } }
        public BitmapSource Slika2 { get => _slika2; set { _slika2 = value; NotifyPropertyChanged(nameof(Slika2)); } }
        public BitmapSource Slika3 { get => _slika3; set { _slika3 = value; NotifyPropertyChanged(nameof(Slika3)); } }
        public StorageFile Sf1 { get => sf1; set => sf1 = value; }
        public StorageFile Sf2 { get => sf2; set => sf2 = value; }
        public StorageFile Sf3 { get => sf3; set => sf3 = value; }

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
            VIme = VPrezime = VGrad = VEmail = VSifra = VOpis = VFacebook = VInstagram = VBrojTelefona = "";
            Slika1 = Slika2 =  Slika3 = null;
            aktivnaSlika = 1;
            sf1 = sf2 = sf3 = null;

            //Prikaz slike - nije testirano!
            /*var bi = new BitmapImage(new Uri("https://korisnicislike.blob.core.windows.net/korisnicislike/CCapture (9).jpg", UriKind.Absolute));
            Slika2 = bi; */
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
            if (aktivnaSlika == 4)
            {
                await new MessageDialog("Možete dodati maksimalno 3 slike!").ShowAsync();
                aktivnaSlika = 1;
                return;
            }
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
            StorageFile file = await openPicker.PickSingleFileAsync();

            // 'file' is null if user cancels the file picker.
            if (file != null)
            {
                // Open a stream for the selected file.
                // The 'using' block ensures the stream is disposed
                // after the image is loaded.
                using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                {
                    // Set the image source to the selected bitmap.
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.SetSource(fileStream);
                    if (aktivnaSlika == 1)
                    {
                        Slika1 = bitmapImage;
                        sf1 = file;
                    }
                    else if (aktivnaSlika == 2)
                    {
                        Slika2 = bitmapImage;
                        sf2 = file;
                    }
                    else
                    {
                        Slika3 = bitmapImage;
                        sf3 = file;
                    }
                    aktivnaSlika++;
                }
            }
        }

        private async void UploadKamera(object parameter)
        {
            if (aktivnaSlika == 4)
            {
                await new MessageDialog("Moguće je dodati tačno 3 slike!").ShowAsync();
                return;
            }
            var capture = new CameraCaptureUI
            {
                PhotoSettings =
                {
                    Format = CameraCaptureUIPhotoFormat.Jpeg
                }
            };
            var file = await capture.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (file != null)
            {
                // Open a stream for the selected file.
                // The 'using' block ensures the stream is disposed
                // after the image is loaded.
                using (IRandomAccessStream fileStream =
                    await file.OpenAsync(FileAccessMode.Read))
                {
                    // Set the image source to the selected bitmap.
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.SetSource(fileStream);
                    if (aktivnaSlika == 1)
                    {
                        Slika1 = bitmapImage;
                        sf1 = file;
                    }
                    else if (aktivnaSlika == 2)
                    {
                        Slika2 = bitmapImage;
                        sf2 = file;
                    }
                    else
                    {
                        Slika3 = bitmapImage;
                        sf3 = file;
                    }
                    aktivnaSlika++;
                }
            }
        }

        private CloudStorageAccount createStorageAccountFromConnectionString()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=korisnicislike;AccountKey=NL2i15uIwXOH+wJeyktiigGNBy4LPpvGHKLQv5YoxIHMurAokcBbboXEOrMhSjK8M4aq+E99CfhcI6Cbza+H4Q==;EndpointSuffix=core.windows.net");
            }
            catch (FormatException)
            {
                throw new FormatException("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
            }
            return storageAccount;
        }

        private async void saveUserPhoto(StorageFile photo)
        {
            CloudStorageAccount storageAccount = createStorageAccountFromConnectionString();
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("korisnicislike");
            await container.CreateIfNotExistsAsync();
            CloudBlockBlob blob = container.GetBlockBlobReference(photo.Name);
            await blob.UploadFromFileAsync(photo);
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

            if(Slika1 == null || Slika2 == null || Slika3 == null)
            {
                Poruka = new MessageDialog("Potrebno je dodati 3 slike!");
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

            saveUserPhoto(sf1);
            saveUserPhoto(sf2);
            saveUserPhoto(sf3);

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
            obj.Slika1 = pathAzure + sf1.Name;
            obj.Slika2 = pathAzure + sf2.Name;
            obj.Slika3 = pathAzure + sf3.Name;

            obj2.Id = k.ToString();
            obj2.Facebook = VFacebook;
            obj2.Instagram = VInstagram;
            obj2.BrojTelefona = VBrojTelefona;
            //obj2.Korisnici_id = k.ToString();

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
