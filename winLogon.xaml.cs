using System.Windows;

namespace GroepswerkTaak1
{
    /// <summary>
    /// Interaction logic for winLogon.xaml
    /// </summary>
    public partial class winLogon : Window
    {
        public winLogon()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAnnuleer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
