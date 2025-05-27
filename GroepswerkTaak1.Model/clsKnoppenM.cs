using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroepswerkTaak1.Model
{
    public class clsKnoppenM : INotifyPropertyChanged
    {  /// <summary>
       /// TODO  Error Handling
       /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Constructor
        public clsKnoppenM()
        {
        }

        public clsKnoppenM(int MyKnopId, string MyKnopNaam, string MyKnopTekst, int MyKnopPositie, byte[] MyKnopImage)
        {
            _KnopId = MyKnopId;
            _KnopNaam = MyKnopNaam;
            _KnopTekst = MyKnopTekst;
            _KnopPositie = MyKnopPositie;
            _KnopImage = MyKnopImage;
        }


        private int _KnopId;

        public int KnopId
        {
            get { return _KnopId; }
            set { _KnopId = value; OnPropertyChanged(nameof(KnopId)); }
        }


        private string _KnopNaam;

        public string KnopNaam
        {
            get { return _KnopNaam; }
            set { _KnopNaam = value; OnPropertyChanged(nameof(KnopNaam)); }
        }


        private string _KnopTekst;

        public string KnopTekst
        {
            get { return _KnopTekst; }
            set { _KnopTekst = value; OnPropertyChanged(nameof(KnopTekst)); }
        }


        private int _KnopPositie;

        public int KnopPositie
        {
            get { return _KnopPositie; }
            set { _KnopPositie = value; OnPropertyChanged(nameof(KnopPositie)); }
        }




        private byte[] _KnopImage;

        public byte[] KnopImage
        {
            get { return _KnopImage; }
            set { _KnopImage = value; OnPropertyChanged(nameof(KnopImage)); }
        }

        private Object _ControlField;
        public Object ControlField
        {
            get { return _ControlField; }
            set { _ControlField = value; }
        }
    }
}
