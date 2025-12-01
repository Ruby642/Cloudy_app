using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Cloudy_config;
using Npgsql;

namespace Cloudy
{
    public partial class MainPage : ContentPage
    {
        bool _dbTested;

        public MainPage()
        {
            InitializeComponent();
            TestDbConnectionAsync();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_dbTested)
                return;

            _dbTested = true;
            await TestDbConnectionAsync();
        }

        private async Task TestDbConnectionAsync()
        {
            try
            {
              
                await using var conn = new NpgsqlConnection(DatabaseConfig.ConnectionString);
                await conn.OpenAsync();
                await DisplayAlertAsync("Success", "Connected to PostgreSQL!", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Error", $"Failed to connect: {ex.Message}", "OK");
            }
        }
    }
}

