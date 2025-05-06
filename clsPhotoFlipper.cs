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

namespace GroepswerkTaak1
{
	public class clsPhotoFlipper : INotifyPropertyChanged
	{
		public Image ImageObject { get; set; }
		public ScaleTransform ImageObjectScaleTransform { get; set; }

		private DispatcherTimer _timer = new();

		private int _currentIndex = 0;

		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public event PropertyChangedEventHandler? PropertyChanged = delegate { };

		public ObservableCollection<byte[]> imageFilesBytes { get; set; }


		private byte[] _ActiveImageBytes;
		public byte[] ActiveImageBytes
		{
			get { return _ActiveImageBytes; }
			set
			{
				if (_ActiveImageBytes != value)
				{
					_ActiveImageBytes = value;
					OnPropertyChanged(nameof(ActiveImageBytes));
				}
			}
		}

		public clsPhotoFlipper(Image image, ScaleTransform scaleTransform)
		{
			ImageObjectScaleTransform = scaleTransform;
			ImageObject = image;
			this.imageFilesBytes = App.StoreDB.GetBytesForImages();
			_ActiveImageBytes = imageFilesBytes[0];
			StartTimer();
		}

		public void StartTimer()
		{

			_timer = new DispatcherTimer
			{
				Interval = TimeSpan.FromSeconds(5)
			};
			_timer.Tick += (s, e) => ShowNextImage();
			_timer.Start();
		}

		private void ShowNextImage()
		{
			_currentIndex++;

			if (_currentIndex >= imageFilesBytes.Count)
			{
				_currentIndex = 0;
			}

			var flipOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(175));
			var flipIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(175));


			flipOut.Completed += (s, e) =>
			{
				ActiveImageBytes = imageFilesBytes[_currentIndex];
				ImageObjectScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, flipIn);
			};

			ImageObjectScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, flipOut);
		}
	}
}
