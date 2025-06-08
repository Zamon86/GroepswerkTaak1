using GroepswerkTaak1.Model.Base;


namespace GroepswerkTaak1.Model
{
	public class clsImagePhotoFlipper: clsBaseModel
	{		
		public short ImagePhotoFlipperID { get; set; }
		public required byte[] ThumbnailBytes { get; set; }
		public required short FullImageId { get; set; }
		public required object ControlField { get; set; }
		public byte[]? FullImageBytes { get; set; }

	}
}
