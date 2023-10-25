using Microsoft.EntityFrameworkCore;
using WebAppNasaApi.Models;

namespace WebAppNasaApi.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Apod> ApodDB { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOption) 
            : base(contextOption)
        { 
            
        }



    }
}
