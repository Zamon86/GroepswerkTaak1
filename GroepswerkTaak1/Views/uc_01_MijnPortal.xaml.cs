using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GroepswerkTaak1
{

	public partial class uc_01_MijnPortal : UserControl
	{
		public clsPhotoFlipper? PhotoFlipper { get; set; }
		
		public uc_01_MijnPortal()
		{
			InitializeComponent();
			DataContext = this;
			Task task = InitalizeAsync();
		}

		public async Task InitalizeAsync()
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
				Style = (Style)FindResource("TabItemStyle"),
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
