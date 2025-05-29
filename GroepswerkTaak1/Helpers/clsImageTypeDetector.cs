namespace GroepswerkTaak1.Helpers;

public static class clsImageTypeDetector
{
	public static string? GetImageExtensionFromBytes(byte[] bytes)
	{
		if (bytes.Length < 4)
			return null;
		
		if (bytes[0] == 0xFF && bytes[1] == 0xD8)
			return ".jpg";
		
		if (bytes[0] == 0x89 && bytes[1] == 0x50 &&
		    bytes[2] == 0x4E && bytes[3] == 0x47)
			return ".png";
		
		if (bytes[0] == 0x42 && bytes[1] == 0x4D)
			return ".bmp";

		return null; 
	}
}