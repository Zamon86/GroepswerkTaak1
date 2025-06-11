using Common;
using GroepswerkTaak1.DAL;
using GroepswerkTaak1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace GroepswerkTaak1.ViewModels
{
    public class clsRollenVM : clsCommonModelPropertiesBase
    {

        public clsRollenRepo repo = new clsRollenRepo();


        #region Commands
       
        public ICommand cmdSave { get; set; }

        public ICommand cmdClose { get; set; }

        public ICommand cmdDelete { get; set; }

        public ICommand cmdCancel { get; set; }

        public ICommand cmdNew { get; set; }
        #endregion

        private bool _NewStatus = false;
        //clsKnoppenRepo Repo = new clsKnoppenRepo();

        private ObservableCollection<clsRolM>? _MijnCollectie;
        public ObservableCollection<clsRolM> MijnCollectie
        {
            get { return _MijnCollectie; }
            set { _MijnCollectie = value; OnPropertyChanged(); }
        }

        private clsRolM? _MijnSelectedItem;

        public clsRolM MijnSelectedItem
        {
            get { return _MijnSelectedItem; }
            set
            {  // Validatie schrijven wanneer een ander item in ComboBx wordt geselecteerd
                if (value != null)
                {
                    OpslaanCommando(null);
                    LoadData();
                }
                _MijnSelectedItem = value;
                OnPropertyChanged();
            }
        }


        public clsRollenVM()
        {

            // als test forceer ik het een item

            cmdNew = new clsCustomCommand(NewCommando, canExecuteNew);
            cmdClose = new clsCustomCommand(CloseCommando, canExecuteClose);
            cmdCancel = new clsCustomCommand(CancelCommando, canExecuteCancel);
            cmdSave = new clsCustomCommand(OpslaanCommando, canExecuteSave);
           
            cmdDelete = new clsCustomCommand(DeleteCommando, CanExecuteDelete);
            LoadData();
            MijnSelectedItem = repo.GetFirst(); // haal een item op met ID 1
        }

        private bool CanExecuteDelete(object obj)
        {
            return true;
        }

        private void DeleteCommando(object obj)
        {
            if (MijnSelectedItem != null)
            {

                if (repo.Delete(MijnSelectedItem, clsActiveUserData.ActiveUser.Naam))
                {
                    _NewStatus = false; // reset de status na opslaan
                    MijnSelectedItem.IsDirty = false; // reset dirty status
                    MijnSelectedItem.ErrorBoodschap = string.Empty; // reset error boodschap
                    MijnSelectedItem.MijnSelectedIndex = 0; // reset de index van de combobox
                    MijnSelectedItem.MyVisibility = (int)Visibility.Visible;                                        // eventueel knoppen aan of uitzetten
                    LoadData(); // herlaad de collectie om de nieuwe item te tonen
                }
                else
                {
                    MijnSelectedItem.ErrorBoodschap = "Nieuwe knop kan niet worden Verwijdert";
                }
            }


        }







        private bool canExecuteSave(object obj)
        {
            return true;
            //        if (MijnSelectedItem != null  && MijnSelectedItem.IsDirty == true)//&& MijnSelectedItem.Error==null
            //         {
            //	return true;

            //}
            //         else
            //         {
            //             return false;
            //         }
        }

        private bool canExecuteCancel(object obj)
        {
            return true;
            //   return _NewStatus;
        }

        private void CancelCommando(object obj)
        {
            MijnSelectedItem = repo.GetFirst();
            if (MijnSelectedItem != null)
            {
                MijnSelectedItem.MyVisibility = (int)Visibility.Visible;
            }
            _NewStatus = false;

        }

        private bool canExecuteClose(object obj)
        {
            return true;
        }

        private void CloseCommando(object obj)
        {
            //        UserControl uc=obj as UserControl;
            //if (uc != null) { 
            //uc.

            //}
        }

        private bool canExecuteNew(object t)
        {
            return !_NewStatus;
        }

        private void LoadData()
        {
            MijnCollectie = repo.GetAll();
        }

        private void OpslaanCommando(object obj)
        {
            if (MijnSelectedItem != null)
            {
                if (_NewStatus) // nieuw item
                {
                    if (repo.Insert(MijnSelectedItem,clsActiveUserData.ActiveUser.Naam))
                    {
                        _NewStatus = false; // reset de status na opslaan
                        MijnSelectedItem.IsDirty = false; // reset dirty status
                        MijnSelectedItem.ErrorBoodschap = string.Empty; // reset error boodschap
                        MijnSelectedItem.MijnSelectedIndex = 0; // reset de index van de combobox
                        MijnSelectedItem.MyVisibility = (int)Visibility.Visible;                                        // eventueel Rollpen aan of uitzetten
                        LoadData(); // herlaad de collectie om de nieuwe item te tonen
                    }
                    else
                    {
                        MijnSelectedItem.ErrorBoodschap = "Nieuwe Roll kan niet worden opgeslagen";
                    }
                }
                else
                {
                    if (repo.Update(MijnSelectedItem, clsActiveUserData.ActiveUser.Naam))
                    {
                        _NewStatus = false; // reset de status na opslaan
                        MijnSelectedItem.IsDirty = false; // reset dirty status
                        MijnSelectedItem.ErrorBoodschap = string.Empty; // reset error boodschap
                        MijnSelectedItem.MijnSelectedIndex = 0; // reset de index van de combobox
                                                                // eventueel knoppen aan of uitzetten
                        LoadData(); // herlaad de collectie om de nieuwe item te tonen
                    }
                    else
                    {
                        MijnSelectedItem.ErrorBoodschap = "update Roll is niet gelukt";
                    }
                }
            }
        }

        private void NewCommando(object obj)
        {
            clsRolM ItemToInsert = new clsRolM()
            {
                RolId = 0



            };
            MijnSelectedItem = ItemToInsert;
            MijnSelectedItem.MyVisibility = (int)Visibility.Hidden;
            _NewStatus = true;



        }
    

    }
}
