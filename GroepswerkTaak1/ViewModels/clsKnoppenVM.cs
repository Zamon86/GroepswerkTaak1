using Common;
using GroepswerkTaak1.DAL;
using GroepswerkTaak1.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GroepswerkTaak1.ViewModels
{
    public class clsKnoppenVM : clsCommonModelPropertiesBase
    {

        public clsKnoppenRepo repo = new clsKnoppenRepo();
		//public clsKnoppenVM()
		//{

		//}


		public ICommand cmdLoadPicture { get; set; }
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
					OpslaanCommando(null);
					LoadData();
				}
				_MijnSelectedItem = value;
				OnPropertyChanged();
			}
		}


		public clsKnoppenVM()
		{
			
			// als test forceer ik het een item

			cmdNew=new clsCustomCommand(NewCommando, canExecuteNew);
            cmdClose = new clsCustomCommand(CloseCommando, canExecuteClose);
			cmdCancel = new clsCustomCommand(CancelCommando, canExecuteCancel);
			cmdSave = new clsCustomCommand(OpslaanCommando,canExecuteSave);
            cmdLoadPicture = new clsCustomCommand(LoadImageCommando, CanExecuteLoadImage);
			cmdDelete = new clsCustomCommand(DeleteCommando, CanExecuteDelete);
            LoadData();
            _MijnSelectedItem = repo.GetByID((short)1); // haal een item op met ID 1
		}

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

        private bool CanExecuteLoadImage(object obj)
        {
            return MijnSelectedItem != null;
        }

        private void LoadImageCommando(object obj)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                // Read image bytes
                byte[] bytes = File.ReadAllBytes(filePath);
                MijnSelectedItem.KnopImage = bytes;

                // Optional: convert to ImageSource for preview
                var bitmap = new BitmapImage();
                using (var stream = new MemoryStream(bytes))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                    bitmap.Freeze(); // Important for cross-thread use
                }

              //  MijnSelectedItem.KnopImage = File.ReadAllBytes(bitmap);
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

		private void OpslaanCommando(object obj)
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
						MijnSelectedItem.MyVisibility = (int)Visibility.Visible;										// eventueel knoppen aan of uitzetten
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
