using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
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
            _photosResponse = null;
        }

        public void OnPost()
        {
            string selected_rover = Request.Form["rover_name"];
            DateTime selected_date = DateTime.Parse(Request.Form["earth_date"]);
            //Console.WriteLine(selected_date);
            //Console.ReadLine();
            _photosResponse = MarsRoverPhotosTask(selected_rover, selected_date).Result;
            ViewData["IsPost"] = true;
        }

        public async Task<MarsRoverPhotosResponse> MarsRoverPhotosTask(string category,DateTime date)
        {
            //Console.WriteLine(date.ToString("yyyy-MM-dd"));
            //Console.ReadLine();
            var marsRoverPhotos = await _nasaApiService.GetMarsRoverPhotosAsync(category,date);

            return marsRoverPhotos;
        }
    }
}
