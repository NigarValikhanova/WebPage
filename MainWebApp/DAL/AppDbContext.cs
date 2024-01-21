using MainWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MainWebApp.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Slider>().HasData(
                new Slider
                {
                    Id = 1,
                    CollectionName = "New Trend",
                    Title = "Summer Sale Stylish",
                    Description = "",
                    ImageUrl = "slideshow-character1.png"
                }
                ); ; 
            base.OnModelCreating(modelBuilder);
        }
    }
}
