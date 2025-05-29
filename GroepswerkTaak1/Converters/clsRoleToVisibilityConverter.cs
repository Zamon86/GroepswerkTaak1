using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GroepswerkTaak1.Converters;

public class clsRoleToVisibilityConverter: IValueConverter
{
	private const int RoleIdRequired = 2;

	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value is not int roleID) return Visibility.Hidden;
		return roleID == RoleIdRequired ?  Visibility.Visible : Visibility.Hidden;
		
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}