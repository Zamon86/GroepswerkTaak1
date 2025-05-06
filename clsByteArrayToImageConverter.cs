using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GroepswerkTaak1
{
	public class clsByteArrayToImageConverter : IValueConverter
	{

		public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{

			byte[]? bytes = value as byte[];

			if (bytes == null)
			{
				return null;
			}

			return ByteArrayToImageSource(bytes);

		}



		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{

			MessageBox.Show("Not implemented!");
			throw new NotImplementedException();

		}

		private ImageSource ByteArrayToImageSource(byte[] bytes)
		{

			BitmapImage image = new BitmapImage();

			using (MemoryStream ms = new MemoryStream(bytes))
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