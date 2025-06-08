using System;
using System.Collections.Generic;
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

namespace GroepswerkTaak1.CustomControls
{
	
	public class clsLoadingSpinner : Control
	{
		public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
			nameof(Color), typeof(Brush), typeof(clsLoadingSpinner), new PropertyMetadata(Brushes.Black));

		public Brush Color
		{
			get => (Brush)GetValue(ColorProperty);
			set => SetValue(ColorProperty, value);
		}
		
		
		public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(
			nameof(Thickness), typeof(double), typeof(clsLoadingSpinner), new PropertyMetadata(100.0));

		public double Thickness
		{
			get => (double)GetValue(ThicknessProperty);
			set => SetValue(ThicknessProperty, value);
		}
		
		
		public static readonly DependencyProperty DiameterProperty = DependencyProperty.Register(
			nameof(Diameter), typeof(double), typeof(clsLoadingSpinner), new PropertyMetadata(1.0));

		public double Diameter
		{
			get => (double)GetValue(DiameterProperty);
			set => SetValue(DiameterProperty, value);
		}
		
		
		static clsLoadingSpinner()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(clsLoadingSpinner), new FrameworkPropertyMetadata(typeof(clsLoadingSpinner)));
		}
	}
}
