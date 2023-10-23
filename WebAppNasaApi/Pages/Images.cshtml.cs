using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppNasaApi.Models.ImagesLibrary;
using WebAppNasaApi.Services;

namespace WebAppNasaApi.Pages
{
    public class ImageVideosModel : PageModel
    {
        private readonly NasaApiService _nasaApiService;
        public NasaImagesResponse image_response;
        public string input_search;

        public ImageVideosModel(NasaApiService nasaapiservice)
        {
            _nasaApiService = nasaapiservice;
        }

        public void OnGet()
        {
            image_response = ImagesVideosTask("all").Result;
        }

        public void OnPost()
        {
            input_search = Request.Form["input-search-image"];
            image_response = null;
            image_response = ImagesVideosTask(input_search).Result;
            ViewData["is_post"] = true;
        }


        public async Task<NasaImagesResponse> ImagesVideosTask(string query)
        {
            var marsRoverPhotos = await _nasaApiService.GetNasaImagesAsync(query);

            return marsRoverPhotos;
        }
    }
}
