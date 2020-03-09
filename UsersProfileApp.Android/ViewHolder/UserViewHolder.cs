using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using static Android.Views.View;

namespace UsersProfileApp.Android.ViewHolder
{
    public class UserViewHolder : RecyclerView.ViewHolder
    {
        public TextView Caption { get; set; }
        public ImageView Thumbnail { get; set; }
        public LinearLayout Row { get; set; }

        public UserViewHolder(View itemView)
            : base(itemView)
        {
            Caption = itemView.FindViewById<TextView>(Resource.Id.textView1);
            Thumbnail = itemView.FindViewById<ImageView>(Resource.Id.imageView1);
            Row = itemView.FindViewById<LinearLayout>(Resource.Id.userThumbnailRow);
        }
    }
}
