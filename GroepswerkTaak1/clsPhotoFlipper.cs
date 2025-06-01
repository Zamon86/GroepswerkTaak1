using System.ComponentModel;
using System.Runtime.CompilerServices;
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
		public Image ImageWpfObject { get; set; }
		public ScaleTransform ImageObjectScaleTransform { get; set; }

		public DispatcherTimer Timer = new();

		public clsImagePhotoFlipperRepo Repo = new();

		private short _currentIndex = 0;

		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public event PropertyChangedEventHandler? PropertyChanged = delegate { };

		private clsImagePhotoFlipper? _activeImage;
		public clsImagePhotoFlipper? ActiveImage
		{
			get => _activeImage;
			set
			{
				if (_activeImage == value) return;
				_activeImage = value;
				OnPropertyChanged();
			}
		}

		public clsPhotoFlipper(Image image, ScaleTransform scaleTransform)
		{
			ImageObjectScaleTransform = scaleTransform;
			ImageWpfObject = image;					
		}

		public async Task InitializeAsync()
		{
			ActiveImage = await Task.Run(() => Repo.GetFirst());
			StartTimer();
		}

		public void StartTimer()
		{

			Timer = new DispatcherTimer
			{
				Interval = TimeSpan.FromSeconds(5)
			};
			Timer.Tick += (s, e) => ShowNextImage();
			Timer.Start();
		}

		private void ShowNextImage()
		{
			_currentIndex++;
			var imageCollection = Repo.GetAll();

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
