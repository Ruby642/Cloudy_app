using Cloudy;
namespace Cloudy
{


    public partial class LoginPage : ContentPage
    {
        readonly UserService _userService = new UserService();

        public LoginPage()
        {
            InitializeComponent();
        }
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = LoginUsernameEntry.Text.Trim();
            string password = LoginPasswordEntry.Text;

            bool ok = await _userService.LoginUser(username, password);
            if (ok)
                await DisplayAlertAsync("Success", "Login successful", "OK");
            else
                await DisplayAlertAsync("Error", "Invalid username or password", "OK");
        }
        private async void OnGoToSignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp());
        }

    }
}