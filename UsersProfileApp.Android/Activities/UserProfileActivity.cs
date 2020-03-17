using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using UsersProfileApp.Android.Helper;
using UsersProfileApp.Core.DataStorage;
using AndroidSupport = Android.Support; // To fully qualify the conflict between the project name and namespace

namespace UsersProfileApp.Android.Activities
{
    [Activity(Label = "UserProfileActivity", LaunchMode = LaunchMode.SingleInstance)]
    public class UserProfileActivity : AppCompatActivity
    {
        public TextView Caption { get; set; }
        public ImageView Photo { get; set; }
        public TextView ProfileLink { get; set; }
        private AndroidSupport.V7.Widget.Toolbar toolbar;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.user_profile_layout);

            Caption = FindViewById<TextView>(Resource.Id.userTitle);
            ProfileLink = FindViewById<TextView>(Resource.Id.userProfileLink);
            Photo = FindViewById<ImageView>(Resource.Id.userImage);
            toolbar = FindViewById<AndroidSupport.V7.Widget.Toolbar>(Resource.Id.toolbar); // For Logout Action Menu
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Selected Profile";

            ProfileLink.Click -= ProfileLink_Click;
            ProfileLink.Click += ProfileLink_Click;

            if (UsersProfile.SelectedProfile != null)
            {
                Caption.Text = UsersProfile.SelectedProfile.Title;

                var bitmapImage = await ImageHelper.GetBitmapFromUrl(UsersProfile.SelectedProfile.Url);
                Photo.SetImageBitmap(bitmapImage);
            }
        }

        // Open up the Image in browser
        private void ProfileLink_Click(object sender, EventArgs e)
        {
            var uri = global::Android.Net.Uri.Parse(UsersProfile.SelectedProfile?.Url);
            Intent browserIntent = new Intent(Intent.ActionView, uri);
            StartActivity(browserIntent);
        }
    }
}
