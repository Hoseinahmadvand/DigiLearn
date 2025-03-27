using AutoMapper;
using BannerModule.Domain;
using BannerModule.Repositories;
using BannerModule.Services.DTOs.Command;
using BannerModule.Services.DTOs.Query;
using BannerModule.Utils;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.SecurityUtil;

namespace BannerModule.Services;

public interface IBannerService
{
    Task<OperationResult> CreateBanner(CreateBannerCommand command);
    Task<OperationResult> EditBanner(EditBannerCommand command);
    Task<OperationResult> DeleteBanner(Guid bannerId);
    Task<List<BannerDto>> GetAllBanners();
    Task<List<BannerDto>> GetSliders();
    Task<BannerDto> GetBannerById(Guid id);
    Task<BannerDto> GetBannerByType(BannerPositon position);
}

class BannerService : IBannerService
{
    private readonly IMapper _mapper;
    private readonly IBannerRepository _bannerRepository;

    private readonly ILocalFileService _localFileService;
    public BannerService(IMapper mapper, IBannerRepository bannerRepository, ILocalFileService localFileService)
    {
        _mapper = mapper;

        _bannerRepository = bannerRepository;
        _localFileService = localFileService;
    }

    public async Task<OperationResult> CreateBanner(CreateBannerCommand command)
    {
        var banner = _mapper.Map<Banner>(command);
        if (command.ImageName.IsImage() == false)
            return OperationResult.Error("عکس وارد شده نامعتبر است");

        var imageName = await _localFileService.SaveFileAndGenerateName(command.ImageName, BannerDirectories.BannerImage);
        banner.ImageName = imageName;
        _bannerRepository.Add(banner);
        await _bannerRepository.Save();
        return OperationResult.Success();
    }

    public async Task<OperationResult> DeleteBanner(Guid bannerId)
    {
        var banner = await _bannerRepository.GetAsync(bannerId);
        if (banner == null)
            return OperationResult.NotFound();
        _bannerRepository.Delete(banner);
        await _bannerRepository.Save();
        _localFileService.DeleteFile(BannerDirectories.BannerImage, banner.ImageName);
        return OperationResult.Success();

    }

    public async Task<OperationResult> EditBanner(EditBannerCommand command)
    {
        var banner = await _bannerRepository.GetAsync(command.Id);
        if (banner == null)
            return OperationResult.NotFound();

        if (command.ImageName != null)
            if (command.ImageName.IsImage() == false)
                return OperationResult.Error("عکس وارد شده نامعتبر است");
            else
            {
                var imageName = await _localFileService.SaveFileAndGenerateName(command.ImageName, BannerDirectories.BannerImage);
                banner.ImageName = imageName;
            }
        banner.Positon = command.Positon;
        banner.Url = command.Url;
        banner.Title = command.Title;
        banner.Order = command.Order;
    
        banner.IsActive = command.IsActive;
        _bannerRepository.Update(banner);
        await _bannerRepository.Save();
        return OperationResult.Success();
    }

    public async Task<List<BannerDto>> GetAllBanners()
    {
        var banners = await _bannerRepository.GetAll();
        return _mapper.Map<List<BannerDto>>(banners);
    }

    public async Task<BannerDto> GetBannerById(Guid id)
    {
        var banner = await _bannerRepository.GetAsync(id);
        return _mapper.Map<BannerDto>(banner);
    }

    public async Task<BannerDto> GetBannerByType(BannerPositon position)
    {
        var banner = await _bannerRepository.GetBannerByType(position);
        return _mapper.Map<BannerDto>(banner);
    }

    public async Task<List<BannerDto>> GetSliders()
    {
        var banners = await _bannerRepository.GetAllSlider();
        return _mapper.Map<List<BannerDto>>(banners);
    }
}
