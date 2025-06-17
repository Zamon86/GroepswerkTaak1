	using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroepswerkTaak1.Model
{
		public class clsLoggingM : INotifyPropertyChanged
		{
				public event PropertyChangedEventHandler? PropertyChanged;

				private void OnPropertyChanged(string propertyName)
				{
						PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
				}

				// Constructor
				public clsLoggingM()
				{
				}

				public clsLoggingM(int MyLogId, string MyAktie, string MyTabelColumn, string MyPreValue, string MyPostValue, string MyGebruiker)
				{
						_LogId = MyLogId;
						_Aktie = MyAktie;
						_TabelColumn = MyTabelColumn;
						_PreValue = MyPreValue;
						_PostValue = MyPostValue;
						_Gebruiker = MyGebruiker;

				}

				private int _LogId;

				public int LogId
				{
						get { return _LogId; }
						set { _LogId = value; }
				}

				private string _Aktie;

				public string Aktie
				{
						get { return _Aktie; }
						set { _Aktie = value; }
				}


				private string _TabelColumn;

				public string TabelColumn
				{
						get { return _TabelColumn; }
						set { _TabelColumn = value; }
				}

				private string _PreValue;

				public string PreValue
				{
						get { return _PreValue; }
						set { _PreValue = value; }
				}

				private string _PostValue;

				public string PostValue
				{
						get { return _PostValue; }
						set { _PostValue = value; }
				}

				private string _Gebruiker;

				public string Gebruiker
				{
						get { return _Gebruiker; }
						set { _Gebruiker = value; }
				}


				private DateTime _Registratie;

				public DateTime Registratie
				{
						get { return _Registratie; }
						set { _Registratie = value; }
				}


				private Object _ControlField;
				public Object ControlField
				{
						get { return _ControlField; }
						set { _ControlField = value; }
				}

		}
}
