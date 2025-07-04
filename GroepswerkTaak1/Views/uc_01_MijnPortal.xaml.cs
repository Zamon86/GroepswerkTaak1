﻿using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GroepswerkTaak1.CustomControls;
using GroepswerkTaak1.ViewModels;
using GroepswerkTaak1.Helpers;
using GroepswerkTaak1.Model;




namespace GroepswerkTaak1.Views
{

	public partial class uc_01_MijnPortal : UserControl
	{
		public clsPhotoFlipper? PhotoFlipper { get; set; }
		

		public uc_01_MijnPortal()
		{
			InitializeComponent();
			DataContext = this;

			var task = InitializeAsync();
		}


		//Deze asynchrone methode initialiseert het clsPhotoFlipper-object,
		//Na voltooiing wordt de laadanimatie verborgen en de afbeelding weergegeven.
		public async Task InitializeAsync()
		{
			PhotoFlipper = new clsPhotoFlipper(PhotoFlipperImage, ImageHorizontalScaleTransform);
			await PhotoFlipper.InitializeAsync();
			LoadingAnimation.Visibility = Visibility.Collapsed;
			PhotoFlipperImage.Visibility = Visibility.Visible;
			btnPhotoFlipperSettings.IsEnabled = true;

		}


		//Deze code definieert een methode die de Click-gebeurtenis afhandelt
		//voor 6 knoppen in de UI. Het checkt of bepaalde tab al bestaat.
		//Als het bestaat, wordt deze geopend. Anders gaat het nieuwe tab maken.
		public void btnOpenTab_Click(object sender, RoutedEventArgs e)
		{
			if (Window.GetWindow(this) is not MainWindow mainWindow) return;

			var mainTabControl = mainWindow.tcMain;

			if (sender is not Button button) return;

			var tabName = button.Tag.ToString();
			if (tabName == null) return;

			var existingTab = mainTabControl.Items.OfType<TabItem>()
				.FirstOrDefault(t => t.Header?.ToString() == tabName);

			if (existingTab != null)
			{
				mainTabControl.SelectedItem = existingTab;
				return;
			}

			var newTab = new clsCustomTabItem
			{

				Background = Application.Current.Resources["ThemeColor1"] as Brush,
				BackgroundHighlighted = Application.Current.Resources["ThemeColor2"] as Brush,
				Header = tabName
			};

			mainTabControl.Items.Add(newTab);
			mainTabControl.SelectedItem = newTab;
		}

		//Deze code ondersteunt klikken op fotoflipper
		private void PhotoFlipperImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (sender is not Image image) return;
			if (PhotoFlipper?.ActiveImage is clsImagePhotoFlipper activeImage)
			{
				if (activeImage.FullImageBytes == null)
				{
					if (PhotoFlipper.Repo.LoadImageFull(activeImage))
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

		private void OpenUserControl(UserControl myUserControl)
		{
			if (grdExpanders.Children.Count > 1)
			{
				grdExpanders.Children.RemoveAt(1);
			}
			grdExpanders.Children.Add(myUserControl);
		}

		private void ButtonSettings_OnClick(object sender, RoutedEventArgs e)
		{
			if (PhotoFlipper != null)
			{
				var window = new winPhotoFlipperBeheerPaneel(PhotoFlipper);

				// luisteren om te zien of een venster gesloten is
				window.Closed += (s, args) =>
				{
					btnPhotoFlipperSettings.IsEnabled = true;
					PhotoFlipper.Timer.Start();
					PhotoFlipperImage.Opacity = 1;
					PhotoFlipperImage.IsEnabled = true;
				};

				PhotoFlipper.Timer.Stop();
				PhotoFlipperImage.IsEnabled = false;
				PhotoFlipperImage.Opacity = 0.6;
				window.Show();
			}

			btnPhotoFlipperSettings.IsEnabled = false;
		}

		private void AdminListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (sender is not ListBox listBox) return;
			if (listBox.SelectedItem is not ListBoxItem selectedItem) return;

			var keuze = selectedItem.Tag.ToString();
			if (keuze == null) return;

			switch (keuze)
			{
				case "Knoppen":
					OpenUserControl(new uc_Knoppen());
					break;
				case "Rollen":
					OpenUserControl(new uc_Rollen());
					break;
				case "Users":
					OpenUserControl(new uc_10_Users());
					break;
				case "Logging":
					new winLoggingFilter().ShowDialog();
					break;
				default:
					MessageBox.Show("Onbekende keuze gemaakt.");
					break;
			}

			listBox.SelectedItem = null;

		}
	}
}
