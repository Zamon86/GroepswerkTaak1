using GroepswerkTaak1.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroepswerkTaak1.DAL;


namespace GroepswerkTaak1.DAL
{
	public class clsImagePhotoFlipperRepo
	{
		private ObservableCollection<clsImagePhotoFlipper> images = new ObservableCollection<clsImagePhotoFlipper>();
		bool isDataModified = true;
		int queryResult = 0;

		private void UpdateCollection()
		{
			
			using (SqlDataReader? reader = clsDAL.GetData(Properties.Resources.S_Images))
			{
				if (reader != null) 
				{
					while (reader.Read())
					{
						clsImagePhotoFlipper image = new clsImagePhotoFlipper()
						{
							ImagePhotoFlipperID = (short)reader["ID"],
							ImageBytes = (byte[])reader["Image"],
							ControlField = reader["ControlFIeld"]
						};
						images.Add(image);
					}
				}				
			}

			if (images.Count == 0)
			{
				clsImagePhotoFlipper image = GetErrorImage();
				images.Add(image);
			}
			isDataModified = false;
		}
		public ObservableCollection<clsImagePhotoFlipper> GetAll()
		{
			if (isDataModified)
			{
				UpdateCollection();
				isDataModified= false;
			}
			
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

			isDataModified = true;

			return Helpers.clsSqlReturnValueHandler.HandleSqlReturnValue(queryResult, entity);
		}
	
		public clsImagePhotoFlipper GetById(short id)
		{
			if (isDataModified)
			{
				UpdateCollection();
				isDataModified = false;
			}
			return images.Where(e => e.ImagePhotoFlipperID == id).FirstOrDefault() ?? GetErrorImage();
		}

		public clsImagePhotoFlipper GetFirst()
		{
			if (isDataModified)
			{
				UpdateCollection();
				isDataModified = false;
			}

			return images.FirstOrDefault() ?? GetErrorImage();
		}

		public bool Insert(clsImagePhotoFlipper entity)
		{
			clsDAL.ExecuteDataTable(Properties.Resources.I_Image, ref queryResult, clsDAL.Parameter("Image", entity.ImageBytes),
				clsDAL.Parameter("User", Environment.UserName),
				clsDAL.Parameter("@ReturnValue", 0));

			isDataModified = true;

			return Helpers.clsSqlReturnValueHandler.HandleSqlReturnValue(queryResult, entity);
		}

		public bool Update (clsImagePhotoFlipper entity)
		{
			clsDAL.ExecuteDataTable(Properties.Resources.U_Image, ref queryResult, clsDAL.Parameter("ID", entity.ImagePhotoFlipperID),
				clsDAL.Parameter("Image", entity.ImageBytes),
				clsDAL.Parameter("User", Environment.UserName),
				clsDAL.Parameter("ControlField", entity.ControlField),
				clsDAL.Parameter("@ReturnValue", 0));

			isDataModified = true;

			return Helpers.clsSqlReturnValueHandler.HandleSqlReturnValue(queryResult, entity);
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
