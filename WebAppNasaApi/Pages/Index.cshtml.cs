using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppNasaApi.Context;
using WebAppNasaApi.Models;
using WebAppNasaApi.Services;

namespace WebAppNasaApi.Pages
{

    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly NasaApiService _nasaApiService;
        public Apod DataApod { get; set; }
        public bool _saved { get; set; }

        public IndexModel(NasaApiService nasaApiService, ApplicationDbContext context)
        {
            _nasaApiService = nasaApiService;
            _context = context;
        }

        public void OnGet()
        {
            DataApod = index().Result;
            _saved=verify(DataApod);
        }

        public void OnPost()
        {
            
            DataApod = index().Result;
            if (verify(DataApod))
            {
                Apod datadelete = _context.ApodDB.Where(x => x.title == DataApod.title).First();
                _context.ApodDB.Remove(datadelete);
            }
            else 
            {
                _context.ApodDB.Add(DataApod);
            }
            _context.SaveChanges();
            _saved = verify(DataApod);
        }

        public bool verify(Apod ap)
        {
            if (_context.ApodDB.Any(c => c.title == ap.title))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Apod> index()
        {
            var nasaData = await _nasaApiService.GetNasaDataAsync();

            return nasaData;
        }
    }
}