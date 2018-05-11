using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match2Date.AzureDB
{
    public class Kontakti
    {
        String id;
        String facebook;
        String instagram;
        String brojTelefona;
        //String korisnici_id;

        public String Id { get => id; set => id = value; }
        public String Facebook { get => facebook; set => facebook = value; }
        public String Instagram { get => instagram; set => instagram = value; }
        public String BrojTelefona { get => brojTelefona; set => brojTelefona = value; }
        //public String Korisnici_id { get => korisnici_id; set => korisnici_id = value; }
    }
}
