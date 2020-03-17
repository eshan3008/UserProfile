using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using UsersProfileApp.Android.Adapter;
using UsersProfileApp.Core.DataStorage;
using UsersProfileApp.Core.Model;
using UsersProfileApp.Core.Service;
using Android.Content.PM;
using AndroidSupport = Android.Support;
using Android.Views;
using UsersProfileApp.Android.Helper;

namespace UsersProfileApp.Android.Activities
{
    [Activity(Label = "HomeActivity", LaunchMode = LaunchMode.SingleInstance)]
    public class HomeActivity : AppCompatActivity, IItemClickListner
    {
        RecyclerView _recyclerView;
        UserAdapter _adapter;
        Toolbar toolbar;
        List<PhotoModel> users;
        Dialog dialog;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_home);

            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "User List";

            setDialog();
            _recyclerView = FindViewById<RecyclerView>(Resource.Id.usersProfileRecyclerView);
            users = await UserProfileService.GetUsersProfile();

            _adapter = new UserAdapter(users);
            _adapter.iItemClickListner = this;

            _recyclerView.SetLayoutManager(new LinearLayoutManager(Application.Context));
            _recyclerView.SetAdapter(_adapter);

            if (users != null) dialog.Dismiss();
        }

        public void OnItemClick(int position)
        {
            if (users?.Count > position)
            {
                UsersProfile.SelectedProfile = users[position];
                StartActivity(typeof(UserProfileActivity));
            }
        }

        private void setDialog()
        {
            AndroidSupport.V7.App.AlertDialog.Builder builder = new AndroidSupport.V7.App.AlertDialog.Builder(this);
            builder.SetView(Resource.Layout.Progress);
            dialog = builder.Create();
            dialog.SetCancelable(false);
            dialog.Show();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menuLogout)
            {
                FirebaseHelper.FirebaseAuthentication.SignOut();
                StartActivity(typeof(MainActivity));
                Finish();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        // Action for hardware Back button
        public override void OnBackPressed()
        {
            // To take no action
        }
    }
}