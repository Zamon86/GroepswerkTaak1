using System.Text;
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

    public partial class MainWindow : Window
    {
        private clsMenuData menuData = new clsMenuData();
        private string strAuthorisatie = string.Empty;  // deze string zal de authorisatie dragen
        public MainWindow()
        {
            InitializeComponent();
            //hier lezen we de authorisatie in vanuit het loginscherm
            // TODO
            // strAuthorisatie = winLogon.strAuthorisatie;
            // display de machtiging op het scherm dmv  een label of textblock
        }

        #region METHODES

        private void OpenUserControl(UserControl myUS)
        {
            if (grdMain.Children.Count > 1)
            {
                grdMain.Children.RemoveAt(1);
            }
            Grid.SetColumn(myUS, 1);
            Grid.SetRow(myUS, 0);
            grdMain.Children.Add(myUS);
        }

        #endregion


        public void btnOpenTab_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            string? tabName = button.Tag.ToString();
            if (tabName == null) return;

            var existingTab = tcMain.Items.OfType<TabItem>()
                .FirstOrDefault(t => t.Header?.ToString() == tabName);

            if (existingTab != null)
            {
                tcMain.SelectedItem = existingTab;
                return;
            }

            TabItem newTab = new TabItem
            {
                Header = tabName,
            };

            tcMain.Items.Add(newTab);
            tcMain.SelectedItem = newTab;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            menuData.ReadDataFromConfigFile();
            mnuMainMenu.ItemsSource = menuData.CreateMenuItems();
        }

        private void btnDannyTest_Click(object sender, RoutedEventArgs e)
        {
            uc_Users _uc_Users = new uc_Users();
            OpenUserControl(_uc_Users);
        }
    }






}