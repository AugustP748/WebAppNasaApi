using System.Web;
using WebAppNasaApi.Models;

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

        public async Task<Apod> GetNasaDataAsync()
        {
            // Construye la URL de la API de la NASA según tus necesidades.
            string apiUrl = "https://api.nasa.gov/planetary/apod"; // Sustituye esto con la URL correcta.

            // Agrega la clave de API a la URL.
            var uriBuilder = new UriBuilder(apiUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["api_key"] = _apiKey;
            uriBuilder.Query = query.ToString();

            // Realiza la solicitud a la API.
            var response = await _httpClient.GetFromJsonAsync<Apod>(uriBuilder.ToString());

            return response;
        }
    }
}
