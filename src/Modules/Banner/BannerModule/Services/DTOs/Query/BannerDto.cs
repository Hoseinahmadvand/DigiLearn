using BannerModule.Domain;

namespace BannerModule.Services.DTOs.Query;

public class BannerDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ImageName { get; set; }
    public string Url { get; set; }
    public string Alt { get; set; }
    public bool IsActive { get; set; }
    public int Order { get; set; }
    public BannerPositon Positon { get; set; }

}
