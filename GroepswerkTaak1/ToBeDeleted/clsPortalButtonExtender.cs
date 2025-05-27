using System.Windows;

namespace GroepswerkTaak1.ToBeDeleted
{
	//Ik heb deze class gemaakt om de mogelijkheden van de buttons uit te breiden. Ik gebruik het in de style van de buttons:
	//Content="{Binding RelativeSource={RelativeSource TemplatedParent}, Path=(local:clsPortalButtonExtender.MainIcon)}"
	//En dankzij dit kan ik dan eenvoudig het juiste pictogram toewijzen: local:clsPortalButtonExtender.MainIcon="{StaticResource Engineer}"

	// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	// Te verwijderen, ik heb custom control gemaakt in apart project
		public static class clsPortalButtonExtender
	{

		public static readonly DependencyProperty MainIconProperty = DependencyProperty.RegisterAttached("MainIcon", typeof(UIElement),
			typeof(clsPortalButtonExtender), new PropertyMetadata(null));

		public static void SetMainIcon(UIElement element, UIElement value)
		{
			element.SetValue(MainIconProperty, value);
		}

		public static readonly DependencyProperty SmallIconProperty = DependencyProperty.RegisterAttached("SmallIcon", typeof(UIElement),
			typeof(clsPortalButtonExtender), new PropertyMetadata(null));

		public static void SetSmallIcon(UIElement element, UIElement value)
		{
			element.SetValue(SmallIconProperty, value);
		}

	}
}
