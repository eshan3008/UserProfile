using System;
using Android.App;
using Android.Gms.Tasks;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using UsersProfileApp.Android.Helper;

namespace UsersProfileApp.Android.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", Icon = "@drawable/app_launcher", MainLauncher = true)]
    public class MainActivity : Activity, IOnCompleteListener
    {
        Button signInButton, newUserButton;
        TextInputLayout loginUsername, loginPassword;
        ImageView appImage;
        FirebaseAuth firebaseauth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            newUserButton = (Button)FindViewById(Resource.Id.newUser);
            signInButton = (Button)FindViewById(Resource.Id.signInButton);
            loginUsername = (TextInputLayout)FindViewById(Resource.Id.loginusernameWrapper);
            loginPassword = (TextInputLayout)FindViewById(Resource.Id.loginpasswordWrapper);
            appImage = (ImageView)FindViewById(Resource.Id.imageIcon);

            appImage.RequestLayout();
            appImage.LayoutParameters.Height = 300;
            appImage.LayoutParameters.Width = 300;
            appImage.SetScaleType(ImageView.ScaleType.FitXy);

            InitializeFirebase();

            newUserButton.Click += delegate
            {
                StartActivity(typeof(RegisterActivity));
                Finish();
            };

            signInButton.Click -= LoginButton_Click;
            signInButton.Click += LoginButton_Click;

            loginPassword.HintEnabled = false;
            loginUsername.HintEnabled = false;
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            bool _isValidLogin = false;

            string username = loginUsername.EditText.Text.ToString();
            string password = loginPassword.EditText.Text.ToString();

            if (!EmailValidator.IsValidEmail(username))
            {
                Toast.MakeText(this, "Please provide a valid email address", ToastLength.Short).Show();
                _isValidLogin = false;
            }
            else if (PasswordValidator.validate(password, username) != null)
            {
                Toast.MakeText(this, "Password doesnot meet the requirements", ToastLength.Short).Show();
                _isValidLogin = false;
            }
            else
            {
                _isValidLogin = true;

                firebaseauth.SignInWithEmailAndPassword(username, password).AddOnCompleteListener(this);
            }
        }

        void InitializeFirebase()
        {
            var app = FirebaseApp.InitializeApp(this);
            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                                .SetApplicationId("1:470655144273:android:7ac3b51a4bf8a7c3d38370")
                                .SetApiKey("AIzaSyAi5ihveO6Eh-NbLo7f_IvJNEqFbmvkR0g")
                                .SetDatabaseUrl("https://loginapp-4e7ca.firebaseio.com")
                                .SetStorageBucket("loginapp-4e7ca.appspot.com")
                                .Build();
                app = FirebaseApp.InitializeApp(this, options);
            }

            firebaseauth = new FirebaseAuth(app);
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful == true)
            {
                StartActivity(typeof(HomeActivity));
                Finish();
            }
            else
            {
                Toast.MakeText(this, "Username and Password doesnot match", ToastLength.Short).Show();
            }
        }
    }
}


