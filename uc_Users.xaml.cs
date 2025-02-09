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
        }


        private void cboKeuze_Opvullen()
        {
            Loading = true;
            try
            {
                this.Cursor = Cursors.Wait;
                using (SqlConnection CN = new SqlConnection(Properties.Settings.Default.strCN))
                {
                    using (SqlCommand CMD = new SqlCommand(Properties., CN))
                    {
                        using (SqlDataAdapter DA = new SqlDataAdapter(CMD))
                        {
                            try
                            {
                                CMD.CommandType = CommandType.StoredProcedure;
                                DT = new DataTable();
                                DA.Fill(DT);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }

                        }
                    }
                }
                cboKeuze.ItemsSource = DT.DefaultView;
                cboKeuze.SelectedValuePath = "ProductID";
                cboKeuze.DisplayMemberPath = "ProductName";

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


        #endregion

        #region EVENTS
        private void cboKeuze_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
