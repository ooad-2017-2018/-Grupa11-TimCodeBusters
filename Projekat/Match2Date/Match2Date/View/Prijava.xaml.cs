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
using Match2Date.AzureDB;
using Windows.UI.Popups;
using Match2Date.Model;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Match2Date.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Prijava : Page
    {
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
                Korisnik korisnik = await (DBHelp.DajKorisnika(mail, pass));
                this.Frame.Navigate(typeof(MainPage), korisnik);
            }
            catch (IzuzetakNetacnaSifra ex)
            {
                await new MessageDialog(ex.ToString()).ShowAsync();
            }
            catch (IzuzetakKorisnikNePostoji ex)
            {
                await new MessageDialog(ex.ToString()).ShowAsync();
                sifra.Password = "";
                sifra.Focus(FocusState.Keyboard);
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.ToString()).ShowAsync();
            }
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RegistracijaKorisnika));
        }
    }
}
