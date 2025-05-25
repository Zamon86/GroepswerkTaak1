using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace GroepswerkTaak1.CustomControls;

public class clsCustomTabItem : TabItem
{
	static clsCustomTabItem()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(clsCustomTabItem),
			new FrameworkPropertyMetadata(typeof(clsCustomTabItem)));
	}
	
	public static readonly DependencyProperty IsCloseableProperty = DependencyProperty.Register(
		nameof(IsCloseable), typeof(bool), typeof(clsCustomTabItem), new PropertyMetadata(true));

	public static readonly DependencyProperty BackgroundHighlightedProperty = DependencyProperty.Register(
		nameof(BackgroundHighlighted), typeof(Brush), typeof(clsCustomTabItem), new PropertyMetadata(Brushes.Cyan));

	public Brush BackgroundHighlighted
	{
		get => (Brush)GetValue(BackgroundHighlightedProperty);
		set => SetValue(BackgroundHighlightedProperty, value);
	}

	public bool IsCloseable
	{
		get => (bool)GetValue(IsCloseableProperty);
		
		set => SetValue(IsCloseableProperty, value);
	}

	public override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		
		if (GetTemplateChild("btnCloseTab") is Button btn)
		{
			btn.Click += (s, e) =>
			{
				var tabControl = this.Parent as TabControl;
				tabControl?.Items.Remove(this);
			};
		}
	}
	
	
}