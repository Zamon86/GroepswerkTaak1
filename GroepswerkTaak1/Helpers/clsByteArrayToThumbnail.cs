using System.IO;
using System.Security.Cryptography.Xml;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Data.SqlClient.DataClassification;

namespace GroepswerkTaak1.Helpers;

public static class clsByteArrayToThumbnail
{
	public static byte[] CreateThumbnail(byte[] sourceImage, int maxWidth, int maxHeight)
	{
		if (sourceImage == null || sourceImage.Length == 0)
			throw new ArgumentException(@"No data!", nameof(sourceImage));
		
		var image = new BitmapImage();

		using (var ms = new MemoryStream(sourceImage))
		{
			image.BeginInit();
			image.CacheOption = BitmapCacheOption.OnLoad;
			image.StreamSource = ms;
			image.EndInit();
		}
		image.Freeze();

		var scaleX = (double)maxWidth / image.PixelWidth;
		var scaleY = (double)maxHeight / image.PixelHeight;
		var scale = Math.Min(scaleX, scaleY);

		if (scale >= 1.0)
		{
			return sourceImage;
		}

		var transformation = new ScaleTransform(scale, scale);
		var thumbnail = new TransformedBitmap(image, transformation);
		thumbnail.Freeze();

		var encoder = new JpegBitmapEncoder();
		encoder.Frames.Add(BitmapFrame.Create(thumbnail));
		
		using var outStream = new MemoryStream();
		encoder.Save(outStream);
		return outStream.ToArray();
	}
	
}