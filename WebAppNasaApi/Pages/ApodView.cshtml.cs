using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppNasaApi.Models;
using WebAppNasaApi.Services;

namespace WebAppNasaApi.Pages
{
    public class ApodViewModel : PageModel
    {

        private readonly NasaApiService _nasaApiService;
        public Apod DataApod;

        public ApodViewModel(NasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;
        }

        public void OnGet()
        {
            DataApod = index().Result;
        }

        public async Task<Apod> index()
        {
            var nasaData = await _nasaApiService.GetNasaDataAsync();
            
            return nasaData;
        }
    }
}
