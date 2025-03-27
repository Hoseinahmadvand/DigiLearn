using CoreModule.Facade.Courses;
using CoreModule.Facade.Teachers;
using CoreModule.Query.Courses._DTOs;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;

namespace DigiLearn.Web.Pages.Profile.Teacher.Courses.Sections;


[ServiceFilter(typeof(TeacherActionFilter))]
public class IndexModel : BaseRazor
{
    private ICourseFacade _courseFacade;
    private readonly ITeacherFacade _teacherFacade;
    public IndexModel(ICourseFacade courseFacade, ITeacherFacade teacherFacade)
    {
        _courseFacade = courseFacade;
        _teacherFacade = teacherFacade;
    }

    public CourseDto Course { get; set; }
    public async Task<IActionResult> OnGet(Guid courseId)
    {
        var course = await _courseFacade.GetCourseById(courseId);
        var teacher = await _teacherFacade.GetByUserId(User.GetUserId());

        if (course == null || course.TeacherId != teacher!.Id)
        {
            return RedirectToPage("../Index");
        }

        Course = course;
        return Page();
    }
}