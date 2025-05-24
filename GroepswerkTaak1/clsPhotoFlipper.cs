
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using GroepswerkTaak1.DAL;
using GroepswerkTaak1.Model;

namespace GroepswerkTaak1
{
	public class clsPhotoFlipper : INotifyPropertyChanged
	{
		public Image ImageWPFObject { get; set; }
		public ScaleTransform ImageObjectScaleTransform { get; set; }

		public DispatcherTimer timer = new();

		clsImagePhotoFlipperRepo repo = new clsImagePhotoFlipperRepo();

		private short _currentIndex = 0;

		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public event PropertyChangedEventHandler? PropertyChanged = delegate { };

		private clsImagePhotoFlipper? _ActiveImage;
		public clsImagePhotoFlipper? ActiveImage
		{
			get { return _ActiveImage; }
			set
			{
				if (_ActiveImage != value)
				{
					_ActiveImage = value;
					OnPropertyChanged(nameof(ActiveImage));
				}
			}
		}

		public clsPhotoFlipper(Image image, ScaleTransform scaleTransform)
		{
			ImageObjectScaleTransform = scaleTransform;
			ImageWPFObject = image;					
		}

		public async Task InitializeAsync()
		{
			ActiveImage = await Task.Run(() => repo.GetFirst());
			StartTimer();
		}

		public void StartTimer()
		{

			timer = new DispatcherTimer
			{
				Interval = TimeSpan.FromSeconds(5)
			};
			timer.Tick += (s, e) => ShowNextImage();
			timer.Start();
		}

		private void ShowNextImage()
		{
			_currentIndex++;
			var imageCollection = repo.GetAll();

			if (_currentIndex >= imageCollection.Count)
			{
				_currentIndex = 0;
			}

			var flipOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(175));
			var flipIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(175));


			flipOut.Completed += (s, e) =>
			{
				ActiveImage = imageCollection[_currentIndex];
				ImageObjectScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, flipIn);
			};
			ImageObjectScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, flipOut);
			
		}
	}
}
