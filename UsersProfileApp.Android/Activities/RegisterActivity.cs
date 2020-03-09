using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Tasks;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using UsersProfileApp.Android.Helper;

namespace UsersProfileApp.Android.Activities
{
    [Activity(Label = "RegisterActivity", LaunchMode = LaunchMode.SingleInstance)]
    public class RegisterActivity : Activity, IOnCompleteListener
    {
        EditText usernameTextBox, passwordTextBox;
        Button registerButton;
        TextInputLayout passwordWrapper, usernameWrapper;
        View view;
        public static FirebaseApp app;
        FirebaseAuth firebaseAuth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register);
            passwordWrapper = (TextInputLayout)FindViewById(Resource.Id.passwordWrapper);
            usernameWrapper = (TextInputLayout)FindViewById(Resource.Id.usernameWrapper);
            usernameTextBox = (EditText)FindViewById(Resource.Id.regUsernameEditText);
            passwordTextBox = (EditText)FindViewById(Resource.Id.regPasswordEditText);
            registerButton = (Button)FindViewById(Resource.Id.registerButton);

            registerButton.Click -= OnRegisterClick;
            registerButton.Click += OnRegisterClick;

            InitializeFirebase();

            passwordWrapper.HintEnabled = false;
            usernameWrapper.HintEnabled = false;

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

            firebaseAuth = new FirebaseAuth(app);
        }

        public static void hideKeyboard(object textbox)
        {
            var _view = (View)textbox;
            var _currentActivity = ((Activity)_view.Context);
            View currentFocus = _currentActivity.CurrentFocus;
            if (currentFocus != null)
            {
                InputMethodManager inputMethodManager = (InputMethodManager)_currentActivity.GetSystemService(Context.InputMethodService);
                inputMethodManager.HideSoftInputFromWindow(currentFocus.WindowToken, HideSoftInputFlags.None);
            }
        }

        // Register button Event Handler
        private void OnRegisterClick(object sender, EventArgs args)
        {
            hideKeyboard(sender);

            if (!EmailValidator.IsValidEmail(usernameTextBox.Text))
            {
                usernameWrapper.Error = "Please enter a valid email address";
                usernameWrapper.ErrorEnabled = true;
            }
            else
            {
                CorrectUsername();

            }

            if (PasswordValidator.validate(passwordTextBox.Text, usernameTextBox.Text) != null)
            {
                passwordWrapper.Error = PasswordValidator.validate(passwordTextBox.Text, usernameTextBox.Text);
                passwordWrapper.ErrorEnabled = true;
            }
            else
            {
                CorrectPassword();
            }

            if (usernameWrapper.ErrorEnabled == false && passwordWrapper.ErrorEnabled == false)
            {
                CorrectInputs();
            }

        }

        private void CorrectUsername()
        {
            usernameWrapper.Error = null;
            usernameWrapper.ErrorEnabled = false;
        }

        private void CorrectPassword()
        {
            passwordWrapper.Error = null;
            passwordWrapper.ErrorEnabled = false;
        }

        private void CorrectInputs()
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivityForResult(intent, 0);
            SetResult(Result.Ok, intent);
            RegisterUser(usernameTextBox.Text, passwordTextBox.Text);
            Finish();
        }

        public void OnClick(View v)
        {
            view = v;

        }

        // Action for hardware Back button
        public override void OnBackPressed()
        {
            base.OnBackPressed();
            StartActivity(typeof(MainActivity));
        }

        protected void onStart()
        {
            base.OnStart();
            if (firebaseAuth.CurrentUser != null)
            {
                // handle already logged in user
                Toast.MakeText(this, "Already logged in User", ToastLength.Short).Show();
            }
        }

        // Username is the email address provided
        void RegisterUser(string username, string password)
        {
            firebaseAuth.CreateUserWithEmailAndPassword(username, password).AddOnCompleteListener(this);
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful == true)
            {
                Toast.MakeText(this, "Registration Success ", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "Registration Failed ", ToastLength.Short).Show();
            }
        }
    }
}