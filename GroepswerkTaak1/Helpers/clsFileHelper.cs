using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace GroepswerkTaak1.Helpers;

public static class clsFileHelper
{
	public static void OpenBytesAsTempFile(byte[] fileBytes)
	{

		var tempPath = Path.GetTempPath();
		var extension = clsImageTypeDetector.GetImageExtensionFromBytes(fileBytes);
		var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");

		if (extension == null) return;

		var filePath = Path.Combine(tempPath, $"TempImage_{timestamp}{extension}");
		
		File.WriteAllBytes(filePath, fileBytes);
		
		Process.Start(new ProcessStartInfo
		{
			FileName = filePath,
			UseShellExecute = true
		});
	}
}