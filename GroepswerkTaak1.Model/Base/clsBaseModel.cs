namespace GroepswerkTaak1.Model.Base
{
	public class clsBaseModel
	
	//We kunnen error handling implementeren, maar het is niet verplicht.
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
