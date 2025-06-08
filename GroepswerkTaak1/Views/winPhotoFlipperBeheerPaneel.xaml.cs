using GroepswerkTaak1.DAL;
using GroepswerkTaak1.Helpers;
using GroepswerkTaak1.Model;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using System.Windows.Threading;

namespace GroepswerkTaak1.Views
{
	/// <summary>
	/// Interaction logic for winPhotoFlipperBeheerPaneel.xaml
	/// </summary>
	public partial class winPhotoFlipperBeheerPaneel : Window
	{

		public ObservableCollection<clsImagePhotoFlipper> Collection { get; }
		private readonly clsImagePhotoFlipperRepo _repo;
		
		public winPhotoFlipperBeheerPaneel(clsPhotoFlipper photoFlipper)
		{
			
			_repo =  photoFlipper.Repo;
			Collection = photoFlipper.Repo.GetAll();
			InitializeComponent();
			DataContext = this;
			ThumbnailList.SelectedIndex = 0;
		}

		private void ThumbnailList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			btnLeft.IsEnabled = ThumbnailList.SelectedIndex != 0;
			btnRight.IsEnabled = ThumbnailList.SelectedIndex != Collection.Count - 1;
			ScrollSelectionIntoView(ThumbnailList);
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
				MessageBox.Show(ex.Message);
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
							ThumbnailBytes = clsByteArrayToThumbnail.CreateThumbnail(fullImage, 250, 350),
							FullImageId = -1
						};
						if (!_repo.Insert(im)) MessageBox.Show("Insert failed!");
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
			ThumbnailList.SelectedIndex = Math.Max(0, ThumbnailList.Items.Count - 3);
		}


		private void BtnReplace_OnClick(object sender, RoutedEventArgs e)
		{
			var image = (clsImagePhotoFlipper)ThumbnailList.SelectedItem;
			var index = ThumbnailList.SelectedIndex;


			var openFileDialog = new OpenFileDialog
			{
				Filter = "Image (*.jpg;*.jpeg;*.bmp;*.png)|*.jpg;*.jpeg;*.bmp;*.png",
				Title = "Select a replacement image",
				Multiselect = false
			};

			if (openFileDialog.ShowDialog() != true) return;

			var filePath = openFileDialog.FileName;
			
			try
			{
				image.FullImageBytes = File.ReadAllBytes(filePath);
				image.ThumbnailBytes = clsByteArrayToThumbnail.CreateThumbnail(image.FullImageBytes, 250, 350);
				
				if (!_repo.Update(image)) MessageBox.Show("Failed to replace the image.");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

			_repo.UpdateCollection();
			ThumbnailList.SelectedIndex = index;
		}

		private static void ScrollSelectionIntoView(ListBox? listBox)
		{
			if (listBox?.SelectedItem == null) return;

			listBox.UpdateLayout();
			listBox.ScrollIntoView(listBox.SelectedItem);
		}

		private void ThumbnailList_OnSizeChanged(object sender, SizeChangedEventArgs e)
		{
			ScrollSelectionIntoView(sender as ListBox);
		}

		private void SelectedImage_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (sender is not Image image) return;
			if (ThumbnailList.SelectedItem is clsImagePhotoFlipper activeImage)
			{
				if (activeImage.FullImageBytes == null)
				{
					if (_repo.LoadImageFull(activeImage))
					{
						clsFileHelper.OpenBytesAsTempFile(activeImage.FullImageBytes!);
					}
				}
				else
				{
					var source = activeImage.FullImageBytes;
					if (source == null) return;
					clsFileHelper.OpenBytesAsTempFile(source);
				}
			}
		}
	}
}
