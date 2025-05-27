using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GroepswerkTaak1.CustomControls;

namespace GroepswerkTaak1.Views
{

	public partial class uc_01_MijnPortal : UserControl
	{
		public clsPhotoFlipper? PhotoFlipper { get; set; }

		public uc_01_MijnPortal()
		{
			InitializeComponent();
			DataContext = this;
			Task task = InitializeAsync();
		}


		//Deze asynchrone methode initialiseert het clsPhotoFlipper-object,
		//Na voltooiing wordt de laadanimatie verborgen en de afbeelding weergegeven.
		public async Task InitializeAsync()
		{
			PhotoFlipper = new clsPhotoFlipper(PhotoFlipperImage, ImageHorizontalScaleTransform);
			await PhotoFlipper.InitializeAsync();
			LoadingAnimation.Visibility = Visibility.Collapsed;
			PhotoFlipperImage.Visibility = Visibility.Visible;

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

		private void PhotoFlipperImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			// te implementeren -> opslaan als een bestand en openen met default app
		}
	}
}
