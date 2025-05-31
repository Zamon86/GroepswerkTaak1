using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroepswerkTaak1.Model
{
    public class clsRollenM : clsCommonModelPropertiesBase  // later toevoegen , IDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Constructor
        public clsRollenM()
        {
        }

        public clsRollenM(int MyRolId, string MyRolNaam)
        {
            _RolId = MyRolId;
            _RolNaam = MyRolNaam;
        }

        private int _RolId;

        public int RolId
        {
            get { return _RolId; }
            set { _RolId = value; }
        }

        private string _RolNaam;

        public string RolNaam
        {
            get { return _RolNaam; }
            set { _RolNaam = value; OnPropertyChanged(nameof(RolNaam)); }
        }


        private Object _ControlField;
        public Object ControlField
        {
            get { return _ControlField; }
            set { _ControlField = value; }
        }

    }
}
