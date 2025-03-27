using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BannerModule.Utils;

public class BannerDirectories
{
    public static string BannerImage = "wwwroot/banner/images";
    public static string GetBannerImage(string imageName)
    {
        return $"{BannerImage.Replace("wwwroot", "")}/{imageName}";
    }
}

