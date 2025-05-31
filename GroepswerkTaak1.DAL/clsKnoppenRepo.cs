using GroepswerkTaak1.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroepswerkTaak1.DAL
{
	public class clsKnoppenRepo
	{
		bool isDataModified = true;
		public ObservableCollection<clsKnoppenM> MijnCollectie;
		int nr = 0;
		public clsKnoppenRepo()
		{
			MijnCollectie = new ObservableCollection<clsKnoppenM>();
		}
		private void UpdateCollection()
		{

			using (SqlDataReader? reader = clsDAL.GetData(Properties.Resources.S_Knop))
			{
				if (reader != null)
				{
					while (reader.Read())
					{
						clsKnoppenM image = new clsKnoppenM()
						{
							KnopId = (int)reader["ID"],
							KnopNaam = reader["naam"].ToString(),
							KnopTekst = reader["tekst"].ToString(),
							KnopImage = reader.IsDBNull(reader.GetOrdinal("knopImage"))
					? null
					: (byte[])reader["knopImage"],
							// knopImage = (byte[])reader["knopImage"],
							KnopPositie = (short)reader["positie"],
							ControlField = reader["ControlField"]
						};
						MijnCollectie.Add(image);
					}
				}
			}

			if (MijnCollectie.Count == 0)
			{
				clsKnoppenM image = GetErrorImage();
				MijnCollectie.Add(image);
			}
			isDataModified = false;
		}

		public clsKnoppenM GetByID(short id)
		{
			if (isDataModified)
			{
				UpdateCollection();
				isDataModified = false;
			}
			return MijnCollectie.Where(e => e.KnopId == id).FirstOrDefault() ?? GetErrorImage();
		}

		public clsKnoppenM GetFirst()
		{
			if (isDataModified)
			{
				UpdateCollection();
				isDataModified = false;
			}

			return MijnCollectie.FirstOrDefault() ?? GetErrorImage();
		}
		public ObservableCollection<clsKnoppenM> GetAll()
		{
			if (isDataModified)
			{
				UpdateCollection();
				isDataModified = false;
			}

			if (MijnCollectie.Count == 0)
			{
				MijnCollectie.Add(GetErrorImage());
			}
			return MijnCollectie;
		}


		private clsKnoppenM GetErrorImage()
		{
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Buttons", "something-went-wrong.jpg");

			clsKnoppenM image = new clsKnoppenM()
			{
				KnopId = 0,
				//	KnopImage = File.ReadAllBytes(path),
				ControlField = new object()
			};

			return image;
		}

		#region CodeVanDanny

		//public bool Delete(clsKnoppenM entity)
		//{
		//	(DataTable DT, bool OK, string Boodschap) =
		//					clsDAL.ExecuteDataTable(Properties.Resources.D_Knop,
		//					clsDAL.Parameter("ID", entity.KnopId),
		//					clsDAL.Parameter("User", Environment.UserName),
		//					clsDAL.Parameter("ControlField", entity.ControlField),
		//					clsDAL.Parameter("@ReturnValue", 0));
		//	if (!OK)
		//	{
		//		entity.ErrorBoodschap = Boodschap;
		//	}
		//	return OK;
		//}

		//// public clsKnoppenM Find() { throw new NotImplementedException(); }  // not implemented

		//public ObservableCollection<clsKnoppenM> GetAll()
		//{ GenerateCollection(); return MijnCollectie; }


		//private void GenerateCollection()
		//{
		//	SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_Knop);
		//	MijnCollectie = new ObservableCollection<clsKnoppenM>();
		//	while (MijnDataReader.Read())
		//	{
		//		clsKnoppenM Knop = new clsKnoppenM()
		//		{
		//			KnopId = (int)MijnDataReader["ID"],
		//			KnopNaam = MijnDataReader["Naam"].ToString(),
		//			KnopTekst = MijnDataReader["Tekst"].ToString(),
		//			KnopPositie = (short)MijnDataReader["Positie"],
		//			KnopImage = (byte[])MijnDataReader["knopImage"],
		//			ControlField = MijnDataReader["ControlField"]
		//		};
		//		MijnCollectie.Add(Knop);
		//	}
		//}

		//public clsKnoppenM GetByID(int id)
		//{
		//	if (MijnCollectie == null)
		//	{
		//		GenerateCollection();
		//	}
		//	return MijnCollectie.Where(x => x.KnopId == id).FirstOrDefault();
		//}

		public bool Insert(clsKnoppenM entity)
		{
			(DataTable DT, bool OK, string Boodschap) =
							clsDAL.ExecuteDataTable(Properties.Resources.I_Knop,
							clsDAL.Parameter("Naam", entity.KnopNaam),
							clsDAL.Parameter("Tekst", entity.KnopTekst),
							clsDAL.Parameter("Positie", entity.KnopPositie),
							clsDAL.Parameter("knopImage", entity.KnopImage),
							clsDAL.Parameter("@ReturnValue", 0));
			if (!OK)
			{
				entity.ErrorBoodschap = Boodschap;
			}
			return OK;
		}
		public bool Update(clsKnoppenM entity)
		{
			(DataTable DT, bool OK, string Boodschap) =
							clsDAL.ExecuteDataTable(Properties.Resources.I_Knop,
							clsDAL.Parameter("ID", entity.KnopId),
							clsDAL.Parameter("Naam", entity.KnopNaam),
							clsDAL.Parameter("Tekst", entity.KnopTekst),
							clsDAL.Parameter("Positie", entity.KnopPositie),
							clsDAL.Parameter("knopImage", entity.KnopImage),
							clsDAL.Parameter("ControlField", entity.ControlField),
							clsDAL.Parameter("@ReturnValue", 0));
			if (!OK)
			{
				entity.ErrorBoodschap = Boodschap;

				#endregion

			}
			return OK;
		}
	}
}