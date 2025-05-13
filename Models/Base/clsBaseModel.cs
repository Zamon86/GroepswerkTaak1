using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroepswerkTaak1.Models.Base
{
	public class clsBaseModel
	{		public string? ErrorMessage { get; set; }

		private List<string> ErrorList = new List<string>();
		public string? Error
		{
			get
			{
				if (ErrorList.Count > 0)
				{
					return "NOK";
				}
				else
				{
					return null;
				}
			}
		}
	}
}
