﻿namespace WebAppNasaApi.Models
{
    public class Apod
    {
        public string explanation { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string hdurl { get; set; }
        public string copyright { get; set; }
        public DateTime date { get; set; }
        public string media_type { get; set; }
    }
}
