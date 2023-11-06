using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAppNasaApi.Models.ImagesLibrary
{
    public class NasaImageModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string href { get; set; }
        public string? media_type { get; set; }
        public string nasa_id { get; set; }


    }
}
