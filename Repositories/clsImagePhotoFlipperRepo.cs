using GroepswerkTaak1.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GroepswerkTaak1.Repositories
{
	public class clsImagePhotoFlipperRepo
	{
		private ObservableCollection<clsImagePhotoFlipper> images = new ObservableCollection<clsImagePhotoFlipper>();
		int queryResult = 0;

		private void UpdateCollection()
		{
			using (SqlDataReader reader = clsDAL.GetData(Properties.Resources.S_Images))
			{
				while (reader.Read())
				{
					clsImagePhotoFlipper image = new clsImagePhotoFlipper()
					{
						ImagePhotoFlipperID = (int)reader["ID"],
						ImageBytes = (byte[])reader["Image"],
						ControlField = reader["ControlFIeld"]
					};
					images.Add(image);
				}
			}

			if (images.Count == 0)
			{
				clsImagePhotoFlipper image = GetErrorImage();
				images.Add(image);
			}
		}
		public ObservableCollection<clsImagePhotoFlipper> GetAll()
		{
			UpdateCollection();
			if (images.Count == 0)
			{
				images.Add(GetErrorImage());
			}
			return images;
		}

		public bool Delete(clsImagePhotoFlipper entity)
		{
			clsDAL.ExecuteDataTable(Properties.Resources.S_Images, ref queryResult, clsDAL.Parameter("ID", entity.ImagePhotoFlipperID),
				clsDAL.Parameter("User", Environment.UserName),
				clsDAL.Parameter("ControlField", entity.ControlField),
				clsDAL.Parameter("@ReturnValue", 0));
			return Helpers.SqlReturnValueHandler.HandleSqlReturnValue(queryResult, entity);
		}

		public clsImagePhotoFlipper GetById(int id)
		{
			UpdateCollection();
			return images.Where(e => e.ImagePhotoFlipperID == id).FirstOrDefault() ?? GetErrorImage();
		}

		public clsImagePhotoFlipper GetFirst()
		{
			UpdateCollection();
			return images.FirstOrDefault() ?? GetErrorImage();
		}

		public bool Insert(clsImagePhotoFlipper entity)
		{
			clsDAL.ExecuteDataTable(Properties.Resources.I_Image, ref queryResult, clsDAL.Parameter("Image", entity.ImageBytes),
				clsDAL.Parameter("User", Environment.UserName),
				clsDAL.Parameter("@ReturnValue", 0));
			return Helpers.SqlReturnValueHandler.HandleSqlReturnValue(queryResult, entity);
		}

		public bool Update (clsImagePhotoFlipper entity)
		{
			clsDAL.ExecuteDataTable(Properties.Resources.U_Image, ref queryResult, clsDAL.Parameter("ID", entity.ImagePhotoFlipperID),
				clsDAL.Parameter("Image", entity.ImageBytes),
				clsDAL.Parameter("User", Environment.UserName),
				clsDAL.Parameter("ControlField", entity.ControlField),
				clsDAL.Parameter("@ReturnValue", 0));
			return Helpers.SqlReturnValueHandler.HandleSqlReturnValue(queryResult, entity);
		}

		private clsImagePhotoFlipper GetErrorImage()
		{
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "something-went-wrong.jpg");

			clsImagePhotoFlipper image = new clsImagePhotoFlipper()
			{
				ImagePhotoFlipperID = 0,
				ImageBytes = File.ReadAllBytes(path),
				ControlField = new object()
			};

			return image;
		}
	}
}
