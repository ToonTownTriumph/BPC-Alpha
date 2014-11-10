using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class HomeSeriesCollection : ObservableCollection<HomeSeriesPoints>
    {
        //public List<HomeSeriesPoints> HomeSeriesList;
        //public bool IsRo = false;
        //public IEnumerator<HomeSeriesPoints> GetEnumerator()
        //{
            //return new HomeSeriesEnumerator(this);
        //}
        //IEnumerator IEnumerable.GetEnumerator()
        //{
            //return new HomeSeriesEnumerator(this);
        //}

        public HomeSeriesCollection()
        {
            //Add(new HomeSeriesPoints())
        }
        //public HomeSeriesPoints this[int index]
        //{
            //get { return (HomeSeriesPoints)HomeSeriesList[index]; }
            //set { HomeSeriesList[index] = value; }
        //}
        //public bool Contains(HomeSeriesPoints homepoint)
        //{
     //       bool found = false;
     //       foreach(HomeSeriesPoints hp in HomeSeriesList)
     //       {
     //           if(hp.Equals(homepoint))
     //           {
     //               found = true;
     //           }
     //       }
     //       return found;
     //   }
     //   public void Add(HomeSeriesPoints homepoint)
     //   {
      //      if(!Contains(homepoint))
      //      {
      //          HomeSeriesList.Add(homepoint);
     //       }
      //  }
      //  public void Clear()
      //  {
     //       HomeSeriesList.Clear();
     //   }
      //  public void CopyTo(HomeSeriesPoints[] homearray, int homearrayIndex)
      //  {
      //      for (int i = 0; i < HomeSeriesList.Count; i++)
       //     {
     //         homearray[i] = (HomeSeriesPoints)HomeSeriesList[i];
       //     }
      //  }
      //  public int Count
       // {
       //     get
       //     {
       //         return HomeSeriesList.Count;
       //     }
       // }
       // public bool IsReadOnly
       // {
        //    get
        //    {
        //        return IsRo;
         //   }
       // }
        //public bool Remove(HomeSeriesPoints homepoint)
        //{
          //  bool result = false;
           // for (int i = 0; i < HomeSeriesList.Count; i++)
           // {
             //   HomeSeriesPoints hp = (HomeSeriesPoints)HomeSeriesList[i];
            //}
            //return result;
        //}
    }
}
