using Common;
using GroepswerkTaak1.DAL;
using GroepswerkTaak1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

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

        public ICollectionView LoggingsCV { get; }

        private string _FilterAktie = string.Empty;

        public string FilterAktie
        {
            get { return _FilterAktie; }
            set { 
                    _FilterAktie = value;
                    OnPropertyChanged(nameof(FilterAktie));
                    LoggingsCV.Refresh();
            }
        }

        private string _FilterTabelColumn = string.Empty;
        public string FilterTabelColumn
        {
            get { return _FilterTabelColumn; }
            set
            {
                _FilterTabelColumn = value;
                OnPropertyChanged(nameof(FilterTabelColumn));
                LoggingsCV.Refresh();
            }
        }

        private string _FilterPreValue = string.Empty;
        public string FilterPreValue
        {
            get { return _FilterPreValue; }
            set
            {
                _FilterPreValue = value;
                OnPropertyChanged(nameof(FilterPreValue));
                LoggingsCV.Refresh();
            }
        }
        private string _FilterPostValue = string.Empty;
        public string FilterPostValue
        {
            get { return _FilterPostValue; }
            set
            {
                _FilterPostValue = value;
                OnPropertyChanged(nameof(FilterPostValue));
                LoggingsCV.Refresh();
            }
        }


        private string _FilterGebruiker =  string.Empty;

        public string FilterGebruiker
        {
            get { return _FilterGebruiker; }
            set { 
                    _FilterGebruiker = value; 
                    OnPropertyChanged(nameof(FilterGebruiker)); 
                    LoggingsCV.Refresh();
            }
        }

        private bool wijzigStartDatum = false;
        private bool wijzigEindDatum = false;


        private DateTime _FilterStartDatum = DateTime.Now.AddDays(-7);

        public DateTime FilterStartDatum
        {
            get { return _FilterStartDatum; }
            set {
                    //wijzigStartDatum = true;
                    //if (_FilterEindDatum > value)
                    //{
                        _FilterStartDatum = value;
                    //}
                    //else
                    //{
                    //    if (!wijzigEindDatum) _FilterStartDatum = _FilterEindDatum;
                    //}
                    OnPropertyChanged(nameof(FilterStartDatum));
                    LoggingsCV.Refresh();
                    //wijzigStartDatum = false;
            }
        }


        private DateTime _FilterEindDatum = DateTime.Now.AddDays(1);

        public DateTime FilterEindDatum
        {
            get { return _FilterEindDatum; }
            set {
                    //wijzigEindDatum = true;
                    //if (_FilterEindDatum < _FilterStartDatum)
                    //    {
                            _FilterEindDatum = value;
                    //    }
                    //else
                    //    {
                    //        if(!wijzigStartDatum) _FilterEindDatum = _FilterStartDatum;
                    //    }
                    OnPropertyChanged(nameof(FilterEindDatum));
                    LoggingsCV.Refresh();
                    //wijzigStartDatum = false;
            }
        }


        public clsLoggingRepo repo = new clsLoggingRepo();
        public clsLoggingVM()
        {
            Loggings = repo.GetAll();
            LoggingsCV = CollectionViewSource.GetDefaultView(Loggings);
            LoggingsCV.Filter = new Predicate<object>(FilterLoggings);
        }

        private bool FilterLoggings(object obj)
        {
            if (obj is clsLoggingM logging)
            {
                // Hier kun je de filtercriteria aanpassen
                // Bijvoorbeeld: filteren op een specifieke gebruiker of datum
                return
                    logging.Aktie.Contains(_FilterAktie, StringComparison.InvariantCultureIgnoreCase) &&
                    logging.TabelColumn.Contains(_FilterTabelColumn, StringComparison.InvariantCultureIgnoreCase) &&
                    logging.PreValue.Contains(_FilterPreValue, StringComparison.InvariantCultureIgnoreCase) &&
                    logging.PostValue.Contains(_FilterPostValue, StringComparison.InvariantCultureIgnoreCase) &&
                    logging.Gebruiker.Contains(_FilterGebruiker, StringComparison.InvariantCultureIgnoreCase) &&
                    logging.Registratie >= _FilterStartDatum && logging.Registratie <= _FilterEindDatum.AddDays(1);
            }
       
            return false; // Als het object geen clsLoggingM is, wordt het niet weergegeven
        }
    }
}
