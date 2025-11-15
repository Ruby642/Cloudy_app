using Cloudy_config;
using Npgsql;

namespace Cloudy
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            TestDbConnection();
            
        }

        
        private async void TestDbConnection()
        {
            try
            {
                using var conn = new NpgsqlConnection(DatabaseConfig.ConnectionString);
                await conn.OpenAsync();
                await DisplayAlert("Success", "Connected to PostgreSQL!", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to connect: {ex.Message}", "OK");
            }
        }
    }
}

