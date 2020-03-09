using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Net;
using UsersProfileApp.Android.ViewHolder;
using UsersProfileApp.Core.Model;
using UsersProfileApp.Android.Helper;
using static Android.Views.View;

namespace UsersProfileApp.Android.Adapter
{
    public class UserAdapter : RecyclerView.Adapter
    {
        public event EventHandler ItemClick;
        private List<PhotoModel> mUserList;
        public UserAdapter(List<PhotoModel> userName) => mUserList = userName;
        public IItemClickListner iItemClickListner;

        public override RecyclerView.ViewHolder
            OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.user_thumbnail_row, parent, false);

            UserViewHolder vh = new UserViewHolder(itemView);
            return vh;
        }

        public async override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            UserViewHolder vh = holder as UserViewHolder;

            vh.ItemView.Click -= ItemClick;
            vh.ItemView.Click += (sender, e) =>
            {
                if (iItemClickListner != null)
                    iItemClickListner.OnItemClick(position);
            };

            vh.Caption.Text = mUserList[position].Title;

            var bitmapImage = await ImageHelper.GetBitmapFromUrl(mUserList[position].ThumbnailUrl);
            vh.Thumbnail.SetImageBitmap(bitmapImage);
        }

        public override int ItemCount => mUserList.Count;
    }

    public interface IItemClickListner
    {
        void OnItemClick(int position);
    }
}