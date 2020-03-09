using System;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;

namespace UsersProfileApp.Android.Activities
{
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : Activity
    {
        RecyclerView _recyclerView;
        UserAdapter _adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _recyclerView = FindViewById<RecyclerView>(Resource.Id.usersProfileRecyclerView);
            var listOfusers = UserProfileService.GetUsersProfile();
            _adapter = new UserAdapter(listOfusers);
            _recyclerView.SetAdapter(_adapter);
            // Create your application here
        }
    }
}