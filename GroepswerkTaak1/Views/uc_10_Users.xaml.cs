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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GroepswerkTaak1.Views
{
    /// <summary>
    /// Interaction logic for uc_10_Users.xaml
    /// extra code om een commit te forceren , mag weg
    /// </summary>
    public partial class uc_10_Users : UserControl
    {
        public uc_10_Users()
        {
            InitializeComponent();
        }

        private void btnSluiten_Click(object sender, RoutedEventArgs e)
        {
           // this.Visibility = Visibility.Collapsed;
            //  this.Parent.Controls.Remove(this);

            if (this.Parent is Panel panel)
            {
                panel.Children.Remove(this);
            }
        }

       
    }
}
