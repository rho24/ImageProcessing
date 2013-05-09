using System.Windows.Media;
using Caliburn.Micro;
using ImageProcessing.Core;
using ImageProcessing.Wpf;
using Microsoft.Win32;

namespace TestBed.ViewModels
{
    public class ShellViewModel : PropertyChangedBase
    {
        private readonly IWindowManager _windowManager;
        private ImageSource _image;

        public ImageSource Image {
            get { return _image; }
            set {
                if (Equals(value, _image)) return;
                _image = value;
                NotifyOfPropertyChange(() => Image);
            }
        }

        public ShellViewModel(IWindowManager windowManager) {
            _windowManager = windowManager;
        }

        public void OpenFile() {
            var openFileDialog = new OpenFileDialog();

            var result = openFileDialog.ShowDialog();

            if (result == true) Image = ImgProc.ImageFromFile(openFileDialog.FileName).Filter(new DelayingFilter(2)).ToImageSource();
        }
    }
}