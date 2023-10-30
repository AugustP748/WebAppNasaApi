using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppNasaApi.Context;
using WebAppNasaApi.Models;

namespace WebAppNasaApi.Pages
{
    public class SavedModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Apod> list_apod;

        public SavedModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            LoadApodSaveData();
        }

        public void LoadApodSaveData()
        {
            list_apod = _context.ApodDB.ToList();
        }
    }
}
