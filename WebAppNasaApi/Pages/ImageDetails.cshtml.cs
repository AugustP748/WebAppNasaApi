using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Dynamic;
using WebAppNasaApi.Context;
using WebAppNasaApi.Models;
using WebAppNasaApi.Models.ImagesLibrary;
using WebAppNasaApi.Services;

namespace WebAppNasaApi.Pages
{
    public class ImageDetailsModel : PageModel
    {
        public List<NasaImageModel> nasaimagemodel = new List<NasaImageModel>();
        private readonly NasaApiService _nasaApiService;
        private readonly ApplicationDbContext _context;
        public bool _saved { get; set; }


        public ImageDetailsModel(NasaApiService nasaApiService, ApplicationDbContext context)
        {
            _nasaApiService = nasaApiService;
            _context = context;
        }

        public IActionResult OnGetAsync(string nasa_id)
        {
            nasaimagemodel = OrderData(GetTheImageTask(nasa_id).Result);
            _saved = verify(nasaimagemodel[0]);

            if (nasaimagemodel == null)
            {
                return NotFound();
            }

            return Page();
        }

        public void OnPost(string id_image)
        {
            //Console.WriteLine(id_image);
            //Console.ReadLine();
            nasaimagemodel = OrderData(GetTheImageTask(id_image).Result);

            if (verify(nasaimagemodel[0]))
            {
                NasaImageModel datadelete = _context.NasaImageDB.Where(x => x.title == nasaimagemodel[0].title).First();
                _context.NasaImageDB.Remove(datadelete);
            }
            else
            {
                _context.NasaImageDB.Add(nasaimagemodel[0]);
            }
            _context.SaveChanges();
            _saved = verify(nasaimagemodel[0]);
        }

        public bool verify(NasaImageModel imagemodel)
        {
            if (_context.NasaImageDB.Any(c => c.nasa_id == imagemodel.nasa_id))
            {
                return true;
            }
            else
            {
                return false;
            }
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
