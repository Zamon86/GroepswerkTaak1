﻿using GroepswerkTaak1.Model.Base;

namespace GroepswerkTaak1.DAL.Helpers
{
	public static class clsSqlReturnValueHandler
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
