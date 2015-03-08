using Android.App;
using Android.Support.V7.Widget;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace GridViewInfiniteScroll
{
  [Activity(Label = "GridViewInfiniteScroll", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {
    private const string TAG = "InfiniteScroll";

    private MySimpleItemLoader _mySimpleItemLoader;
    private readonly object _scrollLockObject = new object();
    private RecyclerView _recyclerView;
    private MyRecyclerAdapter _myRecyclerAdapter;
    private InfiniteScrollListener _infiniteScrollListener;

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

      _myRecyclerAdapter = new MyRecyclerAdapter(_mySimpleItemLoader);
      _recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerGridView);
      var sglm = new StaggeredGridLayoutManager(2, StaggeredGridLayoutManager.Vertical);

      _recyclerView.SetLayoutManager(sglm);
      _recyclerView.SetAdapter(_myRecyclerAdapter);

      _infiniteScrollListener = new InfiniteScrollListener(_mySimpleItemLoader, 
                                                           _myRecyclerAdapter, 
                                                           sglm, 
                                                           ItemsPerPage, 
                                                           this.UpdateDataAdapter);

      _recyclerView.SetOnScrollListener(_infiniteScrollListener);
    }

    private void UpdateDataAdapter()
    {
      int count = _mySimpleItemLoader.MySimpleItems.Count;
      Toast.MakeText(this, string.Format("{0} items", count), ToastLength.Short).Show();
      if (count > 0)
      {
        _myRecyclerAdapter.NotifyDataSetChanged();
      }
    }


  }
}

