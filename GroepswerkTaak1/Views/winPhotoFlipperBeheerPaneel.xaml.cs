using GroepswerkTaak1.DAL;
using GroepswerkTaak1.Helpers;
using GroepswerkTaak1.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace GroepswerkTaak1.Views
{
	/// <summary>
	/// Interaction logic for winPhotoFlipperBeheerPaneel.xaml
	/// </summary>
	public partial class winPhotoFlipperBeheerPaneel : Window
	{

		public ObservableCollection<clsImagePhotoFlipper> Collection { get; private set; }
		private readonly clsImagePhotoFlipperRepo _repo;
		public winPhotoFlipperBeheerPaneel(clsPhotoFlipper photoFlipper)
		{
			_repo = photoFlipper.Repo;
			Collection = photoFlipper.Repo.GetAll();
			InitializeComponent();
			DataContext = this;
			ThumbnailList.SelectedIndex = 0;
		}

		private void ThumbnailList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			btnLeft.IsEnabled = ThumbnailList.SelectedIndex != 0;
			btnRight.IsEnabled = ThumbnailList.SelectedIndex != Collection.Count - 1;
		}

		private void BtnLeft_OnClick(object sender, RoutedEventArgs e)
		{
			ThumbnailList.SelectedIndex -= 1;
		}

		private void BtnRight_OnClick(object sender, RoutedEventArgs e)
		{
			ThumbnailList.SelectedIndex += 1;
		}

		private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
		{

			if (ThumbnailList.Items.Count == 1)
			{
				MessageBox.Show("The last item cannot be removed!");
				return;
			}

			if (!_repo.Delete((clsImagePhotoFlipper)ThumbnailList.SelectedItem))
			{
				MessageBox.Show("It was not possible to delete the item from the database");
				return;
			}

			var tobeDeletedIndex = ThumbnailList.SelectedIndex;
			var afterDeletionIndex = tobeDeletedIndex == 0 ? 0 : tobeDeletedIndex - 1;


			Collection.RemoveAt(ThumbnailList.SelectedIndex);
			ThumbnailList.SelectedIndex = afterDeletionIndex;

		}

		private async void BtnNew_OnClick(object sender, RoutedEventArgs e)
		{
			try
			{
				var openFileDialog = new OpenFileDialog
				{
					Filter = "Images (*.jpg;*.jpeg;*.bmp;*.png)|*.jpg;*.jpeg;*.bmp;*.png",
					Title = "Select image files",
					Multiselect = true
				};

				var result = openFileDialog.ShowDialog();

				if (result != true) return;

				string[] selectedFilePaths = openFileDialog.FileNames;

				await AddImagesToRepositoryAndCollection(selectedFilePaths);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		private async Task AddImagesToRepositoryAndCollection(string[] selectedFilePaths)
		{
			SelectedImage.Visibility = Visibility.Collapsed;
			LoadingAnimation.Visibility = Visibility.Visible;

			await Task.Run(() =>
			{
				try
				{
					foreach (var filePath in selectedFilePaths)
					{

						var fullImage = clsFileToByteArrayLoader.ReadFileAsBytes(filePath);
						clsImagePhotoFlipper im = new()
						{
							ControlField = DBNull.Value,
							FullImageBytes = fullImage,
							ImageBytes = clsByteArrayToThumbnail.CreateThumbnail(fullImage, 250, 350),
							FullImageId = -1
						};
						_repo.Insert(im);
					}
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			});
			
			_repo.UpdateCollection();
			
			LoadingAnimation.Visibility = Visibility.Collapsed;
			SelectedImage.Visibility = Visibility.Visible;
			ThumbnailList.SelectedIndex = ThumbnailList.Items.Count - 3;
		}

		private void BtnReplace_OnClick(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}






//var repo = new clsImagePhotoFlipperRepo();
//string[] names =
//[
//	"Codewars",
//	"Dotnet",
//	"Csharp",
//	"Bild",
//	"WallStreetJournal",
//	"Forbes",
//	"Syntra",
//	"Guardian",
//	"NYTimes"
//];


