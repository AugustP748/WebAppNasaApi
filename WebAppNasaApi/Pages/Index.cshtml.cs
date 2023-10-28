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
        public bool _saved = false;

        public IndexModel(NasaApiService nasaApiService, ApplicationDbContext context)
        {
            _nasaApiService = nasaApiService;
            _context = context;
        }

        public void OnGet()
        {
            DataApod = index().Result;
            verify(DataApod);
        }

        public void OnPost()
        {
            
            DataApod = index().Result;
            if (_saved is true)
            {
                Apod datadelete = _context.ApodDB.Where(x => x.title == DataApod.title).First();
                _context.ApodDB.Remove(datadelete);
            }
            else 
            {
                _context.ApodDB.Add(DataApod);
            }
            _context.SaveChanges();
            verify(DataApod);
        }

        public void verify(Apod ap)
        {
            if (_context.ApodDB.Any(c => c.title == ap.title))
            {
                _saved = true;
            }
            else
            {
                _saved = false;
            }
            //Console.WriteLine(_saved);
            //Console.ReadLine();
        }

        public async Task<Apod> index()
        {
            var nasaData = await _nasaApiService.GetNasaDataAsync();

            return nasaData;
        }
    }
}