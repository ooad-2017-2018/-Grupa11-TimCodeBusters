using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match2Date.Model
{
    public class Kontakt
    {
        String facebook;
        String instagram;
        String brojTelefona;

        public Kontakt()
        {
        }

        public Kontakt(string facebook, string instagram, string brojTelefona)
        {
            this.facebook = facebook;
            this.instagram = instagram;
            this.brojTelefona = brojTelefona;
        }
    }
}
