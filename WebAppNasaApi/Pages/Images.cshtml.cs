using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppNasaApi.Context;
using WebAppNasaApi.Models.ImagesLibrary;
using WebAppNasaApi.Services;
using static System.Net.Mime.MediaTypeNames;

namespace WebAppNasaApi.Pages
{
    public class ImageVideosModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly NasaApiService _nasaApiService;
        public NasaImagesResponse image_response;
        public List<NasaImageModel> image_list;
        public string input_search;

        public ImageVideosModel(NasaApiService nasaapiservice, ApplicationDbContext context)
        {
            _nasaApiService = nasaapiservice;
            _context = context;
        }

        public void OnGet()
        {
            //image_response = ImagesVideosTask("all").Result;
            image_list = OrderData(ImagesVideosTask("all").Result);
        }

        public void OnPost()
        {
            var submitButton = Request.Form["submitButton"];
            if (submitButton == "searchbutton")
            {
                input_search = Request.Form["input-search-image"];
                image_list = null;
                image_list = OrderData(ImagesVideosTask(input_search).Result);
                ViewData["is_post"] = true;
            }
            else
            {
                Console.WriteLine();
                Console.ReadLine();
                //Console.WriteLine("maybe.. favorite button");
                //Console.ReadLine();
            }

        }

        public List<NasaImageModel> OrderData(NasaImagesResponse response)
        {
            List<NasaImageModel> listado_Images = new List<NasaImageModel>();
            string title;
            string description;
            string href;
            string mediatype;

            foreach (var item in response.collection.items)
            {
                int index = 0;
                foreach (var i in item.links)
                { 
                    title = item.data.ElementAt(index).title;
                    description = item.data.ElementAt(index).title;
                    mediatype = item.data.ElementAt(index).title;
                    href = i.href;

                    listado_Images.Add(new NasaImageModel {
                        title = title,
                        description = description,
                        href = href,
                        media_type = mediatype
                    });
                }
            }

            return listado_Images;
        }

        public async Task<NasaImagesResponse> ImagesVideosTask(string query)
        {
            var marsRoverPhotos = await _nasaApiService.GetNasaImagesAsync(query);

            return marsRoverPhotos;
        }
    }
}
