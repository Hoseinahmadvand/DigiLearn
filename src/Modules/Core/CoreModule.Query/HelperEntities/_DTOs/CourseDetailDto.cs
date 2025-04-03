namespace CoreModule.Query.HelperEntities._DTOs;

public class CourseDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<VideoDto> Videos { get; set; }
}

public class VideoDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public TimeSpan Duration { get; set; }
    // سایر خصوصیات ویدیو
}
public class StudentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Family { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Avatar { get; set; }
    public DateTime RegistrationDate { get; set; }
}
