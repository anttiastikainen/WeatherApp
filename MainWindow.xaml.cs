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
        public MainWindow()
        {
            InitializeComponent();
            UpdateView("Joensuu,fi");
            Console.WriteLine("Hello world!");
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
            string temp = ($"Temperature: {weather.Main.Temp}°C");
            TemperatureTextBlock.Text = temp;
            string humid = ($"Humidity: {weather.Main.Humidity}%");
            HumidityTextBlock.Text = humid;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var location = (string)button.Tag;
            UpdateView(location);
        }
    }
}
