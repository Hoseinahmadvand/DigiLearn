using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Courses._DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.HelperEntities.GetUserCourses;

public record GetUserCoursesQuery(Guid UserId) : IQuery<List<CourseFilterData?>>;

class GetUserCoursesQueryHandler : IQueryHandler<GetUserCoursesQuery, List<CourseFilterData?>>
{
    private readonly QueryContext _context;

    public GetUserCoursesQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<List<CourseFilterData?>> Handle(GetUserCoursesQuery request, CancellationToken cancellationToken)
    {
        var courseIds = await _context.CourseStudents
            .Where(cs => cs.UserId == request.UserId)
            .Select(cs => cs.CourseId)
            .ToListAsync(cancellationToken);

        // دریافت اطلاعات دوره‌های مرتبط
        var courses = await _context.Courses
            .Where(c => courseIds.Contains(c.Id))
            .AsNoTracking()
            .Include(c => c.Teacher.User)
            .Include(c => c.Category)
            .Include(c => c.SubCategory)
            .Include(c => c.Sections)
            .ThenInclude(c => c.Episodes)
            .Select(s => new CourseFilterData
            {
                Id = s.Id,
                CreationDate = s.CreationDate,
                ImageName = s.ImageName,
                Title = s.Title,
                Slug = s.Slug,
                Price = s.Price,
                CourseStatus = s.Status,
                Category = s.Category.Title,
                CategorySlug = s.Category.Slug,
                Teacher = $"{s.Teacher.User.Name} {s.Teacher.User.Family}",
                Sections = s.Sections.Select(r => new CourseSectionDto
                {
                    Id = r.Id,
                    CreationDate = r.CreationDate,
                    CourseId = r.CourseId,
                    Title = r.Title,
                    DisplayOrder = r.DisplayOrder,
                    Episodes = r.Episodes.Select(e => new EpisodeDto
                    {
                        Id = e.Id,
                        CreationDate = e.CreationDate,
                        SectionId = e.SectionId,
                        Title = e.Title,
                        EnglishTitle = e.EnglishTitle,
                        Token = e.Token,
                        TimeSpan = e.TimeSpan,
                        VideoName = e.VideoName,
                        AttachmentName = e.AttachmentName,
                        IsActive = e.IsActive,
                        IsFree = e.IsFree
                    }).ToList()
                }).ToList()

            })
            .ToListAsync(cancellationToken);

        return courses;
    }
}
