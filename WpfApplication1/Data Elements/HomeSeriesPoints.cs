using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class HomeSeriesPoints : IEquatable<HomeSeriesPoints>
    {
        public HomeSeriesPoints(double mV, double hV)
        {
            this.minutesValue = mV;
            this.homeValue = hV;
        }
        public double minutesValue { get; set; }
        public double homeValue { get; set; }

        public bool Equals(HomeSeriesPoints homepoint) { return false; }
    }
}
