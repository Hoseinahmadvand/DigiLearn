﻿using CoreModule.Application._Utilities;
using CoreModule.Facade.Courses;
using CoreModule.Query.Courses._DTOs;
using CoreModule.Query.HelperEntities._DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Pages;

public class CourseModel : PageModel
{
    private readonly ICourseFacade _courseFacade;

    public CourseModel(ICourseFacade courseFacade)
    {
        _courseFacade = courseFacade;
    }

    public CourseDto Course { get; set; }
    public List<StudentDto> MyProperty { get; set; }
    public async Task<IActionResult> OnGet(string slug)
    {
        var course = await _courseFacade.GetCourseBySlug(slug);
        if (course == null)
        {
            return NotFound();
        }
        var students = await _courseFacade.GetStudentsCours(course.Id);

        MyProperty=students;
        if (students == null)
        {
            students = new List<StudentDto>(); 
        }

        course.Students = MyProperty;
        Course = course;
     
        return Page();
    }
    public async Task<IActionResult> OnGetShowOnline(string slug, Guid sectionId, Guid token)
    {
        var course = await _courseFacade.GetCourseBySlug(slug);
        if (course == null)
        {
            return NotFound();
        }

        var section = course.Sections.First(f => f.Id == sectionId);
        var episode = section.Episodes.FirstOrDefault(f => f.Token == token);
        if (episode == null)
        {
            return NotFound();
        }


        return Content(CoreModuleDirectories.GetEpisodeFile(course.Id, token, episode.VideoName));
    }
    public async Task<IActionResult> OnGetDownloadEpisode(string slug, Guid sectionId, Guid token)
    {
        var course = await _courseFacade.GetCourseBySlug(slug);
        if (course == null)
        {
            return NotFound();
        }

        var section = course.Sections.First(f => f.Id == sectionId);
        var episode = section.Episodes.FirstOrDefault(f => f.Token == token);
        if (episode == null)
        {
            return NotFound();
        }

        var fileName = Path.Combine(Directory.GetCurrentDirectory(),
            CoreModuleDirectories.CourseEpisode(course.Id, token), episode.VideoName);
        var file = new FileStream(fileName, FileMode.Open);
        return File(file, "application/force-download", episode.VideoName);
    }
}
