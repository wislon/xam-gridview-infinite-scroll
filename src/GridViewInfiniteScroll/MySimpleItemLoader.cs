using System.Collections.Generic;
using System.Text;

namespace GridViewInfiniteScroll
{
  public class MySimpleItemLoader
  {
    public List<MySimpleItem> MySimpleItems { get; private set; }
    public bool CanLoadMoreItems { get; private set; }
    public bool IsBusy { get; set; }
    public int CurrentPageValue { get; set; }

    public MySimpleItemLoader()
    {
      MySimpleItems = new List<MySimpleItem>();
    }

    public void LoadMoreItems(int itemsPerPage)
    {
      IsBusy = true;
      for (int i = CurrentPageValue; i < CurrentPageValue + itemsPerPage; i++)
      {
        MySimpleItems.Add(new MySimpleItem(){DisplayName = string.Format("This is item {0:0000}", i)});
      }
      // normally you'd check to see if the number of items returned is less than the number requested, i.e. you've run out, and then set this accordinly.
      CanLoadMoreItems = true;
      CurrentPageValue = MySimpleItems.Count;
      IsBusy = false;
    }
  }
}