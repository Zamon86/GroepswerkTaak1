using System;
using System.Collections.Generic;
using System.Data;
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
using Microsoft.Data.SqlClient;

namespace GroepswerkTaak1
{
    /// <summary>
    /// Interaction logic for uc_Users.xaml
    /// </summary>
    public partial class uc_Users : UserControl
    {
        #region  VARIABELEN
        DataTable DTUsers;
        DataTable DTRollen;
        DataRow DR;
        bool Loading = true;
        bool IsUpdate = true; // false = Nieuws   , True = wijzigen
        object ControlField;
        #endregion

        #region METHODES
        public uc_Users()
        {
            InitializeComponent();
            cboKeuze_Opvullen();
            cboRollen_Opvullen();
        }


        private void cboKeuze_Opvullen()
        {
            Loading = true;
            try
            {
                this.Cursor = Cursors.Wait;
                using (SqlConnection CN = new SqlConnection(Properties.Settings.Default.strCN))
                {
                    using (SqlCommand CMD = new SqlCommand(Properties.Resources.S_Users, CN))
                    {
                        using (SqlDataAdapter DA = new SqlDataAdapter(CMD))
                        {
                            try
                            {
                                CMD.CommandType = CommandType.StoredProcedure;
                                DTUsers = new DataTable();
                                DA.Fill(DTUsers);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                    }
                }
                cboKeuze.ItemsSource = DTUsers.DefaultView;
                cboKeuze.SelectedValuePath = "ID";
                cboKeuze.DisplayMemberPath = "loginNaam";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Er heeft zich een fout voorgedaan bij het opvullen van de lijst" + ex.ToString(), "opvul fout", MessageBoxButton.OK);
            }
            finally
            {
                this.Cursor = null;
            }
            Loading = false;
        }

        private void cboRollen_Opvullen()
        {
            Loading = true;
            try
            {
                this.Cursor = Cursors.Wait;
                using (SqlConnection CN = new SqlConnection(Properties.Settings.Default.strCN))
                {
                    using (SqlCommand CMD = new SqlCommand(Properties.Resources.S_Rollen, CN))
                    {
                        using (SqlDataAdapter DA = new SqlDataAdapter(CMD))
                        {
                            try
                            {
                                CMD.CommandType = CommandType.StoredProcedure;
                                DTRollen = new DataTable();
                                DA.Fill(DTRollen);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                    }
                }
                cboMachteging.ItemsSource = DTRollen.DefaultView;
                cboMachteging.SelectedValuePath = "ID";
                cboMachteging.DisplayMemberPath = "rol";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Er heeft zich een fout voorgedaan bij het opvullen van de lijst" + ex.ToString(), "opvul fout", MessageBoxButton.OK);
            }
            finally
            {
                this.Cursor = null;
            }
            Loading = false;
        }


        public void GetUsersByID(int ID)
        {
            DTUsers.PrimaryKey = new DataColumn[] { DTUsers.Columns["ID"] };
            DR = DTUsers.Rows.Find(ID);
            if (DR == null)
            {
                ControlsLeegmaken();
            }
            else
            {
                cboMachteging.SelectedIndex = (int)DR["ID"];
                txtNaam.Text = DR[2].ToString();
                txtVoornaam.Text = DR[3].ToString();
                txtEmail.Text = DR[4].ToString();
                txtTelefoon.Text = DR[5].ToString();
                cboMachteging.SelectedIndex = (int)DR[6]-1;// selected index start aan 0 , onze tabel start aan 1
                chkActief.IsChecked = (bool)DR[7];
                ControlField = DR[8];
            }
        }
        private void ControlsLeegmaken()
        {
            lblLogin.Visibility = Visibility.Visible;
            cboKeuze.Visibility = Visibility.Visible;
            cboKeuze.SelectedIndex = -1;
            cboMachteging.SelectedIndex = -1;
            txtNaam.Text = string.Empty;
            txtVoornaam.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelefoon.Text = string.Empty;
            chkActief.IsChecked = false;
        }


        #endregion

        #region EVENTS
        private void cboKeuze_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Loading == false)
            {
                if (cboKeuze.SelectedIndex >= 0)
                {
                    GetUsersByID((int)cboKeuze.SelectedValue);
                }
                else
                {
                    ControlsLeegmaken();
                    cboKeuze.Focus();
                }
            }
        }

        private void cboMachteging_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnNieuw_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSluiten_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }

}
