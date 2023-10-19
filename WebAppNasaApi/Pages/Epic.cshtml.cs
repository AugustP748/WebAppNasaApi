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
        public Task<byte[]> ImageBytes;

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
            //ImageBytes = GetImageByte(selected_date, epicImages[0].image);
            //Console.WriteLine(ImageBytes);
            //Console.ReadLine();
            ViewData["IsPost"] = true;
        }

        public async Task<List<EpicImage>> EpicImagesTask(DateTime date_epic)
        {
            //Console.WriteLine(date.ToString("yyyy-MM-dd"));
            //Console.ReadLine();
            var epicresult = await _nasaApiService.GetEpicImagesAsync(date_epic);
            
            return epicresult;
        }

        public async Task<byte[]> GetImageByte(DateTime selected_data,string name_image)
        {
            var result = await _nasaApiService.GetImageURLEPIC(selected_data, name_image);
            return result;
        }
    }
}
