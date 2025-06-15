using Common;
using GroepswerkTaak1.DAL;
using GroepswerkTaak1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GroepswerkTaak1.ViewModels
{
    public class clsUserVM : clsCommonModelPropertiesBase
    {

        public clsUserRepo repo = new clsUserRepo();
    

public ICommand cmdSave { get; set; }

public ICommand cmdClose { get; set; }

public ICommand cmdDelete { get; set; }

public ICommand cmdCancel { get; set; }

public ICommand cmdNew { get; set; }

        private string _Boodschap = string.Empty;

        private bool _NewStatus = false;

        private ObservableCollection<clsUserM>? _MijnCollectie;
        public ObservableCollection<clsUserM> MijnCollectie
        {
            get { return _MijnCollectie; }
            set { _MijnCollectie = value; OnPropertyChanged(); }
        }

        private clsUserM? _MijnSelectedItem;

        public clsUserM MijnSelectedItem
        {
            get { return _MijnSelectedItem; }
            set
            {  // Validatie schrijven wanneer een ander item in ComboBox wordt geselecteerd
                if (value != null)
                {
                   // OpslaanCommando(null);
                    LoadData();
                }
                _MijnSelectedItem = value;
                OnPropertyChanged();
            }
        }

        public clsUserVM()
        {
            cmdNew = new clsCustomCommand(NewCommando, canExecuteNew);
            cmdClose = new clsCustomCommand(CloseCommando, canExecuteClose);
            cmdCancel = new clsCustomCommand(CancelCommando, canExecuteCancel);
            cmdSave = new clsCustomCommand(OpslaanCommando, canExecuteSave);
            cmdDelete = new clsCustomCommand(DeleteCommando, CanExecuteDelete);
            LoadData();
            MijnSelectedItem = repo.GetFirst(); // haal een item op met ID 1
        }

        private void LoadData()
        {
            MijnCollectie = repo.GetAll();
        }

        #region New
        private bool canExecuteNew(object t)
        {
            return !_NewStatus;
        }

        private void NewCommando(object obj)
        {
            clsUserM ItemToInsert = new clsUserM()
            {
                UserId = 0
            };
            MijnSelectedItem = ItemToInsert;
            MijnSelectedItem.MyVisibility = (int)Visibility.Hidden;
            _NewStatus = true;
        }

        #endregion

        #region Opslaan

        private bool canExecuteSave(object obj)
        {
            return true;
        }

        private void OpslaanCommando(object obj)
        {
            if (MijnSelectedItem != null)
            {
                if (_NewStatus) // nieuw item
                {
                    if (repo.Insert(MijnSelectedItem))
                    {
                        _Boodschap = "Nieuwe gebruiker is opgeslagen";
                        _NewStatus = false; // reset de status na opslaan
                        MijnSelectedItem.IsDirty = false; // reset dirty status
                        MijnSelectedItem.ErrorBoodschap = string.Empty; // reset error boodschap
                        MijnSelectedItem.MijnSelectedIndex = 0; // reset de index van de combobox
                        MijnSelectedItem.MyVisibility = (int)Visibility.Visible;                                        // eventueel knoppen aan of uitzetten
                        LoadData(); // herlaad de collectie om de nieuwe item te tonen
                    }
                    else
                    {
                        //MijnSelectedItem.ErrorBoodschap = "Nieuwe gebruiker kan niet worden opgeslagen";
                        _Boodschap = "Nieuwe gebruiker kan niet worden opgeslagen";
                    }
                }
                else
                {
                    if (repo.Update(MijnSelectedItem))
                    {
                        _Boodschap = "Gebruiker is aangepast";
                        _NewStatus = false; // reset de status na opslaan
                        MijnSelectedItem.IsDirty = false; // reset dirty status
                        MijnSelectedItem.ErrorBoodschap = string.Empty; // reset error boodschap
                        MijnSelectedItem.MijnSelectedIndex = 0; // reset de index van de combobox
                        LoadData(); // herlaad de collectie om de nieuwe item te tonen
                    }
                    else
                    {
                        // MijnSelectedItem.ErrorBoodschap = "update gebruiker is niet gelukt";
                        _Boodschap  = "update gebruiker is niet gelukt";
                    }
                }
                MessageBox.Show(_Boodschap, "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        #endregion

        #region Delete
        private bool CanExecuteDelete(object obj)
        {
            return true;
        }

        private void DeleteCommando(object obj)
        {
            if (MijnSelectedItem != null)
            {
                if (repo.Delete(MijnSelectedItem))
                {
                    _Boodschap = "gebruiker is verwijderd";
                    _NewStatus = false; // reset de status na opslaan
                    MijnSelectedItem.IsDirty = false; // reset dirty status
                    MijnSelectedItem.ErrorBoodschap = string.Empty; // reset error boodschap
                    MijnSelectedItem.MijnSelectedIndex = 0; // reset de index van de combobox
                    MijnSelectedItem.MyVisibility = (int)Visibility.Visible;                                        // eventueel knoppen aan of uitzetten
                    LoadData(); // herlaad de collectie om de nieuwe item te tonen
                }
                else
                {
                   // MijnSelectedItem.ErrorBoodschap = "Nieuwe knop kan niet worden verwijderd";
                    _Boodschap = "Nieuwe knop kan niet worden verwijderd";
                }
                MessageBox.Show(_Boodschap, "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        #endregion

        #region Cancel

        private bool canExecuteCancel(object obj)
        {
            return true;
        }

        private void CancelCommando(object obj)
        {
            MijnSelectedItem.Wachtwoord = string.Empty; // reset wachtwoord van de actuele gebruiker
            LoadData(); // eigenlijk moet je nakijken of data gewijzigd was en de user bevragen om op te slaan
            MijnSelectedItem = repo.GetFirst();  // dan pas eerste record terug geven
            if (MijnSelectedItem != null)
            {
                MijnSelectedItem.MyVisibility = (int)Visibility.Visible;
            }
            _NewStatus = false;
        }

        #endregion

        #region  Close
        private bool canExecuteClose(object obj)
        {
            return true;
        }

        private void CloseCommando(object obj)
        {
        }

        #endregion









    }
}
