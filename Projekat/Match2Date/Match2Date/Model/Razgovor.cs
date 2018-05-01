using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match2Date.Model
{
    public class Razgovor
    {
        Korisnik sagovornik1;
        Korisnik sagovornik2;
        List<Poruka> listaPoruka;

        public Razgovor()
        {
            listaPoruka = new List<Poruka>();
        }
    }
}
