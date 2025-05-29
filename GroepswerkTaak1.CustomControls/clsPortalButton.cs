using System.Windows;
using System.Windows.Controls;

namespace GroepswerkTaak1.CustomControls
{
	public class clsPortalButton : Button
	{
		static clsPortalButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(clsPortalButton), new FrameworkPropertyMetadata(typeof(clsPortalButton)));
		}

		public static readonly DependencyProperty MainIconProperty =
			DependencyProperty.Register(nameof(MainIcon), typeof(UIElement), typeof(clsPortalButton), new PropertyMetadata(null));

		public static readonly DependencyProperty SmallIconProperty =
			DependencyProperty.Register(nameof(SmallIcon), typeof(UIElement), typeof(clsPortalButton), new PropertyMetadata(null));

		public UIElement MainIcon
		{
			get => (UIElement)GetValue(MainIconProperty);
			set => SetValue(MainIconProperty, value);
		}

		public UIElement SmallIcon
		{
			get => (UIElement)GetValue(SmallIconProperty);
			set => SetValue(SmallIconProperty, value);
		}
	}
}