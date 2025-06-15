using Common;


namespace GroepswerkTaak1.Model
{
	public class clsRolM : clsCommonModelPropertiesBase  // later toevoegen , IDataErrorInfo
	{
		// Constructor
		public clsRolM()
		{
			RolId = 0;
			_rolNaam = string.Empty;
			MyVisibility = 0;
			ControlField = new object();
		}

		public clsRolM(int myRolId, string myRolNaam)
		{
			RolId = myRolId;
			_rolNaam = myRolNaam;
		}

		private int _rolId;
		public int RolId
		{
			get => _rolId;
			set
			{
				if (value == _rolId) return;
				_rolId = value;
				OnPropertyChanged();
			}
		}

		private string _rolNaam;
		public string RolNaam
		{
			get => _rolNaam;
			set
			{
				if (value == _rolNaam) return;
				_rolNaam = value;
				OnPropertyChanged();
			}
		}


		public override string ToString()
		{
			return RolNaam;
		}
	}
}
