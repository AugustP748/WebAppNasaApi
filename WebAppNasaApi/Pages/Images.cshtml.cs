using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppNasaApi.Context;
using WebAppNasaApi.Models.ImagesLibrary;
using WebAppNasaApi.Services;

namespace WebAppNasaApi.Pages
{
    public class ImageVideosModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly NasaApiService _nasaApiService;
        public NasaImagesResponse image_response;
        public string input_search;

        public ImageVideosModel(NasaApiService nasaapiservice, ApplicationDbContext context)
        {
            _nasaApiService = nasaapiservice;
            _context = context;
        }

        public void OnGet()
        {
            image_response = ImagesVideosTask("all").Result;
        }

        public void OnPost()
        {
            var submitButton = Request.Form["submitButton"];
            if (submitButton == "searchbutton")
            {
                input_search = Request.Form["input-search-image"];
                image_response = null;
                image_response = ImagesVideosTask(input_search).Result;
                ViewData["is_post"] = true;
            }
            else
            {
                //Console.WriteLine("maybe.. favorite button");
                //Console.ReadLine();
            }

        }

        public async Task<NasaImagesResponse> ImagesVideosTask(string query)
        {
            var marsRoverPhotos = await _nasaApiService.GetNasaImagesAsync(query);

            return marsRoverPhotos;
        }
    }
}
