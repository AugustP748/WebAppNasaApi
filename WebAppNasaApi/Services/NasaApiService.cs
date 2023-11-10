using System.Web;
using WebAppNasaApi.Models;
using WebAppNasaApi.Models.ImagesLibrary;



namespace WebAppNasaApi.Services
{
    public class NasaApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public NasaApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["NasaApiKey"]; // Configura tu clave de API en la configuración de la aplicación.
        }

        // APOD API
        public async Task<Apod> GetNasaDataAsync()
        {
            
            string apiUrl = "https://api.nasa.gov/planetary/apod"; 

            // Clave de API a la URL.
            var uriBuilder = new UriBuilder(apiUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["api_key"] = _apiKey;
            uriBuilder.Query = query.ToString();

            // Solicitud a la API.
            var response = await _httpClient.GetFromJsonAsync<Apod>(uriBuilder.ToString());

            return response;
        }

        // IMAGES
        public async Task<NasaImagesResponse> GetNasaImagesAsync(string query)
        {
            string apiUrl = $"https://images-api.nasa.gov/search?q={query}&media_type=image";

            var uriBuilder = new UriBuilder(apiUrl);
            var queryParameters = HttpUtility.ParseQueryString(uriBuilder.Query);
            
            uriBuilder.Query = queryParameters.ToString();

            var response = await _httpClient.GetFromJsonAsync<NasaImagesResponse>(uriBuilder.ToString());

            return response;
        }

        // GET A SPECIFIC IMAGE BY NASA_ID
        public async Task<NasaImagesResponse> GetTheNasaImageAsync(string nasa_id)
        {
            string apiUrl = $"https://images-api.nasa.gov/search?nasa_id={nasa_id}";

            var uriBuilder = new UriBuilder(apiUrl);
            var queryParameters = HttpUtility.ParseQueryString(uriBuilder.Query);

            uriBuilder.Query = queryParameters.ToString();

            var response = await _httpClient.GetFromJsonAsync<NasaImagesResponse>(uriBuilder.ToString());

            return response;
        }

    }
}
