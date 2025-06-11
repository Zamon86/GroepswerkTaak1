using GroepswerkTaak1.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroepswerkTaak1;

namespace GroepswerkTaak1.DAL
{
	public class clsRollenRepo
	{
		private bool _isDataModified = true;
		private readonly ObservableCollection<clsRolM> _rollen = [];

		public bool Delete(clsRolM entity, string loginNaam)
		{

			(DataTable dt, bool ok, string boodschap) =
							clsDAL.ExecuteDataTable(Properties.Resources.D_Rol,
							clsDAL.Parameter("RolID", entity.RolId),
							///TODO: vervangen door de loginnaam
							///
							clsDAL.Parameter("UserNaam", loginNaam),
							clsDAL.Parameter("ControlField", entity.ControlField),
							clsDAL.Parameter("@ReturnValue", 0));
			if (!ok)
			{
				entity.ErrorBoodschap = boodschap;
			}
			else
			{
				_isDataModified = true;
			}
			return ok;
		}

		// public clsRollenM Find() { throw new NotImplementedException(); }  // not implemented
		public ObservableCollection<clsRolM> GetAll()
		{
			if (_isDataModified)
			{
				UpdateCollection();
			}
			
			return _rollen;
		}


		private void UpdateCollection()
		{
			SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_Rol);

			while (MijnDataReader.Read())
			{
				clsRolM Roll = new clsRolM()
				{
					RolId = (int)MijnDataReader["ID"],
					RolNaam = MijnDataReader["rol"].ToString(),
					ControlField = MijnDataReader["ControlField"]
				};
				_rollen.Add(Roll);
			}

			_isDataModified = false;
		}

		public clsRolM GetByID(int id)
		{
			if (_isDataModified)
			{
				UpdateCollection();
			}
			var role = _rollen.FirstOrDefault(x => x.RolId == id);
			
			return role ?? new clsRolM();
		}

		public clsRolM GetFirst()
		{
			if (_isDataModified)
			{
				UpdateCollection();
			}

			var role = _rollen.FirstOrDefault();

			return role ?? new clsRolM();
		}

		public bool Insert(clsRolM entity, string LoginNaam)
		{
			(DataTable DT, bool OK, string Boodschap) =
							clsDAL.ExecuteDataTable(Properties.Resources.I_Rol,
							clsDAL.Parameter("RolNaam", entity.RolNaam),
							clsDAL.Parameter("UserNaam", LoginNaam),
							clsDAL.Parameter("@ReturnValue", 0));
			if (!OK)
			{
				entity.ErrorBoodschap = Boodschap;
			}

			_isDataModified = true;
			return OK;

			
		}
		public bool Insert(clsRolM entity)
		{
			(DataTable DT, bool OK, string Boodschap) =
							clsDAL.ExecuteDataTable(Properties.Resources.I_Rol,
							clsDAL.Parameter("RolNaam", entity.RolNaam),
							clsDAL.Parameter("UserNaam", Environment.UserName),
							clsDAL.Parameter("@ReturnValue", 0));
			if (!OK)
			{
				entity.ErrorBoodschap = Boodschap;
			}
			_isDataModified = true;
			return OK;
		}

		public bool Update(clsRolM entity, string LoginNaam)
		{
			(DataTable DT, bool OK, string Boodschap) =
							clsDAL.ExecuteDataTable(Properties.Resources.U_Rol,
							clsDAL.Parameter("RolID", entity.RolId),
							clsDAL.Parameter("RolNaam", entity.RolNaam),
							clsDAL.Parameter("UserNaam", LoginNaam),
							clsDAL.Parameter("ControlField", entity.ControlField),
							clsDAL.Parameter("@ReturnValue", 0));
			if (!OK)
			{
				entity.ErrorBoodschap = Boodschap;
			}
			_isDataModified = true;
			return OK;

		}
		public bool Update(clsRolM entity)
		{
			(DataTable DT, bool OK, string Boodschap) =
							clsDAL.ExecuteDataTable(Properties.Resources.U_Rol,
							clsDAL.Parameter("RolID", entity.RolId),
							clsDAL.Parameter("RolNaam", entity.RolNaam),
							clsDAL.Parameter("UserNaam", Environment.UserName),
							clsDAL.Parameter("ControlField", entity.ControlField),
							clsDAL.Parameter("@ReturnValue", 0));
			if (!OK)
			{
				entity.ErrorBoodschap = Boodschap;
			}
			_isDataModified = true;
			return OK;

		}
	}
}
