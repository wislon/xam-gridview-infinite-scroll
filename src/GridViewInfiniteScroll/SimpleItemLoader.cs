using System.Collections.Generic;
using System.Text;

namespace GridViewInfiniteScroll
{
  public class SimpleItemLoader
  {
    public List<SimpleItem> SimpleItems { get; set; }
    public bool CanLoadMoreItems { get; private set; }
    public bool IsBusy { get; set; }
    public int CurrentPageValue { get; set; }

    public SimpleItemLoader()
    {
      SimpleItems = new List<SimpleItem>();
    }

    public void LoadMoreItems(int itemsPerPage)
    {
      IsBusy = true;
      for (int i = CurrentPageValue; i < CurrentPageValue + itemsPerPage; i++)
      {
        SimpleItems.Add(new SimpleItem(){DisplayName = string.Format("This is item {0:0000}", i)});
      }
      // normally you'd check to see if the number of items returned is less than the number requested, i.e. you've run out, and then set this accordinly.
      CanLoadMoreItems = true;
      CurrentPageValue = SimpleItems.Count;
      IsBusy = false;
    }
  }
}