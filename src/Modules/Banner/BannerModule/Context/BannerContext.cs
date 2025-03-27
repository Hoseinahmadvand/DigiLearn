using BannerModule.Domain;
using Microsoft.EntityFrameworkCore;

namespace BannerModule.Context;

class BannerContext : DbContext
{
    public BannerContext(DbContextOptions<BannerContext> option) : base(option)
    {
        
    }

    public DbSet<Banner> Banners { get; set; }
}
