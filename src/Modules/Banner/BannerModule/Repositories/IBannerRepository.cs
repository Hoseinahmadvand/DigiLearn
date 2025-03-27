using BannerModule.Context;
using BannerModule.Domain;
using Common.Domain.Repository;
using Common.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BannerModule.Repositories;

interface IBannerRepository : IBaseRepository<Banner>
{
    void Delete(Banner banner);
    Task<List<Banner>> GetAll();
    Task<List<Banner>> GetAllSlider();
    Task<Banner> GetBannerByType(BannerPositon positon);
}

class BannerRepository: BaseRepository<Banner, BannerContext>, IBannerRepository
{
    public BannerRepository(BannerContext context) : base(context)
    {
    }

    public void Delete(Banner banner)
    {
        Context.Banners.Remove(banner);
    }

    public async Task<List<Banner>> GetAll()
    {
       return await Context.Banners.ToListAsync();
    }

    public async Task<List<Banner>> GetAllSlider()
    {
        return await Context.Banners.Where(b=>b.Positon==BannerPositon.اسلایدر).ToListAsync();
    }

    public async Task<Banner> GetBannerByType(BannerPositon positon)
    {
        return await Context.Banners.Where(p=>p.Positon==positon).OrderByDescending(b=>b.CreationDate).FirstOrDefaultAsync();
    }
}
