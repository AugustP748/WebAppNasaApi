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

        public EpicModel(NasaApiService nasaapiservice)
        {
            _nasaApiService = nasaapiservice;
        }

        public void OnGet()
        {
            epicImages = null;
        }

        public void OnPost()
        {
            DateTime selected_date = DateTime.Parse(Request.Form["date-epic"]);
            //Console.WriteLine(selected_date);
            //Console.ReadLine();
            epicImages = EpicImagesTask(selected_date).Result;
            ViewData["IsPost"] = true;
        }

        public async Task<List<EpicImage>> EpicImagesTask(DateTime date)
        {
            //Console.WriteLine(date.ToString("yyyy-MM-dd"));
            //Console.ReadLine();
            var epicresult = await _nasaApiService.GetEpicImagesAsync(date);

            return epicresult;
        }
    }
}
