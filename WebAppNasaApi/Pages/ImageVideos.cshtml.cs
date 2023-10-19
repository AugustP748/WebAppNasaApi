using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppNasaApi.Models.ImagesLibrary;
using WebAppNasaApi.Services;

namespace WebAppNasaApi.Pages
{
    public class ImageVideosModel : PageModel
    {
        private readonly NasaApiService _nasaApiService;
        public NasaImagesAndVideosResponse image_video_response;

        public ImageVideosModel(NasaApiService nasaapiservice)
        {
            _nasaApiService = nasaapiservice;
        }

        public void OnGet()
        {
            image_video_response = ImagesVideosTask("space").Result;
        }

        public async Task<NasaImagesAndVideosResponse> ImagesVideosTask(string query)
        {
            var marsRoverPhotos = await _nasaApiService.GetNasaImagesAndVideosAsync(query);

            return marsRoverPhotos;
        }
    }
}
