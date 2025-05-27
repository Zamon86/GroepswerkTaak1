using System.Windows;
using System.Windows.Controls;

namespace GroepswerkTaak1.ToBeDeleted
{

	// Te verwijderen, ik heb een uitgebreide versie geschreven in het CustomControls project
	// Ik heb deze klasse gemaakt om een extra eigenschap te hebben voor de TabItem
	public class clsCustomTabItem : TabItem
    {
        // De notatie hieronder registreert eigenschap zodat het kan worden gebruikt in xaml
        public static readonly DependencyProperty IsClosableProperty = DependencyProperty.Register(
            nameof(IsClosable), typeof(bool), typeof(clsCustomTabItem), new PropertyMetadata(true));
        public bool IsClosable 
        {
            get => (bool)GetValue(IsClosableProperty);

            set => SetValue(IsClosableProperty, value);
        }
        


        // : base() hoeft niet te worden geschreven maar
        // het maakt het makkelijker voor mij om te lezen,
        // het laat me weten dat het verwijst naar de constructor van ouder
        public clsCustomTabItem() : base()
        {
            IsClosable = true;
        }

        public clsCustomTabItem(bool isClosable) : base()
        {
            IsClosable = isClosable;
        }
    }
}
