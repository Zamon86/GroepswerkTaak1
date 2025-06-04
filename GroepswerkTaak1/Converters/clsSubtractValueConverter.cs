using System.Globalization;
using System.Windows.Data;

namespace GroepswerkTaak1.Converters;

public class clsSubtractValueConverter: IValueConverter
{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value is double original && double.TryParse(parameter?.ToString(), out var subtract))
			{
				return original - subtract;
			}
			return value;
		}

		public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

}