using GroepswerkTaak1.Models.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroepswerkTaak1.Models
{
	public class clsImagePhotoFlipper: clsBaseModel
	{		
		public short ImagePhotoFlipperID { get; set; }
		public required byte[] ImageBytes {  get; set; }
		public required object ControlField { get; set; }	

	}
}
