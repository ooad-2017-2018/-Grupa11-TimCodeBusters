using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.WindowsAzure.MobileServices;
using Match2Date.AzureDB;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Match2Date.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Prijava : Page
    {
        IMobileServiceTable<korisnici> Korisnici = App.MobileService.GetTable<korisnici>();
        public Prijava()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var mail = email.Text;
            var pass = sifra.Password;
            try
            {
                IEnumerable<korisnici> x = await Korisnici.ReadAsync();
                foreach(var a in x)
                {
                    if(a.Sifra.Equals(pass) && a.Email.Equals(mail))
                    {
                        this.Frame.Navigate(typeof(MainPage), a);
                    }
                }
                /*MessageDialog md = new MessageDialog("Korisnik ne postoji");
                await md.ShowAsync();*/
            }
            catch(Exception ex)
            {
                MessageDialog md = new MessageDialog("GRESKA: " + ex.ToString());
                await md.ShowAsync();
            }
        }

        
    }
}
