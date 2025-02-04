using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace GroepswerkTaak1
{
	[XmlRoot("Menu")]
	public class clsMenuData
	{
		[XmlElement("MenuItem")]
		public List<clsMenuItemData>? Items { get; set; }

		public string xmlFilePath = @"resources\MenuConfig.xml";

		public void ReadDataFromConfigFile() 
		{
			XmlSerializer serializer = new XmlSerializer(typeof(clsMenuData));
			
			using (FileStream fileStream = new FileStream(xmlFilePath, FileMode.Open))
			{
				var menuData = serializer.Deserialize(fileStream) as clsMenuData;
				Items = menuData?.Items;
			}
		}

		public List<MenuItem> CreateMenuItems() 
		{			
			foreach (var item in Items)
			{
				MenuItem menuItem = new MenuItem();
				menuItem.Header = item.Header;
				if (item.IconResourceName != null) 
				{

				}

			}


			return null;
		}
	}

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
