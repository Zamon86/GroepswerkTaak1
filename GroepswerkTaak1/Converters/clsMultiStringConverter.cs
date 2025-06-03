using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace GroepswerkTaak1.Converters;

public class clsMultiStringConverter: IMultiValueConverter
{
	public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
	{
		var sb = new StringBuilder();
		
		foreach (var text in values)
		{
			if (text is string t)
			{
				sb.Append(t.Trim() + " - ");
			}
		}
		
		return sb.Length >= 3 ? sb.ToString(0, sb.Length - 3) : string.Empty;
	}

	public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}