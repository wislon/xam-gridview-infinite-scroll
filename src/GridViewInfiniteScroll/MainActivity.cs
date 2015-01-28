using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace GridViewInfiniteScroll
{
  [Activity(Label = "GridViewInfiniteScroll", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {
    private const string TAG = "InfiniteScroll";

    private GridView _gridView;
    private SimpleItemLoader _simpleItemLoader;
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
      _simpleItemLoader = new SimpleItemLoader();
      _simpleItemLoader.LoadMoreItems(ItemsPerPage); 

      _gridView = FindViewById<GridView>(Resource.Id.gridView);
      _gridviewAdapter = new MyGridViewAdapter(this, _simpleItemLoader);
      _gridView.Adapter = _gridviewAdapter;
      _gridView.Scroll += KeepScrollingInfinitely;
    }

    private void KeepScrollingInfinitely(object sender, AbsListView.ScrollEventArgs args)
    {
      lock (_scrollLockObject)
      {
        var mustLoadMore = args.FirstVisibleItem + args.VisibleItemCount >= args.TotalItemCount - LoadNextItemsThreshold;
        if (mustLoadMore && _simpleItemLoader.CanLoadMoreItems && !_simpleItemLoader.IsBusy)
        {
          _simpleItemLoader.IsBusy = true;
          Log.Info(TAG, "Requested to load more items");
          _simpleItemLoader.LoadMoreItems(ItemsPerPage);
          _gridviewAdapter.NotifyDataSetChanged();
          _gridView.InvalidateViews();
        }
      }
    }
  }
}

