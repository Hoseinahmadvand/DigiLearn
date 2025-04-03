using CoreModule.Facade.Courses;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Pages.Profile;

public class MyCoursesModel : PageModel
{
    private readonly ICourseFacade _courseFacade;

    public MyCoursesModel(ICourseFacade courseFacade)
    {
        _courseFacade = courseFacade;
    }

    public List<CourseCardViewModel?> Courses { get; set; }
    public async Task OnGet()
    {
        var courses = await _courseFacade.GetUserCourses(User.GetUserId());

        Courses = courses.Select( s => new CourseCardViewModel
        {
            Title = s.Title,
            Slug = s.Slug,
            ImageName = s.ImageName,
            Price = s.Price,
            Duration = s.GetDuration(),
            Visit = 0,
            CommentCounts = 0,
            TeacherName = s.Teacher,
            Category = s.Category,
            CategorySlug = s.CategorySlug
        }).ToList();
    }
}
