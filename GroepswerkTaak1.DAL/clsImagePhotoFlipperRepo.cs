using GroepswerkTaak1.Model;
using System.Collections.ObjectModel;


namespace GroepswerkTaak1.DAL
{
	public class clsImagePhotoFlipperRepo
	{
		private readonly ObservableCollection<clsImagePhotoFlipper> _images = [];
		private bool _isDataModified = true;
		private int _queryResult = 0;

		private void UpdateCollection()
		{
			_images.Clear();
			
			using (var reader = clsDAL.GetData(Properties.Resources.S_Images))
			{
				while (reader.Read())
				{
					var image = new clsImagePhotoFlipper()
					{
						ImagePhotoFlipperID = (short)reader["ID"],
						ImageBytes = (byte[])reader["Image"],
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
				_isDataModified= false;
			}
			
			if (_images.Count == 0)
			{
				_images.Add(GetErrorImage());
			}
			return _images;
		}

		public bool Delete(clsImagePhotoFlipper entity)
		{
			clsDAL.ExecuteDataTable(Properties.Resources.S_Images, ref _queryResult, clsDAL.Parameter("ID", entity.ImagePhotoFlipperID),
				clsDAL.Parameter("User", Environment.UserName),
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
			_isDataModified = false;
			
			return _images.FirstOrDefault(e => e.ImagePhotoFlipperID == id) ?? GetErrorImage();
		}

		public clsImagePhotoFlipper GetFirst()
		{
			if (!_isDataModified)
				return _images.FirstOrDefault() ?? GetErrorImage();
			
			UpdateCollection();
			_isDataModified = false;

			return _images.FirstOrDefault() ?? GetErrorImage();
		}

		public bool Insert(clsImagePhotoFlipper entity)
		{
			clsDAL.ExecuteDataTable(Properties.Resources.I_Image, ref _queryResult, clsDAL.Parameter("Image", entity.ImageBytes),
				clsDAL.Parameter("User", Environment.UserName),
				clsDAL.Parameter("@ReturnValue", 0));

			_isDataModified = true;

			return Helpers.clsSqlReturnValueHandler.HandleSqlReturnValue(_queryResult, entity);
		}

		public bool Update (clsImagePhotoFlipper entity)
		{
			clsDAL.ExecuteDataTable(Properties.Resources.U_Image, ref _queryResult, clsDAL.Parameter("ID", entity.ImagePhotoFlipperID),
				clsDAL.Parameter("Image", entity.ImageBytes),
				clsDAL.Parameter("User", Environment.UserName),
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
				ImageBytes = File.ReadAllBytes(path),
				ControlField = new object()
			};

			return image;
		}
	}
}
