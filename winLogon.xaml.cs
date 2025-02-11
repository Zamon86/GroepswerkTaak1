using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

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
