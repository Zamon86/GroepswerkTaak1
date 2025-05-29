using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using GroepswerkTaak1.CustomControls;

namespace GroepswerkTaak1.Views
{

	public partial class MainWindow : Window
	{
		#region VARIABLES		
		private string strAuthorisatie = string.Empty;  // deze string zal de authorisatie dragen

		
		private readonly DispatcherTimer _timer = new();

		// Het wordt gebruikt voor het opslaan van instanties van UserControls.
		private readonly Dictionary<string, UserControl> _ucInstances = new();

		// Het is een mapping van UserControls, waarbij namen (strings) worden gekoppeld aan hun bijbehorende typen.
		private readonly Dictionary<string, Type> _ucMapping = new()
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
		private void OpenUserControl(UserControl myUserControl)
		{			
			if (grdMain.Children.Count > 1)
			{				
				grdMain.Children.RemoveAt(1);
				
			}

			if (myUserControl.Parent is Panel panel)
			{
				panel.Children.Remove(myUserControl);
			}

			Grid.SetColumn(myUserControl, 1);
			Grid.SetRow(myUserControl, 0);
			grdMain.Children.Add(myUserControl);
		}

		// Deze methode configureert een timer met een interval van één seconde,
		// waarbij bij elke tick de huidige datum en tijd wordt bijgewerkt in statusbalk.
		private void StartTimer()
		{
			_timer.Interval = TimeSpan.FromSeconds(1);
			_timer.Tick += (s, e) => txtDateAndTime.Text = DateTime.Now.ToString();
			_timer.Start();
		}

		// Deze methode maakt instantie van onze portal
		private void CreateMainUserControl()
		{
			var uc_01_MijnPortal = new uc_01_MijnPortal();
			OpenUserControl(uc_01_MijnPortal);
			_ucInstances["Mijn portal"] = uc_01_MijnPortal;
		}


		// Deze methode maakt eerste tab
		private void CreateStartTab()
		{
			var startTab = new clsCustomTabItem()
			{
				Background = Application.Current.Resources["ThemeColor1"] as Brush,
				BackgroundHighlighted = Application.Current.Resources["ThemeColor2"] as Brush,
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
			// var _uc_Users = new uc_Users();
			// OpenUserControl(_uc_Users);
			var _uc_Knoppen = new uc_Knoppen();
            OpenUserControl(_uc_Knoppen);


        }


		//Deze code behandelt de gebeurtenis SelectionChanged van een TabControl.
		//Wanneer een tabblad wordt geselecteerd,
		//controleert de methode of er een bijbehorende UserControl-instantie
		//in de _ucInstances-cache aanwezig is.
		//Als dat zo is, wordt deze geopend. Als de instantie niet in de cache staat,
		//wordt het type van de UserControl opgehaald uit _ucMapping. Vervolgens wordt een nieuwe instantie
		//van dat type gemaakt en toegevoegd aan de cache.
		//Daarna wordt de nieuwe UserControl geopend.
		private void tcMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (tcMain.SelectedIndex == -1) return;

			var key = ((clsCustomTabItem)tcMain.SelectedItem).Header.ToString();
			if (key == null) return;

			if (_ucInstances.TryGetValue(key, out var cashedUserControl))
			{
				OpenUserControl(cashedUserControl);
				return;
			}

			_ucMapping.TryGetValue(key, out var typeUserControl);
			if (typeUserControl == null) return;

			var uc = Activator.CreateInstance(typeUserControl);

			if (uc is not UserControl control) return;
			
			_ucInstances[key] = control;
			
			OpenUserControl(control);
		}

		#endregion

		
		}

}