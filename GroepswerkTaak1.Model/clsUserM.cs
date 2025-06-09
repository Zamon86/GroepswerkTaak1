using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GroepswerkTaak1.Model
{
    public class clsUserM : clsCommonModelPropertiesBase   // later toevoegen , IDataErrorInfo
    {/// <summary>
     /// TODO  Error Handling
     /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // Constructor
    public clsUserM()
    {
    }

    public clsUserM(int MyUserId, string MyLoginNaam, string MyNaam, string MyVoorNaam,
                                   string MyEmail, string MyTelefoon, int MyRolId, bool MyUserActief, string MyWachtwoord)
    {
            _UserId = MyUserId;
            _LoginNaam = MyLoginNaam;
            _Naam = MyNaam;
            _VoorNaam = MyVoorNaam;
            _Email = MyEmail;
            _Telefoon = MyTelefoon;
            _RolId = MyRolId;
            _UserActief = MyUserActief;
            _Wachtwoord = MyWachtwoord;

        }


    private int _UserId;

    public int UserId
        {
        get { return _UserId; }
        set { _UserId = value; OnPropertyChanged(nameof(UserId)); }
    }


    private string _LoginNaam;

    public string LoginNaam
        {
        get { return _LoginNaam; }
        set { _LoginNaam = value; OnPropertyChanged(nameof(LoginNaam)); }
    }


        private string _Naam;
        public string Naam
        {
            get { return _Naam; }
            set { _Naam = value; OnPropertyChanged(nameof(Naam)); }
        }

        
        private string _VoorNaam;

        public string VoorNaam
        {
            get { return _VoorNaam; }
            set { _VoorNaam = value; OnPropertyChanged(nameof(VoorNaam)); }
        }


        private string _Email;

    public string Email
        {
        get { return _Email; }
        set { _Email = value; OnPropertyChanged(nameof(Email)); }
    }



private string _Telefoon;
        public string Telefoon
        {
            get { return _Telefoon; }
            set { _Telefoon = value; OnPropertyChanged(nameof(Telefoon)); }
        }


        private int _RolId;

        public int RolId
        {
            get { return _RolId; }
            set { _RolId = value; OnPropertyChanged(nameof(RolId)); }
        }


        private bool _UserActief;

        public bool UserActief
        {
            get { return _UserActief; }
            set { _UserActief = value; OnPropertyChanged(nameof(UserActief)); }
        }


        private string _Wachtwoord;
        public string Wachtwoord
        {
            get { return _Wachtwoord; }
            set { _Wachtwoord = value; OnPropertyChanged(nameof(Wachtwoord)); }
        }


        private Object _ControlField;
    public Object ControlField
    {
        get { return _ControlField; }
        set { _ControlField = value; }
    }

        public override string ToString()
        {
            return LoginNaam;
        }
    }
}
