using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Cloudy;
namespace Cloudy
{


    public partial class SignUp : ContentPage
    {
        readonly UserService _userService = new UserService();

        public SignUp()
        {
            InitializeComponent();
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text.Trim();
            string username = UsernameEntry.Text.Trim();
            string password = PasswordEntry.Text;
            string repeat = RepeatPasswordEntry.Text;

            if (password != repeat)
            {
                await DisplayAlertAsync("Error", "Passwords don't match", "OK");
                return;
            }

            try
            {
                bool ok = await _userService.RegisterUser(username, email, password);
                if (ok)
                {
                    await DisplayAlertAsync("Success", "Registered", "OK");
                    await Navigation.PushAsync(new LoginPage());
                }
            }
            catch (InvalidOperationException ex)
            {
                await DisplayAlertAsync("Error", ex.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Error", "Something went wrong: " + ex.Message, "OK");
            }
        }
        private async void OnGoToLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }




    }
}