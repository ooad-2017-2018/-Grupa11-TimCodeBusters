﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Match2Date.Model;
using Match2Date.AzureDB;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Match2Date.View;

namespace Match2Date.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private string _vEmail;
        private string _vSifra;

        public string Email { get => _vEmail; set { _vEmail = value; NotifyPropertyChanged(nameof(Email)); } }
        public string Sifra { get => _vSifra; set { _vSifra = value; NotifyPropertyChanged(nameof(Sifra)); } }
        public ICommand PrijaviSe { get; set; }

        public LoginViewModel()
        {
            PrijaviSe = new RelayCommand<object>(Prijava);
        }

        private async void Prijava(object parameter)
        {
            var mail = Email;  
            var pass = Sifra;
            if (mail == "admin" && pass == "admin")
            {
                ((Frame)Window.Current.Content).Navigate(typeof(AdminPage));
            }
            else
            {
                try
                {
                    Korisnik korisnik = await (DBHelp.DajKorisnika(mail, pass));
                    ((Frame)Window.Current.Content).Navigate(typeof(MainPage), korisnik);
                }
                catch (IzuzetakNetacnaSifra ex)
                {
                    await new MessageDialog(ex.ToString()).ShowAsync();
                }
                catch (IzuzetakKorisnikNePostoji ex)
                {
                    await new MessageDialog(ex.ToString()).ShowAsync();
                    Sifra = "";
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.ToString()).ShowAsync();
                }
            }
        }
    }
}
