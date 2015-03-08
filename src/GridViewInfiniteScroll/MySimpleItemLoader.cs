using System.Collections.Generic;
using System.Text;
using Java.Util;

namespace GridViewInfiniteScroll
{
  public class MySimpleItemLoader
  {
    public List<MySimpleItem> MySimpleItems { get; private set; }
    public bool CanLoadMoreItems { get; private set; }
    public bool IsBusy { get; set; }
    public int CurrentPageValue { get; set; }

    private readonly string[] _lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.".Split(' ');
    private readonly Random _rand;

    public MySimpleItemLoader()
    {
      MySimpleItems = new List<MySimpleItem>();
      _rand = new Random();
    }


    public void LoadMoreItems(int itemsPerPage)
    {
      IsBusy = true;
      for (int i = CurrentPageValue; i < CurrentPageValue + itemsPerPage; i++)
      {
        string randomLorem = string.Join(" ", _lorem, 0, _rand.NextInt(_lorem.Length - 1));
        MySimpleItems.Add(new MySimpleItem(){DisplayName = string.Format("This is item {0:0000} ({1})", i, randomLorem)});
      }
      // normally you'd check to see if the number of items returned is less than the number requested, i.e. you've run out, and then set this accordinly.
      CanLoadMoreItems = true;
      CurrentPageValue = MySimpleItems.Count;
      IsBusy = false;
    }
  }
}