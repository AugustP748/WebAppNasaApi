using Microsoft.EntityFrameworkCore;
using WebAppNasaApi.Models;
using WebAppNasaApi.Models.ImagesLibrary;

namespace WebAppNasaApi.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Apod> ApodDB { get; set; }
        public DbSet<NasaImageModel> NasaImageDB { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOption) 
            : base(contextOption)
        { 
            
        }



    }
}
