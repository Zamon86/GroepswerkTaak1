using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GroepswerkTaak1.ToBeDeleted
{
	
	//Te verwijderen, zo'n converter is niet nodig want Image can gewoon byte array lezen
	public class clsByteArrayToImageConverter : IValueConverter
	{

		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			return value is not byte[] bytes ? null : ByteArrayToImageSource(bytes);
		}


		public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			MessageBox.Show("Not implemented!");
			throw new NotImplementedException();
		}

		private ImageSource ByteArrayToImageSource(byte[] bytes)
		{

			var image = new BitmapImage();

			using (var ms = new MemoryStream(bytes))
			{
				image.BeginInit();
				image.CacheOption = BitmapCacheOption.OnLoad;
				image.StreamSource = ms;
				image.EndInit();
			}
			image.Freeze();
			return image;
		}
	}
}