using System;
using System.Windows.Media;
using Caliburn.Micro;
using ImageProcessing.Core;
using ImageProcessing.Core.Filters;
using ImageProcessing.Wpf;
using Microsoft.Win32;

namespace TestBed.ViewModels
{
    public class ShellViewModel : PropertyChangedBase
    {
        private readonly IWindowManager _windowManager;
        private IFrame<int> _grey;
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

            if (result == true) LoadFile(openFileDialog.FileName);
        }

        private void LoadFile(string fileName) {
            _grey = ImgProc.ImageFromFile(fileName)
                           .ToGreyScale();


            ShowFrame(_grey);
        }

        private void ShowFrame(IFrame<int> frame) {
            Image = frame
                .Range(0, 255)
                .ToArgb()
                .ToImageSource();
        }

        public void QuickOpen() {
            LoadFile(@"C:\temp\Penguins-small.jpg");
        }

        public void QuickEdges() {
            var horizontal = _grey
                .Filter(new ConvolutingFilter(new[] {new[] {-1, 0, 1}, new[] {-2, 0, 2}, new[] {-1, 0, 1}}));

            var vertical = _grey
                .Filter(new ConvolutingFilter(new[] {new[] {-1, -2, -1}, new[] {0, 0, 0}, new[] {1, 2, 1}}));

            var average = horizontal.Zip(vertical, (h, v) => (Math.Abs(h) + Math.Abs(v))/2);

            ShowFrame(average);
        }
    }
}