using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Firebase.Auth;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using UsersProfileApp.Android.Adapter;
using UsersProfileApp.Core.DataStorage;
using UsersProfileApp.Core.Model;
using UsersProfileApp.Core.Service;
using Android.Content.PM;

namespace UsersProfileApp.Android.Activities
{
    [Activity(Label = "HomeActivity", LaunchMode = LaunchMode.SingleInstance)]
    public class HomeActivity : AppCompatActivity, IItemClickListner
    {
        RecyclerView _recyclerView;
        UserAdapter _adapter;
        Toolbar toolbar;
        FirebaseAuth FirebaseAuth;
        List<PhotoModel> users;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_home);

            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "User List";

            _recyclerView = FindViewById<RecyclerView>(Resource.Id.usersProfileRecyclerView);
            users = await UserProfileService.GetUsersProfile();

            _adapter = new UserAdapter(users);
            _adapter.iItemClickListner = this;

            _recyclerView.SetLayoutManager(new LinearLayoutManager(Application.Context));
            _recyclerView.SetAdapter(_adapter);
        }

        public void OnItemClick(int position)
        {
            if (users?.Count > position)
            {
                UsersProfile.SelectedProfile = users[position];
                StartActivity(typeof(UserProfileActivity));
            }
        }

        //public bool onCreateOptionsMenu(IMenu menu)
        //{
        //    MenuInflater.Inflate(Resource.Menu.menu_main, menu);
        //    return base.OnCreateOptionsMenu(menu);
        //}

        // Logout functionality from action menu
        //public override bool OnOptionsItemSelected(IMenuItem item)
        //{
        //    switch (item.ItemId)
        //    {
        //        case Resource.Id.menuLogout:
        //            FirebaseAuth.SignOut();
        //            StartActivity(typeof(MainActivity));
        //            Finish();
        //            return true;

        //        default:
        //            return base.OnOptionsItemSelected(item);
        //    }
        //}

        // Action for hardware Back button
        public override void OnBackPressed()
        {
            // To take no action
        }
    }
}