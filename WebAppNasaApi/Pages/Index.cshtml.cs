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
        public Apod DataApod;

        public IndexModel(NasaApiService nasaApiService, ApplicationDbContext context)
        {
            _nasaApiService = nasaApiService;
            _context = context;
        }

        public void OnGet()
        {
            //DataApod = index().Result;
        }

        public async Task<Apod> index()
        {
            var nasaData = await _nasaApiService.GetNasaDataAsync();

            return nasaData;
        }
    }
}