using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GroepswerkTaak1.CustomControls;
using GroepswerkTaak1.ViewModels;
using GroepswerkTaak1.Helpers;


namespace GroepswerkTaak1.Views
{

	public partial class uc_01_MijnPortal : UserControl
	{
		public clsPhotoFlipper? PhotoFlipper { get; set; }
		public int UserRoleId { get; }

		public uc_01_MijnPortal(int userRoleId)
		{
			UserRoleId = userRoleId;
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
			if (sender is not Image image) return;
			var source = PhotoFlipper?.ActiveImage?.ImageBytes;
			if (source == null) return;
			
			clsFileHelper.OpenBytesAsTempFile(source);
		}
        private void OpenUserControl(UserControl myUserControl)
        {
            if (grdExpanders.Children.Count > 1)
            {
                grdExpanders.Children.RemoveAt(1);
            }
          //  Grid.SetColumn(myUserControl, 1);
          //  Grid.SetRow(myUserControl, 0);
            grdExpanders.Children.Add(myUserControl);
        }
        private void ListBox_Selected(object sender, RoutedEventArgs e)
        {
            String keuze = ((ListBoxItem)sender).Tag.ToString();
            switch (keuze)
            {
             case "1":
                    var _uc_Knoppen = new uc_Knoppen();
                    OpenUserControl(_uc_Knoppen);
                    break;
             case "2":
                break;
             case "3":
                    var _uc_Users = new uc_Users();
                    OpenUserControl(_uc_Users);
                   
                break;
             case "4":
                break;
             case "5":
                break;
             case "6":
                break;
			 default:
                MessageBox.Show("Onbekende keuze gemaakt.");
                break;
            }

        }
    }
}
