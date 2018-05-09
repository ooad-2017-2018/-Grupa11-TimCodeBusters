using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Match2Date.Model;
using Match2Date.Enumeration;

namespace Match2Date.ViewModel
{
    public class RegistracijaKorisnikaViewModel : INotifyPropertyChanged
    {
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
        public Spol VSpol { get => _vSpol; set { _vSpol = value; NotifyPropertyChanged(nameof(VSpol)); } }
        public string VOpis { get => _vOpis; set { _vOpis = value; NotifyPropertyChanged(nameof(VOpis)); } }
        //List<Byte[]> VListaSlika { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
