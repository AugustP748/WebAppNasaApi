namespace WebAppNasaApi.Models.Mars
{
    public class MarsRoverPhoto
    {
        public int id { get; set; }
        public int sol { get; set; }
        public Camera camera { get; set; }
        public string img_src { get; set; }
        public DateTime earth_date { get; set; }
    }
}
