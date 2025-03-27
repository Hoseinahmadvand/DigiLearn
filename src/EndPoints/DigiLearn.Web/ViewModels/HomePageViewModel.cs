using BannerModule.Services.DTOs.Query;
using BlogModule.Services.DTOs.Query;

namespace DigiLearn.Web.ViewModels;

public class HomePageViewModel
{
    public List<CourseCardViewModel> LatestCourses { get; set; }
    public List<BlogPostFilterItemDto> LatestArticles { get; set; }
    public List<BannerDto> Slider { get; set; }
}

public class CourseCardViewModel
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public string ImageName { get; set; }
    public int Price { get; set; }
    public string Duration { get; set; }
    public int Visit { get; set; }
    public int CommentCounts { get; set; }
    public string TeacherName { get; set; }

    public string Category {  get; set; }
    public string CategorySlug {  get; set; }
}
