using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using WeatherApp.Service;
using Xamarin.Essentials;

namespace WeatherApp.View
{
    public partial class DetailPage : ContentPage
    {
        
        public DetailPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            loadingLabel.IsVisible = true;
            SetWeatherInfoVisibility(false);

            await GetLocationDataAsync();
            
            loadingLabel.IsVisible = false;
            SetWeatherInfoVisibility(true);
        }

        private async Task GetLocationDataAsync()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                }

                if (status == PermissionStatus.Granted)
                {
                    Location location = await Geolocation.GetLastKnownLocationAsync();
                    if (location == null)
                    {
                        location = await Geolocation.GetLocationAsync(new GeolocationRequest
                        {
                            DesiredAccuracy = GeolocationAccuracy.Medium,
                            Timeout = TimeSpan.FromSeconds(30)
                        });
                    }

                    if (location != null)
                    {
                        loc.Text = $"Lat: {location.Latitude}, Lon: {location.Longitude}";
                        WeatherService weatherService = new WeatherService();
                        Root weatherData = await weatherService.getWeatherByCity(location.Latitude.ToString(), location.Longitude.ToString());
                        BindingContext = weatherData;
                    }
                    else
                    {
                        loc.Text = "Unable to retrieve location.";
                    }
                }
                else
                {
                    loc.Text = "Location permission denied.";
                }
            }
            catch (Exception ex)
            {
                loc.Text = $"Error: {ex.Message}";
                Console.WriteLine(ex);
            }
        }

        private void SetWeatherInfoVisibility(bool isVisible)
        {
            cityLabel.IsVisible = isVisible;
            temperatureStack.IsVisible = isVisible;
            descriptionStack.IsVisible = isVisible;
            windSpeedStack.IsVisible = isVisible;
            loc.IsVisible = isVisible;
        }
    }
}
