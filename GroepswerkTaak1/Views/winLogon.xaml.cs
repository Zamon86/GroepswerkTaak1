using GroepswerkTaak1.DAL;
using GroepswerkTaak1.Model;
using GroepswerkTaak1.Views;
using System.Windows;

namespace GroepswerkTaak1
{
   

    /// <summary>
    /// Interaction logic for winLogon.xaml
    /// </summary>
    public partial class winLogon : Window
    {
        int LoginId;
        //clsUsersM Gebruiker = new clsUsersM();
        //clsRollenM Rol = new clsRollenM();
        clsLoginRepo LoginRepo = new clsLoginRepo();
        clsUsersRepo UsersRepo = new clsUsersRepo();
        clsRollenRepo RollenRepo = new clsRollenRepo();
        public winLogon()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //contoleer Login
            LoginId = LoginRepo.Login(txtLoginNaam.Text, txtLoginPwd.Password);
            if (LoginId > 0)
            {
                MessageBox.Show("Login gelukt"); // nadien verwijderen, alleen voor test doeleinden
                App.Gebruiker = UsersRepo.GetByID(LoginId);
                MessageBox.Show(App.Gebruiker.Email); // nadien verwijderen, alleen voor test doeleinden


                MainWindow _MainWindow = new MainWindow();
                _MainWindow.Show();
                this.Close();

            }
            else
            {
                MessageBox.Show("Login mislukt, probeer het opnieuw.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            

        }

        private void btnAnnuleer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
