using CoreModule.Domain.Courses.Enums;
using CoreModule.Facade.Courses;
using CoreModule.Query.Courses._DTOs;
using DigiLearn.Web.Infrastructure.RazorUtils;

namespace DigiLearn.Web.Pages;

public class CoursesModel : BaseRazorFilter<CourseFilterParams>
{
    private ICourseFacade _courseFacade;

    public CoursesModel(ICourseFacade courseFacade)
    {
        _courseFacade = courseFacade;
    }

    public CourseFilterResult FilterResult { get; set; }
    public async Task OnGet()
    {
        FilterParams.ActionStatus = CourseActionStatus.Active;
        FilterParams.TeacherId = null;
        FilterResult = await _courseFacade.GetCourseFilter(FilterParams);
    }
}
