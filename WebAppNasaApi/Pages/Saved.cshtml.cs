using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppNasaApi.Context;
using WebAppNasaApi.Models;
using WebAppNasaApi.Models.ImagesLibrary;

namespace WebAppNasaApi.Pages
{
    public class SavedModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Apod> list_apod;
        public List<NasaImageModel> list_image_model;

        public SavedModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            LoadSavedData();
        }

        public void LoadSavedData()
        {
            list_apod = _context.ApodDB.ToList();
            list_image_model = _context.NasaImageDB.ToList();
        }

        public void OnPost(string id_image)
        {
            NasaImageModel datadelete = _context.NasaImageDB.Where(x => x.nasa_id == id_image).First();
            _context.NasaImageDB.Remove(datadelete);
            _context.SaveChanges();
            LoadSavedData();
        }
    }
}
