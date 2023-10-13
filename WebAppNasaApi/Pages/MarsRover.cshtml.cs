using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppNasaApi.Models.Mars;
using WebAppNasaApi.Services;

namespace WebAppNasaApi.Pages
{
    public class MarsRoverModel : PageModel
    {
        private readonly NasaApiService _nasaApiService;
        public MarsRoverPhotosResponse _photosResponse;

        public MarsRoverModel(NasaApiService nasaapiservice) 
        { 
            _nasaApiService = nasaapiservice;
        }


        public void OnGet()
        {
            _photosResponse = MarsRoverPhotosTask("curiosity", 1000).Result;
        }

        public async Task<MarsRoverPhotosResponse> MarsRoverPhotosTask(string category,int sun)
        {
            var marsRoverPhotos = await _nasaApiService.GetMarsRoverPhotosAsync(category,sun);

            return marsRoverPhotos;
        }
    }
}
