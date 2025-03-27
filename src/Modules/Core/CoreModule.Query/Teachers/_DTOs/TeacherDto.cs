using Common.Query;
using CoreModule.Domain.Teachers.Enums;
using CoreModule.Query._DTOs;

namespace CoreModule.Query.Teachers._DTOs;

public class TeacherDto : BaseDto
{
    public string UserName { get; set; }
    public string CvFileName { get; set; }
    public TeacherStatus Status { get; set; }
    public CoreModuleUserDto User { get; set; }
    public string? Description { get; set; }
    public string? Biography { get; set; }
}