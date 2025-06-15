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

        private bool _isPasswordChanging = false;

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), 
                typeof(BindablePasswordBox), new PropertyMetadata(string.Empty,PasswordPropertyChanged));

        private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
          if(d is BindablePasswordBox passwordBox)
            {
                // Update the PasswordBox when the Password property changes
                passwordBox.UpdatePassword();
            }
        }

       
        public BindablePasswordBox()
        {
            InitializeComponent();
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _isPasswordChanging = true;
            //Sync zal werken van box naar property , omgekeerd willen we dat niet
            Password = passwordBox.Password;
            _isPasswordChanging = false;
        }

        private void UpdatePassword()
        {  if(!_isPasswordChanging)
                // Update the PasswordBox only if the change is not initiated by the PasswordBox itself
                passwordBox.Password = Password ;
        }

    }
}
