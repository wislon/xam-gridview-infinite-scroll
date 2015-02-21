using Android.Content;
using Android.Views;
using Android.Widget;

namespace GridViewInfiniteScroll
{
  public class MyGridViewAdapter : BaseAdapter<MySimpleItem>
  {
    private readonly MySimpleItemLoader _mySimpleItemLoader;
    private readonly Context _context;

    public MyGridViewAdapter(Context context, MySimpleItemLoader mySimpleItemLoader)
    {
      _context = context;
      _mySimpleItemLoader = mySimpleItemLoader;
    }

    public override View GetView(int position, View convertView, ViewGroup parent)
    {
      var dataItem = _mySimpleItemLoader.MySimpleItems[position];

      View itemView = convertView;
      MySimpleItemViewHolder viewHolder;

      if (itemView != null)
      {
        viewHolder = (MySimpleItemViewHolder)itemView.Tag;
      }
      else
      {
        viewHolder = new MySimpleItemViewHolder();
        itemView = LayoutInflater.From(_context).Inflate(Resource.Layout.MyGridViewCell, parent, false);

        var imgThumbail = itemView.FindViewById<ImageView>(Resource.Id.imgThumbnail);
        imgThumbail.SetScaleType(ImageView.ScaleType.CenterCrop);
        imgThumbail.SetPadding(8, 8, 8, 8);

        viewHolder.DisplayName = itemView.FindViewById<TextView>(Resource.Id.tvDisplayName);
        viewHolder.Thumbnail = imgThumbail;

        itemView.Tag = viewHolder;
      }

      viewHolder.DisplayName.Text = dataItem.DisplayName;
      viewHolder.Thumbnail.SetImageResource(Resource.Drawable.Icon);
      return itemView;
    }

    public override long GetItemId(int position)
    {
      return position;
    }

    public override int Count
    {
      get { return _mySimpleItemLoader.MySimpleItems.Count; }
    }

    public override MySimpleItem this[int position]
    {
      get { return _mySimpleItemLoader.MySimpleItems[position]; }
    }
  }
}