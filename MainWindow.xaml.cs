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

        public MainWindow()
        {
            InitializeComponent();
        }

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
                Style = (Style)FindResource("TabItemStyle"),
                Header = tabName                
            };     
            
            tcMain.Items.Add(newTab);
            tcMain.SelectedItem = newTab;            
        }     

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            menuData.ReadDataFromConfigFile();
            mnuMainMenu.ItemsSource = menuData.CreateMenuItems();
        }
    }






}