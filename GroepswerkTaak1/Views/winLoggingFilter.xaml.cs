using GroepswerkTaak1.DAL;
using GroepswerkTaak1.Model;
using GroepswerkTaak1.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace GroepswerkTaak1.Views
{
    /// <summary>
    /// Interaction logic for winLoggingFilter.xaml
    /// </summary>
    public partial class winLoggingFilter : Window
    {
      
        

        public winLoggingFilter()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
this.Close();
        }

        private void btnCleanFilters_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new clsLoggingVM (); // reset the DataContext to clear filters
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
    /// Bronnen :
    /// https://www.youtube.com/watch?v=wg1HUSIowTA&t=3s  // opmaak grid + uitleg
    /// https://www.youtube.com/watch?v=nnxCO4JX1Wc
    /// https://www.youtube.com/watch?v=fBKW-spQboc&t=2s  // filtering  + grouping
    /// https://www.youtube.com/watch?v=wpBRHNDFXYI&t=8s  //extra uitleg
    /// https://learn.microsoft.com/en-us/answers/questions/36999/how-to-speed-up-a-wpf-datagrid-bound-to-an-observa  //speeding up datagrid
    /// 




}
