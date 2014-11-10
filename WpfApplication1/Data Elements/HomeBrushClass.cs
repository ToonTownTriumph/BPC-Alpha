using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class HomeBrushClass : INotifyPropertyChanged
    {
        Color _HomeBrush;
        
        public Color HomeBrush
        {
            get { return  _HomeBrush;}
            set
            {
                _HomeBrush = value;
                OnPropertyChanged("HomeBrush");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string Property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Property));
            }
        }
    }
}
