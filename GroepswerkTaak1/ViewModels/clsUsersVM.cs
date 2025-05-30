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
    public class clsUsersVM : clsCommonModelPropertiesBase
    {
        private bool _NewStatus = false;
        clsUsersRepo Repo = new clsUsersRepo();

        private ObservableCollection<clsUsersM>? _MijnCollectie;
        public ObservableCollection<clsUsersM> MijnCollectie
        {
            get { return _MijnCollectie; }
            set { _MijnCollectie = value; OnPropertyChanged(); }
        }

        private clsUsersM? _MijnSelectedItem;

        public clsUsersM MijnSelectedItem
        {
            get { return _MijnSelectedItem; }
            set
            {  // Validatie schrijven wanneer een ander item in ComboBx wordt geselecteerd
                if (value != null)
                {
                    OpslaanCommando();
                    LoadData();
                }
                _MijnSelectedItem = value;
                OnPropertyChanged();
            }
        }
        public clsUsersVM()
        {
            LoadData();
            // als test forceer ik het een item
            _MijnSelectedItem = Repo.GetByID(3); // haal een item op met ID 1
        }

        private void LoadData()
        {
            MijnCollectie = Repo.GetAll();
        }
        private void OpslaanCommando()
        {
            if (MijnSelectedItem != null)
            {
                if (_NewStatus) // nieuw item
                {
                    if (Repo.Insert(MijnSelectedItem))
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
                        MijnSelectedItem.ErrorBoodschap = "Nieuwe gebruiker kan niet worden opgeslagen";
                    }
                }
                else
                {
                    if (Repo.Update(MijnSelectedItem))
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
                        MijnSelectedItem.ErrorBoodschap = "update gebruiker is niet gelukt";
                    }
                }
            }
        }
    }
}
