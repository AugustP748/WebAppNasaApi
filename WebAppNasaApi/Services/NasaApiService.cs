using System.Net;
using System.Web;
using WebAppNasaApi.Models;
using WebAppNasaApi.Models.Epic;
using WebAppNasaApi.Models.ImagesLibrary;
using WebAppNasaApi.Models.Mars;


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

        // MARS ROVER PHOTOS
        public async Task<MarsRoverPhotosResponse> GetMarsRoverPhotosAsync(string roverName,DateTime earth_date)
        {
            string apiUrl = $"https://api.nasa.gov/mars-photos/api/v1/rovers/{roverName}/photos?earth_date={earth_date.ToString("yyyy-MM-dd")}";

            var uriBuilder = new UriBuilder(apiUrl);
            var queryParameters = HttpUtility.ParseQueryString(uriBuilder.Query);
            queryParameters["api_key"] = _apiKey;
            uriBuilder.Query = queryParameters.ToString();

            var response = await _httpClient.GetFromJsonAsync<MarsRoverPhotosResponse>(uriBuilder.ToString());

            return response;
        }

        // EPIC API 
        public async Task<List<EpicImage>> GetEpicImagesAsync(DateTime date_epic)
        {
            string apiUrl = $"https://api.nasa.gov/EPIC/api/natural/date/{date_epic:yyyy-MM-dd}";

            var uriBuilder = new UriBuilder(apiUrl);
            var queryParameters = HttpUtility.ParseQueryString(uriBuilder.Query);
            queryParameters["api_key"] = _apiKey;
            uriBuilder.Query = queryParameters.ToString();

            var response = await _httpClient.GetFromJsonAsync<List<EpicImage>>(uriBuilder.ToString());

            return response;
        }

        // GET IMAGE EPIC
        public async Task<byte[]> GetImageURLEPIC(DateTime date_epic,string name_image)
        {
            //string apiUrl = $"https://epic.gsfc.nasa.gov/archive/natural/2015/10/31/png/{name_image}.png";
            string apiUrl = $"https://api.nasa.gov/EPIC/archive/natural/2019/05/30/png/epic_1b_20190530011359.png";

            var uriBuilder = new UriBuilder(apiUrl);
            var queryParameters = HttpUtility.ParseQueryString(uriBuilder.Query);
            queryParameters["api_key"] = _apiKey;
            uriBuilder.Query = queryParameters.ToString();

            var response = await _httpClient.GetAsync(uriBuilder.ToString());

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Failed to download image: {response.StatusCode}");
            }

            return await response.Content.ReadAsByteArrayAsync();
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
