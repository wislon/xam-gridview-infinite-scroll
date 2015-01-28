using Android.Content;
using Android.Views;
using Android.Widget;

namespace GridViewInfiniteScroll
{
  public class MyGridViewAdapter : BaseAdapter<SimpleItem>
  {
    private readonly SimpleItemLoader _simpleItemLoader;
    private readonly Context _context;

    public MyGridViewAdapter(Context context, SimpleItemLoader simpleItemLoader)
    {
      _context = context;
      _simpleItemLoader = simpleItemLoader;
    }

    public override View GetView(int position, View convertView, ViewGroup parent)
    {
      var item = _simpleItemLoader.SimpleItems[position];

      View itemView = convertView ?? LayoutInflater.From(_context).Inflate(Resource.Layout.GridViewCell, parent, false);
      var tvDisplayName = itemView.FindViewById<TextView>(Resource.Id.tvDisplayName);
      var imgThumbail = itemView.FindViewById<ImageView>(Resource.Id.imgThumbnail);

      imgThumbail.SetScaleType(ImageView.ScaleType.CenterCrop);
      imgThumbail.SetPadding(8, 8, 8, 8);

      tvDisplayName.Text = item.DisplayName;
      imgThumbail.SetImageResource(Resource.Drawable.Icon);

      return itemView;
    }


    public override long GetItemId(int position)
    {
      return position;
    }

    public override int Count
    {
      get { return _simpleItemLoader.SimpleItems.Count; }
    }

    public override SimpleItem this[int position]
    {
      get { return _simpleItemLoader.SimpleItems[position]; }
    }
  }
}