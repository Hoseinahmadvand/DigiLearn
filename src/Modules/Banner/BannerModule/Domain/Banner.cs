using Common.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace BannerModule.Domain;

[Table("Banner", Schema = "banner")]
internal class Banner : BaseEntity
{
    public string Title { get; set; }
    public string ImageName { get; set; }
    public string Url { get; set; }
    public string Alt { get; set; }
    public bool IsActive { get; set; }
    public int Order { get; set; }
    public BannerPositon Positon { get; set; }

}
public enum BannerPositon
{
    اسلایدر,
    بنر_1,
    بنر_2,
    بنر_3,
    بنر_4,
    بنر_5,
}

