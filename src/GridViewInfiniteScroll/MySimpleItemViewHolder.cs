using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace GridViewInfiniteScroll
{
  /// <summary>
  /// Acts as a 'pointer' to the controls in the view, so we
  /// only need to find them the very first time we inflate it
  /// </summary>
  public class MySimpleItemViewHolder : RecyclerView.ViewHolder
  {
    public MySimpleItemViewHolder(View itemView) : base(itemView)
    {
      DisplayName = itemView.FindViewById<TextView>(Resource.Id.tvDisplayName);
      Thumbnail = itemView.FindViewById<ImageView>(Resource.Id.imgThumbnail);
    }

    public TextView DisplayName { get; set; }
    public ImageView Thumbnail { get; set; }
  }
}
