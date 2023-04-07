using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class Weather
{
    public string Name { get; set; }
    public WeatherDescription[] WeatherDescriptions { get; set; }
    public MainData Main { get; set; }
}

public class WeatherDescription
{
    public string Description { get; set; }
}

public class MainData
{
    [JsonProperty("temp")]
    public float Temp { get; set; }
    [JsonProperty("feels_like")]
    public float FeelsLike { get; set; }
    [JsonProperty("temp_min")]
    public float TempMin { get; set; }
    [JsonProperty("temp_max")]
    public float TempMax { get; set; }
    [JsonProperty("pressure")]
    public float Pressure { get; set; }
    [JsonProperty("humidity")]
    public float Humidity { get; set; }
}

public class WeatherApiClient
{

    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public WeatherApiClient(string apiKey)
    {
        _httpClient = new HttpClient();
        _apiKey = apiKey;
    }

    public async Task<Weather> GetWeatherAsync(string location)
    {
        var apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={location}&appid={_apiKey}&units=metric";
        var response = await _httpClient.GetAsync(apiUrl);
        var responseContent = await response.Content.ReadAsStringAsync();
        var weather = JsonConvert.DeserializeObject<Weather>(responseContent);
        return weather;
    }

}
