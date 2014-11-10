using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApplication1
{
    class SliderImages : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Style _SliderImage;
        public Style SliderImage
        {
            get { return _SliderImage; }
            set
            {
                _SliderImage = value;
                OnPropertyChanged("SliderImage");
            }
        }

        public void SetColorGradient(LinearGradientBrush SliderLGBdata)
        {
            SliderLGB = SliderLGBdata;
        }
        LinearGradientBrush sliderLGB;
        public LinearGradientBrush SliderLGB
        {
            get { return sliderLGB; }
            set
            {
                sliderLGB = value;
                OnPropertyChanged("Background");
            }
        }
        public void SetHomeImageData(byte[] data) 
        {
            var source = new BitmapImage();
            source.BeginInit();
            source.StreamSource = new MemoryStream(data);
            source.EndInit();

            // use public setter
            HomeImageSource = source;
        }
        ImageSource homeimageSource;
        public ImageSource HomeImageSource
        {
            get { return homeimageSource; }
            set
            {
                homeimageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }
        public void SetAwayImageData(byte[] data) 
        {
            var source = new BitmapImage();
            source.BeginInit();
            source.StreamSource = new MemoryStream(data);
            source.EndInit();

            // use public setter
            AwayImageSource = source;
        }
        ImageSource awayimageSource;
        public ImageSource AwayImageSource
        {
            get { return awayimageSource; }
            set
            {
                awayimageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }
        SolidColorBrush _HomeBrush;
        public SolidColorBrush HomeBrush
        {
            get { return _HomeBrush; }
            set
            {
                _HomeBrush = value;
                OnPropertyChanged("HomeBrush");
            }
        }
        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
