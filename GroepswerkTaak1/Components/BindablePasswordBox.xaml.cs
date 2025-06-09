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

namespace GroepswerkTaak1.Components
{
    /// <summary>
    /// Interaction logic for BindablePasswordBox.xaml
    /// 
    /// zie  bron : https://www.youtube.com/watch?v=G9niOcc5ssw&t=1s
    /// 
    /// standaard passwordbox kan niet gebind worden aan een property
    /// dus maken we een UserControl die de PasswordBox bevat
    /// en zo meer controle hebben over de PasswordBox
    /// 
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {



        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), 
                typeof(BindablePasswordBox), new PropertyMetadata(string.Empty));



        public BindablePasswordBox()
        {
            InitializeComponent();
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //Sync zal werken van box naar property , omgekeerd willen we dat niet
            Password = passwordBox.Password;
        }
    }
}
