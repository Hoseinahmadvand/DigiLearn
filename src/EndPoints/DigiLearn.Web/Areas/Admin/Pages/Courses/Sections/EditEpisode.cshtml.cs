using DigiLearn.Web.Infrastructure.Utils.CustomValidation.IFormFile;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using CoreModule.Application.Courses.Episodes.Edit;
using CoreModule.Facade.Courses;
using CoreModule.Query.Courses._DTOs;
using DigiLearn.Web.Infrastructure.RazorUtils;

namespace DigiLearn.Web.Areas.Admin.Pages.Courses.Sections;


public class EditEpisodeModel : BaseRazor
{
    private readonly ICourseFacade _courseFacade;

    public EditEpisodeModel(ICourseFacade courseFacade)
    {
        _courseFacade = courseFacade;
    }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [BindProperty]
    public string Title { get; set; }


 
    public TimeSpan Time { get; set; }
    [Display(Name = "مدت زمان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [RegularExpression(@"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$", ErrorMessage = "لطفا زمان را با فرمت درست وارد کنید")]
    [BindProperty]
    public string TimeFormatted { get; set; } = string.Empty;


    [Display(Name = "ویدئو")]
    [FileType("mp4", ErrorMessage = "ویدئو نامعتبر است")]
    [BindProperty]
    public IFormFile? VideoFile { get; set; }


    [Display(Name = "فایل ضمیمه")]
    [FileType("rar", ErrorMessage = "فایل ضمیمه نامعتبر است")]
    [BindProperty]

    public IFormFile? AttachmentFile { get; set; }

    [BindProperty]
    [Display(Name = "این قسمت رایگان است")]
    public bool IsFree { get; set; }


    [BindProperty]

    public bool IsActive { get; set; }
    [BindProperty]
    public string? VideoFileName { get; set; }

    public Guid? CourseId { get; set; }
    public EpisodeDto? EpisodeDto { get; set; } = new EpisodeDto();

    public async Task<IActionResult> OnGet(Guid episodeId, Guid courseId)
    {
        var episode = await _courseFacade.GetEpisodeById(episodeId);
        if (episode == null)
        {
            return RedirectToPage("../Index", new { courseId });
        }


        Title = episode.Title;
        IsActive = episode.IsActive;
        Time =episode.TimeSpan;
        TimeFormatted = episode.TimeSpan.ToString(@"hh\:mm\:ss"); // مقدار را تنظیم کن
        EpisodeDto = episode;
        CourseId = courseId;
        IsFree = episode.IsFree;
        return Page();
    }


    public async Task<IActionResult> OnPost(Guid episodeId, Guid courseId)
    {
        var episode = await _courseFacade.GetEpisodeById(episodeId);

        if (!TimeSpan.TryParseExact(TimeFormatted, @"hh\:mm\:ss", null, out var parsedTime))
        {
            ModelState.AddModelError("TimeFormatted", "فرمت زمان نادرست است. لطفاً به‌صورت hh:mm:ss وارد کنید.");
            return Page();
        }

        Time = parsedTime; // مقدار نهایی را تنظیم کن

        var result = await _courseFacade.EditEpisode(new EditEpisodeCommand()
        {
            Title = Title,
            AttachmentFile = AttachmentFile,
            CourseId = courseId,
            SectionId = episode.SectionId,
            IsActive = IsActive,
            Id = episodeId,
            VideoFile = VideoFile,
            TimeSpan = Time,
            IsFree = IsFree
        });

        return RedirectAndShowAlert(result, RedirectToPage("Index", new { courseId }));
    }

}