using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GroepswerkTaak1{
	public class clsMenuItemData
	{
		[XmlAttribute("Header")]
		public required string Header { get; set; }

		[XmlAttribute("IconResourceName")]
		public string? IconResourceName { get; set; }

		[XmlElement("MenuItem")]
		public List<clsMenuItemData>? SubItems { get; set; }
	}
}
