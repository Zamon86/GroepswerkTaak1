using System.Windows;
using System.Windows.Controls;

namespace GroepswerkTaak1
{
	public partial class App : Application
	{		
		private void btnCloseTab_Click(object sender, RoutedEventArgs e)
		{
			Button? button = sender as Button;
			if (button == null) return;

			TabItem? tabItem = button.TemplatedParent as TabItem;
			if (tabItem == null) return;

			TabControl tabControl = FindTabControl(tabItem);
			tabControl.Items.Remove(tabItem);
		}

		private TabControl FindTabControl(TabItem tabItem)
		{
			var parent = tabItem.Parent;

			while (true)
			{
				if (parent is TabControl)
				{
					return (TabControl)parent;
				}
				else
				{
					parent = ((Control)parent).Parent;
				}
			}

		}
	}
}
