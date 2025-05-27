using GroepswerkTaak1.Model.Base;


namespace GroepswerkTaak1.Model
{
	public class clsImagePhotoFlipper: clsBaseModel
	{		
		public short ImagePhotoFlipperID { get; set; }
		public required byte[] ImageBytes {  get; set; }
		public required object ControlField { get; set; }	

	}
}
