using CoreModule.Facade.Teachers;
using CoreModule.Query.Teachers._DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Pages;

public class MastersModel : PageModel
{
    private readonly ITeacherFacade _teacherFacade;

    public MastersModel(ITeacherFacade teacherFacade)
    {
        _teacherFacade = teacherFacade;
    }

    public List<TeacherDto> Teachers { get; set; }
    public async Task OnGet()
    {
        Teachers=await _teacherFacade.GetList();
    }
}
