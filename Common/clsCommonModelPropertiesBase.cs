using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ref cursus Peter Ceulemans 2025
namespace Common
{
    public class clsCommonModelPropertiesBase : clsObservable
    {
        private bool _IsDirty = false;

        public bool IsDirty
        {
            get { return _IsDirty; }
            set { _IsDirty = value; OnPropertyChanged(); }
        }

        private int _MijnSelectedIndex;

        public int MijnSelectedIndex
        {
            get { return _MijnSelectedIndex; }
            set { _MijnSelectedIndex = value; OnPropertyChanged(); }
        }

        private int _MyVisibility;

        public int MyVisibility
        {
            get { return _MyVisibility; }
            set { _MyVisibility = value; OnPropertyChanged(); }
        }

        private int _MyVisibility_Contrary;

        public int MyVisibility_Contrary
        {
            get { return _MyVisibility_Contrary; }
            set { _MyVisibility_Contrary = value; OnPropertyChanged(); }
        }

        private object _ControlField;

        public object ControlField
        {
            get { return _ControlField; }
            set { _ControlField = value; OnPropertyChanged(); }
        }

        public string ErrorBoodschap { get; set; }

        private List<string> _ErrorList;

        public List<string> ErrorList
        {
            get { return _ErrorList; }
            set { _ErrorList = value; }
        }

        public string Error
        {
            get
            {
                if (ErrorList.Count > 0)
                {
                    return "NOK";
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
