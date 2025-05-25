using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

		public static readonly DependencyProperty IsAdminButtonProperty =
			DependencyProperty.Register(nameof(IsAdminButton), typeof(bool), typeof(clsPortalButton), new PropertyMetadata(false));

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

		public bool IsAdminButton
		{
			get => (bool)GetValue(IsAdminButtonProperty);
			set => SetValue(IsAdminButtonProperty, value);
		}
	}
}