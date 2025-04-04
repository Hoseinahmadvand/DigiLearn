using CoreModule.Facade.Courses;
using CoreModule.Facade.Teachers;
using CoreModule.Query.Courses._DTOs;
using CoreModule.Query.Teachers._DTOs;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;

namespace DigiLearn.Web.Pages
{
    public class MasterModel : BaseRazorFilter<CourseFilterParams>
    {
        private readonly ITeacherFacade _teacherFacade;
        private readonly ICourseFacade _courseFacade;
        public MasterModel(ITeacherFacade teacherFacade, ICourseFacade courseFacade)
        {
            _teacherFacade = teacherFacade;
            _courseFacade = courseFacade;
        }
        public TeacherDto? Teacher { get; set; }
        public CourseFilterResult FilterResult { get; set; }
        public async Task<IActionResult> OnGetAsync(string username)
        {
            var teacher = await _teacherFacade.GetByUserName(username);
            if (teacher == null)
                return NotFound();

            Teacher = teacher;
            FilterParams.TeacherId = teacher?.Id;
            FilterResult = await _courseFacade.GetCourseFilter(FilterParams);
            return Page();
        }
    }
}
