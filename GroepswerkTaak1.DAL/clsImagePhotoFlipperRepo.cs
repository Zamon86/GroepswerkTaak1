﻿using GroepswerkTaak1.Model;
using System.Collections.ObjectModel;


namespace GroepswerkTaak1.DAL
{
	public class clsImagePhotoFlipperRepo
	{
		private readonly ObservableCollection<clsImagePhotoFlipper> _images = [];
		private bool _isDataModified = true;
		private int _queryResult;
		

		public void UpdateCollection()
		{
			_images.Clear();
			
			using (var reader = clsDAL.GetData(Properties.Resources.S_Images))
			{
				while (reader.Read())
				{
					var image = new clsImagePhotoFlipper()
					{
						ImagePhotoFlipperID = (short)reader["ID"],
						ThumbnailBytes = (byte[])reader["Image"],
						FullImageId = (short)reader["FullImageId"],
						ControlField = reader["ControlFIeld"]
					};
					
					_images.Add(image);
				}
			}

			if (_images.Count == 0)
			{
				var imagePhotoFlipper = GetErrorImage();
				_images.Add(imagePhotoFlipper);
			}
			_isDataModified = false;
		}
		public ObservableCollection<clsImagePhotoFlipper> GetAll()
		{
			if (_isDataModified)
			{
				UpdateCollection();
			}
			
			if (_images.Count == 0)
			{
				_images.Add(GetErrorImage());
			}
			return _images;
		}

		public bool Delete(clsImagePhotoFlipper entity)
		{
			clsDAL.ExecuteDataTable(Properties.Resources.D_Image, ref _queryResult, clsDAL.Parameter("ID", entity.ImagePhotoFlipperID),
				clsDAL.Parameter("User", $"{clsActiveUserData.ActiveUser.VoorNaam} {clsActiveUserData.ActiveUser.Naam}"),
				clsDAL.Parameter("ControlField", entity.ControlField),
				clsDAL.Parameter("@ReturnValue", 0));

			_isDataModified = true;

			return Helpers.clsSqlReturnValueHandler.HandleSqlReturnValue(_queryResult, entity);
		}
	
		public clsImagePhotoFlipper GetById(short id)
		{
			if (!_isDataModified) 
				return _images.FirstOrDefault(e => e.ImagePhotoFlipperID == id) ?? GetErrorImage();
			
			UpdateCollection();
			
			return _images.FirstOrDefault(e => e.ImagePhotoFlipperID == id) ?? GetErrorImage();
		}

		public clsImagePhotoFlipper GetFirst()
		{
			if (!_isDataModified)
				return _images.FirstOrDefault() ?? GetErrorImage();
			
			UpdateCollection();

			return _images.FirstOrDefault() ?? GetErrorImage();
		}

		public bool Insert(clsImagePhotoFlipper entity)
		{
			if (entity.FullImageBytes == null) return false;
			
			clsDAL.ExecuteDataTable(Properties.Resources.I_Image, ref _queryResult, clsDAL.Parameter("Image", entity.FullImageBytes),
				clsDAL.Parameter("Thumbnail", entity.ThumbnailBytes),
				clsDAL.Parameter("User", $"{clsActiveUserData.ActiveUser.VoorNaam} {clsActiveUserData.ActiveUser.Naam}"),
				clsDAL.Parameter("@ReturnValue", 0));

			_isDataModified = true;

			return Helpers.clsSqlReturnValueHandler.HandleSqlReturnValue(_queryResult, entity);
		}

		public bool Update(clsImagePhotoFlipper entity)
		{
			if (entity.FullImageBytes == null) return false;
			
			clsDAL.ExecuteDataTable(Properties.Resources.U_Image, ref _queryResult, clsDAL.Parameter("ID", entity.ImagePhotoFlipperID),
				clsDAL.Parameter("Image", entity.FullImageBytes),
				clsDAL.Parameter("FullImageId", entity.FullImageId),
				clsDAL.Parameter("Thumbnail", entity.ThumbnailBytes),
				clsDAL.Parameter("User", $"{clsActiveUserData.ActiveUser.VoorNaam} {clsActiveUserData.ActiveUser.Naam}"),
				clsDAL.Parameter("ControlField", entity.ControlField),
				clsDAL.Parameter("@ReturnValue", 0));

			_isDataModified = true;

			return Helpers.clsSqlReturnValueHandler.HandleSqlReturnValue(_queryResult, entity);
		}

		private clsImagePhotoFlipper GetErrorImage()
		{
			var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "something-went-wrong.jpg");

			var image = new clsImagePhotoFlipper()
			{
				ImagePhotoFlipperID = 0,
				ThumbnailBytes = File.ReadAllBytes(path),
				FullImageId = 0,
				ControlField = new object()
			};

			return image;
		}

		public bool LoadImageFull(clsImagePhotoFlipper entity)
		{
			var dt = clsDAL.ExecuteDataTable(Properties.Resources.S_ImageFull, ref _queryResult, clsDAL.Parameter("ImageId", entity.FullImageId),
				clsDAL.Parameter("@ReturnValue", 0));
			
			var result= Helpers.clsSqlReturnValueHandler.HandleSqlReturnValue(_queryResult, entity);

			if (result && dt.Rows.Count > 0)
			{
				entity.FullImageBytes = (byte[])dt.Rows[0]["Image"];
			}
			
			return result;
		}
	}
}
