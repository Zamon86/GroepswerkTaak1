using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace GroepswerkTaak1.CustomControls.Converters;

public class clsCalculationStrokeDashArrayConverter: IMultiValueConverter
{
	public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
	{
		if (values.Length < 2 || !double.TryParse(values[0].ToString(), out var diameter) || !double.TryParse(values[1].ToString(), out var thickness))
		{
			return 0;
		}

		var circ = Math.PI * diameter;
		var line = circ * 0.75;
		var gap = circ - line;

		return new DoubleCollection([line/thickness, gap/thickness]);
	}

	public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}