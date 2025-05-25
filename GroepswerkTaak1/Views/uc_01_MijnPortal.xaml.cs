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

		public async Task InitializeAsync()
		{
			PhotoFlipper = new clsPhotoFlipper(PhotoFlipperImage, ImageHorizontalScaleTransform);
			await PhotoFlipper.InitializeAsync();
			LoadingAnimation.Visibility = Visibility.Collapsed;
			PhotoFlipperImage.Visibility = Visibility.Visible;

		}

		public void btnOpenTab_Click(object sender, RoutedEventArgs e)
		{
			var mainWindow = Window.GetWindow(this) as MainWindow;

			if (mainWindow == null) return;

			TabControl mainTabControl = mainWindow.tcMain;


			var button = sender as Button;
			if (button == null) return;

			string? tabName = button.Tag.ToString();
			if (tabName == null) return;

			var existingTab = mainTabControl.Items.OfType<TabItem>()
				.FirstOrDefault(t => t.Header?.ToString() == tabName);

			if (existingTab != null)
			{
				mainTabControl.SelectedItem = existingTab;
				return;
			}

			clsCustomTabItem newTab = new clsCustomTabItem
			{
				Background = (Brush)Application.Current.Resources["ThemeColor1"],
				BackgroundHighlighted = (Brush)Application.Current.Resources["ThemeColor2"],
				Header = tabName
			};

			mainTabControl.Items.Add(newTab);
			mainTabControl.SelectedItem = newTab;
		}

		private void PhotoFlipperImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{

		}
	}
}
