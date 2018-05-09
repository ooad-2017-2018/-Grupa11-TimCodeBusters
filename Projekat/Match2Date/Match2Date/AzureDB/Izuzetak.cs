using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match2Date.AzureDB
{
    public class IzuzetakKorisnikNePostoji : Exception
    {
        const string message = "Korisnik ne postoji!";

        public IzuzetakKorisnikNePostoji()
        {
        }
        public override string ToString()
        {
            return message;
        }
    }

    public class IzuzetakNetacnaSifra : Exception
    {
        const string message = "Netačna šifra, pokušajte ponovo!";

        public IzuzetakNetacnaSifra()
        {

        }
        public override string ToString()
        {
            return message;
        }
    }
}
