using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Dynamic;
using WebAppNasaApi.Models.ImagesLibrary;
using WebAppNasaApi.Services;

namespace WebAppNasaApi.Pages
{
    public class ImageDetailsModel : PageModel
    {
        public List<NasaImageModel> nasaimagemodel = new List<NasaImageModel>();
        private readonly NasaApiService _nasaApiService;

        public ImageDetailsModel(NasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;
        }

        public IActionResult OnGetAsync(string nasa_id)
        {
            //nasaimagemodel = GetTheImageTask(nasa_id).Result;
            nasaimagemodel = OrderData(GetTheImageTask(nasa_id).Result);
            //nasaimagemodel = JsonConvert.DeserializeObject<NasaImageModel>(json);

            if (nasaimagemodel == null)
            {
                return NotFound();
            }

            return Page();
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

        public async Task<NasaImagesResponse> GetTheImageTask(string nasa_id)
        {
            return await _nasaApiService.GetTheNasaImageAsync(nasa_id);
        }
    }
}
