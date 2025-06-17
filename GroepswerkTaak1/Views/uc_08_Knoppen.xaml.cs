using GroepswerkTaak1.Model;
using GroepswerkTaak1.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace GroepswerkTaak1
{
    /// <summary>
    /// Interaction logic for uc_Knoppen.xaml
    /// </summary>
    public partial class uc_Knoppen : UserControl
    {
        public uc_Knoppen()
        {
            InitializeComponent();
        }
        //bool IsNew = false;
        //clsKnoppenVM clsKnoppenVM = new clsKnoppenVM();
        private void cboKeuze_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnNieuw_Click(object sender, RoutedEventArgs e)
        {
        
            
            //IsNew = true;

            //clsKnoppenM x = new clsKnoppenM();
            //DataContext = x;

            //lblKeuze.Visibility = Visibility.Hidden;
            //cboKeuze.Visibility = Visibility.Hidden;
        }

        

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
           // clsKnoppenM MyLocalObject = DataContext as clsKnoppenM;
           // if (MyLocalObject is not null)
           // {
           //     if (IsNew)
           //     {
           //         clsKnoppenVM.repo.Insert(MyLocalObject);
           //     }
           //     else
           //     {
           //         clsKnoppenVM.repo.Update(MyLocalObject);
           //     }
           // }
           //// CboKeuzeOpvullen();
           //// cboKeuze.SelectedIndex = MijnSelectedIndex;
           // IsNew = false;

           // lblKeuze.Visibility = Visibility.Visible;
           // cboKeuze.Visibility = Visibility.Visible;
        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSluiten_Click(object sender, RoutedEventArgs e)
        {
         
          //  this.Parent.Controls.Remove(this);

            if (this.Parent is Panel panel)
            {
                panel.Children.Remove(this);
            }

        }

   
    }
}
