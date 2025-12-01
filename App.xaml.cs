using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace Cloudy
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            bool isLoggedIn = AuthService.IsLoggedIn();             

            if (isLoggedIn)
            {
                return new Window(new AppShell());
            }
            else
            {
                return new Window(new NavigationPage(new LoginPage()));
            }
        }

        public static class AuthService
        {
            private const string LoginKey = "IsLoggedIn";

            public static bool IsLoggedIn()
            {
                return Preferences.Get(LoginKey, false);
            }

            public static void SetLoggedIn(bool loggedIn)
            {
                
                    Preferences.Set(LoginKey, loggedIn);
                   

            }

            public static void Logout()
            {
                Preferences.Set(LoginKey, false);
            }
        }
    }
}