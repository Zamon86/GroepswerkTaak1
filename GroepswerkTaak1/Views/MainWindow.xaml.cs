using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using GroepswerkTaak1.CustomControls;
using GroepswerkTaak1.Views;

namespace GroepswerkTaak1
{

	public partial class MainWindow : Window
	{
		#region VARIABLES		
		private string strAuthorisatie = string.Empty;  // deze string zal de authorisatie dragen

		private DispatcherTimer _timer = new();

		private readonly Dictionary<string, UserControl> _ucInstances = new();

		private Dictionary<string, Type> _ucMapping = new()
				{
						{"Mijn portal", typeof(uc_01_MijnPortal) },
						{"Smoelenboek", typeof(uc_02_Smoelenboek) },
						{"Helpdesk TD", typeof(uc_03_HelpdeskTD) },
						{"Helpdesk ICT", typeof(uc_04_HelpdeskICT) },
						{"Bladeren DMS", typeof(uc_05_BladerenInDMS) },
						{"Helpdesk keuken", typeof(uc_06_HelpdeskKeuken) },
						{"Admin paneel", typeof(uc_07_PortalBeheerpaneel) }
				};

		#endregion

		public MainWindow()
		{		
			InitializeComponent();	

			//hier lezen we de authorisatie in vanuit het loginscherm
			// TODO
			// strAuthorisatie = winLogon.strAuthorisatie;
			// display de machtiging op het scherm dmv  een label of textblock
		}

		#region METHODS
		private void OpenUserControl(UserControl myUS)
		{			
			if (grdMain.Children.Count > 1)
			{				
				grdMain.Children.RemoveAt(1);
				
			}

			if (myUS.Parent is Panel panel)
			{
				panel.Children.Remove(myUS);
			}

			Grid.SetColumn(myUS, 1);
			Grid.SetRow(myUS, 0);
			grdMain.Children.Add(myUS);
		}

		private void StartTimer()
		{
			_timer.Interval = TimeSpan.FromSeconds(1);
			_timer.Tick += (s, e) => txtDateAndTime.Text = DateTime.Now.ToString();
			_timer.Start();
		}		

		private void CreateMainUserControl()
		{
			uc_01_MijnPortal uc_01_MijnPortal = new uc_01_MijnPortal();
			OpenUserControl(uc_01_MijnPortal);
			_ucInstances["Mijn portal"] = uc_01_MijnPortal;
		}

		private void CreateStartTab()
		{
			var startTab = new clsCustomTabItem()
			{
				Background = (Brush)Application.Current.Resources["ThemeColor1"],
				BackgroundHighlighted = (Brush)Application.Current.Resources["ThemeColor2"],
				Header = "Mijn portal",
				IsCloseable = false
			};

			tcMain.Items.Add(startTab);
			tcMain.SelectedIndex = 0;
		}
		#endregion

		#region EVENTS


		private void Window_Loaded(object sender, RoutedEventArgs e)
		{			
			StartTimer();
			CreateStartTab();
			CreateMainUserControl();
		}

		private void btnAfmelden_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Not implemented");
		}

		private void btnDannyTest_Click(object sender, RoutedEventArgs e)
		{
			uc_Users _uc_Users = new uc_Users();
			OpenUserControl(_uc_Users);
		}

		private void tcMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (tcMain.SelectedIndex == -1) return;

			string? key = ((clsCustomTabItem)tcMain.SelectedItem).Header.ToString();
			if (key == null) return;

			if (_ucInstances.TryGetValue(key, out UserControl? cachedUC))
			{
				OpenUserControl(cachedUC);
				return;
			}

			_ucMapping.TryGetValue(key, out Type? typeUC);
			if (typeUC == null) return;

			var uc = Activator.CreateInstance(typeUC);

			if (uc is UserControl)
			{
				_ucInstances[key] = (UserControl)uc;
				OpenUserControl((UserControl)uc);
			}
		}

		#endregion

		
		}

}