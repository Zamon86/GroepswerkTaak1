using GroepswerkTaak1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroepswerkTaak1.Helpers
{
	public static class SqlReturnValueHandler
	{
		public static bool HandleSqlReturnValue(int returnValue, clsBaseModel entity)
		{
			switch (returnValue)
			{
				case 999:
					return true;
				case 998:
					entity.ErrorMessage = "Concurrency issue";
					return false;
				case 997:
					entity.ErrorMessage = "Already exists";
					return false;
				case 996:
					entity.ErrorMessage = "Unexpected error";
					return false;
				default:
					entity.ErrorMessage = "A oeps issue";
					return false;
			}
		}
	}
}
