using Common;
using GroepswerkTaak1.DAL;
using GroepswerkTaak1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroepswerkTaak1.ViewModels
{
    public class clsLoggingVM : clsCommonModelPropertiesBase
    {
        private ObservableCollection<clsLoggingM> _Loggings;
        public ObservableCollection<clsLoggingM> Loggings
        {
            get { return _Loggings; }
            set { _Loggings = value; }
        }
        public clsLoggingRepo repo = new clsLoggingRepo();
        public clsLoggingVM()
        {
            Loggings = repo.GetAll();
        }


    }
}
