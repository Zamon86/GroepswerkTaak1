using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroepswerkTaak1.DAL
{
    public class clsLoginRepo
    {

        int nr = 0;
        public clsLoginRepo() { }

        public int Login(string username, string password)
        {
            // Hier zou je de logica voor het inloggen implementeren.
            // Bijvoorbeeld, controleren of de gebruikersnaam en het wachtwoord overeenkomen met een database.
            // Voor nu, laten we aannemen dat de login altijd succesvol is.

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return -990; // Login mislukt als gebruikersnaam of wachtwoord leeg is.
            }

            // Simuleer een succesvolle login
            clsDAL.ExecuteDataTable(Properties.Resources.SV_Login, ref nr,
                clsDAL.Parameter("LoginNaam", username),
                clsDAL.Parameter("PassWord", password),
                clsDAL.Parameter("@ReturnValue", 0));
            
            return nr; // Retourneer een waarde die aangeeft of de login succesvol was.  >0 => = ID <0 => login mislukt
        }
    }
}
