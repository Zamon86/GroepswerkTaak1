using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace GroepswerkTaak1
{
		public static class clsPortalButtonExtender
		{

		public static readonly DependencyProperty MainIconProperty = DependencyProperty.RegisterAttached("MainIcon", typeof(Path),
			typeof(clsPortalButtonExtender), new PropertyMetadata(null));

		public static void SetMainIcon(UIElement element, Path value)
		{
			element.SetValue(MainIconProperty, value);
		}

		public static readonly DependencyProperty SmallIconProperty = DependencyProperty.RegisterAttached("SmallIcon", typeof(Path),
			typeof(clsPortalButtonExtender), new PropertyMetadata(null));

		public static void SetSmallIcon(UIElement element, Path value)
		{
			element.SetValue(SmallIconProperty, value);
		}

	}
}
