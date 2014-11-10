using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class HomeSeriesEnumerator : IEnumerator<HomeSeriesPoints>
    {
        private HomeSeriesCollection _homecollection;
        private int homeColIndex;
        private HomeSeriesPoints homecolPoint;

        public HomeSeriesEnumerator(HomeSeriesCollection homecollection)
        {
            _homecollection = homecollection;
            homeColIndex = -1;
            homecolPoint = default(HomeSeriesPoints);
        }
        public HomeSeriesPoints Current
        {
            get { return homecolPoint; }
        }
        object IEnumerator.Current
        {
            get { return Current; }
        }
        public bool MoveNext()
        {
            if(++homeColIndex >= _homecollection.Count)
            {
                return false;
            }
            else
            {
                homecolPoint = _homecollection[homeColIndex];
            }
            return true;
        }
        public void Reset() { homeColIndex = -1; }
        void IDisposable.Dispose() { }
    }
}
