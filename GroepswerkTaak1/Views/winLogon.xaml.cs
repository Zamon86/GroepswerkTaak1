﻿using GroepswerkTaak1.DAL;
using GroepswerkTaak1.Model;
using GroepswerkTaak1.Views;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

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
		clsUserRepo UsersRepo = new clsUserRepo();
		clsRollenRepo RollenRepo = new clsRollenRepo();
		public winLogon()
		{
			InitializeComponent();
			LoginRepo.Logging("Starten", "Program", "Form", "WinLogon", "Windows"); // Log de actie van het openen van het login venster
		}

		private void btnLogin_Click(object sender, RoutedEventArgs e)
		{
			//contoleer Login
			LoginId = LoginRepo.Login(txtLoginNaam.Text, txtLoginPwd.Password);
			if (LoginId > 0)
			{
				//MessageBox.Show("Login gelukt"); // nadien verwijderen, alleen voor test doeleinden
				clsActiveUserData.ActiveUser = UsersRepo.GetByID(LoginId);
				clsActiveUserData.ActiveUserRole = RollenRepo.GetByID(clsActiveUserData.ActiveUser.RolId);
				//MessageBox.Show(App.Gebruiker.Email); // nadien verwijderen, alleen voor test doeleinden
				//MessageBox.Show(App.Rol.RolNaam); // nadien verwijderen, alleen voor test doeleinden
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

		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
				this.DragMove();
		}

		void DataWindow_Closing(object sender, CancelEventArgs e)
		{
			LoginRepo.Logging("Sluiten", "Program", "Form", "WinLogon", "Windows"); // Log de actie van het sluiten van het login venster
		}
	}
}
