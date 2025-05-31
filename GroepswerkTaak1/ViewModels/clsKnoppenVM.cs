using Common;
using GroepswerkTaak1.DAL;
using GroepswerkTaak1.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GroepswerkTaak1.ViewModels
{
    public class clsKnoppenVM : clsCommonModelPropertiesBase
    {

        public clsKnoppenRepo repo = new clsKnoppenRepo();
		//public clsKnoppenVM()
		//{

		//}



		public ICommand cmdSave {  get; set; }

        public ICommand cmdClose { get; set; }

        public ICommand cmdDelete { get; set; }

        public ICommand cmdCancel { get; set; }

        public ICommand cmdNew { get; set; }
        #region CodeVanDanny

        private bool _NewStatus = false;
		//clsKnoppenRepo Repo = new clsKnoppenRepo();

		private ObservableCollection<clsKnoppenM>? _MijnCollectie;
		public ObservableCollection<clsKnoppenM> MijnCollectie
		{
			get { return _MijnCollectie; }
			set { _MijnCollectie = value; OnPropertyChanged(); }
		}

		private clsKnoppenM? _MijnSelectedItem;

		public clsKnoppenM MijnSelectedItem
		{
			get { return _MijnSelectedItem; }
			set
			{  // Validatie schrijven wanneer een ander item in ComboBx wordt geselecteerd
				if (value != null)
				{
					//OpslaanCommando();
					LoadData();
				}
				_MijnSelectedItem = value;
				OnPropertyChanged();
			}
		}


		public clsKnoppenVM()
		{
			LoadData();
			// als test forceer ik het een item

			cmdNew=new clsCustomCommand(NewCommando, canExecuteNew);
            cmdClose = new clsCustomCommand(CloseCommando, canExecuteClose);
			cmdCancel = new clsCustomCommand(CancelCommando, canExecuteCancel);

            _MijnSelectedItem = repo.GetByID((short)1); // haal een item op met ID 1
		}

        private bool canExecuteCancel(object obj)
        {
            return !_NewStatus;
        }

        private void CancelCommando(object obj)
        {
			MijnSelectedItem = repo.GetFirst();
			if (MijnSelectedItem != null) {
			MijnSelectedItem.MyVisibility=(int)Visibility.Visible;
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

		private void OpslaanCommando()
		{
			if (MijnSelectedItem != null)
			{
				if (_NewStatus) // nieuw item
				{
					if (repo.Insert(MijnSelectedItem))
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
						MijnSelectedItem.ErrorBoodschap = "Nieuwe knop kan niet worden opgeslagen";
					}
				}
				else
				{
					if (repo.Update(MijnSelectedItem))
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
						MijnSelectedItem.ErrorBoodschap = "update knop is niet gelukt";
					}
				}
			}
		}

		private void NewCommando(object obj)
		{
			clsKnoppenM ItemToInsert = new clsKnoppenM()
			{
				KnopId = 0



			};
			MijnSelectedItem = ItemToInsert;
		MijnSelectedItem.MyVisibility=(int)Visibility.Hidden;
            _NewStatus=true;



        }
		#endregion

	}
}
