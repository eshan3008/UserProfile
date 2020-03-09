using System;
namespace UsersProfileApp.Android.Activity
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", Icon = "@drawable/app_launcher", MainLauncher = true)]
    public class MainActivity : Activity
    {
        EditText usernameTextBox, passwordTextBox;
        Button signInButton, newUserButton;
        TextInputLayout loginUsername, loginPassword;
        FirebaseAuth firebaseauth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            usernameTextBox = (EditText)FindViewById(Resource.Id.usernameEditText);
            passwordTextBox = (EditText)FindViewById(Resource.Id.passwordEditText);
            newUserButton = (Button)FindViewById(Resource.Id.newUser);
            signInButton = (Button)FindViewById(Resource.Id.signInButton);
            loginUsername = (TextInputLayout)FindViewById(Resource.Id.loginusernameWrapper);
            loginPassword = (TextInputLayout)FindViewById(Resource.Id.loginpasswordWrapper);

            InitializeFirebase();

            newUserButton.Click += delegate
            {
                StartActivity(typeof(RegisterActivity));
                Finish();
            };

            signInButton.Click -= LoginButton_Click;
            signInButton.Click += LoginButton_Click;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            bool _isValidLogin = true;

            string username, password;
            username = "1@1.com";
            password = "Eshan12345";

            //username = loginUsername.EditText.Text;
            //password = loginPassword.EditText.Text;

            if (!username.Contains("@"))
            {
                Toast.MakeText(this, "Please provide a valid email address", ToastLength.Short).Show();
                _isValidLogin = false;
            }
            //if (PasswordValidator.validate(passwordTextBox.Text) != null)
            //{
            //    Toast.MakeText(this, "Please provide a valid password", ToastLength.Short).Show();
            //    _isValidLogin = false;
            //}

            if (!_isValidLogin)
                return;

            //TaskCompleteListener taskCompleteListener = new TaskCompleteListener();
            //taskCompleteListener.Success += TaskCompleteListener_Success;
            //taskCompleteListener.Failure += TaskCompleteListener_Failure;

            //firebaseauth.SignInWithEmailAndPassword(username, password)
            //    .AddOnSuccessListener(taskCompleteListener)
            //    .AddOnFailureListener(taskCompleteListener);

            StartActivity(typeof(HomeActivity));
        }

        private void TaskCompleteListener_Failure(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Login Failed !  Please try again", ToastLength.Short).Show();
        }

        private void TaskCompleteListener_Success(object sender, EventArgs e)
        {
            StartActivity(typeof(HomeActivity));
        }

        void InitializeFirebase()
        {
            var app = FirebaseApp.InitializeApp(this);
            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                                .SetApplicationId("1:470655144273:android:7ac3b51a4bf8a7c3d38370 ")
                                .SetApiKey("AIzaSyAi5ihveO6Eh-NbLo7f_IvJNEqFbmvkR0g ")
                                .SetDatabaseUrl("https://loginapp-4e7ca.firebaseio.com")
                                .SetStorageBucket("loginapp-4e7ca.appspot.com")
                                .Build();
                app = FirebaseApp.InitializeApp(this, options);
                firebaseauth = FirebaseAuth.Instance;

            }
            else
            {
            }
        }
    }
}


