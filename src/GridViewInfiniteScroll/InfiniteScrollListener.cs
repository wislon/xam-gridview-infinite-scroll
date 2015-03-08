using System;
using System.Linq;
using Android.App;
using Android.Support.V7.Widget;
using Android.Util;

namespace GridViewInfiniteScroll
{
  public class InfiniteScrollListener : RecyclerView.OnScrollListener
  {
    private readonly MySimpleItemLoader _mySimpleItemLoader;
    private readonly MyRecyclerAdapter _myRecyclerAdapter;
    private readonly StaggeredGridLayoutManager _staggeredGridLayoutManager;

    private readonly object scrollLockObject = new object();
    private readonly int _itemsPerPage;
    private readonly Action _moreItemsLoadedCallback;

    /// <summary>
    /// How many items away from the end of the list before we need to
    /// trigger a load of the next page of items
    /// </summary>
    private const int LoadNextItemsThreshold = 8;

    public InfiniteScrollListener(MySimpleItemLoader mySimpleItemLoader,
      MyRecyclerAdapter myRecyclerAdapter,
      StaggeredGridLayoutManager staggeredGridLayoutManager, 
      int itemsPerPage,
      Action moreItemsLoadedCallback)
    {
      _mySimpleItemLoader = mySimpleItemLoader;
      _myRecyclerAdapter = myRecyclerAdapter;
      _staggeredGridLayoutManager = staggeredGridLayoutManager;
      _itemsPerPage = itemsPerPage;
      _moreItemsLoadedCallback = moreItemsLoadedCallback;
    }

    public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
    {
      base.OnScrolled(recyclerView, dx, dy);

      var visibleItemCount = recyclerView.ChildCount;
      var totalItemCount = _myRecyclerAdapter.ItemCount;

      // size of array must be >= the number of items you may have in view at any one time. 
      // should be set to at least the same value as the 'span' parameter in 
      // StaggerGridLayoutManager ctor: i.e. 2 for phone in portrait, 3 for phone in landscape, 
      // assume more for a tablet, etc.
      var positions = new int[6] {-1, -1, -1, -1, -1, -1,};

      var lastVisibleItems = _staggeredGridLayoutManager.FindLastCompletelyVisibleItemPositions(positions);

      // remember you'll need to handle re-scrolling to last viewed item, if user flips between landscape/portrait.
      int currentPosition = lastVisibleItems.LastOrDefault(item => item > -1);

      if (currentPosition == 0) return;

      if (totalItemCount - currentPosition <= LoadNextItemsThreshold)
      {
        lock (scrollLockObject)
        {
          if (_mySimpleItemLoader.CanLoadMoreItems && !_mySimpleItemLoader.IsBusy)
          {
            _mySimpleItemLoader.IsBusy = true;
            Log.Info("InfiniteScrollListener", "Load more items requested");
            _mySimpleItemLoader.LoadMoreItems(_itemsPerPage);
            if (_moreItemsLoadedCallback != null)
            {
              _moreItemsLoadedCallback();
            }
          }
        }
      }
    }


  }
}
