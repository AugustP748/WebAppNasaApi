using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppNasaApi.Models.Epic;
using WebAppNasaApi.Models.Mars;
using WebAppNasaApi.Services;

namespace WebAppNasaApi.Pages
{
    public class EpicModel : PageModel
    {
        private readonly NasaApiService _nasaApiService;
        public List<EpicImage> epicImages;
        public string ImageURL;

        public EpicModel(NasaApiService nasaapiservice)
        {
            _nasaApiService = nasaapiservice;
        }

        public void OnGet()
        {
            epicImages = null;
        }

        public async void OnPost()
        {
            DateTime selected_date = DateTime.Parse(Request.Form["date-epic"]);
            //Console.WriteLine(selected_date);
            //Console.ReadLine();
            epicImages = EpicImagesTask(selected_date).Result;
            ImageURL = await _nasaApiService.GetImageURLEPIC(selected_date, epicImages[0].image);
            Console.WriteLine(ImageURL);
            Console.ReadLine();
            ViewData["IsPost"] = true;
        }

        public async Task<List<EpicImage>> EpicImagesTask(DateTime date_epic)
        {
            //Console.WriteLine(date.ToString("yyyy-MM-dd"));
            //Console.ReadLine();
            var epicresult = await _nasaApiService.GetEpicImagesAsync(date_epic);
            
            return epicresult;
        }
    }
}
