using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Match2Date.Model;
using Match2Date.Enumeration;

namespace Match2Date.ViewModel
{
    public class RegistracijaKorisnikaViewModel
    {
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
    }
}
