using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Configuration;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // to keep on track which button is pressed
        private Button _selectedButton;

        public MainWindow()
        {
            InitializeComponent();
            UpdateView("Joensuu,fi");
        }
        private async void UpdateView(string location)
        {
            // Enter your OpenWeatherMap API key here
            string apiKey = ConfigurationManager.AppSettings["ApiKey"];

            // Create a new instance of WeatherApiClient
            var client = new WeatherApiClient(apiKey);

            // Get the weather data for the given location
            var weather = await client.GetWeatherAsync(location);

            // Print the weather data to the console
            System.Diagnostics.Debug.WriteLine($"Location: {weather.Name}");
            System.Diagnostics.Debug.WriteLine($"Temperature: {weather.Main.Temp}°C");
            System.Diagnostics.Debug.WriteLine($"Humidity: {weather.Main.Humidity}%");
            System.Diagnostics.Debug.WriteLine($"Feels like: {weather.Main.FeelsLike}°C");
            System.Diagnostics.Debug.WriteLine($"Min: {weather.Main.TempMin}°C");
            System.Diagnostics.Debug.WriteLine($"Max: {weather.Main.TempMax}°C");

            // Update Textblocks
            SelectedCityTextBlock.Text=weather.Name;
            string temp = ($"Lämpötila: {weather.Main.Temp}°C");
            TemperatureTextBlock.Text = temp;
            string feels = ($"Tuntuu kuin: {weather.Main.FeelsLike}°C");
            FeelsLikeTextBlock.Text = feels;
            string humid = ($"Ilman kosteus: {weather.Main.Humidity}%");
            HumidityTextBlock.Text = humid;

            // set tooltip text
            TemperatureToolTip.Text = $"Alin: {weather.Main.TempMin}°C, Ylin: {weather.Main.TempMax}°C";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush(Color.FromRgb(173, 216, 230)); // set the background color to light blue
            }

            var button = (Button)sender;
            var location = (string)button.Tag;
            UpdateView(location);

            button.Background = new SolidColorBrush(Color.FromRgb(0, 191, 255)); // set the background color to red
            _selectedButton = button;
        }
    }
}
