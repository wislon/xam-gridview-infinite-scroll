using Android.App;
using Android.Util;
using Android.Widget;
using Android.OS;

namespace GridViewInfiniteScroll
{
  [Activity(Label = "GridViewInfiniteScroll", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {
    private const string TAG = "InfiniteScroll";

    private GridView _gridView;
    private MySimpleItemLoader _mySimpleItemLoader;
    private MyGridViewAdapter _gridviewAdapter;
    private readonly object _scrollLockObject = new object();
    private const int ItemsPerPage = 24;


    private const int LoadNextItemsThreshold = 6;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Main);
      SetupUiElements();
    }

    private void SetupUiElements()
    {
      _mySimpleItemLoader = new MySimpleItemLoader();
      _mySimpleItemLoader.LoadMoreItems(ItemsPerPage); 

      _gridView = FindViewById<GridView>(Resource.Id.gridView);
      _gridviewAdapter = new MyGridViewAdapter(this, _mySimpleItemLoader);
      _gridView.Adapter = _gridviewAdapter;
      _gridView.Scroll += KeepScrollingInfinitely;
    }

    private void KeepScrollingInfinitely(object sender, AbsListView.ScrollEventArgs args)
    {
      lock (_scrollLockObject)
      {
        var mustLoadMore = args.FirstVisibleItem + args.VisibleItemCount >= args.TotalItemCount - LoadNextItemsThreshold;
        if (mustLoadMore && _mySimpleItemLoader.CanLoadMoreItems && !_mySimpleItemLoader.IsBusy)
        {
          _mySimpleItemLoader.IsBusy = true;
          Log.Info(TAG, "Requested to load more items");
          _mySimpleItemLoader.LoadMoreItems(ItemsPerPage);
          _gridviewAdapter.NotifyDataSetChanged();
          _gridView.InvalidateViews();
        }
      }
    }
  }
}

