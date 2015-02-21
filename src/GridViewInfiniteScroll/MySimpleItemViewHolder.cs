using Android.Widget;

namespace GridViewInfiniteScroll
{
  /// <summary>
  /// Acts as a 'pointer' to the controls in the view, so we 
  /// only need to find them the very first time we inflate it
  /// </summary>
  public class MySimpleItemViewHolder : Java.Lang.Object
  {
    public TextView DisplayName { get; set; }
    public ImageView Thumbnail { get; set; }
  }
}