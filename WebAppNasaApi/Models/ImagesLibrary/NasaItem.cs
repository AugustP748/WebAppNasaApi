namespace WebAppNasaApi.Models.ImagesLibrary
{
    public class NasaItem
    {
        public IEnumerable<NasaData> data { get; set; }
        public IEnumerable<NasaLinks> links { get; set; }

    }
}
