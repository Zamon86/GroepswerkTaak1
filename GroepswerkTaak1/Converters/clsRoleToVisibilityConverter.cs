using System.Globalization;
using System.Windows;
using System.Windows.Data;
using GroepswerkTaak1.Model;

namespace GroepswerkTaak1.Converters;

public class clsRoleToVisibilityConverter: IValueConverter
{
	private const string Admin = "Admin";

	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value is not clsRolM userRole) return Visibility.Hidden;
		
		return userRole.RolNaam.Trim() == Admin ? Visibility.Visible : Visibility.Hidden;
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}