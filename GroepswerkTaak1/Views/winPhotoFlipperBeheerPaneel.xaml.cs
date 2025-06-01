using GroepswerkTaak1.DAL;
using GroepswerkTaak1.Helpers;
using GroepswerkTaak1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GroepswerkTaak1.Views
{
	/// <summary>
	/// Interaction logic for winPhotoFlipperBeheerPaneel.xaml
	/// </summary>
	public partial class winPhotoFlipperBeheerPaneel : Window
	{

		public ObservableCollection<clsImagePhotoFlipper> Collection { get; private set; }
		private clsImagePhotoFlipperRepo _repo;
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

			var tobeDeletedIndex = ThumbnailList.SelectedIndex;
			var afterDeletionIndex = tobeDeletedIndex == 0 ? 0 : tobeDeletedIndex - 1;

			Collection.RemoveAt(ThumbnailList.SelectedIndex);
			ThumbnailList.SelectedIndex = afterDeletionIndex;
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

//foreach (string name in names)
//{
//	byte[] fullImage =
//		Helpers.clsFileToByteArrayLoader.ReadFileAsBytes($"C:\\Users\\PiotrZambrzycki\\Downloads\\{name}.png");
//	clsImagePhotoFlipper im = new()
//	{
//		ControlField = DBNull.Value,
//		FullImageBytes = fullImage,
//		ImageBytes = clsByteArrayToThumbnail.CreateThumbnail(fullImage, 250, 350),
//		FullImageId = -1
//	};
//	repo.Insert(im);
//}
