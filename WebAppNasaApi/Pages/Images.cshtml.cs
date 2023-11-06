using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppNasaApi.Models.ImagesLibrary;
using WebAppNasaApi.Services;

namespace WebAppNasaApi.Pages
{
    public class ImageVideosModel : PageModel
    {
        private readonly NasaApiService _nasaApiService;
        public NasaImagesResponse image_response;
        public List<NasaImageModel> image_list;
        public string input_search;

        public ImageVideosModel(NasaApiService nasaapiservice)
        {
            _nasaApiService = nasaapiservice;
        }

        public void OnGet()
        {
            //image_response = ImagesVideosTask("all").Result;
            image_list = OrderData(ImagesVideosTask("all").Result);
        }

        public void OnPost()
        {
            input_search = Request.Form["input-search-image"];
            image_list = null;
            image_list = OrderData(ImagesVideosTask(input_search).Result);
            ViewData["is_post"] = true;
        }

        public List<NasaImageModel> OrderData(NasaImagesResponse response)
        {
            List<NasaImageModel> listado_Images = new List<NasaImageModel>();
            string title;
            string description;
            string href;
            string mediatype;
            string nasa_id;

            foreach (var item in response.collection.items)
            {
                int index = 0;
                foreach (var i in item.links)
                { 
                    title = item.data.ElementAt(index).title;
                    description = item.data.ElementAt(index).description;
                    mediatype = item.data.ElementAt(index).media_type;
                    nasa_id = item.data.ElementAt(index).nasa_id;
                    href = i.href;

                    listado_Images.Add(new NasaImageModel
                    {
                        title = title,
                        description = description,
                        href = href,
                        media_type = mediatype,
                        nasa_id = nasa_id
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
