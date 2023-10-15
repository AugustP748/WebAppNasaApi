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
        }

        public void OnPost()
        {
            var selected_rover = Request.Form["rover_name"];
            var selected_date = Request.Form["earth_date"];
            //Console.WriteLine(selected_date);
            //Console.ReadLine();
            _photosResponse = MarsRoverPhotosTask(selected_rover).Result;
        }

        public async Task<MarsRoverPhotosResponse> MarsRoverPhotosTask(string category)
        {
            var marsRoverPhotos = await _nasaApiService.GetMarsRoverPhotosAsync(category);

            return marsRoverPhotos;
        }
    }
}
