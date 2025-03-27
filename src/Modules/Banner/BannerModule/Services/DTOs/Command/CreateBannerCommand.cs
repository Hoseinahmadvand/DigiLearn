using BannerModule.Domain;
using Microsoft.AspNetCore.Http;

namespace BannerModule.Services.DTOs.Command
{
    public class CreateBannerCommand
    {
        public string Title { get; set; }
        public IFormFile ImageName { get; set; }
        public string Url { get; set; }
        public string Alt { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public BannerPositon Positon { get; set; }

    } 
    public class EditBannerCommand
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public IFormFile? ImageName { get; set; }
        public string Url { get; set; }
        public string Alt { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public BannerPositon Positon { get; set; }

    }

}
