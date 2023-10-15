using Microsoft.AspNetCore.Mvc;

namespace WebAppNasaApi.Models.Mars
{
    public class MarsFormModel
    {
        [BindProperty]
        public string rover_name { get; set; }
        [BindProperty]
        public DateTime earth_date { get; set; }

    }
}
